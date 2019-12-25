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

namespace ElementsAwoken.NPCs.Bosses.Azana
{
    [AutoloadBossHead]
    public class Azana : ModNPC
    {
        int moveAi = 0;
        int wormTimer = 1800;
        int text = 0;
        int dustTimer = 0;
        int stopHitTimer = 0;

        float spinAI = 0f;

        bool reset = false;
        bool activeTrail = false;
        bool canStopHit = false;
        bool halfLife = false;
        bool lowLife = false;

        bool justTransformed = true;
        int lightTimer = 0;

        int projectileBaseDamage = 150;

        Vector2 maskCenter;
        public override void SetDefaults()
        {
            npc.lifeMax = 1000000;
            npc.damage = 200;
            npc.defense = 100;
            npc.knockBackResist = 0f;

            npc.width = 150;
            npc.height = 270;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;

            npc.scale = 1f;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 75, 0, 0);
            npc.npcSlots = 1f;
            music = MusicID.Boss4;

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

            bossBag = mod.ItemType("AzanaBag");

            NPCID.Sets.TrailCacheLength[npc.type] = 3;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Azana");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 1200000;
            npc.damage = 300;
            npc.defense = 120;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 1500000;
                npc.damage = 350;
                npc.defense = 130;
            }
        }
        public override bool PreDraw(SpriteBatch spritebatch, Color lightColor)
        {
            if (activeTrail)
            {
                Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
                for (int k = 0; k < npc.oldPos.Length; k++)
                {
                    Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                    Color color = new Color(93, 25, 43, 200);
                    //Color color2 = npc.GetAlpha(lightColor) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
                    Texture2D shadowTexture = mod.GetTexture("NPCs/Bosses/Azana/AzanaShadow");
                    SpriteEffects spriteEffects = npc.direction != -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

                    spritebatch.Draw(shadowTexture, drawPos, null, color, npc.rotation, drawOrigin, npc.scale, spriteEffects, 0f);
                }
            }
            return true;
        }
        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            // azana says no to cheat sheet 
            if (damage > npc.lifeMax / 2)
            {
                Main.PlaySound(29, (int)npc.position.X, (int)npc.position.Y, 104);
                Main.NewText("Hah, you really think cheat sheet could save you from your demise?", new Color(93, 25, 43, 200));

                int type = mod.ProjectileType("AzanaWave");
                int projDamage = Main.expertMode ? 200 : 300;
                float numberProjectiles = 30;
                float rotation = MathHelper.ToRadians(360);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(2, 2).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 2f;
                    int num1 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, projDamage, 2f, 0);
                }

                damage = 0;
            }
            return false;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            if (npc.frameCounter > 10)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y > frameHeight * 4)
            {
                npc.frame.Y = 0;
            }
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AzanaTrophy"));
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AzanaMask"));
            }

            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                int choice = Main.rand.Next(5);
                if (choice == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Anarchy"));
                }
                if (choice == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PurgeRifle"));
                }
                if (choice == 2)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ChaoticImpaler"));
                }
                if (choice == 3)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GleamOfAnnhialation"));
                }
                if (choice == 4)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Pandemonium"));
                }
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EntropicCoating"));
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DiscordantOre"), Main.rand.Next(35, 80));
            }
            Main.NewText("But you wont stop at anything to satisfy your bloodlust...", new Color(93, 25, 43, 200));
            MyWorld.downedAzana = true;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            stopHitTimer = 0;
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            npc.TargetClosest(true);

            maskCenter = new Vector2(npc.Center.X, npc.Center.Y - 102);

            if (!P.active || P.dead)
            {
                npc.TargetClosest(true);
                if (!P.active || P.dead)
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
            Lighting.AddLight(npc.Center, ((255 - npc.alpha) * 0.9f) / 255f, ((255 - npc.alpha) * 0.1f) / 255f, ((255 - npc.alpha) * 0f) / 255f);
            npc.spriteDirection = npc.direction;

            if (justTransformed)
            {
                Terraria.GameContent.Events.MoonlordDeathDrama.RequestLight(1f, npc.Center);

                lightTimer++;
                if (lightTimer >= 60)
                {
                    justTransformed = false;
                }
            }
            else
            {
                #region text & stophit
                if (!reset)
                {
                    reset = true;
                    //npc.ai[1] = 3400;

                    Main.NewText("At last, I am free!", new Color(93, 25, 43, 200));
                }
                if (npc.life <= npc.lifeMax * 0.90f && text == 0)
                {
                    Main.NewText("Hah, this is just a warmup", new Color(93, 25, 43, 200));
                    text++;
                }
                if (npc.life <= npc.lifeMax * 0.75f && text == 1)
                {
                    Main.NewText("You are quite impressive child", new Color(93, 25, 43, 200));
                    text++;
                }
                if (npc.life <= npc.lifeMax * 0.50f && text == 2)
                {
                    Main.NewText("Andromeda! Kill this puny terrarian", new Color(93, 25, 43, 200));
                    halfLife = true;
                    text++;
                }
                if (npc.life <= npc.lifeMax * 0.30f && text == 3)
                {
                    Main.NewText("Ah... You are strong", new Color(93, 25, 43, 200));
                    lowLife = true;
                    text++;
                }
                if (npc.life <= npc.lifeMax * 0.15f && text == 4)
                {
                    Main.NewText("It doesnt have to be this way!", new Color(93, 25, 43, 200));
                    text++;
                }
                if (npc.life <= npc.lifeMax * 0.05f && text == 5)
                {
                    Main.NewText("You dont need to fight me anymore! End this madness... Please...", new Color(93, 25, 43, 200));
                    text++;

                    canStopHit = true;
                    stopHitTimer = 0;
                }
                if (canStopHit)
                {
                    stopHitTimer++;
                }
                if (stopHitTimer == 1800)
                {
                    Main.NewText("You're... You're not attacking me?", new Color(93, 25, 43, 200));
                }
                if (stopHitTimer >= 3600)
                {
                    Main.NewText("Thank you...", new Color(93, 25, 43, 200));
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AzanaMinionStaff"));
                    MyWorld.downedAzana = true;
                    npc.active = false;
                }
                #endregion
                #region escape           
                if (Vector2.Distance(P.Center, npc.Center) >= 2500)
                {
                    if (Main.netMode != 1)
                    {
                        string escapeText = "Trying to escape so early?";
                        if (halfLife && !lowLife)
                        {
                            int choice = Main.rand.Next(3);
                            if (choice == 0)
                            {
                                escapeText = "Dont try to run away!";
                            }
                            if (choice == 1)
                            {
                                escapeText = "You cant outrun me!";
                            }
                            if (choice == 2)
                            {
                                escapeText = "Get over here!";
                            }
                        }
                        if (lowLife)
                        {
                            int choice = Main.rand.Next(3);
                            if (choice == 0)
                            {
                                escapeText = "Get... Back... Here...";
                            }
                            if (choice == 1)
                            {
                                escapeText = "Ah, I'll kill you! Mark my words...";
                            }
                            if (choice == 2)
                            {
                                escapeText = "You cant escape... your demise...";
                            }
                        }
                        Main.NewText(escapeText, new Color(93, 25, 43, 200));
                        Teleport(P.position.X + Main.rand.Next(-300, 300), P.position.Y - (400 + Main.rand.Next(0, 600)));
                        npc.netUpdate = true;
                    }
                }
                #endregion
                if (Main.netMode != 1)
                {
                    npc.ai[1]++;
                    npc.ai[2]--;
                    npc.ai[3]++;

                    wormTimer--;
                    dustTimer--;
                }
                if (npc.ai[1] > 5000)
                {
                    npc.ai[1] = 0;
                }
                if (wormTimer <= 0 && halfLife) //  && !NPC.AnyNPCs(mod.NPCType("AzanaWormHead"))
                {
                    Vector2 spawnAt = npc.Center + new Vector2(0f, (float)npc.height / 2f);
                    NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("AzanaWormHead"));
                    wormTimer = 2700;
                }

                if (stopHitTimer < 2700)
                {
                    #region attack 1
                    if (npc.ai[1] < 1000)
                    {

                        if (halfLife && Main.netMode != 1)
                        {
                            npc.ai[2] -= 0.5f;
                        }
                        activeTrail = true;

                        //movement
                        float speed = 0.2f;
                        float playerX = P.Center.X - npc.Center.X;
                        float playerY = P.Center.Y - 400f - npc.Center.Y;
                        if (moveAi == 0)
                        {
                            playerX = P.Center.X - 600f - npc.Center.X;
                            if (Math.Abs(P.Center.X - 600f - npc.Center.X) <= 20)
                            {
                                moveAi = 1;
                            }
                        }
                        if (moveAi == 1)
                        {
                            playerX = P.Center.X + 600f - npc.Center.X;
                            if (Math.Abs(P.Center.X + 600f - npc.Center.X) <= 20)
                            {
                                moveAi = 0;
                            }
                        }
                        Move(P, speed, playerX, playerY);

                        if (npc.ai[2] <= 0)
                        {
                            float projSpeed = 18f;
                            if (halfLife) projSpeed += 2f;
                            if (lowLife) projSpeed += 2f;
                            Blasts(P, projSpeed, projectileBaseDamage + 25);
                            npc.ai[2] = 35;
                        }

                        if (halfLife)
                        {
                            npc.ai[2]--;
                            if (lowLife)
                            {
                                npc.ai[2]--;
                            }
                            if (npc.ai[2] <= 0)
                            {
                                if (Main.netMode != 1)
                                {
                                    int distance = Main.rand.Next(200, 800);
                                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                                    Vector2 offset = new Vector2((float)Math.Sin(angle) * distance, (float)Math.Cos(angle) * distance);

                                    Teleport(P.Center.X + offset.X, P.Center.Y + offset.Y);
                                    npc.ai[2] = 180 + Main.rand.Next(0, 120);
                                    npc.netUpdate = true;
                                }
                            }
                        }
                    }
                    #endregion
                    #region attack 2 - waves
                    if (npc.ai[1] == 1000)
                    {
                        npc.ai[3] = 0;
                    }
                    if (npc.ai[1] >= 1000 && npc.ai[1] < 1600)
                    {
                        activeTrail = false;

                        npc.velocity.X = 0f;
                        npc.velocity.Y = 0f;

                        if (Main.netMode != 1)
                        {
                            if (halfLife) npc.ai[3] += 0.5f;
                            if (lowLife) npc.ai[3] += 0.5f;
                            if (MyWorld.awakenedMode) npc.ai[3] += 0.5f;
                        }
                        //teleport
                        if (npc.ai[3] >= 20 && npc.ai[0] == 0)
                        {
                            if (Main.netMode != 1)
                            {
                                int distance = Main.rand.Next(200, 800);
                                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                                Vector2 offset = new Vector2((float)Math.Sin(angle) * distance, (float)Math.Cos(angle) * distance);

                                Teleport(P.Center.X + offset.X, P.Center.Y + offset.Y);
                                npc.ai[0]++;
                                npc.netUpdate = true;
                            }
                        }
                        if (npc.ai[3] >= 45)
                        {
                            int damage = Main.expertMode ? projectileBaseDamage + 50 : projectileBaseDamage + 25;
                            float speed = 14f;
                            if (halfLife && !lowLife)
                            {
                                speed += 1f;
                            }
                            if (lowLife)
                            {
                                speed -= 1f; // make them slower cuz she shoots faster
                            }
                            WaveRing(P, speed, damage);
                            npc.ai[3] = 0;
                            npc.ai[0] = 0;
                        }
                    }
                    #endregion
                    #region attack 3 - tp to the sides and shoot at player while moving up
                    if (npc.ai[1] >= 1600 && npc.ai[1] < 2000)
                    {
                        activeTrail = false;

                        int type = mod.ProjectileType("AzanaMiniBlast");
                        float movespeed = 10f;
                        if (halfLife) movespeed += 2f;
                        if (lowLife) movespeed += 2;
                        float speed = 22f;

                        if (npc.ai[1] == 1620)
                        {
                            npc.Center = new Vector2(P.Center.X - 500, P.Center.Y + 800);
                        }
                        if (npc.ai[1] >= 1620 && npc.ai[1] < 1800)
                        {
                            npc.velocity.Y = -movespeed;
                            if (npc.ai[2] <= 0)
                            {
                                if (Main.netMode != 1)
                                {
                                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 21);
                                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speed, 0f, type, projectileBaseDamage, 0f, 0);
                                    npc.ai[2] = 5 + Main.rand.Next(0, 10);
                                    npc.netUpdate = true;
                                }
                            }
                        }
                        if (npc.ai[1] == 1820)
                        {
                            npc.Center = new Vector2(P.Center.X + 500, P.Center.Y + 800);
                        }
                        if (npc.ai[1] >= 1820 && npc.ai[1] < 2000)
                        {
                            npc.velocity.Y = -movespeed;
                            if (npc.ai[2] <= 0)
                            {
                                if (Main.netMode != 1)
                                {
                                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 21);
                                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -speed, 0f, type, projectileBaseDamage, 0f, 0);
                                    npc.ai[2] = Main.rand.Next(5, 15);
                                    if (lowLife) npc.ai[2] -= 2;
                                    npc.netUpdate = true;
                                }
                            }
                        }
                    }
                    #endregion
                    #region attack 4 - fly around and create spikes from the sides
                    if (npc.ai[1] >= 2000 && npc.ai[1] < 2600)
                    {
                        activeTrail = true;

                        float speed = 0.2f;
                        float playerX = P.Center.X - npc.Center.X;
                        float playerY = P.Center.Y - 500f - npc.Center.Y;
                        Move(P, speed, playerX, playerY);

                        if (Main.netMode != 1)
                        {
                            int randValue = lowLife ? 6 : 8;
                            if (Main.rand.Next(randValue) == 0)
                            {
                                int damage = projectileBaseDamage - 25;
                                EdgeSpikes(P, damage);
                            }
                            npc.netUpdate = true;
                        }
                    }
                    #endregion
                    #region attack 5 - shoot blades
                    if (npc.ai[1] >= 2600 && npc.ai[1] < 3000)
                    {
                        activeTrail = true;

                        float speed = 0.2f;
                        float playerX = P.Center.X + 400 - npc.Center.X;
                        float playerY = P.Center.Y - npc.Center.Y;
                        Move(P, speed, playerX, playerY);

                        if (npc.ai[2] <= 0)
                        {
                            if (Main.netMode != 1)
                            {
                                float projSpeed = halfLife ? 20f : 18f;
                                int damage = projectileBaseDamage + 25;
                                Blades(P, projSpeed, damage);
                                npc.ai[2] = 10 + Main.rand.Next(0, 20);
                                npc.netUpdate = true;
                            }
                        }
                    }
                    #endregion
                    #region attack 6 - fire in circle
                    if (npc.ai[1] >= 3000 && npc.ai[1] < 3600)
                    {
                        activeTrail = false;

                        npc.velocity.X = 0f;
                        npc.velocity.Y = 0f;

                        if (npc.ai[1] >= 3000 && npc.ai[1] < 3120 && dustTimer <= 0)
                        {
                            int maxdusts = 20;
                            for (int i = 0; i < maxdusts; i++)
                            {
                                float dustDistance = 100;
                                float dustSpeed = 15;
                                Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                                Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                                Dust vortex = Dust.NewDustPerfect(new Vector2(npc.Center.X, npc.Center.Y - 102) + offset, 127, velocity, 0, default(Color), 1.5f);
                                vortex.noGravity = true;
                            }
                            dustTimer = 5;
                        }
                        if (npc.ai[1] == 3120)spinAI = 0f;
                        if (npc.ai[1] >= 3120 && npc.ai[1] < 3600)
                        {
                            // shoot in circle
                            Vector2 offset = new Vector2(400, 0);
                            float rotateSpeed = 0.027f;
                            spinAI += rotateSpeed;

                            float projSpeed = halfLife ? 17f : 14f;
                            int type = mod.ProjectileType("AzanaMiniBlast");
                            int damage = halfLife ? projectileBaseDamage : projectileBaseDamage - 20;

                            if (npc.ai[2] <= 0)
                            {
                                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                                float numProj = 4f;
                                float projOffset = MathHelper.ToRadians(360f) / numProj;
                                for (int i = 0; i < numProj; i++)
                                {
                                    Vector2 shootTarget1 = maskCenter + offset.RotatedBy(spinAI + (projOffset * (float)i));
                                    float rotation = (float)Math.Atan2(maskCenter.Y - shootTarget1.Y, maskCenter.X - shootTarget1.X);
                                    Projectile.NewProjectile(maskCenter.X, maskCenter.Y, (float)((Math.Cos(rotation) * projSpeed) * -1), (float)((Math.Sin(rotation) * projSpeed) * -1), type, damage, 0f, 0);
                                }
                                npc.ai[2] = 5;
                            }
                        }
                    }
                    #endregion
                    #region attack 7 - fly diagonally shooting blasts
                    if (npc.ai[1] == 3600)
                    {
                        npc.ai[2] = 0;
                    }
                    if (npc.ai[1] >= 3600 && npc.ai[1] < 4400)
                    {
                        activeTrail = true;
                        float speed = halfLife ? 10f : 8f;
                        Vector2 diagSpeed = new Vector2();

                        //top left
                        if (npc.ai[3] == 1) Teleport(P.position.X + 700, P.position.Y + 700);
                        if (npc.ai[3] >= 1 && npc.ai[3] < 120)
                        {
                            npc.velocity = new Vector2(-8f,-8f);
                            diagSpeed = new Vector2(speed, -speed);
                        }
                        // top right
                        if (npc.ai[3] == 120) Teleport(P.position.X - 700, P.position.Y + 700);
                        if (npc.ai[3] >= 120 && npc.ai[3] < 240)
                        {
                            npc.velocity = new Vector2(8f, -8f);
                            diagSpeed = new Vector2(-speed, -speed);
                        }
                        // bottom left
                        if (npc.ai[3] == 240) Teleport(P.position.X + 700, P.position.Y - 700);
                        if (npc.ai[3] >= 240 && npc.ai[3] < 360)
                        {
                            npc.velocity = new Vector2(-8f, 8f);
                            diagSpeed = new Vector2(-speed, -speed);
                        }
                        // bottom right
                        if (npc.ai[3] == 360) Teleport(P.position.X - 700, P.position.Y - 700);
                        if (npc.ai[3] >= 360)
                        {
                            npc.velocity = new Vector2(8f, 8f);
                            diagSpeed = new Vector2(speed, -speed);
                        }

                        if (npc.ai[3] >= 480) npc.ai[3] = 0;
                        if (npc.ai[2] <= 0)
                        {
                            int damage = projectileBaseDamage - 60;
                            int type = mod.ProjectileType("AzanaMiniBlast");
                            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 21);
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, diagSpeed.X, diagSpeed.Y, type, damage, 0f, 0);
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -diagSpeed.X, -diagSpeed.Y, type, damage, 0f, 0);
                            npc.ai[2] = 9;
                        }
                    }
                    #endregion
                    #region attack 8 - homing fire
                    if (npc.ai[1] >= 4400 && npc.ai[1] < 5000)
                    {
                        activeTrail = false;
                        int dust = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y + 150), 20, 20, 127, npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f, 100, default(Color), 2f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale = 1f + Main.rand.Next(10) * 0.1f;
                        if (npc.ai[1] == 4400)
                        {
                            TeleportDust();

                            npc.position.X = P.position.X;
                            npc.position.Y = P.position.Y - 400;
                        }
                        npc.velocity.X = 0f;
                        npc.velocity.Y = 0f;
                        if (npc.ai[1] >= 4060)
                        {
                            if (npc.ai[2] <= 0)
                            {
                                HomingFire(projectileBaseDamage - 25);
                                npc.ai[2] = 12;
                                if (halfLife)
                                {
                                    npc.ai[2] -= 3;
                                }
                                if (lowLife)
                                {
                                    npc.ai[2] -= 2;
                                }
                            }
                        }
                    }
                    #endregion
                }
                else // not been attacked for 45 seconds
                {
                    for (int k = 0; k < Main.npc.Length; k++)
                    {
                        NPC other = Main.npc[k];
                        if (other.type == mod.NPCType("AzanaWormHead") ||
                            other.type == mod.NPCType("AzanaWormBody") ||
                            other.type == mod.NPCType("AzanaWormTail"))
                        {
                            other.alpha += 5;
                            if (other.alpha >= 255)
                            {
                                other.active = false;
                            }
                        }
                    }

                    activeTrail = true;
                    float speed = 0.2f;
                    float playerX = P.Center.X - npc.Center.X;
                    float playerY = P.Center.Y - 400f - npc.Center.Y;
                    if (moveAi == 0)
                    {
                        playerX = P.Center.X - 600f - npc.Center.X;
                        if (Math.Abs(P.Center.X - 600f - npc.Center.X) <= 20)
                        {
                            moveAi = 1;
                        }
                    }
                    if (moveAi == 1)
                    {
                        playerX = P.Center.X + 600f - npc.Center.X;
                        if (Math.Abs(P.Center.X + 600f - npc.Center.X) <= 20)
                        {
                            moveAi = 0;
                        }
                    }
                    Move(P, speed, playerX, playerY);
                }
            }
        }
        public override bool CheckActive()
        {
            return false;
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

        private void Blasts(Player P, float speed, int damage)
        {
            int type = mod.ProjectileType("AzanaBlast");
            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 21);
            float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
            int num54 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), type, damage, 0f, 0);
        }

        private void WaveRing(Player P, float speed, int damage)
        {
            float spread = 45f * 0.0174f;
            double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - spread / 2;
            double deltaAngle = spread / 8f;
            double offsetAngle;
            int type = mod.ProjectileType("AzanaWave");            
            for (int i = 0; i < 4; i++)
            {
                offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * speed), (float)(Math.Cos(offsetAngle) * speed), type, damage, 8f, 0);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(-Math.Sin(offsetAngle) * speed), (float)(-Math.Cos(offsetAngle) * speed), type, damage, 8f, 0);
            }
        }

        private void EdgeSpikes(Player P, int damage)
        {
            float posX = P.Center.X + 1000;
            float posY = P.Center.Y + Main.rand.Next(5000) - 3000;
            Projectile.NewProjectile(posX, posY, -2f, 0f, mod.ProjectileType("AzanaSpike"), damage, 0f);
            float posX2 = P.Center.X - 1000;
            float posY2 = P.Center.Y + Main.rand.Next(5000) - 3000;
            Projectile.NewProjectile(posX2, posY2, 2f, 0f, mod.ProjectileType("AzanaSpike"), damage, 0f);
        }

        private void Blades(Player P, float speed, int damage)
        {
            Vector2 vector = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2) - 300);
            int type = mod.ProjectileType("AzanaBlade");
            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 21);
            float rotation = (float)Math.Atan2(vector.Y - (P.position.Y + (P.height * 0.5f)), vector.X - (P.position.X + (P.width * 0.5f)));
            int proj = Projectile.NewProjectile(vector.X, vector.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), type, damage, 0f, 0);
        }

        private void HomingFire(int damage)
        {
            Vector2 vector = new Vector2(npc.Center.X, npc.Center.Y + 150);

            int type = mod.ProjectileType("AzanaFireBall");
            int choice = Main.rand.Next(2);
            if (choice == 0)
            {
                Projectile.NewProjectile(vector.X, vector.Y, -3 + Main.rand.Next(-2, 2), 4 + Main.rand.Next(-4, 0), type, damage, 0f, 0);
            }
            if (choice == 1)
            {
                Projectile.NewProjectile(vector.X, vector.Y, 3 + Main.rand.Next(-2, 2), 4 + Main.rand.Next(-4, 0), type, damage, 0f, 0);
            }
        }

        private void TeleportDust()
        {
            for (int k = 0; k < 80; k++)
            {
                int dust = Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 127, npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f, 100, default(Color), 2f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f + Main.rand.Next(10) * 0.1f;
            }
        }

        private void Teleport(float posX, float posY)
        {
            npc.position.X = posX;
            npc.position.Y = posY;
            for (int k = 0; k < 80; k++)
            {
                int dust = Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 127, npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f, 100, default(Color), 2f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f + Main.rand.Next(10) * 0.1f;
            }
        }
    }
}

