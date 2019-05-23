using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class EquinoxSwirl : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.timeLeft = 120;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fireweaver");
        }
        public override void AI()
        {
            Vector2 offset = new Vector2(12, 0);
            Projectile parent = Main.projectile[(int)projectile.ai[1]];
            projectile.ai[0] += 0.1f;
            projectile.position = parent.position + offset.RotatedBy(projectile.ai[0] + projectile.ai[1] * (Math.PI * 2 / 8));

            if (parent.active == false)
            {
                projectile.Kill();
            }
            if (Main.rand.Next(2) == 0)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 242, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 0, default(Color), 3.75f);
                Main.dust[dust].velocity *= 0.6f;
                Main.dust[dust].scale *= 0.6f;
                Main.dust[dust].noGravity = true;
                int dust2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 0, default(Color), 3.75f);
                Main.dust[dust2].velocity *= 0.6f;
                Main.dust[dust2].scale *= 0.6f;
                Main.dust[dust2].noGravity = true;
                int dust3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 229, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 0, default(Color), 1.75f);
                Main.dust[dust3].velocity *= 0.6f;
                Main.dust[dust3].scale *= 0.6f;
                Main.dust[dust3].noGravity = true;
                int dust4 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 197, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 0, default(Color), 1.75f);
                Main.dust[dust4].velocity *= 0.6f;
                Main.dust[dust4].scale *= 0.6f;
                Main.dust[dust4].noGravity = true;
            }
        }
    }
}