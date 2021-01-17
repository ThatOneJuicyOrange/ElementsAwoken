using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Items.Accessories;
using ElementsAwoken.Items.Weapons.Thrown;
using ElementsAwoken.Items.BossDrops.Ancients;
using ElementsAwoken.Items.Donator.Buildmonger;
using ElementsAwoken.Items.Donator.Crow;
using ElementsAwoken.Items.Placeable;
using ElementsAwoken.Buffs.Debuffs;
using System.IO;
using ElementsAwoken.Items.Placeable.LabTiles;

namespace ElementsAwoken.NPCs
{
    public class NPCsGLOBAL : GlobalNPC
    {
        public bool liftable = false;

        public bool iceBound = false;
        public bool extinctionCurse = false;
        public bool handsOfDespair = false;
        public bool endlessTears = false;
        public bool ancientDecay = false;
        public bool soulInferno = false;
        public bool dragonfire = false;
        public bool discordDebuff = false;
        public bool chaosBurn = false;
        public bool electrified = false;
        public bool acidBurn = false;
        public bool corroding = false;
        public bool fastPoison = false;
        public bool starstruck = false;
        public bool incineration = false;

        public bool saveNPC = false;

        public bool impishCurse = false;

        public bool variableLifeDrain = false;
        public int lifeDrainAmount = 0;

        public float generalTimer = 0;

        public bool hasHands = false;

        public bool delete = false;

        public bool shrinking = false;
        public bool shrunk = false;
        public bool growing = false;
        public bool grown = false;
        public bool storedScale = false;
        public float initialScale = 1f;

        public bool platformNPC = false;
        public bool solidX = false;
        public bool solidY = false;

        public override void ResetEffects(NPC npc)
        {
            iceBound = false;
            extinctionCurse = false;
            handsOfDespair = false;
            endlessTears = false;
            ancientDecay = false;
            soulInferno = false;
            dragonfire = false;
            discordDebuff = false;
            chaosBurn = false;
            electrified = false;
            acidBurn = false;
            corroding = false;
            fastPoison = false;
            starstruck = false;
            incineration = false;

            impishCurse = false;

            variableLifeDrain = false;
        }

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public static void ImmuneAllEABuffs(NPC npc)
        {
            npc.buffImmune[BuffType<IceBound>()] = true;
            npc.buffImmune[BuffType<ExtinctionCurse>()] = true;
            npc.buffImmune[BuffType<HandsOfDespair>()] = true;
            npc.buffImmune[BuffType<EndlessTears>()] = true;
            npc.buffImmune[BuffType<AncientDecay>()] = true;
            npc.buffImmune[BuffType<SoulInferno>()] = true;
            npc.buffImmune[BuffType<Dragonfire>()] = true;
            npc.buffImmune[BuffType<Discord>()] = true;
            npc.buffImmune[BuffType<FastPoison>()] = true;
            npc.buffImmune[BuffType<Incineration>()] = true;
            npc.buffImmune[BuffType<Starstruck>()] = true;
        }
        public override void AI(NPC npc)
        {
            generalTimer++;
            if (extinctionCurse)
            {
                /*for (int j = 0; j < 2; j++)
                {
                    int dustLength = ModContent.GetInstance<Config>().lowDust ? 1 : 3;
                    for (int i = 0; i < dustLength; i++)
                    {
                        float X = ((float)Math.Sin(generalTimer / 10) * 10) * MathHelper.Clamp(npc.width / 30, 1, 5) * (j % 2 == 0 ? 1 : -1) + (j % 2 == 0 ? 1 : -1) * 10;
                        float Y = (float)Math.Sin(generalTimer / 20) * npc.height * (j % 2 == 0 ? 1 : -1);
                        Vector2 dustPos = new Vector2(X, Y);

                        Dust dust = Main.dust[Dust.NewDust(npc.Center + dustPos - Vector2.One * 4f, 8, 8, DustID.PinkFlame)];
                        dust.velocity = Vector2.Zero;
                        dust.position -= npc.velocity / dustLength * (float)i;
                        dust.noGravity = true;
                        dust.alpha = npc.alpha;
                    }
                }*/
                if (!GetInstance<Config>().lowDust)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        float speedScale = 1;
                        if (j == 1) speedScale = 1.2f;
                        else if (j == 2) speedScale = 0.5f;
                        else if (j == 3) speedScale = 0.9f;
                        else if (j == 4) speedScale = 1.5f;

                        int distance = (int)(npc.width / 2 + 20);
                        double rad = ((generalTimer / 30) * speedScale) + npc.whoAmI + j * 60 * (Math.PI / 180); // angle to radians
                        Vector2 dustCenter = npc.Center - new Vector2((int)(Math.Cos(rad) * distance), (int)(Math.Sin(rad) * distance));

                        int maxDist = 8;
                        if (j == 1) maxDist = 10;
                        else if (j == 2) maxDist = 15;
                        else if (j == 3) maxDist = 5;
                        else if (j == 4) maxDist = 10;

                        int numDusts = (int)(maxDist / 2);
                        for (int i = 0; i < numDusts; i++)
                        {
                            double angle = Main.rand.NextDouble() * 2d * Math.PI;
                            Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                            Dust dust = Main.dust[Dust.NewDust(dustCenter + offset - Vector2.One * 4, 0, 0, DustID.PinkFlame, 0, 0, 100)];
                            dust.noGravity = true;
                            dust.velocity *= 0.2f;
                        }
                    }
                }
            }
            if (starstruck)
            {
                for (int j = 0; j < 5; j++)
                {
                    int distance = (int)(npc.width * 1.5f * ((Math.Sin(generalTimer / 10) + 1) / 2));
                    double rad = (generalTimer * 2 * (Math.PI / 180)) + (MathHelper.ToRadians(360 / 5) * j) + MathHelper.ToRadians(npc.whoAmI * 15); // angle to radians
                    Vector2 dustCenter = npc.Center - new Vector2((int)(Math.Cos(rad) * distance), (int)(Math.Sin(rad) * distance));

                    Dust dust = Main.dust[Dust.NewDust(dustCenter, 4, 4, DustID.PinkFlame)];
                    dust.noGravity = true;
                    dust.velocity *= 0.2f;
                    dust.fadeIn = 1f;
                    dust.scale = 0.3f;
                }
            }
            bool immune = false;
            foreach (int k in ElementsAwoken.instakillImmune)
            {
                if (npc.type == k)
                {
                    immune = true;
                }
            }
            if (!immune && npc.active && npc.damage > 0 && !npc.dontTakeDamage && !npc.boss && npc.lifeMax < 1000)
            {
                if (shrinking)
                {
                    if (!storedScale)
                    {
                        initialScale = npc.scale;
                        if (grown)
                        {
                            initialScale = npc.scale / 1.5f;
                        }
                        storedScale = true;
                    }
                    if (npc.scale > initialScale * 0.75)
                    {
                        npc.scale *= 0.99f;
                    }
                    else
                    {
                        shrinking = false;
                    }
                    shrunk = true;
                    grown = false;
                }
                if (growing)
                {
                    if (!storedScale)
                    {
                        initialScale = npc.scale;
                        if (shrunk)
                        {
                            initialScale = npc.scale / 0.75f;
                        }
                        storedScale = true;
                    }
                    if (npc.scale < initialScale * 1.5)
                    {
                        npc.scale *= 1.01f;
                    }
                    else
                    {
                        growing = false;
                    }
                    grown = true;
                    shrunk = false;
                }
                if (!shrinking && !growing)
                {
                    storedScale = false;
                }
                if (shrinking && growing)
                {
                    npc.scale = 1f;
                    shrinking = false;
                    growing = false;
                    shrunk = false;
                    grown = false;
                    // make dust in an expanding circle
                    int numDusts = 36;
                    for (int i = 0; i < numDusts; i++)
                    {
                        Vector2 position = (Vector2.Normalize(new Vector2(5, 5)) * new Vector2((float)npc.width / 2f, (float)npc.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + npc.Center;
                        Vector2 velocity = position - npc.Center;
                        int dust = Dust.NewDust(position + velocity, 0, 0, 131, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].noLight = true;
                        Main.dust[dust].velocity = Vector2.Normalize(velocity) * 3f;
                    }
                }
            }

            /*if (!npc.noTileCollide)
            {
                Point npcTile = npc.position.ToTileCoordinates();

                bool inQuicksand = false;
                //bool quicksandSulfuric = false;
                for (int i = 0; i < npc.width / 16; i++)
                {
                    for (int j = 0; j < npc.height / 16; j++)
                    {
                        Tile t = Framing.GetTileSafely(npcTile.X + i, npcTile.Y + j);
                        if (Tiles.GlobalTiles.quicksands.Contains(t.type) && t.active())
                        {
                            inQuicksand = true;
                            //if (t.type == TileType<Tiles.VolcanicPlateau.SulfuricQuicksand>()) quicksandSulfuric = true;
                            break;
                        }
                    }
                }
                if (inQuicksand)
                {
                    npc.velocity.Y -= 0.6f;
                    /*if (quicksandSulfuric) npc.AddBuff(BuffType<AcidBurn>(), 60);
                    npc.velocity.X *= 0.1f;
                    npc.velocity.Y *= 0.1f;
                    if (npc.type == NPCID.Antlion)
                    {
                        npc.velocity.Y -= 0.6f;
                    }
                }

            }*/

        }
        public static bool AnyBoss()
        {
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                NPC boss = Main.npc[i];
                if (boss.boss && boss.active)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Uses all npc.ai slots apart from 0
        /// </summary>
        public static void AdjustableFighterAI(NPC npc, float acceleration, float maxSpd)
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


            if (npc.velocity.X < -maxSpd || npc.velocity.X > maxSpd)
            {
                if (npc.velocity.Y == 0f)
                {
                    npc.velocity *= 0.8f;
                }
            }
            else if (npc.velocity.X < maxSpd && npc.direction == 1)
            {
                npc.velocity.X = npc.velocity.X + acceleration;
                if (npc.velocity.X > maxSpd)
                {
                    npc.velocity.X = maxSpd;
                }
            }
            else if (npc.velocity.X > -maxSpd && npc.direction == -1)
            {
                npc.velocity.X = npc.velocity.X - acceleration;
                if (npc.velocity.X < -maxSpd)
                {
                    npc.velocity.X = -maxSpd;
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
                NPCsGLOBAL.StepUpTiles(npc);
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
        public static void GoThroughPlatforms(NPC npc)
        {
            Vector2 platform = npc.Bottom / 16;
            if (TileID.Sets.Platforms[Framing.GetTileSafely((int)platform.X, (int)platform.Y).type]) npc.noTileCollide = true;
            else npc.noTileCollide = false;
            /*if (ModContent.GetInstance<Config>().debugMode)
            {
                Dust dust = Main.dust[Dust.NewDust(platform * 16, 16, 16, 135)];
                dust.noGravity = true;
            }*/
        }
        public static int ReducePierceDamage(int damage, Projectile projectile)
        {
            if (projectile.type == ProjectileID.LastPrismLaser && ModContent.GetInstance<Config>().vItemChangesDisabled) return (int)(damage * 0.1f);
            else if (projectile.type == ProjectileID.LastPrismLaser && !ModContent.GetInstance<Config>().vItemChangesDisabled) return (int)(damage * 0.85f);
            else if (projectile.maxPenetrate == -1 && ProjectileID.Sets.YoyosMaximumRange[projectile.type] == 0) return (int)(damage * 0.5f);
            else if (projectile.maxPenetrate > 10) return (int)(damage * 0.5f);
            else if (projectile.maxPenetrate > 6) return (int)(damage * 0.75f);
            else if (projectile.maxPenetrate > 3) return (int)(damage * 0.9f);
            else return damage;
        }
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (player.FindBuffIndex(mod.BuffType("CalamityPotionBuff")) != -1)
            {
                spawnRate = (int)(spawnRate / 12.5f);
                maxSpawns = (int)(maxSpawns * 12.5f);
            }
            else if (player.FindBuffIndex(mod.BuffType("ChaosPotionBuff")) != -1 || player.FindBuffIndex(mod.BuffType("CalamityBannerBuff")) != -1)
            {
                spawnRate = (int)(spawnRate / 7.5f);
                maxSpawns = (int)(maxSpawns * 7.5f);
            }
            else if (player.FindBuffIndex(mod.BuffType("ChaosBannerBuff")) != -1)
            {
                spawnRate = (int)(spawnRate / 5f);
                maxSpawns = (int)(maxSpawns * 5f);
            }
            else if(player.FindBuffIndex(mod.BuffType("HavocPotionBuff")) != -1)
            {
                spawnRate = (int)(spawnRate / 4f);
                maxSpawns = (int)(maxSpawns * 4f);
            }
            else if (player.FindBuffIndex(mod.BuffType("HavocBannerBuff")) != -1)
            {
                spawnRate = (int)(spawnRate / 2f);
                maxSpawns = (int)(maxSpawns * 2f);
            }
            if (MyWorld.aggressiveEnemies)
            {
                spawnRate = (int)(spawnRate / 5f);
                maxSpawns = (int)(maxSpawns * 5f);
            }
            if (modPlayer.creditsTimer >= 0)
            {
                maxSpawns = 0;
            }
        }
        public override bool SpecialNPCLoot(NPC npc)
        {
            Player player = Main.LocalPlayer;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.glassHeart && npc.boss)
            {
                CombatText.NewText(npc.getRect(), Color.Red, "No-hit!", true);
                npc.NPCLoot();
            }
            return base.SpecialNPCLoot(npc);
        }
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if (AnyBoss()) pool.Clear();
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            // poisoned: -4
            // on fire: -8
            // frostburn: -16
            // shadowflame: -30
            if (iceBound)
            {
                npc.velocity.Y = 0f;
                npc.velocity.X = 0f;
            }
            if (endlessTears)
            {
                npc.velocity *= 0.8f;
            }

            if (acidBurn)
            {
                npc.lifeRegen -= 20;
                if (damage < 3) damage = 3;
            }
            if (dragonfire)
            {
                npc.lifeRegen -= 40;
                if (damage < 2)  damage = 2;
            }
            if (electrified)
            {
                npc.lifeRegen -= 40;
                if (damage < 4) damage = 4;
            }
            if (ancientDecay)
            {
                npc.lifeRegen -= 50;
                if (damage < 5)  damage = 5;
            }
            if (corroding || soulInferno)
            {
                npc.lifeRegen -= 75;
                if (damage < 8) damage = 8;
            }
            if (handsOfDespair)
            {
                npc.lifeRegen -= 120;
                if (damage < 15) damage = 15;
                if (!hasHands && !npc.boss)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("HandsOfDespair"), 0, 0f, 0, 1f, npc.whoAmI);
                    hasHands = true;
                }
            }
            else hasHands = false;
            if (extinctionCurse)
            {
                npc.lifeRegen -= 150;
                if (damage < 30)   damage = 30;
            }
            if (chaosBurn || discordDebuff || fastPoison || starstruck)
            {
                npc.lifeRegen -= 300;
                if (damage < 50) damage = 50;
            }
            if (variableLifeDrain && lifeDrainAmount > 0 && !npc.SpawnedFromStatue)
            {
                npc.lifeRegen -= lifeDrainAmount;
                if (damage < lifeDrainAmount / 2)  damage = lifeDrainAmount / 2;
            }
            if (incineration)
            {
                int amount = NPC.downedMoonlord ? 50 : 20;
                npc.lifeRegen -= amount;
                if (damage < amount / 2) damage = amount / 2;
            }
            if (delete)
            {
                npc.active = false;
                if (npc.active)
                {
                    npc.StrikeNPC(npc.life, 0f, 0, false, false, false);
                }
                npc.netUpdate = true;
            }
            Player player = Main.LocalPlayer;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.buffDPS += Math.Abs(npc.lifeRegen / 2);
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (fastPoison) drawColor = new Color(14, 150, 45);
            if (ancientDecay)
            {
                drawColor = Color.LightYellow;
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, mod.DustType("AncientDust"), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.25f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.35f;
                    }
                }
                Lighting.AddLight(npc.position, 0.025f, 0f, 0f);
            }
            if (acidBurn)
            {
                npc.color = new Color(178, 244, 124, 120);
            }
            if (MyWorld.swearingEnemies && npc.damage > 0)
            {
                if (Main.rand.Next(600) == 0)
                {
                    string s = "";
                    for (int l = 0; l < 4; l++)
                    {
                        int choice = Main.rand.Next(7);
                        if (choice == 0)
                        {
                            s = s + "$";
                        }
                        if (choice == 1)
                        {
                            s = s + "#";
                        }
                        if (choice == 2)
                        {
                            s = s + "!";
                        }
                        if (choice == 3)
                        {
                            s = s + "$";
                        }
                        if (choice == 4)
                        {
                            s = s + "@";
                        }
                        if (choice == 5)
                        {
                            s = s + "*";
                        }
                        if (choice == 6)
                        {
                            s = s + "?";
                        }
                    }
                    CombatText.NewText(npc.getRect(), Color.Red, s, false, false);
                }
            }

            if (impishCurse)
            {
                if (!npc.ichor) drawColor = new Color(255, 80, 80);
                else drawColor = new Color(255, 180, 40);
                Lighting.AddLight(npc.Center, 0.6f, 0.2f, 0.3f);
            }

            // seeing the npc id
            //Main.spriteBatch.DrawString(Main.fontMouseText, "whoAmI: " + npc.whoAmI, new Vector2(npc.Center.X - 30 - Main.screenPosition.X, npc.Top.Y - 20 - Main.screenPosition.Y), Color.White);
        }

        /*public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if (soulRip && soulRipTimer <= 30 && !npc.boss)
            {
                    SpriteEffects effects = npc.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                    Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);

                    Vector2 drawPos = npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, -2);
                    drawPos.Y -= soulRipTimer;
                    Color color = new Color (109, 20, 106, soulRipAlpha);
                    Main.spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, npc.frame, color, npc.rotation, drawOrigin, npc.scale, effects, 0f);
                    Main.NewText(soulRipTimer);
            }
            return true;
        }
        public override void AI(NPC npc)
        {
            if (!soulRip)
            {
                soulRipTimer = 0;
                soulRipAlpha = 0.3f;
            }
            else
            {
                soulRipTimer++;

                soulRipAlpha += 0.03f;
            }
        }*/

        public override void SetDefaults(NPC npc)
        {
            // all vanilla bosses
            #region buff immunity
            if (npc.type == NPCID.EyeofCthulhu ||
                npc.type == NPCID.EaterofWorldsHead ||
                npc.type == NPCID.EaterofWorldsBody ||
                npc.type == NPCID.EaterofWorldsTail ||
                npc.type == NPCID.BrainofCthulhu ||
                npc.type == NPCID.Creeper ||
                npc.type == NPCID.SkeletronHead ||
                npc.type == NPCID.SkeletronHand ||
                npc.type == NPCID.QueenBee ||
                npc.type == NPCID.KingSlime ||
                npc.type == NPCID.WallofFlesh ||
                npc.type == NPCID.WallofFleshEye ||
                npc.type == NPCID.TheDestroyer ||
                npc.type == NPCID.TheDestroyerBody ||
                npc.type == NPCID.TheDestroyerTail ||
                npc.type == NPCID.Retinazer ||
                npc.type == NPCID.Spazmatism ||
                npc.type == NPCID.SkeletronPrime ||
                npc.type == NPCID.PrimeCannon ||
                npc.type == NPCID.PrimeSaw ||
                npc.type == NPCID.PrimeVice ||
                npc.type == NPCID.PrimeLaser ||
                npc.type == NPCID.Plantera ||
                npc.type == NPCID.PlanterasTentacle ||
                npc.type == NPCID.Golem ||
                npc.type == NPCID.GolemHead ||
                npc.type == NPCID.GolemFistLeft ||
                npc.type == NPCID.GolemFistRight ||
                npc.type == NPCID.DukeFishron ||
                npc.type == NPCID.CultistBoss ||
                npc.type == NPCID.MoonLordHead ||
                npc.type == NPCID.MoonLordHand ||
                npc.type == NPCID.MoonLordCore)
            {
                npc.buffImmune[mod.BuffType("IceBound")] = true;
                npc.buffImmune[mod.BuffType("EndlessTears")] = true;
                npc.buffImmune[mod.BuffType("HandsOfDespair")] = true;
            }
            // later bosses
            if (npc.type == NPCID.DukeFishron ||
                npc.type == NPCID.CultistBoss ||
                npc.type == NPCID.MoonLordHead ||
                npc.type == NPCID.MoonLordHand ||
                npc.type == NPCID.MoonLordCore)
            {
                npc.buffImmune[mod.BuffType("AncientDecay")] = true;
                npc.buffImmune[mod.BuffType("SoulInferno")] = true;
                npc.buffImmune[mod.BuffType("DragonFire")] = true;
            }
            #endregion

            if (MyWorld.aggressiveEnemies)
            {
                npc.damage = (int)(npc.damage * 1.25f);
            }
            if (npc.SpawnedFromStatue)
            {
                npc.buffImmune[mod.BuffType("LifeDrain")] = true;
            }         
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Merchant && NPC.downedBoss1)
            {
                shop.item[nextSlot].SetDefaults(ItemType<ThrowableBook>());
                shop.item[nextSlot].shopCustomPrice = 80;
                nextSlot++; 
                shop.item[nextSlot].SetDefaults(ItemType<RainMeter>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,5,0,0);
                nextSlot++;
            }
            if (type == NPCID.Dryad && Main.bloodMoon)
            {
                shop.item[nextSlot].SetDefaults(ItemType<DryadsRadar>());
                nextSlot++;
            }
            if (type == NPCID.Wizard)
            {
                if (Main.hardMode)
                {
                    shop.item[nextSlot].SetDefaults(ItemType<Dictionary>());
                    shop.item[nextSlot].shopCustomPrice = 800;
                    nextSlot++;
                }
                if (MyWorld.downedAncients)
                {
                    shop.item[nextSlot].SetDefaults(ItemType<CrystalAmalgamate>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 25, 0, 0);
                    nextSlot++;
                }
            }
            if (type == NPCID.Steampunker)
            {
                shop.item[nextSlot].SetDefaults(ItemType<SonicArm>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 25, 0, 0);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemType<FeatheredGoggles>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(2, 0, 0, 0);
                nextSlot++;
            }
            if (type == NPCID.Cyborg)
            {
                shop.item[nextSlot].SetDefaults(ItemType<Computer>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 5, 0, 0);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemType<Desk>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1, 50, 0);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemType<OfficeChair>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1, 50, 0);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemType<LabLightFunctional>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1, 50, 0);
                nextSlot++;
            }
        }

        public override void GetChat(NPC npc, ref string chat)
        {
            int storyteller = NPC.FindFirstNPC(mod.NPCType("Storyteller"));
            int alchemist = NPC.FindFirstNPC(mod.NPCType("Alchemist"));
            if (npc.type == NPCID.Guide && storyteller >= 0)
            {
                if (Main.rand.Next(10) == 0)
                {
                    chat = "Dont trust " + Main.npc[storyteller].GivenName + ", he isn't as helpful as you may think.";
                }
            }
            /*if (npc.type == NPCID.Guide && Main.rand.Next(10) == 0 && MyWorld.downedAncients)
            {
                Main.npcChatText = "At last, the storyteller is gone!"; // crap text 
            }*/
            if (npc.type == NPCID.Nurse && storyteller >= 0)
            {
                if (Main.rand.Next(10) == 0)
                {
                    chat = "Does " + Main.npc[storyteller].GivenName + " ever age? Everyone seems to be getting older but he stays the same";
                }
            }
            if (npc.type == NPCID.Dryad && storyteller >= 0)
            {
                if (Main.rand.Next(10) == 0)
                {
                    chat = "How old is " + Main.npc[storyteller].GivenName + "? I seem to have memories of him from when I was a kid...";
                }
                if (NPC.downedBoss1 && !MyWorld.downedWasteland)
                {
                    if (Main.rand.Next(5) == 0)
                    {
                        switch (Main.rand.Next(3))
                        {
                            case 0: chat = "That scorpion, 'Wasteland' as you call it, why do we fear her?"; break;
                            case 1: chat = "I feel her speaking to me... she’s afraid... "; break;
                            case 2: chat = "All creatures deserve a chance to live. Live and be free..."; break;
                        }
                    }
                }
            }
            if (npc.type == NPCID.ArmsDealer && storyteller >= 0)
            {
                if (Main.rand.Next(10) == 0)
                {
                    chat = Main.npc[storyteller].GivenName + " keeps talking down my weapons! I'll teach him a lesson one day.";
                }
                if (Main.rand.Next(10) == 0)
                {
                    chat = "Dont buy " + Main.npc[storyteller].GivenName + "'s weapons, something feels off about them...";
                }
            }
            if (npc.type == NPCID.Truffle && storyteller >= 0)
            {
                if (Main.rand.Next(10) == 0)
                {
                    chat = Main.npc[storyteller].GivenName + " seems to be the only person in this town that doesn't want to eat me. Does he eat at all?";
                }
            }
            if (npc.type == NPCID.Cyborg)
            {
                if (Main.rand.Next(15) == 0)
                {
                    chat = "Some townsfolk have been finding strange abandoned labs underground... I think some have been staying overnight "; //reference to the bug where npcs will move into labs
                }
            }
        }
        public static void StepUpTiles(NPC npc)
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

    }
}