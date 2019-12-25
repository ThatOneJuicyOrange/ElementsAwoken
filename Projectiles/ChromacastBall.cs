using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ChromacastBall : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }

        int startingDamage = 100;
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 300;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chromacast");
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0)
            {
                startingDamage = projectile.damage;
                projectile.localAI[0]++;
            }
            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.1f) / 255f, ((255 - projectile.alpha) * 0.1f) / 255f, ((255 - projectile.alpha) * 0f) / 255f);
            int dustID = mod.DustType("AncientRed");
            projectile.ai[0]++;
            if (projectile.ai[0] >= 20 && projectile.ai[0] < 40) dustID = mod.DustType("AncientGreen");
            else if (projectile.ai[0] >= 40 && projectile.ai[0] < 60) dustID = mod.DustType("AncientBlue");
            else if (projectile.ai[0] >= 60 && projectile.ai[0] < 80) dustID = mod.DustType("AncientPink");
            else if (projectile.ai[0] >= 80) projectile.ai[0] = 0;
            for (int i = 0; i < 2; i++)
            {
                int dust1 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, Main.rand.Next(3) <= 1 ? 264 : 31);
                Main.dust[dust1].velocity = projectile.velocity * 0.2f;
                Main.dust[dust1].scale *= 1.5f;
                Main.dust[dust1].noGravity = true;

                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustID);
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[dust].velocity = projectile.velocity * Main.rand.NextFloat(0.7f,0.9f);
                    Main.dust[dust].scale *= 1.4f;
                }
                else
                {
                    Main.dust[dust].velocity *= 0.6f;
                }
                Main.dust[dust].noGravity = true;
            }

            projectile.damage = (int)(startingDamage * MathHelper.Lerp(0.05f, 2, (float)projectile.timeLeft / 300f));
        }

        public override void Kill(int timeLeft)
        {
            // big boom
            Projectile proj = Main.projectile[Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("BigExplosion"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f)];
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
            for (int num369 = 0; num369 < 30; num369++)
            {
                int num370 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num370].velocity *= 1.7f;
            }
            for (int num371 = 0; num371 < 20; num371++)
            {
                int num372 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, GetDustID(), 0f, 0f, 100, default(Color), 2.5f);
                Main.dust[num372].noGravity = true;
                Main.dust[num372].velocity *= 7f;
                num372 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, GetDustID(), 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num372].velocity *= 5f;
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
        private int GetDustID()
        {
            switch (Main.rand.Next(4))
            {
                case 0:
                    return mod.DustType("AncientRed");
                case 1:
                    return mod.DustType("AncientGreen");
                case 2:
                    return mod.DustType("AncientBlue");
                case 3:
                    return mod.DustType("AncientPink");
                default:
                    return mod.DustType("AncientRed");
            }
        }
    }
}