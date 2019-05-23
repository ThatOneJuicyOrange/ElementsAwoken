using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.Ancients
{
    public class AncientProjection : ModProjectile
    {
        int distance = 200;
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
            if (projectile.alpha > 80)
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
            int drawY = height * (int)projectile.ai[0];

            Rectangle rectangle = new Rectangle(0, drawY, projTexture.Width, height);
            Vector2 origin = rectangle.Size() / 4f;
            origin = new Vector2(origin.X - (float)(projectile.width * 0.5), origin.Y + (float)(projectile.height * 0.5)); // to get it drawing at the center of the npc
            rectangle = new Rectangle(projectile.width * projectile.frame, drawY, projectile.width, projectile.height);

            Main.spriteBatch.Draw(projTexture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, color, projectile.rotation, origin, 1.3f, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            if (projectile.ai[0] == 0)
            {
                Lighting.AddLight(projectile.Center, 1.2f, 0f, 1.5f);
            }
            else if (projectile.ai[0] == 1)
            {
                Lighting.AddLight(projectile.Center, 0, 1.5f, 0.5f);
            }
            else if (projectile.ai[0] == 2)
            {
                Lighting.AddLight(projectile.Center, 1.5f, 0f, 0.5f);
            }
            else if (projectile.ai[0] == 3)
            {
                Lighting.AddLight(projectile.Center, 0.3f, 0f, 1.5f);
            }      

            Player player = Main.player[Main.myPlayer];

                projectile.spriteDirection = projectile.velocity.X > 0 ? -1 : 1;

            if (!NPC.AnyNPCs(mod.NPCType("AncientAmalgam")))
            {
                projectile.Kill();
            }

            if (projectile.alpha > 80)
            {
                projectile.alpha -= 10;
            }

            float rotateIntensity = 2;
            float waveTime = 60f;
            projectile.localAI[0]++;
            if (projectile.localAI[1] == 0) // this part is to fix the offset (it is still slightlyyyy offset)
            {
                if (projectile.localAI[0] > waveTime * 0.5f)
                {
                    projectile.localAI[0] = 0;
                    projectile.localAI[1] = 1;
                }
                else
                {
                    Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(-rotateIntensity));
                    projectile.velocity = perturbedSpeed;
                }
            }
            else
            {
                if (projectile.localAI[0] <= waveTime)
                {
                    Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(rotateIntensity));
                    projectile.velocity = perturbedSpeed;
                }
                else
                {
                    Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(-rotateIntensity));
                    projectile.velocity = perturbedSpeed;
                }
                if (projectile.localAI[0] >= waveTime * 2)
                {
                    projectile.localAI[0] = 0;
                }
            }
        }
    }
}