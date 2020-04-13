using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class TerreneRock : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;

            projectile.minion = true;
            projectile.friendly = true;       
            projectile.timeLeft = 600;
            projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terrene Rock");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            projectile.velocity.Y += 0.16f;

            for (int i = 0; i < 2; i++)
            {
                int dust1 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 75, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].velocity *= 0f;
            }
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 75, damageType: "minion");
        }
    }
}