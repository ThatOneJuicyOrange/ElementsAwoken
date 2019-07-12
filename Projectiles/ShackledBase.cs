using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles
{
    public class ShackledBase : ModProjectile
    {
        float timeFlinging = 0;
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;

            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 100000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shackle");
        }
        public override void AI()
        {
            Player parent = Main.player[(int)projectile.ai[1]];
            MyPlayer modPlayer = parent.GetModPlayer<MyPlayer>(mod);

            if (modPlayer.forgedShackled <= 0)
            {
                projectile.Kill();
            }
            Vector2 toTarget = new Vector2(projectile.Center.X - parent.Center.X, projectile.Center.Y - parent.Center.Y);
            toTarget.Normalize();
            if (Vector2.Distance(parent.Center, projectile.Center) > 250)
            {
                parent.velocity += toTarget * 1.75f;
            }
            if (Vector2.Distance(parent.Center, projectile.Center) > 350)
            {
                parent.velocity += toTarget * 3;
            }
            if (Vector2.Distance(parent.Center, projectile.Center) > 450)
            {
                parent.velocity = toTarget * 10;
            }
            if (modPlayer.flingToShackle)
            {
                parent.velocity = toTarget * 20;
                if (Vector2.Distance(parent.Center, projectile.Center) < 10)
                {
                    modPlayer.flingToShackle = false;
                    parent.velocity = Vector2.Zero;
                }
                timeFlinging++;
                if (timeFlinging > 60)
                {
                    modPlayer.flingToShackle = false;
                }
            }
            else
            {
                timeFlinging = 0;
            }
            if (!parent.active)
            {
                projectile.Kill();
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Player parent = Main.player[(int)projectile.ai[1]];

            Texture2D texture = ModContent.GetTexture("ElementsAwoken/Projectiles/ShackledBaseChain");

            Vector2 position = projectile.Center;
            Vector2 mountedCenter = parent.MountedCenter;
            Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float num1 = (float)texture.Height;
            Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if ((double)vector2_4.Length() < (double)num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }

            return true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item37, (int)projectile.position.X, (int)projectile.position.Y);
        }
    }
}
    