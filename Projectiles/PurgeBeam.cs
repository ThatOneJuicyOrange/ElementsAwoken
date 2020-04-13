using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class PurgeBeam : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            //projectile.aiStyle = 48;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 10;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 320;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Purge Beam");
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
            projectile.localAI[1]++;
            if (projectile.localAI[1] >= 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        Vector2 vector = projectile.position;
                        vector -= projectile.velocity * ((float)i * 0.25f);
                        projectile.alpha = 255;
                        int dust = Dust.NewDust(vector, 1, 1, 127, 0f, 0f, 0, default(Color), 0.75f);
                        Main.dust[dust].position = vector;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                        Main.dust[dust].velocity *= 0.05f;
                    }
                }
            }
            return;
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 127, damageType: "ranged");
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            ProjectileUtils.Explosion(projectile, 127, damageType: "ranged");
            target.AddBuff(mod.BuffType("ChaosBurn"), 180);
        }
    }
}