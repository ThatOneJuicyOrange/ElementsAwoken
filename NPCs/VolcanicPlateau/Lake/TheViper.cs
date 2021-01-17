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
    public class TheViper : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 214;
            npc.height = 94;

            npc.aiStyle = -1;

            npc.damage = 40;
            npc.defense = 20;
            npc.lifeMax = 10;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(0, 0, 2, 0);
            npc.knockBackResist = 0f;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;

            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffType<Buffs.Debuffs.Incineration>()] = true;

            banner = npc.type;
            bannerItem = mod.ItemType("DragonSlimeBanner");
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "The Viper is a nickname given to the powerful fish living in the Volcanic lake. It is, despite its appearance, not aggressive, though it will grow hostile if it perceives any creature to be invading its territory or if it feels threatened by a powerful presence.";
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Viper");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {

        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffType<Buffs.Debuffs.Incineration>(), 300, false);
        }
        public override void AI()
        {
            NPC parent = Main.npc[(int)npc.ai[0]];
            if (parent.active)
            {
                npc.Center = parent.Center + new Vector2(-143 * parent.spriteDirection, 0).RotatedBy(parent.rotation);
                npc.rotation = parent.rotation;
                npc.direction = parent.direction;
                npc.spriteDirection = parent.spriteDirection;
            }
            else npc.active = false;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
    }
    public class TheViperHead : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 76;
            npc.height = 94;

            npc.aiStyle = -1;

            npc.damage = 40;
            npc.defense = 20;
            npc.lifeMax = 100000;

            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(0, 0, 2, 0);
            npc.knockBackResist = 0f;

            npc.lavaImmune = true;
            npc.noGravity = true;

            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffType<Buffs.Debuffs.Incineration>()] = true;

            npc.reflectingProjectiles = true;

            banner = npc.type;
            bannerItem = mod.ItemType("DragonSlimeBanner");
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "The Viper is a nickname given to the powerful fish living in the Volcanic lake. It is, despite its appearance, not aggressive, though it will grow hostile if it perceives any creature to be invading its territory or if it feels threatened by a powerful presence.";
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Viper");
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
        public override void AI()
        {
            if (npc.direction == 0) npc.TargetClosest(true);

            npc.spriteDirection = npc.direction;
            bool aggressive = false;
            if (NPC.downedMoonlord) aggressive = true;
            else if (npc.life < npc.lifeMax)
            {
                aggressive = true;
            }
            if (npc.ai[1] == 0)
            {
                NPC body = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<TheViper>(), npc.whoAmI)];
                body.realLife = npc.whoAmI;
                body.ai[0] = npc.whoAmI;
                npc.ai[1]++;
            }
            npc.ai[2]++;
            if (npc.ai[2] < 600 || !aggressive)
            {
                if (npc.lavaWet)
                {
                    bool flag14 = false;
                    npc.TargetClosest(false);
                    Player player = Main.player[npc.target];
                    if (player.lavaWet && !player.dead) flag14 = true;

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
                        float spdX = 0.25f;
                        float spdY = 0.2f;
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
            }
            else if (npc.ai[2] == 600)
            {
                //if (Main.rand.NextBool()) npc.ai[2] = 1200;
            }
            else if (npc.ai[2] < 1200)
            {
                if (npc.velocity.X != 0) npc.direction = Math.Sign(npc.velocity.X);
                if (npc.lavaWet)
                {
                    npc.TargetClosest(false);
                    Player player = Main.player[npc.target];
                    Vector2 target = new Vector2(player.Center.X - 300 * npc.direction, player.Center.Y);
                    Vector2 toTarget = new Vector2(target.X - npc.Center.X, target.Y - npc.Center.Y);
                    toTarget.Normalize();
                    float fullSpd = 20 / MathHelper.Lerp(10, 1, MathHelper.Clamp(Math.Abs(npc.Center.X - target.X) / 500,0,1));
                        npc.velocity.X = toTarget.X * fullSpd;
                    if (npc.velocity.X > 0 && npc.velocity.X < 4) npc.velocity.X = 4;
                    if (npc.velocity.X < 0 && npc.velocity.X > -4) npc.velocity.X = -4;
                    if (npc.velocity.Y < 6) npc.velocity.Y -= 0.16f;

                    npc.rotation = npc.velocity.ToRotation() + (npc.direction == -1 ? (float)Math.PI : 0);
                }
                else
                {
                    npc.velocity.Y = 0f;
                    npc.velocity.X = 0;
                    if (npc.ai[2] % 6 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Main.PlaySound(SoundID.DD2_PhantomPhoenixShot, npc.position);
                        Vector2 vel = Vector2.One.RotatedBy(npc.rotation + (npc.direction == -1 ? (float)Math.PI / 2 : 0)).RotatedByRandom(MathHelper.ToRadians(30)) * 8;
                        vel.Y -= 4;
                        Projectile.NewProjectile(npc.Center, vel, ProjectileType<SolarFragmentProj>(), 50, 0f, Main.myPlayer, 1);
                    }
                }
                if (npc.ai[2] == 1199) npc.ai[2] = 601;
            }
            else if (npc.ai[2] > 1200)
            {

            }
        }
    }
}
