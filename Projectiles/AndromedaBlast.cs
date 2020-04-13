using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class AndromedaBlast : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.TerraBeam);
            Main.projFrames[projectile.type] = 1;
            projectile.scale = 1.2f;
            projectile.penetrate = -1;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.timeLeft = 300;
            ProjectileID.Sets.Homing[projectile.type] = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Andromeda");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            int numberProjectiles = 2 + Main.rand.Next(2);
            for (int index = 0; index < numberProjectiles; ++index)
            {
                Vector2 vector2_1 = new Vector2((float)((double)projectile.position.X + (double)projectile.width * 0.5 + (double)(Main.rand.Next(201) * -projectile.direction) + ((double)Main.mouseX + (double)Main.screenPosition.X - (double)projectile.position.X)), (float)((double)projectile.position.Y + (double)projectile.height * 0.5 - 600.0));   //this defines the projectile width, direction and position
                vector2_1.X = (float)(((double)vector2_1.X + (double)projectile.Center.X) / 2.0) + (float)Main.rand.Next(-200, 201);
                vector2_1.Y -= (float)(100 * index);
                float num12 = (float)Main.mouseX + Main.screenPosition.X - vector2_1.X;
                float num13 = (float)Main.mouseY + Main.screenPosition.Y - vector2_1.Y;
                if ((double)num13 < 0.0) num13 *= -1f;
                if ((double)num13 < 20.0) num13 = 20f;
                float num14 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
                float num15 = 14f / num14;
                float num16 = num12 * num15;
                float num17 = num13 * num15;
                float SpeedX = num16 + (float)Main.rand.Next(-40, 1) * 0.02f;
                float SpeedY = num17 + (float)Main.rand.Next(-40, 1) * 0.02f;
                Projectile.NewProjectile(vector2_1.X, vector2_1.Y, SpeedX, SpeedY, mod.ProjectileType("AndromedaStar"), projectile.damage, 1f, Main.myPlayer, 0.0f, (float)Main.rand.Next(5));
            }
            Projectile.NewProjectile(projectile.position.X + Main.rand.Next(-400, 400), projectile.position.Y + Main.rand.Next(-400, 400), 0, 0, mod.ProjectileType("AndromedaBase"), damage, 3f, projectile.owner, 0f, 0f);
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.3f) / 255f, ((255 - projectile.alpha) * 0.4f) / 255f, ((255 - projectile.alpha) * 1f) / 255f);
            if (Main.rand.Next(4) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 242);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1.2f;
                int dust2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 197);
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].scale = 1.2f;
                int dust3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6);
                Main.dust[dust3].noGravity = true;
                Main.dust[dust3].scale = 1.2f;
                int dust4 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 229);
                Main.dust[dust4].noGravity = true;
                Main.dust[dust4].scale = 1.2f;
            }
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, new int[] { 242, 197, 6 }, damageType: "melee");
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }
    }
}