using ElementsAwoken.Items.Pets;
using ElementsAwoken.NPCs.ItemSets.ToySlime;
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
            Player player = Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)];
            #region Essence Drops
            if (NPC.downedBoss1) //EoC
            {
                if (player.ZoneDesert && !player.ZoneBeach && !npc.SpawnedFromStatue)
                {
                    int chance = Main.expertMode ? MyWorld.awakenedMode ? 9 : 11 : 12;
                    if (Main.rand.Next(chance) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DesertEssence"), 1);
                    }
                }
            }
            if (NPC.downedBoss3) //Skeletron
            {
                if (player.ZoneUnderworldHeight && !npc.SpawnedFromStatue)
                {
                    int chance = Main.expertMode ? MyWorld.awakenedMode ? 10 : 12 : 13;
                    if (Main.rand.Next(chance) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FireEssence"), 1);
                    }
                }
            }
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
            {
                if (player.ZoneSkyHeight && !npc.SpawnedFromStatue)
                {
                    int chance = Main.expertMode ? MyWorld.awakenedMode ? 6 : 8 : 10;
                    if (Main.rand.Next(chance) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SkyEssence"), 1);
                    }
                }
            }
            if (NPC.downedPlantBoss)
            {
                if (player.ZoneSnow && !npc.SpawnedFromStatue)
                {
                    int chance = Main.expertMode ? MyWorld.awakenedMode ? 10 : 14 : 16;
                    if (Main.rand.Next(chance) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FrostEssence"), 1);
                    }
                }
            }
            if (NPC.downedFishron)
            {
                if (player.ZoneBeach && !npc.SpawnedFromStatue)
                {
                    int chance = Main.expertMode ? MyWorld.awakenedMode ? 12 : 15 : 16;
                    if (Main.rand.Next(chance) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WaterEssence"), 1);
                    }
                }
            }
            #endregion

            #region Boss Drops
            if (npc.type == NPCID.KingSlime)
            {
                if (Main.rand.Next(3) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("NinjaKatana"), 1);
                    }
                }
            }
            if (npc.type == NPCID.EyeofCthulhu)
            {
                if (Main.rand.Next(3) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TearsOfSorrow"), 1);
                    }
                }
            }
            if (npc.type == NPCID.EaterofWorldsHead)
            {
                if (Main.rand.Next(22) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TheEater"), 1);
                    }
                }
            }
            if (npc.type == NPCID.BrainofCthulhu)
            {
                if (Main.rand.Next(3) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SpinalSplayer"), 1);
                    }
                }
            }
            if (npc.type == NPCID.SkeletronHead)
            {
                if (Main.rand.Next(3) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SkeletronFist"), 1);
                    }
                }
            }
            if (npc.type == NPCID.Spazmatism)
            {
                if (Main.rand.Next(4) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Retinasm"), 1);
                    }
                }
            }
            if (npc.type == NPCID.Retinazer)
            {
                if (Main.rand.Next(4) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Retinasm"), 1);
                    }
                }
            }
            if (npc.type == NPCID.TheDestroyer)
            {
                if (Main.rand.Next(3) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TheDestroyer"), 1);
                    }
                }
            }
            if (npc.type == NPCID.SkeletronPrime)
            {
                if (Main.rand.Next(3) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PrimeCannon"), 1);
                    }
                }
            }
            if (npc.type == NPCID.CultistBoss)
            {
                if (Main.rand.Next(6) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CelestialIdol"), 1);
                    }
                }
                if (Main.rand.Next(6) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Shockstorm"), 1);
                    }
                }
                if (Main.rand.Next(6) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Frosthail"), 1);
                    }
                }
                if (Main.rand.Next(6) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Fireblast"), 1);
                    }
                }
                if (Main.rand.Next(6) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Starthrower"), 1);
                    }
                }
            }
            if (npc.type == NPCID.MourningWood)
            {
                if (Main.rand.Next(9) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GreekFire"), 1);
                    }
                }
            }
            #endregion

            #region Other
            if (npc.boss)
            {
                int dropChance = Main.expertMode ? MyWorld.awakenedMode ?  150 : 200 : 250;
                if (Main.rand.Next(dropChance) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Awakener"));
                }
            }
            if (npc.type == NPCID.EyeofCthulhu)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LensFragment"), Main.rand.Next(5, 15));
            }
            if (npc.type == NPCID.CultistBoss)
            {
                if (Main.rand.NextBool(10)) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<EldritchKeepsake>());
            }
            if (npc.type == NPCID.DukeFishron)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RoyalScale"), Main.rand.Next(5, 15));
            }
            if (npc.type == NPCID.UndeadViking)
            {
                if (Main.rand.Next(15) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Warhorn"));
                }
            }
            if (npc.type == NPCID.Shark && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(15) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BabyShark"));
                }
            }
            if (npc.type >= NPCID.Salamander && npc.type <= NPCID.Salamander9 && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(15) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AxolotlMask"));
                }
            }
            if ((npc.type == NPCID.Penguin || npc.type == NPCID.CrimsonPenguin || npc.type == NPCID.CorruptPenguin || npc.type == NPCID.PenguinBlack) && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PenguinFeather"));
                }
            }
            if (npc.type == NPCID.Mothron)
            {
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BrokenHeroWhip"));
                }
            }
            if (npc.type == NPCID.Frankenstein || npc.type == NPCID.SwampThing)
            {
                if (Main.rand.Next(249) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BrokenHeroWhip"));
                }
            }
            if (npc.type == NPCID.Golem)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SunFragment"), Main.rand.Next(10, 30));
            }
            if (npc.type == NPCID.WallofFlesh)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DemonicFleshClump"), Main.rand.Next(10, 30));
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ThrowerEmblem"), 1);
                }
            }
            if (npc.type == NPCID.GiantWormHead)
            {
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DiggerTooth"));
                }
            }
            if (npc.type == NPCID.DuneSplicerHead)
            {
                if (Main.rand.Next(15) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TombCrawlerTooth"));
                }
            }
            if (npc.type == NPCID.Plantera)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MysticLeaf"), Main.rand.Next(1, 2));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MagicalHerbs"), Main.rand.Next(3, 8));
                if (Main.rand.Next(8) == 0 && !Main.expertMode)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CrematedChaos"));
                }
            }
            if (npc.type == NPCID.Tim && !npc.SpawnedFromStatue)
            {
                int dropChance = Main.expertMode ? MyWorld.awakenedMode ? 1 : 2 : 3;
                if (Main.rand.Next(dropChance) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("IllusiveCharm"), 1);
                }
            }
            if (npc.type == NPCID.FireImp && !npc.SpawnedFromStatue)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ImpEar"), Main.rand.Next(1, 2));
            }
            if (npc.type == NPCID.LavaSlime && !npc.SpawnedFromStatue)
            {
                int chance = Main.expertMode ? 4 : 5;
                if (MyWorld.awakenedMode) chance = 3;
                if (Main.rand.Next(chance) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MagmaCrystal"), Main.rand.Next(1, 2));
                }
            }
            if (npc.type == NPCID.Hellbat && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(79) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HellbatWing"), 1);
                }
            }
            if (npc.type == NPCID.Lavabat && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(39) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HellbatWing"), 1);
                }
            }
            if (npc.type == NPCID.Harpy && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(99) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LightningCloud"), 1);
                }
            }
            if (Main.hardMode && Main.rand.Next(1499) == 0 && !npc.SpawnedFromStatue)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("InfinityCrys"), 1);
            }
            if (Main.bloodMoon && Main.rand.Next(19) == 0 && !npc.SpawnedFromStatue)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FleshClump"), Main.rand.Next(1, 2));
            }
            if (player.ZoneRockLayerHeight && !player.ZoneDungeon && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(400) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AssassinsKnife"), 1);
                }
            }
            if (npc.type == NPCID.Zombie && !Main.hardMode && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(12) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SteakShield"), 1);
                }
            }
            if (player.ZoneRockLayerHeight && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(3999) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Tomato"), 1);
                }
            }
            #endregion

            #region Artifact Drops
            if (player.ZoneDesert && !player.ZoneBeach && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(1000) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DiscordantAmber"), 1);
                }
            }
            if (player.ZoneUnderworldHeight && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(1000) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FieryJar"), 1);
                }
            }
            if (player.ZoneSkyHeight && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(800) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("StrangeTotem"), 1);
                }
            }
            if (player.ZoneSnow && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(1000) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GlowingSlush"), 1);
                }
            }
            if (player.ZoneBeach && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(1000) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("OddWater"), 1);
                }
            }
            if (MyWorld.voidInvasionUp && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(1000) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SoulOfPlight"), 1);
                }
            }          
            #endregion

            #region Enemy Spawns
            if (NPC.downedMoonlord) //EoC
            {
                if (player.ZoneDungeon && !npc.SpawnedFromStatue)
                {
                    if (Main.rand.Next(9) == 0)
                    {
                        Vector2 spawnAt = npc.Center + new Vector2(0f, (float)npc.height / 2f);
                        NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("InfernoSpirit"));
                    }
                }
            }
            if (NPC.downedMoonlord) //EoC
            {
                if (npc.type == NPCID.Bunny)
                {
                    if (Main.rand.Next(29) == 0)
                    {
                        Vector2 spawnAt = npc.Center + new Vector2(0f, (float)npc.height / 2f);
                        NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("GiantTick"));
                    }
                }
                if (npc.type == NPCID.Squirrel)
                {
                    if (Main.rand.Next(29) == 0)
                    {
                        Vector2 spawnAt = npc.Center + new Vector2(0f, (float)npc.height / 2f);
                        NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("GiantTick"));
                    }
                }
                if (npc.type == NPCID.SquirrelRed)
                {
                    if (Main.rand.Next(29) == 0)
                    {
                        Vector2 spawnAt = npc.Center + new Vector2(0f, (float)npc.height / 2f);
                        NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("GiantTick"));
                    }
                }
            }
            #endregion

            if (player.GetModPlayer<PlayerEnergy>().soulConverter)
            {
                if (Main.rand.Next(12) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EnergyPickup"), 1);
                }
            }
            if (Main.slimeRain && Main.slimeRainNPC[npc.type] && !NPC.AnyNPCs(NPCType<ToySlime>()) && NPC.downedBoss3)
            {
                int killsRequired = 75;
                if (NPC.downedSlimeKing)
                {
                    killsRequired /= 2;
                }
                if (Main.slimeRainKillCount == killsRequired)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, NPCType<ToySlime>());
                }
            }
            if (npc.type == NPCID.MoonLordCore && !ElementsAwoken.ancientsAwakenedEnabled && MyWorld.moonlordKills < 5)
            {
                MyWorld.moonlordKills++;
                MyWorld.genLuminite = true;
                if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
            }
        }       
    }
}