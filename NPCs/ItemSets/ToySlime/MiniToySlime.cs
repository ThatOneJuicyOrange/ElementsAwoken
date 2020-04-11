using System;
using System.Collections.Generic;
using System.IO;
using ElementsAwoken.Items.ItemSets.ToySlime;
using ElementsAwoken.NPCs;
using ElementsAwoken.Projectiles.NPCProj.ToySlime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.NPCs.ItemSets.ToySlime
{
    public class MiniToySlime : ModNPC
	{
        public bool dropBlocks = true;
        private float jumpTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float jumpNum
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float shootTimer
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        public override void SetDefaults()
		{
            npc.width = 32;
            npc.height = 22;

            npc.aiStyle = -1;

            npc.damage = 25;
			npc.lifeMax = 100;
            npc.defense = 6;
            npc.knockBackResist = 0.9f;

            npc.value = Item.buyPrice(0, 0, 20, 0);

			npc.lavaImmune = false;
			npc.noGravity = false;
			npc.noTileCollide = false;

			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;

            npc.alpha = 75;

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mini Toy Slime");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
            npc.damage = 45;
            npc.lifeMax = 200;
            npc.defense = 12;
            if (MyWorld.awakenedMode)
            {
                npc.damage = 70;
                npc.lifeMax = 400;
                npc.defense = 18;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (jumpTimer <= 40) npc.frameCounter++;
            if (npc.frameCounter > 8)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y > frameHeight * 1)
            {
                npc.frame.Y = 0;
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float spawnchance = 0.1f;
            MyPlayer modPlayer = spawnInfo.player.GetModPlayer<MyPlayer>();
            if (modPlayer.toySlimeChanceTimer > 0 && !NPC.AnyNPCs(NPCType<ToySlime>()))
            {
                spawnchance = 0.4f;
            }
            //SpawnCondition.OverworldDaySlime.Chance * 0.9f;

            return (spawnInfo.spawnTileY < Main.worldSurface)
                && NPC.downedBoss3
                && !spawnInfo.playerInTown
                && !spawnInfo.invasion
                ? spawnchance : 0f;
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            SlimeAI();
            if (MyWorld.awakenedMode)
            {
                shootTimer--;
                if (shootTimer <= 0)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1), ProjectileType<LegoBrick>(), npc.damage, 0f, Main.myPlayer, 0, 0);
                    shootTimer = Main.rand.Next(300, 600);
                }
            }
        }
        public override void NPCLoot()
        {
            int gelCount = Main.expertMode ? MyWorld.awakenedMode ? Main.rand.Next(2, 5) : Main.rand.Next(1, 4) : Main.rand.Next(1, 3);
            int item = Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Gel, gelCount);
            Main.item[item].color = new Color(0, 220, 40, 100);
            if(dropBlocks) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<BrokenToys>(), Main.rand.Next(1, 3));
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects spriteEffects = npc.direction != 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            var origin = npc.frame.Size() * 0.5f;
            Color color = drawColor;
            spriteBatch.Draw(mod.GetTexture("NPCs/ItemSets/ToySlime/MiniToySlimeToys"), npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame, color, npc.rotation, origin, npc.scale, spriteEffects, 0);
            return true;
        }
        private void SlimeAI()
        {
            npc.TargetClosest(true);
            if (npc.wet)
            {
                if (npc.collideY)
                {
                    npc.velocity.Y = -2f;
                }
                if (npc.velocity.Y < 0f && npc.ai[3] == npc.position.X)
                {
                    npc.direction *= -1;
                }
                if (npc.velocity.Y > 0f)
                {
                    npc.ai[3] = npc.position.X;
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
            }

            if (npc.velocity.Y == 0f)
            {
                if (npc.collideY && npc.oldVelocity.Y != 0f && Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    npc.position.X = npc.position.X - (npc.velocity.X + (float)npc.direction);
                }
                if (npc.ai[3] == npc.position.X)
                {
                    npc.direction *= -1;
                }
                npc.ai[3] = 0f;
                npc.velocity.X = npc.velocity.X * 0.8f;
                if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                {
                    npc.velocity.X = 0f;
                }
                jumpTimer--; // jump speed

                if (jumpTimer <= 0)
                {
                    jumpNum++;
                    npc.netUpdate = true;
                    if (jumpNum == 4)
                    {
                        npc.velocity.Y = -8f;
                        npc.velocity.X = npc.velocity.X + (float)(3 * npc.direction);
                        jumpTimer = 120f;
                        npc.ai[3] = npc.position.X;
                        jumpNum = 0;
                    }
                    else
                    {
                        npc.velocity.Y = -6f;
                        npc.velocity.X = npc.velocity.X + (float)(2 * npc.direction);
                        jumpTimer = 70f;
                    }
                }
                else if (jumpTimer >= -30f)
                {
                    return;
                }
            }
            else if (npc.target < 255 && ((npc.direction == 1 && npc.velocity.X < 3f) || (npc.direction == -1 && npc.velocity.X > -3f)))
            {
                if (npc.collideX && Math.Abs(npc.velocity.X) == 0.2f)
                {
                    npc.position.X = npc.position.X - 1.4f * (float)npc.direction;
                }
                if (npc.collideY && npc.oldVelocity.Y != 0f && Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    npc.position.X = npc.position.X - (npc.velocity.X + (float)npc.direction);
                }
                if ((npc.direction == -1 && (double)npc.velocity.X < 0.01) || (npc.direction == 1 && (double)npc.velocity.X > -0.01))
                {
                    npc.velocity.X = npc.velocity.X + 0.2f * (float)npc.direction;
                    return;
                }
                npc.velocity.X = npc.velocity.X * 0.93f;
            }
        }

    }
}