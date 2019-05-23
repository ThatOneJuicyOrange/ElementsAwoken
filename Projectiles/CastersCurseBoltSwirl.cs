using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class CastersCurseBoltSwirl : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.timeLeft = 200;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zerg Caster");
        }
        public override void AI()
        {
            Vector2 offset = new Vector2(14, 0);
            Projectile parent = Main.projectile[(int)projectile.ai[1]];
            projectile.ai[0] += 0.1f;
            projectile.position = parent.position + offset.RotatedBy(projectile.ai[0] + projectile.ai[1] * (Math.PI * 2 / 8));

            if (parent.active == false)
            {
                projectile.Kill();
            }
            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 60, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 0, default(Color), 3.75f);
            Main.dust[dust].velocity *= 0.6f;
            Main.dust[dust].scale *= 0.6f;
            Main.dust[dust].noGravity = true;
        }
    }
}