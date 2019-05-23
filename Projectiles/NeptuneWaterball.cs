using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class NeptuneWaterball : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.aiStyle = 1;
            aiType = ProjectileID.Bullet;

            projectile.friendly = true;
            projectile.melee = true;

            projectile.timeLeft = 600;
            projectile.alpha = 255;
            projectile.extraUpdates = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Neptune");
        }
        public override void AI()
        {
            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 41, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 1f);
            Main.dust[dust].velocity *= 0.6f;
            Main.dust[dust].noGravity = true;

            projectile.velocity.Y += 0.16f;
        }
    }
}