using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class BunnyBreath : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.ranged = true;

            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.timeLeft = 200;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bunny Breath");
        }
        public override void AI()
        {
            projectile.velocity.Y += 0.16f;

            for (int i = 0; i < 2; i++)
            {
                int num113 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 66, 0f, 0f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 2.5f);
                Dust dust = Main.dust[num113];
                dust.velocity *= 0.1f;
                dust = Main.dust[num113];
                dust.velocity += projectile.velocity * 0.2f;
                Main.dust[num113].position.X = projectile.position.X + (float)(projectile.width / 2) + 4f + (float)Main.rand.Next(-2, 3);
                Main.dust[num113].position.Y = projectile.position.Y + (float)(projectile.height / 2) + (float)Main.rand.Next(-2, 3);
                Main.dust[num113].noGravity = true;
            }
        }
    }
}
