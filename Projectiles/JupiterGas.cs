using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class JupiterGas : ModProjectile
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
            DisplayName.SetDefault("Jupiter");
        }
        public override void AI()
        {
            int dustType = 31;
            switch (Main.rand.Next(3))
            {
                case 0:
                    dustType = 31;
                    break;
                case 1:
                    dustType = 32;
                    break;
                case 2:
                    dustType = 102;
                    break;
                default: break;
            }
            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustType, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 1f);
            Main.dust[dust].velocity *= 0.6f;
            Main.dust[dust].noGravity = true;
        }
    }
}