using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.ScourgeFighter
{
    public class ScourgeHomingRocket : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.alpha = 0;
            projectile.timeLeft = 150;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Homing Rocket");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            projectile.localAI[0]++;
            if (projectile.localAI[0] == 0f)
            {
                //Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 20);
                projectile.localAI[0] = 1f;
            }
            if (projectile.localAI[0] >= 1 && projectile.localAI[0] < 200)
            {
                double angle = Math.Atan2(Main.player[Main.myPlayer].position.Y - projectile.position.Y, Main.player[Main.myPlayer].position.X - projectile.position.X);
                projectile.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 7;
            }

            for (int num255 = 0; num255 < 2; num255++)
            {
                float num256 = 0f;
                float num257 = 0f;
                if (num255 == 1)
                {
                    num256 = projectile.velocity.X * 0.5f;
                    num257 = projectile.velocity.Y * 0.5f;
                }
                Vector2 position71 = new Vector2(projectile.position.X + 3f + num256, projectile.position.Y + 3f + num257) - projectile.velocity * 0.5f;
                int width67 = projectile.width - 8;
                int height67 = projectile.height - 8;
                int num258 = Dust.NewDust(position71, width67, height67, DustID.PinkFlame, 0f, 0f, 100, default(Color), 1f);
                Dust dust = Main.dust[num258];
                dust.scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
                dust = Main.dust[num258];
                dust.velocity *= 0.2f;
                Main.dust[num258].noGravity = true;
                Vector2 position72 = new Vector2(projectile.position.X + 3f + num256, projectile.position.Y + 3f + num257) - projectile.velocity * 0.5f;
                int width68 = projectile.width - 8;
                int height68 = projectile.height - 8;
                num258 = Dust.NewDust(position72, width68, height68, 31, 0f, 0f, 100, default(Color), 0.5f);
                Main.dust[num258].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
                dust = Main.dust[num258];
                dust.velocity *= 0.05f;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("ExplosionHostile"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
            for (int num369 = 0; num369 < 20; num369++)
            {
                int num370 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.PinkFlame, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num370].velocity *= 1.4f;
            }
            for (int num371 = 0; num371 < 10; num371++)
            {
                int num372 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.PinkFlame, 0f, 0f, 100, default(Color), 2.5f);
                Main.dust[num372].noGravity = true;
                Main.dust[num372].velocity *= 5f;
                num372 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.PinkFlame, 0f, 0f, 100, default(Color), 1.5f);
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
 