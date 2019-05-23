using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class BugToxin : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.alpha = 60;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.scale = 1.3f;
            projectile.timeLeft = 600;
            projectile.ranged = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BugToxin");
            Main.projFrames[projectile.type] = 5;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 3;
        }
        public override void AI()
        {
            projectile.velocity.X *= 0.94f;
            projectile.velocity.Y *= 0.94f;
            projectile.localAI[0]++;
            if (projectile.localAI[0] <= 60)
            {
                if (Main.rand.Next(3) == 0)
                {
                    int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 182);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1f;
                    Main.dust[dust].noLight = true;
                }
            }
            else
            {
                projectile.alpha += 2;
                if (projectile.alpha > 250)
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