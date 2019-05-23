using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Volcanox
{
    [AutoloadBossHead]
    public class SoulOfInfernace : ModNPC
    {
        public float shootTimer1 = 0f;
        public float shootTimer2 = 0f;
        public float fireTimer = 0f;
        public float fireAITimer = 0f;
        public float tpCooldown1 = 300f;
        public float tpDustCooldown = 10f;

        int projectileBaseDamage = 90;

        bool runTPAlphaChange = false;
        int tpAlphaChangeTimer = 0;
        float telePosX = 0;
        float telePosY = 0;
        public override void SetDefaults()
        {
            npc.width = 120;
            npc.height = 90;

            npc.lifeMax = 50000;
            npc.damage = 90;
            npc.defense = 55;
            npc.knockBackResist = 0f;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;

            npc.HitSound = SoundID.NPCHit52;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.alpha = 150;

            npc.value = Item.buyPrice(0, 3, 0, 0);
            npc.npcSlots = 1f;

            bossBag = mod.ItemType("InfernaceBag");

            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Infernace");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 120;
            npc.lifeMax = 75000;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 100000;
                npc.damage = 150;
                npc.defense = 70;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            ++npc.frameCounter;
            if (npc.frameCounter >= 30.0)
                npc.frameCounter = 0.0;
            npc.frame.Y = frameHeight * (int)(npc.frameCounter / 6.0);

            //harpy rotation
            npc.rotation = npc.velocity.X * 0.1f;
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.OnFire, 180, false);
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override void AI()
        {
            Player P = Main.player[npc.target];
            // despawn
            if (!NPC.AnyNPCs(mod.NPCType("Volcanox")))
            {
                npc.active = false;
            }

            Lighting.AddLight(npc.Center, ((255 - npc.alpha) * 0.4f) / 255f, ((255 - npc.alpha) * 0.1f) / 255f, ((255 - npc.alpha) * 0f) / 255f);
            int dust = Dust.NewDust(npc.position, npc.width, npc.height, 6);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;

            Vector2 infernaceCenter = new Vector2(npc.Center.X, npc.Center.Y);
            npc.ai[1] += 1f;
            fireTimer--;
            tpCooldown1--;
            tpDustCooldown--;
            if (shootTimer1 > 0f)
            {
                shootTimer1 -= 1f;
            }
            if (shootTimer2 > 0f)
            {
                shootTimer2 -= 1f;
            }

            if (npc.life > npc.lifeMax * 0.75f)
            {
                if (npc.ai[1] > 1060f)
                {
                    npc.ai[1] = 0f;
                }
            }
            else if (npc.life <= npc.lifeMax * 0.75f && npc.life > npc.lifeMax * 0.5f)
            {
                if (npc.ai[1] > 1660f)
                {
                    npc.ai[1] = 0f;
                }
            }
            else if (npc.life <= npc.lifeMax * 0.45f)
            {
                if (npc.ai[1] > 1900f)
                {
                    npc.ai[1] = 0f;
                }
            }

            if (runTPAlphaChange)
            {
                tpAlphaChangeTimer++;
                if (tpAlphaChangeTimer < 20)
                {
                    npc.alpha += 8;
                }
                if (tpAlphaChangeTimer == 20)
                {
                    npc.position.X = telePosX;
                    npc.position.Y = telePosY;
                }
                if (tpAlphaChangeTimer > 20)
                {
                    npc.alpha -= 8;
                    if (npc.alpha <= 0)
                    {
                        runTPAlphaChange = false;
                    }
                }
            }
            else
            {
                npc.alpha = 0;
            }
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }
            if (npc.alpha > 150)
            {
                npc.alpha = 150;
            }

            MoveDirect(P, 3f);
            if (npc.ai[1] < 700f)
            {
                if (Main.netMode != 1 && shootTimer1 == 0f)
                {
                    Spike(P, 12f, projectileBaseDamage);
                    shootTimer1 = 100f;
                }
                //tp dust
                int maxdusts = 20;
                if (tpCooldown1 <= 20f && tpDustCooldown <= 0)
                {
                    for (int i = 0; i < maxdusts; i++)
                    {
                        float dustDistance = 100;
                        float dustSpeed = 10;
                        Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                        Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                        Dust vortex = Dust.NewDustPerfect(npc.Center + offset, 6, velocity, 0, default(Color), 1.5f);
                        vortex.noGravity = true;

                        tpDustCooldown = 5;
                    }
                }
                //teleport
                if (tpCooldown1 <= 0f)
                {
                    int distance = 200 + Main.rand.Next(0, 200);
                    int choice = Main.rand.Next(4);
                    if (choice == 0)
                    {
                        Teleport(Main.player[npc.target].position.X + distance, Main.player[npc.target].position.Y - distance);
                    }
                    if (choice == 1)
                    {
                        Teleport(Main.player[npc.target].position.X - distance, Main.player[npc.target].position.Y - distance);
                    }
                    if (choice == 2)
                    {
                        Teleport(Main.player[npc.target].position.X + distance, Main.player[npc.target].position.Y + distance);
                    }
                    if (choice == 3)
                    {
                        Teleport(Main.player[npc.target].position.X - distance, Main.player[npc.target].position.Y + distance);
                    }
                    tpCooldown1 = 300f;
                }
            }
            if (npc.ai[1] == 700f)
            {
                fireAITimer = 0f;
            }
            // greek fire and fly upwards
            if (npc.ai[1] >= 700f && npc.ai[1] <= 1060)
            {
                fireAITimer++;
                npc.velocity.X = 0;
                npc.velocity.Y = -6;
                if (fireAITimer == 1f)
                {
                    Teleport(Main.player[npc.target].position.X + 300, Main.player[npc.target].position.Y + 200);
                }
                if (fireAITimer == 180f)
                {
                    Teleport(Main.player[npc.target].position.X - 300, Main.player[npc.target].position.Y + 200);
                }
                float projSpeedX = fireAITimer < 180f ? -5f : 5f;
                if (fireAITimer >= 20)
                {
                    if (fireTimer <= 0f)
                    {
                        int type = mod.ProjectileType("InfernaceFire");
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 13);
                        Projectile.NewProjectile(infernaceCenter.X, infernaceCenter.Y, projSpeedX, -1, type, projectileBaseDamage, 0f, 0);
                        fireTimer = 10f + Main.rand.Next(0, 15);
                    }
                }
            }
            if (npc.ai[1] > 1060f && npc.ai[1] <= 1660)
            {
                if (npc.ai[1] == 1070f)
                {
                    Teleport(Main.player[npc.target].position.X, Main.player[npc.target].position.Y - 250);
                }
                // waves
                if (Main.netMode != 1)
                {
                    npc.velocity *= 0;
                    if (shootTimer2 == 0f)
                    {
                        Waves(P, 10f, projectileBaseDamage - 5, 4);
                        shootTimer2 = 50 + Main.rand.Next(0, 30);
                    }
                }
            }
            if (npc.ai[1] == 1660)
            {
                npc.ai[2] = 0;
            }
            if (npc.ai[1] > 1660)
            {
                float speed = 8f;
                float speedX = 0f;
                float speedY = 0f;

                npc.ai[2]++;
                if (npc.ai[2] == 1)
                {
                    Teleport(Main.player[npc.target].position.X + 500, Main.player[npc.target].position.Y + 500);
                }
                if (npc.ai[2] >= 1 && npc.ai[2] < 140)
                {
                    npc.velocity.X = -8f;
                    npc.velocity.Y = -8f;

                    speedX = speed;
                    speedY = -speed;
                }
                if (npc.ai[2] == 120)
                {
                    Teleport(Main.player[npc.target].position.X - 500, Main.player[npc.target].position.Y + 500);
                }
                if (npc.ai[2] >= 140 && npc.ai[2] < 240)
                {
                    npc.velocity.X = 8f;
                    npc.velocity.Y = -8f;

                    speedX = -speed;
                    speedY = -speed;
                }
            }

        }
        private void MoveDirect(Player P, float moveSpeed)
        {
            Vector2 toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
            toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
            toTarget.Normalize();
            npc.velocity = toTarget * moveSpeed;
        }

        private void Move(Player P, float speed, float playerX, float playerY)
        {
            int maxDist = 1500;
            if (Vector2.Distance(P.Center, npc.Center) >= maxDist)
            {
                float moveSpeed = 14f;
                Vector2 toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
                toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
                toTarget.Normalize();
                npc.velocity = toTarget * moveSpeed;
            }
            else
            {
                if (Main.expertMode)
                {
                    speed += 0.1f;
                }
                if (npc.velocity.X < playerX)
                {
                    npc.velocity.X = npc.velocity.X + speed * 2;
                }
                else if (npc.velocity.X > playerX)
                {
                    npc.velocity.X = npc.velocity.X - speed * 2;
                }
                if (npc.velocity.Y < playerY)
                {
                    npc.velocity.Y = npc.velocity.Y + speed;
                    if (npc.velocity.Y < 0f && playerY > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + speed;
                        return;
                    }
                }
                else if (npc.velocity.Y > playerY)
                {
                    npc.velocity.Y = npc.velocity.Y - speed;
                    if (npc.velocity.Y > 0f && playerY < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y - speed;
                        return;
                    }
                }
            }
        }

        private void Spike(Player P, float speed, int damage)
        {
            int type = mod.ProjectileType("InfernaceSpike");
            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
            float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), type, damage, 0f, 0);
        }

        private void Waves(Player P, float speed, int damage, int numberProj)
        {
            for (int k = 0; k < numberProj; k++)
            {
                Vector2 perturbedSpeed = new Vector2(speed, speed).RotatedByRandom(MathHelper.ToRadians(15));
                Vector2 vector8 = new Vector2(npc.Center.X - 46, npc.Center.Y - 69);
                float rotation = (float)Math.Atan2(vector8.Y - P.Center.Y, vector8.X - P.Center.X);
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * perturbedSpeed.X) * -1), (float)((Math.Sin(rotation) * perturbedSpeed.Y) * -1), mod.ProjectileType("InfernaceWave"), damage, 0f, Main.myPlayer);
            }
        }

        private void Teleport(float posX, float posY)
        {
            runTPAlphaChange = true;
            tpAlphaChangeTimer = 0;
            telePosX = posX;
            telePosY = posY;
        }
    }
}
