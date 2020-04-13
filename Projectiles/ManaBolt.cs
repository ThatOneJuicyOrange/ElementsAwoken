using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ManaBolt : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.alpha = 255;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mana Bolt");
        }
        public override void AI()
        {
            projectile.localAI[0]++;
            if (projectile.localAI[0] > 3)
            {
                float numDust = 4;
                for (int l = 0; l < numDust; l++)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 234)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= projectile.velocity / numDust * (float)l;
                    dust.noGravity = true;
                    dust.scale = 1f;
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 27);
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 value15 = new Vector2((float)Main.rand.Next(-9, 9), (float)Main.rand.Next(-9, 9));
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value15.X, value15.Y, mod.ProjectileType("Manashatter"), projectile.damage / 2, 2f, projectile.owner, 0f, 0f);
            }

            ProjectileUtils.Explosion(projectile, 234, damageType: "ranged");
        }
    }
}