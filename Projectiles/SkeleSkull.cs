using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class SkeleSkull : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;

            projectile.friendly = true;
            projectile.tileCollide = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skull");
            Main.projFrames[projectile.type] = 3;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frame = (int)(projectile.ai[1] - 1);
            return true;
        }
        public override void AI()
        {
            if (projectile.ai[1] == 0)
            {
                projectile.ai[1] = Main.rand.Next(1, 4);
            }
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X);

            int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 6, projectile.velocity.X * 0.15f, projectile.velocity.Y * 0.15f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale *= 1.5f;

            ProjectileUtils.Home(projectile, 6f);
        }
    }
}