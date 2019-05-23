using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Arrows
{
    public class AquaticArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.arrow = true;
            projectile.width = 10;
            projectile.height = 10;
            //projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.timeLeft = 600;
            projectile.penetrate = -1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquatic Arrow");
        }
        public override void AI()
        {
            Lighting.AddLight((int)projectile.Center.X, (int)projectile.Center.Y, 0f, 0.1f, 0.5f);
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            Dust dust = Main.dust[Dust.NewDust(projectile.Center, 4, 4, 111)];
            dust.velocity *= 0.6f;
            dust.scale *= 0.6f;
            dust.noGravity = true;

            projectile.localAI[1]++;
            if (projectile.localAI[1] >= 20)
            {
                float numberProjectiles = 6;
                float rotation = MathHelper.ToRadians(10);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("AquaticArrow2"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                }
                projectile.Kill();
            }
        }
    }
}