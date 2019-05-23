using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class FireweaverProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.alpha = 60;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.timeLeft = 120;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fireweaver");
        }
        public override void AI()
        {
            if (projectile.ai[0] == 0)
            {
                int swirlCount = 2;
                int orbital = projectile.whoAmI;
                for (int l = 0; l < swirlCount; l++)
                {
                    int distance = 16;

                    orbital = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("FireweaverSwirl"), projectile.damage, projectile.knockBack, projectile.owner, l * distance, projectile.whoAmI);
                }
                projectile.ai[0] = 1;
            }
            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f);
            Main.dust[dust].velocity *= 0.1f;
            Main.dust[dust].scale *= 0.6f;
            Main.dust[dust].noGravity = true;
        }
    }
}