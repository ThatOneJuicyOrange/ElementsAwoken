using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using System.IO;
using ElementsAwoken.Projectiles.NPCProj;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using ElementsAwoken.Items.ItemSets.Radia;
using ElementsAwoken.Buffs.Debuffs;

namespace ElementsAwoken.Events.RadiantRain.Enemies
{
    public class RadiantWarrior : ModNPC
    {
        private float aiState
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 36;
            npc.height = 50;

            npc.aiStyle = -1;
            npc.lifeMax = 7600;
            npc.damage = 100;
            npc.defense = 40;

            //animationType = NPCID.Skeleton;

            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath6;

            npc.value = Item.buyPrice(0, 3, 0, 0);
            npc.knockBackResist = 0.5f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Warrior");
            Main.npcFrameCount[npc.type] = 10;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffType<Starstruck>(), 300);
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 15000;
            npc.damage = 150;
            npc.defense = 50;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 20000;
                npc.damage = 200;
                npc.defense = 65;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            if (aiState <= 0)
            {
                npc.frameCounter += Math.Abs(npc.velocity.X);
                if (npc.frameCounter > 12 && npc.frame.Y != frameHeight * 9)
                {
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    npc.frameCounter = 0.0;
                }
                if (npc.frame.Y > frameHeight * 4)   npc.frame.Y = 0;
            }
            else
            {
                if (npc.frame.Y < frameHeight * 4) npc.frame.Y = frameHeight * 4;
                npc.frameCounter += 1;
                if (npc.frameCounter > 8 && npc.frame.Y != frameHeight * 9)
                {
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    npc.frameCounter = 0.0;
                    if (npc.frame.Y == frameHeight * 5 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int projDamage = Main.expertMode ? MyWorld.awakenedMode ? 150 : 100 : 75;
                        Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center.X + 30 * npc.spriteDirection, npc.Center.Y, 0, 0, ProjectileType<WarriorSlice>(), projDamage, 0f, Main.myPlayer, npc.whoAmI, 0f)];
                        proj.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                    }
                }

                if (npc.frame.Y > frameHeight * 9)   npc.frame.Y = frameHeight * 9;

                if (npc.frameCounter > 10)
                {
                    aiState = -90;
                    npc.frame.Y = 0;
                }
            }


        }
        public override void NPCLoot()
        {
            if (Main.rand.NextBool()) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Radia>());
            if (Main.rand.NextBool(20)) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<RadiantKatana>());
        }
        public override void AI()
        {
            npc.TargetClosest(false);
            Player P = Main.player[npc.target];
            if (aiState <= 0)
            {
                if (aiState < 0) aiState++;
                if (Math.Abs(P.Center.X - npc.Center.X) < 200 && Math.Abs(P.Center.Y - npc.Center.Y) < 30 && aiState == 0 && (npc.velocity.Y ==0 || MyWorld.awakenedMode))
                {
                    aiState = 1;
                    npc.velocity.X += Math.Sign(P.Center.X - npc.Center.X) * 6;
                    npc.velocity.Y -= 3f;
                }
                npc.direction = Math.Sign(P.Center.X - npc.Center.X);
                if (npc.Bottom.Y - P.Bottom.Y >= 160 && npc.Bottom.Y - P.Bottom.Y <= 320 && Math.Abs(P.Center.X - npc.Center.X) < 100 && npc.velocity.Y == 0)
                {
                    npc.velocity.Y -= MathHelper.Lerp(10, 14.5f, (npc.Center.Y - P.Center.Y - 160) / 160);
                }
                CustomAI_3();
            }
            else
            {
                if (npc.velocity.Y == 0)  npc.velocity.X *= 0.9f;
            }
        }
        private void CustomAI_3()
        {
            bool flag3 = false;
            if (npc.velocity.X == 0f)
            {
                flag3 = true;
            }
            if (npc.justHit)
            {
                flag3 = false;
            }
            int num35 = 60;
            bool flag4 = false;
            bool flag5 = false;
            bool flag6 = false;
            bool flag7 = true;

            if (!flag6 & flag7)
            {
                if (npc.velocity.Y == 0f && ((npc.velocity.X > 0f && npc.direction < 0) || (npc.velocity.X < 0f && npc.direction > 0)))
                {
                    flag4 = true;
                }
                if ((npc.position.X == npc.oldPosition.X || npc.ai[3] >= (float)num35) | flag4)
                {
                    npc.ai[3] += 1f;
                }
                else if ((double)Math.Abs(npc.velocity.X) > 0.9 && npc.ai[3] > 0f)
                {
                    npc.ai[3] -= 1f;
                }
                if (npc.ai[3] > (float)(num35 * 10))
                {
                    npc.ai[3] = 0f;
                }
                if (npc.justHit)
                {
                    npc.ai[3] = 0f;
                }
                if (npc.ai[3] == (float)num35)
                {
                    npc.netUpdate = true;
                }
            }

            float speed = 0.1f;
            if (MyWorld.awakenedMode) speed = 0.15f;
            if (npc.velocity.X < -1.5f || npc.velocity.X > 1.5f)
            {
                if (npc.velocity.Y == 0f)
                {
                    npc.velocity *= 0.88f;
                }
            }
            else if (npc.velocity.X < 2f && npc.direction == 1)
            {
                npc.velocity.X = npc.velocity.X + speed;
                if (npc.velocity.X > 2f)
                {
                    npc.velocity.X = 2f;
                }
            }
            else if (npc.velocity.X > -2f && npc.direction == -1)
            {
                npc.velocity.X = npc.velocity.X - speed;
                if (npc.velocity.X < -2f)
                {
                    npc.velocity.X = -2f;
                }
            }


            bool flag22 = false;
            if (npc.velocity.Y == 0f)
            {
                int num161 = (int)(npc.position.Y + (float)npc.height + 7f) / 16;
                int arg_A8FB_0 = (int)npc.position.X / 16;
                int num162 = (int)(npc.position.X + (float)npc.width) / 16;
                for (int num163 = arg_A8FB_0; num163 <= num162; num163++)
                {
                    if (Main.tile[num163, num161] == null)
                    {
                        return;
                    }
                    if (Main.tile[num163, num161].nactive() && Main.tileSolid[(int)Main.tile[num163, num161].type])
                    {
                        flag22 = true;
                        break;
                    }
                }
            }
            if (npc.velocity.Y >= 0f)
            {
                StepUpTiles();
            }
            if (flag22)
            {
                int num170 = (int)((npc.position.X + (float)(npc.width / 2) + (float)(15 * npc.direction)) / 16f);
                int num171 = (int)((npc.position.Y + (float)npc.height - 15f) / 16f);
                //if (npc.type == 257)
                {
                    num170 = (int)((npc.position.X + (float)(npc.width / 2) + (float)((npc.width / 2 + 16) * npc.direction)) / 16f);
                }
                if (Main.tile[num170, num171] == null)
                {
                    Main.tile[num170, num171] = new Tile();
                }
                if (Main.tile[num170, num171 - 1] == null)
                {
                    Main.tile[num170, num171 - 1] = new Tile();
                }
                if (Main.tile[num170, num171 - 2] == null)
                {
                    Main.tile[num170, num171 - 2] = new Tile();
                }
                if (Main.tile[num170, num171 - 3] == null)
                {
                    Main.tile[num170, num171 - 3] = new Tile();
                }
                if (Main.tile[num170, num171 + 1] == null)
                {
                    Main.tile[num170, num171 + 1] = new Tile();
                }
                if (Main.tile[num170 + npc.direction, num171 - 1] == null)
                {
                    Main.tile[num170 + npc.direction, num171 - 1] = new Tile();
                }
                if (Main.tile[num170 + npc.direction, num171 + 1] == null)
                {
                    Main.tile[num170 + npc.direction, num171 + 1] = new Tile();
                }
                if (Main.tile[num170 - npc.direction, num171 + 1] == null)
                {
                    Main.tile[num170 - npc.direction, num171 + 1] = new Tile();
                }
                Main.tile[num170, num171 + 1].halfBrick();
                if ((Main.tile[num170, num171 - 1].nactive() && (Main.tile[num170, num171 - 1].type == 10 || Main.tile[num170, num171 - 1].type == 388)) & flag5)
                {
                    npc.ai[2] += 1f;
                    npc.ai[3] = 0f;
                    if (npc.ai[2] >= 60f)
                    {
                        npc.velocity.X = 0.5f * (float)(-(float)npc.direction);
                        int num172 = 5;
                        if (Main.tile[num170, num171 - 1].type == 388)
                        {
                            num172 = 2;
                        }
                        npc.ai[1] += (float)num172;

                        npc.ai[2] = 0f;
                        bool flag23 = false;
                        if (npc.ai[1] >= 10f)
                        {
                            flag23 = true;
                            npc.ai[1] = 10f;
                        }
                        WorldGen.KillTile(num170, num171 - 1, true, false, false);
                        if ((Main.netMode != NetmodeID.MultiplayerClient || !flag23) && flag23 && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            if (Main.tile[num170, num171 - 1].type == 10)
                            {
                                bool flag24 = WorldGen.OpenDoor(num170, num171 - 1, npc.direction);
                                if (!flag24)
                                {
                                    npc.ai[3] = (float)num35;
                                    npc.netUpdate = true;
                                }
                                if (Main.netMode == 2 & flag24)
                                {
                                    NetMessage.SendData(19, -1, -1, null, 0, (float)num170, (float)(num171 - 1), (float)npc.direction, 0, 0, 0);
                                }
                            }
                            if (Main.tile[num170, num171 - 1].type == 388)
                            {
                                bool flag25 = WorldGen.ShiftTallGate(num170, num171 - 1, false);
                                if (!flag25)
                                {
                                    npc.ai[3] = (float)num35;
                                    npc.netUpdate = true;
                                }
                                if (Main.netMode == 2 & flag25)
                                {
                                    NetMessage.SendData(19, -1, -1, null, 4, (float)num170, (float)(num171 - 1), 0f, 0, 0, 0);
                                }
                            }
                        }
                    }
                }
                else
                {
                    int num173 = npc.spriteDirection;
                    if ((npc.velocity.X < 0f && num173 == -1) || (npc.velocity.X > 0f && num173 == 1))
                    {
                        if (npc.height >= 32 && Main.tile[num170, num171 - 2].nactive() && Main.tileSolid[(int)Main.tile[num170, num171 - 2].type])
                        {
                            if (Main.tile[num170, num171 - 3].nactive() && Main.tileSolid[(int)Main.tile[num170, num171 - 3].type])
                            {
                                npc.velocity.Y = -8f;
                                npc.netUpdate = true;
                            }
                            else
                            {
                                npc.velocity.Y = -7f;
                                npc.netUpdate = true;
                            }
                        }
                        else if (Main.tile[num170, num171 - 1].nactive() && Main.tileSolid[(int)Main.tile[num170, num171 - 1].type])
                        {
                            npc.velocity.Y = -6f;
                            npc.netUpdate = true;
                        }
                        else if (npc.position.Y + (float)npc.height - (float)(num171 * 16) > 20f && Main.tile[num170, num171].nactive() && !Main.tile[num170, num171].topSlope() && Main.tileSolid[(int)Main.tile[num170, num171].type])
                        {
                            npc.velocity.Y = -5f;
                            npc.netUpdate = true;
                        }
                        else if (npc.directionY < 0 && (!Main.tile[num170, num171 + 1].nactive() || !Main.tileSolid[(int)Main.tile[num170, num171 + 1].type]) && (!Main.tile[num170 + npc.direction, num171 + 1].nactive() || !Main.tileSolid[(int)Main.tile[num170 + npc.direction, num171 + 1].type]))
                        {
                            npc.velocity.Y = -8f;
                            npc.velocity.X = npc.velocity.X * 1.5f;
                            npc.netUpdate = true;
                        }
                        else if (flag5)
                        {
                            npc.ai[1] = 0f;
                            npc.ai[2] = 0f;
                        }
                        if ((npc.velocity.Y == 0f & flag3) && npc.ai[3] == 1f)
                        {
                            npc.velocity.Y = -5f;
                        }
                    }
                }
            }
            else if (flag5)
            {
                npc.ai[1] = 0f;
                npc.ai[2] = 0f;
            }
        }
        private void StepUpTiles()
        {
            int num164 = 0;
            if (npc.velocity.X < 0f)
            {
                num164 = -1;
            }
            if (npc.velocity.X > 0f)
            {
                num164 = 1;
            }
            Vector2 position2 = npc.position;
            position2.X += npc.velocity.X;
            int num165 = (int)((position2.X + (float)(npc.width / 2) + (float)((npc.width / 2 + 1) * num164)) / 16f);
            int num166 = (int)((position2.Y + (float)npc.height - 1f) / 16f);
            if (Main.tile[num165, num166] == null)
            {
                Main.tile[num165, num166] = new Tile();
            }
            if (Main.tile[num165, num166 - 1] == null)
            {
                Main.tile[num165, num166 - 1] = new Tile();
            }
            if (Main.tile[num165, num166 - 2] == null)
            {
                Main.tile[num165, num166 - 2] = new Tile();
            }
            if (Main.tile[num165, num166 - 3] == null)
            {
                Main.tile[num165, num166 - 3] = new Tile();
            }
            if (Main.tile[num165, num166 + 1] == null)
            {
                Main.tile[num165, num166 + 1] = new Tile();
            }
            if (Main.tile[num165 - num164, num166 - 3] == null)
            {
                Main.tile[num165 - num164, num166 - 3] = new Tile();
            }
            if ((float)(num165 * 16) < position2.X + (float)npc.width && (float)(num165 * 16 + 16) > position2.X && ((Main.tile[num165, num166].nactive() && !Main.tile[num165, num166].topSlope() && !Main.tile[num165, num166 - 1].topSlope() && Main.tileSolid[(int)Main.tile[num165, num166].type] && !Main.tileSolidTop[(int)Main.tile[num165, num166].type]) || (Main.tile[num165, num166 - 1].halfBrick() && Main.tile[num165, num166 - 1].nactive())) && (!Main.tile[num165, num166 - 1].nactive() || !Main.tileSolid[(int)Main.tile[num165, num166 - 1].type] || Main.tileSolidTop[(int)Main.tile[num165, num166 - 1].type] || (Main.tile[num165, num166 - 1].halfBrick() && (!Main.tile[num165, num166 - 4].nactive() || !Main.tileSolid[(int)Main.tile[num165, num166 - 4].type] || Main.tileSolidTop[(int)Main.tile[num165, num166 - 4].type]))) && (!Main.tile[num165, num166 - 2].nactive() || !Main.tileSolid[(int)Main.tile[num165, num166 - 2].type] || Main.tileSolidTop[(int)Main.tile[num165, num166 - 2].type]) && (!Main.tile[num165, num166 - 3].nactive() || !Main.tileSolid[(int)Main.tile[num165, num166 - 3].type] || Main.tileSolidTop[(int)Main.tile[num165, num166 - 3].type]) && (!Main.tile[num165 - num164, num166 - 3].nactive() || !Main.tileSolid[(int)Main.tile[num165 - num164, num166 - 3].type]))
            {
                float num167 = (float)(num166 * 16);
                if (Main.tile[num165, num166].halfBrick())
                {
                    num167 += 8f;
                }
                if (Main.tile[num165, num166 - 1].halfBrick())
                {
                    num167 -= 8f;
                }
                if (num167 < position2.Y + (float)npc.height)
                {
                    float num168 = position2.Y + (float)npc.height - num167;
                    float num169 = 16.1f;
                    if (num168 <= num169)
                    {
                        npc.gfxOffY += npc.position.Y + (float)npc.height - num167;
                        npc.position.Y = num167 - (float)npc.height;
                        if (num168 < 9f)
                        {
                            npc.stepSpeed = 1f;
                        }
                        else
                        {
                            npc.stepSpeed = 2f;
                        }
                    }
                }
            }
            FallThroughPlatforms();
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
