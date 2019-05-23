using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class AsteroxShieldSwirl : ModProjectile
    {
        public float shrink = 100;
        public float dustAI = 20;

        public bool shrinking = true;
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
            DisplayName.SetDefault("Andromeda");
        }
        public override void AI()
        {
            if (shrinking)
            {
                shrink -= 3f;
            }
            if (!shrinking)
            {
                shrink += 3f;
            }
            Vector2 offset = new Vector2(shrink, 0);
            Projectile parent = Main.projectile[(int)projectile.ai[1]];
            projectile.ai[0] += 0.05f;
            projectile.Center = parent.Center + offset.RotatedBy(projectile.ai[0] + projectile.ai[1] * (Math.PI * 2 / 8));
            if (!parent.active)
            {
                projectile.Kill();
            }
            if (shrink <= 0)
            {
                shrinking = false;
            }
            if (shrink >= 100)
            {
                shrinking = true;
            }
            if (shrink > 80)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 4.75f);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 0.6f;
                Main.dust[dust].noGravity = true;
            }
            if (shrink <= 80 && shrink > 60)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 197, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 2.75f);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 0.6f;
                Main.dust[dust].noGravity = true;
            }
            if (shrink <= 60 && shrink > 40)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 242, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 0.6f;
                Main.dust[dust].noGravity = true;
            }
            if (shrink <= 40 && shrink > 20)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 229, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 2.75f);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 0.6f;
                Main.dust[dust].noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 2;
        }
    }
}