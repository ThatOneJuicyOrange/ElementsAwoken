using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ChaosGazer : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 300;
            projectile.light = 1f;
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
            projectile.knockBack = 10;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Gazer");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("ChaosBurn"), 180);
        }
        public override void AI()
        {
            projectile.localAI[1]++;
            if (projectile.localAI[1] > 5)
            {
                for (int l = 0; l < 5; l++)
                {
                    float num95 = projectile.velocity.X / 3f * (float)l;
                    float num96 = projectile.velocity.Y / 3f * (float)l;
                    int num97 = 4;
                    int num98 = Dust.NewDust(new Vector2(projectile.position.X + (float)num97, projectile.position.Y + (float)num97), projectile.width - num97 * 2, projectile.height - num97 * 2, 127, 0f, 0f, 100, default(Color), 1.2f);
                    Main.dust[num98].noGravity = true;
                    Dust dust = Main.dust[num98];
                    dust.velocity *= 0.1f;
                    dust = Main.dust[num98];
                    dust.velocity += projectile.velocity * 0.1f;
                    Dust var_2_4829_cp_0_cp_0 = Main.dust[num98];
                    var_2_4829_cp_0_cp_0.position.X = var_2_4829_cp_0_cp_0.position.X - num95;
                    Dust var_2_4843_cp_0_cp_0 = Main.dust[num98];
                    var_2_4843_cp_0_cp_0.position.Y = var_2_4843_cp_0_cp_0.position.Y - num96;
                }
                if (Main.rand.Next(5) == 0)
                {
                    int num99 = 4;
                    int num100 = Dust.NewDust(new Vector2(projectile.position.X + (float)num99, projectile.position.Y + (float)num99), projectile.width - num99 * 2, projectile.height - num99 * 2, 127, 0f, 0f, 100, default(Color), 0.6f);
                    Dust dust = Main.dust[num100];
                    dust.velocity *= 0.25f;
                    dust = Main.dust[num100];
                    dust.velocity += projectile.velocity * 0.5f;
                }
            }

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("Explosion"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 103);
            for (int num369 = 0; num369 < 20; num369++)
            {
                int num370 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num370].velocity *= 1.4f;
            }
            for (int num371 = 0; num371 < 10; num371++)
            {
                int num372 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 127, 0f, 0f, 100, default(Color), 2.5f);
                Main.dust[num372].noGravity = true;
                Main.dust[num372].velocity *= 5f;
                num372 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 127, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num372].velocity *= 3f;
            }
            int num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore85 = Main.gore[num373];
            gore85.velocity.X = gore85.velocity.X + 1f;
            Gore gore86 = Main.gore[num373];
            gore86.velocity.Y = gore86.velocity.Y + 1f;
            num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore87 = Main.gore[num373];
            gore87.velocity.X = gore87.velocity.X - 1f;
            Gore gore88 = Main.gore[num373];
            gore88.velocity.Y = gore88.velocity.Y + 1f;
            num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore89 = Main.gore[num373];
            gore89.velocity.X = gore89.velocity.X + 1f;
            Gore gore90 = Main.gore[num373];
            gore90.velocity.Y = gore90.velocity.Y - 1f;
            num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore91 = Main.gore[num373];
            gore91.velocity.X = gore91.velocity.X - 1f;
            Gore gore92 = Main.gore[num373];
            gore92.velocity.Y = gore92.velocity.Y - 1f;
        }
    }
}