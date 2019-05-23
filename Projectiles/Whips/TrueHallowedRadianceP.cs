﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Enums;
using Microsoft.Xna.Framework.Graphics;
using ElementsAwoken.Projectiles.GlobalProjectiles;

namespace ElementsAwoken.Projectiles.Whips
{
    public class TrueHallowedRadianceP : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.hide = true;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.GetGlobalProjectile<EAProjectileType>().whip = true;
            projectile.GetGlobalProjectile<EAProjectileType>().whipAliveTime = 20;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Hallowed Radiance");
        }
        public override void AI()
        {       
            Player player = Main.player[projectile.owner];
            float num = 1.57079637f;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            if (projectile.localAI[1] > 0f)
            {
                projectile.localAI[1] -= 1f;
            }
            projectile.alpha -= 42;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = projectile.velocity.ToRotation();
            }
            float num32 = (float)((projectile.localAI[0].ToRotationVector2().X >= 0f) ? 1 : -1);
            if (projectile.ai[1] <= 0f)
            {
                num32 *= -1f;
            }
            int timeAlive = projectile.GetGlobalProjectile<EAProjectileType>().whipAliveTime;
            Vector2 vector17 = (num32 * (projectile.ai[0] / timeAlive * 6.28318548f - 1.57079637f)).ToRotationVector2();
            vector17.Y *= (float)Math.Sin((double)projectile.ai[1]);
            if (projectile.ai[1] <= 0f)
            {
                vector17.Y *= -1f;
            }
            vector17 = vector17.RotatedBy((double)projectile.localAI[0], default(Vector2));
            projectile.ai[0] += 1f;
            if (projectile.ai[0] < timeAlive)
            {
                float distance = 30f;
                projectile.velocity += distance * vector17; // determines how far it goes
            }
            else
            {
                projectile.Kill();
            }
            if (projectile.ai[0] == timeAlive / 2)
            {
                Projectile.NewProjectile(projectile.position.X + projectile.velocity.X, projectile.position.Y + projectile.velocity.Y, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, mod.ProjectileType("TrueHallowedRadianceScythe"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            }
            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + num;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
            Vector2 vector24 = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
            if (player.direction != 1)
            {
                vector24.X = (float)player.bodyFrame.Width - vector24.X;
            }
            if (player.gravDir != 1f)
            {
                vector24.Y = (float)player.bodyFrame.Height - vector24.Y;
            }
            vector24 -= new Vector2((float)(player.bodyFrame.Width - player.width), (float)(player.bodyFrame.Height - 42)) / 2f;
            projectile.Center = player.RotatedRelativePoint(player.position + vector24, true) - projectile.velocity;
            if (projectile.alpha == 0)
            {
                if (Main.rand.Next(2) == 0)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.position + projectile.velocity * 2f, projectile.width, projectile.height, 57, 0f, 0f, 100, default(Color), 2f)];
                    dust.noGravity = true;
                    dust.velocity *= 2f;
                    dust.velocity += projectile.localAI[0].ToRotationVector2();
                    dust.fadeIn = 1.5f;
                }
                float num52 = 18f;
                int num53 = 0;
                while ((float)num53 < num52)
                {
                    if (Main.rand.Next(4) == 0)
                    {
                        Vector2 position = projectile.position + projectile.velocity + projectile.velocity * ((float)num53 / num52);
                        Dust dust2 = Main.dust[Dust.NewDust(position, projectile.width, projectile.height, 57, 0f, 0f, 100, default(Color), 1f)];
                        dust2.noGravity = true;
                        dust2.fadeIn = 0.5f;
                        dust2.velocity += projectile.localAI[0].ToRotationVector2();
                    }
                    num53++;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Color color25 = Lighting.GetColor((int)((double)projectile.position.X + (double)projectile.width * 0.5) / 16, (int)(((double)projectile.position.Y + (double)projectile.height * 0.5) / 16.0));
            if (projectile.hide && !ProjectileID.Sets.DontAttachHideToAlpha[projectile.type])
            {
                color25 = Lighting.GetColor((int)mountedCenter.X / 16, (int)(mountedCenter.Y / 16f));
            }
            Vector2 projPos = projectile.position;
            projPos = new Vector2((float)projectile.width, (float)projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition; //fuck it
            Texture2D texture2D22 = Main.projectileTexture[projectile.type];
            Color alpha3 = projectile.GetAlpha(color25);
            if (projectile.velocity == Vector2.Zero)
            {
                return false;
            }
            // first segment
            float num230 = projectile.velocity.Length() + 16f;
            bool flag24 = num230 < 100f;
            Vector2 value28 = Vector2.Normalize(projectile.velocity);
            Rectangle rectangle8 = new Rectangle(0, 0, texture2D22.Width, 26);
            Vector2 value29 = new Vector2(0f, Main.player[projectile.owner].gfxOffY);
            float rotation24 = projectile.rotation + 3.14159274f;
            Main.spriteBatch.Draw(texture2D22, projectile.Center.Floor() - Main.screenPosition + value29, new Rectangle?(rectangle8), alpha3, rotation24, rectangle8.Size() / 2f - Vector2.UnitY * 4f, projectile.scale, SpriteEffects.None, 0f);
            //chain
            num230 -= 40f * projectile.scale; // how long chains are? dont mess with this, it messes stuff up
            Vector2 vector31 = projectile.Center.Floor();
            vector31 += value28 * projectile.scale * 16f; // changes where the chain part starts
            rectangle8 = new Rectangle(0, 42, texture2D22.Width, 10); 
            if (num230 > 0f)
            {
                float num231 = 0f;
                while (num231 + 1f < num230)
                {
                    if (num230 - num231 < (float)rectangle8.Height)
                    {
                        rectangle8.Height = (int)(num230 - num231);
                    }
                    Main.spriteBatch.Draw(texture2D22, vector31 - Main.screenPosition + value29, new Rectangle?(rectangle8), alpha3, rotation24, new Vector2((float)(rectangle8.Width / 2), 0f), projectile.scale, SpriteEffects.None, 0f);
                    num231 += (float)rectangle8.Height * projectile.scale;
                    vector31 += value28 * (float)rectangle8.Height * projectile.scale;
                }
            }
            //mid section
            Vector2 value30 = vector31;
            vector31 = projectile.Center.Floor();
            vector31 += value28 * projectile.scale * 24f;
            rectangle8 = new Rectangle(0, 28, texture2D22.Width, 12); 
            int num232 = 4; // how many chains there are
            if (flag24)
            {
                num232 = 2;
            }
            float num233 = num230;
            if (num230 > 0f)
            {
                float num234 = 0f;
                float num235 = num233 / (float)num232;
                num234 += num235 * 0.25f;
                vector31 += value28 * num235 * 0.25f;
                for (int num236 = 0; num236 < num232; num236++)
                {
                    float num237 = num235;
                    if (num236 == 0)
                    {
                        num237 *= 0.75f;
                    }
                    Main.spriteBatch.Draw(texture2D22, vector31 - Main.screenPosition + value29, new Rectangle?(rectangle8), alpha3, rotation24, new Vector2((float)(rectangle8.Width / 2), 0f), projectile.scale, SpriteEffects.None, 0f);
                    num234 += num237;
                    vector31 += value28 * num237;
                }
            }

            rectangle8 = new Rectangle(0, 54, texture2D22.Width, 24); //end
            Main.spriteBatch.Draw(texture2D22, value30 - Main.screenPosition + value29, new Rectangle?(rectangle8), alpha3, rotation24, texture2D22.Frame(1, 1, 0, 0).Top(), projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Vector2 unit = projectile.velocity;
            Utils.PlotTileLine(projectile.Center, projectile.Center + unit * projectile.localAI[1], (float)projectile.width * projectile.scale, new Utils.PerLinePoint(DelegateMethods.CutTiles));
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projHitbox.Intersects(targetHitbox))
            {
                return true;
            }
            float num8 = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, projectile.Center + projectile.velocity, 16f * projectile.scale, ref num8))
            {
                return true;
            }
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 180, false);
        }
    }
}