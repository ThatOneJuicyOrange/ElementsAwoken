using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Pets
{
    public class CelestialWatcher : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 34;

            projectile.netImportant = true;
            projectile.friendly = true;
            Main.projPet[projectile.type] = true;
            projectile.tileCollide = false;

            projectile.minionSlots = 1f;
            projectile.timeLeft = 18000;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;

            projectile.scale = 0.8f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Watcher");
            Main.projFrames[projectile.type] = 4;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 600)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.dead)
            {
                modPlayer.royalEye = false;
            }
            if (modPlayer.royalEye)
            {
                projectile.timeLeft = 2;
            }

            if (Math.Abs(projectile.velocity.X) > 0.02f || Math.Abs(projectile.velocity.Y) > 0.02f)
            {
                int dustType = 6;
                if (projectile.frame == 1) dustType = 197;
                else if (projectile.frame == 2) dustType = 229;
                else if (projectile.frame == 3) dustType = 242;
                for (int i = 0; i < 2; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType)];
                    dust.noGravity = true;
                    dust.velocity *= 0.1f;
                    dust.fadeIn = 0.6f;
                }
            }

            if (!Collision.CanHitLine(projectile.Center, 1, 1, player.Center, 1, 1))
            {
                projectile.ai[0] = 1f;
            }
            float speed = 6f;
            if (projectile.ai[0] == 1f)
            {
                speed = 15f;
            }
            Vector2 center = projectile.Center;
            Vector2 direction = player.Center - center;
            projectile.ai[1] = 3600f;
            projectile.netUpdate = true;
            int num = 1;
            for (int k = 0; k < projectile.whoAmI; k++)
            {
                if (Main.projectile[k].active && Main.projectile[k].owner == projectile.owner && Main.projectile[k].type == projectile.type)
                {
                    num++;
                }
            }
            direction.X -= (float)((10 + num * 40) * player.direction);
            direction.Y -= 70f;
            float distanceTo = direction.Length();
            if (distanceTo > 200f && speed < 9f)
            {
                speed = 9f;
            }
            if (distanceTo < 100f && projectile.ai[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
            {
                projectile.ai[0] = 0f;
                projectile.netUpdate = true;
            }
            if (distanceTo > 2000f)
            {
                projectile.Center = player.Center;
            }
            if (distanceTo > 48f)
            {
                direction.Normalize();
                direction *= speed;
                float temp = 40 / 2f;
                projectile.velocity = (projectile.velocity * temp + direction) / (temp + 1);
            }
            else
            {
                projectile.direction = Main.player[projectile.owner].direction;
                projectile.velocity *= (float)Math.Pow(0.9, 40.0 / 40);
            }

            projectile.rotation = projectile.velocity.X * 0.05f;

            if ((double)Math.Abs(projectile.velocity.X) > 0.2)
            {
                projectile.spriteDirection = -projectile.direction;
                return;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.penetrate == 0)
            {
                projectile.Kill();
            }
            return false;
        }
    }
}