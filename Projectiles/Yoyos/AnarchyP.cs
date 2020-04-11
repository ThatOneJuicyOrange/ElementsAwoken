using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Yoyos
{
    public class AnarchyP : ModProjectile
    {
        int shootTimer = 0;
        int shootTimer2 = 0;
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;

            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;

            projectile.light = 0.5f;

            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 450f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 21f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Anarchy");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 3;
            target.AddBuff(mod.BuffType("ChaosBurn"), 180);
        }
        public override void AI()
        {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 127, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            
            shootTimer--;
            shootTimer2--;
            if (shootTimer <= 0)
            {
                int choice = Main.rand.Next(7);

                float speedX = 0;
                float speedY = 0;

                if (choice == 0)
                {
                    speedX = 0;
                    speedY = 8;
                }
                if (choice == 1)
                {
                    speedX = 3;
                    speedY = 8;
                }
                if (choice == 2)
                {
                    speedX = 5;
                    speedY = 8;
                }
                if (choice == 3)
                {
                    speedX = 8;
                    speedY = 8;
                }
                if (choice == 4)
                {
                    speedX = 8;
                    speedY = 5;
                }
                if (choice == 5)
                {
                    speedX = 8;
                    speedY = 3;
                }
                if (choice == 6)
                {
                    speedX = 8;
                    speedY = 0;
                }
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, speedX, speedY, mod.ProjectileType("AnarchyWave"), projectile.damage * 4, 0, projectile.owner);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -speedX, -speedY, mod.ProjectileType("AnarchyWave"), projectile.damage * 4, 0, projectile.owner);
                shootTimer = 30 + Main.rand.Next(0, 60);
            }
            if (shootTimer2 <= 0)
            {
                int numberProjectiles = 3;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 value15 = new Vector2((float)Main.rand.Next(-12, 12), (float)Main.rand.Next(-12, 12));
                    value15.X *= 0.25f;
                    value15.Y *= 0.25f;
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value15.X, value15.Y, mod.ProjectileType("AnarchyLaser"), projectile.damage, 2f, projectile.owner, 0f, 0f);
                }
                shootTimer2 = 9 + Main.rand.Next(0, 4);

            }
        }
    }
}