using System;
using System.Collections.Generic;
using System.IO;
using ElementsAwoken.NPCs;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using ElementsAwoken.Projectiles.NPCProj;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Items.ItemSets.Radia;
using ElementsAwoken.Buffs.Debuffs;

namespace ElementsAwoken.Events.RadiantRain.Enemies
{
    public class SparklingSlime : ModNPC
    {
        private float jumpTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float spikeTimer
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float aiTimer
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 22;

            npc.aiStyle = -1;

            npc.damage = 182;
            npc.defense = 20;
            npc.lifeMax = 6000;
            npc.knockBackResist = 0.2f;

            npc.value = Item.buyPrice(0, 0, 20, 0);

            npc.lavaImmune = false;
            npc.noGravity = false;
            npc.noTileCollide = false;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;

            npc.scale *= 1.2f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sparkling Slime");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 10000;
            npc.defense = 50;
            npc.damage = 210;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 12500;
                npc.defense = 65;
                npc.damage = 230;
            }
        }
        public override void NPCLoot()
        {
            if (Main.rand.NextBool()) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Radia>(), 1);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffType<Starstruck>(), 300);
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
            if (npc.frame.Y > frameHeight * 4)
            {
                npc.frame.Y = 0;
            }
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            bool aggro = false;
            if (!Main.dayTime || npc.life != npc.lifeMax || Main.slimeRain)
            {
                aggro = true;
            }

            if (aiTimer > 1f)
            {
                aiTimer -= 1f;
            }
            if (npc.wet)
            {
                if (npc.collideY)
                {
                    npc.velocity.Y = -2f;
                }
                if (npc.velocity.Y < 0f && npc.ai[3] == npc.position.X)
                {
                    npc.direction *= -1;
                    aiTimer = 200f;
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

                if (aiTimer == 1f & aggro)
                {
                    npc.TargetClosest(true);
                }
            }
            npc.aiAction = 0;
            if (aiTimer == 0f)
            {
                jumpTimer = -100f;
                aiTimer = 1f;
                npc.TargetClosest(true);
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
                    aiTimer = 200f;
                }
                npc.ai[3] = 0f;
                npc.velocity.X = npc.velocity.X * 0.8f;
                if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                {
                    npc.velocity.X = 0f;
                }
                if (aggro)
                {
                    jumpTimer += 1f;
                }
                jumpTimer += 1f;

                int num19 = 0;
                if (jumpTimer >= 0f)
                {
                    num19 = 1;
                }
                if (jumpTimer >= -1000f && jumpTimer <= -500f)
                {
                    num19 = 2;
                }
                if (jumpTimer >= -2000f && jumpTimer <= -1500f)
                {
                    num19 = 3;
                }
                if (num19 > 0)
                {
                    npc.netUpdate = true;
                    if (aggro && aiTimer == 1f)
                    {
                        npc.TargetClosest(true);
                    }
                    if (num19 == 3)
                    {
                        npc.velocity.Y = -8f;
                        npc.velocity.X = npc.velocity.X + (float)(3 * npc.direction);
                        jumpTimer = -200f;
                        npc.ai[3] = npc.position.X;
                    }
                    else
                    {
                        npc.velocity.Y = -6f;
                        npc.velocity.X = npc.velocity.X + (float)(2 * npc.direction);
                        jumpTimer = -120f;
                        if (num19 == 1)
                        {
                            jumpTimer -= 1000f;
                        }
                        else
                        {
                            jumpTimer -= 2000f;
                        }
                    }
                }
                else if (jumpTimer >= -30f)
                {
                    npc.aiAction = 1;
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
            if (aggro)
            {
                spikeTimer--;
                int projDamage = Main.expertMode ? MyWorld.awakenedMode ? 150 : 100 : 75;
                if (!npc.wet && !P.npcTypeNoAggro[npc.type])
                {
                    float num14 = P.Center.X - npc.Center.X;
                    float num15 = P.Center.Y - npc.Center.Y;
                    float num16 = (float)Math.Sqrt((double)(num14 * num14 + num15 * num15));
                    if (Main.expertMode && num16 < 200f && Collision.CanHit(npc.position, npc.width, npc.height, P.position, P.width, P.height) && npc.velocity.Y == 0f)
                    {
                        jumpTimer = -40f;
                        if (npc.velocity.Y == 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.9f;
                        }
                        if (Main.netMode != NetmodeID.MultiplayerClient && spikeTimer <= 0f)
                        {
                            for (int n = 0; n < 3; n++)
                            {
                                float speed = 6f;
                                Vector2 vector4 = new Vector2(0, -speed).RotatedByRandom(MathHelper.ToRadians(50));
                                Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector4.X, vector4.Y, ProjectileType<RadiantStar>(), npc.damage, 0f, Main.myPlayer, 0f, 0f)];
                                proj.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                                spikeTimer = 60f;
                            }
                        }
                    }
                    else if (num16 < 350f && Collision.CanHit(npc.position, npc.width, npc.height, P.position, P.width, P.height) && npc.velocity.Y == 0f)
                    {
                        jumpTimer = -40f;
                        if (npc.velocity.Y == 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.9f;
                        }
                        if (Main.netMode != NetmodeID.MultiplayerClient && spikeTimer <= 0f)
                        {
                            num15 = P.position.Y - npc.Center.Y - (float)Main.rand.Next(0, 200);
                            num16 = (float)Math.Sqrt((double)(num14 * num14 + num15 * num15));
                            num16 = 6.5f / num16;
                            num14 *= num16;
                            num15 *= num16;
                            spikeTimer = 30f;
                            Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, num14, num15, ProjectileType<RadiantStar>(), npc.damage, 0f, Main.myPlayer, 0f, 0f)];
                            proj.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                        }
                    }
                }
            }
        }
    }
}