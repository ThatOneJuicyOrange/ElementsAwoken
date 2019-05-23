using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles
{
    public class SacredCrystalLaser : ModProjectile
    {	
        public override void SetDefaults()
        {
            projectile.width = 5;
            projectile.height = 5;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.penetrate = 4;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sacred Crystal");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            Lighting.AddLight((int)projectile.Center.X, (int)projectile.Center.Y, 0f, 0.1f, 0.5f);
            float trailLength = 75f;
            if (projectile.ai[1] == 0f)
            {
                projectile.localAI[0] += 3f;
                if (projectile.localAI[0] > trailLength)
                {
                    projectile.localAI[0] = trailLength;
                }
            }
            else
            {
                projectile.localAI[0] -= 3f;
                if (projectile.localAI[0] <= 0f)
                {
                    projectile.Kill();
                    return;
                }
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            switch ((int)projectile.ai[0])
            {
                case 0:
                    return new Color(255, 105, 45, 0);
                case 1:
                    return new Color(100, 255, 100, 0);
                case 2:
                    return new Color(73, 255, 251, 0);
                case 3:
                    return new Color(219, 112, 255, 0);
                default:
                    return new Color(219, 112, 255, 0);
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Color color25 = Lighting.GetColor((int)((double)projectile.position.X + (double)projectile.width * 0.5) / 16, (int)(((double)projectile.position.Y + (double)projectile.height * 0.5) / 16.0));


            Rectangle value7 = new Rectangle((int)Main.screenPosition.X - 500, (int)Main.screenPosition.Y - 500, Main.screenWidth + 1000, Main.screenHeight + 1000);
            float num148 = (float)(Main.projectileTexture[projectile.type].Width - projectile.width) * 0.5f + (float)projectile.width * 0.5f;
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            if (projectile.getRect().Intersects(value7))
            {
                Vector2 value8 = new Vector2(projectile.position.X - Main.screenPosition.X + num148, projectile.position.Y - Main.screenPosition.Y + (float)(projectile.height / 2) + projectile.gfxOffY);
                float num173 = 100f;
                float scaleFactor = 3f;
                if (projectile.ai[1] == 1f)
                {
                    num173 = (float)((int)projectile.localAI[0]);
                }
                for (int num174 = 1; num174 <= (int)projectile.localAI[0]; num174++)
                {
                    Vector2 value9 = Vector2.Normalize(projectile.velocity) * (float)num174 * scaleFactor;
                    Color color32 = projectile.GetAlpha(color25);
                    color32 *= (num173 - (float)num174) / num173;
                    color32.A = 0;
                    spriteBatch.Draw(Main.projectileTexture[projectile.type], value8 - value9, null, color32, projectile.rotation, new Vector2(num148, (float)(projectile.height / 2)), projectile.scale, spriteEffects, 0f);
                }
            }
            return false;
        }
    }
}