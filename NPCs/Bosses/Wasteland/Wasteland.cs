using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Events;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.NPCProj.Wasteland;
using ElementsAwoken.Items.Essence;
using ElementsAwoken.Projectiles.Other;

namespace ElementsAwoken.NPCs.Bosses.Wasteland
{
    [AutoloadBossHead]
    public class Wasteland : ModNPC
    {
        private int shootTimer = 100;
        private int eggTimer = 75;
        private int jumpUpTimer = 200;
        private int[] superJumpAI = new int[2];

        private int stormTimer = 400;

        private int spoutSpawnTimer = 1000;

        private bool underground = false;
        private int diggingType = 0; // 0 is none, 1 is up, 2 is down
        private int diggingTimer = 60;

        private float digSpeed = 3f;

        private int aiTimer = 0;
        private int acidBallTimer = 200;
        private int jumpSpikeTimer = 0;

        private int projectileBaseDamage = 25;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(shootTimer);
            writer.Write(eggTimer);
            writer.Write(jumpUpTimer);
            writer.Write(superJumpAI[0]);
            writer.Write(superJumpAI[1]);
            writer.Write(stormTimer);
            writer.Write(diggingTimer);
            writer.Write(diggingType);
            writer.Write(acidBallTimer);
            writer.Write(jumpSpikeTimer);

            writer.Write(underground);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            shootTimer = reader.ReadInt32();
            eggTimer = reader.ReadInt32();
            jumpUpTimer = reader.ReadInt32();
            superJumpAI[0] = reader.ReadInt32();
            superJumpAI[1] = reader.ReadInt32();
            stormTimer = reader.ReadInt32();
            diggingTimer = reader.ReadInt32();
            diggingType = reader.ReadInt32();
            acidBallTimer = reader.ReadInt32();
            jumpSpikeTimer = reader.ReadInt32();

            underground = reader.ReadBoolean();
        }

        private float despawnTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        // other AI's is used in fighter AI

        public override void BossHeadSlot(ref int index)
        {
            if (underground || diggingType == 2)
            {
                index = NPCHeadLoader.GetBossHeadSlot("ElementsAwoken/NPCs/Bosses/Wasteland/Wasteland_Head_Boss_Blank");
            }
            else
            {
                index = NPCHeadLoader.GetBossHeadSlot("ElementsAwoken/NPCs/Bosses/Wasteland/Wasteland_Head_Boss");
            }
        }
        public override void SetDefaults()
        {
            npc.width = 140;
            npc.height = 130;

            npc.aiStyle = -1;

            npc.damage = 0;
            npc.defense = 15;
            npc.lifeMax = 4300;          
            npc.knockBackResist = 0f;

            npc.value = Item.buyPrice(0, 5, 0, 0);

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath36;

            npc.boss = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;

            NPCID.Sets.NeedsExpertScaling[npc.type] = true;

            music = MusicID.Boss1;
            bossBag = mod.ItemType("WastelandBag");

            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wasteland");
            Main.npcFrameCount[npc.type] = 6;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 5000;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 7500;
                npc.defense = 20;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.velocity.Y == 0f)
            {
                if (npc.direction == 1)
                {
                    npc.spriteDirection = 1;
                }
                if (npc.direction == -1)
                {
                    npc.spriteDirection = -1;
                }
            }
            if (npc.velocity.Y != 0f || (npc.direction == -1 && npc.velocity.X > 0f) || (npc.direction == 1 && npc.velocity.X < 0f))
            {
                npc.frameCounter = 0.0;
                npc.frame.Y = npc.frame.Height * 4;
                return;
            }
            if (npc.velocity.X == 0f)
            {
                npc.frameCounter = 0.0;
                npc.frame.Y = 0;
                return;
            }
            npc.frameCounter += (double)Math.Abs(npc.velocity.X);
            int frameLength = 12;
            if (npc.frameCounter < frameLength)
            {
                Vector2 snapPos = new Vector2(npc.Center.X - 52, npc.Center.Y + 24);
                Vector2 snapPos2 = new Vector2(npc.Center.X - 14, npc.Center.Y + 32);
                if (npc.direction == 1)
                {
                    snapPos.X = npc.Center.X + 52;
                    snapPos2.X = npc.Center.X + 14;
                }
                Projectile.NewProjectile(snapPos.X, snapPos.Y, 0f, 0f, ProjectileType<WastelandSnap>(), 40, 0, Main.myPlayer, npc.whoAmI);
                Projectile.NewProjectile(snapPos2.X, snapPos2.Y, 0f, 0f, ProjectileType<WastelandSnap>(), 40, 0, Main.myPlayer, npc.whoAmI);

                npc.frame.Y = 0;
                return;
            }
            if (npc.frameCounter < frameLength * 2)
            {
                npc.frame.Y = npc.frame.Height;
                return;
            }
            if (npc.frameCounter < frameLength * 3)
            {
                npc.frame.Y = npc.frame.Height * 2;
                return;
            }
            if (npc.frameCounter < frameLength * 4)
            {
                npc.frame.Y = npc.frame.Height * 3;
                return;
            }

            npc.frameCounter = 0.0;
            if (aiTimer < 0) npc.frame.Y = 0;
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 32, hitDirection, -1f, 0, default(Color), 1f);
            }
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 32, hitDirection, -1f, 0, default(Color), 1f);
                }
                for (int i = 0; i < 3; i++)
                {
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Wasteland" + i), npc.scale);
                }
                if (Main.netMode != NetmodeID.MultiplayerClient) Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileType<AbilityGiver>(), 0, 0, Main.myPlayer);
            }
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WastelandTrophy"));
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WastelandMask"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                int choice = Main.rand.Next(4);
                if (choice == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Pincer"));
                }
                if (choice == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ScorpionBlade"));
                }
                if (choice == 2)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Stinger"));
                }
                if (choice == 3)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ChitinStaff"));
                }
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<DesertEssence>(), Main.rand.Next(5,20));
            MyWorld.downedWasteland = true;

                    // stop the sandstorm
                    Sandstorm.Happening = false;
            Sandstorm.TimeLeft = 0;
            Sandstorm.IntendedSeverity = 0;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.LesserHealingPotion;
        }

        public override int SpawnNPC(int tileX, int tileY)
        {
            npc.TargetClosest(true);
            Player P = Main.player[npc.target];
            npc.Center = P.Center + new Vector2(0, 700);

            Sandstorm.Happening = true;
            Sandstorm.TimeLeft = 90000;
            SandstormStuff();
            return base.SpawnNPC(tileX, tileY);
        }
        public override bool PreNPCLoot()
        {
            if (aiTimer < 0)
            {
                NPCLoot();
                Main.NewText("Wasteland sinks back into the sand",Color.MediumPurple);
                NPC death = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<WastelandDeath>())];
                death.spriteDirection = npc.spriteDirection;
                death.Center = npc.Center;
                return false;
            }
            return base.PreNPCLoot();
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            npc.TargetClosest(true);
            if (aiTimer < 0)
            {
                npc.immortal = true;
                npc.dontTakeDamage = true;
                int dryad = NPC.FindFirstNPC(NPCID.Dryad);
                if (dryad >= 0)
                {
                    NPC dryadNPC = Main.npc[dryad];
                    dryadNPC.ai[0] = 0;
                   // dryadNPC.localAI[3] = 0;
                }
                aiTimer++;
                if (npc.velocity.Y < 0) npc.velocity.Y *= 0.9f;
                npc.velocity.X = 0;
                if (aiTimer > -5)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 0);
                    npc.checkDead();
                }
            }
            else
            {
                if (npc.life <= npc.lifeMax * 0.1f && aiTimer >= 0)
                {
                    int dryad = NPC.FindFirstNPC(NPCID.Dryad);
                    if (dryad >= 0)
                    {
                        NPC dryadNPC = Main.npc[dryad];
                        if (Vector2.Distance(dryadNPC.Center, npc.Center) < 2000)
                        {
                            dryadNPC.alpha = 255;
                            aiTimer = -720;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(dryadNPC.Center, Vector2.Zero, ProjectileType<WastelandDryad>(), 0, 0, Main.myPlayer);
                            }
                        }
                    }
                }
                if (!P.active || P.dead)
                {
                    npc.TargetClosest(true);
                    if (!P.active || P.dead)
                    {
                        despawnTimer++;
                        if (despawnTimer >= 300) npc.active = false;
                    }
                    else
                        despawnTimer = 0;
                }
                bool enraged = !P.ZoneDesert;
                if (enraged) npc.defense = 30;
                else npc.defense = 18;

                if (!underground && diggingType == 0) spoutSpawnTimer--;

                // make it so wasteland cant be hit when hes underground
                if (underground)
                {
                    npc.immortal = true;
                    npc.dontTakeDamage = true;
                    npc.alpha = 255;
                }
                else
                {
                    npc.immortal = false;
                    npc.dontTakeDamage = false;
                    npc.alpha = 0;
                }


                // create the wasteland spouts
                if (spoutSpawnTimer <= 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int numFakes = Main.expertMode ? MyWorld.awakenedMode ? 4 : 3 : 2;
                        for (int k = 0; k < numFakes; k++)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2(Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-7, 7)), ProjectileType<WastelandDiggingProj>(), 0, 0, Main.myPlayer);
                        }
                        Projectile.NewProjectile(npc.Center, new Vector2(Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-7, 7)), ProjectileType<WastelandDiggingProjReal>(), 0, 0, Main.myPlayer, npc.whoAmI);
                    }
                    spoutSpawnTimer = 10000; // temporary so it doesnt constantly shoot- proper time set after the projectile is killed below

                    diggingType = 1; // 1 is down
                    digSpeed = 4.5f;
                    diggingTimer = 120;

                    npc.noTileCollide = true;
                }
                DetectSpouts();
                // digging up and down
                if (diggingType != 0)
                {
                    Dig(digSpeed);
                }
                // if too far away
                if (Vector2.Distance(P.Center, npc.Center) >= 1200)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        npc.Center = new Vector2(P.Center.X, P.Center.Y + 700);
                        npc.netUpdate = true;
                    }
                    diggingType = 2;
                    digSpeed = 6f;
                }

                if (!underground && diggingType == 0)
                {
                    aiTimer++;
                    CustomAI_3();
                    npc.noTileCollide = false;
                    if (aiTimer < 600)
                    {
                        shootTimer--;
                        if (shootTimer <= 0) shootTimer = enraged ? 60 : 100;
                        acidBallTimer--;
                        if (acidBallTimer <= 0) acidBallTimer = 900;
                        eggTimer--;

                        if (eggTimer <= 0 && NPC.CountNPCS(NPCType<WastelandMinion>()) < 10)
                        {
                            NPC.NewNPC((int)npc.Center.X + (npc.direction == -1 ? 40 : -40), (int)npc.Center.Y + 10, mod.NPCType("WastelandEgg"));
                            eggTimer = 120;
                        }
                        // shoot stingers
                        int under = Main.expertMode ? MyWorld.awakenedMode ? 24 : 18 : 12;
                        if (shootTimer % 6 == 0 && shootTimer <= under)
                        {
                            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 17);
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                float speed = 8f;
                                Vector2 tailPos = new Vector2(npc.Center.X + npc.direction * 40, npc.Center.Y - 30);
                                float rotation = (float)Math.Atan2(tailPos.Y - P.Center.Y, tailPos.X - P.Center.X);
                                Projectile.NewProjectile(tailPos.X, tailPos.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), ProjectileType<WastelandStinger>(), projectileBaseDamage, 0f, Main.myPlayer);
                            }
                        }
                        //storm
                        if (Main.expertMode)
                        {
                            stormTimer--;
                            if (stormTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                for (int k = 0; k < 3; k++)
                                {
                                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-12, -2), ProjectileType<WastelandStormBolt>(), 6, 0f, Main.myPlayer);
                                }
                                stormTimer = 1800;
                                if (enraged) stormTimer -= 300;
                            }
                        }
                        //acid balls
                        if (MyWorld.awakenedMode)
                        {
                            if (acidBallTimer % 5 == 0 && acidBallTimer <= 20 && Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 48);

                                float speed = 10f;
                                float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1) - (acidBallTimer / 5) * 2, ProjectileType<AcidBall>(), projectileBaseDamage, 0f, Main.myPlayer);
                            }
                        }
                        //jump up
                        jumpUpTimer--;
                        if (jumpUpTimer <= 0 || npc.Center.Y > P.Center.Y + 700)
                        {
                            JumpDust();
                            npc.velocity.Y = Main.rand.NextFloat(-12, -8);
                            Main.PlaySound(SoundID.Item69, npc.position);
                            jumpUpTimer = 350;
                            npc.netUpdate = true;
                        }
                    }
                    else if (aiTimer >= 600)
                    {
                        if (superJumpAI[1] == 0)
                        {
                            JumpDust();
                            npc.velocity.Y = Main.rand.NextFloat(-20, -14);
                            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 69);

                            jumpSpikeTimer = 0;
                            superJumpAI[1] = 1;
                            npc.netUpdate = true;
                        }
                        else if (superJumpAI[1] == 1)
                        {
                            superJumpAI[0]++;
                            if (superJumpAI[0] >= 60 || npc.velocity.Y > 0)
                            {
                                superJumpAI[1] = 2;
                            }
                        }
                        else if (superJumpAI[1] == 2)
                        {
                            if (!MyWorld.awakenedMode) superJumpAI[1] = Main.rand.Next(3, 5);
                            else superJumpAI[1] = Main.rand.Next(3, 6);
                            if (superJumpAI[1] == 5)
                            {
                                npc.velocity.Y = 12;
                            }
                            npc.netUpdate = true;
                            // 0 is nothing
                            // 1 is the jump
                            // 1 is air time
                            // 3 is normal drop
                            // 4 is spikes drop
                            // 5 is slam drop
                        }
                        else if (superJumpAI[1] == 3)
                        {
                            aiTimer = 0;
                            superJumpAI[0] = 0;
                            superJumpAI[1] = 0;
                            jumpUpTimer = 250;
                        }
                        else if (superJumpAI[1] == 4)
                        {
                            jumpSpikeTimer++; // jump up and circle shoot ai

                            npc.velocity = Vector2.Zero;
                            int timeBetweenSpikes = 15;
                            if (jumpSpikeTimer % timeBetweenSpikes == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                float numberProjectiles = 4;
                                if (Main.expertMode) numberProjectiles = 6;
                                if (MyWorld.awakenedMode) numberProjectiles = 8;
                                float projSpeed = 5.5f;
                                for (int i = 0; i < numberProjectiles; i++)
                                {
                                    Vector2 perturbedSpeed = new Vector2(projSpeed, projSpeed).RotatedByRandom(MathHelper.ToRadians(360));
                                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<WastelandStinger>(), projectileBaseDamage - 5, 2f, Main.myPlayer);
                                }
                                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 17);
                            }

                            if (jumpSpikeTimer >= timeBetweenSpikes * 3.5f)
                            {
                                aiTimer = 0;
                                superJumpAI[0] = 0;
                                superJumpAI[1] = 0;
                                jumpUpTimer = 250;
                            }
                        }
                        else if (superJumpAI[1] == 5)
                        {
                            if (npc.velocity.Y == 0f)
                            {
                                JumpDust();

                                Vector2 shockwavePosition = new Vector2(npc.Center.X, npc.Bottom.Y);
                                Point shockwavePoint = shockwavePosition.ToTileCoordinates();
                                Tile shockwaveTile = Framing.GetTileSafely((int)shockwavePoint.X, (int)shockwavePoint.Y);
                                if (!Main.tileSolid[shockwaveTile.type] && shockwaveTile.active())
                                {
                                    for (int i = 0; i < 3; i++)
                                    {
                                        if (shockwaveTile.active())
                                        {
                                            if (!Main.tileSolid[shockwaveTile.type])
                                            {
                                                shockwavePosition -= new Vector2(0f, 16);
                                                shockwavePoint = shockwavePosition.ToTileCoordinates();
                                                shockwaveTile = Framing.GetTileSafely((int)shockwavePosition.X, (int)shockwavePosition.Y);
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                {
                                    Projectile.NewProjectile(shockwavePoint.X * 16 + 8, shockwavePoint.Y * 16 + 8, 0f, 0f, ProjectileType<Shockwave>(), 0, 0f, Main.myPlayer, 24f, 1f);
                                    Projectile.NewProjectile(shockwavePoint.X * 16 + 8, shockwavePoint.Y * 16 + 8, 0f, 0f, ProjectileType<Shockwave>(), 0, 0f, Main.myPlayer, 24f, -1f);
                                }
                                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 69);

                                aiTimer = 0;
                                superJumpAI[0] = 0;
                                superJumpAI[1] = 0;
                                jumpUpTimer = 250;
                            }
                        }
                    }
                }
            }
        }
        private void JumpDust()
        {
            for (int k = 0; k < 250; k++)
            {
                int dust = Dust.NewDust(new Vector2(npc.BottomLeft.X, npc.BottomLeft.Y - 8), npc.width, 16, 75, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.5f;
                dust = Dust.NewDust(new Vector2(npc.BottomLeft.X, npc.BottomLeft.Y - 8), npc.width, 16, 32, 0f, 0f, 100, default(Color), 2.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.5f;
            }
        }
            public static void SandstormStuff()
        {
            Sandstorm.IntendedSeverity = !Sandstorm.Happening ? (Main.rand.Next(3) != 0 ? Main.rand.NextFloat() * 0.3f : 0.0f) : 0.4f + Main.rand.NextFloat();
            if (Main.netMode == 1)
                return;
        }

        private void DetectSpouts()
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.type == ProjectileType<WastelandDiggingSpoutReal>() && proj.ai[0] == npc.whoAmI)
                {
                    float posX = proj.Center.X;
                    float posY = proj.Center.Y + 300;
                    npc.Center = new Vector2(posX, posY);

                    if (proj.ai[1] >= 360)
                    {
                        int lifeTime = 0;
                        if (npc.life < npc.lifeMax * 0.75)lifeTime = 100;
                        else if (npc.life < npc.lifeMax * 0.5)  lifeTime = 200;
                        else if (npc.life < npc.lifeMax * 0.25)  lifeTime = 300;
                        spoutSpawnTimer = 1400 - lifeTime;

                        underground = false;
                        diggingType = 2; // 2 is up
                        digSpeed = 3f;
                        diggingTimer = 60;

                        proj.Kill();
                    }
                    npc.velocity.X = 0f;
                    npc.velocity.Y = 0f;
                }
            }
        }

        private void Dig(float digSpeed)
        {
            for (int k = 0; k < 10; k++)
            {
                int dust = Dust.NewDust(npc.position, npc.width, npc.height, 32);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].velocity *= 0.1f;
            }
            npc.noTileCollide = true;

            if (diggingType == 1) npc.velocity.Y = digSpeed;
            else if (diggingType == 2) npc.velocity.Y = -digSpeed;
            npc.velocity.X = 0f;

            diggingTimer--;
            if (diggingTimer <= 0)
            {
                if (diggingType == 1)
                {
                    underground = true;
                    diggingType = 0;
                }
                if (diggingType == 2)
                {
                    if (Collision.SolidCollision(npc.position,npc.width,npc.height) == false)
                    {
                        for (int k = 0; k < 500; k++)
                        {
                            int dust2 = Dust.NewDust(npc.position, npc.width, npc.height, 32, 0f, 0f, 100, default(Color), 1.5f);
                            Main.dust[dust2].noGravity = true;
                            Main.dust[dust2].velocity *= 1.5f;
                        }
                        npc.noTileCollide = false;
                        diggingType = 0;
                    }
                    underground = false;
                }
            }
        }

        private void CustomAI_3()
        {
            bool flag3 = false;
            if (npc.velocity.X == 0f)
            {
                flag3 = true;
            }
            if (npc.justHit)
            {
                flag3 = false;
            }
            int num35 = 60;
            bool flag4 = false;
            bool flag5 = false;
            bool flag6 = false;
            bool flag7 = true;

            if (!flag6 & flag7)
            {
                if (npc.velocity.Y == 0f && ((npc.velocity.X > 0f && npc.direction < 0) || (npc.velocity.X < 0f && npc.direction > 0)))
                {
                    flag4 = true;
                }
                if ((npc.position.X == npc.oldPosition.X || npc.ai[3] >= (float)num35) | flag4)
                {
                    npc.ai[3] += 1f;
                }
                else if ((double)Math.Abs(npc.velocity.X) > 0.9 && npc.ai[3] > 0f)
                {
                    npc.ai[3] -= 1f;
                }
                if (npc.ai[3] > (float)(num35 * 10))
                {
                    npc.ai[3] = 0f;
                }
                if (npc.justHit)
                {
                    npc.ai[3] = 0f;
                }
                if (npc.ai[3] == (float)num35)
                {
                    npc.netUpdate = true;
                }
            }

            float speed = 0.15f;
            if (MyWorld.awakenedMode) speed = 0.25f;
            if (npc.velocity.X < -2f || npc.velocity.X > 2f)
            {
                if (npc.velocity.Y == 0f)
                {
                    npc.velocity *= 0.8f;
                }
            }
            else if (npc.velocity.X < 2f && npc.direction == 1)
            {
                npc.velocity.X = npc.velocity.X + speed;
                if (npc.velocity.X > 2f)
                {
                    npc.velocity.X = 2f;
                }
            }
            else if (npc.velocity.X > -2f && npc.direction == -1)
            {
                npc.velocity.X = npc.velocity.X - speed;
                if (npc.velocity.X < -2f)
                {
                    npc.velocity.X = -2f;
                }
            }
        

            bool flag22 = false;
            if (npc.velocity.Y == 0f)
            {
                int num161 = (int)(npc.position.Y + (float)npc.height + 7f) / 16;
                int arg_A8FB_0 = (int)npc.position.X / 16;
                int num162 = (int)(npc.position.X + (float)npc.width) / 16;
                for (int num163 = arg_A8FB_0; num163 <= num162; num163++)
                {
                    if (Main.tile[num163, num161] == null)
                    {
                        return;
                    }
                    if (Main.tile[num163, num161].nactive() && Main.tileSolid[(int)Main.tile[num163, num161].type])
                    {
                        flag22 = true;
                        break;
                    }
                }
            }
            if (npc.velocity.Y >= 0f)
            {
                int num164 = 0;
                if (npc.velocity.X < 0f)
                {
                    num164 = -1;
                }
                if (npc.velocity.X > 0f)
                {
                    num164 = 1;
                }
                Vector2 position2 = npc.position;
                position2.X += npc.velocity.X;
                int num165 = (int)((position2.X + (float)(npc.width / 2) + (float)((npc.width / 2 + 1) * num164)) / 16f);
                int num166 = (int)((position2.Y + (float)npc.height - 1f) / 16f);
                if (Main.tile[num165, num166] == null)
                {
                    Main.tile[num165, num166] = new Tile();
                }
                if (Main.tile[num165, num166 - 1] == null)
                {
                    Main.tile[num165, num166 - 1] = new Tile();
                }
                if (Main.tile[num165, num166 - 2] == null)
                {
                    Main.tile[num165, num166 - 2] = new Tile();
                }
                if (Main.tile[num165, num166 - 3] == null)
                {
                    Main.tile[num165, num166 - 3] = new Tile();
                }
                if (Main.tile[num165, num166 + 1] == null)
                {
                    Main.tile[num165, num166 + 1] = new Tile();
                }
                if (Main.tile[num165 - num164, num166 - 3] == null)
                {
                    Main.tile[num165 - num164, num166 - 3] = new Tile();
                }
                if ((float)(num165 * 16) < position2.X + (float)npc.width && (float)(num165 * 16 + 16) > position2.X && ((Main.tile[num165, num166].nactive() && !Main.tile[num165, num166].topSlope() && !Main.tile[num165, num166 - 1].topSlope() && Main.tileSolid[(int)Main.tile[num165, num166].type] && !Main.tileSolidTop[(int)Main.tile[num165, num166].type]) || (Main.tile[num165, num166 - 1].halfBrick() && Main.tile[num165, num166 - 1].nactive())) && (!Main.tile[num165, num166 - 1].nactive() || !Main.tileSolid[(int)Main.tile[num165, num166 - 1].type] || Main.tileSolidTop[(int)Main.tile[num165, num166 - 1].type] || (Main.tile[num165, num166 - 1].halfBrick() && (!Main.tile[num165, num166 - 4].nactive() || !Main.tileSolid[(int)Main.tile[num165, num166 - 4].type] || Main.tileSolidTop[(int)Main.tile[num165, num166 - 4].type]))) && (!Main.tile[num165, num166 - 2].nactive() || !Main.tileSolid[(int)Main.tile[num165, num166 - 2].type] || Main.tileSolidTop[(int)Main.tile[num165, num166 - 2].type]) && (!Main.tile[num165, num166 - 3].nactive() || !Main.tileSolid[(int)Main.tile[num165, num166 - 3].type] || Main.tileSolidTop[(int)Main.tile[num165, num166 - 3].type]) && (!Main.tile[num165 - num164, num166 - 3].nactive() || !Main.tileSolid[(int)Main.tile[num165 - num164, num166 - 3].type]))
                {
                    float num167 = (float)(num166 * 16);
                    if (Main.tile[num165, num166].halfBrick())
                    {
                        num167 += 8f;
                    }
                    if (Main.tile[num165, num166 - 1].halfBrick())
                    {
                        num167 -= 8f;
                    }
                    if (num167 < position2.Y + (float)npc.height)
                    {
                        float num168 = position2.Y + (float)npc.height - num167;
                        float num169 = 16.1f;
                        if (num168 <= num169)
                        {
                            npc.gfxOffY += npc.position.Y + (float)npc.height - num167;
                            npc.position.Y = num167 - (float)npc.height;
                            if (num168 < 9f)
                            {
                                npc.stepSpeed = 1f;
                            }
                            else
                            {
                                npc.stepSpeed = 2f;
                            }
                        }
                    }
                }
            }
            if (flag22)
            {
                int num170 = (int)((npc.position.X + (float)(npc.width / 2) + (float)(15 * npc.direction)) / 16f);
                int num171 = (int)((npc.position.Y + (float)npc.height - 15f) / 16f);
                //if (npc.type == 257)
                {
                    num170 = (int)((npc.position.X + (float)(npc.width / 2) + (float)((npc.width / 2 + 16) * npc.direction)) / 16f);
                }
                if (Main.tile[num170, num171] == null)
                {
                    Main.tile[num170, num171] = new Tile();
                }
                if (Main.tile[num170, num171 - 1] == null)
                {
                    Main.tile[num170, num171 - 1] = new Tile();
                }
                if (Main.tile[num170, num171 - 2] == null)
                {
                    Main.tile[num170, num171 - 2] = new Tile();
                }
                if (Main.tile[num170, num171 - 3] == null)
                {
                    Main.tile[num170, num171 - 3] = new Tile();
                }
                if (Main.tile[num170, num171 + 1] == null)
                {
                    Main.tile[num170, num171 + 1] = new Tile();
                }
                if (Main.tile[num170 + npc.direction, num171 - 1] == null)
                {
                    Main.tile[num170 + npc.direction, num171 - 1] = new Tile();
                }
                if (Main.tile[num170 + npc.direction, num171 + 1] == null)
                {
                    Main.tile[num170 + npc.direction, num171 + 1] = new Tile();
                }
                if (Main.tile[num170 - npc.direction, num171 + 1] == null)
                {
                    Main.tile[num170 - npc.direction, num171 + 1] = new Tile();
                }
                Main.tile[num170, num171 + 1].halfBrick();
                if ((Main.tile[num170, num171 - 1].nactive() && (Main.tile[num170, num171 - 1].type == 10 || Main.tile[num170, num171 - 1].type == 388)) & flag5)
                {
                    npc.ai[2] += 1f;
                    npc.ai[3] = 0f;
                    if (npc.ai[2] >= 60f)
                    {
                        npc.velocity.X = 0.5f * (float)(-(float)npc.direction);
                        int num172 = 5;
                        if (Main.tile[num170, num171 - 1].type == 388)
                        {
                            num172 = 2;
                        }
                        npc.ai[1] += (float)num172;

                        npc.ai[2] = 0f;
                        bool flag23 = false;
                        if (npc.ai[1] >= 10f)
                        {
                            flag23 = true;
                            npc.ai[1] = 10f;
                        }
                        WorldGen.KillTile(num170, num171 - 1, true, false, false);
                        if ((Main.netMode != NetmodeID.MultiplayerClient || !flag23) && flag23 && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            if (Main.tile[num170, num171 - 1].type == 10)
                            {
                                bool flag24 = WorldGen.OpenDoor(num170, num171 - 1, npc.direction);
                                if (!flag24)
                                {
                                    npc.ai[3] = (float)num35;
                                    npc.netUpdate = true;
                                }
                                if (Main.netMode == 2 & flag24)
                                {
                                    NetMessage.SendData(19, -1, -1, null, 0, (float)num170, (float)(num171 - 1), (float)npc.direction, 0, 0, 0);
                                }
                            }
                            if (Main.tile[num170, num171 - 1].type == 388)
                            {
                                bool flag25 = WorldGen.ShiftTallGate(num170, num171 - 1, false);
                                if (!flag25)
                                {
                                    npc.ai[3] = (float)num35;
                                    npc.netUpdate = true;
                                }
                                if (Main.netMode == 2 & flag25)
                                {
                                    NetMessage.SendData(19, -1, -1, null, 4, (float)num170, (float)(num171 - 1), 0f, 0, 0, 0);
                                }
                            }
                        }
                    }
                }
                else
                {
                    int num173 = npc.spriteDirection;
                    if ((npc.velocity.X < 0f && num173 == -1) || (npc.velocity.X > 0f && num173 == 1))
                    {
                        if (npc.height >= 32 && Main.tile[num170, num171 - 2].nactive() && Main.tileSolid[(int)Main.tile[num170, num171 - 2].type])
                        {
                            if (Main.tile[num170, num171 - 3].nactive() && Main.tileSolid[(int)Main.tile[num170, num171 - 3].type])
                            {
                                npc.velocity.Y = -8f;
                                npc.netUpdate = true;
                            }
                            else
                            {
                                npc.velocity.Y = -7f;
                                npc.netUpdate = true;
                            }
                        }
                        else if (Main.tile[num170, num171 - 1].nactive() && Main.tileSolid[(int)Main.tile[num170, num171 - 1].type])
                        {
                            npc.velocity.Y = -6f;
                            npc.netUpdate = true;
                        }
                        else if (npc.position.Y + (float)npc.height - (float)(num171 * 16) > 20f && Main.tile[num170, num171].nactive() && !Main.tile[num170, num171].topSlope() && Main.tileSolid[(int)Main.tile[num170, num171].type])
                        {
                            npc.velocity.Y = -5f;
                            npc.netUpdate = true;
                        }
                        else if (npc.directionY < 0 && (!Main.tile[num170, num171 + 1].nactive() || !Main.tileSolid[(int)Main.tile[num170, num171 + 1].type]) && (!Main.tile[num170 + npc.direction, num171 + 1].nactive() || !Main.tileSolid[(int)Main.tile[num170 + npc.direction, num171 + 1].type]))
                        {
                            npc.velocity.Y = -8f;
                            npc.velocity.X = npc.velocity.X * 1.5f;
                            npc.netUpdate = true;
                        }
                        else if (flag5)
                        {
                            npc.ai[1] = 0f;
                            npc.ai[2] = 0f;
                        }
                        if ((npc.velocity.Y == 0f & flag3) && npc.ai[3] == 1f)
                        {
                            npc.velocity.Y = -5f;
                        }
                    }
                }
            }
            else if (flag5)
            {
                npc.ai[1] = 0f;
                npc.ai[2] = 0f;
            }
        }
    }
}