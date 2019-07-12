using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Wasteland
{
    public class WastelandSnap : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 42;
            projectile.height = 38;

            projectile.penetrate = -1;
            projectile.timeLeft = 3;
            projectile.alpha = 255;

            projectile.tileCollide = false;
            projectile.hostile = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wasteland's Pincers");
        }
    }
}