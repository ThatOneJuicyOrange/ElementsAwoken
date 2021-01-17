using ElementsAwoken.NPCs.MovingPlatforms;
using ElementsAwoken.Projectiles.NPCProj.Obsidious;
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
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.NPCs.Bosses.Obsidious
{
    [AutoloadBossHead]
    public class Obsidious : ModNPC
    {
        private float aiTimer3 = 0;
        private float aiTimer4 = 0;
        private float platformTimer = 0;
        private List<int> arenaBlocks = new List<int> { TileType<Tiles.ObsidiousArenaManager>(), TileType<Tiles.ObsidiousTempBlock>(), TileType<Tiles.ObsidiousBrick>(), TileType<Tiles.ObsidiousArenaManager>() };
        private Vector2 arenaCenter = Vector2.Zero;
        private float flipGrav
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float aiState
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float aiTimer
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float aiTimer2
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public enum ObsidiousPhase : int 
        {
            Spawn = 0,
            Vibe = 1,
            Intro = 2,
            Slide = 3,
            Fireballs = 4,
            Slam = 5,
            FirePillars = 6,
            Voidballs = 7,
            Dropper = 8,
            Deathray = 9
        };
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidious");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.width = 148;
            npc.height = 148;

            npc.scale *= 1.22f;
            npc.aiStyle = -1;

            npc.lifeMax = 75000;
            npc.damage = 75;
            npc.defense = 55;
            npc.knockBackResist = 0f;
            npc.alpha = 255;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.netAlways = true;
            npc.gfxOffY = -4;

            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;

            npc.value = Item.buyPrice(0, 20, 0, 0);
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ObsidiousTheme");

            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;

            bossBag = mod.ItemType("ObsidiousBag");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 90;
            npc.lifeMax = 100000;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 150000;
                npc.damage = 110;
                npc.defense = 65;
            }
        }
        public override void NPCLoot()
        {
            RemoveTempBlocks();
            GravityPlayer gravPlayer = Main.LocalPlayer.GetModPlayer<GravityPlayer>();
            gravPlayer.forceGrav = 1;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void FindFrame(int frameHeight)
        {
            if (npc.life < npc.lifeMax * 0.5f) npc.frame.Y = frameHeight;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = mod.GetTexture("NPCs/Bosses/Obsidious/" + GetType().Name + "_Glow");
            Rectangle frame = new Rectangle(0, npc.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[npc.type]);
            Vector2 origin = new Vector2(texture.Width * 0.5f, (texture.Height / Main.npcFrameCount[npc.type]) * 0.5f);
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, 4), frame, Color.White * (1 - ((float)npc.alpha / 255)), npc.rotation, origin, npc.scale, effects, 0.0f);
        }
        public override void AI()
        {
            if (arenaCenter == Vector2.Zero) arenaCenter = npc.Center;
            npc.TargetClosest(false);
            Player player = Main.player[npc.target];
            Point playerWorld = (player.Center / 16).ToPoint();
            //Despawn(player);
            Rectangle arena = new Rectangle(EAWorldGen.obsidiousTempleLoc.X + 33, EAWorldGen.obsidiousTempleLoc.Y + 2, 83, 65); // not fixed for flipping
            Rectangle arenaJustAir = new Rectangle(EAWorldGen.obsidiousTempleLoc.X + 35, EAWorldGen.obsidiousTempleLoc.Y + 5, 79, 61); // not fixed for flipping
            /*for (int i = 0; i < 160; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(arenaJustAir.TopLeft() * 16, arenaJustAir.Width * 16, arenaJustAir.Height * 16, 6)];
                dust.noGravity = true;
                dust.scale *= 1.6f;
                dust.velocity *= 0;
            }*/
            if (npc.life < npc.lifeMax / 2)
            {
                platformTimer++;
                if (platformTimer >= 1200)
                {
                    List<int> ids = new List<int>();
                    for (int p = 0; p < Main.maxNPCs; p++)
                    {
                        NPC other = Main.npc[p];
                        if (other.active && other.type == NPCType<ObsidiousPlatform>())
                        {
                            ids.Add(other.whoAmI);
                        }
                    }
                    int num = Main.expertMode ? MyWorld.awakenedMode ? 3 : 2 : 1;
                    for (int p = 0; p < num; p++)
                    {
                        int chosen = Main.rand.Next(ids);
                        Main.npc[chosen].ai[3] = 3;
                        ids.Remove(chosen);
                    }
                    platformTimer = 0;
                }
                for (int p = 0; p < Main.maxNPCs; p++)
                {
                    NPC other = Main.npc[p];
                    if (other.active && other.type == NPCType<ObsidiousPlatform>())
                    {
                        if (other.ai[3] != 1) break;
                        other.ai[3] = 2; 
                    }
                }
            }
            float slideSpeed = 20;
            float gravFlipTimerMax = 180;
            float acceleration = MathHelper.Lerp(0.4f, 0.2f, MathHelper.Clamp((float)npc.life / (float)npc.lifeMax, 0, 1)) * (Main.expertMode ? MyWorld.awakenedMode ? 1.5f : 1.2f : 1f);
            if (arena.Contains(playerWorld) && !NPC.downedPlantBoss && !GetInstance<Config>().debugMode)
            {
                aiState = 9999;
            }
            if (aiState >= 3)
            {
                if (!arena.Contains(playerWorld))
                {
                    npc.alpha += 10;
                    if (npc.alpha >= 255)
                    {
                        npc.active = false;
                        RemoveTempBlocks();
                    }
                }
                if (aiState > 3 && arena.Contains(npc.Center.ToTileCoordinates())) // break blocks when not in slide phase
                {
                    Rectangle breakBlocks = new Rectangle((int)npc.position.X / 16 - 1, (int)npc.position.Y / 16 - 1, npc.width / 16 + 2, npc.height / 16 + 2);
                    for (int i = breakBlocks.X; i < breakBlocks.X + breakBlocks.Width; i++)
                    {
                        for (int j = breakBlocks.Y; j < breakBlocks.Y + breakBlocks.Height; j++)
                        {
                            Tile t = Framing.GetTileSafely(i, j);
                            if (!arenaBlocks.Contains(t.type))
                            {
                                WorldGen.KillTile(i, j);
                            }
                        }
                    }
                }
            }
            if (aiState == (int)ObsidiousPhase.Spawn) // spawn
            {
                npc.boss = false;
                music = MusicID.Plantera;
                npc.GivenName = " ";
                int toRoof = 478;
                int toWall = 620;
                int apart = 200;

                int centerID = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<ObsidiousArenaCrystal>());
                NPC center = Main.npc[centerID];
                center.ai[0] = npc.whoAmI;
                center.Center = npc.Center;

                CreateCrystal(new Vector2(apart, toRoof), 0, centerID);
                CreateCrystal(new Vector2(-apart, toRoof), 0, centerID);

                CreateCrystal(new Vector2(apart, -toRoof), 3.14f, centerID);
                CreateCrystal(new Vector2(-apart, -toRoof), 3.14f, centerID);

                CreateCrystal(new Vector2(toWall, apart), 4.71f, centerID);
                CreateCrystal(new Vector2(toWall, -apart), 4.71f, centerID);

                CreateCrystal(new Vector2(-toWall, apart), 1.57f, centerID);
                CreateCrystal(new Vector2(-toWall, -apart), 1.57f, centerID);
                aiState = 1;
            }
            else if (aiState == (int)ObsidiousPhase.Vibe)
            {
                npc.immortal = true;
                npc.dontTakeDamage = true;
                if (!NPC.AnyNPCs(NPCType<ObsidiousWallCrystal>()))
                {
                    npc.immortal = false;
                    npc.dontTakeDamage = false;
                    npc.boss = true;
                    float orbitalcount = 16;
                    for (int l = 0; l < orbitalcount; l++)
                    {
                        float distance = 360f / orbitalcount;
                        NPC plat = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<ObsidiousPlatform>(), npc.whoAmI, l * distance)];
                        ObsidiousPlatform obbyPlat = (ObsidiousPlatform)plat.modNPC;
                        obbyPlat.arenaMiddle = new Vector2(npc.Center.X, npc.Center.Y);
                    }
                    Main.PlaySound(SoundLoader.customSoundType, (int)npc.position.X, (int)npc.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/NPC/ObsidiousIntro"));
                    aiState = 2;
                    ReconstructArena();
                }
            }
            else if (aiState == (int)ObsidiousPhase.Intro)
            {
                npc.GivenName = "";
                if (npc.alpha > 0) npc.alpha -= 10;
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    aiTimer2++;
                    if (aiTimer2 > 180)
                    {
                        aiTimer++;
                        if (aiTimer > 5 && Math.Abs(npc.velocity.Y) < 0.3f)
                        {
                            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 70, pitchOffset: -0.5f);
                            Main.LocalPlayer.GetModPlayer<MyPlayer>().screenshakeAmount = 4;
                            aiTimer = 0;
                            aiTimer2 = 0;
                            aiState++;
                            aiState = (int)ObsidiousPhase.Dropper;
                        }
                        if (npc.velocity.Y < slideSpeed) npc.velocity.Y += 0.2f;
                    }
                }
                else
                {
                    aiTimer++;
                    if (aiTimer > 5 && Math.Abs(npc.velocity.Y) < 0.3f)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 70, pitchOffset: -0.5f);
                        Main.LocalPlayer.GetModPlayer<MyPlayer>().screenshakeAmount = 4;
                        aiTimer = 0;
                        aiState++;
                    }
                    if (npc.velocity.Y < slideSpeed) npc.velocity.Y += 0.2f;
                }
            }
            else if (aiState == (int)ObsidiousPhase.Slide)
            {
                PushPlayer(npc);
                if (aiTimer2 == 0 || aiTimer2 == 4)
                {
                    Slide(new Vector2(-slideSpeed, 0), arenaJustAir, acceleration);
                }
                else if (aiTimer2 == 1 || aiTimer2 == 5)
                {
                    Slide(new Vector2(0, -slideSpeed), arenaJustAir, acceleration);
                }
                else if (aiTimer2 == 2 || aiTimer2 == 6)
                {
                    Slide(new Vector2(slideSpeed, 0), arenaJustAir, acceleration);
                }
                else if (aiTimer2 == 3 || aiTimer2 == 7 || aiTimer2 == -1)
                {
                    Slide(new Vector2(0, slideSpeed), arenaJustAir,acceleration);
                }
            }
            else if (aiState == (int)ObsidiousPhase.Fireballs)
            {
                aiTimer2++;
                Vector2 toTarget = new Vector2(player.Center.X - npc.Center.X - 300, player.Center.Y - npc.Center.Y);
                toTarget.Normalize();
                npc.velocity = toTarget * 4;
                if (aiTimer == 0)
                {
                    Main.PlaySound(SoundID.DD2_BetsyWindAttack, npc.position);
                    float orbitalcount = 8;
                    for (int l = 0; l < orbitalcount; l++)
                    {
                        float distance = 360f / orbitalcount;
                        Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, ProjectileType<ObsidiousFireball>(), npc.damage / 2, 1, Main.myPlayer, npc.whoAmI, l * distance)];
                    }
                }
                aiTimer++;
                if (aiTimer > 300) aiTimer = 0;
                if (aiTimer2 > 900)
                {
                    aiTimer = 0;
                    aiTimer2 = 0;
                    aiTimer3 = 0;
                    aiTimer4 = 0;
                    aiState++;
                }
            }
            else if (aiState == (int)ObsidiousPhase.Slam) // slam from above
            {
                aiTimer4++;
                if (aiTimer == 0)
                {
                    aiTimer2 = player.Center.X;
                    aiTimer = 1;
                }
                else if (aiTimer == 1)
                {
                    aiTimer3++;
                    if (aiTimer2 == 0) aiTimer = 0;
                    Vector2 abovePlayer = new Vector2(aiTimer2, player.Center.Y - 500);
                    Vector2 toTarget = new Vector2(abovePlayer.X - npc.Center.X, abovePlayer.Y - npc.Center.Y);
                    toTarget.Normalize();
                    npc.velocity.X = toTarget.X * 13;
                    npc.velocity.Y = toTarget.Y * 13 * MathHelper.Clamp(MathHelper.Distance(abovePlayer.Y, npc.Center.Y) / 500, 1, 2);

                    bool cantReach = (Collision.SolidCollision(abovePlayer - npc.Size / 2, npc.width, npc.height) || !arena.Contains(abovePlayer.ToTileCoordinates())) && Collision.SolidCollision(npc.position - Vector2.One * 10, npc.width + 20, npc.height + 20) && aiTimer3 > 40;
                    if (Vector2.Distance(npc.Center, abovePlayer) < 50 || cantReach)
                    {
                        aiTimer = 2;
                        aiTimer2 = 0;
                        aiTimer3 = 0;
                        npc.velocity = Vector2.Zero;
                    }
                }
                else if (aiTimer == 2)
                {
                    aiTimer2++;
                    if (aiTimer2 > 10 && Math.Abs(npc.velocity.Y) < 0.3f)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 70, pitchOffset: -0.5f);
                        Main.LocalPlayer.GetModPlayer<MyPlayer>().screenshakeAmount = 8;
                        aiTimer2 = 0;
                        aiTimer = 0;
                    }
                    float accel = Main.expertMode ? MyWorld.awakenedMode ? 0.8f : 0.6f : 0.4f;
                    if (npc.velocity.Y < slideSpeed) npc.velocity.Y += accel;
                }
                if (aiTimer4 > 600)
                {
                    aiTimer = 0;
                    aiTimer2 = 0;
                    aiTimer3 = 0;
                    aiTimer4 = 0;
                    aiState++;

                }
            }
            else if (aiState == (int)ObsidiousPhase.FirePillars)
            {
                if (aiTimer == 0)
                {
                    aiTimer2 = player.Center.X;
                    aiTimer = 1;
                }
                else if (aiTimer == 1)
                {
                    aiTimer3++;
                    if (aiTimer2 == 0) aiTimer = 0;
                    Vector2 toTarget = new Vector2(arenaCenter.X - npc.Center.X, arenaCenter.Y - npc.Center.Y);
                    toTarget.Normalize();
                    npc.velocity = toTarget * 13;

                    if (Vector2.Distance(npc.Center, arenaCenter) < 20)
                    {
                        aiTimer = 2;
                        aiTimer2 = 0;
                        aiTimer3 = 0;
                        npc.velocity = Vector2.Zero;
                    }
                }
                else if (aiTimer == 2)
                {
                    aiTimer2++;
                    if (aiTimer2 > 10 && Math.Abs(npc.velocity.Y) < 0.3f)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 70, pitchOffset: -0.5f);
                        Main.LocalPlayer.GetModPlayer<MyPlayer>().screenshakeAmount = 4;
                        npc.velocity = Vector2.Zero;
                        aiTimer = 3;
                        aiTimer2 = 0;
                        aiTimer3 = 0;
                        for (int j = 0; j < 60; j++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(npc.BottomLeft, npc.width, 2, 6)];
                            dust.velocity.X = dust.position.X > npc.Center.X ? 16 : -16;
                            dust.velocity.X *= Main.rand.NextFloat(0.4f, 1f);
                            dust.velocity.Y = Main.rand.NextFloat(-4f, 0f);
                            dust.noGravity = true;
                            dust.fadeIn = 1f;
                            dust.scale *= 3f;
                        }
                    }
                    float accel = Main.expertMode ? MyWorld.awakenedMode ? 0.8f : 0.6f : 0.4f;
                    if (npc.velocity.Y < slideSpeed) npc.velocity.Y += accel;
                }
                else if (aiTimer == 3)
                {
                    aiTimer2++;
                    int numSpikes = Main.expertMode ? MyWorld.awakenedMode ? 12 : 10 : 8;
                    float distance = 580f / (float)(numSpikes / 2);
                    if (aiTimer3 * 2 >= numSpikes)
                    {
                        aiTimer = 4;
                        aiTimer2 = 0;
                        aiTimer3 = 0;
                    }

                    int height = 45;
                    if (npc.life < npc.lifeMax * 0.5f)
                        height = 21;
                    if (aiTimer2 == 1 && aiTimer3 == 0)
                    {
                        if (npc.life < npc.lifeMax * 0.5f)
                        {
                            Projectile proj2 = Main.projectile[Projectile.NewProjectile(npc.Center.X, (arena.Y + 3) * 16, 0, 0, ProjectileType<ObsidiousPillarSpawner2>(), npc.damage / 2, 1, Main.myPlayer, 0, 1)];
                            proj2.localAI[1] = height;
                        }
                        Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Bottom.X, npc.Bottom.Y, 0, 0, ProjectileType<ObsidiousPillarSpawner>(), npc.damage / 2, 1, Main.myPlayer, 0)];
                        proj.localAI[1] = height;

                    }
                    if (aiTimer2 >= 60 / (numSpikes / 2))
                    {
                        aiTimer2 = 0;
                        aiTimer3++;
                        for (int l = -1; l <= 1; l += 2)
                        {
                            float xPos = npc.Center.X + aiTimer3 * (float)l * (float)distance;
                            if (npc.life < npc.lifeMax * 0.5f)
                            {
                                Projectile proj2 = Main.projectile[Projectile.NewProjectile(xPos, (arena.Y + 3) * 16, 0, 0, ProjectileType<ObsidiousPillarSpawner2>(), npc.damage / 2, 1, Main.myPlayer, 0, 1)];
                                proj2.localAI[1] = height;
                            }
                            Projectile proj = Main.projectile[Projectile.NewProjectile(xPos, npc.Bottom.Y, 0, 0, ProjectileType<ObsidiousPillarSpawner>(), npc.damage / 2, 1, Main.myPlayer, 0)];
                            proj.Bottom = new Vector2(xPos, npc.Bottom.Y + 10);
                            proj.localAI[1] = height;
                        }
                    }
                }
                else if (aiTimer == 4)
                {
                    if (npc.velocity.Y > -slideSpeed) npc.velocity.Y -= 0.2f;
                    aiTimer2++;
                    if (npc.Center.Y < arenaCenter.Y || aiTimer2 > 300)
                    {
                        aiTimer = 0;
                        aiTimer2 = 0;
                        aiTimer3 = 0;
                        aiState++;
                    }

                }
            }
            else if (aiState == (int)ObsidiousPhase.Voidballs)
            {
                if (aiTimer2 == 0)
                {
                    Vector2 toTarget = new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y);
                    toTarget.Normalize();
                    npc.velocity = toTarget * 4;

                    aiTimer++;
                    if (aiTimer % 60 == 0)
                    {
                        Vector2 speed = new Vector2(6, 0).RotatedByRandom(MathHelper.ToRadians(360));
                        float projSpeed = Main.expertMode ? MyWorld.awakenedMode ? 20 : 16 : 10;
                        Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center, speed, ProjectileType<ObsidiousFireballVoid>(), npc.damage / 2, 1, Main.myPlayer, npc.whoAmI, projSpeed)];
                        Main.PlaySound(SoundID.DD2_GhastlyGlaiveImpactGhost, npc.position);
                    }
                    if (aiTimer > 600)
                    {
                        aiTimer2 = 1;
                        aiTimer = 0;
                        npc.velocity = Vector2.Zero;
                    }
                }
                else if (aiTimer2 == 1)
                {
                    aiTimer++;
                    if (aiTimer > 10 && Math.Abs(npc.velocity.X) < 0.3f)
                    {
                        aiTimer2 = 2;
                        aiTimer = 0;

                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 70, pitchOffset: -0.5f);
                        Main.LocalPlayer.GetModPlayer<MyPlayer>().screenshakeAmount = 4;
                    }
                    if (npc.velocity.X < slideSpeed) npc.velocity.X += 0.6f;
                }
                else if (aiTimer2 == 2)
                {
                    aiTimer++;
                    if (aiTimer > 10 && Math.Abs(npc.velocity.X) < 0.3f)
                    {
                        aiTimer2 = 0;
                        aiTimer = 0;
                        aiState++;

                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 70, pitchOffset: -0.5f);
                        Main.LocalPlayer.GetModPlayer<MyPlayer>().screenshakeAmount = 4;
                    }
                    if (aiTimer % 10 == 0)
                    {
                        Vector2 speed = new Vector2(6, 0).RotatedByRandom(MathHelper.ToRadians(360));
                        float projSpeed = Main.expertMode ? MyWorld.awakenedMode ? 12 : 10 : 6;
                        Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center, speed, ProjectileType<ObsidiousFireballVoid>(), npc.damage / 2, 1, Main.myPlayer, npc.whoAmI, projSpeed)];
                        Main.PlaySound(SoundID.DD2_GhastlyGlaiveImpactGhost, npc.position);
                    }
                    if (npc.velocity.X > -slideSpeed) npc.velocity.X -= 0.2f;
                }
            }
            else if (aiState == (int)ObsidiousPhase.Dropper)
            {
                if (aiTimer == 0)
                {
                    if (flipGrav > 0) flipGrav = -gravFlipTimerMax;
                    else flipGrav = gravFlipTimerMax;
                    aiTimer = 1;
                }
                else if (aiTimer == 1)
                {
                    aiTimer2++;
                    if (aiTimer2 >= 120)
                    {
                        int num = Main.expertMode ? MyWorld.awakenedMode ? 6 : 5 : 3;
                        for (int i = 0; i < num; i++)
                        {
                            for (int l = -1; l <= 1; l += 2)
                            {
                                Projectile.NewProjectile(arenaCenter, new Vector2(l * i * (38f / num), 0), ProjectileType<ObsidiousHoverRock>(), npc.damage / 2, 1, Main.myPlayer, Math.Sign(flipGrav));
                            }
                        }
                        aiTimer = 2;
                        aiTimer2 = 0;
                    }
                }
                else if (aiTimer == 2)
                {
                    aiTimer2++;
                    if (aiTimer2 > 300)
                    {
                        aiTimer = 0;
                        aiTimer2 = 0;
                        aiTimer3 = 0;
                        aiTimer4 = 0;
                        aiState = 3;
                        aiState = (int)ObsidiousPhase.Dropper;
                    }
                }
                if (aiState == 8) // to make it not do it after the ai has been switched
                {
                    aiTimer4++;
                    if (aiTimer4 > 30 && Math.Abs(npc.velocity.Y) < 0.1f)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 70, pitchOffset: -0.5f);
                        Main.LocalPlayer.GetModPlayer<MyPlayer>().screenshakeAmount = 4;
                        aiTimer4 = 0;
                        aiTimer3++;
                        if (aiTimer3 > 1) aiTimer3 = 0;
                    }
                    if (npc.velocity.Y < slideSpeed && aiTimer3 == 0) npc.velocity.Y += acceleration;
                    if (npc.velocity.Y > -slideSpeed && aiTimer3 == 1) npc.velocity.Y -= acceleration;
                }
            }
            else if (aiState == (int)ObsidiousPhase.Deathray)
            {
            }
            else if (aiState == 10)
            {

            }
            else if (aiState == 11)
            {
            }
            if (flipGrav > 1)
            {
                if (flipGrav == gravFlipTimerMax) Projectile.NewProjectile((arena.X + 41) * 16 + 8, (arena.Y + 64) * 16, 0, 0, ProjectileType<ObsidiousGravitySwitcher>(), 0, 0, Main.myPlayer);
                if (flipGrav == 2)
                {
                    if (arena.Contains(Main.LocalPlayer.Center.ToTileCoordinates()))
                    {
                        GravityPlayer gravPlayer = Main.LocalPlayer.GetModPlayer<GravityPlayer>();
                        gravPlayer.forceGrav = -1;
                        if (aiState == 8)
                        {
                            player.wingTime = 0;
                            player.wingTimeMax = 0;
                            player.velocity.Y = -1f;
                            gravPlayer.noWings = true;
                        }
                    }
                }
                flipGrav--;
            }
            else if (flipGrav < -1)
            {
                if (flipGrav == -gravFlipTimerMax) Projectile.NewProjectile((arena.X + 41) * 16 + 8, (arena.Y + 3) * 16, 0, 0, ProjectileType<ObsidiousGravitySwitcher>(), 0, 0, Main.myPlayer, 0, 1);
                if (flipGrav == -2)
                {
                    if (arena.Contains(Main.LocalPlayer.Center.ToTileCoordinates()))
                    {
                        GravityPlayer gravPlayer = Main.LocalPlayer.GetModPlayer<GravityPlayer>();
                        gravPlayer.forceGrav = 1;
                        if (aiState == 8)
                        {
                            player.wingTime = 0;
                            player.wingTimeMax = 0;
                            player.velocity.Y = 1f;
                        }
                    }
                }
                flipGrav++;
            } 
            if (Main.mouseRight && Main.mouseRightRelease && false)
            {
                if (flipGrav >= 0) flipGrav = -gravFlipTimerMax;
                else flipGrav = gravFlipTimerMax;
            }
            if (aiState == 999) // just smashed crystal
            {
                npc.velocity *= 0.97f;
                if (Vector2.Distance(npc.velocity, Vector2.Zero) < 0.3f)
                {
                    npc.velocity = Vector2.Zero;
                    aiTimer = 0;
                    aiTimer2 = 0;
                    aiState = 4;
                }
            }
            else if (aiState == 5000) // spared
            {
                npc.damage = 0;
                aiTimer++;
                npc.velocity.X *= 0.96f;
                if (aiTimer2 > 10 && Math.Abs(npc.velocity.Y) < 0.3f)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 70, pitchOffset: -0.5f);
                    Main.LocalPlayer.GetModPlayer<MyPlayer>().screenshakeAmount = 8;

                    Gore.NewGore(npc.position, new Vector2(-1,-1), mod.GetGoreSlot("Gores/Obsidious0"), npc.scale);
                    Gore.NewGore(npc.position, new Vector2(1, -1), mod.GetGoreSlot("Gores/Obsidious1"), npc.scale);
                }
                npc.velocity.Y += 0.16f;
            }
            else if (aiState == 9999) // entering before they are supposed to
            {
                npc.alpha = 0;
                if (!player.dead)
                {
                    Vector2 toTarget = new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y);
                    toTarget.Normalize();
                    npc.velocity = toTarget * 30;
                }
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (aiState == 9999)
            {
                Main.PlaySound(SoundLoader.customSoundType, (int)npc.position.X, (int)npc.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/NPC/GetDunkedOn"));
                target.KillMe(PlayerDeathReason.ByCustomReason(target.name + "  was crushed by Obsidious, the absolute unit."), 99999, 0);
                CombatText.NewText(npc.getRect(), Color.Red, "thwomped", true, false);
            }
        }
        private void CreateCrystal(Vector2 offset, float rot, int parent)
        {
            NPC chris = Main.npc[NPC.NewNPC((int)(npc.Center.X + offset.X), (int)(npc.Center.Y + offset.Y), NPCType<ObsidiousWallCrystal>())];
            chris.rotation = rot;
            chris.ai[0] = parent;
            chris.Center = npc.Center + offset;
        }
        private void ReconstructArena()
        {
            int width = 89;
            int height = 71;
            int x = EAWorldGen.obsidiousTempleLoc.X + 30;
            int y = EAWorldGen.obsidiousTempleLoc.Y;
            Rectangle arenaMiddle = new Rectangle(x + 5, y + 5, width - 10, height - 10);

            for (int i = x; i < x + width; i++)
            {
                for (int j = y; j < y + height; j++)
                {
                    if (arenaMiddle.Contains(new Point(i, j)))
                    {
                        WorldGen.KillTile(i, j);
                    }
                    else
                    {
                        Tile t = Framing.GetTileSafely(i, j);
                        if (!t.active() || t.type != TileType<Tiles.ObsidiousBrick>() && t.type != TileType<Tiles.ObsidiousArenaManager>())
                        {
                            WorldGen.KillTile(i, j);
                            WorldGen.PlaceTile(i, j, TileType<Tiles.ObsidiousTempBlock>());
                        }
                    }
                }
            }
        }
        private static void RemoveTempBlocks()
        {
            int width = 89;
            int height = 71;
            int x = EAWorldGen.obsidiousTempleLoc.X + 30;
            int y = EAWorldGen.obsidiousTempleLoc.Y;

            for (int i = x; i < x + width; i++)
            {
                for (int j = y; j < y + height; j++)
                {
                    Tile t = Framing.GetTileSafely(i, j);
                    if (t.type == TileType<Tiles.ObsidiousTempBlock>())
                    {
                        WorldGen.KillTile(i, j);
                    }
                }
            }
        }
    
        public static void PushPlayer(NPC npc)
        {
            Player player = Main.LocalPlayer;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (!Collision.SolidCollision(player.position, player.width, player.height))
            {
                int width = (int)Math.Abs(player.velocity.X) + 8;
                Rectangle playerRect = new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height);
                Rectangle npcRectLeft = new Rectangle((int)npc.position.X + (int)npc.velocity.X, (int)npc.position.Y + 4, width, npc.height - 4);
                Rectangle npcRectRight = new Rectangle((int)npc.Right.X + (int)npc.velocity.X - width, (int)npc.position.Y + 4, width, npc.height - 4);

                /*for (int j = 0; j < 10; j++)
                {
                    Dust dust = Main.dust[Dust.NewDust(npcRectLeft.TopLeft(), npcRectLeft.Width, npcRectLeft.Height, DustID.PinkFlame)];
                    dust.velocity = Vector2.Zero;
                    dust.noGravity = true;
                    Dust dust2 = Main.dust[Dust.NewDust(npcRectRight.TopLeft(), npcRectRight.Width, npcRectRight.Height, 6)];
                    dust2.velocity = Vector2.Zero;
                    dust2.noGravity = true;
                }*/

                if (playerRect.Intersects(npcRectLeft))
                {
                    //if (player.velocity.X >= 0 || npc.velocity.X > player.velocity.X)
                    {
                        if (player.velocity.X >= 0) player.velocity.X = 0;
                        player.position.X = npc.position.X - player.width;
                        player.position.X += npc.position.X - npc.oldPosition.X;
                        player.dashDelay = 0;
                        player.dashTime = 0;
                        modPlayer.eaDashDelay = 0;
                        modPlayer.eaDashTime = 0;
                    }
                }
                if (playerRect.Intersects(npcRectRight))
                {
                    //if (player.velocity.X <= 0 || npc.velocity.X < player.velocity.X)
                    {
                        if (player.velocity.X <= 0) player.velocity.X = 0;
                        player.position.X = npc.Right.X;
                        player.position.X += npc.position.X - npc.oldPosition.X;
                        player.dashDelay = 0;
                        player.dashTime = 0;
                        modPlayer.eaDashDelay = 0;
                        modPlayer.eaDashTime = 0;
                    }
                }
            }
        }
        private void Slide(Vector2 speed, Rectangle arenaAir, float acceleration = 0.02f)
        {
            // to stop the absolute CRETIN clipping into the walls becuase normal collision just DOESNT WORK?
            npc.noTileCollide = true;
            if (Collision.SolidCollision(npc.position, npc.width, npc.height))
            {
                int trials = 0;
                while (Collision.SolidCollision(npc.position, npc.width, npc.height) && trials < 20)
                {
                    trials++;
                    Vector2 push = Vector2.Zero;
                    if (npc.position.Y < arenaAir.Y * 16) push.Y += 2; // top
                    else if (npc.Bottom.Y > (arenaAir.Y + arenaAir.Height) * 16) push.Y -= 2; // bottom
                    if (npc.position.X < arenaAir.X * 16) push.X += 2; // left
                    else if (npc.Right.X  > (arenaAir.X + arenaAir.Width) * 16) push.X -= 2; // right

                    npc.velocity = Vector2.Zero;
                    Vector2 fix = push; // -speed
                    fix.Normalize();
                    npc.position += fix;
                }
                //ElementsAwoken.DebugModeText("stuck in wall- fixing");
            }
            else
            {
                aiTimer++;
                if (npc.soundDelay == 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/StoneSlide"));
                    npc.soundDelay = 999;
                }
                if (aiTimer > 55 && (Math.Abs(npc.velocity.X) < 0.3f && speed.X != 0 || Math.Abs(npc.velocity.Y) < 0.3f && speed.Y != 0))
                {
                    if (aiTimer2 == 0)
                    {
                        if (aiTimer3 == 0)
                        {
                            int illusionType = Main.rand.Next(2);
                            if (npc.life < npc.lifeMax * 0.5f)
                            {
                                NPC illusion = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<ObsidiousIllusion>())];
                                illusion.ai[0] = npc.whoAmI;
                                illusion.TopRight = npc.BottomLeft + new Vector2(1264, -976);
                                illusion.ai[2] = illusionType;
                                illusion.ai[1] = 0;

                                NPC illusion2 = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<ObsidiousIllusion>())];
                                illusion2.ai[0] = npc.whoAmI;
                                illusion2.TopLeft = npc.BottomLeft + new Vector2(0, -976);
                                illusion2.ai[2] = illusionType;
                                illusion2.ai[1] = 1;

                                NPC illusion3 = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<ObsidiousIllusion>())];
                                illusion3.ai[0] = npc.whoAmI;
                                illusion3.BottomRight = npc.BottomLeft + new Vector2(1264, 0);
                                illusion3.ai[2] = illusionType;
                                illusion3.ai[1] = 2;
                            }
                            else if (npc.life < npc.lifeMax * 0.75f)
                            {
                                NPC illusion = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<ObsidiousIllusion>())];
                                illusion.ai[0] = npc.whoAmI;
                                illusion.TopRight = npc.BottomLeft + new Vector2(1264, -976);
                                illusion.ai[2] = illusionType;
                                illusion.ai[1] = 0;
                            }
                            aiTimer3++;
                        }
                    }
                    aiTimer2++;

                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 70, pitchOffset: -0.5f);
                    Main.LocalPlayer.GetModPlayer<MyPlayer>().screenshakeAmount = 4;
                    if (aiTimer2 > 7)
                    {
                        aiTimer = 0;
                        aiTimer2 = 0;
                        aiTimer3 = 0;
                        aiState = 4;
                        npc.noTileCollide = false;
                    }
                    aiTimer = 0;
                    npc.soundDelay = 20;
                }
                int delay = (int)MathHelper.Lerp(5, 20, MathHelper.Clamp((float)npc.life / (float)npc.lifeMax, 0, 1));
                if (aiTimer > delay)
                {
                    if (Math.Abs(npc.velocity.Y) <= 0.2f && speed.X != 0) npc.velocity.Y = 0;
                    Vector2 dir = speed;
                    dir.Normalize();
                    if ((speed.X != 0 && Math.Abs(npc.velocity.X) < Math.Abs(speed.X)) || (speed.Y != 0 && Math.Abs(npc.velocity.Y) < Math.Abs(speed.Y)))
                        npc.velocity += dir * acceleration;

                    int numDust = (int)Math.Abs(npc.velocity.X + npc.velocity.Y);
                    for (int i = 0; i < numDust; i++)
                    {
                        Vector2 add = Vector2.Zero;
                        float rand = Main.rand.NextFloat(0.3f, 1f);
                        Dust dust = null;
                        if (speed.X > 0)
                        {
                            dust = Main.dust[Dust.NewDust(npc.position, npc.width, 2, 6)];
                            dust.velocity = new Vector2(0, 1 * rand);
                        }
                        else if (speed.X < 0)
                        {
                            dust = Main.dust[Dust.NewDust(npc.BottomLeft, npc.width, 2, 6)];
                            dust.velocity = new Vector2(0, -1 * rand);
                        }
                        else if (speed.Y > 0)
                        {
                            dust = Main.dust[Dust.NewDust(npc.TopRight, 2, npc.height, 6)];
                            dust.velocity = new Vector2(-1 * rand, 0);
                        }
                        else if (speed.Y < 0)
                        {
                            dust = Main.dust[Dust.NewDust(npc.TopLeft, 2, npc.height, 6)];
                            dust.velocity = new Vector2(1 * rand, 0);
                        }
                        if (dust != null)
                        {
                            dust.noGravity = true;
                            dust.velocity *= Math.Abs(npc.velocity.X + npc.velocity.Y);
                            //dust.velocity += npc.velocity * 1f;
                            dust.scale *= 1.6f;
                        }
                    }
                }
            }
        }
    }
}
