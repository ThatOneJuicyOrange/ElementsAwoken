using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Azana
{
    public class AzanaInfection : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 34;

            projectile.hostile = true;
            projectile.tileCollide = false;

            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 240;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infection");
            Main.projFrames[projectile.type] = 4;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
            Texture2D tex = Main.projectileTexture[projectile.type];
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Rectangle rectangle = new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type]);
                sb.Draw(tex, drawPos, rectangle, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void AI()
        {
            projectile.rotation += 0.01f;

            projectile.localAI[0]++;
            if (projectile.localAI[0] <= 180)
            {
                projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(0.5f));
                projectile.velocity *= 1.005f;

                projectile.ai[1]--;
                if (projectile.ai[1] <= 0)
                {
                    Player P = Main.player[(int)projectile.ai[0]];
                    float Speed = 15f;
                    float rotation = (float)Math.Atan2(projectile.Center.Y - P.Center.Y, projectile.Center.X - P.Center.X);
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("AzanaGlob"), projectile.damage, 0f, 0);
                    projectile.ai[1] = Main.rand.Next(30, 120);
                    Main.PlaySound(SoundID.Item99, projectile.Center);
                }
            }
            if (projectile.timeLeft < 30)
            {
                projectile.alpha += 255 / 30;
                if (projectile.alpha >= 255) projectile.Kill();
            }
        }
    }
}