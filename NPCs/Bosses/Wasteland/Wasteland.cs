using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Events;

namespace ElementsAwoken.NPCs.Bosses.Wasteland
{
    [AutoloadBossHead]
    public class Wasteland : ModNPC
    {
        public bool reset = true;

        public float burstTimer = 0;
        public int shootTimer = 100;
        public int eggTimer = 75;
        public int jumpUpTimer = 200;

        public int sandstormTimer = 0;
        public int wastelandStormTimer = 0;

        public bool enraged = false;

        public int spoutSpawnTimer = 1000;

        public bool underground = false;
        public int diggingType = 0; // 0 is none, 1 is up, 2 is down
        public int diggingTimer = 60;

        public bool collision = false;
        public float digSpeed = 3f;

        public float aiTimer = 0;
        public float jumpSpikeTimer = 0;

        public int projectileBaseDamage = 25;


        public override void BossHeadSlot(ref int index)
        {
            /*if (underground || wastelandAI[1] == 0 || diggingType == 2)
            {
                index = NPCHeadLoader.GetBossHeadSlot("ElementsAwoken/NPCs/Bosses/Wasteland/Wasteland_Head_Boss_Blank");
            }
            else
            {*/
                index = NPCHeadLoader.GetBossHeadSlot("ElementsAwoken/NPCs/Bosses/Wasteland/Wasteland_Head_Boss");
            //}
        }
        public override void SetDefaults()
        {
            npc.width = 140;
            npc.height = 130;

            npc.damage = 25;
            npc.defense = 15;
            npc.lifeMax = 4300;          
            npc.knockBackResist = 0f;

           // npc.aiStyle = 3;
           // aiType = NPCID.AnomuraFungus;
            //animationType = 257;

            npc.value = Item.buyPrice(0, 5, 0, 0);

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath36;

            npc.boss = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;

            music = MusicID.Boss1;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/WastelandTheme");
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
            Main.npcFrameCount[npc.type] = 5;
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
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 5000;
            npc.damage = 50;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 7500;
                npc.damage = 65;
                npc.defense = 20;
            }
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Poisoned, 300, false);
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
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DesertEssence"), Main.rand.Next(5,20));
            MyWorld.downedWasteland = true;

            // stop the sandstorm
            Sandstorm.Happening = false;
            Sandstorm.TimeLeft = 0;
            Sandstorm.IntendedSeverity = 0;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.LesserHealingPotion;
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            npc.TargetClosest(true);
            if (!Main.player[npc.target].active || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (!Main.player[npc.target].active || Main.player[npc.target].dead)
                {
                    npc.localAI[2]++;
                    if (npc.localAI[2] >= 300)
                    {
                        npc.active = false;
                    }
                }
                else
                    npc.localAI[2] = 0;
            }
            #region collision check
            int minTilePosX = (int)(npc.position.X / 16.0) - 1;
            int maxTilePosX = (int)((npc.position.X + npc.width) / 16.0) + 2;
            int minTilePosY = (int)(npc.position.Y / 16.0) - 1;
            int maxTilePosY = (int)((npc.position.Y + npc.height) / 16.0) + 2;
            if (minTilePosX < 0)
                minTilePosX = 0;
            if (maxTilePosX > Main.maxTilesX)
                maxTilePosX = Main.maxTilesX;
            if (minTilePosY < 0)
                minTilePosY = 0;
            if (maxTilePosY > Main.maxTilesY)
                maxTilePosY = Main.maxTilesY;

            collision = false;
            for (int i = minTilePosX; i < maxTilePosX; ++i)
            {
                for (int j = minTilePosY; j < maxTilePosY; ++j)
                {
                    if (Main.tile[i, j] != null && TileID.Sets.Platforms[Main.tile[i, j].type] != true && (Main.tile[i, j].nactive() && (Main.tileSolid[(int)Main.tile[i, j].type] || Main.tileSolidTop[(int)Main.tile[i, j].type] && (int)Main.tile[i, j].frameY == 0) || (int)Main.tile[i, j].liquid > 64))
                    {
                        Vector2 vector2;
                        vector2.X = (float)(i * 16);
                        vector2.Y = (float)(j * 16);
                        if (npc.position.X + npc.width > vector2.X && npc.position.X < vector2.X + 16.0 && (npc.position.Y + npc.height > (double)vector2.Y && npc.position.Y < vector2.Y + 16.0))
                        {
                            collision = true;
                            if (Main.rand.Next(100) == 0 && Main.tile[i, j].nactive())
                                WorldGen.KillTile(i, j, true, true, false);
                        }
                    }
                }
            }
            #endregion
            #region enraged stuff
            if (P.ZoneDesert)
            {
                enraged = false;
            }
            else
            {
                enraged = true;
            }
            if (enraged)
            {
                npc.defense = 30;
                npc.damage = 40;
            }
            if (!enraged)
            {
                npc.defense = 18;
                npc.damage = 25;
            }
            #endregion
            if (sandstormTimer <= 0)
            {
                Sandstorm.Happening = true;
                Sandstorm.TimeLeft = (int)(3600.0 * (8.0 + (double)Main.rand.NextFloat() * 16.0));
                SandstormStuff();
                sandstormTimer = 3600;
            }

            // timers
            shootTimer--;
            eggTimer--;
            if (!underground && diggingType == 0)
            {
                spoutSpawnTimer--;
            }
            sandstormTimer--;

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

            #region spawn tp and too far tp
            // spawn reset
            if (!reset)
            {
                float posX = Main.player[npc.target].Center.X;
                float posY = Main.player[npc.target].Center.Y + 700;
                npc.Center = new Vector2(posX, posY);
                reset = true;
            }
            /*if (tooFarRise)
            {
                npc.noTileCollide = true;
                for (int k = 0; k < 10; k++)
                {
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, 32);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1f;
                    Main.dust[dust].velocity *= 0.1f;
                }
                npc.velocity.Y = -6;
                npc.velocity.X = 0;
                if (collision == false && Math.Abs(npc.Center.Y - P.Center.Y + 200) <= 200) // 200 is 400 pixels lol
                {
                    for (int k = 0; k < 500; k++)
                    {
                        int dust2 = Dust.NewDust(npc.position, npc.width, npc.height, 32, 0f, 0f, 100, default(Color), 1.5f);
                        Main.dust[dust2].noGravity = true;
                        Main.dust[dust2].velocity *= 1.5f;
                    }
                    npc.noTileCollide = false;
                    tooFarRise = false;
                }
            }*/
            #endregion
           // Main.NewText(diggingType + " " + underground);
            // create the wasteland spouts
            if (spoutSpawnTimer <= 0)
            {
                int numFakes = Main.expertMode ? Main.rand.Next(2, 4) : 2;
                for (int k = 0; k < numFakes; k++)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-12, -2), mod.ProjectileType("WastelandDiggingProj"), 0, 0);
                }
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-12, -2), mod.ProjectileType("WastelandDiggingProjReal"), 0, 0, 0, npc.whoAmI);
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
            int maxDist = diggingType != 0 ? 1200 : 3000;
            if (Vector2.Distance(P.Center, npc.Center) >= 1200)
            {
                npc.Center = new Vector2(P.Center.X, P.Center.Y + 700);
                //tooFarRise = true;
                diggingType = 2;
                digSpeed = 6f;
            }

            if (!underground && diggingType == 0)
            {
                aiTimer++; // ai timer
                CustomAI_3();
                npc.noTileCollide = false;
                if (aiTimer < 600)
                {
                    int numScorpions = NPC.CountNPCS(mod.NPCType("WastelandMinion"));
                    if (eggTimer <= 0 && numScorpions < 10)
                    {
                        int add = 0;
                        if (npc.direction == -1)
                        {
                            add = 40;
                        }
                        else if (npc.direction == 1)
                        {
                            add = -40;
                        }
                        NPC.NewNPC((int)npc.Center.X + add, (int)npc.Center.Y + 10, mod.NPCType("WastelandEgg"));
                        eggTimer = 120;
                    }
                    burstTimer--;
                    if (Main.netMode != 1 && burstTimer <= 0f && shootTimer <= 12) // 0 , 6 , 12 = 3 shots
                    {
                        float speed = 8f;
                        int add = 0;
                        if (npc.direction == -1)
                        {
                            add = 40;
                        }
                        else if (npc.direction == 1)
                        {
                            add = -40;
                        }
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 17);
                        Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2) + add, npc.position.Y + (npc.height / 2) - 30);
                        float rotation = (float)Math.Atan2(vector8.Y - P.Center.Y, vector8.X - P.Center.X);
                        Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), mod.ProjectileType("WastelandStinger"), projectileBaseDamage, 0f, 0);
                        burstTimer = 6f;
                    }
                    if (shootTimer <= 0)
                    {
                        shootTimer = enraged ? 60 : 100;
                    }

                    if (Main.expertMode)
                    {
                        wastelandStormTimer--;
                        if (wastelandStormTimer <= 0)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-12, -2), mod.ProjectileType("WastelandStormBolt"), 6, 0f, 0);
                            }
                        }
                        if (!enraged)
                        {
                            wastelandStormTimer = Main.rand.Next(1200, 2000);
                        }
                        if (enraged)
                        {
                            wastelandStormTimer = Main.rand.Next(200, 600);
                        }
                    }
                    //jump up
                    jumpUpTimer--;
                    if (jumpUpTimer <= 0 || Math.Abs(npc.Center.Y - P.Center.Y - 700) <= 200) // 200 is 400 pixels lol
                    {
                        for (int k = 0; k < 500; k++)
                        {
                            int dust = Dust.NewDust(new Vector2(npc.Center.X - 95, npc.Center.Y + 90), npc.width, 16, 75, 0f, 0f, 100, default(Color), 2f);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].velocity *= 1.5f;
                            dust = Dust.NewDust(new Vector2(npc.Center.X - 95, npc.Center.Y + 90), npc.width, 32, 32, 0f, 0f, 100, default(Color), 2.5f);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].velocity *= 1.5f;
                        }
                        npc.velocity.Y -= Main.rand.NextFloat(4, 9);
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 69);
                        jumpUpTimer = 200;
                    }
                }
                else if (aiTimer == 600)
                {
                    for (int k = 0; k < 500; k++)
                    {
                        int dust = Dust.NewDust(new Vector2(npc.Center.X - 95, npc.Center.Y + 90), npc.width, 16, 75, 0f, 0f, 100, default(Color), 2f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].velocity *= 1.5f;
                        dust = Dust.NewDust(new Vector2(npc.Center.X - 95, npc.Center.Y + 90), npc.width, 32, 32, 0f, 0f, 100, default(Color), 2.5f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].velocity *= 1.5f;
                    }
                    npc.velocity.Y -= Main.rand.NextFloat(8, 12);
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 69);
                    jumpSpikeTimer = 0;
                }
                else if (aiTimer >= 640)
                {
                    jumpSpikeTimer++; // jump up and circle shoot ai
                    if (jumpSpikeTimer >= 20)
                    {
                        npc.velocity.X = 0f;
                        npc.velocity.Y = 0f;
                        if (jumpSpikeTimer % 20 == 0)
                        {
                            float numberProjectiles = Main.expertMode ? 10 : 6;
                            float projSpeed = 5.5f;
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(projSpeed, projSpeed).RotatedByRandom(MathHelper.ToRadians(360));
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("WastelandStinger"), projectileBaseDamage - 5, 2f, 0);
                            }
                            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 17);
                        }
                    }
                    if (jumpSpikeTimer >= 70)
                    {
                        aiTimer = 0;
                    }
                }
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
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.type == mod.ProjectileType("WastelandDiggingSpoutReal") && proj.ai[0] == npc.whoAmI)
                {
                    float posX = proj.Center.X;
                    float posY = proj.Center.Y + 300;
                    npc.Center = new Vector2(posX, posY);

                    if (proj.ai[1] >= 360)
                    {
                        int lifeTime = 0;
                        if (npc.life < npc.lifeMax * 0.75)
                        {
                            lifeTime = 100;
                        }
                        else if (npc.life < npc.lifeMax * 0.5)
                        {
                            lifeTime = 200;
                        }
                        else if (npc.life < npc.lifeMax * 0.25)
                        {
                            lifeTime = 300;
                        }
                        spoutSpawnTimer = Main.rand.Next(1400, 1600) - lifeTime;

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
                    if (collision == false)
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
                        if ((Main.netMode != 1 || !flag23) && flag23 && Main.netMode != 1)
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