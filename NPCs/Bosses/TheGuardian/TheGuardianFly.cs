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

namespace ElementsAwoken.NPCs.Bosses.TheGuardian
{
    [AutoloadBossHead]
    public class TheGuardianFly : ModNPC
    {
        public int orbCooldown = 75;
        int projectileBaseDamage = 65;
        public override void SetDefaults()
        {
            npc.width = 252;
            npc.height = 152;

            npc.lifeMax = 115000;
            npc.damage = 120;
            npc.defense = 45;
            npc.knockBackResist = 0f;

            npc.scale = 1.2f;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;

            npc.HitSound = SoundID.NPCHit2;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 55, 0, 0);

            music = MusicID.Boss4;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GuardianTheme");

            for (int num2 = 0; num2 < 206; num2++)
            {
                npc.buffImmune[num2] = true;
            }
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;

            bossBag = mod.ItemType("GuardianBag");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Guardian");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 200000;
            npc.damage = 150;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 250000;
                npc.damage = 200;
                npc.defense = 60;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            if (npc.frameCounter > 5)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y > frameHeight * 5)
            {
                npc.frame.Y = 0;
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TheGuardianTrophy"));
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TheGuardianMask"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                int choice = Main.rand.Next(3);
                if (choice == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Godslayer"));
                }
                if (choice == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("InfernoStorm"));
                }
                if (choice == 2)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TemplesWrath"));
                }
            }
            MyWorld.downedGuardian = true;
        }

        public override void AI()
        { 
            Player P = Main.player[npc.target];
            #region despawning
            if (!Main.player[npc.target].active || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (!Main.player[npc.target].active || Main.player[npc.target].dead)
                {
                    npc.localAI[0]++;
                }
            }
            if (Main.dayTime)
            {
                npc.localAI[0]++;
            }
            if (npc.localAI[0] >= 300)
            {
                npc.active = false;
            }
            #endregion
            npc.ai[0]--; // shoot timer
            npc.ai[1]--; // minion timer
            npc.ai[2]++; // ai timer
            npc.ai[3]--; // portals & infernoballs
            orbCooldown--;
            if (orbCooldown <= 0)
            {
                orbCooldown = 75;
            }
            if (npc.life >= npc.lifeMax * 0.5f)
            {
                if (npc.ai[2] > 1700f)
                {
                    npc.ai[2] = 0f;
                }
            }
            if (npc.life <= npc.lifeMax * 0.5f)
            {
                if (npc.ai[2] > 2500f)
                {
                    npc.ai[2] = 0f;
                }
            }
            // minions
            if (npc.ai[1] <= 0)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("GuardianProbe"));
                npc.ai[1] = Main.rand.Next(300, 1200);
            }

            //attack 1- flys left and right of the player and leaves shooting orbs 
            if (npc.ai[2] <= 500)
            {
                if (npc.ai[2] <= 250)
                {
                    Move(P, 0.2f, P.Center.X - npc.Center.X + 400, P.Center.Y - npc.Center.Y - 300);
                }
                if (npc.ai[2] >= 250)
                {
                    Move(P, 0.2f, P.Center.X - npc.Center.X - 400, P.Center.Y - npc.Center.Y - 300);
                }
                if (Main.netMode != 1 && npc.ai[0] <= 0 && orbCooldown <= 25)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("GuardianOrb"), projectileBaseDamage, 0f, 0);
                    npc.ai[0] = 5;
                }
            }

            //attack 2- flys above the player and drops fire/lasers
            if (npc.ai[2] >= 500 && npc.ai[2] <= 800)
            {
                Move(P, 0.25f, P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y - 400);
                if (npc.life >= npc.lifeMax * 0.5f)
                {
                    if (npc.ai[0] <= 0)
                    {
                        //Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 1);
                        int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(-2, 2), 8f, mod.ProjectileType("GuardianFire"), projectileBaseDamage, 0f, 0);
                        Projectile fire = Main.projectile[proj];
                        fire.timeLeft = 120;
                        npc.ai[0] = 4;
                    }
                }
                if (npc.life <= npc.lifeMax * 0.5f)
                {
                    if (npc.ai[0] <= 30)
                    {
                        int maxdusts = 10;
                        for (int i = 0; i < maxdusts; i++)
                        {
                            float dustDistance = 100;
                            float dustSpeed = 15;
                            Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                            Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                            Dust vortex = Dust.NewDustPerfect(new Vector2(npc.Center.X, npc.Center.Y + 40) + offset, 6, velocity, 0, default(Color), 1.5f);
                            vortex.noGravity = true;
                        }
                    }
                    if (npc.ai[0] <= 0)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 122);
                        for (int i = 0; i < 6; i++)
                        {
                            int proj = Projectile.NewProjectile(npc.position.X + 140 + 6.6f * i, npc.Bottom.Y, 0f, 9f, mod.ProjectileType("GuardianBeam"), projectileBaseDamage, 0f, 0);
                            Projectile Beam = Main.projectile[proj];
                            Beam.timeLeft = 75;
                        }
                        npc.ai[0] = 50;
                    }

                }
            }

            // so the guardian wont instantly shoot the player in the face 
            if (npc.ai[2] == 800)
            {
                npc.ai[0] = 100;
            }

            //attack 3 - throws multiple sticky grenades at the player
            if (npc.ai[2] >= 800 && npc.ai[2] <= 1200)
            {
                Move(P, 0.2f, P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y - 300);

                if (npc.ai[0] <= 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                    Bolts(P, 18f, projectileBaseDamage - 20, Main.rand.Next(4, 6), 13);
                    npc.ai[0] = Main.rand.Next(30, 80);
                }
            }

            //attack 4 - fireball cluster
            if (npc.ai[2] >= 1200 && npc.ai[2] <= 1700)
            {
                npc.velocity.X = 0f;
                npc.velocity.Y = 0f;
                if (npc.ai[0] <= 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    for (int i = 0; i < 6; i++)
                    {
                        float speed = 16 + Main.rand.NextFloat(-2, 2);
                        Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1)).RotatedByRandom(MathHelper.ToRadians(5));
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("GuardianFireball"), projectileBaseDamage - 10, 0f, 0);
                    }
                    npc.ai[0] = 50;
                }
            }

            //attack 5 - shoots a fast exploding bolt at the player 
            if (npc.life <= npc.lifeMax * 0.5f && npc.ai[2] >= 1700f && npc.ai[2] <= 2000f)
            {
                Move(P, 0.2f, P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y - 300);
                if (npc.ai[0] <= 0)
                {
                    float Speed = 20f;
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("GuardianStrike"), projectileBaseDamage + 20, 0f, 0, 0, 1);
                    npc.ai[0] = 100;
                }
            }

            //attack 6 - releases shots in a circle
            if (npc.life <= npc.lifeMax * 0.5f && npc.ai[2] >= 2000f && npc.ai[2] <= 2500f)
            {
                npc.velocity.X = 0f;
                npc.velocity.Y = 0f;
                if (npc.ai[0] <= 0)
                {
                    Vector2 perturbedSpeed = new Vector2(4f, 4f).RotatedByRandom(MathHelper.ToRadians(360));
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("GuardianShot"), projectileBaseDamage, 0f, 0);
                    npc.ai[0] = 5;
                }
            }

            // portals and infernoballs
            if (npc.life <= npc.lifeMax * 0.5f)
            {
                // infernoballs
                if (npc.ai[3] == 200)
                {
                    int direction = 1;
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            direction = 1;
                            break;
                        case 1:
                            direction = -1;
                            break;
                        default: break;
                    }
                    float speed = 12f;
                    int damage = projectileBaseDamage - 20;
                    for (int i = 0; i < 3; i++)
                    {
                        Projectile.NewProjectile(P.Center.X - 1000 * direction + i * 50, P.Center.Y - i * 200, speed * direction, 0f, mod.ProjectileType("GuardianInfernoball"), damage, 0f);
                        Projectile.NewProjectile(P.Center.X - 1000 * direction + i * 50, P.Center.Y + i * 200, speed * direction, 0f, mod.ProjectileType("GuardianInfernoball"), damage, 0f);
                    }
                    Projectile.NewProjectile(P.Center.X - 1000 * direction, P.Center.Y, speed * direction, 0f, mod.ProjectileType("GuardianInfernoball"), damage, 0f);
                }
                // portal
                if (npc.ai[3] <= 0)
                {
                    int minDist = 400;
                    int vectorX = Main.rand.Next(-800, 800);
                    int vectorY = Main.rand.Next(-800, 800);
                    if (vectorX < minDist && vectorX > 0)
                    {
                        vectorX = minDist;
                    }
                    else if (vectorX > -minDist && vectorX < 0)
                    {
                        vectorX = -minDist;
                    }
                    if (vectorY < minDist && vectorY > 0)
                    {
                        vectorY = minDist;
                    }
                    else if (vectorY > -minDist && vectorX < 0)
                    {
                        vectorY = -minDist;
                    }
                    Projectile.NewProjectile(P.Center.X + vectorX, P.Center.Y + vectorY, 0f, 0f, mod.ProjectileType("GuardianPortal"), projectileBaseDamage * 2, 0f);
                    npc.ai[3] = 400;
                }
            }

            // strikes coming up from underneath
            if (npc.life <= npc.lifeMax * 0.2f)
            {
                if (Main.rand.Next(14) == 0)
                {
                    float posX = P.Center.X + Main.rand.Next(5000) - 3000;
                    float posY = P.Center.Y + 1000;
                    Projectile.NewProjectile(posX, posY, 0f, -10f, mod.ProjectileType("GuardianStrike"), projectileBaseDamage, 0f);
                }
            }

            // create circle
            if (npc.life <= npc.lifeMax * 0.2f && npc.localAI[1] == 0f)
            {
                npc.localAI[1]++;
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("GuardianCircle"), npc.damage, 0f, 0, 0, npc.whoAmI);
            }
        }

        private void Move(Player P, float speed, float playerX, float playerY)
        {
            int maxDist = 1000;
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

        private void Bolts(Player P, float speed, int damage, int numberProj, int angle)
        {
            float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
            for (int i = 0; i < numberProj; i++)
            {
                Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1)).RotatedByRandom(MathHelper.ToRadians(angle));
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("GuardianStickyBolt"), damage, 0f, 0);
            }
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}
