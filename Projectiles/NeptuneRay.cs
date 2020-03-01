using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class NeptuneRay : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            //projectile.aiStyle = 48;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 150;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ocean's Ray");
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
            projectile.localAI[0]++;
            if (projectile.localAI[0] > 4)
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector2 vector33 = projectile.position;
                    vector33 -= projectile.velocity * ((float)i * 0.33f);
                    projectile.alpha = 255;
                    Dust dust = Main.dust[Dust.NewDust(vector33, 1, 1, 221, 0f, 0f, 0, default(Color), 0.75f)];
                    dust.position = vector33;
                    dust.scale = (float)Main.rand.Next(70, 110) * 0.013f;
                    dust.velocity *= 0.05f;
                    dust.noGravity = true;
                }
            }
            return;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            int numProj = Main.rand.Next(1, 3);
            for (int k = 0; k < numProj; k++)
            {
                Projectile.NewProjectile(target.Center.X + Main.rand.Next(-6,6), target.Top.Y - Main.rand.Next(60,75), 0f, 2, mod.ProjectileType("OceansSeashell"), (int)(projectile.damage * 0.75f), projectile.knockBack, projectile.owner, 0f, 0f);
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 221, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f)];
                dust.noGravity = true;
            }
        }
    }
}