using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ChargeRifleFull : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.alpha = 0;
            projectile.timeLeft = 600;
            projectile.scale *= 1.1f;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Charge Rifle");
            Main.projFrames[projectile.type] = 4;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 3)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;
            }
            if (Main.rand.Next(2) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 220, 0f, 0f, 100, default(Color), 1f);
                Main.dust[dust].velocity *= 0.3f;
                Main.dust[dust].fadeIn = 0.9f;
                Main.dust[dust].noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 220, damageType: "melee");
        }
    }
}