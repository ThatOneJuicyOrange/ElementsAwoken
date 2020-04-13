using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class BlossomBoomerP : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.thrown = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 180;
            projectile.aiStyle = 16;
            //aiType = ProjectileID.WoodenArrowFriendly;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blossom Boomer");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 6; i++)
            {
                float speed = Main.rand.NextFloat(1, 3);
                Vector2 perturbedSpeed = new Vector2(speed, speed).RotatedByRandom(MathHelper.ToRadians(360));
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("BlossomSpore"), (int)(projectile.damage * 0.75f), 0f, 0);
            }

            int explosion = ProjectileUtils.Explosion(projectile, 6, damageType: "thrown");
            Main.projectile[explosion].hostile = true;
        }
    }
}