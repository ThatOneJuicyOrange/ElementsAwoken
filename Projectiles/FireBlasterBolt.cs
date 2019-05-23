using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class FireBlasterBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.friendly = true;
            projectile.magic = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 600;

            projectile.alpha = 255;
            projectile.light = 1f;

            projectile.extraUpdates = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Blaster");
        }
        public override void AI()
        {
            for (int num121 = 0; num121 < 5; num121++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 6)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 6f * (float)num121;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 2; k++)
            {
                Projectile proj = Main.projectile[Projectile.NewProjectile(projectile.position.X, projectile.position.Y, (float)Main.rand.Next(-50, 50) * 0.15f, (float)Main.rand.Next(-50, 50) * 0.15f, mod.ProjectileType("GreekFire"), (int)(projectile.damage * 0.7), 1f, Main.myPlayer)];
                proj.timeLeft = 90;
            }
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
        }
    }
}