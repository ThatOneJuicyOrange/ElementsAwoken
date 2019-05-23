using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace ElementsAwoken.Projectiles.Yoyos
{
    public class SaturnP : ModProjectile
    {
        public bool hasRing = false;
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;

            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;

            projectile.light = 0.5f;

            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 285f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 14.5f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Saturn");
        }
        public override void AI()
        {
            if (!hasRing)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("SaturnRing"), projectile.damage, 0.5f, 0, 0f, projectile.whoAmI);
                hasRing = true;
            }
        }
    }
}