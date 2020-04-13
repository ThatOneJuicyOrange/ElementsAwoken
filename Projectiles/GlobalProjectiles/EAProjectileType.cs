using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken;

namespace ElementsAwoken.Projectiles.GlobalProjectiles
{
    public class EAProjectileType : GlobalProjectile
    {
        public bool whip = false;
        public int whipAliveTime = 30;
        public int specialPenetrate = -2;
        public EAProjectileType()
        {
            whip = false;
        }
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override void SetDefaults(Projectile projectile)
        {
            if (projectile.type == ProjectileID.SolarWhipSword)
            {
                whip = true;
                whipAliveTime = 30;
            }
        }
    }
}
