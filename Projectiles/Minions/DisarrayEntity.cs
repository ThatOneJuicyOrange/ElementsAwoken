using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Projectiles.Minions
{   
    public class DisarrayEntity : ModProjectile
    {
        public float shootTimer = 0f;
        public float shootTimer2 = 0f;
        private float shootAi = 0f;

        private float dashAi = 0f;
        private int dashCount = 0;
        Vector2 dashTargetPos = new Vector2();
        NPC dashTarget = null;

        NPC tpTarget = null;
        private float tpAi = -1f;
        public override void SetDefaults()
        {
            projectile.width = 48;
            projectile.height = 56;

            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.minion = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            projectile.tileCollide = false;

            projectile.minionSlots = 2f;
            projectile.timeLeft = 18000;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;

            //projectile.aiStyle = 54;
            //aiType = 317;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 9;
            DisplayName.SetDefault("Crystalline Entity");
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 16)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.ai[0] == 0 && projectile.frame != 0) projectile.frame = 0;
            else if (projectile.ai[0] == 1)
            {
                if (projectile.frame < 1) projectile.frame = 1;
                if (projectile.frame > 2) projectile.frame = 1;
            }
            else if (projectile.ai[0] == 2)
            {
                if (projectile.frame < 3) projectile.frame = 3;
                if (projectile.frame > 5) projectile.frame = 3;
            }
            else if (projectile.ai[0] == 3)
            {
                if (projectile.frame < 6) projectile.frame = 6;
                if (projectile.frame > 8) projectile.frame = 6;
            }

            if (tpAi >= 0)
            {
                Texture2D texture = GetTexture("ElementsAwoken/Projectiles/Minions/DisarrayCrystal");
                Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, (Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]) * 0.5f);
                for (int k = 0; k < 4; k++)
                {
                    Vector2 drawPos = new Vector2(projectile.Center.X + 50 * (k >= 2 ? -1 : 1), projectile.Center.Y - 50 * (k % 2 == 0 ? -1 : 1)) - Main.screenPosition + drawOrigin;
                    sb.Draw(texture, drawPos, null, Color.White, 0f, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            player.AddBuff(mod.BuffType("CrystallineEntityBuff"), 3600);
            if (player.dead) modPlayer.crystalEntity = false;
            if (modPlayer.crystalEntity) projectile.timeLeft = 2;

            float intensity = 0.2f * (projectile.ai[0] + 1);
            Lighting.AddLight(projectile.Center, 1.2f * intensity, 0f * intensity, 1.5f * intensity);

            if (projectile.ai[0] != 3)
            {
                shootTimer--;
                shootTimer2--;
                if (shootTimer2 <= 0) shootTimer2 = 120;
                if (projectile.ai[0] < 3 && projectile.ai[1] == 0)
                {
                    if (shootTimer <= 0)
                    {
                        if (projectile.owner == Main.myPlayer)
                        {
                            float max = 400f;
                            for (int i = 0; i < Main.npc.Length; i++)
                            {
                                NPC nPC = Main.npc[i];
                                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(projectile.Center, nPC.Center) <= max && Collision.CanHit(projectile.position, projectile.width, projectile.height, nPC.position, nPC.width, nPC.height))
                                {
                                    float Speed = 7f;
                                    if (projectile.ai[0] == 1) Speed = 9.5f;
                                    else if (projectile.ai[0] == 2) Speed = 11f;
                                    if ((projectile.ai[0] != 1 && shootTimer <= 0) || (shootTimer <= 0f && shootTimer2 <= 24))
                                    {
                                        Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 28);
                                        float rotation = (float)Math.Atan2(projectile.Center.Y - nPC.Center.Y, projectile.Center.X - nPC.Center.X);
                                        Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));

                                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projSpeed.X, projSpeed.Y, mod.ProjectileType("DisarrayShard"), projectile.damage, projectile.knockBack, projectile.owner);
                                        if (projectile.ai[0] == 0) shootTimer = 75;
                                        else if (projectile.ai[0] == 1)
                                        {
                                            shootTimer = 8;
                                        }
                                        else
                                        {
                                            shootTimer = 12;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (shootTimer == 5)
                    {
                        for (int k = 0; k < 20; k++)
                        {
                            int num5 = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("AncientPink"), 0f, 0f, 200, default(Color), 1.2f);
                            Main.dust[num5].noGravity = true;
                            Main.dust[num5].velocity *= 0.75f;
                            Main.dust[num5].fadeIn = 1.8f;
                            Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                            vector.Normalize();
                            vector *= (float)Main.rand.Next(50, 100) * 0.04f;
                            Main.dust[num5].velocity = vector;
                            vector.Normalize();
                            vector *= 34f;
                            Main.dust[num5].position = projectile.Center - vector;

                        } // pre shoot dust
                    }
                }


                float targetLocX = projectile.position.X;
                float targetLocY = projectile.position.Y;
                float closestTarget = 900f;
                bool attacking = false;
                if (projectile.ai[1] != 0)
                {
                    attacking = true;
                }
                int maxPlayerDist = 500; // how far away the player can be

                if (Math.Abs(projectile.Center.X - player.Center.X) + Math.Abs(projectile.Center.Y - player.Center.Y) > (float)maxPlayerDist)
                {
                    projectile.localAI[0] = 1f;
                }
                if (projectile.localAI[0] == 0f)
                {
                    projectile.tileCollide = true;
                    NPC targettedNPC = projectile.OwnerMinionAttackTargetNPC;
                    if (targettedNPC != null && targettedNPC.CanBeChasedBy(projectile, false))
                    {
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        Vector2 offset = new Vector2((float)Math.Sin(angle) * 200, (float)Math.Cos(angle) * 200);

                        float targetX = targettedNPC.Center.X + offset.X;
                        float targetY = targettedNPC.Center.Y + offset.Y;
                        float npcDist = Math.Abs(projectile.Center.X - targetX) + Math.Abs(projectile.Center.Y - targetY);
                        if (npcDist < closestTarget && Collision.CanHit(projectile.position, projectile.width, projectile.height, targettedNPC.position, targettedNPC.width, targettedNPC.height))
                        {
                            closestTarget = npcDist;
                            targetLocX = targetX;
                            targetLocY = targetY;
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
                                float npcDist = Math.Abs(projectile.Center.X - targetX) + Math.Abs(projectile.Center.Y - targetY);
                                if (npcDist < closestTarget && Collision.CanHit(projectile.position, projectile.width, projectile.height, nPC.position, nPC.width, nPC.height))
                                {
                                    closestTarget = npcDist;
                                    targetLocX = targetX;
                                    targetLocY = targetY;
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
                    float speed = 8f;
                    if (projectile.localAI[0] == 1f) speed = 12f; // too far from player
                    float goToX = player.Center.X - projectile.Center.X;
                    float goToY = player.Center.Y - projectile.Center.Y - 60f;
                    float targetDist = (float)Math.Sqrt((double)(goToX * goToX + goToY * goToY));
                    if (targetDist < 100f && projectile.localAI[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height)) // if in range of player and not in tile
                    {
                        projectile.localAI[0] = 0f;
                    }
                    if (targetDist > 2000f) projectile.Center = player.Center;
                    if (targetDist > 70f)
                    {
                        targetDist = speed / targetDist;
                        goToX *= targetDist;
                        goToY *= targetDist;
                        projectile.velocity.X = (projectile.velocity.X * 20f + goToX) / 21f;
                        projectile.velocity.Y = (projectile.velocity.Y * 20f + goToY) / 21f;
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
                    if (projectile.ai[0] != 3)
                    {
                        if (projectile.ai[1] == 0)
                        {
                            float speed = 8f;
                            float goToX = targetLocX - projectile.Center.X;
                            float goToY = targetLocY - projectile.Center.Y;
                            float targetDist = (float)Math.Sqrt((double)(goToX * goToX + goToY * goToY));
                            if (targetDist < 100f)
                            {
                                speed = 10f;
                            }
                            targetDist = speed / targetDist;
                            goToX *= targetDist;
                            goToY *= targetDist;
                            projectile.velocity.X = (projectile.velocity.X * 14f + goToX) / 15f;
                            projectile.velocity.Y = (projectile.velocity.Y * 14f + goToY) / 15f;

                            if (projectile.ai[0] == 2)
                            {
                                shootAi++;
                                if (shootAi > 300)
                                {
                                    shootAi = 0;
                                    projectile.ai[1] = 1;
                                }
                            }
                        }
                        else // dashing
                        {
                            projectile.tileCollide = false;
                            if (projectile.localAI[1] == 0f)
                            {
                                for (int i = 0; i < Main.npc.Length; i++)
                                {
                                    NPC nPC = Main.npc[i];
                                    if (nPC.CanBeChasedBy(projectile, false))
                                    {
                                        float npcDist = Math.Abs(projectile.Center.X - nPC.Center.X) + Math.Abs(projectile.Center.Y - nPC.Center.Y);
                                        if (nPC.active && npcDist < closestTarget && Collision.CanHit(projectile.position, projectile.width, projectile.height, nPC.position, nPC.width, nPC.height))
                                        {
                                            closestTarget = npcDist;
                                            dashTarget = nPC;
                                            dashTargetPos = nPC.Center;
                                        }
                                    }
                                }
                                if (dashTargetPos.X != 0 && dashTargetPos.Y != 0)
                                {
                                    dashAi++;
                                    if (dashAi >= 10)
                                    {
                                        float speed = 14f;
                                        Vector2 toTarget = new Vector2(dashTargetPos.X - projectile.Center.X, dashTargetPos.Y - projectile.Center.Y);
                                        toTarget.Normalize();
                                        projectile.velocity = toTarget * speed;
                                        projectile.localAI[1] = 1f;
                                        projectile.netUpdate = true;
                                        dashAi = 0;
                                    }
                                }
                            }
                            else if (projectile.localAI[1] == 1f)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("AncientPink"));
                                    Main.dust[dust].velocity *= 0.1f;
                                    Main.dust[dust].scale *= 1.2f;
                                    Main.dust[dust].noGravity = true;
                                }

                                dashAi += 1f;
                                if (dashAi >= 20f)
                                {
                                    projectile.velocity *= 0.96f;
                                    if ((double)projectile.velocity.X > -0.1 && (double)projectile.velocity.X < 0.1)
                                    {
                                        projectile.velocity.X = 0f;
                                    }
                                    if ((double)projectile.velocity.Y > -0.1 && (double)projectile.velocity.Y < 0.1)
                                    {
                                        projectile.velocity.Y = 0f;
                                    }
                                }
                                int dashTime = 30;
                                if (dashAi >= dashTime)
                                {
                                    dashCount += 1;
                                    dashAi = 0f;
                                    if (dashCount >= 3)
                                    {
                                        projectile.localAI[1] = 0f;
                                        projectile.ai[1] = 0f;
                                        dashAi = 0f;
                                        dashCount = 0;
                                    }
                                    else
                                    {
                                        projectile.localAI[1] = 0f;
                                        projectile.velocity *= 0.1f;
                                    }
                                }
                            }
                            if (dashTarget == null || !dashTarget.active)
                            {
                                projectile.ai[1] = 0f;
                            }
                        }
                    }
                    else
                    {

                    }
                    projectile.rotation = projectile.velocity.X * 0.05f;
                    if ((double)Math.Abs(projectile.velocity.X) > 0.2)
                    {
                        projectile.spriteDirection = -projectile.direction;
                        return;
                    }
                }
            }
            if (projectile.ai[0] == 3)
            {
                bool attacking = false;
                if (tpTarget != null)
                {
                    if (tpTarget.active) attacking = true;                
                    else FindTpTarget();
                }
                else FindTpTarget();
                if (!attacking)
                {
                    tpAi = -1f;
                    // fly around

                    float speed = 8f;
                    if (projectile.localAI[0] == 1f) speed = 12f; // too far from player
                    float goToX = player.Center.X - projectile.Center.X;
                    float goToY = player.Center.Y - projectile.Center.Y - 60f;
                    float targetDist = (float)Math.Sqrt((double)(goToX * goToX + goToY * goToY));
                    if (targetDist < 100f && projectile.localAI[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height)) // if in range of player and not in tile
                    {
                        projectile.localAI[0] = 0f;
                    }
                    if (targetDist > 2000f) projectile.Center = player.Center;
                    if (targetDist > 70f)
                    {
                        targetDist = speed / targetDist;
                        goToX *= targetDist;
                        goToY *= targetDist;
                        projectile.velocity.X = (projectile.velocity.X * 20f + goToX) / 21f;
                        projectile.velocity.Y = (projectile.velocity.Y * 20f + goToY) / 21f;
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
                else
                {
                    if (!tpTarget.active) attacking = false;
                    else
                    {
                        projectile.spriteDirection = Math.Sign(projectile.Center.X - tpTarget.Center.X);


                        tpAi++;
                        Dust dust2 = Main.dust[Dust.NewDust(new Vector2(tpTarget.Center.X, tpTarget.Center.Y), 25, 25, 6, 0, 0, 100)];
                        dust2.noGravity = true;
                        if (tpAi == 20)
                        {
                            int maxDist = 250;
                            double angle = Main.rand.NextDouble() * 2d * Math.PI;
                            Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);

                            TpDust();
                            projectile.Center = tpTarget.Center + offset;
                            projectile.velocity = Vector2.Zero;
                            projectile.rotation = 0f;
                            TpDust();
                        }
                        if (tpAi == 30)
                        {
                            float Speed = 22f;

                            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 62);
                            float rotation = (float)Math.Atan2(projectile.Center.Y - tpTarget.Center.Y, projectile.Center.X - tpTarget.Center.X);
                            Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));

                            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projSpeed.X, projSpeed.Y, mod.ProjectileType("DisarrayBlast"), projectile.damage, projectile.knockBack, projectile.owner);
                            for (int k = 0; k < 4; k++)
                            {
                                Vector2 crystalPos = new Vector2(projectile.Center.X + 50 * (k >= 2 ? -1 : 1), projectile.Center.Y - 50 * (k % 2 == 0 ? -1 : 1));
                                float beamRotation = (float)Math.Atan2(crystalPos.Y - tpTarget.Center.Y, crystalPos.X - tpTarget.Center.X);
                                Vector2 beamSpeed = new Vector2((float)((Math.Cos(beamRotation) * Speed) * -1), (float)((Math.Sin(beamRotation) * Speed) * -1));
                                Projectile.NewProjectile(crystalPos.X, crystalPos.Y, beamSpeed.X, beamSpeed.Y, mod.ProjectileType("DisarrayBeam"), (int)(projectile.damage * 0.75f), projectile.knockBack, projectile.owner);

                            }

                            tpAi = 0;
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
            if (projectile.localAI[1] == 1f)
            {
                target.immune[projectile.owner] = 10;
            }
        }

        private void FindTpTarget()
        {
            float max = 600f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(projectile.Center, nPC.Center) <= max)
                {
                    tpTarget = nPC;
                }
            }
        }
        private void TpDust()
        {
            for (int k = 0; k < 30; k++)
            {
                int num5 = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("AncientPink"), 0f, 0f, 200, default(Color), 1.2f);
                Main.dust[num5].noGravity = true;
                Main.dust[num5].velocity *= 0.75f;
                Main.dust[num5].fadeIn = 1.8f;
                Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                vector.Normalize();
                vector *= (float)Main.rand.Next(50, 100) * 0.04f;
                Main.dust[num5].velocity = vector;
                vector.Normalize();
                vector *= 34f;
                Main.dust[num5].position = projectile.Center - vector;

            }
        }
    }
}