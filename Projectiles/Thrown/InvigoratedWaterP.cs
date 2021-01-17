using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class InvigoratedWaterP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 34;

            projectile.friendly = true;
            projectile.thrown = true;
            projectile.scale *= 0.7f;

            projectile.penetrate = 1;
            projectile.timeLeft = 360;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Invigorated Water");
        }
        public override void AI()
        {
            projectile.rotation += projectile.velocity.X * 0.05f;
            projectile.velocity.Y += 0.16f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item107, projectile.position);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, ProjectileType<InvigoratedAura>(), 1, projectile.knockBack, projectile.owner);
        }
    }
}