using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class ThorsHammerP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.scale = 1f;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = -1;
            projectile.aiStyle = 3;
            projectile.timeLeft = 200;
            projectile.extraUpdates = 2;
            aiType = 301;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storm Hammer");
        }
        public override void AI()
        {
            projectile.velocity.X *= 1.05f;
            projectile.velocity.Y *= 1.05f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.ai[0] += 0.1f;
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }

            float random = Main.rand.Next(30, 90);
            float spread = random * 0.0174f;
            double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spread / 2;
            double deltaAngle = spread / 8f;
            double offsetAngle;
            int i;
            for (i = 0; i < 6; i++)
            {
                offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), mod.ProjectileType("ThorLightning"), (int)((double)projectile.damage * 1.75f), projectile.knockBack, projectile.owner, 0f, 0f);
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 122);
            }

            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            float random = Main.rand.Next(30, 90);
            float spread = random * 0.0174f;
            double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spread / 2;
            double deltaAngle = spread / 8f;
            double offsetAngle;
            int i;
            for (i = 0; i < 6; i++)
            {
                offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), mod.ProjectileType("ThorLightning"), (int)((double)projectile.damage * 1.75f), projectile.knockBack, projectile.owner, 0f, 0f);
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 122);
            }
        }
    }
}