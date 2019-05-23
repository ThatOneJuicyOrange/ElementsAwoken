using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Yoyos
{
    public class SaturnRing : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;

            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.friendly = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 1000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Saturn");
        }
        public override void AI()
        {
            projectile.rotation += 0.2f;

            Projectile parent = Main.projectile[(int)projectile.ai[1]];
            projectile.Center = parent.Center;


            if (!parent.active)
            {
                projectile.Kill();
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 4;
        }
    }
}