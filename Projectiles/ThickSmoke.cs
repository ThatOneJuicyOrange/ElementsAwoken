using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ThickSmoke : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.tileCollide = false;

            projectile.alpha = 60;
            projectile.penetrate = -1;

            projectile.scale = 1f;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discord Toxin");
            Main.projFrames[projectile.type] = 5;
        }
        public override void AI()
        {
            projectile.velocity.X *= 0.97f;
            projectile.velocity.Y *= 0.97f;

            projectile.rotation += 0.1f;

            float aliveTime = 60;
            projectile.scale -= 0.5f / aliveTime;
            projectile.ai[0]++;
            if (projectile.ai[0] <= aliveTime - 20)
            {
                if (!ModContent.GetInstance<Config>().lowDust && Main.rand.Next(4)== 0)
                {
                    int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 54);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                }
            }
            else
            {
                projectile.alpha += 255 / 20;
                if (projectile.alpha >= 255)
                {
                    projectile.Kill();
                }
            }
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 4)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > Main.projFrames[projectile.type] - 1)
                    projectile.frame = 0;
            }
            return true;
        }
    }
}