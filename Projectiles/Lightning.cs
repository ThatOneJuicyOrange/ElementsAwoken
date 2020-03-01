using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class Lightning : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public Vector2 velocity = new Vector2();
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.friendly = true;
            projectile.tileCollide = true;

            projectile.penetrate = 2;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 200;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Tome");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(2) == 0)
            {
                projectile.ai[0]++;
                projectile.ai[1] = target.whoAmI;
            }
            else
            {
                projectile.Kill();
            }
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

            int dustLength = ModContent.GetInstance<Config>().lowDust ? 1 : 3;
            for (int i = 0; i < dustLength; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 226)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / dustLength * (float)i;
                dust.noGravity = true;
                dust.color = new Color(154, 255, 145);
            }
            if (velocity == Vector2.Zero) velocity = projectile.velocity;
            projectile.velocity = velocity.RotatedByRandom(.75);

            if (projectile.ai[0] != 0)
            {
                float closestEntity = 200f;
                NPC target = null;
                for (int i = 0; i < 200; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (nPC.CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, nPC.Center, 1, 1))
                    {
                        float dist = Math.Abs(projectile.Center.X - nPC.Center.X) + Math.Abs(projectile.Center.Y - nPC.Center.Y);
                        if (dist < closestEntity && nPC.whoAmI != projectile.ai[1])
                        {
                            closestEntity = dist;
                            target = nPC;
                        }
                    }
                }
                if (target != null)
                {
                    Vector2 toTarget = target.Center - projectile.Center;
                    toTarget.Normalize();
                    toTarget *= 5;
                    velocity = toTarget;
                }
            }

        }
    }
}