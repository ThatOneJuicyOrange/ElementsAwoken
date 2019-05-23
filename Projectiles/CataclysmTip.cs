using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class CataclysmTip : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.TerraBeam);
            Main.projFrames[projectile.type] = 1;
            projectile.scale = 1.2f;
            projectile.penetrate = 1;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.timeLeft = 300;
            ProjectileID.Sets.Homing[projectile.type] = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cataclysm");
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("Explosion"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
            for (int num369 = 0; num369 < 20; num369++)
            {
                int num370 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 127, 0f, 0f, 100, default(Color), 1.5f);
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
            //MORE BLADES
            {
                Vector2 vector8 = new Vector2(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2));
                int damage = 40;
                int type = mod.ProjectileType("CataclysmicLaser");
                Projectile.NewProjectile(vector8.X, vector8.Y, 0, 12, type, damage, 0f, 0);
                Projectile.NewProjectile(vector8.X, vector8.Y, 0, -12, type, damage, 0f, 0);
                Projectile.NewProjectile(vector8.X, vector8.Y, 12, 0, type, damage, 0f, 0);
                Projectile.NewProjectile(vector8.X, vector8.Y, -12, 0, type, damage, 0f, 0);
            }
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.3f) / 255f, ((255 - projectile.alpha) * 0.4f) / 255f, ((255 - projectile.alpha) * 1f) / 255f);
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 127);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1.2f;

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }
    }
}