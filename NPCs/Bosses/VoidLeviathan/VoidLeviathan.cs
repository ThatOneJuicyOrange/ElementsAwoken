using ElementsAwoken.Items.BossDrops.VoidLeviathan;
using ElementsAwoken.Items.Essence;
using ElementsAwoken.Items.Pets;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.NPCs.Bosses.VoidLeviathan
{
    [AutoloadBossHead]
    class VoidLeviathanHead : VoidLeviathan
    {
        public override string Texture { get { return "ElementsAwoken/NPCs/Bosses/VoidLeviathan/VoidLeviathanHead"; } }

        public override void SetDefaults()
        {
            base.SetDefaults();

            npc.width = 108;
            npc.height = 134;

            npc.damage = 250;
            npc.knockBackResist = 0f;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;

            npc.value = Item.buyPrice(1, 50, 0, 0);
            npc.npcSlots = 1f;
            music = MusicID.LunarBoss;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/VoidLeviathanTheme");
            npc.netAlways = true;

            bossBag = mod.ItemType("VoidLeviathanBag");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 700000;
            npc.damage = 350;
            npc.defense = 56;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 850000;
                npc.damage = 450;
                npc.defense = 67;
            }
        }
        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                npc.DropBossBags();
                if (Main.rand.Next(10) == 0) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<VoidCrystal>());
            }
            else
            {
                int choice = Main.rand.Next(10);
                switch (choice)
                {
                    case 0:
                        choice = ItemType<VoidInferno>();
                        break;
                    case 1:
                        choice = ItemType<EndlessAbyssBlaster>();
                        break;
                    case 2:
                        choice = ItemType<ExtinctionBow>();
                        break;
                    case 3:
                        choice = ItemType<Reaperstorm>();
                        break;
                    case 4:
                        choice = ItemType<BladeOfTheNight>();
                        break;
                    case 5:
                        choice = ItemType<CosmicWrath>();
                        break;
                    case 6:
                        choice = ItemType<PikeOfEternalDespair>();
                        break;
                    case 7:
                        choice = ItemType<VoidLeviathansAegis>();
                        break;
                    case 8:
                        choice = ItemType<BreathOfDarkness>(); 
                        break;
                    case 9:
                        choice = ItemType<LightsAffliction>();
                        break;
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, choice);
                if (Main.rand.Next(7) == 0) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<VoidLeviathanMask>());
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<VoidLeviathanHeart>(), 1);

                if (Main.rand.Next(10) == 0) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<VoidLeviathanTrophy>());
            }
            int essenceAmount = Main.rand.Next(2, 8);
            if (Main.expertMode) essenceAmount = Main.rand.Next(5, 13);
            if (MyWorld.awakenedMode) essenceAmount = Main.rand.Next(8, 20);
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<VoidEssence>(), essenceAmount);

            if (!MyWorld.downedVoidLeviathan)
            {
                ElementsAwoken.encounter = 3;
                ElementsAwoken.encounterTimer = 3600;
                ElementsAwoken.DebugModeText("encounter 3 start");
                Main.NewText("Infection overcomes the world...", new Color(235, 70, 106));
            }
            if (MyWorld.voidLeviathanKills < 3)
            {
                MyWorld.genVoidite = true;
            }
            MyWorld.voidLeviathanKills++;
            MyWorld.downedVoidLeviathan = true;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.

        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = mod.ItemType("EpicHealingPotion");
        }
        public override bool CheckActive()
        {
            return false;
        }

        public override void Init()
        {
            base.Init();
            head = true;
        }

        private int projectileBaseDamage = 80;

        private float attackCounter;
        private float strikeCircleTimer;
        private float roarTimer;
        private float despawnTimer = 0;
        public float orbTimer = 0;
        private int spawnNPCs = 0;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
            writer.Write(strikeCircleTimer);
            writer.Write(roarTimer);
            writer.Write(despawnTimer);
            writer.Write(orbTimer);
            writer.Write(spawnNPCs);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadSingle();
            strikeCircleTimer = reader.ReadSingle();
            roarTimer = reader.ReadSingle();
            despawnTimer = reader.ReadSingle();
            orbTimer = reader.ReadSingle();
            spawnNPCs = reader.ReadInt32();
        }

        public override void CustomBehavior()
        {
            Player P = Main.player[npc.target];
            MyPlayer modPlayer = P.GetModPlayer<MyPlayer>();
            if (P.dead)
            {
                npc.velocity.Y = npc.velocity.Y + 0.11f;
                despawnTimer++;
                if (despawnTimer >= 300)
                {
                    npc.active = false;
                }
            }
            roarTimer--;
            if (roarTimer <= 0)
            {
                Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 10);
                roarTimer = 300 + Main.rand.Next(1, 400);
            }
            int maxDist = 3000;
            if (Main.netMode == 0)
            {
                P.AddBuff(mod.BuffType("BehemothGaze"), 20, true);
                float dist = Vector2.Distance(npc.Center, P.Center);
                if (dist > maxDist) modPlayer.behemothGazeTimer++;
                else modPlayer.behemothGazeTimer = 0;
                modPlayer.leviathanDist = (int)dist;
            }
            else
            {
                for (int i = 0; i < Main.player.Length; i++)
                {
                    Player loopP = Main.player[i];
                    MyPlayer modLoopP = P.GetModPlayer<MyPlayer>();
                    if (loopP.active && !loopP.dead)
                    {
                        loopP.AddBuff(mod.BuffType("BehemothGaze"), 20, true);
                        float dist = Vector2.Distance(npc.Center, loopP.Center);
                        if (dist > maxDist) modLoopP.behemothGazeTimer++;
                        else modLoopP.behemothGazeTimer = 0;
                        modLoopP.leviathanDist = (int)dist;
                    }
                }
            }
            if (spawnNPCs == 0)
            {
                int soulCount = Main.expertMode ? 4 : 3;
                if (MyWorld.awakenedMode) soulCount = 7;
                for (int l = 0; l < soulCount; l++)
                {
                    int distance = 360 / soulCount;
                    NPC soul = Main.npc[NPC.NewNPC((int)(npc.Center.X + (Math.Sin(l * distance) * 150)), (int)(npc.Center.Y + (Math.Cos(l * distance) * 150)), mod.NPCType("BarrenOrbital"), npc.whoAmI, l * distance, npc.whoAmI)];
                    soul.ai[2] += 60 * l;
                }
                spawnNPCs++;
            }
            strikeCircleTimer++;
            if (strikeCircleTimer >= 500 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int numProj = Main.expertMode ? 12 : 8;
                int dist = Main.expertMode ? 1400 : 1600;
                if (MyWorld.awakenedMode)
                {
                    numProj = 16;
                    dist = 1200;
                }
                int projDamage = Main.expertMode ? (int)(projectileBaseDamage * 1.5f) : projectileBaseDamage;
                if (MyWorld.awakenedMode) projDamage = (int)(projectileBaseDamage * 1.8f);
                for (float l = 0; l < numProj; l++)
                {
                    Vector2 projPos = P.Center + new Vector2(0, dist).RotatedBy(l * (Math.PI * 2f / numProj));
                    Vector2 projVel = P.Center - projPos;
                    projVel.Normalize();
                    projVel *= 8f;
                    Projectile strike = Main.projectile[Projectile.NewProjectile(projPos.X, projPos.Y, projVel.X, projVel.Y, mod.ProjectileType("VoidStrike"), projDamage, 6f, 0)];
                    strike.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                }
                strikeCircleTimer = 0;
            }
            attackCounter++;
            int num = 600;
            if (npc.life <= npc.lifeMax * 0.9f) num -= 25;
            if (npc.life <= npc.lifeMax * 0.7f) num -= 25;
            if (npc.life <= npc.lifeMax * 0.5f) num -= 25;
            if (npc.life <= npc.lifeMax * 0.3f) num -= 75;
            if (npc.life <= npc.lifeMax * 0.1f) num -= 75;
            if (attackCounter % num == 0)
            {
                int numSouls = Main.expertMode ? 3 : 1;
                if (MyWorld.awakenedMode) numSouls = 5;
                int distance = 600;
                for (float l = 0; l < numSouls; l++)
                {
                    Vector2 soulPos = new Vector2(P.Center.X + Main.rand.Next(-distance, distance), P.Center.Y + Main.rand.Next(-distance, distance));
                    NPC soul = Main.npc[NPC.NewNPC((int)soulPos.X, (int)soulPos.Y, mod.NPCType("BarrenSoul"), npc.whoAmI)];
                    soul.ai[2] -= 30 * l;
                }
            }
            if (aiTimer > 600 && aiTimer <= 1020)
            {
                if (aiTimer % 45 == 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 8);
                    int projDamage = Main.expertMode ? (int)(projectileBaseDamage * 1.5f) : projectileBaseDamage;
                    if (MyWorld.awakenedMode) projDamage = (int)(projectileBaseDamage * 2f);

                    Projectile rune = Main.projectile[Projectile.NewProjectile(P.Center.X + Main.rand.Next(-600, 600), P.Center.Y + Main.rand.Next(-600, 600), Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f), mod.ProjectileType("VoidRunes"), projDamage, 6f, Main.myPlayer)];
                    rune.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                }
            }
            if (!NPC.AnyNPCs(mod.NPCType("VoidLeviathanOrb"))) orbTimer++;
            if (npc.life <= npc.lifeMax * 0.3f)
            {
                int vulnerableTime = Main.expertMode ? 1200 : 1800;
                if (MyWorld.awakenedMode) vulnerableTime = 1050;
                if (orbTimer > vulnerableTime)
                {
                    int numOrbs = Main.expertMode ? 2 : 1;
                    if (MyWorld.awakenedMode) numOrbs = 3;
                    int i = 0;
                    int tries = 0;
                    float[] otherPosX = new float[numOrbs];
                    float[] otherPosY = new float[numOrbs];
                    while (i < numOrbs)
                    {
                        int maxOrbDistance = 1500;

                        Vector2 orbPos = new Vector2(P.Center.X + Main.rand.Next(-maxOrbDistance, maxOrbDistance), /*P.Center.Y*/ 2700 + Main.rand.Next(-maxOrbDistance, maxOrbDistance));

                        Vector2 worldMapSize = new Vector2(Main.maxTilesX * 16, Main.maxTilesY * 16);
                        if (orbPos.X < 0) orbPos.X = Main.rand.Next(100, maxOrbDistance / 2);
                        else if (orbPos.X > worldMapSize.X) orbPos.X = worldMapSize.X- Main.rand.Next(200, maxOrbDistance / 2);
                        if (orbPos.Y < 0) orbPos.Y = Main.rand.Next(100, maxOrbDistance / 2);
                        else if (orbPos.Y > worldMapSize.Y) orbPos.Y = worldMapSize.Y - Main.rand.Next(200, maxOrbDistance / 2);

                        bool tooCloseToOthers = false;
                        for (int k = 0; k < otherPosX.Length; k++)
                        {
                            if (otherPosX[k] != 0)
                            {
                                if (Vector2.Distance(new Vector2(otherPosX[k], otherPosY[k]), orbPos) < 600) tooCloseToOthers = true;
                            }
                        }
                        Tile orbTile = Framing.GetTileSafely((int)(orbPos.X / 16), (int)(orbPos.Y / 16));
                        if ((!orbTile.active() /*&& orbTile.wall == 0*/&& !tooCloseToOthers) || tries > 1000)
                        {
                            NPC.NewNPC((int)orbPos.X, (int)orbPos.Y, mod.NPCType("VoidLeviathanOrb"), npc.whoAmI);
                            for (int k = 0; k < otherPosX.Length; k++)
                            {
                                if (otherPosX[k] == 0)
                                {
                                    otherPosX[k] = orbPos.X;
                                    otherPosY[k] = orbPos.Y;
                                    break;
                                }
                            }
                            i++;
                        }
                        tries++;
                        //Main.NewText("tries: "+ tries + " i: " +i);
                    }
                    orbTimer = 0;
                }

                if (spawnNPCs == 1)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("ElderShadeWyrmHead"), npc.whoAmI);
                    spawnNPCs++;
                }
                else if (spawnNPCs == 2 && npc.life <= npc.lifeMax * 0.2f && Main.expertMode)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("ElderShadeWyrmHead"), npc.whoAmI);
                    spawnNPCs++;
                }
                else if (spawnNPCs == 3 && npc.life <= npc.lifeMax * 0.1f && MyWorld.awakenedMode)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("ElderShadeWyrmHead"), npc.whoAmI);
                    spawnNPCs++;
                }
            }
        }
    }
    [AutoloadBossHead]
    class VoidLeviathanBody : VoidLeviathan
    {
        public override string Texture { get { return "ElementsAwoken/NPCs/Bosses/VoidLeviathan/VoidLeviathanBody"; } }

        public override void SetDefaults()
        {
            base.SetDefaults();

            npc.width = 80;
            npc.height = 68;

            npc.damage = 150;
            npc.knockBackResist = 0.0f;

            npc.behindTiles = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.noGravity = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 175;
            npc.defense = 56;
            if (MyWorld.awakenedMode)
            {
                npc.damage = 200;
                npc.defense = 67;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }


        private int projectileBaseDamage = 100;
        public int bodyNum = 0;

        public override void CustomBehavior()
        {
            Player P = Main.player[npc.target];
            NPC headNPC = Main.npc[(int)npc.ai[3]];
            int num = Main.expertMode ? 3 : 5;
            if (MyWorld.awakenedMode) num = 2;

            if (bodyNum % num == 0 && headNPC.life > headNPC.lifeMax * 0.3f)
            {
                if (aiTimer == 1440 + betweenShots * bodyNum && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int projDamage = Main.expertMode ? (int)(projectileBaseDamage * 1.3f) : projectileBaseDamage;
                    if (MyWorld.awakenedMode) projDamage = (int)(projectileBaseDamage * 1.5f);

                    float speedMult = Main.expertMode ? 8f : 6f;
                    if (MyWorld.awakenedMode) speedMult = 10f;
                    for (int k = 0; k < 2; k++)
                    {
                        Vector2 projSpeed = new Vector2(0, 1).RotatedBy(npc.rotation + MathHelper.ToRadians(k == 0 ? 90 : 270));
                        projSpeed.Normalize();
                        projSpeed *= speedMult;

                        Projectile bolt = Main.projectile[Projectile.NewProjectile(npc.Center, projSpeed, mod.ProjectileType("VoidBolt"), projDamage, 0f, Main.myPlayer)];
                        bolt.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                    }
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 12);
                }
            }
        }
    }
    [AutoloadBossHead]
    class VoidLeviathanTail : VoidLeviathan
    {
        public override string Texture { get { return "ElementsAwoken/NPCs/Bosses/VoidLeviathan/VoidLeviathanTail"; } }

        public override void SetDefaults()
        {
            base.SetDefaults();

            npc.width = 80;
            npc.height = 100;

            npc.damage = 150;
            npc.defense = 10;
            npc.knockBackResist = 0.0f;

            npc.behindTiles = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.noGravity = true;
            npc.dontCountMe = true;

            npc.takenDamageMultiplier = 3;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 175;
            npc.defense = 20;
            if (MyWorld.awakenedMode)
            {
                npc.damage = 200;
                npc.defense = 30;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
        private int attackCounter;
        private int projectileBaseDamage = 150;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
        }
        public override void CustomBehavior()
        {
            Player P = Main.player[npc.target];

            if (Vector2.Distance(P.Center, npc.Center) < 600)
            {
                attackCounter++;

                if (attackCounter >= 20 && Main.netMode != NetmodeID.MultiplayerClient && !npc.dontTakeDamage)
                {
                    int numProj = Main.expertMode ? 8 : 4;
                    if (MyWorld.awakenedMode) numProj = 16;
                    for (float l = 0; l < numProj; l++)
                    {
                        Vector2 projPos = npc.Center + new Vector2(0, 2).RotatedBy(l * (Math.PI * 2f / numProj));
                        Vector2 projVel = projPos - npc.Center;
                        projVel.Normalize();
                        projVel *= 8f;
                        Projectile.NewProjectile(projPos.X, projPos.Y, projVel.X, projVel.Y, mod.ProjectileType("VoidSpine"), projectileBaseDamage, 6f, Main.myPlayer);
                    }
                    attackCounter = 0;
                }
            }
            else attackCounter = -90;
        }
        public override void Init()
        {
            base.Init();
            tail = true;
        }
    }

    // I made this 2nd base class to limit code repetition.
    public abstract class VoidLeviathan : VoidLeviathanAI
    {
        public override void SetDefaults()
        {
            npc.lifeMax = 500000;
            npc.defense = 50;
            npc.scale = 1.1f;

            npc.aiStyle = -1;

            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath6;

            NPCsGLOBAL.ImmuneAllEABuffs(npc);
            // all vanilla buffs
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Leviathan");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void Init()
        {
            wormLength = 60;
            tailType = NPCType<VoidLeviathanTail>();
            bodyType = NPCType<VoidLeviathanBody>();
            headType = NPCType<VoidLeviathanHead>();
            speed = 40f;
            if (MyWorld.awakenedMode) speed = 50f;
            else if (Main.expertMode) speed = 60f;
            turnSpeed =  0.6f;
            if (MyWorld.awakenedMode) turnSpeed = 0.8f;
            else if (Main.expertMode) turnSpeed = 0.6f;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }

    public abstract class VoidLeviathanAI : ModNPC
    {
        /* ai[0] = follower
		 * ai[1] = following
		 * ai[2] = distanceFromTail
		 * ai[3] = head
		 */
        public bool head;
        public bool tail;
        public int wormLength;
        public int headType;
        public int bodyType;
        public int tailType;
        public bool directional = false;
        public float speed;
        public float turnSpeed;

        public float aiTimer = 0;
        private float wanderTimer = 0;
        private float wanderX = 0;
        private float wanderY = 0;
        private int circleNum = 0;

        public const int betweenShots = 4;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(aiTimer);
            writer.Write(wanderTimer);
            writer.Write(wanderX);
            writer.Write(wanderY);
            writer.Write(circleNum);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            aiTimer = reader.ReadSingle();
            wanderTimer = reader.ReadSingle();
            wanderX = reader.ReadSingle();
            wanderY = reader.ReadSingle();
            circleNum = reader.ReadInt32();
        }

        public override void AI()
        {
            Player P = Main.player[npc.target];
            NPC headNPC = Main.npc[(int)npc.ai[3]];
            if (npc.localAI[1] == 0f)
            {
                npc.localAI[1] = 1f;
                Init();
            }
            //Main.NewText(npc.Center + " | " + new Vector2(wanderX,wanderY));
            int phase = 0;
            if (aiTimer > 600 && aiTimer <= 1020) phase = 1;
            if (aiTimer > 1020 && aiTimer <= 1440) phase = 2;
            if (aiTimer > 1440) phase = 3;
            //Main.NewText(state);

            if (headNPC.life <= headNPC.lifeMax * 0.3f && headNPC.life != 0)
            {
                if (npc.localAI[3] == 0)
                {
                    bool doGore = true;
                    if (!tail && !head)
                    {
                        VoidLeviathanBody vleviBody = (VoidLeviathanBody)npc.modNPC;
                        int modNum = 4;
                        if (ModContent.GetInstance<Config>().lowDust) modNum = 2;
                        if (vleviBody.bodyNum % modNum == 0) doGore = false;
                    }
                    if (doGore)
                    {
                        Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/" + this.GetType().Name), 1.1f);
                        npc.position.X = npc.position.X + (float)(npc.width / 2);
                        npc.position.Y = npc.position.Y + (float)(npc.height / 2);
                        npc.width = 50;
                        npc.height = 50;
                        npc.position.X = npc.position.X - (float)(npc.width / 2);
                        npc.position.Y = npc.position.Y - (float)(npc.height / 2);
                    }
                    if (head)
                    {
                        VoidLeviathanHead vlevi = (VoidLeviathanHead)npc.modNPC;
                        vlevi.orbTimer = 1800;
                    }
                    npc.localAI[3]++;
                }
                if (NPC.AnyNPCs(mod.NPCType("VoidLeviathanOrb")))
                {
                    npc.immortal = true;
                    npc.dontTakeDamage = true;
                    npc.alpha = 200;
                    if (head)
                    {
                        aiTimer = 620;
                        Wander(P);
                    }
                }
                else
                {
                    npc.immortal = false;
                    npc.dontTakeDamage = false;
                    npc.alpha = 90;
                    if (head)
                    {
                        aiTimer = 0;
                    }
                }
                if (!tail)
                {
                    npc.defense = 20;
                    if (MyWorld.awakenedMode) npc.defense = 30;
                }
                else
                {
                    npc.defense = 0;
                }
                npc.HitSound = SoundID.NPCHit49;
            }
            else aiTimer++;
            if (aiTimer > 1440 + wormLength * betweenShots) aiTimer = 0;


            //worm stuff down
            if (npc.ai[3] > 0f)
            {
                npc.realLife = (int)npc.ai[3];
            }
            if (!head && npc.timeLeft < 300)
            {
                npc.timeLeft = 300;
            }
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
            }
            if (Main.player[npc.target].dead && npc.timeLeft > 300)
            {
                npc.timeLeft = 300;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!tail && npc.ai[0] == 0f)
                {
                    if (head)
                    {
                        npc.ai[3] = (float)npc.whoAmI;
                        npc.realLife = npc.whoAmI;
                        npc.ai[2] = wormLength;
                        npc.ai[0] = (float)NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), bodyType, npc.whoAmI);
                    }
                    else if (npc.ai[2] > 0f)
                    {
                        npc.ai[0] = (float)NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), npc.type, npc.whoAmI);
                        VoidLeviathanBody bodyNPC = (VoidLeviathanBody)npc.modNPC;
                        VoidLeviathanBody newBodyNPC = (VoidLeviathanBody)Main.npc[(int)npc.ai[0]].modNPC;
                        newBodyNPC.bodyNum = bodyNPC.bodyNum + 1;
                    }
                    else
                    {
                        npc.ai[0] = (float)NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), tailType, npc.whoAmI);
                    }
                    Main.npc[(int)npc.ai[0]].ai[3] = npc.ai[3];
                    Main.npc[(int)npc.ai[0]].realLife = npc.realLife;
                    Main.npc[(int)npc.ai[0]].ai[1] = (float)npc.whoAmI;
                    Main.npc[(int)npc.ai[0]].ai[2] = npc.ai[2] - 1f;
                    npc.netUpdate = true;
                }
                if (!head && (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[1]].type != headType && Main.npc[(int)npc.ai[1]].type != bodyType))
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                }
                if (!tail && (!Main.npc[(int)npc.ai[0]].active || Main.npc[(int)npc.ai[0]].type != bodyType && Main.npc[(int)npc.ai[0]].type != tailType))
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                }
                if (!npc.active && Main.netMode == 2)
                {
                    NetMessage.SendData(28, -1, -1, null, npc.whoAmI, -1f, 0f, 0f, 0, 0, 0);
                }
            }
            if (directional)
            {
                if (npc.velocity.X < 0f)
                {
                    npc.spriteDirection = 1;
                }
                else if (npc.velocity.X > 0f)
                {
                    npc.spriteDirection = -1;
                }
            }
            float speedAI = speed;
            float turnSpeedAI = turnSpeed;
            if (Main.dayTime)
            {
                speedAI += 20;
                turnSpeedAI += 0.2f;
                if (npc.localAI[2] == 0)
                {
                    npc.damage += 50;
                    npc.localAI[2]++;
                }
            }
            //if (MathHelper.Distance(npc.Center.Y, P.Center.Y) > 2000 && phase == 0) speedAI *= 0.3f; // to stop him going turbo speed

            Vector2 vector18 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float targetX = P.Center.X;
            float targetY = P.Center.Y;
            if (phase == 1 || phase == 2)
            {
                speedAI *= 0.2f;
                turnSpeedAI *= 1.2f;
                if (wanderX != 0 && wanderY != 0)
                {
                    targetX = wanderX;
                    targetY = wanderY;
                }
            }
            targetX = (float)((int)(targetX / 16f) * 16);
            targetY = (float)((int)(targetY / 16f) * 16);
            vector18.X = (float)((int)(vector18.X / 16f) * 16);
            vector18.Y = (float)((int)(vector18.Y / 16f) * 16);
            targetX -= vector18.X;
            targetY -= vector18.Y;
            float num193 = (float)Math.Sqrt((double)(targetX * targetX + targetY * targetY));
            if (npc.ai[1] > 0f && npc.ai[1] < (float)Main.npc.Length)
            {
                try
                {
                    vector18 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    targetX = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - vector18.X;
                    targetY = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - vector18.Y;
                }
                catch
                {
                }
                npc.rotation = (float)Math.Atan2((double)targetY, (double)targetX) + 1.57f;
                num193 = (float)Math.Sqrt((double)(targetX * targetX + targetY * targetY));
                int num194 = npc.width;
                num193 = (num193 - (float)num194) / num193;
                targetX *= num193;
                targetY *= num193;
                npc.velocity = Vector2.Zero;
                npc.position.X = npc.position.X + targetX;
                npc.position.Y = npc.position.Y + targetY;
                if (directional)
                {
                    if (targetX < 0f)
                    {
                        npc.spriteDirection = 1;
                    }
                    if (targetX > 0f)
                    {
                        npc.spriteDirection = -1;
                    }
                }
            }
            else
            {

                num193 = (float)System.Math.Sqrt((double)(targetX * targetX + targetY * targetY));
                float num196 = System.Math.Abs(targetX);
                float num197 = System.Math.Abs(targetY);
                float num198 = speedAI / num193;
                targetX *= num198;
                targetY *= num198;

                if (headNPC.life > npc.lifeMax * 0.3f)
                    if (phase == 1)
                    {
                        Wander(P);
                    }
                if (phase == 2)
                {
                    Circle(P);
                }
                if (phase < 3)
                {
                    float yTurnSpeedScale = 0.3f;
                    float xTurnSpeedScale = 1f;
                    if (MathHelper.Distance(npc.Center.Y, P.Center.Y) > 600 || phase != 0) yTurnSpeedScale = 1f; // to stop him turning so hard onto the player
                    if (MathHelper.Distance(npc.Center.Y, P.Center.Y) > 900 && MathHelper.Distance(npc.Center.Y, P.Center.Y) < 3000 && phase == 0) targetY *= 0.2f; // to stop him going turbo speed
                    if (MathHelper.Distance(npc.Center.X, P.Center.X) > 3000)
                    {
                        xTurnSpeedScale = 5;
                        targetX *= 15f;
                    }
                    if (MathHelper.Distance(npc.Center.X, P.Center.X) > 2500) if ((npc.velocity.X > targetX && npc.velocity.X > 0) || npc.velocity.X < targetX && npc.velocity.X < 0) npc.velocity.X *= 0.5f;      
                    if (npc.velocity.X > 0f && targetX > 0f || npc.velocity.X < 0f && targetX < 0f || npc.velocity.Y > 0f && targetY > 0f || npc.velocity.Y < 0f && targetY < 0f)
                    {
                        if (npc.velocity.X < targetX)
                        {
                            npc.velocity.X = npc.velocity.X + turnSpeedAI * xTurnSpeedScale;
                        }
                        else
                        {
                            if (npc.velocity.X > targetX)
                            {
                                npc.velocity.X = npc.velocity.X - turnSpeedAI * xTurnSpeedScale;
                            }
                        }
 

                        if (npc.velocity.Y < targetY)
                        {
                            npc.velocity.Y = npc.velocity.Y + turnSpeedAI * yTurnSpeedScale;
                        }
                        else
                        {
                            if (npc.velocity.Y > targetY)
                            {
                                npc.velocity.Y = npc.velocity.Y - turnSpeedAI * yTurnSpeedScale;
                            }
                        }
                        if ((double)System.Math.Abs(targetY) < (double)speedAI * 0.2 && (npc.velocity.X > 0f && targetX < 0f || npc.velocity.X < 0f && targetX > 0f))
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y + turnSpeedAI * 2f;
                            }
                            else
                            {
                                npc.velocity.Y = npc.velocity.Y - turnSpeedAI * 2f;
                            }
                        }
                        if ((double)System.Math.Abs(targetX) < (double)speedAI * 0.2 && (npc.velocity.Y > 0f && targetY < 0f || npc.velocity.Y < 0f && targetY > 0f))
                        {
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X = npc.velocity.X + turnSpeedAI * 2f;
                            }
                            else
                            {
                                npc.velocity.X = npc.velocity.X - turnSpeedAI * 2f;
                            }
                        }
                    }
                    else
                    {
                        if (num196 > num197)
                        {
                            if (npc.velocity.X < targetX)
                            {
                                npc.velocity.X = npc.velocity.X + turnSpeedAI * 1.1f;
                            }
                            else if (npc.velocity.X > targetX)
                            {
                                npc.velocity.X = npc.velocity.X - turnSpeedAI * 1.1f;
                            }
                            if ((double)(System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y)) < (double)speedAI * 0.5)
                            {
                                if (npc.velocity.Y > 0f)
                                {
                                    npc.velocity.Y = npc.velocity.Y + turnSpeedAI;
                                }
                                else
                                {
                                    npc.velocity.Y = npc.velocity.Y - turnSpeedAI;
                                }
                            }
                        }
                        else
                        {
                            if (npc.velocity.Y < targetY)
                            {
                                npc.velocity.Y = npc.velocity.Y + turnSpeedAI * 1.1f;
                            }
                            else if (npc.velocity.Y > targetY)
                            {
                                npc.velocity.Y = npc.velocity.Y - turnSpeedAI * 1.1f;
                            }
                            if ((double)(System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y)) < (double)speedAI * 0.5)
                            {
                                if (npc.velocity.X > 0f)
                                {
                                    npc.velocity.X = npc.velocity.X + turnSpeedAI;
                                }
                                else
                                {
                                    npc.velocity.X = npc.velocity.X - turnSpeedAI;
                                }
                            }

                        }
                    }
                }
                else
                {
                    npc.velocity *= 0.96f;
                }

                npc.rotation = (float)System.Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f;
                if (head)
                {

                    if (npc.localAI[0] != 1f)
                    {
                        npc.netUpdate = true;
                    }
                    npc.localAI[0] = 1f;

                    if ((npc.velocity.X > 0f && npc.oldVelocity.X < 0f || npc.velocity.X < 0f && npc.oldVelocity.X > 0f || npc.velocity.Y > 0f && npc.oldVelocity.Y < 0f || npc.velocity.Y < 0f && npc.oldVelocity.Y > 0f) && !npc.justHit)
                    {
                        npc.netUpdate = true;
                        return;
                    }
                }
            }
            CustomBehavior();
        }
        private void Wander(Player P)
        {
            if (ModContent.GetInstance<Config>().debugMode)
            {
                int dust = Dust.NewDust(new Vector2(wanderX, wanderY), 16, 16, DustID.PinkFlame);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0.1f;
            }
            if (wanderX == 0 || wanderY == 0 || Vector2.Distance(new Vector2(wanderX, wanderY), P.Center) > 1600)
            {
                wanderX = P.Center.X + Main.rand.Next(-800, 800);
                wanderY = P.Center.Y + Main.rand.Next(-800, 800);
                npc.netUpdate = true;
            }
            wanderTimer++;
            if (wanderTimer >= 180 || Vector2.Distance(new Vector2(wanderX, wanderY), npc.Center) < 40)
            {
                wanderX = P.Center.X + Main.rand.Next(-800, 800);
                wanderY = P.Center.Y + Main.rand.Next(-800, 800);
                npc.netUpdate = true;
                wanderTimer = 0;
            }
        }
        private void Circle(Player P)
        {
            if (ModContent.GetInstance<Config>().debugMode)
            {
                int dust = Dust.NewDust(new Vector2(wanderX, wanderY), 16, 16, DustID.PinkFlame);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0.1f;
            }
            if (Vector2.Distance(new Vector2(wanderX, wanderY), npc.Center) < 40 || (wanderX == 0 || wanderY == 0) || Vector2.Distance(new Vector2(wanderX, wanderY), P.Center) > 1600)
            {
                int numPoints = 180;
                Vector2 circlePos = P.Center + new Vector2(0, 600).RotatedBy(circleNum * (Math.PI * 2f / numPoints));
                wanderX = circlePos.X;
                wanderY = circlePos.Y;
                circleNum++;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC headNPC = Main.npc[(int)npc.ai[3]];
            if (headNPC.life <= headNPC.lifeMax * 0.3f)
            {
                npc.frame.Y = frameHeight;
                if (npc.alpha > 150) npc.frame.Y = frameHeight * 2;
            }
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(mod.BuffType("ExtinctionCurse"), 300, true);
        }
        public virtual void Init()
        {
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.type == ProjectileID.LastPrismLaser)
            {
                damage = (int)(damage * 0.1f);
            }
            else if (projectile.penetrate == -1)
            {
                damage = (int)(damage * 0.3f);
            }
            else
            {
                if (projectile.penetrate > projectile.maxPenetrate * 0.8f) damage = (int)MathHelper.Lerp(0, damage * 0.7f, (float)projectile.penetrate / (float)projectile.maxPenetrate);
                else damage = (int)MathHelper.Lerp(0, damage * 0.3f, (float)projectile.penetrate / (float)projectile.maxPenetrate);
            }
            /*else if (projectile.maxPenetrate > 10)
            {
                damage = (int)(damage * 0.3f);
            }
            else if (projectile.maxPenetrate > 6)
            {
                damage = (int)(damage * 0.5f);
            }
            else if (projectile.maxPenetrate > 3)
            {
                damage = (int)(damage * 0.8f);
            }*/
        }
        public virtual bool ShouldRun()
        {
            return false;
        }

        public virtual void CustomBehavior()
        {
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (npc.alpha > 150) return false;
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.2f;
            return head ? (bool?)null : false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            int frameHeight = texture.Height / Main.npcFrameCount[npc.type];
            Vector2 origin = (new Vector2(texture.Width * 0.5f, frameHeight * 0.5f)); 
            Color color = npc.GetAlpha(drawColor);
            Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition, npc.frame, color, npc.rotation, origin, npc.scale, SpriteEffects.None, 0);
            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            NPC headNPC = Main.npc[(int)npc.ai[3]];
            if (headNPC.life > headNPC.lifeMax * 0.3f)
            {
                Texture2D texture = mod.GetTexture("NPCs/Bosses/VoidLeviathan/Glow/" + GetType().Name + "_Glow");
                Rectangle frame = new Rectangle(0, texture.Height * npc.frame.Y, texture.Width, texture.Height);
                Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f); 
                SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), frame, new Color(255, 255, 255, 0), npc.rotation, origin, npc.scale, effects, 0.0f);
            }
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
    }
}