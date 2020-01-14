using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.NPCs.Bosses.VoidLeviathan.ElderShadeWyrm
{
    [AutoloadBossHead]
    class ElderShadeWyrmHead : ElderShadeWyrm
    {
        public override string Texture { get { return "ElementsAwoken/NPCs/Bosses/VoidLeviathan/ElderShadeWyrm/ElderShadeWyrmHead"; } }

        public override void SetDefaults()
        {
            base.SetDefaults();

            npc.width = 68;
            npc.height = 88;

            npc.damage = 50;
            npc.defense = 10;
            npc.knockBackResist = 0f;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;

            npc.scale = 1.1f;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;

            npc.npcSlots = 1f;
            npc.netAlways = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 65000;
            npc.damage = 90;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 75000;
                npc.damage = 150;
                npc.defense = 15;
            }
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

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.type == ProjectileID.LastPrismLaser)
            {
                damage = 20;
            }
        }
        public override void CustomBehavior()
        {
            Player P = Main.player[npc.target];
            MyPlayer modPlayer = P.GetModPlayer<MyPlayer>();
            if (!NPC.AnyNPCs(mod.NPCType("VoidLeviathanHead"))) npc.active = false;
        }
    }

    class ElderShadeWyrmBody : ElderShadeWyrm
    {
        public override string Texture { get { return "ElementsAwoken/NPCs/Bosses/VoidLeviathan/ElderShadeWyrm/ElderShadeWyrmBody"; } }

        public override void SetDefaults()
        {
            base.SetDefaults();

            npc.width = 38;
            npc.height = 50;

            npc.damage = 150;
            npc.defense = 90;
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
        public int bodyNum = 0;
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

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int num = Main.expertMode ? 2 : 3;
                if (MyWorld.awakenedMode) num = 1;
                if (bodyNum % num == 0)
                {
                    if (aiTimer == 1100 + 10 * bodyNum)
                    {
                        for (int k = 0; k < 2; k++)
                        {
                            int projDamage = Main.expertMode ? (int)(projectileBaseDamage * 1.5f) : projectileBaseDamage;
                            if (MyWorld.awakenedMode) projDamage = (int)(projectileBaseDamage * 2f);

                            float speedMult = Main.expertMode ? 9f : 7f;
                            if (MyWorld.awakenedMode) speedMult = 12f;

                            Vector2 projSpeed = new Vector2(0, 1).RotatedBy(npc.rotation + MathHelper.ToRadians(k == 0 ? 90 : 270));
                            projSpeed.Normalize();
                            projSpeed *= speedMult;

                            Projectile bolt = Main.projectile[Projectile.NewProjectile(npc.Center, projSpeed, mod.ProjectileType("VoidBolt"), projDamage, 0f, 0)];
                            bolt.Name = "Elder Shade Wyrm Bolt";
                            bolt.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                        }
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 12);
                    }
                }
            }
        }
    }

    class ElderShadeWyrmTail : ElderShadeWyrm
    {
        public override string Texture { get { return "ElementsAwoken/NPCs/Bosses/VoidLeviathan/ElderShadeWyrm/ElderShadeWyrmTail"; } }

        public override void SetDefaults()
        {
            base.SetDefaults();

            npc.width = 48;
            npc.height = 40;

            npc.damage = 150;
            npc.defense = 90;
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
    public abstract class ElderShadeWyrm : ElderShadeWyrmAI
    {
        public override void SetDefaults()
        {
            npc.lifeMax = 40000;

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
            DisplayName.SetDefault("Elder Shade Wyrm");
        }

        public override void Init()
        {
            wormLength = 20;
            tailType = NPCType<ElderShadeWyrmTail>();
            bodyType = NPCType<ElderShadeWyrmBody>();
            headType = NPCType<ElderShadeWyrmHead>();
            speed = 10f;
            if (MyWorld.awakenedMode) speed = 25f;
            else if (Main.expertMode) speed = 20f;
            turnSpeed =  0.4f;
            if (MyWorld.awakenedMode) turnSpeed = 0.8f;
            else if (Main.expertMode) turnSpeed = 0.6f;
            flies = true;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }

    public abstract class ElderShadeWyrmAI : ModNPC
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
        public bool flies = false;
        public bool directional = false;
        public float speed;
        public float turnSpeed;
        public bool tooFar = false;


        public float aiTimer = 0;
        private float wanderTimer = 0;
        private float wanderX = 0;
        private float wanderY = 0;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(aiTimer);
            writer.Write(wanderTimer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            aiTimer = reader.ReadSingle();
            wanderTimer = reader.ReadSingle();
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

            aiTimer++;
            if (aiTimer > 1100 + wormLength * 10) aiTimer = 0;
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
                        ElderShadeWyrmBody bodyNPC = (ElderShadeWyrmBody)npc.modNPC;
                        ElderShadeWyrmBody newBodyNPC = (ElderShadeWyrmBody)Main.npc[(int)npc.ai[0]].modNPC;
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
            bool collision = flies;
            if (!collision)
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
                                collision = true;
                                if (Main.rand.NextBool(100) && npc.behindTiles && Main.tile[num184, num185].nactive())
                                {
                                    WorldGen.KillTile(num184, num185, true, true, false);
                                }
                                if (Main.netMode != NetmodeID.MultiplayerClient && Main.tile[num184, num185].type == 2)
                                {
                                    ushort arg_BFCA_0 = Main.tile[num184, num185 - 1].type;
                                }
                            }
                        }
                    }
                }
            }
            if ((!collision) && head)
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
                    collision = true;
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
            Vector2 vector18 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float targetX = P.Center.X;
            float targetY = P.Center.Y;
            if (aiTimer >= 300 && aiTimer <= 1100)
            {
                speedAI *= 0.4f;
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
                if (!collision)
                {
                    npc.TargetClosest(true);
                    npc.velocity.Y = npc.velocity.Y + 0.11f;
                    if (npc.velocity.Y > speedAI)
                    {
                        npc.velocity.Y = speedAI;
                    }
                    if ((double)(System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y)) < (double)speedAI * 0.4)
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X = npc.velocity.X - turnSpeed * 1.1f;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X + turnSpeed * 1.1f;
                        }
                    }
                    else if (npc.velocity.Y == speedAI)
                    {
                        if (npc.velocity.X < targetX)
                        {
                            npc.velocity.X = npc.velocity.X + turnSpeed;
                        }
                        else if (npc.velocity.X > targetX)
                        {
                            npc.velocity.X = npc.velocity.X - turnSpeed;
                        }
                    }
                    else if (npc.velocity.Y > 4f)
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X = npc.velocity.X + turnSpeed * 0.9f;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X - turnSpeed * 0.9f;
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
                    num193 = (float)System.Math.Sqrt((double)(targetX * targetX + targetY * targetY));
                    float num196 = System.Math.Abs(targetX);
                    float num197 = System.Math.Abs(targetY);
                    float num198 = speedAI / num193;
                    targetX *= num198;
                    targetY *= num198;
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
                            if (Main.netMode != NetmodeID.MultiplayerClient && (double)(npc.position.Y / 16f) > (Main.rockLayer + (double)Main.maxTilesY) / 2.0)
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
                            targetX = 0f;
                            targetY = speedAI;
                        }
                    }

                    if (aiTimer >= 300 && aiTimer <= 1100)
                    {
                        Wander(P);                      
                    }
                    if (aiTimer <= 1100)
                    {
                        int xTurnSpeedScale = 1;
                        if (MathHelper.Distance(npc.Center.X, P.Center.X) > 3000)
                        {
                            xTurnSpeedScale = 5;
                            targetX *= 15f;
                        }
                        if (npc.velocity.X > 0f && targetX > 0f || npc.velocity.X < 0f && targetX < 0f || npc.velocity.Y > 0f && targetY > 0f || npc.velocity.Y < 0f && targetY < 0f)
                        {
                            if (npc.velocity.X < targetX)
                            {
                                npc.velocity.X = npc.velocity.X + turnSpeed * xTurnSpeedScale;
                            }
                            else
                            {
                                if (npc.velocity.X > targetX)
                                {
                                    npc.velocity.X = npc.velocity.X - turnSpeed * xTurnSpeedScale;
                                }
                            }
                            if (npc.velocity.Y < targetY)
                            {
                                npc.velocity.Y = npc.velocity.Y + turnSpeed;
                            }
                            else
                            {
                                if (npc.velocity.Y > targetY)
                                {
                                    npc.velocity.Y = npc.velocity.Y - turnSpeed;
                                }
                            }
                            if ((double)System.Math.Abs(targetY) < (double)speedAI * 0.2 && (npc.velocity.X > 0f && targetX < 0f || npc.velocity.X < 0f && targetX > 0f))
                            {
                                if (npc.velocity.Y > 0f)
                                {
                                    npc.velocity.Y = npc.velocity.Y + turnSpeed * 2f;
                                }
                                else
                                {
                                    npc.velocity.Y = npc.velocity.Y - turnSpeed * 2f;
                                }
                            }
                            if ((double)System.Math.Abs(targetX) < (double)speedAI * 0.2 && (npc.velocity.Y > 0f && targetY < 0f || npc.velocity.Y < 0f && targetY > 0f))
                            {
                                if (npc.velocity.X > 0f)
                                {
                                    npc.velocity.X = npc.velocity.X + turnSpeed * 2f;
                                }
                                else
                                {
                                    npc.velocity.X = npc.velocity.X - turnSpeed * 2f;
                                }
                            }
                        }
                        else
                        {
                            if (num196 > num197)
                            {
                                if (npc.velocity.X < targetX)
                                {
                                    npc.velocity.X = npc.velocity.X + turnSpeed * 1.1f;
                                }
                                else if (npc.velocity.X > targetX)
                                {
                                    npc.velocity.X = npc.velocity.X - turnSpeed * 1.1f;
                                }
                                if ((double)(System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y)) < (double)speedAI * 0.5)
                                {
                                    if (npc.velocity.Y > 0f)
                                    {
                                        npc.velocity.Y = npc.velocity.Y + turnSpeed;
                                    }
                                    else
                                    {
                                        npc.velocity.Y = npc.velocity.Y - turnSpeed;
                                    }
                                }
                            }
                            else
                            {
                                if (npc.velocity.Y < targetY)
                                {
                                    npc.velocity.Y = npc.velocity.Y + turnSpeed * 1.1f;
                                }
                                else if (npc.velocity.Y > targetY)
                                {
                                    npc.velocity.Y = npc.velocity.Y - turnSpeed * 1.1f;
                                }
                                if ((double)(System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y)) < (double)speedAI * 0.5)
                                {
                                    if (npc.velocity.X > 0f)
                                    {
                                        npc.velocity.X = npc.velocity.X + turnSpeed;
                                    }
                                    else
                                    {
                                        npc.velocity.X = npc.velocity.X - turnSpeed;
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        npc.velocity *= 0.96f;
                    }
                }
                npc.rotation = (float)System.Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f;
                if (head)
                {
                    if (collision)
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
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(mod.BuffType("ExtinctionCurse"), 90, true);
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
            else if (projectile.maxPenetrate > 10)
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
            }
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
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
    }
}