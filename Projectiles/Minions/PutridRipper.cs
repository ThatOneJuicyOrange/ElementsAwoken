using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;

namespace ElementsAwoken.Projectiles.Minions
{
    public class PutridRipper : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 66;
            projectile.height = 82;

            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.minion = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            projectile.tileCollide = false;

            projectile.minionSlots = 1.5f;
            projectile.timeLeft = 18000;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Putrid Ripper");
            Main.projFrames[projectile.type] = 6;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 12)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.ai[1] <= 30)
                {
                    if (projectile.frame > 5)
                        projectile.frame = 3;
                }
                else
                {
                    if (projectile.frame > 2)
                        projectile.frame = 0;
                }
            }
            return true;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (ModContent.GetInstance<Config>().debugMode)
            {
                spriteBatch.DrawString(Main.fontMouseText, "ai[0]: " + projectile.ai[0], projectile.Hitbox.TopLeft() + new Vector2(0, -10), Color.White);
            }
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(mod.BuffType("PutridRipperBuff"), 3600);
            if (player.dead)
            {
                modPlayer.putridRipper = false;
            }
            if (modPlayer.putridRipper)
            {
                projectile.timeLeft = 2;
            }
            float viewDist = 500f;
            float chaseDist = 200f;
            float chaseAccel = 6f;
            float inertia = 40f;

            for (int k = 0; k < Main.projectile.Length; k++)
            {
                Projectile other = Main.projectile[k];
                if (k != projectile.whoAmI && other.type == projectile.type && other.active && Math.Abs(projectile.position.X - other.position.X) + Math.Abs(projectile.position.Y - other.position.Y) < projectile.width)
                {
                    const float pushAway = 0.05f;
                    if (projectile.position.X < other.position.X)
                    {
                        projectile.velocity.X -= pushAway;
                    }
                    else
                    {
                        projectile.velocity.X += pushAway;
                    }
                    if (projectile.position.Y < other.position.Y)
                    {
                        projectile.velocity.Y -= pushAway;
                    }
                    else
                    {
                        projectile.velocity.Y += pushAway;
                    }
                }
            }

            Vector2 targetPos = projectile.position;
            float targetDist = viewDist;
            bool target = false;
            projectile.tileCollide = true;
            for (int k = 0; k < 200; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.CanBeChasedBy(this, false))
                {
                    float distance = Vector2.Distance(npc.Center, projectile.Center);
                    if ((distance < targetDist/* || !target*/) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height))
                    {
                        targetDist = distance;
                        targetPos = npc.Center;
                        target = true;
                    }
                }
            }
            if (Vector2.Distance(player.Center, projectile.Center) > (target ? 1000f : 500f))
            {
                projectile.ai[0] = 1f;
                projectile.netUpdate = true;
            }
            if (projectile.ai[0] == 1f)
            {
                projectile.tileCollide = false;
            }
            if (target && projectile.ai[0] == 0f)
            {
                Vector2 direction = targetPos - projectile.Center;
                if (direction.Length() > chaseDist)
                {
                    direction.Normalize();
                    projectile.velocity = (projectile.velocity * inertia + direction * chaseAccel) / (inertia + 1);
                }
                else
                {
                    projectile.velocity *= (float)Math.Pow(0.97, 40.0 / inertia);
                }
            }
            else
            {
                if (!Collision.CanHitLine(projectile.Center, 1, 1, player.Center, 1, 1))
                {
                    projectile.ai[0] = 1f;
                }
                /*float speed = 6f;
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
                    float temp = inertia / 2f;
                    projectile.velocity = (projectile.velocity * temp + direction) / (temp + 1);
                }
                else
                {
                    projectile.direction = Main.player[projectile.owner].direction;
                    projectile.velocity *= (float)Math.Pow(0.9, 40.0 / inertia);
                }*/
                float num546 = 8f;
                if (projectile.ai[0] == 1f)
                {
                    num546 = 12f;
                }
                if (Vector2.Distance(player.Center,projectile.Center) < 100f && projectile.ai[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.ai[0] = 0f;
                    projectile.netUpdate = true;
                }
                Vector2 vector42 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float num547 = player.Center.X - vector42.X;
                float num548 = player.Center.Y - vector42.Y - 60f;
                float num549 = (float)Math.Sqrt((double)(num547 * num547 + num548 * num548));
                if (num549 > 2000f)
                {
                    projectile.Center = player.Center;
                }
                if (num549 > 70f)
                {
                    num549 = num546 / num549;
                    num547 *= num549;
                    num548 *= num549;
                    projectile.velocity.X = (projectile.velocity.X * 20f + num547) / 21f;
                    projectile.velocity.Y = (projectile.velocity.Y * 20f + num548) / 21f;
                }
                else
                {
                    if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
                    {
                        projectile.velocity.X = -0.15f;
                        projectile.velocity.Y = -0.05f;
                    }
                    projectile.velocity *= 1.01f;
                }
                projectile.rotation = projectile.velocity.X * 0.05f;
            }
            projectile.rotation = projectile.velocity.X * 0.05f;

            if (projectile.ai[0] == 0f)
            {
                if (target)
                {
                    projectile.ai[1]--;
                    if (projectile.ai[1] == 0f)
                    {
                        Main.PlaySound(SoundID.NPCDeath13, projectile.Center);
                    }
                    if (projectile.ai[1] <= 0f && projectile.ai[1] % 2 == 0)
                    {
                        if (projectile.ai[1] < -30)
                        {
                            projectile.ai[1] = 180f;
                        }
                        if (ModContent.GetInstance<Config>().debugMode)
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                Dust dust = Main.dust[Dust.NewDust(targetPos - new Vector2(0, MathHelper.Lerp(0, 90, MathHelper.Clamp(Math.Abs(projectile.Center.X - targetPos.X), 0, 300) / 300)), 2, 2, 6)];
                                dust.noGravity = true;
                            }
                        }
                        if (Main.myPlayer == projectile.owner && targetDist < 300)
                        {
                            Vector2 mouth = projectile.Center + new Vector2(0, -22);

                            Vector2 shootVel = targetPos - projectile.Center -new Vector2(0, MathHelper.Lerp(0,90,MathHelper.Clamp(Math.Abs(projectile.Center.X - targetPos.X), 0, 300) / 300)); // to make it aim more up the further away the target is
                            if (shootVel == Vector2.Zero) shootVel = new Vector2(0f, 1f);
                            shootVel.Normalize();
                            shootVel *= 5f; 
                            shootVel = shootVel.RotatedByRandom(MathHelper.ToRadians(12));

                            Projectile proj = Main.projectile[Projectile.NewProjectile(mouth.X, mouth.Y, shootVel.X, shootVel.Y, mod.ProjectileType("PutridVomit"), projectile.damage / 3, projectile.knockBack, Main.myPlayer, 0f, 0f)];
                            proj.timeLeft = 300;
                            proj.netUpdate = true;
                            projectile.netUpdate = true;
                            projectile.spriteDirection = (projectile.direction = Math.Sign(shootVel.X));
                        }
                    }
                }
                if (projectile.ai[1] > 0 || !target)
                {
                    if (projectile.velocity.X > 0f)
                    {
                        projectile.spriteDirection = -(projectile.direction = -1);
                    }
                    else if (projectile.velocity.X < 0f)
                    {
                        projectile.spriteDirection = -(projectile.direction = 1);
                    }
                }
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