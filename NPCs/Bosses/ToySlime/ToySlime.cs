using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace ElementsAwoken.NPCs.Bosses.ToySlime
{
    [AutoloadBossHead]
    public class ToySlime : ModNPC
    {
        bool reset = false;
        int projectileBaseDamage = 15;
        int brickTimer = 0;

        float despawnTimer = 0;
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 22;

            //npc.aiStyle = 1;

            npc.knockBackResist = 0.1f;
            npc.damage = 25;
            npc.defense = 8;
            npc.lifeMax = 1200;

            animationType = NPCID.RainbowSlime;
            npc.value = Item.buyPrice(0, 3, 0, 0);

            npc.alpha = 60;

            npc.lavaImmune = false;
            npc.boss = true;
            npc.noGravity = false;
            npc.noTileCollide = false;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            music = MusicID.Boss1;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ToySlimeTheme");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("A Toy Slime");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 30;
            npc.lifeMax = 1600;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 2000;
                npc.damage = 35;
                npc.defense = 10;
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float spawnchance = Main.expertMode ? 0.006f : 0.005f;  
            MyPlayer modPlayer = spawnInfo.player.GetModPlayer<MyPlayer>();
            bool enoughStats = true;
            if (modPlayer.increasedToySlimeChance && !NPC.AnyNPCs(mod.NPCType("ToySlime")))
            {
                return 0.3f;
            }
            else
            {
                if (MyWorld.downedToySlime)
                {
                    return 0.0f;
                }
                if (spawnInfo.player.statDefense > 7 && spawnInfo.player.statLifeMax2 > 140)
                {
                    enoughStats = true;
                }
                else
                {
                    enoughStats = false;
                }
            }
            return (spawnInfo.spawnTileY < Main.rockLayer) && !spawnInfo.playerInTown && !Main.bloodMoon && !NPC.AnyNPCs(mod.NPCType("ToySlime")) && enoughStats ? spawnchance : 0f;
        }



        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, Main.npcTexture[npc.type].Height * 0.5f);
            SpriteEffects spriteEffects = npc.direction != 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            var origin = npc.frame.Size() * 0.5f;
            Color color = npc.GetAlpha(drawColor) * 0.5f;
            spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/ToySlime/ToySlimeArmor"), npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame, color, npc.rotation, origin, npc.scale * 0.9f, spriteEffects, 0);
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BrokenToys"), Main.rand.Next(20, 35));
            int item = Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Gel, Main.rand.Next(10, 35));
            Main.item[item].color = new Color(0, 220, 40, 100);
            MyWorld.downedToySlime = true;
        }

        public override void AI()
        {
            Player P = Main.player[npc.target];

            #region despawning
            if (!P.active || P.dead)
            {
                npc.TargetClosest(true);
                if (!P.active || P.dead)
                {
                    npc.localAI[0]++;
                }
            }
            if (P.active && !P.dead)
            {
                npc.localAI[0] = 0;
            }
            if (npc.localAI[0] >= 300)
            {
                npc.active = false;
            }
            #endregion

            float num234 = 1f;
            bool flag8 = false;
            bool hideSlime = false;
            if (npc.ai[3] == 0f && npc.life > 0)
            {
                npc.ai[3] = (float)npc.lifeMax;
            }
            if (npc.localAI[3] == 0f && Main.netMode != 1)
            {
                npc.ai[0] = -100f;
                npc.localAI[3] = 1f;
                npc.TargetClosest(true);
                npc.netUpdate = true;
            }
            if (!reset)
            {
                Main.PlaySound(15, (int)P.position.X, (int)P.position.Y, 0);
                Main.NewText(Language.GetTextValue("Announcement.HasAwoken", "A Toy Slime"), 175, 75, 255, false);
                reset = true;
            }
            // despawning
            if (Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    npc.timeLeft = 0;
                    if (Main.player[npc.target].Center.X < npc.Center.X)
                    {
                        npc.direction = 1;
                    }
                    else
                    {
                        npc.direction = -1;
                    }
                }
            }
            if (!Main.player[npc.target].dead && npc.ai[2] >= 300f && npc.ai[1] < 5f && npc.velocity.Y == 0f)
            {
                npc.ai[2] = 0f; // teleport timer
                npc.ai[0] = 0f; // jump timer
                npc.ai[1] = 5f; // ai 'phase' (whether jumping, teleporting etc), 5 is teleport
                // collision checking for teleport
                if (Main.netMode != 1)
                {
                    npc.TargetClosest(false);
                    Point npcTileCoord = npc.Center.ToTileCoordinates();
                    Point playerTileCoord = Main.player[npc.target].Center.ToTileCoordinates();
                    Vector2 vector30 = Main.player[npc.target].Center - npc.Center;
                    // collision checking
                    int num235 = 10;
                    int num236 = 0;
                    int num237 = 7;
                    int num238 = 0;
                    bool flag10 = false;
                    if (vector30.Length() > 2000f)
                    {
                        flag10 = true;
                        num238 = 100;
                    }
                    while (!flag10 && num238 < 100)
                    {
                        num238++;
                        int num239 = Main.rand.Next(playerTileCoord.X - num235, playerTileCoord.X + num235 + 1);
                        int num240 = Main.rand.Next(playerTileCoord.Y - num235, playerTileCoord.Y + 1);
                        if ((num240 < playerTileCoord.Y - num237 || num240 > playerTileCoord.Y + num237 || num239 < playerTileCoord.X - num237 || num239 > playerTileCoord.X + num237) && (num240 < npcTileCoord.Y - num236 || num240 > npcTileCoord.Y + num236 || num239 < npcTileCoord.X - num236 || num239 > npcTileCoord.X + num236) && !Main.tile[num239, num240].nactive())
                        {
                            int num241 = num240;
                            int num242 = 0;
                            bool flag11 = Main.tile[num239, num241].nactive() && Main.tileSolid[(int)Main.tile[num239, num241].type] && !Main.tileSolidTop[(int)Main.tile[num239, num241].type] && Main.tile[num239, num241].type != TileID.Rope;
                            if (flag11)
                            {
                                num242 = 1;
                            }
                            else
                            {
                                while (num242 < 150 && num241 + num242 < Main.maxTilesY)
                                {
                                    int num243 = num241 + num242;
                                    bool flag12 = Main.tile[num239, num243].nactive() && Main.tileSolid[(int)Main.tile[num239, num243].type] && !Main.tileSolidTop[(int)Main.tile[num239, num243].type];
                                    if (flag12)
                                    {
                                        num242--;
                                        break;
                                    }
                                    int num = num242;
                                    num242 = num + 1;
                                }
                            }
                            num240 += num242;
                            bool flag13 = true;
                            if (flag13 && Main.tile[num239, num240].lava())
                            {
                                flag13 = false;
                            }
                            if (flag13 && !Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
                            {
                                flag13 = false;
                            }
                            if (flag13)
                            {
                                npc.localAI[1] = (float)(num239 * 16 + 8);
                                npc.localAI[2] = (float)(num240 * 16 + 16);
                                break;
                            }
                        }
                    }
                    if (num238 >= 100)
                    {
                        Vector2 bottom = Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].Bottom;
                        npc.localAI[1] = bottom.X;
                        npc.localAI[2] = bottom.Y;
                    }
                }
            }
            if (!Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0)) // if the player is behind a wall
            {
                npc.ai[2]++;
                if (MyWorld.awakenedMode)
                {
                    npc.ai[2]++;
                }
            }
            if (Math.Abs(npc.Top.Y - Main.player[npc.target].Bottom.Y) > 320f) // if the players feet are 320 units above 
            {
                npc.ai[2]++;
                if (MyWorld.awakenedMode)
                {
                    npc.ai[2]++;
                }
            }
            if (MyWorld.awakenedMode && Main.time % 2 == 0)
            {
                npc.ai[2]++;
            }
            if (npc.ai[1] == 5f)
            {
                flag8 = true;
                npc.ai[0]++;

                num234 = MathHelper.Clamp((60f - npc.ai[0]) / 60f, 0f, 1f);
                num234 = 0.5f + num234 * 0.5f;
                if (npc.ai[0] >= 60f)
                {
                    hideSlime = true;
                }
                if (npc.ai[0] == 60f)
                {
                    // create slime crown
                    //Gore.NewGore(npc.Center + new Vector2(-40f, (float)(-(float)npc.height / 2)), npc.velocity, 734, 1f);
                }
                // teleport
                if (npc.ai[0] >= 60f && Main.netMode != 1)
                {
                    npc.Bottom = new Vector2(npc.localAI[1], npc.localAI[2]);
                    npc.ai[1] = 6f;
                    npc.ai[0] = 0f;
                    npc.netUpdate = true;
                }
                if (Main.netMode == 1 && npc.ai[0] >= 120f)
                {
                    npc.ai[1] = 6f;
                    npc.ai[0] = 0f;
                }
                if (!hideSlime)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(npc.position + Vector2.UnitX * -20f, npc.width + 40, npc.height, 4, npc.velocity.X, npc.velocity.Y, 150, new Color(0, 220, 40, 100), 2f)];
                        dust.noGravity = true;
                        dust.velocity *= 0.5f;
                    }
                }
            }
            else if (npc.ai[1] == 6f)
            {
                flag8 = true;
                npc.ai[0]++;

                num234 = MathHelper.Clamp(npc.ai[0] / 30f, 0f, 1f);
                num234 = 0.5f + num234 * 0.5f;
                if (npc.ai[0] >= 30f && Main.netMode != 1)
                {
                    npc.ai[1] = 0f;
                    npc.ai[0] = 0f;
                    npc.netUpdate = true;
                    npc.TargetClosest(true);
                }
                if (Main.netMode == 1 && npc.ai[0] >= 60f)
                {
                    npc.ai[1] = 0f;
                    npc.ai[0] = 0f;
                    npc.TargetClosest(true);
                }
                for (int k = 0; k < 10; k++)
                {
                    Dust dust = Main.dust[Dust.NewDust(npc.position + Vector2.UnitX * -20f, npc.width + 40, npc.height, 4, npc.velocity.X, npc.velocity.Y, 150, new Color(0, 220, 40, 100), 2f)];
                    dust.noGravity = true;
                    dust.velocity *= 2f;
                }
            }
            npc.dontTakeDamage = (npc.hide = hideSlime);
            if (npc.velocity.Y == 0f) // if its on the ground
            {
                npc.velocity.X = npc.velocity.X * 0.8f;
                if (npc.velocity.X > -0.1 && npc.velocity.X < 0.1)
                {
                    npc.velocity.X = 0f;
                }
                if (!flag8)
                {
                    // jumps faster when low health
                    npc.ai[0] += 2f;                   
                    if (npc.life < npc.lifeMax * 0.8)
                    {
                        npc.ai[0] += 1f;
                    }
                    if (npc.life < npc.lifeMax * 0.6)
                    {
                        npc.ai[0] += 1f;
                    }
                    if (npc.life < npc.lifeMax * 0.4)
                    {
                        npc.ai[0] += 2f;
                    }
                    if (npc.life < npc.lifeMax * 0.2)
                    {
                        npc.ai[0] += 3f;
                    }
                    if (npc.life < npc.lifeMax * 0.1)
                    {
                        npc.ai[0] += 4f;
                    }
                    if (npc.ai[0] >= 0f)
                    {
                        npc.netUpdate = true;
                        npc.TargetClosest(true);
                        if (npc.ai[1] == 3f) // regular jump
                        {
                            npc.velocity.Y = -13f;
                            npc.velocity.X = npc.velocity.X + 3.5f * npc.direction;
                            npc.ai[0] = -200f;
                            npc.ai[1] = 0f;
                        }
                        else if (npc.ai[1] == 2f) // small jump
                        {
                            npc.velocity.Y = -6f;
                            npc.velocity.X = npc.velocity.X + 4.5f * npc.direction;
                            npc.ai[0] = -120f;
                            npc.ai[1] += 1f;
                        }
                        else // big jump
                        {
                            npc.velocity.Y = -8f;
                            npc.velocity.X = npc.velocity.X + 4f * npc.direction;
                            npc.ai[0] = -120f;
                            npc.ai[1] += 1f;
                        }
                    }
                }
            }
            else if (npc.target < 255 && ((npc.direction == 1 && npc.velocity.X < 3f) || (npc.direction == -1 && npc.velocity.X > -3f)))
            {
                if ((npc.direction == -1 && npc.velocity.X < 0.1) || (npc.direction == 1 && npc.velocity.X > -0.1))
                {
                    npc.velocity.X = npc.velocity.X + 0.2f * (float)npc.direction;
                }
                else
                {
                    npc.velocity.X = npc.velocity.X * 0.93f;
                }
            }
            Dust dust1 = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 4, npc.velocity.X, npc.velocity.Y, 255, new Color(0, 220, 40, 100), npc.scale * 1.2f)];
            dust1.noGravity = true;
            dust1.velocity *= 0.5f;
            if (npc.life > 0)
            {
                float npcLife = (float)npc.life / (float)npc.lifeMax;
                npcLife = npcLife * 0.5f + 0.75f;
                npcLife *= num234;
                if (npcLife != npc.scale)
                {
                    npc.position.X = npc.position.X + (float)(npc.width / 2);
                    npc.position.Y = npc.position.Y + (float)npc.height;
                    npc.scale = npcLife;
                    npc.width = (int)(74f * npc.scale);
                    npc.height = (int)(50f * npc.scale);
                    npc.position.X = npc.position.X - (float)(npc.width / 2);
                    npc.position.Y = npc.position.Y - (float)npc.height;
                }
            }
            // spawn slimes
            Vector2 spawnAt = npc.Center + new Vector2(0f, (float)npc.height / 2f);
            int type = NPCID.GreenSlime;
            int spawnRate = (int)(npc.lifeMax * 0.05);
            if (npc.life + spawnRate < npc.ai[3])
            {
                npc.ai[3] = npc.life; // assign the current life
                int numSlimes = Main.rand.Next(1, 4);
                if (Main.expertMode)
                {
                    numSlimes += Main.rand.Next(1, 2);
                }
                for (int i = 0; i < numSlimes; i++)
                {
                    int slime = NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, type);
                }
            }
            if (Main.expertMode)
            {
                // ai[0] && [1] are being used in slime ai
                // ai[0] is jump timer
                brickTimer--;
                if (npc.life < npc.lifeMax * 0.75)
                {
                    brickTimer--;
                }
                if (npc.life < npc.lifeMax * 0.5)
                {
                    brickTimer--;
                }
                if (MyWorld.awakenedMode)
                {
                    if (npc.life < npc.lifeMax * 0.25)
                    {
                        brickTimer--;
                    }
                    if (npc.life < npc.lifeMax * 0.1)
                    {
                        brickTimer--;
                    }
                }
                if (brickTimer <= 0)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 10, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-6, -2), mod.ProjectileType("LegoBrick"), projectileBaseDamage, 0f, 0, 0, 0);
                    }
                    brickTimer = Main.rand.Next(200, 350);
                    if (MyWorld.awakenedMode)
                    {
                        brickTimer = Main.rand.Next(150, 300);
                    }
                }
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 4, hitDirection, -1f, 0, new Color(0, 220, 40, 100), 1f);
            }
            if (npc.life <= 0)
            {
                for (int k = 0; k < 50; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 4, (float)(2 * hitDirection), -2f, npc.alpha, new Color(0, 220, 40, 100), 1f);
                }
            }
        }
    }
}