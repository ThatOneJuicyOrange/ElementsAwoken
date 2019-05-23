using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.TheGuardian
{
    public class GuardianPortalSwirl : ModProjectile
    {
        public int shrink = 100;

        public bool shrinking = true;
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.timeLeft = 4000;
            projectile.extraUpdates = 2;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Guardian");
        }
        public override void AI()
        {
            if (shrinking)
            {
                shrink -= 1;
            }
            if (!shrinking)
            {
                shrink += 1;
            }
            Projectile parent = Main.projectile[(int)projectile.ai[1]];
            int distance = shrink;
            double rad = projectile.ai[0] * (Math.PI / 180);
            projectile.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - projectile.width / 2;
            projectile.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - projectile.height / 2;
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

            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 2.75f);
            Main.dust[dust].velocity *= 0.1f;
            Main.dust[dust].scale *= 0.6f;
            Main.dust[dust].noGravity = true;

        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 2;
        }
    }
}