using ElementsAwoken.Buffs.Debuffs;
using ElementsAwoken.Items.BossDrops.Permafrost;
using ElementsAwoken.Items.Essence;
using ElementsAwoken.Projectiles.NPCProj.Permafrost;
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
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.NPCs.Bosses.Permafrost
{
    [AutoloadBossHead]
    public class Permafrost : ModNPC
    {
        private int projectileBaseDamage = 50;
        //shockwave
        private int rippleCount = 2;
        private int rippleSize = 15;
        private int rippleSpeed = 30;
        private float distortStrength = 600f;


        private float storePosX = 0;
        private float storePosY = 0;
        private float enrageTimer = 0;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(storePosX);
            writer.Write(storePosY);
            writer.Write(enrageTimer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            storePosX = reader.ReadSingle();
            storePosY = reader.ReadSingle();
            enrageTimer = reader.ReadSingle();
        }
        /* int shootTimer = 0;

         int portalTimer = 600;
         int minionTimer = 300;

         bool canSpawnOrbitals = true;
         int phase = 1;

         bool enraged = false;
         int enrageTimer = 0;
         bool lowLife = false;
         bool animate = true;

         float spinAI = 0f;

         int projectileBaseDamage = 60;
         */

        private float aiTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float state
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float shockwave
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float shootTimer
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 152;
            npc.height = 158;

            npc.lifeMax = 40000;
            npc.damage = 80;
            npc.defense = 36;
            npc.knockBackResist = 0f;

            npc.aiStyle = -1;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;

            npc.HitSound = SoundID.NPCHit52;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(0, 20, 0, 0);
            music = MusicID.Boss3;
            bossBag = ItemType<PermafrostBag>();

            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[BuffType<IceBound>()] = true;
            npc.buffImmune[BuffType<EndlessTears>()] = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Permafrost");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 65000;
            npc.damage = 160;
            npc.defense = 38;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 80000;
                npc.damage = 190;
                npc.defense = 42;
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
            if (npc.frame.Y > frameHeight * 3)
            {
                npc.frame.Y = 0;
            }
        }
    
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Frostburn, 180, false);
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<PermafrostTrophy>());
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<PermafrostMask>());
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                int choice = Main.rand.Next(4);
                if (choice == 0)Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<IceReaver>());
                else if (choice == 1) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Snowdrift>());
                else if(choice == 2) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<IceWrath>());
                else if (choice == 3)  Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Flurry>());
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<FrostEssence>(), Main.rand.Next(5, 25));
            if (!MyWorld.downedPermafrost)
            {
                ElementsAwoken.encounter = 2;
                ElementsAwoken.encounterTimer = 3600;
                ElementsAwoken.DebugModeText("encounter 2 start");
            }
            MyWorld.downedPermafrost = true;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
            Filters.Scene["Shockwave"].Deactivate();
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (state == 4 && Main.netMode == NetmodeID.SinglePlayer)
            {
                if (shockwave == 0)
                {
                    if (!Filters.Scene["Shockwave"].IsActive())
                    {
                        Filters.Scene.Activate("Shockwave", npc.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(npc.Center);
                    }
                }
                else
                {
                    shockwave++;
                    float progress = shockwave / 30f;
                    Filters.Scene["Shockwave"].GetShader().UseProgress(progress).UseOpacity(distortStrength * (1 - progress / 3f));
                }
                if (aiTimer == 180) Filters.Scene["Shockwave"].Deactivate();
            }
        }
        public override void AI()
        {
            npc.TargetClosest(true);

            Lighting.AddLight(npc.Center, 1f, 1f, 1f);

            Player P = Main.player[npc.target];
            // despawn
            if (!P.active || P.dead)
            {
                npc.TargetClosest(true);
                if (!P.active || P.dead)
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
            if (!P.ZoneSnow)
            {
                if (enrageTimer < 600) enrageTimer++;
            }
            if (P.ZoneSnow)
            {
                if (enrageTimer > 0) enrageTimer--;
            }
            bool enraged = false;
            if (enrageTimer >= 600) enraged = true;

            if (state == 0)
            {
                aiTimer++;
                shootTimer++;
                if (aiTimer < 300) FlyTo(new Vector2(P.Center.X - 200, P.Center.Y - 200), 0.1f, 14f);
                else FlyTo(new Vector2(P.Center.X + 200, P.Center.Y - 200), 0.1f, 14f);
                int shootRate = enraged ? 3 : 6;
                if (shootTimer % shootRate == 0 && shootTimer < 30 && shootTimer >= 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                    int proj = -1;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 pos = npc.Center + Main.rand.NextVector2Square(-150, 150);
                        float Speed = 12f;
                        float rotation = (float)Math.Atan2(pos.Y - P.Center.Y, pos.X - P.Center.X);
                        Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        proj = Projectile.NewProjectile(pos, projSpeed, ProjectileType<PermafrostIcicle>(), projectileBaseDamage, 0f, Main.myPlayer);
                    }
                    if (proj != -1)
                    {
                        Projectile ice = Main.projectile[proj];
                        if (!GetInstance<Config>().lowDust)
                        {
                            int numDusts = 20;
                            for (int p = 0; p < numDusts; p++)
                            {
                                Vector2 position = (Vector2.One * new Vector2((float)ice.width / 2f, (float)ice.height) * 0.3f * 0.5f).RotatedBy((double)((float)(p - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + ice.Center;
                                Vector2 velocity = position - ice.Center;
                                int dust = Dust.NewDust(position + velocity, 0, 0, 135, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                                Main.dust[dust].noGravity = true;
                                Main.dust[dust].velocity = Vector2.Normalize(velocity) * 2f;
                            }
                        }
                    }
                }
                if (shootTimer > 75) shootTimer = 0;
                if (aiTimer > 600)
                {
                    aiTimer = 2;
                    shootTimer = 0;
                    state++;
                }
            }
            else if (state == 1)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                    float speed = 5f;
                    float numberProjectiles = Main.expertMode ? MyWorld.awakenedMode ? 12 : 8 : 6;
                    float rotation = MathHelper.ToRadians(360);
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = Vector2.One.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * speed;
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<IceMagic>(), (int)(projectileBaseDamage * 1.2f), 2f, Main.myPlayer);
                    }
                }
                state = aiTimer;
                aiTimer = 0;
                shootTimer = 0;
            }
            else if (state == 2)
            {
                aiTimer++;
                if (shootTimer == 0)
                {
                    Teleport(P.Center - new Vector2(0, 300));
                    npc.velocity = Vector2.Zero;
                }
                else if (shootTimer > 20)
                {
                    npc.velocity.Y = 20;
                }
                Tile tileTest = Framing.GetTileSafely((int)(npc.Bottom.X / 16), (int)(npc.Bottom.Y / 16));
                shootTimer++;
                if ((tileTest.active() && Main.tileSolid[tileTest.type] && !TileID.Sets.Platforms[tileTest.type] && tileTest.type != TileID.PlanterBox && shootTimer > 30) || shootTimer > 60)
                {
                    shootTimer = 0;

                    for (int i = 0; i < 20; i++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(new Vector2(npc.position.X, npc.Bottom.Y - 16), npc.width, 16, 135, 0f, 0f, 100, default(Color), 2.5f)];
                        dust.noGravity = true;
                        if (dust.position.X < npc.Center.X) dust.velocity.X = Main.rand.NextFloat(0.8f, 1.2f) * -6f;
                        else dust.velocity.X = Main.rand.NextFloat(0.8f, 1.2f) * 6f;
                        dust.velocity.Y = Main.rand.NextFloat(-10, -2);
                    }
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 122);
                }
                if (aiTimer > 300)
                {
                    aiTimer = 0;
                    shootTimer = 0;
                    state++;
                    if (npc.life > npc.lifeMax * 0.5f) state++;
                    Teleport(P.Center - new Vector2(0, 300));
                    npc.velocity = Vector2.Zero;
                }
            }
            else if (state == 3)
            {
                FlyTo(new Vector2(P.Center.X, P.Center.Y ), 0.1f, 14f);
                shootTimer--;
                if (shootTimer <= 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);

                    float speed = 5f;
                    float numberProjectiles = MyWorld.awakenedMode ? 3 : 2;
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        int proj = -1;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);

                            proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), ProjectileType<HomingIce>(), projectileBaseDamage, 2f, Main.myPlayer);
                        }
                        if (proj != -1)
                        {
                            Projectile ice = Main.projectile[proj];
                            int distance = (int)((npc.width / 2) * 0.8f);
                            float rad = (MathHelper.ToRadians(360) / numberProjectiles) * i;
                            ice.position.X = npc.Center.X - (int)(Math.Cos(rad) * distance) - ice.width / 2;
                            ice.position.Y = npc.Center.Y - (int)(Math.Sin(rad) * distance) - ice.height / 2;
                            if (!GetInstance<Config>().lowDust)
                            {
                                int numDusts = 20;
                                for (int p = 0; p < numDusts; p++)
                                {
                                    Vector2 position = (Vector2.One * new Vector2((float)ice.width / 2f, (float)ice.height) * 0.3f * 0.5f).RotatedBy((double)((float)(p - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + ice.Center;
                                    Vector2 velocity = position - ice.Center;
                                    int dust = Dust.NewDust(position + velocity, 0, 0, 135, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                                    Main.dust[dust].noGravity = true;
                                    Main.dust[dust].velocity = Vector2.Normalize(velocity) * 2f;
                                }
                            }
                        }
                    }
                    shootTimer = 45;
                    aiTimer++;
                }
                if (aiTimer > 3)
                {
                    aiTimer = 0;
                    shootTimer = 0;
                    state++;
                }
            }
            else if (state == 4)
            {
                aiTimer++;
                if (shockwave == 0)
                {
                    Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 10);
                    shockwave = 1;              
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int numProj = Main.expertMode ? MyWorld.awakenedMode ? 35 : 25 : 15;
                        float speed = Main.expertMode ? MyWorld.awakenedMode ? 7f : 5f : 3f;
                        for (int i = 0; i < numProj; i++)
                        {
                            Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - Main.rand.Next(800, 1800), 0, speed, ProjectileType<IceRain>(), projectileBaseDamage, 0f, Main.myPlayer)];
                            proj.rotation = Main.rand.NextFloat((float)Math.PI * 2);
                        }
                    }
                }
                if (aiTimer > 300)
                {
                    shockwave = 0;
                    aiTimer = 0;
                    shootTimer = 0;
                    state++;
                }
            }
            else if (state == 5)
            {
                float dashSpeed = Main.expertMode ? MyWorld.awakenedMode ? 13 : 10 : 8;
                int tpDist = 700;
                if (npc.life < npc.lifeMax * 0.75f) tpDist -= 100;
                if (npc.life < npc.lifeMax * 0.5f) tpDist -= 100;
                if (npc.life < npc.lifeMax * 0.25f) tpDist -= 100;
                aiTimer++;
                if (shootTimer == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC other = Main.npc[i];
                        if (other.type == NPCType<PermaOrbital>() && other.ai[0] == npc.whoAmI && other.active)
                        {
                            other.active = false;
                        }
                    }
                    int orbitalcount = Main.expertMode ? MyWorld.awakenedMode ? 11 : 8 : 5;
                    for (int l = 0; l < orbitalcount; l++)
                    {
                        int distance = 360 / orbitalcount;
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<PermaOrbital>(), npc.whoAmI, npc.whoAmI, l * distance);
                    }
                    shootTimer++;
                }
                if (aiTimer < 90)
                {
                    Vector2 toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
                    toTarget.Normalize();
                    if (Vector2.Distance(P.Center, npc.Center) >= 30)
                    {
                        npc.velocity = toTarget * 0.1f;
                    }
                }
                else if (aiTimer == 90)
                {
                    Teleport(P.Center + new Vector2(tpDist, 0));
                }
                else if (aiTimer > 90 && aiTimer < 210)
                {
                    npc.velocity.Y = 0;
                    npc.velocity.X = -dashSpeed;
                }
                else if (aiTimer == 210)
                {
                    Teleport(P.Center + new Vector2(-tpDist, 0));
                }
                else if (aiTimer > 210)
                {
                    npc.velocity.Y = 0;
                    npc.velocity.X = dashSpeed;
                }
                if (aiTimer > 330)
                {
                    aiTimer = 6;
                    shootTimer = 0;
                    state = 1;
                    CircularTP(P, 500);
                    npc.velocity = Vector2.Zero;
                }
            }
            else if (state == 6)
            {
                FlyTo(new Vector2(P.Center.X, P.Center.Y), 0.1f, 14f);
                aiTimer++;
                if (shootTimer == 0)
                {
                    storePosX = P.Center.X + Main.rand.Next(-600, 600);
                    storePosY = P.Center.Y + Main.rand.Next(-300, 300);
                    npc.netUpdate = true;
                }
                else if (shootTimer < 0)
                {
                    Vector2 storedPos = new Vector2(storePosX, storePosY);

                    Dust dust = Main.dust[Dust.NewDust(storedPos, 2, 2, 135, 0f, 0f, 200, default(Color), 2.5f)];
                    dust.noGravity = true;
                    dust.fadeIn = 1.3f;
                    Vector2 vector = Main.rand.NextVector2Square(-1, 1f);
                    vector.Normalize();
                    vector *= 3f;
                    dust.velocity = vector;
                    dust.position = storedPos - vector * 15;
                }
                shootTimer--;
                if (shootTimer <= -60 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);

                    Vector2 storedPos = new Vector2(storePosX, storePosY);
                    float Speed = 12f;
                    float rotation = (float)Math.Atan2(storedPos.Y - P.Center.Y, storedPos.X - P.Center.X);
                    Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                    Projectile.NewProjectile(storedPos, projSpeed, ProjectileType<HomingIce>(), projectileBaseDamage, 0f, Main.myPlayer);
                    shootTimer = 0;
                }
                if (aiTimer % 120 == 0)
                {
                    CircularTP(P, 500);
                }
                if (aiTimer > 600)
                {
                    aiTimer = 0;
                    shootTimer = 0;
                    state++;
                    CircularTP(P, 650);
                }
            }
            else if (state == 7)
            {
                FlyTo(new Vector2(P.Center.X, P.Center.Y), 0.05f, 3f);

                aiTimer++;
                shootTimer--;
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Main.PlaySound(SoundID.DD2_BookStaffCast, npc.Center);

                    Vector2 mouth = new Vector2(npc.Center.X, npc.Center.Y + 40);
                    float Speed = 14f;
                    float rotation = (float)Math.Atan2(mouth.Y - P.Center.Y, mouth.X - P.Center.X);
                    Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                    projSpeed = projSpeed.RotatedByRandom(MathHelper.ToRadians(10));
                    Projectile.NewProjectile(mouth, projSpeed, ProjectileType<FrigidBreath>(), projectileBaseDamage, 0f, Main.myPlayer);
                    shootTimer = 6;
                }
                if (aiTimer > 270)
                {
                    aiTimer = 0;
                    shootTimer = 0;
                    state++;
                }
            }
            else if (state == 8)
            {
                aiTimer++;
                if (shootTimer == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    CircularTP(P, 500);
                    npc.velocity = Vector2.Zero;
                   
                    storePosX = P.Center.X;
                    storePosY = P.Center.Y;
                    float Speed = 24f;
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                    Projectile.NewProjectile(npc.Center, projSpeed, ProjectileType<PermaDashWarn>(), 0, 0 , Main.myPlayer);
                }
                else if (shootTimer == -30)
                {
                    Vector2 toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
                    toTarget.Normalize();
                    npc.velocity = toTarget * 20;
                }
                if (shootTimer < -30)
                {
                    npc.velocity *= 0.99f;
                }
                shootTimer--;
                if (shootTimer <= -100)
                {
                    shootTimer = 0;
                }
                if (aiTimer > 360)
                {
                    aiTimer = 0;
                    shootTimer = -30;
                    state = 0;

                    Teleport(P.Center + new Vector2(0, -300));
                    npc.velocity = Vector2.Zero;
                }
            }
        }
        private void Teleport(Vector2 toPos)
        {
            Main.PlaySound(SoundID.Item30, npc.Center); // 46 // 77 // 104
            for (int k = 0; k < 50; k++)
            {
                Dust d = Main.dust[Dust.NewDust(npc.Center + (toPos - npc.Center) * Main.rand.NextFloat() - new Vector2(4, 4), 16, 16, Main.rand.Next(3) == 0 ? 41 : 135)];
                d.noGravity = true;
                d.velocity *= 1.2f;
                if (d.type == 41) d.scale *= 1.8f;
                else d.scale *= 2.8f;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, ProjectileType<PermafrostTP>(), 0, 0f, Main.myPlayer);
                npc.Center = toPos;
                npc.netUpdate = true;
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, ProjectileType<PermafrostTP>(), 0, 0f, Main.myPlayer);
            }
        }
        private void CircularTP(Player P, float dist)
        {
            double angle = Main.rand.NextDouble() * 2d * Math.PI;
            Vector2 offset = new Vector2((float)Math.Sin(angle) * dist, (float)Math.Cos(angle) * dist);
            Teleport(P.Center + offset);
            npc.netUpdate = true;
        }
        private void FlyTo(Vector2 location, float acceleration, float speed)
        {
            float targetX = location.X - npc.Center.X;
            float targetY = location.Y - npc.Center.Y;
            float targetPos = (float)Math.Sqrt((double)(targetX * targetX + targetY * targetY));
            targetPos = speed / targetPos;
            targetX *= targetPos;
            targetY *= targetPos;
            if (npc.velocity.X < targetX)
            {
                npc.velocity.X = npc.velocity.X + acceleration;
                if (npc.velocity.X < 0f && targetX > 0f)
                {
                    npc.velocity.X = npc.velocity.X + acceleration;
                }
            }
            else if (npc.velocity.X > targetX)
            {
                npc.velocity.X = npc.velocity.X - acceleration;
                if (npc.velocity.X > 0f && targetX < 0f)
                {
                    npc.velocity.X = npc.velocity.X - acceleration;
                }
            }
            if (npc.velocity.Y < targetY)
            {
                npc.velocity.Y = npc.velocity.Y + acceleration;
                if (npc.velocity.Y < 0f && targetY > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + acceleration;
                }
            }
            else if (npc.velocity.Y > targetY)
            {
                npc.velocity.Y = npc.velocity.Y - acceleration;
                if (npc.velocity.Y > 0f && targetY < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - acceleration;
                }
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}
