using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.Ancients
{
    public class AncientProjectionSwirl2 : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/NPCProj/Ancients/AncientProjection"; } }

        public int type = 0;
        public override void SetDefaults()
        {
            projectile.width = 88;
            projectile.height = 102;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 10000;
            projectile.alpha = 255;

            //projectile.scale *= 1.3f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Projection");
            Main.projFrames[projectile.type] = 4;
        }
        public override bool CanHitPlayer(Player target)
        {
            if (projectile.alpha > 90)
            {
                return false;
            }
            return base.CanHitPlayer(target);
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
            int drawY = height * (int)type;

            Rectangle rectangle = new Rectangle(0, drawY, projTexture.Width, height);
            Vector2 origin = rectangle.Size() / 4f;
            origin = new Vector2(origin.X - (float)(projectile.width * 0.5), origin.Y + (float)(projectile.height * 0.5)); // to get it drawing at the center of the npc
            rectangle = new Rectangle(projectile.width * projectile.frame, drawY, projectile.width, projectile.height);

            Main.spriteBatch.Draw(projTexture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, color, projectile.rotation, origin, 1.3f, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0)
            {
                projectile.localAI[0]++;
                type = Main.rand.Next(4);
            }
            if (type == 0)
            {
                Lighting.AddLight(projectile.Center, 1.2f, 0f, 1.5f);
            }
            else if (type == 1)
            {
                Lighting.AddLight(projectile.Center, 0, 1.5f, 0.5f);
            }
            else if (type == 2)
            {
                Lighting.AddLight(projectile.Center, 1.5f, 0f, 0.5f);
            }
            else if (type == 3)
            {
                Lighting.AddLight(projectile.Center, 0.3f, 0f, 1.5f);
            }
            if (projectile.alpha > 80)
            {
                projectile.alpha -= 5;
            }
            Player player = Main.player[Main.myPlayer];


            Vector2 direction = player.Center - projectile.Center;
            if (direction.X > 0f)
            {
                projectile.spriteDirection = -1;
            }
            if (direction.X < 0f)
            {
                projectile.spriteDirection = 1;
            }

            if (projectile.alpha > 80)
            {
                projectile.alpha -= 10;
            }
            NPC parent = Main.npc[(int)projectile.ai[1]];

            projectile.ai[0] += -1f; // speed
            int distance =  1350;
            double rad = projectile.ai[0] * (Math.PI / 180); // angle to radians
            projectile.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - projectile.width / 2;
            projectile.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - projectile.height / 2;
            if (!parent.active || (parent.ai[2] != 7 && parent.ai[2] != 11))
            {
                projectile.Kill();
            }
        }
    }
}