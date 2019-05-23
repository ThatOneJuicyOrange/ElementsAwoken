using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class SoulswordSoul : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 2;
            projectile.timeLeft = 200;
            projectile.light = 1f;
            projectile.extraUpdates = 1;
            projectile.alpha = 100;
            Main.projFrames[projectile.type] = 3;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            aiType = ProjectileID.Bullet;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soulsword");
        }
        public override void Kill(int timeLeft)
        {
            int k;
            for (int i = 0; i < 50; i = k + 1)
            {
                int num292 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 180, projectile.velocity.X, projectile.velocity.Y, 0, default(Color), 1f);
                Dust dust = Main.dust[num292];
                dust.velocity *= 2f;
                Main.dust[num292].noGravity = true;
                Main.dust[num292].scale = 1.4f;
                k = i;
            }

            int numberProjectiles = 2 + Main.rand.Next(0,2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 value15 = new Vector2((float)Main.rand.Next(-12, 12), (float)Main.rand.Next(-12, 12));
                value15.X *= 0.25f;
                value15.Y *= 0.25f;
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value15.X, value15.Y, mod.ProjectileType("SoulswordSoul2"), projectile.damage / 2, 2f, projectile.owner, 0f, 0f);
            }
        }
        public override void AI()
        {
            int num748 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 180, 0f, 0f, 0, default(Color), 1f);
            Dust dust = Main.dust[num748];
            dust.velocity *= 0.1f;
            Main.dust[num748].scale = 1.3f;
            Main.dust[num748].noGravity = true;

            projectile.alpha += 4;

            if (projectile.alpha >= 255)
            {
                projectile.Kill();
            }
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 2)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 2)
                    projectile.frame = 0;
            }
            return true;
        }
    }
}