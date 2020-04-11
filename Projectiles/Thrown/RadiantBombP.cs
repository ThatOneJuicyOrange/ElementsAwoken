using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class RadiantBombP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;

            projectile.friendly = true;
            projectile.hostile = false;
            projectile.thrown = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 180;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Master's Vortex");
        }
        public override void AI()
        {
            projectile.rotation += projectile.velocity.X * 0.05f;
            projectile.velocity.Y += 0.056f;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item29, projectile.position);
            float rotation = MathHelper.TwoPi;
            float numProj = 8;
            for (int i = 0; i < numProj; i++)
            {
                Vector2 perturbedSpeed = (rotation / numProj * i).ToRotationVector2() * 6.5f;
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<RadiantStarSpiral>(), (int)(projectile.damage * 0.6f), projectile.knockBack, projectile.owner);
            }
        }
    }
}