using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Arrows
{
    public class DesolationArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;

            projectile.timeLeft = 600;

            projectile.arrow = true;
            projectile.friendly = true;
            projectile.ranged = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desolation Arrow");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            for (int i = 0; i < 2; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType("AncientGreen"), 0f, 0f, 100, default(Color), 1f)];
                dust.noGravity = true;
            }
            if (projectile.ai[0] > 0)
            {
                projectile.velocity.Y += projectile.ai[0];
            }
        }
        public override void Kill(int timeLeft)
        {
            int numberProjectiles = Main.rand.Next(1,4); // 1 - 3 
            if (projectile.ai[1] > 0)
            {
                numberProjectiles = (int)projectile.ai[1];
            }
            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.NextFloat(-1.2f, 1.2f), Main.rand.NextFloat(-1.2f, 1.2f), mod.ProjectileType("DesolationShard"), (int)(projectile.damage * 0.75f), 2f, projectile.owner, 0f, 0f);
            }
        }
    }
}