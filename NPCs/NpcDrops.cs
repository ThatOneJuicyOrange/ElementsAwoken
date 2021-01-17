using ElementsAwoken.Items.Pets;
using ElementsAwoken.NPCs.ItemSets.ToySlime;
using ElementsAwoken.Projectiles.Other;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.NPCs
{
    public class NpcDrops : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (!npc.SpawnedFromStatue)
            {
                Player player = Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)];
                MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
                int posX = (int)npc.position.X; // just to have less text in here 
                int posY = (int)npc.position.Y;
                int w = npc.width;
                int h = npc.height;
                #region Essence Drops
                if (NPC.downedBoss1) //EoC
                {
                    if (player.ZoneDesert && !player.ZoneBeach)
                    {
                        int chance = Main.expertMode ? MyWorld.awakenedMode ? 9 : 11 : 12;
                        if (Main.rand.Next(chance) == 0)
                        {
                            Item.NewItem(posX, posY, w, h, mod.ItemType("DesertEssence"));
                        }
                    }
                }
                if (NPC.downedBoss3) //Skeletron
                {
                    if (player.ZoneUnderworldHeight)
                    {
                        int chance = Main.expertMode ? MyWorld.awakenedMode ? 10 : 12 : 13;
                        if (Main.rand.Next(chance) == 0)
                        {
                            Item.NewItem(posX, posY, w, h, mod.ItemType("FireEssence"));
                        }
                    }
                }
                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                {
                    if (player.ZoneSkyHeight)
                    {
                        int chance = Main.expertMode ? MyWorld.awakenedMode ? 6 : 8 : 10;
                        if (Main.rand.Next(chance) == 0)
                        {
                            Item.NewItem(posX, posY, w, h, mod.ItemType("SkyEssence"));
                        }
                    }
                }
                if (NPC.downedPlantBoss)
                {
                    if (player.ZoneSnow)
                    {
                        int chance = Main.expertMode ? MyWorld.awakenedMode ? 10 : 14 : 16;
                        if (Main.rand.Next(chance) == 0)
                        {
                            Item.NewItem(posX, posY, w, h, mod.ItemType("FrostEssence"));
                        }
                    }
                }
                if (NPC.downedFishron)
                {
                    if (player.ZoneBeach)
                    {
                        int chance = Main.expertMode ? MyWorld.awakenedMode ? 12 : 15 : 16;
                        if (Main.rand.Next(chance) == 0)
                        {
                            Item.NewItem(posX, posY, w, h, mod.ItemType("WaterEssence"));
                        }
                    }
                }
                #endregion

                // bosses
                if (npc.type == NPCID.KingSlime) if (Main.rand.Next(3) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("NinjaKatana"));
                if (npc.type == NPCID.EyeofCthulhu)
                {
                    Item.NewItem(posX, posY, w, h, mod.ItemType("LensFragment"), Main.rand.Next(5, 15));
                    if (Main.rand.Next(3) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("TearsOfSorrow"));
                }
                if (npc.type == NPCID.EaterofWorldsHead) if (Main.rand.Next(22) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("TheEater"));
                if (npc.type == NPCID.BrainofCthulhu) if (Main.rand.Next(3) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("SpinalSplayer"));
                if (npc.type == NPCID.SkeletronHead) if (Main.rand.Next(3) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("SkeletronFist"));
                if (npc.type == NPCID.WallofFlesh)
                {
                    Item.NewItem(posX, posY, w, h, mod.ItemType("DemonicFleshClump"), Main.rand.Next(10, 30));
                    if (Main.rand.Next(7) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("ThrowerEmblem"));
                }
                if (npc.type == NPCID.Spazmatism) if (Main.rand.Next(4) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("Retinasm"));
                if (npc.type == NPCID.Retinazer) if (Main.rand.Next(4) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("Retinasm"));
                if (npc.type == NPCID.TheDestroyer) if (Main.rand.Next(3) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("TheDestroyer"));
                if (npc.type == NPCID.SkeletronPrime) if (Main.rand.Next(3) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("PrimeCannon"));
                if (npc.type == NPCID.Golem) Item.NewItem(posX, posY, w, h, mod.ItemType("SunFragment"), Main.rand.Next(10, 30));
                if (npc.type == NPCID.DukeFishron) Item.NewItem(posX, posY, w, h, mod.ItemType("RoyalScale"), Main.rand.Next(5, 15));
                if (npc.type == NPCID.CultistBoss)
                {
                    if (Main.rand.Next(6) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("CelestialIdol"));
                    if (Main.rand.Next(6) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("Shockstorm"));
                    if (Main.rand.Next(6) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("Frosthail"));
                    if (Main.rand.Next(6) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("Fireblast"));
                    if (Main.rand.Next(6) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("Starthrower"));
                    if (Main.rand.NextBool(10)) Item.NewItem(posX, posY, w, h, ItemType<EldritchKeepsake>());
                }
                if (npc.type == NPCID.MourningWood) if (Main.rand.Next(9) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("GreekFire"));
                if (npc.boss)
                {
                    int dropChance = Main.expertMode ? MyWorld.awakenedMode ? 150 : 200 : 250;
                    if (Main.rand.Next(dropChance) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("Awakener"));
                }

                if (npc.type == NPCID.UndeadViking && Main.rand.Next(15) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("Warhorn"));

                if (npc.type == NPCID.Shark&& Main.rand.Next(15) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("BabyShark"));
                if (npc.type >= NPCID.Salamander && npc.type <= NPCID.Salamander9 && Main.rand.Next(15) == 0)Item.NewItem(posX, posY, w, h, mod.ItemType("AxolotlMask"));
                if ((npc.type == NPCID.Penguin || npc.type == NPCID.CrimsonPenguin || npc.type == NPCID.CorruptPenguin || npc.type == NPCID.PenguinBlack))
                {
                    if (Main.rand.Next(3) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("PenguinFeather"));
                }
                if (npc.type == NPCID.Mothron && Main.rand.Next(3) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("BrokenHeroWhip"));
                if ((npc.type == NPCID.Frankenstein || npc.type == NPCID.SwampThing) &&Main.rand.Next(249) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("BrokenHeroWhip"));
                if (npc.type == NPCID.GiantWormHead && Main.rand.Next(10) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("DiggerTooth"));
                if (npc.type == NPCID.DuneSplicerHead && Main.rand.Next(15) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("TombCrawlerTooth"));
                if (npc.type == NPCID.Plantera)
                {
                    Item.NewItem(posX, posY, w, h, mod.ItemType("MysticLeaf"), Main.rand.Next(1, 2));
                    Item.NewItem(posX, posY, w, h, mod.ItemType("MagicalHerbs"), Main.rand.Next(3, 8));
                    if (Main.rand.Next(8) == 0 && !Main.expertMode) Item.NewItem(posX, posY, w, h, mod.ItemType("CrematedChaos"));
                }
                if (npc.type == NPCID.Tim)
                {
                    int dropChance = Main.expertMode ? MyWorld.awakenedMode ? 1 : 2 : 3;
                    if (Main.rand.Next(dropChance) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("IllusiveCharm"));
                }
                if (npc.type == NPCID.FireImp) Item.NewItem(posX, posY, w, h, mod.ItemType("ImpEar"), Main.rand.Next(1, 2));
                if (npc.type == NPCID.LavaSlime)
                {
                    int chance = Main.expertMode ? 4 : 5;
                    if (MyWorld.awakenedMode) chance = 3;
                    if (Main.rand.Next(chance) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("MagmaCrystal"), Main.rand.Next(1, 2));
                }
                if (npc.type == NPCID.Hellbat) if (Main.rand.Next(79) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("HellbatWing"));
                if (npc.type == NPCID.Lavabat) if (Main.rand.Next(39) == 0)Item.NewItem(posX, posY, w, h, mod.ItemType("HellbatWing"));
                if (npc.type == NPCID.Harpy) if (Main.rand.Next(99) == 0)Item.NewItem(posX, posY, w, h, mod.ItemType("LightningCloud"));
                if (Main.hardMode/* && !npc.boss*/)
                {
                    if (Main.rand.NextBool(1499))
                    {
                        //Item.NewItem(posX, posY, w, h, mod.ItemType("InfinityCrys"));
                        Projectile proj = Main.projectile[Projectile.NewProjectile(posX, posY, 0, 0, ProjectileType<InfinityCrystalSpawner>(), 0, 0f, Main.myPlayer, npc.type, 0f)];
                        proj.spriteDirection = -npc.spriteDirection;
                        proj.direction = -npc.direction;
                        proj.width = npc.width;
                        proj.height = npc.height;
                        proj.Center = npc.Center;
                    }
                }
                if (Main.bloodMoon) if (Main.rand.Next(19) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("FleshClump"), Main.rand.Next(1, 2));

                if (npc.type == NPCID.Zombie) if (Main.rand.Next(12) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("SteakShield"));

                // artifact drops & zones
                if (player.ZoneDesert && !player.ZoneBeach) if (Main.rand.NextBool(1000)) Item.NewItem(posX, posY, w, h, mod.ItemType("DiscordantAmber"));
                if (player.ZoneUnderworldHeight) if (Main.rand.NextBool(1000)) Item.NewItem(posX, posY, w, h, mod.ItemType("FieryJar"));
                if (player.ZoneSkyHeight) if (Main.rand.NextBool(800)) Item.NewItem(posX, posY, w, h, mod.ItemType("StrangeTotem"));
                if (player.ZoneSnow) if (Main.rand.NextBool(1000)) Item.NewItem(posX, posY, w, h, mod.ItemType("GlowingSlush"));
                if (player.ZoneBeach) if (Main.rand.NextBool(1000)) Item.NewItem(posX, posY, w, h, mod.ItemType("OddWater"));
                if (MyWorld.voidInvasionUp) if (Main.rand.NextBool(1000)) Item.NewItem(posX, posY, w, h, mod.ItemType("SoulOfPlight"));
                if (player.ZoneRockLayerHeight)
                {
                    if (Main.rand.Next(3999) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("Tomato"));
                    if (!player.ZoneDungeon) if (Main.rand.Next(400) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("AssassinsKnife"));
                }

                if (!player.ZoneDungeon && modPlayer.zonePlateau && Main.hardMode && !npc.friendly && npc.lifeMax > 1 && npc.damage > 0)
                {
                    // vannilla wacky code
                    bool drop = false;
                    if (Main.expertMode && Main.rand.Next(5) == 0) drop = true;
                    else if (Main.rand.Next(5) == 0) drop = true;
                    if (drop) Item.NewItem(posX, posY, w, h, ItemType<Items.Materials.SoulOfBright>());
                }
                // enemy spawns
                if (NPC.downedMoonlord)
                {
                    if (npc.type == NPCID.Bunny || npc.type == NPCID.Squirrel || npc.type == NPCID.SquirrelRed)
                    {
                        if (Main.rand.Next(29) == 0)
                        {
                            Vector2 spawnAt = npc.Center + new Vector2(0f, (float)h / 2f);
                            NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("GiantTick"));
                        }
                    }

                    if (modPlayer.zonePlateau && npc.type != NPCType<VolcanicPlateau.InfernoSpirit>())
                    {
                        if (Main.rand.Next(9) == 0)
                        {
                            Vector2 spawnAt = npc.Center + new Vector2(0f, (float)h / 2f);
                            NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, NPCType<VolcanicPlateau.InfernoSpirit>());
                        }
                    }
                }

                if (player.GetModPlayer<PlayerEnergy>().soulConverter && Main.rand.Next(12) == 0) Item.NewItem(posX, posY, w, h, mod.ItemType("EnergyPickup"));
                if (Main.slimeRain && Main.slimeRainNPC[npc.type] && !NPC.AnyNPCs(NPCType<ToySlime>()) && NPC.downedBoss3)
                {
                    int killsRequired = 75;
                    if (NPC.downedSlimeKing)  killsRequired /= 2;
                    if (Main.slimeRainKillCount == killsRequired)
                    {
                        NPC.SpawnOnPlayer(player.whoAmI, NPCType<ToySlime>());
                    }
                }
                if (npc.type == NPCID.MoonLordCore && !ElementsAwoken.ancientsAwakenedEnabled)
                {
                    if (MyWorld.moonlordKills < 5)
                    {
                        EAWorldGen.genLuminite = true;
                    }
                    if (MyWorld.moonlordKills == 0) EAWorldGen.genAntHill = true;
                    MyWorld.moonlordKills++;
                    EAWorldGen.genStellorite = true;
                    if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
                }
            }
        }
    }
}