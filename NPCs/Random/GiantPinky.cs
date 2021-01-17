using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using ElementsAwoken.Items.ItemSets.ToySlime;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.NPCProj.ToySlime;
using ElementsAwoken.Projectiles.NPCProj;

namespace ElementsAwoken.NPCs.Random
{
    public class GiantPinky : ModNPC
    {
        private float jumpTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float state
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float waveTimer
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float slimeSpawnLife
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 22;

            npc.aiStyle = -1;

            npc.knockBackResist = 0.1f;
            npc.damage = 10;
            npc.defense = 5;
            npc.lifeMax = 600;

            animationType = NPCID.RainbowSlime;
            npc.value = Item.buyPrice(0, 3, 0, 0);

            npc.alpha = 60;

            npc.lavaImmune = false;
            npc.noGravity = false;
            npc.noTileCollide = false;

            npc.alpha = 75;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Giant Pinky");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 15;
            npc.lifeMax = 1200;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 1500;
                npc.damage = 20;
            }
        }

        public override void NPCLoot()
        {
            int item = Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Gel, Main.rand.Next(10, 21));
            Main.item[item].color = new Color(242, 73, 151, 100);
            int item2 = Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.PinkGel, Main.rand.Next(25, 51));
            if (Main.rand.NextBool(20)) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SlimeStaff, 1);
            MyWorld.downedGiantPinky = true;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData);
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            if (ElementsAwoken.aprilFools) npc.GivenName = "Ponky";

            float num234 = 1f;
            bool flag8 = false;
            if (slimeSpawnLife == 0)
            {
                npc.TargetClosest(true);
                P = Main.player[npc.target];
                if (Vector2.Distance(P.Center, npc.Center) < 300) slimeSpawnLife = 1;
            }
            else
            {
                if (npc.localAI[3] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    jumpTimer = -100f;
                    npc.localAI[3] = 1f;
                    npc.TargetClosest(true);
                    npc.netUpdate = true;
                }
                // despawning
                if (Main.player[npc.target].dead)
                {
                    npc.TargetClosest(true);
                    if (Main.player[npc.target].dead)
                    {
                        npc.timeLeft = 0;
                        if (Main.player[npc.target].Center.X < npc.Center.X)
                        {
                            npc.direction = 1;
                        }
                        else
                        {
                            npc.direction = -1;
                        }
                    }
                }

                if (npc.velocity.Y == 0f) // if its on the ground
                {
                    npc.velocity.X = npc.velocity.X * 0.8f;
                    if (npc.velocity.X > -0.1 && npc.velocity.X < 0.1)
                    {
                        npc.velocity.X = 0f;
                    }
                    if (!flag8)
                    {
                        // jumps faster when low health
                        jumpTimer += 2f;
                        if (npc.life < npc.lifeMax * 0.8) jumpTimer += 1f;
                        if (npc.life < npc.lifeMax * 0.6) jumpTimer += 1f;
                        if (npc.life < npc.lifeMax * 0.4) jumpTimer += 2f;
                        if (npc.life < npc.lifeMax * 0.2) jumpTimer += 3f;
                        if (npc.life < npc.lifeMax * 0.1) jumpTimer += 4f;

                        if (jumpTimer >= 0f)
                        {
                            npc.netUpdate = true;
                            npc.TargetClosest(true);
                            if (state == 3f) // big jump
                            {
                                npc.velocity.Y = -13f;
                                npc.velocity.X = npc.velocity.X + 3.5f * npc.direction;
                                jumpTimer = -200f;
                                state = 0f;
                            }
                            else if (state == 2f) // small jump
                            {
                                npc.velocity.Y = -6f;
                                npc.velocity.X = npc.velocity.X + 4.5f * npc.direction;
                                jumpTimer = -120f;
                                state += 1f;
                            }
                            else // medium jump
                            {
                                npc.velocity.Y = -8f;
                                npc.velocity.X = npc.velocity.X + 4f * npc.direction;
                                jumpTimer = -120f;
                                state += 1f;
                            }

                            for (int k = 0; k < 10; k++)
                            {
                                Dust dust = Main.dust[Dust.NewDust(npc.BottomLeft - new Vector2(0, 6), npc.width, 6, 4, 0, 0, 150, new Color(219, 0, 219, 100), 2f)];
                                dust.noGravity = true;
                                dust.velocity *= 0.5f;
                            }
                        }
                    }
                }
                else if (npc.target < 255 && ((npc.direction == 1 && npc.velocity.X < 3f) || (npc.direction == -1 && npc.velocity.X > -3f)))
                {
                    if ((npc.direction == -1 && npc.velocity.X < 0.1) || (npc.direction == 1 && npc.velocity.X > -0.1))
                    {
                        npc.velocity.X = npc.velocity.X + 0.2f * (float)npc.direction;
                    }
                    else
                    {
                        npc.velocity.X = npc.velocity.X * 0.93f;
                    }
                }

                waveTimer++;
                if (waveTimer > 600)
                {
                    jumpTimer = -100;
                    int between = Main.expertMode ? MyWorld.awakenedMode ? 30 : 45 : 60;
                    int num = Main.expertMode ? MyWorld.awakenedMode ? 5 : 4 : 3;
                    if (waveTimer % 60 == 0)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 95, 1, -0.5f);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center, new Vector2(3.5f, 0), ProjectileType<GiantPinkyWave>(), 0, 0, Main.myPlayer)];
                            proj.Bottom = npc.Bottom;
                            Projectile proj2 = Main.projectile[Projectile.NewProjectile(npc.Center, new Vector2(-3.5f, 0), ProjectileType<GiantPinkyWave>(), 0, 0, Main.myPlayer)];
                            proj2.Bottom = npc.Bottom;
                        }
                    }
                    if (waveTimer > 600 + between * num)
                    {
                        waveTimer = 0;
                    }
                }

                if (npc.life > 0)
                {
                    float npcLife = (float)npc.life / (float)npc.lifeMax;
                    npcLife = npcLife * 0.5f + 0.5f;
                    npcLife *= num234;
                    if (npcLife != npc.scale)
                    {
                        npc.position.X = npc.position.X + (float)(npc.width / 2);
                        npc.position.Y = npc.position.Y + (float)npc.height;
                        npc.scale = npcLife;
                        npc.width = (int)(74f * npc.scale);
                        npc.height = (int)(50f * npc.scale);
                        npc.position.X = npc.position.X - (float)(npc.width / 2);
                        npc.position.Y = npc.position.Y - (float)npc.height;
                    }
                }
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 4, hitDirection, -1f, 100, new Color(219, 0, 219, 100), 1f);
            }
            if (npc.life <= 0)
            {
                for (int k = 0; k < 50; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 4, (float)(2 * hitDirection), -2f, npc.alpha, new Color(219, 0, 219, 100), 1f);
                }
            }
        }
    }
}