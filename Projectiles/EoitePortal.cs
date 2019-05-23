using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles
{
    public class EoitePortal : ModProjectile
    {
        public int shootTimer = 10;
        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 38;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 15;
            projectile.light = 2f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eoite Portal");
        }

        public override void AI()
        {
            shootTimer--;
            if (shootTimer <= 0)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y + 10, 0, 26, mod.ProjectileType("EoiteBlast"), 200, 0, projectile.owner);
                projectile.Kill();
            }
        }
    }
}