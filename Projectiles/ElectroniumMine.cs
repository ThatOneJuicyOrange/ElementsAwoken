using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ElectroniumMine : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.penetrate = 3;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 900;
            projectile.aiStyle = 93;
            //aiType = 514;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mine");
        }
        public override void AI()
        {
            if ((projectile.velocity.X > 0.5f || projectile.velocity.X < -0.5f) || (projectile.velocity.Y > 0.5f || projectile.velocity.Y < -0.5f))
            {
                for (int i = 0; i < 2; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 6)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= projectile.velocity / 6f * (float)i;
                    dust.noGravity = true;
                    dust.scale = 1f;
                }
            }
            // tile stick
            try
            {
                int num187 = (int)(projectile.position.X / 16f) - 1;
                int num188 = (int)((projectile.position.X + (float)projectile.width) / 16f) + 2;
                int num189 = (int)(projectile.position.Y / 16f) - 1;
                int num190 = (int)((projectile.position.Y + (float)projectile.height) / 16f) + 2;
                if (num187 < 0)
                {
                    num187 = 0;
                }
                if (num188 > Main.maxTilesX)
                {
                    num188 = Main.maxTilesX;
                }
                if (num189 < 0)
                {
                    num189 = 0;
                }
                if (num190 > Main.maxTilesY)
                {
                    num190 = Main.maxTilesY;
                }
                int num3;
                for (int num191 = num187; num191 < num188; num191 = num3 + 1)
                {
                    for (int num192 = num189; num192 < num190; num192 = num3 + 1)
                    {
                        if (Main.tile[num191, num192] != null && Main.tile[num191, num192].nactive() && (Main.tileSolid[(int)Main.tile[num191, num192].type] || (Main.tileSolidTop[(int)Main.tile[num191, num192].type] && Main.tile[num191, num192].frameY == 0)))
                        {
                            Vector2 vector18;
                            vector18.X = (float)(num191 * 16);
                            vector18.Y = (float)(num192 * 16);
                            if (projectile.position.X + (float)projectile.width > vector18.X && projectile.position.X < vector18.X + 16f && projectile.position.Y + (float)projectile.height > vector18.Y && projectile.position.Y < vector18.Y + 16f)
                            {
                                projectile.velocity.X = 0f;
                                projectile.velocity.Y = -0.2f;
                            }
                        }
                        num3 = num192;
                    }
                    num3 = num191;
                }
            }
            catch
            {
            }

            projectile.velocity.Y += 0.13f;
            projectile.rotation += 0.2f * (projectile.velocity.X * 0.2f);
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("Explosion"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
            for (int num369 = 0; num369 < 20; num369++)
            {
                int num370 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num370].velocity *= 1.4f;
            }
            for (int num371 = 0; num371 < 10; num371++)
            {
                int num372 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2.5f);
                Main.dust[num372].noGravity = true;
                Main.dust[num372].velocity *= 5f;
                num372 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
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