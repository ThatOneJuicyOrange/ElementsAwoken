using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class DragonfireBullet : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.penetrate = 1;

            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;

            projectile.light = 1f;
            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragonfire");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Dragonfire"), 200);
        }
        public override void AI()
        {
            projectile.localAI[0]++;
            if (projectile.localAI[0] < 5)
            {
                projectile.alpha = 255;
            }
            else
            {
                projectile.alpha = 0;
            }

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (Main.rand.Next(3) == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 6)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 6f;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
    }
}