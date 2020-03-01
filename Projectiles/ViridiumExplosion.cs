using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ViridiumExplosion : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 25;
            projectile.height = 25;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 180;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Explosion");
        }
        public override void AI()
        {
            for (int i = 0; i < 6; i++)
            {
                Vector2 position = projectile.Center + Main.rand.NextVector2Circular(projectile.width * 2, projectile.height * 2);
                Dust circle = Dust.NewDustPerfect(position, 222, Vector2.Zero);
                circle.noGravity = true;
            }
            for (int i = 0; i < 2; i++)
            {
                float randomSpeed = 0;
                switch (Main.rand.Next(2))
                {
                    case 0:
                        randomSpeed = Main.rand.Next(10, 40);
                        break;
                    case 1:
                        randomSpeed = Main.rand.Next(-40, -10);
                        break;
                    default: break;
                }
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 133, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dust].velocity.X = randomSpeed;
                Main.dust[dust].velocity.Y = randomSpeed;
                Main.dust[dust].noGravity = true;
            }
        }
    }
}