using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class SpinningFlame : ModProjectile
    {
        public float shrink = 150;

        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 1000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spinning Flame");
        }
        public override void AI()
        {
            if (shrink > 0f)
            {
                shrink -= 0.1f;
            }
            Vector2 offset = new Vector2(shrink, 0);
            Player player = Main.player[projectile.owner];
            projectile.ai[0] += 0.1f;
            projectile.Center = player.Center + offset.RotatedBy(projectile.ai[0] + projectile.ai[1] * (Math.PI * 2 / 8));


            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Fire, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f);
            Main.dust[dust].velocity *= 0.1f;
            Main.dust[dust].scale *= 0.6f;
            Main.dust[dust].noGravity = true;
        }
    }
}