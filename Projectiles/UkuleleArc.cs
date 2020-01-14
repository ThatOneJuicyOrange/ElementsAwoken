using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class UkuleleArc : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.friendly = true;
            projectile.tileCollide = false;

            projectile.penetrate = 1;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strange Ukulele");
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.whoAmI != (int)projectile.ai[0])
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            if (projectile.velocity.X != projectile.velocity.X)
            {
                projectile.position.X = projectile.position.X + projectile.velocity.X;
                projectile.velocity.X = -projectile.velocity.X;
            }
            if (projectile.velocity.Y != projectile.velocity.Y)
            {
                projectile.position.Y = projectile.position.Y + projectile.velocity.Y;
                projectile.velocity.Y = -projectile.velocity.Y;
            }
            int dustLength = 4;
            for (int i = 0; i < dustLength; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 226)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / dustLength * (float)i;
                dust.noGravity = true;
            }

            NPC target = Main.npc[(int)projectile.ai[0]]; 
            if (target != null)
            {
                float speed = 5.5f;
                float num4 = target.Center.X - projectile.Center.X;
                float num5 = target.Center.Y - projectile.Center.Y;
                float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
                num6 = speed / num6;
                num4 *= num6;
                num5 *= num6;
                projectile.velocity.X = (projectile.velocity.X * 20f + num4) / 21f;
                projectile.velocity.Y = (projectile.velocity.Y * 20f + num5) / 21f;
                return;
            }
        }
    }
}