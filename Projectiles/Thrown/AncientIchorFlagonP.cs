using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class AncientIchorFlagonP : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 38;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = 1;
            projectile.aiStyle = 2;
            projectile.timeLeft = 600;
            aiType = 48;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Ichor Flagon");
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 6; i++)
            {
                float speed = 4f;
                Vector2 perturbedSpeed = new Vector2(speed, speed).RotatedByRandom(MathHelper.ToRadians(360));
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.GoldenShowerFriendly, (int)(projectile.damage * 0.75f), 0f, 0);
            }
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 27);
        }
    }
}