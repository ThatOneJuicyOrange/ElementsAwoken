using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class CosmicWrathP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;

            projectile.friendly = true;
            projectile.thrown = true;

            projectile.penetrate = -1;

            //projectile.aiStyle = 93;
            //aiType = ProjectileID.Daybreak;

            projectile.timeLeft = 1200;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Wrath");
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.ai[0] != 0)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (crit)
            {
                damage *= 3;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("ExtinctionCurse"), 160);
            target.immune[projectile.owner] = 7;

                if (projectile.ai[0] == 0f)
                {
                    projectile.ai[1] = 0f;
                    int num14 = -target.whoAmI - 1;
                    projectile.ai[0] = (float)num14;
                    projectile.velocity = target.Center - projectile.Center;
                }
            
        }
        public override void AI()
        {
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 25;
                if (projectile.alpha <= 0)
                {
                    projectile.alpha = 0;
                }
            }
            if (projectile.velocity.Y > 18f)
            {
                projectile.velocity.Y = 18f;
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[1] += 1f;
                if (projectile.ai[1] > 20f)
                {
                    projectile.velocity.Y = projectile.velocity.Y + 0.1f;
                    projectile.velocity.X = projectile.velocity.X * 0.992f;
                }
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
                return;
            }
            projectile.tileCollide = false;
            if (projectile.ai[0] == 1f)
            {
                projectile.tileCollide = false;
                projectile.velocity *= 0.6f;
            }
            else
            {
                projectile.tileCollide = false;
                int num895 = (int)(-(int)projectile.ai[0]);
                num895--;
                projectile.position = Main.npc[num895].Center - projectile.velocity;
                projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
                projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
                if (!Main.npc[num895].active || Main.npc[num895].life < 0)
                {
                    projectile.tileCollide = true;
                    projectile.ai[0] = 0f;
                    projectile.ai[1] = 20f;
                    projectile.velocity = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    projectile.velocity.Normalize();
                    projectile.velocity *= 6f;
                    projectile.netUpdate = true;
                }
                else if (projectile.velocity.Length() > (float)((Main.npc[num895].width + Main.npc[num895].height) / 3))
                {
                    projectile.velocity *= 0.99f;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("CosmicWrathExplosion"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
            for (int num369 = 0; num369 < 20; num369++)
            {
                int num370 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
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