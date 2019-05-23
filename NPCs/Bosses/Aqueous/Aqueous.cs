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

namespace ElementsAwoken.NPCs.Bosses.Aqueous
{
    [AutoloadBossHead]
    public class Aqueous : ModNPC
    {
        public bool ai1 = true;
        public bool ai2 = false;
        public bool ai3 = false;
        public bool enraged = false;
        public int halfLife = 0;
        public int lowLife = 0;
        public float tpCooldown = 150f;
        public float spinAI = 0;
        public int spinAttack = 0;

        public float shootTimer1 = 0;
        public float shootTimer2 = 0;
        public float shootTimer3 = 0;
        public float shootTimer4 = 0;

        public int projectileBaseDamage = 55; // to make all projectiles harder or easier
        int contactDamage = 75;
        public override void SetDefaults()
        {
            npc.width = 132;
            npc.height = 184;

            npc.lifeMax = 75000;
            npc.damage = 75;
            npc.defense = 52;
            npc.knockBackResist = 0f;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;

            npc.HitSound = SoundID.NPCHit55;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(0, 20, 0, 0);
            music = MusicID.Boss4;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AqueousTheme");
            bossBag = mod.ItemType("AqueousBag");

            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aqueous");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 90;
            npc.lifeMax = 100000;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 125000;
                npc.damage = 110;
                npc.defense = 65;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            if (npc.frameCounter > 6)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y > frameHeight * 4)  // so it doesnt go over
            {
                npc.frame.Y = 0;
            }
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AqueousTrophy"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                int choice = Main.rand.Next(6);
                if (choice == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BrinyBuster"));
                }
                if (choice == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BubblePopper"));
                }
                if (choice == 2)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HighTide"));
                }
                if (choice == 3)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("OceansRazor"));
                }
                if (choice == 4)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TheWave"));
                }
                if (choice == 4)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Varee"));
                }
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WaterEssence"), Main.rand.Next(5, 25));
            Main.windSpeed = 0.5f; // reset wind
            MyWorld.downedAqueous = true;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            npc.netUpdate = true;
            npc.TargetClosest(true);
            if (!P.ZoneBeach)
            {
                enraged = true;
            }
            if (P.ZoneBeach)
            {
                enraged = false;
            }
            if (!Main.player[npc.target].active || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (!Main.player[npc.target].active || Main.player[npc.target].dead)
                {
                    npc.ai[0]++;
                    npc.velocity.Y = npc.velocity.Y + 0.11f;
                    if (npc.ai[0] >= 300)
                    {
                        npc.active = false;
                    }
                }
                else
                    npc.ai[0] = 0;
            }
            if (npc.localAI[1] == 0)
            {
                contactDamage = npc.damage;
                npc.localAI[1]++;
            }
            if (npc.alpha > 100)
            {
                npc.damage = 0;
            }
            else
            {
                npc.damage = contactDamage;
            }
            Lighting.AddLight(npc.Center, 0.1f, 0.5f, 0.6f);
            if ((npc.life <= npc.lifeMax * 0.65f))
            {
                ai1 = false;
                ai2 = true;
                ai3 = false;
                if (halfLife == 0)
                {
                    Main.NewText("You are strong! I understand why my king perished at your hands.", Color.Cyan.R, Color.Cyan.G, Color.Cyan.B);
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 123);
                    halfLife++;
                    npc.ai[1] = 0f;
                }
            }
            if ((npc.life <= npc.lifeMax * 0.3f))
            {
                ai1 = false;
                ai2 = false;
                ai3 = true;
                if (lowLife == 0)
                {
                    Main.NewText("Enough! You destroyed my king, I will not let myself have the same fate!", Color.DarkRed.R, Color.DarkRed.G, Color.DarkRed.B);
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 119);
                    lowLife++;
                    npc.ai[1] = 0f;
                }
            }
            npc.ai[1] += 1f;
            shootTimer1--;
            shootTimer2--;
            shootTimer3--;
            shootTimer4--;
            if (Main.netMode != 1) // this could break it in multiplayer, only meant to be used in projectile shooting
            {
                #region Ai 1
                if (ai1)
                {
                    // no rain
                    Main.rainTime = 0;
                    Main.raining = false;
                    Main.maxRaining = 0f;

                    if (npc.ai[1] > 1500f)
                    {
                        npc.ai[1] = 0f;
                    }
                    //movement
                    Move(P, 0.15f);

                    //bolts that explode
                    if (npc.ai[1] >= 1 && npc.ai[1] <= 1000)
                    {
                        if (shootTimer1 <= 0)
                        {
                            AquaticBolts(P, 14f, projectileBaseDamage + 10);

                            shootTimer1 = enraged ? 10 : 30;
                            shootTimer1 += Main.rand.Next(1, 20);
                        }
                    }

                    //knives
                    if (npc.ai[1] >= 1000 && npc.ai[1] <= 1500)
                    {
                        npc.velocity *= 0.00f;
                        if (shootTimer2 <= 0)
                        {
                            WaterKnives(P, 7.5f, projectileBaseDamage, 2 + Main.rand.Next(1, 3));
                            shootTimer2 = enraged ? 10 : 35;
                            shootTimer2 += Main.rand.Next(10, 35);
                        }
                    }
                }
                #endregion

                #region Ai 2
                if (ai2)
                {
                    // no rain
                    Main.rainTime = 0;
                    Main.raining = false;
                    Main.maxRaining = 0f;

                    //movement
                    Move(P, 0.2f);

                    if (npc.ai[1] > 2500f)
                    {
                        npc.ai[1] = 0f;
                    }
                    //homing knives
                    if (npc.ai[1] <= 750)
                    {
                        if (shootTimer3 <= 30)
                        {
                            npc.velocity.X *= 0f;
                            npc.velocity.Y *= 0f;
                        }
                        if (shootTimer3 <= 25)
                        {
                            int maxdusts = 20;
                            for (int i = 0; i < maxdusts; i++)
                            {
                                float dustDistance = 100;
                                float dustSpeed = 6;
                                Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                                Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                                Dust vortex = Dust.NewDustPerfect(npc.Center + offset, 111, velocity, 0, default(Color), 1.5f);
                                vortex.noGravity = true;
                            }
                        }
                        if (shootTimer3 <= 0)
                        {
                            HomingKnives(P, 6f, projectileBaseDamage - 15);
                            shootTimer3 = enraged ? 20 : 80;
                            shootTimer3 += Main.rand.Next(10, 35);
                        }

                    }

                    //knives
                    if (npc.ai[1] >= 1000 && npc.ai[1] <= 1500)
                    {
                        npc.velocity *= 0.00f;
                        if (shootTimer2 <= 0)
                        {
                            if (Collision.CanHit(npc.position, npc.width, npc.height, P.position, P.width, P.height))
                            {
                                WaterKnives(P, 7.5f, projectileBaseDamage, 3 + Main.rand.Next(1, 3));

                                shootTimer2 = enraged ? 10 : 30;
                                shootTimer2 += Main.rand.Next(10, 35);
                            }
                        }
                    }
                    //bolts that explode
                    if (npc.ai[1] >= 1500 && npc.ai[1] <= 2000)
                    {
                        if (shootTimer1 <= 0)
                        {
                            AquaticBolts(P, 14f, projectileBaseDamage + 10);

                            shootTimer1 = enraged ? 10 : 30;
                            shootTimer1 += Main.rand.Next(1, 20);
                        }
                    }
                    // shoot in circle
                    if (npc.ai[1] >= 2000 && npc.ai[1] <= 2500)
                    {
                        npc.velocity *= 0.00f;

                        SpinningAttack(P, 4f, projectileBaseDamage);
                    }
                    // aquanados
                    if (shootTimer4 <= 0)
                    {
                        int damage = Main.expertMode ? projectileBaseDamage + 40 : projectileBaseDamage + 10;
                        Aquanados(damage);

                        shootTimer4 = enraged ? 100 : (Main.expertMode ? 450 : 600);
                        shootTimer4 += Main.rand.Next(0, 200);
                    }
                }
                #endregion

                #region Ai 3
                if (ai3)
                {
                    //rain
                    Main.rainTime = 600;
                    Main.raining = true;
                    Main.maxRaining = 1f;
                    Main.windSpeed = 2.2f;

                    MoveDirectly(P, 6f);

                    //minions
                    if (!NPC.AnyNPCs(mod.NPCType("AqueousMinion1")))
                    {
                        int minionCount = 3;
                        for (int l = 0; l < minionCount; l++)
                        {
                            //cos = y, sin = x
                            int distance = minionCount * 120;
                            NPC orbital = Main.npc[NPC.NewNPC((int)(P.Center.X + (Math.Sin(l * distance) * 150)), (int)(P.Center.Y + (Math.Cos(l * distance) * 150)), mod.NPCType("AqueousMinion1"), npc.whoAmI, 0, 0, 0, -1)];
                            NPC orbital2 = Main.npc[NPC.NewNPC((int)(P.Center.X + (Math.Sin(l * distance) * 150)), (int)(P.Center.Y + (Math.Cos(l * distance) * 150)), mod.NPCType("AqueousMinion2"), npc.whoAmI, 0, 0, 0, -1)];
                            // where the orbitals is positioned
                            orbital.ai[0] = l * 90;
                            orbital2.ai[0] = l * 90;
                        }
                    }
                    //teleport
                    if (tpCooldown > 0f)
                    {
                        tpCooldown -= 1f;
                    }

                    if (tpCooldown <= 20)
                    {
                        npc.alpha += 15;
                    }
                    if (tpCooldown > 100)
                    {
                        npc.alpha -= 15;
                    }
                    if (npc.alpha < 0)
                    {
                        npc.alpha = 0;
                    }
                    if (npc.alpha > 255)
                    {
                        npc.alpha = 255;
                    }
                    if (tpCooldown <= 0f)
                    {
                        int distance = 400;
                        int choice = Main.rand.Next(4);
                        if (choice == 0)
                        {
                            npc.position.X = Main.player[npc.target].position.X + distance;
                            npc.position.Y = Main.player[npc.target].position.Y + -distance;
                            tpCooldown = 120f + Main.rand.Next(0, 60);
                        }
                        if (choice == 1)
                        {
                            npc.position.X = Main.player[npc.target].position.X + -distance;
                            npc.position.Y = Main.player[npc.target].position.Y + -distance;
                            tpCooldown = 120f + Main.rand.Next(0, 60);
                        }
                        if (choice == 2)
                        {
                            npc.position.X = Main.player[npc.target].position.X + distance;
                            npc.position.Y = Main.player[npc.target].position.Y + distance;
                            tpCooldown = 120f + Main.rand.Next(0, 60);
                        }
                        if (choice == 3)
                        {
                            npc.position.X = Main.player[npc.target].position.X + -distance;
                            npc.position.Y = Main.player[npc.target].position.Y + distance;
                            tpCooldown = 120f + Main.rand.Next(0, 60);
                        }
                    }
                }
                #endregion
            }
        }

        public override bool CheckActive()
        {
            return false;
        }

        private void Move(Player P, float speed)
        {
            int maxDist = 1000;
            if (Vector2.Distance(P.Center, npc.Center) >= maxDist)
            {
                float moveSpeed = 8f;
                Vector2 toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
                toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
                toTarget.Normalize();
                npc.velocity = toTarget * moveSpeed;
            }
            else
            {
                npc.spriteDirection = npc.direction;

                if (Main.expertMode)
                {
                    speed += 0.05f;
                }
                Vector2 vector75 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float playerX = P.position.X + (float)(P.width / 2) - vector75.X;
                float playerY = P.position.Y + (float)(P.height / 2) - 300f + Main.rand.Next(-100, 100) - vector75.Y;
                if (npc.velocity.X < playerX)
                {
                    npc.velocity.X = npc.velocity.X + speed;
                    // if (npc.velocity.X < 0f && playerY > 0f)
                    {
                        npc.velocity.X = npc.velocity.X + speed;
                    }
                }
                else if (npc.velocity.X > playerX)
                {
                    npc.velocity.X = npc.velocity.X - speed;
                    //if (npc.velocity.X > 0f && playerX < 0f) // this breaks it for some reason :(
                    {
                        npc.velocity.X = npc.velocity.X - speed;
                    }
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

        private void MoveDirectly(Player P, float moveSpeed)
        {
            Vector2 toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
            toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
            toTarget.Normalize();
            if (Vector2.Distance(P.Center, npc.Center) >= 30)
            {
                npc.velocity = toTarget * moveSpeed;
            }
        }

        private void AquaticBolts(Player P, float speed, int damage)
        {
            Vector2 vector = new Vector2(npc.position.X + (npc.width / 2) + 52, npc.position.Y + (npc.height / 2) + 80);
            if (npc.direction == 1)
            {
                vector = new Vector2(npc.position.X + (npc.width / 2) - 52, npc.position.Y + (npc.height / 2) - 80);
            }
            if (npc.direction == -1)
            {
                vector = new Vector2(npc.position.X + (npc.width / 2) + 52, npc.position.Y + (npc.height / 2) - 80);
            }
            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 21);
            float rotation = (float)Math.Atan2(vector.Y - (P.position.Y + (P.height * 0.5f)), vector.X - (P.position.X + (P.width * 0.5f)));
            Projectile.NewProjectile(vector.X, vector.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), mod.ProjectileType("AquaticBolt"), damage, 0f, 0);
        }

        private void WaterKnives(Player P, float speed, int damage, int numberProjectiles)
        {
            Vector2 vector = new Vector2(npc.position.X + (npc.width / 2) + 52, npc.position.Y + (npc.height / 2) + 80);
            if (npc.direction == 1)
            {
                vector = new Vector2(npc.position.X + (npc.width / 2) - 52, npc.position.Y + (npc.height / 2) - 80);
            }
            if (npc.direction == -1)
            {
                vector = new Vector2(npc.position.X + (npc.width / 2) + 52, npc.position.Y + (npc.height / 2) - 80);
            }
            float rotation = (float)Math.Atan2(vector.Y - (P.position.Y + (P.height * 0.5f)), vector.X - (P.position.X + (P.width * 0.5f)));
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1)).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(vector.X, vector.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("WaterKnife"), damage, 0f, Main.myPlayer, 0f, 0f);
            }
        }

        private void HomingKnives(Player P, float speed, int damage)
        {
            int type = mod.ProjectileType("WaterKnifeHoming");
            Vector2 vector = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
            float spread = 45f * 0.0174f;
            double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - spread / 2;
            double deltaAngle = spread / 8f;
            double offsetAngle;
            for (int i = 0; i < 4; i++)
            {
                offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Sin(offsetAngle) * speed), (float)(Math.Cos(offsetAngle) * speed), type, damage, 0f, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(vector.X, vector.Y, (float)(-Math.Sin(offsetAngle) * speed), (float)(-Math.Cos(offsetAngle) * speed), type, damage, 0f, Main.myPlayer, 0f, 0f);
            }
        }

        private void SpinningAttack(Player P, float speed, int damage)
        {
            Vector2 offset = new Vector2(400, 0);
            spinAI += enraged ? 0.25f : 0.15f;
            Vector2 shootTarget = npc.Center + offset.RotatedBy(spinAI * (Math.PI * 2 / 8));

            int type = mod.ProjectileType("WaterKnife");
            Vector2 vector = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
            spinAttack--;
            if (spinAttack <= 0)
            {
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 21);
                float rotation = (float)Math.Atan2(vector.Y - shootTarget.Y, vector.X - shootTarget.X);
                int num54 = Projectile.NewProjectile(vector.X, vector.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), type, damage, 0f, 0);
                spinAttack = enraged ? 2 : 4;
            }
        }

        private void Aquanados(int damage)
        {
            Vector2 vector = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
            int type = mod.ProjectileType("AquanadoBolt");
            int random = Main.rand.Next(0, 2);
            Projectile.NewProjectile(vector.X, vector.Y, -6 + random, -2 + random, type, damage, 0f, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(vector.X, vector.Y, 6 + random, -2 + random, type, damage, 0f, Main.myPlayer, 0f, 0f);
        }
    }
}
