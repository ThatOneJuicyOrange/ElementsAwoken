using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ChaosRingShield : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.alpha = 60;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 10000;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ring of Chaos");
        }
        public override void AI()
        {
            Player P = Main.player[projectile.owner];

            projectile.position.X = P.position.X;
            projectile.position.Y = P.position.Y;
            if (projectile.ai[0] == 0)
            {
                int swirlCount = 2;
                int orbital = projectile.whoAmI;
                for (int l = 0; l < swirlCount; l++)
                {
                    int distance = 16;

                    orbital = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("ChaosRingSwirl"), projectile.damage, projectile.knockBack, projectile.owner, l * distance, projectile.whoAmI);
                }
                projectile.ai[0] = 1;
            }
            if (P.FindBuffIndex(mod.BuffType("ChaosShield")) == -1 || P.dead)
            {
                projectile.active = false;
            }
        }
    }
}