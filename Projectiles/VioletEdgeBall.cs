using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class VioletEdgeBall : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.ranged = true;
            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Violet Edge Ball");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.ShadowFlame, 180);
        }       
        public override void AI()
        {
            for (int i = 0; i < 5; i++)
            {
                int dust1 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 62, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].velocity *= 0f;
            }
            for (int i = 0; i < 2; i++)
            {
                int dust2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.PinkFlame, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].velocity *= 0f;
            }
            projectile.localAI[1] += 1f;

            if (projectile.localAI[1] <= 90f)
            {
                float centerY = projectile.Center.X;
                float centerX = projectile.Center.Y;
                float num = 400f;
                bool home = false;
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, Main.npc[i].Center, 1, 1))
                    {
                        float num1 = Main.npc[i].position.X + (float)(Main.npc[i].width / 2);
                        float num2 = Main.npc[i].position.Y + (float)(Main.npc[i].height / 2);
                        float num3 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num1) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num2);
                        if (num3 < num)
                        {
                            num = num3;
                            centerY = num1;
                            centerX = num2;
                            home = true;
                        }
                    }
                }
                if (home)
                {
                    float speed = 8f;
                    Vector2 vector35 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                    float num4 = centerY - vector35.X;
                    float num5 = centerX - vector35.Y;
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

        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 62, damageType: "ranged");
        }
    }
}