using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
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
        public bool enraged = false;
        public float[] spinAI = new float[2];
        const int projectileBaseDamage = 55;
        Vector2 staffCenter;
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(spinAI[0]);
                writer.Write(spinAI[1]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                spinAI[0] = reader.ReadSingle();
                spinAI[1] = reader.ReadSingle();
            }
        }
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
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                int choice = Main.rand.Next(6);
                switch (choice)
                {
                    case 0:
                        choice = mod.ItemType("BubblePopper");
                        break;
                    case 1:
                        choice = mod.ItemType("HighTide");
                        break;
                    case 2:
                        choice = mod.ItemType("OceansRazor");
                        break;
                    case 3:
                        choice = mod.ItemType("TheWave");
                        break;
                    case 4:
                        choice = mod.ItemType("Varee");
                        break;
                    case 5:
                        choice = mod.ItemType("BrinyBuster");
                        break;
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, choice);

                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AqueousTrophy"));
                }
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WaterEssence"), Main.rand.Next(5, 25));
            Main.windSpeed = 0.5f; // reset wind
            MyWorld.downedAqueous = true;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (npc.alpha > 100)
            {
                return false;
            }
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            npc.TargetClosest(true);

            enraged = false;
            staffCenter = new Vector2(npc.Center.X + 52 * -npc.direction, npc.Center.Y - 80);

            if (!P.ZoneBeach) enraged = true;

            if (!Main.player[npc.target].active || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (!Main.player[npc.target].active || Main.player[npc.target].dead)
                {
                    npc.localAI[0]++;
                    npc.velocity.Y = npc.velocity.Y + 0.11f;
                    if (npc.localAI[0] >= 300)
                    {
                        npc.active = false;
                    }
                }
                else
                    npc.localAI[0] = 0;
            }

            if (npc.life <= npc.lifeMax * 0.65f && npc.localAI[1] == 0)
            {
                Main.NewText("I understand why my king perished at your hands...", Color.Cyan.R, Color.Cyan.G, Color.Cyan.B);
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 123);
                npc.localAI[1]++;
                npc.ai[0] = 0f;
            }
            if (npc.life <= npc.lifeMax * 0.3f && npc.localAI[1] == 1)
            {
                Main.NewText("Enough! You destroyed my lord, I will not let you cast the same fate upon me!", Color.DarkRed.R, Color.DarkRed.G, Color.DarkRed.B);
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 119);
                npc.localAI[1]++;
                npc.ai[0] = 0f;
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                npc.ai[0] += 1f;
                npc.ai[1]--;
                npc.ai[2]--;
                npc.ai[3]--;
            }

            #region Ai 1
            if (npc.life > npc.lifeMax * 0.65f)
            {
                // no rain
                Main.rainTime = 0;
                Main.raining = false;
                Main.maxRaining = 0f;

                if (npc.ai[0] > 1500f)
                {
                    npc.ai[0] = 0f;
                }

                float movSpeed = 6f;
                if (Main.expertMode) movSpeed += 0.025f;
                if (MyWorld.awakenedMode) movSpeed += 0.05f;
                Move(P, 0.15f);

                //bolts that explode
                if (npc.ai[0] >= 1 && npc.ai[0] <= 1000)
                {
                    if (npc.ai[1] <= 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            AquaticBolts(P, 14f, projectileBaseDamage + 10);

                            npc.ai[1] = enraged ? 10 : 30;
                            npc.ai[1] += Main.rand.Next(1, 20);
                            npc.netUpdate = true;
                        }
                    }
                }

                //knives
                if (npc.ai[0] >= 1000 && npc.ai[0] <= 1500)
                {
                    npc.velocity = Vector2.Zero;
                    if (npc.ai[1] <= 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            WaterKnives(P, 7.5f, projectileBaseDamage, 2 + Main.rand.Next(1, 3));
                            npc.ai[1] = enraged ? 10 : 35;
                            npc.ai[1] += Main.rand.Next(10, 35);
                            npc.netUpdate = true;
                        }
                    }
                }
            }
            #endregion

            #region Ai 2
            if (npc.life <= npc.lifeMax * 0.65f && npc.life > npc.lifeMax * 0.3f)
            {
                // no rain
                Main.rainTime = 0;
                Main.raining = false;
                Main.maxRaining = 0f;

                float movSpeed = 6f;
                if (Main.expertMode) movSpeed += 0.05f;
                if (MyWorld.awakenedMode) movSpeed += 0.075f;
                Move(P, 0.2f);

                if (npc.ai[0] > 2500f)
                {
                    npc.ai[0] = 0f;
                }
                //homing knives
                if (npc.ai[0] <= 750)
                {
                    if (npc.ai[1] <= 30)
                    {
                        npc.velocity.X *= 0f;
                        npc.velocity.Y *= 0f;
                    }
                    if (npc.ai[1] <= 25)
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
                    if (npc.ai[1] <= 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            HomingKnives(P, 6f, projectileBaseDamage - 15);
                            npc.ai[1] = enraged ? 20 : 80;

                            npc.ai[1] += Main.rand.Next(10, 35);
                            npc.netUpdate = true;
                        }
                    }

                }

                //knives
                if (npc.ai[0] >= 1000 && npc.ai[0] <= 1500)
                {
                    npc.velocity *= 0.00f;
                    if (npc.ai[1] <= 0)
                    {
                        if (Collision.CanHit(npc.position, npc.width, npc.height, P.position, P.width, P.height))
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                WaterKnives(P, 7.5f, projectileBaseDamage, 3 + Main.rand.Next(1, 3));

                                npc.ai[1] = enraged ? 10 : 30;
                                npc.ai[1] += Main.rand.Next(10, 35);
                                npc.netUpdate = true;
                            }
                        }
                    }
                }
                //bolts that explode
                if (npc.ai[0] >= 1500 && npc.ai[0] <= 2000)
                {
                    if (npc.ai[1] <= 0)
                    {
                        AquaticBolts(P, 14f, projectileBaseDamage + 10);

                        npc.ai[1] = enraged ? 10 : 30;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            npc.ai[1] += Main.rand.Next(1, 20);
                            npc.netUpdate = true;
                        }
                    }
                }
                // shoot in circle
                if (npc.ai[0] >= 2000 && npc.ai[0] <= 2500)
                {
                    npc.velocity *= 0.00f;

                    SpinningAttack(P, 4f, projectileBaseDamage);
                }
                // aquanados
                if (npc.ai[2] <= 0)
                {
                    int damage = Main.expertMode ? projectileBaseDamage + 40 : projectileBaseDamage + 10;
                    Aquanados(damage);

                    npc.ai[2] = enraged ? 100 : (Main.expertMode ? 450 : 600);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        npc.ai[2] += Main.rand.Next(0, 200);
                        npc.netUpdate = true;
                    }
                }
            }
            #endregion

            #region Ai 3
            if (npc.life <= npc.lifeMax * 0.3f)
            {
                //rain
                Main.rainTime = 600;
                Main.raining = true;
                Main.maxRaining = 1f;
                Main.windSpeed = 2.2f;

                float movSpeed = 6f;
                if (Main.expertMode) movSpeed += 0.4f;
                if (MyWorld.awakenedMode) movSpeed += 0.6f;
                MoveDirectly(P, movSpeed);

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
                int alphaReduceRate = 15;
                if (npc.ai[3] <= 0)
                {
                    npc.alpha += alphaReduceRate;
                }
                else
                {
                    npc.alpha -= alphaReduceRate;
                }
                if (npc.alpha >= 255)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int distance = 400;
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        Vector2 offset = new Vector2((float)Math.Sin(angle) * distance, (float)Math.Cos(angle) * distance);

                        npc.Center = P.Center + offset;
                        npc.ai[3] = 100f + Main.rand.Next(0, 60);
                        npc.netUpdate = true;
                    }
                }
            }
            #endregion
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
                toTarget.Normalize();
                npc.velocity = toTarget * moveSpeed;
            }
            else
            {
                npc.spriteDirection = npc.direction;
                float playerX = P.Center.X - npc.Center.X;
                float playerY = P.Center.Y - 300f - npc.Center.Y;
                if (npc.velocity.X < playerX)
                {
                    npc.velocity.X = npc.velocity.X + speed * 2;
                    if (npc.velocity.X < 0f && playerX > 0f)
                    {
                        npc.velocity.X = npc.velocity.X + speed * 2;
                    }
                }
                else if (npc.velocity.X > playerX)
                {
                    npc.velocity.X = npc.velocity.X - speed * 2;
                    if (npc.velocity.X > 0f && playerX < 0f)
                    {
                        npc.velocity.X = npc.velocity.X - speed * 2;
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
            toTarget.Normalize();
            if (Vector2.Distance(P.Center, npc.Center) >= 30)
            {
                npc.velocity = toTarget * moveSpeed;
            }
        }

        private void AquaticBolts(Player P, float speed, int damage)
        {
            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 21);
            float rotation = (float)Math.Atan2(staffCenter.Y - P.Center.Y, staffCenter.X - P.Center.X);
            Projectile.NewProjectile(staffCenter.X, staffCenter.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), mod.ProjectileType("AquaticBolt"), damage, 0f, Main.myPlayer);
        }

        private void WaterKnives(Player P, float speed, int damage, int numberProjectiles)
        {
            float rotation = (float)Math.Atan2(staffCenter.Y - (P.position.Y + (P.height * 0.5f)), staffCenter.X - (P.position.X + (P.width * 0.5f)));
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1)).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(staffCenter.X, staffCenter.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("WaterKnife"), damage, 0f, Main.myPlayer, 0f, 0f);
            }
        }

        private void HomingKnives(Player P, float speed, int damage)
        {
            int type = mod.ProjectileType("WaterKnifeHoming");
            float spread = 45f * 0.0174f;
            double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - spread / 2;
            double deltaAngle = spread / 8f;
            double offsetAngle;
            for (int i = 0; i < 4; i++)
            {
                offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * speed), (float)(Math.Cos(offsetAngle) * speed), type, damage, 0f, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(-Math.Sin(offsetAngle) * speed), (float)(-Math.Cos(offsetAngle) * speed), type, damage, 0f, Main.myPlayer, 0f, 0f);
            }
        }

        private void SpinningAttack(Player P, float speed, int damage)
        {
            Vector2 offset = new Vector2(400, 0);
            spinAI[0] += enraged ? 0.25f : 0.15f;
            Vector2 shootTarget = npc.Center + offset.RotatedBy(spinAI[0] * (Math.PI * 2 / 8));

            int type = mod.ProjectileType("WaterKnife");
            spinAI[1]--;
            if (spinAI[1] <= 0)
            {
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 21);
                float rotation = (float)Math.Atan2(npc.Center.Y - shootTarget.Y, npc.Center.X - shootTarget.X);
                int num54 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), type, damage, 0f, Main.myPlayer);
                spinAI[1] = enraged ? 2 : 4;
            }
        }

        private void Aquanados(int damage)
        {
            int type = mod.ProjectileType("AquanadoBolt");
            float random = Main.rand.NextFloat(0f, 2f);
            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -6 + random, -2 + random, type, damage, 0f, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 6 + random, -2 + random, type, damage, 0f, Main.myPlayer, 0f, 0f);
        }
    }
}
