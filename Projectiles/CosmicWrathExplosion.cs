using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class CosmicWrathExplosion : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 5;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Explosion");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 1;
        }
    }
}