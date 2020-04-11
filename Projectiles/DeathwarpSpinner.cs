using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Projectiles
{
    public class DeathwarpSpinner : ModProjectile
    {
        private int aiTimer = 0;
        private int shootTimer = 0;
        private float increase = 0f;
        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 50;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deathwarp");
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            Vector2 direction = player.Center - projectile.Center;
            if (direction.X > 0f)
            {
                projectile.spriteDirection = 1;
                projectile.rotation = direction.ToRotation();
            }
            if (direction.X < 0f)
            {
                projectile.spriteDirection = -1;
                projectile.rotation = direction.ToRotation() + 1.57f;
            }
            projectile.rotation += MathHelper.ToRadians(45);
            if (projectile.localAI[0] == 0)
            {
                Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/LaserCharge"));
                projectile.localAI[0]++;
            }
            aiTimer++;
            shootTimer--;
            Vector2 offset = new Vector2(100, 0);
            if (increase <= 0.25f)
            {
                increase += 0.001f;
                if (aiTimer > 30) increase += 0.001f;
                if (aiTimer > 45) increase += 0.001f;
                if (aiTimer > 50)  increase += 0.001f;
            }
            projectile.ai[0] += increase;
            projectile.Center = player.Center + offset.RotatedBy(projectile.ai[0] * (Math.PI * 2 / 8));

            if (!player.active || player.dead) projectile.Kill();

            if (increase >= 0.24f && shootTimer <= 0)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 33);
                float Speed = -15f;
                float rotation = (float)Math.Atan2(projectile.Center.Y - player.Center.Y, projectile.Center.X - player.Center.X);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ProjectileType<DeathwarpLaser>(), projectile.damage, 0f, 0);
                shootTimer = 3;
            }
        }
    }
}