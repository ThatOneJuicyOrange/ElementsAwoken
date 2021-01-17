using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.ProjectileNPCs
{
    public class SolarFragment : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 26;
            npc.height = 26;
            npc.aiStyle = 99;
            npc.damage = 60;
            npc.defense = 0;
            npc.lifeMax = 1;
            npc.HitSound = null;
            npc.DeathSound = null;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.alpha = 0;
            npc.knockBackResist = 0f;
            NPCID.Sets.TrailCacheLength[npc.type] = 20;
            NPCID.Sets.TrailingMode[npc.type] = 7;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solar Fragment");
        }
        
        public override bool PreDraw(SpriteBatch spritebatch, Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Vector2 vector11 = new Vector2((float)(Main.npcTexture[npc.type].Width / 2), (float)(Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type] / 2));
            Color color9 = Lighting.GetColor((int)((double)npc.position.X + (double)npc.width * 0.5) / 16, (int)(((double)npc.position.Y + (double)npc.height * 0.5) / 16.0));
            float num66 = 0f;
            float num67 = Main.NPCAddHeight(npc.whoAmI);
            Texture2D texture = Main.npcTexture[npc.type];
            Vector2 vector39 = npc.Center - Main.screenPosition;

            vector39 -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[npc.type])) * npc.scale / 2f;
            vector39 += vector11 * npc.scale + new Vector2(0f, num66 + num67 + npc.gfxOffY);
            texture = Main.npcTexture[npc.type];
            Main.spriteBatch.Draw(texture, vector39, new Rectangle?(npc.frame), npc.GetAlpha(color9), npc.rotation, vector11, npc.scale, spriteEffects, 0f);
            float num143 = 1f / (float)npc.oldPos.Length * 0.7f;
            int num144 = npc.oldPos.Length - 1;
            while (num144 >= 0f)
            {
                float num145 = (float)(npc.oldPos.Length - num144) / (float)npc.oldPos.Length;
                Color color34 = Color.Pink;
                color34 *= 1f - num143 * (float)num144 / 1f;
                color34.A = (byte)((float)color34.A * (1f - num145));
                Main.spriteBatch.Draw(texture, vector39 + npc.oldPos[num144] - npc.position, new Rectangle?(), color34, npc.oldRot[num144], vector11, npc.scale * MathHelper.Lerp(0.3f, 1.1f, num145), spriteEffects, 0f);
                num144--;
            }
            return false;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Rectangle hitbox2 = npc.Hitbox;
                for (int num67 = 0; num67 < npc.oldPos.Length; num67 += 3)
                {
                    hitbox2.X = (int)npc.oldPos[num67].X;

                    hitbox2.Y = (int)npc.oldPos[num67].Y;
                    for (int i = 0; i < 5; i++)
                    {
                        int num69 = Utils.SelectRandom<int>(Main.rand, new int[]
                        {
                            6,
                            259,
                            158
                        });
                        int num70 = Dust.NewDust(hitbox2.TopLeft(), npc.width, npc.height, num69, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 1f);
                        Main.dust[num70].alpha = 200;
                        Dust dust = Main.dust[num70];
                        dust.velocity *= 2.4f;
                        dust = Main.dust[num70];
                        dust.scale += Main.rand.NextFloat();
                    }
                }
            }
        }
        public override void AI()
        {
            if (npc.velocity.Y == 0f && npc.ai[0] == 0f)
            {
                npc.ai[0] = 1f;
                npc.ai[1] = 0f;
                npc.netUpdate = true;
                return;
            }
            if (npc.ai[0] == 1f)
            {
                npc.velocity = Vector2.Zero;
                npc.position = npc.oldPosition;
                float[] var_9_49F8F_cp_0 = npc.ai;
                int var_9_49F8F_cp_1 = 1;
                float num244 = var_9_49F8F_cp_0[var_9_49F8F_cp_1];
                var_9_49F8F_cp_0[var_9_49F8F_cp_1] = num244 + 1f;
                if (npc.ai[1] >= 5f)
                {
                    npc.HitEffect(0, 9999.0);
                    npc.active = false;
                }
                return;
            }
            npc.velocity.Y = npc.velocity.Y + 0.01f;
            if (npc.velocity.Y > 12f)
            {
                npc.velocity.Y = 12f;
            }
            npc.rotation = npc.velocity.ToRotation() - 1.57079637f;

            if (npc.ai[3] == 0)
            {
                if (npc.localAI[0] == 0f)
                {
                    npc.localAI[0] = 1f;
                    for (int i = 0; i < 13; i++)
                    {
                        int num1489 = Dust.NewDust(npc.position, npc.width, npc.height, 6, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f, 90, default(Color), 2.5f);
                        Main.dust[num1489].noGravity = true;
                        Main.dust[num1489].fadeIn = 1f;
                        Dust dust = Main.dust[num1489];
                        dust.velocity *= 4f;
                        Main.dust[num1489].noLight = true;
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    if (Main.rand.Next(3) < 2)
                    {
                        int num1491 = Dust.NewDust(npc.position, npc.width, npc.height, 6, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f, 90, default(Color), 2.5f);
                        Main.dust[num1491].noGravity = true;
                        Dust dust = Main.dust[num1491];
                        dust.velocity *= 0.2f;
                        Main.dust[num1491].fadeIn = 1f;
                        if (Main.rand.Next(6) == 0)
                        {
                            dust = Main.dust[num1491];
                            dust.velocity *= 30f;
                            Main.dust[num1491].noGravity = false;
                            Main.dust[num1491].noLight = true;
                        }
                        else
                        {
                            Main.dust[num1491].velocity = npc.DirectionFrom(Main.dust[num1491].position) * Main.dust[num1491].velocity.Length();
                        }
                    }
                }
            }
            return;

        }

    }
}