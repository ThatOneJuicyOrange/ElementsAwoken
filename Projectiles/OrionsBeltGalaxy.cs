using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class OrionsBeltGalaxy : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.melee = true;
            projectile.tileCollide = false;

            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orion's Belt");
        }
        public override void AI()
        {
            for (int i = 0; i < 4; i++)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 220, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 100);
                Main.dust[dust].velocity *= 0.6f;
                Main.dust[dust].scale *= 0.6f;
                Main.dust[dust].noGravity = true;
            }
            Projectile parent = Main.projectile[(int)projectile.ai[1]];
            Vector2 whipCenter = parent.position + parent.velocity;

            projectile.ai[0] += 10f;
            int distance = 50;
            double rad = projectile.ai[0] * (Math.PI / 180); // angle to radians
            projectile.position.X = whipCenter.X - (int)(Math.Cos(rad) * distance) - projectile.width / 2;
            projectile.position.Y = whipCenter.Y - (int)(Math.Sin(rad) * distance) - projectile.height / 2;

            if (!parent.active)
            {
                projectile.Kill();
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 9;
        }
    }
}