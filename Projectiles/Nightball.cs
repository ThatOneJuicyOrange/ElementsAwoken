using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class Nightball : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.ranged = true;

            projectile.alpha = 255;

            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nightfall");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.7f, 0.2f, 0.7f);

            if (Main.rand.Next(2) == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 173)];
                dust.velocity *= 0.4f;
                dust.position -= projectile.velocity / 6f;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 20);
            int numProj = Main.rand.Next(2, 4);
            for (int i = 0; i < numProj; i++)
            {
                float speed = 9f;
                Vector2 perturbedSpeed = new Vector2(speed, speed).RotatedByRandom(MathHelper.ToRadians(360));
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Nightball2>(), projectile.damage, projectile.knockBack, 0);
            }
        }
    }
}