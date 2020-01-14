using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.TheCelestial
{
    public class CelestialStar : ModProjectile
    {
        public override string Texture => "Terraria/Extra_57";
        public override void SetDefaults()
        {
            projectile.width = 54;
            projectile.height = 54;

            projectile.hostile = true;
            projectile.ignoreWater = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.light = 2f;
            projectile.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Celestials");
        }
        public override bool CanHitPlayer(Player target)
        {
            if (projectile.alpha > 10) return false;
            return true;
        }
        public override void AI()
        {
            projectile.rotation += 0.05f;
            Lighting.AddLight(projectile.Center, 0.0f, 0.2f, 0.4f);
            if (projectile.timeLeft > 60) projectile.alpha -= 255 / 60;
            else projectile.alpha += 255 / 60;
            if (projectile.alpha < 100)
            {
                int dustAmount = 2;
                if (ModContent.GetInstance<Config>().lowDust) dustAmount = 5;
                if (Main.rand.Next(dustAmount) == 0)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 261)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= projectile.velocity / 6f;
                    dust.noGravity = true;
                    dust.scale = 1f;
                }
            }
        }
    }
}