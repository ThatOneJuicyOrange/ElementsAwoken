using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ClusterGrenade2 : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;

            projectile.friendly = true;
            projectile.ranged = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 60;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Little Buddy");
        }
        public override void AI()
        {
            projectile.rotation += projectile.velocity.X * 0.05f;
            projectile.ai[0]++;
            if (Math.Abs(projectile.velocity.Y) <= 0.1 && projectile.velocity.Y != 0) projectile.velocity.Y = 0;
            if (projectile.ai[0] > 15f)
            {
                if (projectile.velocity.Y == 0f)
                {
                    projectile.velocity.X = projectile.velocity.X * 0.95f;
                }
                projectile.velocity.Y = projectile.velocity.Y + 0.2f;
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.ai[0] < 7) return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.Kill();
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
                projectile.velocity.X *= 0.4f;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            projectile.velocity.Y *= 0.3f;
            return false;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item62, projectile.position);
            Projectile exp = Main.projectile[Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<Explosion>(), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f)];
            exp.ranged = true;
            int num = ModContent.GetInstance<Config>().lowDust ? 10 : 20;
            for (int i = 0; i < num; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f)];
                dust.velocity *= 0.9f;
            }
            int num2 = ModContent.GetInstance<Config>().lowDust ? 5 : 10;
            for (int i = 0; i < num2; i++)
            {
                int dustID = 6;
                Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustID, 0f, 0f, 100, default(Color), 2.5f)];
                dust.noGravity = true;
                dust.velocity *= 2f;
                int dustID2 = 6;
                dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustID2, 0f, 0f, 100, default(Color), 1.5f)];
                dust.velocity *= 1.5f;
            }
            int num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.2f;
            Gore gore85 = Main.gore[num373];
            gore85.velocity.X = gore85.velocity.X + 1f;
            Gore gore86 = Main.gore[num373];
            gore86.velocity.Y = gore86.velocity.Y + 1f;
            num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.2f;
            Gore gore87 = Main.gore[num373];
            gore87.velocity.X = gore87.velocity.X - 1f;
            Gore gore88 = Main.gore[num373];
            gore88.velocity.Y = gore88.velocity.Y + 1f;
            num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.2f;
            Gore gore89 = Main.gore[num373];
            gore89.velocity.X = gore89.velocity.X + 1f;
            Gore gore90 = Main.gore[num373];
            gore90.velocity.Y = gore90.velocity.Y - 1f;
            num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.2f;
            Gore gore91 = Main.gore[num373];
            gore91.velocity.X = gore91.velocity.X - 1f;
            Gore gore92 = Main.gore[num373];
            gore92.velocity.Y = gore92.velocity.Y - 1f;
        }
    }
}