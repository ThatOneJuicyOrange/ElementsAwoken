using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using System.IO;
using ElementsAwoken.Items.ItemSets.Carapace;
using ElementsAwoken.Projectiles.NPCProj;

namespace ElementsAwoken.NPCs.Random
{
    public class GiantToad : ModNPC
    {
        private float aiState
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float jumpTimer
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float aiTimer
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float agag
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 32;

            npc.aiStyle = -1;
            npc.lifeMax = 3000;
            npc.damage = 100;
            npc.defense = 28;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath53;

            npc.value = Item.buyPrice(0, 1, 50, 0);
            npc.knockBackResist = 0.5f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Giant Toad");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 4000;
            npc.damage = 120;
            npc.defense = 32;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 5000;
                npc.damage = 170;
                npc.defense = 38;
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return
            !spawnInfo.player.ZoneTowerStardust &&
            !spawnInfo.player.ZoneTowerSolar &&
            !spawnInfo.player.ZoneTowerVortex &&
            !spawnInfo.player.ZoneTowerNebula &&
            !spawnInfo.playerInTown &&
            !spawnInfo.invasion &&
            spawnInfo.player.ZoneJungle &&
            NPC.downedMoonlord ? 0.2f : 0f;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter += 1;
            if (npc.velocity.Y != 0) npc.frame.Y = 0;
            else if (aiState == 1)
            {
                if (npc.frameCounter > 6)
                {
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    npc.frameCounter = 0.0;
                }
                if (npc.frame.Y < frameHeight * 3) npc.frame.Y = frameHeight * 3;
                if (npc.frame.Y > frameHeight * 4) npc.frame.Y = frameHeight * 4;
            }
            else
            {
                if (npc.frameCounter > 20)
                {
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    npc.frameCounter = 0.0;
                }
                if (npc.frame.Y > frameHeight * 2)
                {
                    npc.frame.Y = frameHeight * 1;
                }
            }
        }
        public override void NPCLoot()
        {

        }
        public override void AI()
        {
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active) npc.TargetClosest(true);
            Player P = Main.player[npc.target];
            /*Tile tile = Framing.GetTileSafely((int)(npc.Center.X + (npc.width / 2 + 4) * npc.direction) / 16, (int)npc.Bottom.Y / 16);
            if (Main.tileSolid[tile.type] && tile.active() && npc.oldVelocity.Y != 0f)
            {
                npc.velocity.X += 2 * npc.direction;
            }*/
            if (aiState == 0)
            {
                aiTimer--;
                if (jumpTimer >= 60) npc.velocity.X = 4 * npc.direction;
                if (npc.velocity.Y == 0)
                {
                    jumpTimer++;
                    npc.TargetClosest(true);

                    if (jumpTimer == 60)npc.velocity.Y = -7f;
                    else if (jumpTimer > 60) jumpTimer = 0;
                    npc.velocity.X = npc.velocity.X * 0.8f;
                    if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                    {
                        npc.velocity.X = 0f;
                    }
                    if (aiTimer <= 0)
                    {
                        if (Vector2.Distance(npc.Center, P.Center) < 300)
                        {
                            aiState = 1;
                            aiTimer = 0;
                        }
                    }
                }
                FallThroughPlatforms();
            }
            else
            {
                npc.velocity.X = 0;
                aiTimer++;
                if (aiTimer == 6 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 toadMouth = npc.Center + new Vector2(12 * npc.direction, 8);
                    float Speed = 12;
                    float rotation = (float)Math.Atan2(toadMouth.Y - P.Center.Y, toadMouth.X - P.Center.X);
                    Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                    Projectile proj = Main.projectile[Projectile.NewProjectile(toadMouth, projSpeed, ProjectileType<ToadTongue>(), 100, 0f, Main.myPlayer)];
                    proj.localAI[0] = 60;
                }
                if (aiTimer > 60)
                {
                    aiState = 0;
                    aiTimer = 120;
                }
            }
            if (npc.wet)
            {
                npc.TargetClosest(true);

                if (npc.collideY)
                {
                    npc.velocity.Y = -2f;
                }
                if (npc.velocity.Y > 2f)
                {
                    npc.velocity.Y = npc.velocity.Y * 0.9f;
                }
                npc.velocity.Y = npc.velocity.Y - 0.5f;
                if (npc.velocity.Y < -4f)
                {
                    npc.velocity.Y = -4f;
                }
                if (npc.velocity.X > -4 && npc.velocity.X < 4) npc.velocity.X += npc.direction * 0.2f;
            }
        }
        private void FallThroughPlatforms()
        {
            Player P = Main.player[npc.target];
            Vector2 platform = npc.Bottom / 16;
            Tile platformTile = Framing.GetTileSafely((int)platform.X, (int)platform.Y);
            if (TileID.Sets.Platforms[platformTile.type] && npc.Bottom.Y < P.Bottom.Y && platformTile.active()) npc.position.Y += 0.3f;
        }
    }
}
