using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class Feather3 : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.hostile = false;
            projectile.melee = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Feather");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            //dust
            /*float num98 = 16f;
            int num99 = 0;
            while ((float)num99 < num98)
            {
                Vector2 vector11 = Vector2.UnitX * 0f;
                vector11 += -Vector2.UnitY.RotatedBy((double)((float)num99 * (6.28318548f / num98)), default(Vector2)) * new Vector2(1f, 4f);
                vector11 = vector11.RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2));
                int num100 = Dust.NewDust(projectile.Center, 0, 0, 137, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num100].scale = 1f;
                Main.dust[num100].noGravity = true;
                Main.dust[num100].position = projectile.Center + vector11;
                Main.dust[num100].velocity = projectile.velocity * 0f + vector11.SafeNormalize(Vector2.UnitY) * 1f;
                num99++;
            }*/
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 2;
        }
    }
}