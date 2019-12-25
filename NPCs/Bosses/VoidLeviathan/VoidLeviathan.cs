using Microsoft.Xna.Framework;
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

            npc.width = 48;
            npc.height = 60;

            npc.lifeMax = 850000;
            npc.damage = 350;
            npc.defense = 10;
            npc.knockBackResist = 0f;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;

            npc.scale = 1.1f;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;

            npc.value = Item.buyPrice(1, 50, 0, 0);
            npc.npcSlots = 1f;
            music = MusicID.LunarBoss;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/VoidLeviathanTheme");
            npc.netAlways = true;

            npc.takenDamageMultiplier = 4f;
            bossBag = mod.ItemType("VoidLeviathanBag");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 1400000;
            npc.damage = 450;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 1750000;
                npc.damage = 999;
                npc.defense = 15;
            }
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidLeviathanTrophy"));
            }
            if (Main.expertMode)
            {
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidCrystal"));
                }
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                int choice = Main.rand.Next(8);
                switch (choice)
                {
                    case 0:
                        choice = mod.ItemType("VoidInferno");
                        break;
                    case 1:
                        choice = mod.ItemType("EndlessAbyssBlaster");
                        break;
                    case 2:
                        choice = mod.ItemType("ExtinctionBow");
                        break;
                    case 3:
                        choice = mod.ItemType("Reaperstorm");
                        break;
                    case 4:
                        choice = mod.ItemType("BladeOfTheNight");
                        break;
                    case 5:
                        choice = mod.ItemType("CosmicWrath");
                        break;
                    case 6:
                        choice = mod.ItemType("PikeOfEternalDespair");
                        break;
                    case 7:
                        choice = mod.ItemType("VoidLeviathanAegis");
                        break;
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, choice);
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidLeviathanMask"));
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidLeviathanHeart"), 1);
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEssence"), Main.rand.Next(5, 15));
            if (!MyWorld.downedVoidLeviathan)
            {
                MyWorld.encounter3 = true;
                MyWorld.encounterTimer = 3600;
            }
            MyWorld.downedVoidLeviathan = true;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = mod.ItemType("EpicHealingPotion");
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.2f;
            return null;
        }
        public override void Init()
        {
            base.Init();
            head = true;
        }

        private int attackCounter;
        private int roarTimer;
        private bool[] speech = new bool[3];
        private bool spawnedSouls = false;
        private int projectileBaseDamage = 100;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
            writer.Write(roarTimer);
            writer.Write(spawnedSouls);
            writer.Write(speech[0]);
            writer.Write(speech[1]);
            writer.Write(speech[2]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
            roarTimer = reader.ReadInt32();
            spawnedSouls = reader.ReadBoolean();
            speech[0] = reader.ReadBoolean();
            speech[1] = reader.ReadBoolean();
            speech[2] = reader.ReadBoolean();
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.type == ProjectileID.LastPrismLaser)
            {
                damage = 20;
                if (!speech[0])
                {
                    Main.NewText("A prism? I feast upon light!", Color.Purple.R, Color.Purple.G, Color.Purple.B);
                    speech[0] = true;
                }
            }
        }
        public override void CustomBehavior()
        {
            Player P = Main.player[npc.target];

            if (P.dead)
            {
                npc.velocity.Y = npc.velocity.Y + 0.11f;
                npc.localAI[3]++;
                if (npc.localAI[3] >= 300)
                {
                    npc.active = false;
                }
            }

            // speech
            if (Main.dayTime && !speech[1])
            {
                Main.NewText("Times up buddy, I'll tear you apart", Color.Purple.R, Color.Purple.G, Color.Purple.B);
                speed += 5;
                npc.damage += 50;
                speech[1] = true;
            }
            if (npc.life <= npc.lifeMax * 0.4f && !speech[2])
            {
                npc.damage += 25;
                speed += 5;
                Main.NewText("Ill feast upon your soul just like I did with the countless others who challenged me", Color.Purple.R, Color.Purple.G, Color.Purple.B);
                speech[2] = true;
            }
            if (npc.localAI[2] == 0)
            {
                if (Main.autoPause == true)
                {
                    Main.NewText("Using autopause huh? What a wimp", Color.LightCyan.R, Color.LightCyan.G, Color.LightCyan.B);
                }
                npc.localAI[2]++;
            }

            if (!spawnedSouls)
            {
                int soulCount = Main.expertMode ? 7 : 5;
                for (int l = 0; l < soulCount; l++)
                {
                    //cos = y, sin = x
                    int distance = 360 / soulCount;
                    NPC.NewNPC((int)(npc.Center.X + (Math.Sin(l * distance) * 150)), (int)(npc.Center.Y + (Math.Cos(l * distance) * 150)), mod.NPCType("BarrenSoul"), npc.whoAmI, l * distance, npc.whoAmI);
                }
                spawnedSouls = true;
            }

            if (Main.netMode != 1)
            {
                roarTimer--;
                if (roarTimer <= 0)
                {
                    Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 10);
                    roarTimer = 300 + Main.rand.Next(1, 400);
                }
                attackCounter++;
                if (attackCounter >= 500)
                {
                    int numProj = Main.expertMode ? 50 : 40;
                    for (int i = 0; i < numProj; ++i)
                    {
                        Projectile.NewProjectile(P.Center.X + Main.rand.Next(-4000, 4000), P.Center.Y + 1500, 0f, -8f, mod.ProjectileType("VoidStrike"), projectileBaseDamage, 0f);
                        Projectile.NewProjectile(P.Center.X + Main.rand.Next(-4000, 4000), P.Center.Y - 1500, 0f, 8f, mod.ProjectileType("VoidStrike"), projectileBaseDamage, 0f);
                    }
                    attackCounter = 0;
                }
            }
        }
    }

    class VoidLeviathanBody : VoidLeviathan
    {
        public override string Texture { get { return "ElementsAwoken/NPCs/Bosses/VoidLeviathan/VoidLeviathanBody"; } }

        public override void SetDefaults()
        {
            base.SetDefaults();

            npc.width = 60;
            npc.height = 50;

            npc.damage = 150;
            npc.defense = 9999;
            npc.lifeMax = 1;
            npc.knockBackResist = 0.0f;

            npc.scale = 1.1f;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;

            npc.behindTiles = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.noGravity = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 175;
            if (MyWorld.awakenedMode)
            {
                npc.damage = 200;
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

            if (Main.netMode != 1)
            {
                attackCounter++;
                if (attackCounter >= 1100)
                {
                    attackCounter = 0;
                }
                if (attackCounter >= 600 && attackCounter <= 1100)
                {
                    if (!Main.dayTime)
                    {
                        if (Main.rand.Next(1000) == 0)
                        {
                            VoidBolt(P, 10f, projectileBaseDamage);
                        }
                    }
                    else
                    {
                        if (Main.rand.Next(800) == 0)
                        {
                            VoidBolt(P, 14f, projectileBaseDamage * 2);
                        }
                    }
                }
            }
        }
        private void VoidBolt(Player P, float speed, int damage)
        {
            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 12);
            float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), mod.ProjectileType("VoidBolt"), damage, 0f, 0);
        }
    }

    class VoidLeviathanBodyWeak : VoidLeviathanBody
    {
        public override string Texture { get { return "ElementsAwoken/NPCs/Bosses/VoidLeviathan/VoidLeviathanBodyWeak"; } }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.defense = 1;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 175;
            if (MyWorld.awakenedMode)
            {
                npc.damage = 200;
            }
        }
    }

    class VoidLeviathanTail : VoidLeviathan
    {
        public override string Texture { get { return "ElementsAwoken/NPCs/Bosses/VoidLeviathan/VoidLeviathanTail"; } }

        public override void SetDefaults()
        {
            base.SetDefaults();

            npc.width = 80;
            npc.height = 40;

            npc.damage = 150;
            npc.defense = 9999;
            npc.lifeMax = 1;
            npc.knockBackResist = 0.0f;

            npc.scale = 1.1f;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;

            npc.behindTiles = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.noGravity = true;
            npc.dontCountMe = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 175;
            if (MyWorld.awakenedMode)
            {
                npc.damage = 200;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void Init()
        {
            base.Init();
            tail = true;
        }
    }

    // I made this 2nd base class to limit code repetition.
    public abstract class VoidLeviathan : Worm
    {
        public override void SetDefaults()
        {
            // all EA modded buffs (unless i forget to add new ones)
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("ExtinctionCurse")] = true;
            npc.buffImmune[mod.BuffType("HandsOfDespair")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;
            npc.buffImmune[mod.BuffType("AncientDecay")] = true;
            npc.buffImmune[mod.BuffType("SoulInferno")] = true;
            npc.buffImmune[mod.BuffType("DragonFire")] = true;
            npc.buffImmune[mod.BuffType("Discord")] = true;
            // all vanilla buffs
            for (int num2 = 0; num2 < 206; num2++)
            {
                npc.buffImmune[num2] = true;
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Leviathan");
        }

        public override void Init()
        {
            minLength = 100;
            maxLength = 110;
            tailType = NPCType<VoidLeviathanTail>();
            bodyType = NPCType<VoidLeviathanBody>();
            headType = NPCType<VoidLeviathanHead>();
            speed = Main.expertMode ? 90f : 70f;
            turnSpeed = Main.expertMode ? 0.8f : 0.6f;
        }
    }

    //ported from my tAPI mod because I'm lazy
    // This abstract class can be used for non splitting worm type NPC.
    public abstract class Worm : ModNPC
    {
        /* ai[0] = follower
		 * ai[1] = following
		 * ai[2] = distanceFromTail
		 * ai[3] = head
		 */
        public bool head;
        public bool tail;
        public int minLength;
        public int maxLength;
        public int headType;
        public int bodyType;
        public int tailType;
        public bool flies = false;
        public bool directional = false;
        public float speed;
        public float turnSpeed;
        public bool tooFar = false;
        public override void AI()
        {
            if (npc.localAI[1] == 0f)
            {
                npc.localAI[1] = 1f;
                Init();
            }
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
            if (Main.netMode != 1)
            {
                if (!tail && npc.ai[0] == 0f)
                {
                    if (head)
                    {
                        npc.ai[3] = (float)npc.whoAmI;
                        npc.realLife = npc.whoAmI;
                        npc.ai[2] = (float)Main.rand.Next(minLength, maxLength + 1);
                        int bodyNPC = NPCType<VoidLeviathanBody>();
                        //if (Main.rand.Next(3) == 0) bodyNPC = mod.NPCType<VoidLeviathanBodyWeak>();
                        npc.ai[0] = (float)NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), bodyNPC, npc.whoAmI);
                    }
                    else if (npc.ai[2] > 0f)
                    {
                        npc.ai[0] = (float)NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), npc.type, npc.whoAmI);
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
            int num180 = (int)(npc.position.X / 16f) - 1;
            int num181 = (int)((npc.position.X + (float)npc.width) / 16f) + 2;
            int num182 = (int)(npc.position.Y / 16f) - 1;
            int num183 = (int)((npc.position.Y + (float)npc.height) / 16f) + 2;
            if (num180 < 0)
            {
                num180 = 0;
            }
            if (num181 > Main.maxTilesX)
            {
                num181 = Main.maxTilesX;
            }
            if (num182 < 0)
            {
                num182 = 0;
            }
            if (num183 > Main.maxTilesY)
            {
                num183 = Main.maxTilesY;
            }
            bool flag18 = flies;
            if (!flag18)
            {
                for (int num184 = num180; num184 < num181; num184++)
                {
                    for (int num185 = num182; num185 < num183; num185++)
                    {
                        if (Main.tile[num184, num185] != null && (Main.tile[num184, num185].nactive() && (Main.tileSolid[(int)Main.tile[num184, num185].type] || Main.tileSolidTop[(int)Main.tile[num184, num185].type] && Main.tile[num184, num185].frameY == 0) || Main.tile[num184, num185].liquid > 64))
                        {
                            Vector2 vector17;
                            vector17.X = (float)(num184 * 16);
                            vector17.Y = (float)(num185 * 16);
                            if (npc.position.X + (float)npc.width > vector17.X && npc.position.X < vector17.X + 16f && npc.position.Y + (float)npc.height > vector17.Y && npc.position.Y < vector17.Y + 16f)
                            {
                                flag18 = true;
                                if (Main.rand.NextBool(100) && npc.behindTiles && Main.tile[num184, num185].nactive())
                                {
                                    WorldGen.KillTile(num184, num185, true, true, false);
                                }
                                if (Main.netMode != 1 && Main.tile[num184, num185].type == 2)
                                {
                                    ushort arg_BFCA_0 = Main.tile[num184, num185 - 1].type;
                                }
                            }
                        }
                    }
                }
            }
            if (Vector2.Distance(npc.Center, Main.player[npc.target].Center) <= 750)
            {
                tooFar = false;
            }
            else
            {
                tooFar = true;
            }
            if ((!flag18 || tooFar) && head)
            {
                Rectangle rectangle = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
                int num186 = 1000;
                bool flag19 = true;
                for (int num187 = 0; num187 < 255; num187++)
                {
                    if (Main.player[num187].active)
                    {
                        Rectangle rectangle2 = new Rectangle((int)Main.player[num187].position.X - num186, (int)Main.player[num187].position.Y - num186, num186 * 2, num186 * 2);
                        if (rectangle.Intersects(rectangle2))
                        {
                            flag19 = false;
                            break;
                        }
                    }
                }
                if (flag19)
                {
                    flag18 = true;
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
            float num188 = speed;
            float num189 = turnSpeed;
            Vector2 vector18 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float num191 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2);
            float num192 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2);
            num191 = (float)((int)(num191 / 16f) * 16);
            num192 = (float)((int)(num192 / 16f) * 16);
            vector18.X = (float)((int)(vector18.X / 16f) * 16);
            vector18.Y = (float)((int)(vector18.Y / 16f) * 16);
            num191 -= vector18.X;
            num192 -= vector18.Y;
            float num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
            if (npc.ai[1] > 0f && npc.ai[1] < (float)Main.npc.Length)
            {
                try
                {
                    vector18 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    num191 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - vector18.X;
                    num192 = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - vector18.Y;
                }
                catch
                {
                }
                npc.rotation = (float)System.Math.Atan2((double)num192, (double)num191) + 1.57f;
                num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
                int num194 = npc.width;
                num193 = (num193 - (float)num194) / num193;
                num191 *= num193;
                num192 *= num193;
                npc.velocity = Vector2.Zero;
                npc.position.X = npc.position.X + num191;
                npc.position.Y = npc.position.Y + num192;
                if (directional)
                {
                    if (num191 < 0f)
                    {
                        npc.spriteDirection = 1;
                    }
                    if (num191 > 0f)
                    {
                        npc.spriteDirection = -1;
                    }
                }
            }
            else
            {
                if (!flag18)
                {
                    npc.TargetClosest(true);
                    npc.velocity.Y = npc.velocity.Y + 0.11f;
                    if (npc.velocity.Y > num188)
                    {
                        npc.velocity.Y = num188;
                    }
                    if ((double)(System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y)) < (double)num188 * 0.4)
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X = npc.velocity.X - num189 * 1.1f;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X + num189 * 1.1f;
                        }
                    }
                    else if (npc.velocity.Y == num188)
                    {
                        if (npc.velocity.X < num191)
                        {
                            npc.velocity.X = npc.velocity.X + num189;
                        }
                        else if (npc.velocity.X > num191)
                        {
                            npc.velocity.X = npc.velocity.X - num189;
                        }
                    }
                    else if (npc.velocity.Y > 4f)
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X = npc.velocity.X + num189 * 0.9f;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X - num189 * 0.9f;
                        }
                    }
                }
                else
                {
                    if (!flies && npc.behindTiles && npc.soundDelay == 0)
                    {
                        float num195 = num193 / 40f;
                        if (num195 < 10f)
                        {
                            num195 = 10f;
                        }
                        if (num195 > 20f)
                        {
                            num195 = 20f;
                        }
                        npc.soundDelay = (int)num195;
                        Main.PlaySound(SoundID.Roar, npc.position, 1);
                    }
                    num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
                    float num196 = System.Math.Abs(num191);
                    float num197 = System.Math.Abs(num192);
                    float num198 = num188 / num193;
                    num191 *= num198;
                    num192 *= num198;
                    if (ShouldRun())
                    {
                        bool flag20 = true;
                        for (int num199 = 0; num199 < 255; num199++)
                        {
                            if (Main.player[num199].active && !Main.player[num199].dead && Main.player[num199].ZoneCorrupt)
                            {
                                flag20 = false;
                            }
                        }
                        if (flag20)
                        {
                            if (Main.netMode != 1 && (double)(npc.position.Y / 16f) > (Main.rockLayer + (double)Main.maxTilesY) / 2.0)
                            {
                                npc.active = false;
                                int num200 = (int)npc.ai[0];
                                while (num200 > 0 && num200 < 200 && Main.npc[num200].active && Main.npc[num200].aiStyle == npc.aiStyle)
                                {
                                    int num201 = (int)Main.npc[num200].ai[0];
                                    Main.npc[num200].active = false;
                                    npc.life = 0;
                                    if (Main.netMode == 2)
                                    {
                                        NetMessage.SendData(23, -1, -1, null, num200, 0f, 0f, 0f, 0, 0, 0);
                                    }
                                    num200 = num201;
                                }
                                if (Main.netMode == 2)
                                {
                                    NetMessage.SendData(23, -1, -1, null, npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                                }
                            }
                            num191 = 0f;
                            num192 = num188;
                        }
                    }
                    bool flag21 = false;
                    if (npc.type == 87) // wyvern
                    {
                        if ((npc.velocity.X > 0f && num191 < 0f || npc.velocity.X < 0f && num191 > 0f || npc.velocity.Y > 0f && num192 < 0f || npc.velocity.Y < 0f && num192 > 0f) && System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y) > num189 / 2f && num193 < 300f)
                        {
                            flag21 = true;
                            if (System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y) < num188)
                            {
                                npc.velocity *= 1.1f;
                            }
                        }
                        if (npc.position.Y > Main.player[npc.target].position.Y || (double)(Main.player[npc.target].position.Y / 16f) > Main.worldSurface || Main.player[npc.target].dead)
                        {
                            flag21 = true;
                            if (System.Math.Abs(npc.velocity.X) < num188 / 2f)
                            {
                                if (npc.velocity.X == 0f)
                                {
                                    npc.velocity.X = npc.velocity.X - (float)npc.direction;
                                }
                                npc.velocity.X = npc.velocity.X * 1.1f;
                            }
                            else
                            {
                                if (npc.velocity.Y > -num188)
                                {
                                    npc.velocity.Y = npc.velocity.Y - num189;
                                }
                            }
                        }
                    }
                    if (!flag21)
                    {
                        if (npc.velocity.X > 0f && num191 > 0f || npc.velocity.X < 0f && num191 < 0f || npc.velocity.Y > 0f && num192 > 0f || npc.velocity.Y < 0f && num192 < 0f)
                        {
                            if (npc.velocity.X < num191)
                            {
                                npc.velocity.X = npc.velocity.X + num189;
                            }
                            else
                            {
                                if (npc.velocity.X > num191)
                                {
                                    npc.velocity.X = npc.velocity.X - num189;
                                }
                            }
                            if (npc.velocity.Y < num192)
                            {
                                npc.velocity.Y = npc.velocity.Y + num189;
                            }
                            else
                            {
                                if (npc.velocity.Y > num192)
                                {
                                    npc.velocity.Y = npc.velocity.Y - num189;
                                }
                            }
                            if ((double)System.Math.Abs(num192) < (double)num188 * 0.2 && (npc.velocity.X > 0f && num191 < 0f || npc.velocity.X < 0f && num191 > 0f))
                            {
                                if (npc.velocity.Y > 0f)
                                {
                                    npc.velocity.Y = npc.velocity.Y + num189 * 2f;
                                }
                                else
                                {
                                    npc.velocity.Y = npc.velocity.Y - num189 * 2f;
                                }
                            }
                            if ((double)System.Math.Abs(num191) < (double)num188 * 0.2 && (npc.velocity.Y > 0f && num192 < 0f || npc.velocity.Y < 0f && num192 > 0f))
                            {
                                if (npc.velocity.X > 0f)
                                {
                                    npc.velocity.X = npc.velocity.X + num189 * 2f;
                                }
                                else
                                {
                                    npc.velocity.X = npc.velocity.X - num189 * 2f;
                                }
                            }
                        }
                        else
                        {
                            if (num196 > num197)
                            {
                                if (npc.velocity.X < num191)
                                {
                                    npc.velocity.X = npc.velocity.X + num189 * 1.1f;
                                }
                                else if (npc.velocity.X > num191)
                                {
                                    npc.velocity.X = npc.velocity.X - num189 * 1.1f;
                                }
                                if ((double)(System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y)) < (double)num188 * 0.5)
                                {
                                    if (npc.velocity.Y > 0f)
                                    {
                                        npc.velocity.Y = npc.velocity.Y + num189;
                                    }
                                    else
                                    {
                                        npc.velocity.Y = npc.velocity.Y - num189;
                                    }
                                }
                            }
                            else
                            {
                                if (npc.velocity.Y < num192)
                                {
                                    npc.velocity.Y = npc.velocity.Y + num189 * 1.1f;
                                }
                                else if (npc.velocity.Y > num192)
                                {
                                    npc.velocity.Y = npc.velocity.Y - num189 * 1.1f;
                                }
                                if ((double)(System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y)) < (double)num188 * 0.5)
                                {
                                    if (npc.velocity.X > 0f)
                                    {
                                        npc.velocity.X = npc.velocity.X + num189;
                                    }
                                    else
                                    {
                                        npc.velocity.X = npc.velocity.X - num189;
                                    }
                                }
                            }
                        }
                    }
                }
                npc.rotation = (float)System.Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f;
                if (head)
                {
                    if (flag18)
                    {
                        if (npc.localAI[0] != 1f)
                        {
                            npc.netUpdate = true;
                        }
                        npc.localAI[0] = 1f;
                    }
                    else
                    {
                        if (npc.localAI[0] != 0f)
                        {
                            npc.netUpdate = true;
                        }
                        npc.localAI[0] = 0f;
                    }
                    if ((npc.velocity.X > 0f && npc.oldVelocity.X < 0f || npc.velocity.X < 0f && npc.oldVelocity.X > 0f || npc.velocity.Y > 0f && npc.oldVelocity.Y < 0f || npc.velocity.Y < 0f && npc.oldVelocity.Y > 0f) && !npc.justHit)
                    {
                        npc.netUpdate = true;
                        return;
                    }
                }
            }
            CustomBehavior();
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(mod.BuffType("ExtinctionCurse"), 300, true);
        }
        public virtual void Init()
        {
        }

        public virtual bool ShouldRun()
        {
            return false;
        }

        public virtual void CustomBehavior()
        {
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return head ? (bool?)null : false;
        }
    }
}