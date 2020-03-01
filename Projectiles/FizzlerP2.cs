using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class FizzlerP2 : ModProjectile
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
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fizzler");
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0)
            {
                projectile.ai[0] = Main.rand.Next(200, 300);
                projectile.localAI[0]++;
            }
            projectile.ai[0]--;
            if (projectile.ai[0] <= 0) projectile.Kill();

            if (Main.rand.NextBool(2))
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 6)];
                dust.velocity *= 0.4f;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
    }
}