using System;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class TimeRocket : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.friendly = true;
            projectile.ranged = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 300;
            projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Time Rocket");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.2f, 0.9f, 0.4f);

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            projectile.ai[1]++;
            if (!ModContent.GetInstance<Config>().lowDust || projectile.ai[1] % 3 == 0)
            {
                float num247 = 0f;
                float num248 = 0f;
                if (Main.rand.Next(2) == 0)
                {
                    num247 = projectile.velocity.X * 0.5f;
                    num248 = projectile.velocity.Y * 0.5f;
                }
                int num249 = Dust.NewDust(new Vector2(projectile.position.X + 3f + num247, projectile.position.Y + 3f + num248) - projectile.velocity * 0.5f, projectile.width - 8, projectile.height - 8, 75, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num249].scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
                Main.dust[num249].velocity *= 0.2f;
                Main.dust[num249].noGravity = true;
                num249 = Dust.NewDust(new Vector2(projectile.position.X + 3f + num247, projectile.position.Y + 3f + num248) - projectile.velocity * 0.5f, projectile.width - 8, projectile.height - 8, 31, 0f, 0f, 100, default(Color), 0.5f);
                Main.dust[num249].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
                Main.dust[num249].velocity *= 0.05f;
            }

            ProjectileUtils.Home(projectile, 9f);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 200);
        }

        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, new int[] { 75 }, projectile.damage, "ranged");
        }
    }
}