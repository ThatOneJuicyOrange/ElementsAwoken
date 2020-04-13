using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Projectiles
{
    public class RadiantStorm: ModProjectile
    {
        public float scale = 1;
        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 96; // 210
            // projectile.aiStyle = 145;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Storm");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Starstruck>(), 300);
        }
        public override bool PreDraw(SpriteBatch spritebatch, Color lightColor)
        {
            float num260 = 300f;
            float num261 = projectile.ai[0];
            float scale9 = MathHelper.Clamp(num261 / 30f, 0f, 1f);
            if (num261 > num260 - 60f)
            {
                scale9 = MathHelper.Lerp(1f, 0f, (num261 - (num260 - 60f)) / 60f);
            }
            float num262 = 0.2f;
            Vector2 top = projectile.Top;
            Vector2 bottom = projectile.Bottom;
            Vector2.Lerp(top, bottom, 0.5f);
            Vector2 vector49 = new Vector2(0f, bottom.Y - top.Y);
            vector49.X = vector49.Y * num262;
            new Vector2(top.X - vector49.X / 2f, top.Y);
            Texture2D texture2D30 = Main.projectileTexture[projectile.type];
            Rectangle rectangle15 = texture2D30.Frame(1, 1, 0, 0);
            Vector2 origin7 = rectangle15.Size() / 2f;
            float num263 = -0.157079637f * num261 * (float)((projectile.velocity.X > 0f) ? -1 : 1);
            SpriteEffects effects2 = (projectile.velocity.X > 0f) ? SpriteEffects.FlipVertically : SpriteEffects.None;
            bool flag25 = projectile.velocity.X > 0f;
            Vector2 arg_CC17_0 = Vector2.UnitY;
            double arg_CC17_1 = (double)(num261 * 0.14f);
            Vector2 center = default(Vector2);
            Vector2 vector50 = arg_CC17_0.RotatedBy(arg_CC17_1, center);
            float num264 = 0f;
            float num265 = 5.01f + num261 / 150f * -0.9f;
            if (num265 < 4.11f)
            {
                num265 = 4.11f;
            }
            Color value35 = new Color(237, 95, 198, 127);
            Color color50 = new Color(83, 76, 145, 127);
            float num266 = num261 % 60f;
            if (num266 < 30f)
            {
                color50 *= Utils.InverseLerp(22f, 30f, num266, true);
            }
            else
            {
                color50 *= Utils.InverseLerp(38f, 30f, num266, true);
            }
            bool flag26 = color50 != Color.Transparent;
            for (float num267 = (float)((int)bottom.Y); num267 > (float)((int)top.Y); num267 -= num265)
            {
                num264 += num265;
                float num268 = num264 / vector49.Y;
                float num269 = num264 * 6.28318548f / -20f;
                if (flag25)
                {
                    num269 *= -1f;
                }
                float num270 = num268 - 0.35f;
                Vector2 arg_CDC3_0 = vector50;
                double arg_CDC3_1 = (double)num269;
                center = default(Vector2);
                Vector2 vector51 = arg_CDC3_0.RotatedBy(arg_CDC3_1, center);
                Vector2 vector52 = new Vector2(0f, num268 + 1f);
                vector52.X = vector52.Y * num262;
                Color color51 = Color.Lerp(Color.Transparent, value35, num268 * 2f);
                if (num268 > 0.5f)
                {
                    color51 = Color.Lerp(Color.Transparent, value35, 2f - num268 * 2f);
                }
                color51.A = (byte)((float)color51.A * 0.5f);
                color51 *= scale9;
                vector51 *= vector52 * 100f;
                vector51.Y = 0f;
                vector51.X = 0f;
                vector51 += new Vector2(bottom.X, num267) - Main.screenPosition;
                if (flag26)
                {
                    Color color52 = Color.Lerp(Color.Transparent, color50, num268 * 2f);
                    if (num268 > 0.5f)
                    {
                        color52 = Color.Lerp(Color.Transparent, color50, 2f - num268 * 2f);
                    }
                    color52.A = (byte)((float)color52.A * 0.5f);
                    color52 *= scale9;
                    Main.spriteBatch.Draw(texture2D30, vector51, new Rectangle?(rectangle15), color52, num263 + num269, origin7, (1f + num270) * scale * 0.8f, effects2, 0f);
                }
                Main.spriteBatch.Draw(texture2D30, vector51, new Rectangle?(rectangle15), color51, num263 + num269, origin7, (1f + num270) * scale, effects2, 0f);
            }
            return false;
        }

        public override void AI()
        {
            float num = 300f;
            if (projectile.soundDelay == 0)
            {
                projectile.soundDelay = -1;
                projectile.localAI[1] = Main.PlayTrackedSound(SoundID.DD2_BookStaffTwisterLoop, projectile.Center).ToFloat();
            }
            ActiveSound activeSound = Main.GetActiveSound(SlotId.FromFloat(projectile.localAI[1]));
            if (activeSound != null)
            {
                activeSound.Position = projectile.Center;
                activeSound.Volume = 1f - Math.Max(projectile.ai[0] - (num - 15f), 0f) / 15f;
            }
            else
            {
                float[] arg_9B_0 = projectile.localAI;
                int arg_9B_1 = 1;
                SlotId invalid = SlotId.Invalid;
                arg_9B_0[arg_9B_1] = invalid.ToFloat();
            }

            if (projectile.localAI[0] >= 16f && projectile.ai[0] < num - 15f)
            {
                projectile.ai[0] = num - 15f;
            }
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= num)
            {
                projectile.Kill();
            }
            Vector2 top = projectile.Top;
            Vector2 bottom = projectile.Bottom;
            Vector2 value = Vector2.Lerp(top, bottom, 0.5f);
            Vector2 vector = new Vector2(0f, bottom.Y - top.Y);


            if (projectile.ai[0] < num - 30f)
            {
                for (int j = 0; j < 1; j++)
                {
                    float value2 = -1f;
                    float value3 = 0.9f;
                    float amount = Main.rand.NextFloat();
                    Vector2 vector2 = new Vector2(MathHelper.Lerp(0.1f, 1f, Main.rand.NextFloat()), MathHelper.Lerp(value2, value3, amount));
                    vector2.X *= MathHelper.Lerp(2.2f, 0.6f, amount);
                    vector2.X *= -1f;
                    Vector2 value4 = new Vector2(6f, 10f);
                    Vector2 position2 = value + vector * vector2 * 0.5f + value4;
                    Dust dust = Main.dust[Dust.NewDust(position2, 0, 0, DustID.PinkFlame, 0f, 0f, 0, default(Color), 1f)];
                    dust.position = position2;
                    dust.fadeIn = 1.3f;
                    dust.scale = 0.87f;
                    dust.alpha = 211;
                    if (vector2.X > -1.2f)
                    {
                        dust.velocity.X = 1f + Main.rand.NextFloat();
                    }
                    dust.noGravity = true;
                    dust.velocity.Y = Main.rand.NextFloat() * -0.5f - 1.3f;
                    Dust expr_473_cp_0_cp_0 = dust;
                    expr_473_cp_0_cp_0.velocity.X = expr_473_cp_0_cp_0.velocity.X + projectile.velocity.X * 2.1f;
                    dust.noLight = true;
                }
            }
        }
    }
}