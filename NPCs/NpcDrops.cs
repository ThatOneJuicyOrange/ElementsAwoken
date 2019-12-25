using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs
{
    public class NpcDrops : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            Player player = Main.player[Main.myPlayer];

            #region Essence Drops
            if (NPC.downedBoss1) //EoC
            {
                if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneDesert && !npc.SpawnedFromStatue)
                {
                    if (Main.rand.Next(11) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DesertEssence"), 1);
                    }
                }
            }
            if (NPC.downedBoss3) //Skeletron
            {
                if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneUnderworldHeight && !npc.SpawnedFromStatue)
                {
                    if (Main.rand.Next(12) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FireEssence"), 1);
                    }
                }
            }
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
            {
                if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneSkyHeight && !npc.SpawnedFromStatue)
                {
                    if (Main.rand.Next(8) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SkyEssence"), 1);
                    }
                }
            }
            if (NPC.downedPlantBoss)
            {
                if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneSnow && !npc.SpawnedFromStatue)
                {
                    if (Main.rand.Next(14) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FrostEssence"), 1);
                    }
                }
            }
            if (NPC.downedFishron)
            {
                if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneBeach && !npc.SpawnedFromStatue)
                {
                    if (Main.rand.Next(15) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WaterEssence"), 1);
                    }
                }
            }
            /*if (NPC.downedMoonlord)
            {
                if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneTowerNebula && !npc.SpawnedFromStatue)
                {
                    if (Main.rand.Next(54) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEssence"), 1);
                    }
                }
            }
            if (NPC.downedMoonlord)
            {
                if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneTowerSolar && !npc.SpawnedFromStatue)
                {
                    if (Main.rand.Next(54) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEssence"), 1);
                    }
                }
            }
            if (NPC.downedMoonlord)
            {
                if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneTowerStardust && !npc.SpawnedFromStatue)
                {
                    if (Main.rand.Next(54) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEssence"), 1);
                    }
                }
            }
            if (NPC.downedMoonlord)
            {
                if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneTowerVortex && !npc.SpawnedFromStatue)
                {
                    if (Main.rand.Next(54) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEssence"), 1);
                    }
                }
            }*/
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
                int dropChance = Main.expertMode ? 200 : 250;
                if (Main.rand.Next(dropChance) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Awakener"));
                }
            }
            if (npc.type == NPCID.EyeofCthulhu)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LensFragment"), Main.rand.Next(5, 15));
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
            if (npc.type == NPCID.Penguin || npc.type == NPCID.CrimsonPenguin || npc.type == NPCID.CorruptPenguin || npc.type == NPCID.PenguinBlack)
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
                if (Main.rand.Next(8) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CrematedChaos"));
                }
            }
            if (npc.type == NPCID.WallofFlesh)
            {
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ThrowerEmblem"), 1);
                }
            }
            if (npc.type == NPCID.Tim)
            {
                if (Main.rand.Next(2) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("IllusiveCharm"), 1);
                }
            }
            if (npc.type == NPCID.FireImp)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ImpEar"), Main.rand.Next(1, 2));
            }
            if (npc.type == NPCID.LavaSlime)
            {
                if (Main.rand.Next(5) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MagmaCrystal"), Main.rand.Next(1, 2));
                }
            }
            if (npc.type == NPCID.Hellbat)
            {
                if (Main.rand.Next(49) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HellbatWing"), 1);
                }
            }
            if (npc.type == NPCID.Lavabat)
            {
                if (Main.rand.Next(39) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HellbatWing"), 1);
                }
            }
            if (npc.type == NPCID.Harpy)
            {
                if (Main.rand.Next(50) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LightningCloud"), 1);
                }
            }
            if (Main.hardMode)
            {
                {
                    if (Main.rand.Next(1499) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("InfinityCrys"), 1);
                    }
                }
            }
            if (Main.bloodMoon)
            {
                {
                    if (Main.rand.Next(19) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FleshClump"), Main.rand.Next(1, 2));
                    }
                }
            }
            if (NPC.downedBoss3)
            {
                if ((Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneRockLayerHeight) && (!Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneDungeon))
                {
                    if (Main.rand.Next(999) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Lament"), 1);
                    }
                }
            }
            if (NPC.downedBoss3)
            {
                if ((Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneRockLayerHeight) && (!Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneDungeon))
                {
                    if (Main.rand.Next(999) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Fireweaver"), 1);
                    }
                }
            }
            if (NPC.downedBoss3)
            {
                if ((Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneRockLayerHeight) && (!Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneDungeon))
                {
                    if (Main.rand.Next(999) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ChargedBlasterRepeater"), 1);
                    }
                }
            }
            if (NPC.downedBoss3)
            {
                if ((Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneRockLayerHeight) && (!Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneJungle) && (!Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneDungeon))
                {
                    if (Main.rand.Next(999) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LightningRod"), 1);
                    }
                }
            }
            if ((Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneRockLayerHeight) && (!Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneDungeon))
            {
                if (Main.rand.Next(99) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AssassinsKnife"), 1);
                }
            }
            if (npc.type == NPCID.Zombie && (!Main.hardMode))
            {
                if (Main.rand.Next(12) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SteakShield"), 1);
                }
            }
            if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneRockLayerHeight)
            {
                if (Main.rand.Next(1999) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Tomato"), 1);
                }
            }
            #endregion

            #region Artifact Drops
            if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneDesert && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(800) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DiscordantAmber"), 1);
                }
            }
            if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneUnderworldHeight && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(800) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FieryJar"), 1);
                }
            }
            if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneSkyHeight && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(600) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("StrangeTotem"), 1);
                }
            }
            if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneSnow && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(800) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GlowingSlush"), 1);
                }
            }
            if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneBeach && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(800) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("OddWater"), 1);
                }
            }
            if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneTowerNebula && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(1200) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SoulOfPlight"), 1);
                }
            }
            if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneTowerSolar && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(1200) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SoulOfPlight"), 1);
                }
            }
            if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneTowerStardust && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(1200) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SoulOfPlight"), 1);
                }
            }
            if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneTowerVortex && !npc.SpawnedFromStatue)
            {
                if (Main.rand.Next(1200) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SoulOfPlight"), 1);
                }
            }
            #endregion

            #region Enemy Spawns
            if (NPC.downedMoonlord) //EoC
            {
                if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneDungeon && !npc.SpawnedFromStatue)
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
            if (Main.slimeRain && Main.slimeRainNPC[npc.type] && !NPC.AnyNPCs(mod.NPCType("ToySlime")))
            {
                int killsRequired = 75;
                if (NPC.downedSlimeKing)
                {
                    killsRequired /= 2;
                }
                if (Main.slimeRainKillCount == killsRequired)
                {
                    NPC.NewNPC((int)player.Center.X - 3000, (int)player.Center.Y - 1500, mod.NPCType("ToySlime"));
                }
            }
            if (npc.type == NPCID.MoonLordCore && !ElementsAwoken.ancientsAwakenedEnabled && MyWorld.moonlordKills < 5)
            {
                MyWorld.moonlordKills++;
                Main.NewText("The world has been blessed with Luminite!", Color.GreenYellow);
                for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 5E-05); k++) // xE-05 x is how many veins will spawn
                {
                    int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                    int y = WorldGen.genRand.Next((int)(Main.maxTilesY * .6f), Main.maxTilesY - 200); // WorldGen.worldSurfaceLow is actually the highest surface tile. In practice you might want to use WorldGen.rockLayer or other WorldGen values.

                    WorldGen.OreRunner(x, y, WorldGen.genRand.Next(3, 5), WorldGen.genRand.Next(4, 7), TileID.LunarOre);
                }
            }
            if (npc.type == mod.NPCType("VoidLeviathanHead") && MyWorld.voidLeviathanKills < 3)
            {
                MyWorld.voidLeviathanKills++;
                Main.NewText("The world has been blessed with Voidite!", Color.DeepPink);
                for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 4E-05); k++) // xE-05 x is how many veins will spawn
                {
                    int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                    int y = WorldGen.genRand.Next((int)(Main.maxTilesY * .4f), Main.maxTilesY - 200);

                    WorldGen.OreRunner(x, y, WorldGen.genRand.Next(3, 5), WorldGen.genRand.Next(4, 7), (ushort)mod.TileType("Voidite"));
                }
            }
        }
    }
}