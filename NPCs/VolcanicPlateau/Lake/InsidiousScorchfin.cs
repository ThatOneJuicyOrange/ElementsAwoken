using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.NPCProj;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace ElementsAwoken.NPCs.VolcanicPlateau.Lake
{
    public class InsidiousScorchfin : ModNPC
    {
        private bool voidBreak = true;
        public override void SetDefaults()
        {
            npc.width = 196;
            npc.height = 46;

            npc.aiStyle = -1;

            npc.damage = 40;
            npc.defense = 20;
            npc.lifeMax = 600;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(0, 0, 2, 0);
            npc.knockBackResist = 0f;

            npc.lavaImmune = true;
            npc.noGravity = true;

            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffType<Buffs.Debuffs.Incineration>()] = true;

            banner = npc.type;
            bannerItem = mod.ItemType("DragonSlimeBanner");
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "Insidious Scorchfins are relatives of order Acribellaformes that live in the lava lake of the plateau. They are fairly hostile and will charge at thought-to-be prey without verifying what they are first.";
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Insidious Scorchfin");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 65;
            npc.defense = 24;
            npc.lifeMax = 800;
            if (MyWorld.awakenedMode)
            {
                npc.damage = 80;
                npc.defense = 30;
                npc.lifeMax = 1200;
            }
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffType<Buffs.Debuffs.Incineration>(), 300, false);
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = mod.GetTexture("NPCs/VolcanicPlateau/Lake/" + GetType().Name + "_Glow");
            Rectangle frame = new Rectangle(0, npc.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[npc.type]);
            Vector2 origin = new Vector2(texture.Width * 0.5f, (texture.Height / Main.npcFrameCount[npc.type]) * 0.5f);
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, 4), frame, Color.White, npc.rotation, origin, npc.scale, effects, 0.0f);
        }
        public override void NPCLoot()
        {
            // Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Drakonite"), Main.rand.Next(1, 3));
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (!npc.lavaWet && npc.ai[2] == 1) npc.velocity.Y *= 0.5f;// to stop the lava jitter
            return base.PreDraw(spriteBatch, drawColor);
        }
        public static void ScorchfinAI(NPC npc, float spdScale = 1f)
        {
            Vector2 swordPoint = npc.Center + new Vector2(86 * npc.direction, 8).RotatedBy(npc.rotation);

            if (GetInstance<Config>().debugMode)
            {
                Dust dust = Main.dust[Dust.NewDust(swordPoint, 2, 2, DustID.PinkFlame)];
                dust.noGravity = true;
            }
            npc.spriteDirection = npc.direction;
            if (npc.direction == 0)
            {
                npc.TargetClosest(true);
            }
            EAUtils.PushOtherEntities(npc);
            npc.ai[1]++;
            if (npc.ai[1] < 600)
            {
                if (npc.lavaWet)
                {
                    bool flag14 = false;
                    npc.TargetClosest(false);
                    Player player = Main.player[npc.target];
                    if (player.lavaWet && !player.dead)  flag14 = true;

                    if (!flag14)
                    {
                        if (npc.collideX)
                        {
                            npc.velocity.X = npc.velocity.X * -1f;
                            npc.direction *= -1;
                            npc.netUpdate = true;
                        }
                        if (npc.collideY)
                        {
                            npc.netUpdate = true;
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y = Math.Abs(npc.velocity.Y) * -1f;
                                npc.directionY = -1;
                                npc.ai[0] = -1f;
                            }
                            else if (npc.velocity.Y < 0f)
                            {
                                npc.velocity.Y = Math.Abs(npc.velocity.Y);
                                npc.directionY = 1;
                                npc.ai[0] = 1f;
                            }
                        }
                    }
                    if (flag14)
                    {
                        npc.TargetClosest(true);

                        if (npc.velocity.X > 0f && npc.direction < 0)
                        {
                            npc.velocity.X = npc.velocity.X * 0.95f;
                        }
                        if (npc.velocity.X < 0f && npc.direction > 0)
                        {
                            npc.velocity.X = npc.velocity.X * 0.95f;
                        }
                        float spdX = 0.25f * spdScale;
                        float spdY = 0.2f * spdScale;
                        npc.velocity.X = npc.velocity.X + (float)npc.direction * spdX;
                        npc.velocity.Y = npc.velocity.Y + (float)npc.directionY * spdY;

                        float maxVelX = 12;
                        float maxVelY = 8;
                        if (npc.velocity.X > maxVelX)
                        {
                            npc.velocity.X = maxVelX;
                        }
                        if (npc.velocity.X < -maxVelX)
                        {
                            npc.velocity.X = -maxVelX;
                        }
                        if (npc.velocity.Y > maxVelY)
                        {
                            npc.velocity.Y = maxVelY;
                        }
                        if (npc.velocity.Y < -maxVelY)
                        {
                            npc.velocity.Y = -maxVelY;
                        }

                    }
                    else
                    {
                        npc.velocity.X = npc.velocity.X + (float)npc.direction * 0.1f;
                        if (npc.velocity.X < -1f || npc.velocity.X > 1f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.95f;
                        }
                        if (npc.ai[0] == -1f)
                        {
                            npc.velocity.Y = npc.velocity.Y - 0.01f;
                            if ((double)npc.velocity.Y < -0.3)
                            {
                                npc.ai[0] = 1f;
                            }
                        }
                        else
                        {
                            npc.velocity.Y = npc.velocity.Y + 0.01f;
                            if ((double)npc.velocity.Y > 0.3)
                            {
                                npc.ai[0] = -1f;
                            }
                        }
                        int num258 = (int)(npc.position.X + (float)(npc.width / 2)) / 16;
                        int num259 = (int)(npc.position.Y + (float)(npc.height / 2)) / 16;
                        if (Main.tile[num258, num259 - 1] == null)
                        {
                            Main.tile[num258, num259 - 1] = new Tile();
                        }
                        if (Main.tile[num258, num259 + 1] == null)
                        {
                            Main.tile[num258, num259 + 1] = new Tile();
                        }
                        if (Main.tile[num258, num259 + 2] == null)
                        {
                            Main.tile[num258, num259 + 2] = new Tile();
                        }
                        if (Main.tile[num258, num259 - 1].liquid > 128)
                        {
                            if (Main.tile[num258, num259 + 1].active())
                            {
                                npc.ai[0] = -1f;
                            }
                            else if (Main.tile[num258, num259 + 2].active())
                            {
                                npc.ai[0] = -1f;
                            }
                        }
                        if ((double)npc.velocity.Y > 0.4 || (double)npc.velocity.Y < -0.4)
                        {
                            npc.velocity.Y = npc.velocity.Y * 0.95f;
                        }
                    }
                }
                else
                {
                    if (npc.velocity.Y == 0f)
                    {
                        npc.velocity.Y = (float)Main.rand.Next(-50, -20) * 0.1f;
                        npc.velocity.X = (float)Main.rand.Next(-20, 20) * 0.1f;
                        npc.netUpdate = true;
                    }
                    npc.velocity.Y = npc.velocity.Y + 0.3f;
                    if (npc.velocity.Y > 10f)
                    {
                        npc.velocity.Y = 10f;
                    }
                    npc.ai[0] = 1f;
                }
                npc.rotation = npc.velocity.Y * (float)npc.direction * 0.1f;
                if ((double)npc.rotation < -0.2)
                {
                    npc.rotation = -0.2f;
                }
                if ((double)npc.rotation > 0.2)
                {
                    npc.rotation = 0.2f;
                    return;
                }
            }
            else if (npc.ai[1] <= 1200)
            {
                if (!npc.lavaWet) npc.ai[1] = 0; // to stop them attacking out of lava
                    npc.TargetClosest(false);
                Player player = Main.player[npc.target];
                npc.rotation = npc.velocity.ToRotation() + (npc.direction == -1 ? (float)Math.PI : 0);
                if (Vector2.Distance(player.Center, swordPoint) > 20)
                {
                    npc.direction = Math.Sign(npc.velocity.X);
                    if (npc.lavaWet)
                    {
                        if (npc.ai[3] == 1) npc.ai[3] = 2;
                        if (npc.ai[3] != 2)
                        {
                            Vector2 toTarget = new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y);
                            toTarget.Normalize();
                            if (npc.velocity.X > 0f && npc.direction < 0)
                            {
                                npc.velocity.X = npc.velocity.X * 0.95f;
                            }
                            if (npc.velocity.X < 0f && npc.direction > 0)
                            {
                                npc.velocity.X = npc.velocity.X * 0.95f;
                            }
                            float spdX = 0.8f * spdScale;
                            float spdY = 0.7f * spdScale;
                            float fullSpd = 20 * spdScale;
                            if (Math.Abs(npc.Center.X - player.Center.X) > 500)
                            {
                                npc.velocity.X += toTarget.X * spdX;
                                if (Math.Abs(npc.Center.Y - player.Center.Y) < 600)
                                {
                                    if (npc.velocity.Y < 6) npc.velocity.Y += 0.16f;
                                }
                                else npc.velocity.Y *= 0.95f;
                            }
                            else
                            {
                                npc.velocity.X = toTarget.X * fullSpd;
                                npc.velocity.Y += toTarget.Y * spdY;
                            }

                            float maxVelX = 16;
                            float maxVelY = 18;
                            if (npc.velocity.X > maxVelX)
                            {
                                npc.velocity.X = maxVelX;
                            }
                            if (npc.velocity.X < -maxVelX)
                            {
                                npc.velocity.X = -maxVelX;
                            }
                            if (npc.velocity.Y > maxVelY)
                            {
                                npc.velocity.Y = maxVelY;
                            }
                            if (npc.velocity.Y < -maxVelY)
                            {
                                npc.velocity.Y = -maxVelY;
                            }
                        }
                    }
                    else
                    {
                        npc.ai[3] = 1;
                        npc.velocity.Y += 0.16f;
                    }
                }
                else
                {
                    if (npc.ai[1] < 1100) npc.ai[1] = 1100;
                    Vector2 toTarget = new Vector2(npc.Center.X - swordPoint.X, npc.Center.Y - swordPoint.Y);
                    npc.Center = player.Center + toTarget;
                    player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " got impaled"), npc.damage, 0);
                }
                if (npc.ai[3] == 2 || npc.ai[1] >= 1200)
                {
                    if (Vector2.Distance(player.Center, swordPoint) <= 20) npc.velocity = Vector2.Zero;
                    npc.ai[1] = 1200;
                    npc.ai[3] = 0;
                }

            }
            else
            {
                npc.velocity.Y += 0.16f;
                if (npc.ai[1] > 1200 + Main.rand.Next(30, 60))
                {
                    if (npc.life < npc.lifeMax) npc.ai[1] = 450;
                    else npc.ai[1] = 0;
                    npc.netUpdate = true;
                }
            }
            npc.ai[2] = npc.lavaWet ? 1 : 0;
            NPCsGLOBAL.GoThroughPlatforms(npc);
        }
        public override void AI()
        {
            if (voidBreak && Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (Main.rand.NextBool(PlateauNPCs.voidBreakChance) || MyWorld.plateauWeather == 3)
                {
                    npc.active = false;
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<VoidbrokenScorchfin>());
                }
                voidBreak = false;
            }
            ScorchfinAI(npc);
        }
    }
}
