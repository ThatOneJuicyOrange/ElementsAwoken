using ElementsAwoken.NPCs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Events.VoidEvent.Enemies.Phase1
{
    public class ZergCaster : ModNPC
    {
        public bool casting = false;
        private float shootTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float teleportTimer
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float tpLocX
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float tpLocY
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 30;
            npc.height = 48; 
            
            npc.damage = 45;
            npc.defense = 25;
            npc.lifeMax = 500;
            npc.knockBackResist = 0f;

            npc.value = Item.buyPrice(0, 0, 20, 0);

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;

            banner = npc.type;
            bannerItem = mod.ItemType("ZergCasterBanner");

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = NPCsGLOBAL.ReducePierceDamage(damage, projectile);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zerg Caster");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 900;
            npc.defense = 35;
            npc.damage = 50;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 1750;
                npc.defense = 45;
                npc.damage = 60;
            }
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEssence"), 1); //Item spawn
            }
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidStone"), Main.rand.Next(3, 5)); //Item spawn
            }
            if (Main.rand.Next(40) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CastersCurse"), 1); //Item spawn
            }
        }
         
        public override void AI()
        {
            npc.TargetClosest(true);
            Player P = Main.player[npc.target];

            Vector2 direction = P.Center - npc.Center;
            npc.spriteDirection = Math.Sign(direction.X);
            npc.velocity.X = 0f;

            if (shootTimer > 0f)shootTimer -= 1f;

                casting = shootTimer <= 30;

            if (Main.netMode != NetmodeID.MultiplayerClient && shootTimer <= 0f)
            {
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);

                float Speed = 8f;
                float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("ZergFireball"), 30, 0f, 0);
                shootTimer = 120f;
            }

            teleportTimer--;
            if (teleportTimer <= 0f)
            {
                teleportTimer = 400f;
                Teleport(P, 0);
            }
        }
        private void Teleport(Player P, int attemptNum)
        {
            int playerTileX = (int)P.position.X / 16;
            int playerTileY = (int)P.position.Y / 16;
            int npcTileX = (int)npc.position.X / 16;
            int npcTileY = (int)npc.position.Y / 16;
            int maxTileDist = 20;
            bool foundNewLoc = false;
            int targetX = Main.rand.Next(playerTileX - maxTileDist, playerTileX + maxTileDist);
            for (int targetY = Main.rand.Next(playerTileY - maxTileDist, playerTileY + maxTileDist); targetY < playerTileY + maxTileDist; ++targetY)
            {
                if ((targetY < playerTileY - 4 ||
                    targetY > playerTileY + 4 ||
                    (targetX < playerTileX - 4 || targetX > playerTileX + 4)) &&
                    (targetY < npcTileY - 1 || targetY > npcTileY + 1 || (targetX < npcTileX - 1 || targetX > npcTileX + 1)) && Main.tile[targetX, targetY].nactive())
                {
                    bool flag2 = true;
                    if (Main.tile[targetX, targetY - 1].lava()) flag2 = false;

                    if (flag2 && Main.tileSolid[(int)Main.tile[targetX, targetY].type] && !Collision.SolidTiles(targetX - 1, targetX + 1, targetY - 4, targetY - 1))
                    {
                        tpLocX = (float)targetX;
                        tpLocY = (float)targetY;
                        foundNewLoc = true;
                        break;
                    }
                    if (ModContent.GetInstance<Config>().debugMode)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(new Vector2(targetX * 16, targetY * 16), 16, 16, 62)];
                            dust.noGravity = true;
                            dust.scale = 1f;
                            dust.velocity *= 0.1f;
                        }
                    }
                }
            }
            Main.PlaySound(SoundID.Item8, npc.position);
            if (tpLocX != 0 && tpLocY != 0 && foundNewLoc)
            {
                npc.position.X = (float)((double)tpLocX * 16.0 - (double)(npc.width / 2) + 8.0);
                npc.position.Y = tpLocY * 16f - (float)npc.height;
                npc.netUpdate = true;

                for (int i = 0; i < 20; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 6)];
                    dust.noGravity = true;
                    dust.scale = 1f;
                    dust.velocity *= 0.1f;
                }
            }
            else if (attemptNum < 10) Teleport(P, attemptNum + 1);
            else ElementsAwoken.DebugModeText("Failed TP");
        }
        public override void FindFrame(int frameHeight)
        {
            if (!casting)
            {
                npc.frame.Y = 0 * frameHeight;
            }
            if (casting)
            {
                npc.frame.Y = 1 * frameHeight;
            }
        }
    }
}