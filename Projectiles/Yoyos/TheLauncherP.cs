using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Yoyos
{
    public class TheLauncherP : ModProjectile
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

            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 275f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 15f;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 12f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Launcher");
        }
        public override void AI()
        {
            if (Main.rand.Next(7) == 0)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 16f, Main.rand.Next(-50, 50) * .25f, Main.rand.Next(-50, 50) * .25f, mod.ProjectileType("TheLauncherBolt"), projectile.damage, 0, projectile.owner);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.velocity.Y -= Main.rand.Next(1, 20);
            target.velocity.X -= Main.rand.Next(-20, 20);
        }
    }
}