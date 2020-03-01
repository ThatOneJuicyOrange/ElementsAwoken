using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class FireBlasterBolt : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.friendly = true;
            projectile.ranged = true;

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
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 6)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 3f * (float)i;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 2; k++)
            {
                Projectile proj = Main.projectile[Projectile.NewProjectile(projectile.position.X, projectile.position.Y, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), mod.ProjectileType("GreekFire"), (int)(projectile.damage * 0.4f), projectile.knockBack * 0.35f, Main.myPlayer, 0f, 0f)];
                proj.timeLeft = 120;
                proj.ranged = true;
            }
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
        }
    }
}