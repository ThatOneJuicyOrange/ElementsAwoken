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
        public int shootTimer = 20;
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 3;
            projectile.extraUpdates = 2;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Shade");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.01f) / 255f, ((255 - projectile.alpha) * 0.15f) / 255f, ((255 - projectile.alpha) * 0.05f) / 255f);
            projectile.scale -= 0.002f;
            if (projectile.scale <= 0f)
            {
                projectile.Kill();
            }
            if (projectile.ai[0] <= 3f)
            {
                projectile.ai[0] += 1f;
                return;
            }
            projectile.velocity.Y = projectile.velocity.Y + 0.075f;
            for (int num151 = 0; num151 < 3; num151++)
            {
                float num152 = projectile.velocity.X / 3f * (float)num151;
                float num153 = projectile.velocity.Y / 3f * (float)num151;
                int num154 = 14;
                int num155 = Dust.NewDust(new Vector2(projectile.position.X + (float)num154, projectile.position.Y + (float)num154), projectile.width - num154 * 2, projectile.height - num154 * 2, 60, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num155].noGravity = true;
                Main.dust[num155].velocity *= 0.1f;
                Main.dust[num155].velocity += projectile.velocity * 0.5f;
                Dust expr_6A04_cp_0 = Main.dust[num155];
                expr_6A04_cp_0.position.X = expr_6A04_cp_0.position.X - num152;
                Dust expr_6A1F_cp_0 = Main.dust[num155];
                expr_6A1F_cp_0.position.Y = expr_6A1F_cp_0.position.Y - num153;
            }
            if (Main.rand.Next(8) == 0)
            {
                int num156 = 16;
                int num157 = Dust.NewDust(new Vector2(projectile.position.X + (float)num156, projectile.position.Y + (float)num156), projectile.width - num156 * 2, projectile.height - num156 * 2, 60, 0f, 0f, 0, default(Color), 0.5f);
                Main.dust[num157].velocity *= 0.25f;
                Main.dust[num157].velocity += projectile.velocity * 0.5f;
                return;
            }
            shootTimer--;
            if (shootTimer <= 0)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 8f, mod.ProjectileType("CrimsonShadeHoming"), projectile.damage / 2, projectile.knockBack, projectile.owner, 0f, 0f);
                shootTimer = 24 + Main.rand.Next(6);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }
    }
}