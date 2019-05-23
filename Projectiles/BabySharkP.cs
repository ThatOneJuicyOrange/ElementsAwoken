using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class BabySharkP : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 54;
            projectile.height = 36;
            projectile.friendly = true;
            projectile.aiStyle = 39;
            aiType = 190;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.ranged = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Baby Shark");
        }
    }
}