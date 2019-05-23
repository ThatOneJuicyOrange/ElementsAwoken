using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class IceMist : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 92;
            projectile.height = 102;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 220;
            projectile.magic = true;
            projectile.tileCollide = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Mist");
        }
        public override void AI()
        {
            projectile.ai[0]++;
            //if (projectile.ai[1] == 1f)
            {
                if (projectile.ai[0] >= 130f)
                {
                    projectile.alpha += 10;
                }
                else
                {
                    projectile.alpha -= 10;
                }
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
                if (projectile.alpha > 255)
                {
                    projectile.alpha = 255;
                }
                if (projectile.ai[0] >= 150f)
                {
                    projectile.Kill();
                    return;
                }
                if (projectile.ai[0] % 30f == 0f)
                {
                    Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 120);

                    float numberProjectiles = 8;
                    float rotation = MathHelper.ToRadians(360);
                    float speed = 2f;
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = new Vector2(2, 2).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * speed;
                        int num1 = Projectile.NewProjectile(projectile.Center.X + projectile.velocity.X, projectile.Center.Y + projectile.velocity.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("IceMistSpike"), projectile.damage, projectile.knockBack, projectile.owner);
                    }
                }
                projectile.rotation += 0.104719758f;
                Lighting.AddLight(projectile.Center, 0.3f, 0.75f, 0.9f);
                return;
            }
        }       
    }
}