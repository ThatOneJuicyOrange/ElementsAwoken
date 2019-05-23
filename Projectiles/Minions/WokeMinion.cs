using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Minions
{
    public class WokeMinion : ModProjectile
    {
        public float shootTimer = 0f;
        public override void SetDefaults()
        {
            projectile.width = 46;
            projectile.height = 34;

            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.minion = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            projectile.tileCollide = false;

            projectile.minionSlots = 1f;
            projectile.timeLeft = 18000;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;

            //projectile.aiStyle = 54;
            //aiType = 317;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Watcher");
        }

        public override void AI()
        {
            bool flag64 = projectile.type == mod.ProjectileType("WokeMinion");
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
            player.AddBuff(mod.BuffType("AwakenedMinionBuff"), 3600);
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.wokeMinion = false;
                }
                if (modPlayer.wokeMinion)
                {
                    projectile.timeLeft = 2;
                }
            }
            Lighting.AddLight(projectile.Center, 0.3f, 0.3f, 0.3f);

            shootTimer--;

            if (shootTimer == 45)
            {
                Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/WokeLaserCharge"));
            }
            if (shootTimer <= 45 && shootTimer >= 0)
            {
                int numDusts = 20;
                if (shootTimer <= 45)
                {
                    numDusts = 1;
                }
                if (shootTimer <= 30)
                {
                    numDusts = 3;
                }
                if (shootTimer <= 20)
                {
                    numDusts = 7;
                }
                if (shootTimer <= 10)
                {
                    numDusts = 13;
                }
                for (int k = 0; k < numDusts; k++)
                {
                    int num5 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 219, 0f, 0f, 200, default(Color), 0.5f);
                    Main.dust[num5].noGravity = true;
                    Main.dust[num5].velocity *= 0.75f;
                    Main.dust[num5].fadeIn = 1.3f;
                    Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    vector.Normalize();
                    vector *= (float)Main.rand.Next(50, 100) * 0.04f;
                    Main.dust[num5].velocity = vector;
                    vector.Normalize();
                    vector *= 34f;
                    Main.dust[num5].position = projectile.Center - vector;
                }
            }
            if (projectile.owner == Main.myPlayer)
            {
                float max = 400f;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(projectile.Center, nPC.Center) <= max)
                    {
                        float Speed = 9f;
                        if (shootTimer <= 0)
                        {
                            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 33);
                            Vector2 leftEye = new Vector2(projectile.Center.X - 8f, projectile.Center.Y - 5);
                            Vector2 rightEye = new Vector2(projectile.Center.X + 8f, projectile.Center.Y - 5);

                            float leftRotation = (float)Math.Atan2(projectile.Center.Y - nPC.Center.Y, projectile.Center.X - nPC.Center.X);
                            float rightRotation = (float)Math.Atan2(projectile.Center.Y - nPC.Center.Y, projectile.Center.X - nPC.Center.X);

                            Vector2 leftSpeed = new Vector2((float)((Math.Cos(leftRotation) * Speed) * -1), (float)((Math.Sin(leftRotation) * Speed) * -1));
                            Vector2 rightSpeed = new Vector2((float)((Math.Cos(rightRotation) * Speed) * -1), (float)((Math.Sin(rightRotation) * Speed) * -1));

                            Projectile.NewProjectile(leftEye.X, leftEye.Y, leftSpeed.X, leftSpeed.Y, mod.ProjectileType("WokeBeam"), projectile.damage * 2, projectile.knockBack, projectile.owner);
                            Projectile.NewProjectile(rightEye.X, rightEye.Y, rightSpeed.X, rightSpeed.Y, mod.ProjectileType("WokeBeam"), projectile.damage * 2, projectile.knockBack, projectile.owner);
                            shootTimer = 75;
                        }
                    }
                }
            }


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

            float num535 = projectile.position.X;
            float num536 = projectile.position.Y;
            float num537 = 900f;
            bool attacking = false;
            int num538 = 500;

            if (Math.Abs(projectile.Center.X - player.Center.X) + Math.Abs(projectile.Center.Y - player.Center.Y) > (float)num538)
            {
                projectile.ai[0] = 1f;
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.tileCollide = true;
                NPC targettedNPC = projectile.OwnerMinionAttackTargetNPC;
                if (targettedNPC != null && targettedNPC.CanBeChasedBy(projectile, false))
                {
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    Vector2 offset = new Vector2((float)Math.Sin(angle) * 200, (float)Math.Cos(angle) * 200);

                    float targetX = targettedNPC.Center.X + offset.X;
                    float targetY = targettedNPC.Center.Y + offset.Y;
                    float num541 = Math.Abs(projectile.Center.X - targetX) + Math.Abs(projectile.Center.Y - targetY);
                    if (num541 < num537 && Collision.CanHit(projectile.position, projectile.width, projectile.height, targettedNPC.position, targettedNPC.width, targettedNPC.height))
                    {
                        num537 = num541;
                        num535 = targetX;
                        num536 = targetY;
                        attacking = true;
                    }
                }
                if (!attacking)
                {
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        NPC nPC = Main.npc[i];
                        if (nPC.CanBeChasedBy(projectile, false))
                        {
                            double angle = Main.rand.NextDouble() * 2d * Math.PI;
                            Vector2 offset = new Vector2((float)Math.Sin(angle) * 200, (float)Math.Cos(angle) * 200);

                            float targetX = nPC.Center.X + offset.X;
                            float targetY = nPC.Center.Y + offset.Y;
                            float num541 = Math.Abs(projectile.Center.X - targetX) + Math.Abs(projectile.Center.Y - targetY);
                            if (num541 < num537 && Collision.CanHit(projectile.position, projectile.width, projectile.height, nPC.position, nPC.width, nPC.height))
                            {
                                num537 = num541;
                                num535 = targetX;
                                num536 = targetY;
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
            // idle
            if (!attacking)
            {
                float num546 = 8f;
                if (projectile.ai[0] == 1f)
                {
                    num546 = 12f;
                }
                Vector2 vector42 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float num547 = player.Center.X - vector42.X;
                float num548 = player.Center.Y - vector42.Y - 60f;
                float num549 = (float)Math.Sqrt((double)(num547 * num547 + num548 * num548));
                if (num549 < 100f && projectile.ai[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.ai[0] = 0f;
                }
                if (num549 > 2000f)
                {
                    projectile.position.X = player.Center.X - (float)(projectile.width / 2);
                    projectile.position.Y = player.Center.Y - (float)(projectile.width / 2);
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

                if ((double)Math.Abs(projectile.velocity.X) > 0.2)
                {
                    projectile.spriteDirection = -projectile.direction;
                    return;
                }
            }
            // attack
            else
            {
                if (projectile.ai[1] == -1f)
                {
                    projectile.ai[1] = 17f;
                }
                if (projectile.ai[1] > 0f)
                {
                    projectile.ai[1] -= 1f;
                }
                if (projectile.ai[1] == 0f)
                {
                    float num550 = 8f;
                    float num551 = num535 - projectile.Center.X;
                    float num552 = num536 - projectile.Center.Y;
                    float num553 = (float)Math.Sqrt((double)(num551 * num551 + num552 * num552));
                    if (num553 < 100f)
                    {
                        num550 = 10f;
                    }
                    num553 = num550 / num553;
                    num551 *= num553;
                    num552 *= num553;
                    projectile.velocity.X = (projectile.velocity.X * 14f + num551) / 15f;
                    projectile.velocity.Y = (projectile.velocity.Y * 14f + num552) / 15f;
                }
                else
                {
                    if (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y) < 10f)
                    {
                        projectile.velocity *= 1.05f;
                    }
                }
                projectile.rotation = projectile.velocity.X * 0.05f;
                if ((double)Math.Abs(projectile.velocity.X) > 0.2)
                {
                    projectile.spriteDirection = -projectile.direction;
                    return;
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