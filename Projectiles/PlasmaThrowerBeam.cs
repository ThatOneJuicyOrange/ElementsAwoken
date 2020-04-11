using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class PlasmaThrowerBeam : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 2;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 320;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Purge Beam");
        }
        public override void AI()
        {
            if (projectile.ai[1] % 2 == 0)
            {
                projectile.height += 1;
                projectile.width += 1;
            }
            if (projectile.ai[1] % 4 == 0)
            {
                projectile.Center -= new Vector2(1, 1);
            }
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
            projectile.ai[1]++;
            if (projectile.ai[1] >= 7)
            {
                float dustAmount = 4;
                for (int i = 0; i < dustAmount; i++)
                {
                    Vector2 vector = projectile.position;
                    vector -= projectile.velocity * ((float)i / dustAmount);
                    projectile.alpha = 255;
                    int dust = Dust.NewDust(vector, projectile.width, projectile.height, Main.rand.NextBool() ? 6 : 127, 0f, 0f, 0, default(Color), 0.35f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                    Main.dust[dust].velocity *= 0.05f;
                    Main.dust[dust].fadeIn = 1.4f;
                }
            }
            return;
        }
        
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180);
        }
    }
}