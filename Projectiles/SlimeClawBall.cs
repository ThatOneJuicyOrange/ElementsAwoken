using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class SlimeClawBall : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.friendly = true;
            projectile.magic = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.light = 0.1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime Ball");
        }
        public override void AI()
        {
            projectile.velocity.Y += 0.05f;

            Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 4, projectile.velocity.X * 0.3f, projectile.velocity.Y * 0.3f, 150, new Color(0, 220, 40, 100), 2f)];
            dust.velocity *= 0.6f;
            dust.scale *= 0.6f;
            dust.noGravity = true;
        }
    }
}