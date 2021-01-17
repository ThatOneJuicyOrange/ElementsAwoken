using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj
{
    public class ShadowTentacle : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;

            projectile.penetrate = 5;
            projectile.MaxUpdates = 3;

            projectile.tileCollide = false;
            projectile.hostile = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Tentacle");
        }
        public override void AI()
        {
            Vector2 center10 = projectile.Center;
            projectile.scale = 1f - projectile.localAI[0];
            projectile.width = (int)(20f * projectile.scale);
            projectile.height = projectile.width;
            projectile.position.X = center10.X - (float)(projectile.width / 2);
            projectile.position.Y = center10.Y - (float)(projectile.height / 2);
            if ((double)projectile.localAI[0] < 0.1)
            {
                projectile.localAI[0] += 0.01f;
            }
            else
            {
                projectile.localAI[0] += 0.025f;
            }
            if (projectile.localAI[0] >= 0.95f)
            {
                projectile.Kill();
            }
            projectile.velocity.X = projectile.velocity.X + projectile.ai[0] * 1.5f;
            projectile.velocity.Y = projectile.velocity.Y + projectile.ai[1] * 1.5f;
            if (projectile.velocity.Length() > 16f)
            {
                projectile.velocity.Normalize();
                projectile.velocity *= 16f;
            }
            projectile.ai[0] *= 1.05f;
            projectile.ai[1] *= 1.05f;
            if (projectile.scale < 1f)
            {
                int num892 = 0;
                while ((float)num892 < projectile.scale * 10f)
                {
                    int num893 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 54, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1.1f);
                    Main.dust[num893].position = (Main.dust[num893].position + projectile.Center) / 2f;
                    Main.dust[num893].noGravity = true;
                    Dust dust = Main.dust[num893];
                    dust.velocity *= 0.1f;
                    dust = Main.dust[num893];
                    dust.velocity -= projectile.velocity * (1.3f - projectile.scale);
                    Main.dust[num893].fadeIn = (float)(100 + projectile.owner);
                    dust = Main.dust[num893];
                    dust.scale += projectile.scale * 0.75f;
                    int num3 = num892;
                    num892 = num3 + 1;
                }
                return;
            }
        }
    }
}