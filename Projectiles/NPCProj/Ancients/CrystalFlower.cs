using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.Ancients
{
    public class CrystalFlower : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 46;
            projectile.height = 46;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Amalgamate");
        }
        public override void AI()
        {
        }
    }
}
    