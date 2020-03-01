using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class FizzlerP : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;

            projectile.friendly = true;
            projectile.ranged = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 600;

            projectile.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fizzler");
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0)
            {
                projectile.ai[0] = Main.rand.Next(20, 30);
                projectile.localAI[0]++;
            }
            projectile.ai[0]--;
            if (projectile.ai[0] <= 0)
            {
                float numProj = Main.rand.Next(2, 4);
                for (int i = 0; i < numProj; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(7));
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<FizzlerP2>(), (int)(projectile.damage * 0.8f), projectile.knockBack, projectile.owner);
                }
                projectile.Kill();
            }
            projectile.localAI[1]++;
            if (projectile.localAI[1] > 4)
            {
                for (int i = 0; i < 3; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 6)];
                    dust.velocity *= 0.4f;
                    dust.noGravity = true;
                    dust.scale = 1f;
                }
            }
        }
    }
}