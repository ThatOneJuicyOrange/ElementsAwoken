using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.TheCelestial
{
    public class TheCelestialLaser : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Celestial");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.3f, 0.3f, 0.3f);
            for (int i = 0; i < 5; i++)
            {
                int dustType = 6;
                switch ((int)projectile.ai[0])
                {
                    case 0:
                        dustType = 6;
                        break;
                    case 1:
                        dustType = 197;
                        break;
                    case 2:
                        dustType = 229;
                        break;
                    case 3:
                        dustType = 242;
                        break;
                    default: break;
                }
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 6f * (float)i;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
    }
}