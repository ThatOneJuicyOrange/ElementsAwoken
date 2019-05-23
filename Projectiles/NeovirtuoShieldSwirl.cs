using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class NeovirtuoShieldSwirl : ModProjectile
    {
        public float spinAi = 0f;
        public bool reset = true;
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.timeLeft = 4000;
            projectile.extraUpdates = 2;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Neovirtuo");
        }
        public override void AI()
        {
            Vector2 offset = new Vector2(projectile.ai[0] * 8, 0);
            Projectile parent = Main.projectile[(int)projectile.ai[1]];
            if (reset)
            {
                if (projectile.ai[0] == 1)
                {
                    spinAi -= 0.15f;
                }
                if (projectile.ai[0] == 2)
                {
                    spinAi -= 0.3f;
                }
                if (projectile.ai[0] == 3)
                {
                    spinAi -= 0.45f;
                }
                if (projectile.ai[0] == 4)
                {
                    spinAi -= 0.6f;
                }
                if (projectile.ai[0] == 5)
                {
                    spinAi -= 0.75f;
                }
                if (projectile.ai[0] == 6)
                {
                    spinAi -= 0.9f;
                }
                if (projectile.ai[0] == 7)
                {
                    spinAi -= 1.05f;
                }
                if (projectile.ai[0] == 8)
                {
                    spinAi -= 1.2f;
                }
                if (projectile.ai[0] == 9)
                {
                    spinAi -= 1.35f;
                }
                if (projectile.ai[0] == 10)
                {
                    spinAi -= 1.5f;
                }
                reset = false;
            }
            spinAi += 0.03f;
            projectile.Center = parent.Center + offset.RotatedBy(spinAi + projectile.ai[1] * (Math.PI * 2 / 8));
            if (!parent.active)
            {
                projectile.Kill();
            }
            if (Main.rand.Next(2) == 0)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 242, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 4.75f);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 0.4f;
                Main.dust[dust].noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 2;
        }
    }
}