using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class DeathwarpSpinner : ModProjectile
    {
        int aiTimer = 0;
        int shootTimer = 0;
        float increase = 0f;
        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 50;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.magic = true;
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
            Projectile parent = Main.projectile[(int)projectile.ai[1]];
            if (increase <= 0.25f) // 0.3
            {
                increase += 0.001f;
                if (aiTimer > 30)
                {
                    increase += 0.001f;
                }
                if (aiTimer > 45)
                {
                    increase += 0.001f;
                }
                if (aiTimer > 50)
                {
                    increase += 0.001f;
                }
            }
            projectile.ai[0] += increase;
            projectile.position = parent.Center + offset.RotatedBy(projectile.ai[0]  * (Math.PI * 2 / 8)) - new Vector2(projectile.width/2, projectile.height/2); // if the projectile isnt dust, you gotta subtract half the size to make it change the projectile center

            if (parent.active == false)
            {
                projectile.Kill();
            }
            if (increase >= 0.24f && shootTimer <= 0)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 33);
                float Speed = 15f;
                Vector2 vector8 = new Vector2(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2));
                int damage = 400;
                int type = mod.ProjectileType("DeathwarpLaser");
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 21);
                float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, -((float)((Math.Cos(rotation) * Speed) * -1)), -((float)((Math.Sin(rotation) * Speed) * -1)), type, damage, 0f, 0); // reverse speed to shoot away from player
                shootTimer = 3;
            }
        }
    }
}