using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Yoyos
{
    public class WaterYoyoP : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;

            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;

            projectile.light = 0.5f;

            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 380f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 16.5f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aqua Marine");
        }
        public override void AI()
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("WaterYoyoWater"), projectile.damage, 5f, projectile.owner, 0f, 0f);
        }
    }
}