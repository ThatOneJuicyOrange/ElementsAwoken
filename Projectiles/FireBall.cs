using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class FireBall : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;

            projectile.friendly = true;
            projectile.magic = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.light = 1f;
            projectile.extraUpdates = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Ball");
        }
        public override void AI()
        {
            Dust smoke = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31)];
            smoke.velocity = projectile.velocity * 0.2f;
            smoke.scale *= 1.5f;
            smoke.noGravity = true;

            Dust fire = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Fire, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f)];
            fire.velocity *= 0.6f;
            fire.scale *= 0.6f;
            fire.noGravity = true;
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 2; k++)
            {
                Projectile proj = Main.projectile[Projectile.NewProjectile(projectile.position.X, projectile.position.Y, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), mod.ProjectileType("GreekFire"), (int)(projectile.damage * 0.7f), projectile.knockBack * 0.35f, Main.myPlayer, 0f, 0f)];
                proj.timeLeft = 90;
                proj.magic = true;
            }
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
        }
    }
}