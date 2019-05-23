using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace ElementsAwoken.Projectiles.Yoyos
{
    public class EarthP : ModProjectile
    {
        public float timer = 30;
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;

            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;

            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 145f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 9f;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 3f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earth");
        }
    }
}