using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace ElementsAwoken.Projectiles.Minions
{
    public class HungryMinion : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;

            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.minion = true;
            projectile.tileCollide = false;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;

            // projectile.aiStyle = 54;
            //aiType = 317;

            projectile.timeLeft = 18000;
            projectile.alpha = 30;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hungry");
            Main.projFrames[projectile.type] = 3;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModContent.GetTexture("ElementsAwoken/Projectiles/Flails/HungryFlailChain");

            Vector2 position = projectile.Center;
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float num1 = (float)texture.Height;
            Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if ((double)vector2_4.Length() < (double)num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1.35f, SpriteEffects.None, 0.0f);
                }
            }

            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 2)
                    projectile.frame = 0;
            }
            return true;
        }
    
        public override void AI()
        {
               Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.dead || !player.active || !modPlayer.hellHeart) projectile.Kill();
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
            float targetX = projectile.position.X;
            float targetY = projectile.position.Y;
            float targetDist = 900f;
            bool attacking = false;
            int maxDist = 500;
            if (projectile.ai[1] != 0f)
            {
                maxDist = 700;
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
                float num1051 = 8f;
                if (projectile.ai[0] == 1f)
                {
                    num1051 = 12f;
                }
                Vector2 vector301 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float num1050 = player.Center.X - vector301.X;
                if (player.velocity.X != 0)
                {
                    num1050 = player.Center.X + 300 * Math.Sign(player.velocity.X) - vector301.X;
                    num1051 = Math.Abs(player.velocity.X * 1.4f);
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
                    num1048 = num1051 / num1048;
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
                projectile.friendly = true;
                float num1043 = 8f;
                Vector2 vector300 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float num1042 = targetX - vector300.X;
                float num1041 = targetY - vector300.Y;
                float num1040 = (float)Math.Sqrt(num1042 * num1042 + num1041 * num1041);
                if (num1040 < 100f)
                {
                    num1043 = 10f;
                }
                num1040 = num1043 / num1040;
                num1042 *= num1040;
                num1041 *= num1040;
                float speed = 14f;
                //if (targetDist < 20) speed = 4f;
                projectile.velocity.X = (projectile.velocity.X * speed + num1042) / 15f;
                projectile.velocity.Y = (projectile.velocity.Y * speed + num1041) / 15f;
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
            Player player = Main.player[projectile.owner];
            if (player.statLife < player.statLifeMax2)
            {
                int heal = Main.rand.Next(1, 2);
                player.statLife += heal;
                player.HealEffect(heal);
            }
            projectile.ai[1] = 9f; // this is to make them not stick to the enemy so hard
        }
    }
}