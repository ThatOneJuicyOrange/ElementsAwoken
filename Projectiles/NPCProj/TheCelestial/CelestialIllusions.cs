using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.TheCelestial
{
    public class CelestialIllusions : ModProjectile
    {
        int distance = 200;
        public override void SetDefaults()
        {
            projectile.width = 120;
            projectile.height = 202;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 10000;
            projectile.alpha = 150;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Celestials");
            Main.projFrames[projectile.type] = 4;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > 9)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }

            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Color color = projectile.GetAlpha(Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16.0)));
            Texture2D projTexture = Main.projectileTexture[projectile.type];
            int height = projTexture.Height / Main.projFrames[projectile.type]; // equals the projectile.height but this is more dynamic
            int drawY = height * (int)projectile.ai[0];

            Rectangle rectangle = new Rectangle(0, drawY, projTexture.Width, height);
            Vector2 origin = rectangle.Size() / 4f;
            origin = new Vector2(origin.X - (float)(projectile.width * 0.5), origin.Y + (float)(projectile.height * 0.5)); // to get it drawing at the center of the npc
            rectangle = new Rectangle(projectile.width * projectile.frame, drawY, projectile.width, projectile.height);

            Main.spriteBatch.Draw(projTexture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, color, projectile.rotation, origin, projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            Lighting.AddLight((int)projectile.Center.X, (int)projectile.Center.Y, 0.5f, 0.2f, 0.2f);
            Player player = Main.player[projectile.owner];
            Vector2 direction = player.Center - projectile.Center;
            if (direction.X > 0f)
            {
                projectile.spriteDirection = -1;
            }
            if (direction.X < 0f)
            {
                projectile.spriteDirection = 1;
            }
            int min = 100;
            int max = 300;
            if (projectile.localAI[0] == 0)
            {
                distance -= 3;
                if (distance <= min)
                {
                    projectile.localAI[0]++;
                }
            }
            else
            {
                distance += 3;
                if (distance >= max)
                {
                    projectile.localAI[0] = 0;
                }
            }
            if (projectile.ai[0] == 0)
            {
                projectile.position.X = player.Center.X + distance - projectile.width / 2;
                projectile.position.Y = player.Center.Y + distance - projectile.height / 2;
            }
            if (projectile.ai[0] == 1)
            {
                projectile.position.X = player.Center.X - distance - projectile.width / 2;
                projectile.position.Y = player.Center.Y + distance - projectile.height / 2;
            }
            if (projectile.ai[0] == 2)
            {
                projectile.position.X = player.Center.X + distance - projectile.width / 2;
                projectile.position.Y = player.Center.Y - distance - projectile.height / 2;
            }
            if (projectile.ai[0] == 3)
            {
                projectile.position.X = player.Center.X - distance - projectile.width / 2;
                projectile.position.Y = player.Center.Y - distance - projectile.height / 2;
            }
            if (player.dead || !NPC.AnyNPCs(mod.NPCType("TheCelestial")))
            {
                projectile.Kill();
            }
            if (!player.active)
            {
                projectile.Kill();
            }
        }
    }
}