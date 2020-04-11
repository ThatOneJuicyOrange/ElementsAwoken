using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class Particles : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;

            projectile.friendly = true;
            projectile.ranged = true;

            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 150;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Particles");
        }
        public override void AI()
        {
            int fadeout = 40;
            if (projectile.ai[0] > 10)
            {
                projectile.ai[1]++;
                if (projectile.ai[1] > fadeout) projectile.Kill();
            }
            int num = (int)(3 * (1-(projectile.ai[1] / (fadeout /2))));
            if (num >= 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    CreateDust();
                }
            }
            else if (Main.rand.NextBool(3))  CreateDust();
        }
        private void CreateDust()
        {
            Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 135)];
            dust.velocity = Vector2.Zero;
            dust.noGravity = true;
            dust.scale = 0.5f;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.ai[0] > 10) return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 1;
            projectile.ai[0]++;
        }
    }
}
