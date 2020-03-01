using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class WindfallFire : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.penetrate = 2;
            projectile.timeLeft = 125;
            projectile.extraUpdates = 3;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Windfall");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.15f) / 255f, ((255 - projectile.alpha) * 0.45f) / 255f, ((255 - projectile.alpha) * 0.05f) / 255f);
            if (projectile.timeLeft > 125)
            {
                projectile.timeLeft = 125;
            }
            if (projectile.ai[0] > 12f)
            {
                if (Main.rand.Next(3) == 0)
                {
                    int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 59, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, (Color.Cyan), 3.75f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 2.5f;
                    int dust2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 59, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, (Color.Cyan), 1.5f);
                }
            }
            else
            {
                projectile.ai[0] += 1f;
            }
            return;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            int numberProjectiles = 1;
            int num1 = Main.rand.Next(-30, 30);
            int num2 = Main.rand.Next(300, 500);
            for (int num131 = 0; num131 < numberProjectiles; num131++)
            {
                if (Main.rand.Next(3) == 0)
                {
                    Projectile.NewProjectile(projectile.Center.X + num1, projectile.Center.Y - num2, 0, 20, mod.ProjectileType("Feather2"), projectile.damage / 2, 0, projectile.owner);
                }
            }
        }
    }
}