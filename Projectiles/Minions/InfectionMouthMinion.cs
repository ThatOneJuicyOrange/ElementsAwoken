using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using ElementsAwoken.Projectiles.GlobalProjectiles;

namespace ElementsAwoken.Projectiles.Minions
{
    public class InfectionMouthMinion : ModProjectile
    {
        public float dashTimer = 0;
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;

            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.tileCollide = false;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;

            // projectile.aiStyle = 54;
            //aiType = 317;

            projectile.minionSlots = 1;
            projectile.timeLeft = 18000;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hungry");
            Main.projFrames[projectile.type] = 4;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Rectangle rectangle = new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type]);
                spriteBatch.Draw(tex, drawPos, rectangle, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }

            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
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
            projectile.rotation += projectile.velocity.X * 0.04f;
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(mod.BuffType("InfectionMouthBuff"), 3600);

                if (player.dead)
                {
                    modPlayer.azanaMinions = false;
                }
                if (modPlayer.azanaMinions)
                {
                    projectile.timeLeft = 2;
                }
                if (Main.rand.NextBool(120))
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 5)];
                dust.velocity = Vector2.Zero;
            }
            ProjectileUtils.PushOtherEntities(projectile);

            float targetX = projectile.position.X;
            float targetY = projectile.position.Y;
            float targetDist = 1200f;
            bool attacking = false;
            int maxDist = 1200;
            if (projectile.ai[1] != 0f)
            {
                maxDist = 1400;
            }
            if (Math.Abs(projectile.Center.X - player.Center.X) + Math.Abs(projectile.Center.Y - player.Center.Y) > (float)maxDist)
            {
                projectile.ai[0] = 1f;
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.tileCollide = true;
                NPC ownerMinionAttackTargetNPC10 = projectile.OwnerMinionAttackTargetNPC;
                if (ownerMinionAttackTargetNPC10 != null && ownerMinionAttackTargetNPC10.CanBeChasedBy(this))
                {
                    float num1059 = ownerMinionAttackTargetNPC10.position.X + (float)(ownerMinionAttackTargetNPC10.width / 2);
                    float num1058 = ownerMinionAttackTargetNPC10.position.Y + (float)(ownerMinionAttackTargetNPC10.height / 2);
                    float num1057 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num1059) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num1058);
                    if (num1057 < targetDist && Collision.CanHit(projectile.position, projectile.width, projectile.height, ownerMinionAttackTargetNPC10.position, ownerMinionAttackTargetNPC10.width, ownerMinionAttackTargetNPC10.height))
                    {
                        targetDist = num1057;
                        targetX = num1059;
                        targetY = num1058;
                        attacking = true;
                    }
                }
                if (!attacking)
                {
                    for (int num1056 = 0; num1056 < 200; num1056++)
                    {
                        if (Main.npc[num1056].CanBeChasedBy(this))
                        {
                            float num1055 = Main.npc[num1056].position.X + (float)(Main.npc[num1056].width / 2);
                            float num1054 = Main.npc[num1056].position.Y + (float)(Main.npc[num1056].height / 2);
                            float num1053 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num1055) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num1054);
                            if (num1053 < targetDist && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[num1056].position, Main.npc[num1056].width, Main.npc[num1056].height))
                            {
                                targetDist = num1053;
                                targetX = num1055;
                                targetY = num1054;
                                attacking = true;
                            }
                        }
                    }
                }
            }
            else
            {
                projectile.tileCollide = false;
            }
            if (!attacking)
            {
                projectile.friendly = true;
                float speed = 16f;
                if (projectile.ai[0] == 1f)
                {
                    speed = 20f;
                }
                Vector2 vector301 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float num1050 = player.Center.X - vector301.X;
                if (player.velocity.X != 0)
                {
                    num1050 = player.Center.X + 300 * Math.Sign(player.velocity.X) - vector301.X;
                    speed = Math.Abs(player.velocity.X * 1.4f);
                }
                float num1049 = player.Center.Y - vector301.Y - 60f;
                float num1048 = (float)Math.Sqrt(num1050 * num1050 + num1049 * num1049);
                if (num1048 < 100f && projectile.ai[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.ai[0] = 0f;
                }
                if (num1048 > 2000f)
                {
                    projectile.position.X = player.Center.X - (float)(projectile.width / 2);
                    projectile.position.Y = player.Center.Y - (float)(projectile.width / 2);
                }
                if (num1048 > 70f)
                {
                    num1048 = speed / num1048;
                    num1050 *= num1048;
                    num1049 *= num1048;
                    projectile.velocity.X = (projectile.velocity.X * 20f + num1050) / 21f;
                    projectile.velocity.Y = (projectile.velocity.Y * 20f + num1049) / 21f;
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
                projectile.friendly = false;
                projectile.rotation = projectile.velocity.X * 0.05f;
                if ((double)Math.Abs(projectile.velocity.X) > 0.2)
                {
                    projectile.spriteDirection = -projectile.direction;
                }
                return;
            }
            if (projectile.ai[1] > 0f)
            {
                projectile.ai[1] -= 1f;
            }
            if (projectile.ai[1] == 0f)
            {
                dashTimer--;
                projectile.friendly = true;
                float speed = 8f;
                float num1042 = targetX - projectile.Center.X;
                float num1041 = targetY - projectile.Center.Y;
                float dist = (float)Math.Sqrt(num1042 * num1042 + num1041 * num1041);
                if (dist < 100f)
                {
                    speed = 10f;
                }
                if (dist < 300f && dashTimer <= 0)
                {
                    Vector2 toTarget = new Vector2(num1042, num1041);
                    toTarget.Normalize();
                    toTarget *= 20f;
                    projectile.velocity.X = toTarget.X;
                    projectile.velocity.Y = toTarget.Y;

                    dashTimer = 30;
                }
                dist = speed / dist;
                num1042 *= dist;
                num1041 *= dist;
                projectile.velocity.X = (projectile.velocity.X * 14f + num1042) / 15f;
                projectile.velocity.Y = (projectile.velocity.Y * 14f + num1041) / 15f;
            }
            else
            {
                projectile.friendly = false;
                if (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y) < 10f)
                {
                    projectile.velocity *= 1.05f;
                }
            }
            projectile.rotation = projectile.velocity.X * 0.05f;
            if ((double)Math.Abs(projectile.velocity.X) > 0.2)
            {
                projectile.spriteDirection = -projectile.direction;
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
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;

            projectile.ai[1] = 3f; // this is to make them not stick to the enemy so hard

            ProjectileUtils.Explosion(projectile, new int[] { 127 }, projectile.damage);
        }
    }
}