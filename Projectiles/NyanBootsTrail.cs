using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles
{
    public class NyanBootsTrail : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.timeLeft = 10000;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 25;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            SpriteEffects spriteEffects = (projectile.velocity.X > 0f) ? SpriteEffects.FlipVertically : SpriteEffects.None;
            Main.instance.LoadProjectile(250);
            Texture2D texture2D32 = Main.projectileTexture[250];
            Vector2 origin9 = new Vector2((float)(texture2D32.Width / 2), 0f);
            Vector2 value36 = new Vector2((float)projectile.width, (float)projectile.height) / 2f;
            Color white2 =Color.White;
            white2.A = 127;
            for (int num271 = projectile.oldPos.Length - 1; num271 > 0; num271--)
            {
                Vector2 vector54 = projectile.oldPos[num271] + value36;
                if (!(vector54 == value36))
                {
                    Vector2 vector55 = projectile.oldPos[num271 - 1] + value36;
                    float rotation25 = (vector55 - vector54).ToRotation() - 1.57079637f;
                    Vector2 scale10 = new Vector2(1f, Vector2.Distance(vector54, vector55) / (float)texture2D32.Height);
                    Color color53 = white2 * (1f - (float)num271 / (float)projectile.oldPos.Length);
                    Texture2D arg_D4F2_1 = texture2D32;
                    Vector2 arg_D4F2_2 = vector54 - Main.screenPosition;
                    Rectangle? sourceRectangle2 = null;
                    sb.Draw(arg_D4F2_1, arg_D4F2_2, sourceRectangle2, color53, rotation25, origin9, scale10, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            projectile.position.X = player.Center.X;
            projectile.position.Y = player.Center.Y + 5;
            if (!player.active || !modPlayer.nyanBoots)
            {
                projectile.Kill();
            }

        }
    }
}