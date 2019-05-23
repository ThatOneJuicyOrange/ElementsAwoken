using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Obsidious
{
    public class ObsidiousTargetCrystal : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 1000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidious");
        }
        public override void AI()
        {
            Lighting.AddLight((int)projectile.Center.X, (int)projectile.Center.Y, 0.5f, 0.2f, 0.2f);

            Projectile parent = Main.projectile[(int)projectile.ai[1]];
            Vector2 direction = parent.Center - projectile.Center;
            projectile.rotation = direction.ToRotation() + 0.785f;
            projectile.rotation += MathHelper.ToRadians(45);

            projectile.ai[0] += 5f; // speed
            int distance = 50;
            double rad = projectile.ai[0] * (Math.PI / 180); // angle to radians
            projectile.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - projectile.width / 2;
            projectile.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - projectile.height / 2;
            if (!parent.active)
            {
                projectile.Kill();
            }
        }
    }
}