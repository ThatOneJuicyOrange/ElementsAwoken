using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class CrimsonShadeP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.friendly = true;
            projectile.penetrate = 3;
            projectile.extraUpdates = 2;
            projectile.magic = true;
            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Shade");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.01f) / 255f, ((255 - projectile.alpha) * 0.15f) / 255f, ((255 - projectile.alpha) * 0.05f) / 255f);
            if (projectile.ai[0] <= 4f)
            {
                projectile.ai[1] = Main.rand.Next(4,60);
                projectile.ai[0] += 1f;
                return;
            }
            projectile.velocity.Y = projectile.velocity.Y + 0.075f;
            int num = ModContent.GetInstance<Config>().lowDust ? 1 : 3;
            for (int i = 0; i < num; i++)
            {
                int num155 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 127, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num155].noGravity = true;
                Main.dust[num155].velocity *= 0.1f;
                Main.dust[num155].velocity += projectile.velocity * 0.5f;
            }
            projectile.ai[1]--;
            if (projectile.ai[1] <= 0)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 8f, mod.ProjectileType("CrimsonShadeHoming"), projectile.damage / 2, projectile.knockBack, projectile.owner, 0f, 0f);
                projectile.ai[1] = Main.rand.Next(4, 60);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }
    }
}