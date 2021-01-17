using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System;
using Terraria.GameContent.Events;
using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using ElementsAwoken.Structures;
using Terraria.DataStructures;
using ReLogic.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using System.Linq;
using ElementsAwoken.Items.Consumable.Potions;
using Terraria.Localization;
using ElementsAwoken.Events.VoidEvent;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.NPCProj;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using ElementsAwoken.Events.RadiantRain.Enemies;
using ElementsAwoken.Tiles.VolcanicPlateau;
using ElementsAwoken.NPCs.VolcanicPlateau;
using ElementsAwoken.NPCs.VolcanicPlateau.Lake;
using ElementsAwoken.NPCs.Town;
using ElementsAwoken.Structures.VolcanicPlateau;

namespace ElementsAwoken
{
    public class EAWorldGen : ModWorld
    {    
        public static string[] mysteriousPotionColours = new string[10];

        public static int labPosition = 0;
        public static bool generatedLabs = false;

        public static bool genVoidite = false;
        public static bool genLuminite = false;
        public static bool genStellorite = false;

        public static bool generatedPlateaus = false;
        public static bool genAntHill = false;

        public static int plateauWidth = 1350;

        public static Point plateauLoc = Point.Zero;
        public static Point spiderTempleLoc = Point.Zero;
        public static Point mineBossArenaLoc = Point.Zero;
        public static Point obsidiousTempleLoc = Point.Zero;
        public static Point pinkyCaveLoc = Point.Zero;

        public static int jungleCenter = 0;
        public static int jungleWidth = 0;

        public static int jungleCenterSurface = 0;
        public static int jungleSurfaceWidth = 0;
        public static Rectangle jungleTempleRect = new Rectangle();
        /*public static int plateauLoc.X = 0;
        public static int plateauLoc.Y = 0;

        public static int spiderTempleLoc.X = 0;
        public static int spiderTempleLoc.Y = 0;

        public static int mineBossArenaLoc.X = 0;
        public static int mineBossArenaLoc.Y = 0;

        public static int obsidiousTempleLoc.X = 0;
        public static int obsidiousTempleLoc.Y = 0;*/

        public override void Initialize() // called when the world is loaded
        {
            generatedLabs = false;

            genVoidite = false;
            genLuminite = false;
            genStellorite = false;

            plateauLoc = Point.Zero;
            spiderTempleLoc = Point.Zero;
            mineBossArenaLoc = Point.Zero;
            obsidiousTempleLoc = Point.Zero;
            pinkyCaveLoc = Point.Zero;

            jungleCenter = 0;
            jungleTempleRect = new Rectangle(0,0,0,0);

            generatedPlateaus = false;
            genAntHill = false;
        }
        public override TagCompound Save()
        {
            TagCompound tag = new TagCompound();    

            tag["plateauLoc.X"] = plateauLoc.X;
            tag["plateauLoc.Y"] = plateauLoc.Y;
            tag["spiderTempleLoc.X"] = spiderTempleLoc.X;
            tag["spiderTempleLoc.Y"] = spiderTempleLoc.Y;
            tag["obsidiousTempleLoc.X"] = obsidiousTempleLoc.X;
            tag["obsidiousTempleLoc.Y"] = obsidiousTempleLoc.Y;
            tag["mineBossArenaLoc.X"] = mineBossArenaLoc.X;
            tag["mineBossArenaLoc.Y"] = mineBossArenaLoc.Y;
            tag["pinkyCaveLoc.X"] = pinkyCaveLoc.X;
            tag["pinkyCaveLoc.Y"] = pinkyCaveLoc.Y;

            tag["jungleCenter"] = jungleCenter;
            tag["jungleWidth"] = jungleWidth;
            tag["jungleCenterSurface"] = jungleCenterSurface;
            tag["jungleSurfaceWidth"] = jungleSurfaceWidth;

            tag["templeX"] = jungleTempleRect.X;
            tag["templeY"] = jungleTempleRect.Y;
            tag["templeWidth"] = jungleTempleRect.Width;
            tag["templeHeight"] = jungleTempleRect.Height;

            tag["generatedPlateaus"] = generatedPlateaus;

            for (int i = 0; i < mysteriousPotionColours.Length; i++)
            {
                tag["potColours" + i] = mysteriousPotionColours[i];
            }
            return tag;
        }
        public override void Load(TagCompound tag)
        {        
            generatedLabs = tag.GetBool("generatedLabs");

            plateauLoc.X = tag.GetInt("plateauLoc.X");
            plateauLoc.Y = tag.GetInt("plateauLoc.Y");
            spiderTempleLoc.X = tag.GetInt("spiderTempleLoc.X");
            spiderTempleLoc.Y = tag.GetInt("spiderTempleLoc.Y");
            obsidiousTempleLoc.X = tag.GetInt("obsidiousTempleLoc.X");
            obsidiousTempleLoc.Y = tag.GetInt("obsidiousTempleLoc.Y");
            mineBossArenaLoc.X = tag.GetInt("mineBossArenaLoc.X");
            mineBossArenaLoc.Y = tag.GetInt("mineBossArenaLoc.Y");
            pinkyCaveLoc.X = tag.GetInt("pinkyCaveLoc.X");
            pinkyCaveLoc.Y = tag.GetInt("pinkyCaveLoc.Y");

            jungleCenter = tag.GetInt("jungleCenter");
            jungleWidth = tag.GetInt("jungleWidth");
            jungleCenterSurface = tag.GetInt("jungleCenterSurface");
            jungleSurfaceWidth = tag.GetInt("jungleSurfaceWidth");

            jungleTempleRect.X = tag.GetInt("templeX");
            jungleTempleRect.Y = tag.GetInt("templeY");
            jungleTempleRect.Width = tag.GetInt("templeWidth");
            jungleTempleRect.Height = tag.GetInt("templeHeight");

            generatedPlateaus = tag.GetBool("generatedPlateaus");

            for (int i = 0; i < mysteriousPotionColours.Length; i++)
            {
                mysteriousPotionColours[i] = tag.GetString("potColours" + i);
            }
        }
        public override void NetSend(BinaryWriter writer)
        {     
            BitsByte flags1 = new BitsByte();
            flags1[0] = generatedLabs;
            flags1[1] = genLuminite;
            flags1[2] = genVoidite;
            flags1[3] = genStellorite;
            flags1[4] = genAntHill;
            writer.Write(flags1);

            // mysterious pots
            for (int i = 0; i < mysteriousPotionColours.Length; i++)
            {
                writer.Write(mysteriousPotionColours[i]);
            }

        }
        public override void NetReceive(BinaryReader reader)
        {           
            BitsByte flags1 = reader.ReadByte();
            generatedLabs = flags1[0];
            genLuminite = flags1[1];
            genVoidite = flags1[2];
            genStellorite = flags1[3];
            genAntHill = flags1[4];

            // mysterious pots
            for (int i = 0; i < mysteriousPotionColours.Length; i++)
            {
                mysteriousPotionColours[i] = reader.ReadString();
            }
            
        }

        // adding item in chests
        public override void PostWorldGen()
        {
            RandomisePotions();
            int[] dungeonItems = new int[] { mod.ItemType("CrashingWave") };
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 2 * 36)
                {
                    if (Main.rand.Next(5) == 0)
                    {
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                        {

                            if (chest.item[inventoryIndex].type == 0)
                            {
                                chest.item[inventoryIndex].SetDefaults(Main.rand.Next(dungeonItems));
                                break;
                            }
                        }
                    }
                }
                if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 1 * 36)
                {
                    if (Main.rand.Next(5) == 0)
                    {
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                        {
                            if (chest.item[inventoryIndex].type == 0)
                            {
                                chest.item[inventoryIndex].SetDefaults(mod.ItemType("MysteriousPotion"));
                                MysteriousPotion pot = (MysteriousPotion)chest.item[inventoryIndex].modItem;
                                pot.potionNum = Main.rand.Next(10);
                                break;
                            }
                        }
                    }
                }
            }

        }
        public override void PostUpdate()
        {
            if (genLuminite)
            {
                GenLuminite();
                genLuminite = false;
            }
            if (genStellorite)
            {
                GenStellorite();
                genStellorite = false;
            }
            if (genVoidite)
            {
                GenVoidite();
                genVoidite = false;
            }
            if (genAntHill)
            {
                VolcanicPlateau.PostMLChanges(plateauLoc.X, plateauLoc.Y);
                genAntHill = false;
            }
        }
        // thanks laugic !
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            // "Lihzahrd Altars" are basically the last thing to be generated
            if (!GetInstance<Config>().labsDisabled)
            {
                int genIndex2 = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
                tasks.Insert(genIndex2, new PassLegacy("Generating Labs", delegate (GenerationProgress progress)
                {
                    progress.Message = "Killing Scientists";
                    LabStructures();
                }));
            }
            //int plateauIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
            int plateauIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Jungle Chests"));
            tasks.Insert(plateauIndex, new PassLegacy("Volcanic Plateau", delegate (GenerationProgress progress)
            {
                progress.Message = "Charring hell some more";
                GenVolcanicPlateau();
                ObsidiousArena.GenerateCaves();
            }));
            int plateauIndex2 = tasks.FindIndex(genpass => genpass.Name.Equals("Stalac")); // Micro Biomes
            int findStructureIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Jungle Temple")); 
            int postMinecarts = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
            tasks.Insert(findStructureIndex, new PassLegacy("LocateStructures", delegate (GenerationProgress progress)
            {
                progress.Message = "Travelling the world";
                LocateStructures();
            })); 
            tasks.Insert(postMinecarts, new PassLegacy("Volcanic Plateau Structures", delegate (GenerationProgress progress)
            {
                progress.Message = "Absolutely incinerating hell";
                GenVolcanicPlateauStructures();
            }));
            tasks.Insert(plateauIndex2, new PassLegacy("EAStructures", delegate (GenerationProgress progress)
            {
                progress.Message = "Downloading DLC";
                SilvaWatchtower.Generate();
                ObsidiousArena.Generate();
                QuicksandGeneration.Generate();
            })); 
            tasks.Insert(postMinecarts, new PassLegacy("EAStructuresUnderground", delegate (GenerationProgress progress)
            {
                progress.Message = "Downloading More DLC";
                PinkyCave.Generate();
                OrderShrine.Generate();
                Fountain.Generate();
            }));
        }
        private void LocateStructures()
        {
            int leftMostJungle = 0;
            int rightMostJungle = 0;

            int leftMostJungleSurface = 0;
            int rightMostJungleSurface = 0;
            for (int x = 0; x < Main.maxTilesX; ++x)
            {
                for (int y = 0; y < Main.maxTilesY; ++y)
                {
                    Tile t = Main.tile[x, y];
                    if (t.type == TileID.LihzahrdBrick)
                    {
                        if (x < jungleTempleRect.X || jungleTempleRect.X == 0) jungleTempleRect.X = x;
                        if (y < jungleTempleRect.Y || jungleTempleRect.Y == 0) jungleTempleRect.Y = y;
                        if (x > jungleTempleRect.X && jungleTempleRect.X != 0) jungleTempleRect.Width = x - jungleTempleRect.X;
                        if (y > jungleTempleRect.Y && jungleTempleRect.Y != 0) jungleTempleRect.Height = y - jungleTempleRect.Y;
                    }
                    if (t.type == TileID.JungleGrass)
                    {
                        if (x < leftMostJungle || leftMostJungle == 0) leftMostJungle = x;
                        if (x > rightMostJungle || rightMostJungle == 0) rightMostJungle = x;
                    }
                }
            }
            for (int x = 0; x < Main.maxTilesX; ++x)
            {
                int y = 0;
                bool solidY = false;
                while(y < Main.maxTilesY && !solidY)
                {
                    Tile t = Main.tile[x, y];
                    if (t.type == TileID.JungleGrass)
                    {
                        if (x < leftMostJungleSurface || leftMostJungleSurface == 0) leftMostJungleSurface = x;
                        if (x > rightMostJungleSurface || rightMostJungleSurface == 0) rightMostJungleSurface = x;
                    }
                    if (Main.tileSolid[t.type] && t.active()) solidY = true;
                    y++;
                }
            }
            jungleCenter = (leftMostJungle + rightMostJungle) / 2;
            jungleCenterSurface = (leftMostJungleSurface + rightMostJungleSurface) / 2;
            jungleWidth = rightMostJungle - leftMostJungle;
            jungleSurfaceWidth = rightMostJungleSurface - leftMostJungleSurface;
        }
        private void LabStructures()
        {
            int s = 0;
            generatedLabs = true;
            for (int q = 0; q < 13; q++) // 13 different labs. dont try generating above 200, it runs out of space and crashes(might be fixed)
            {
                int xMin = Main.maxTilesX / 2 - Main.maxTilesX / 5;
                int xMax = Main.maxTilesX / 2 + Main.maxTilesX / 5;
                int labPosX = WorldGen.genRand.Next(xMin, xMax);

                int yMin = (int)(WorldGen.worldSurfaceHigh + 200.0);
                int yMax = Main.maxTilesY - 230;
                int labPosY = WorldGen.genRand.Next(yMin, yMax);

                s = GenerateLab(s, labPosX, labPosY);
            }
        }

        private int GenerateLab(int s, int structX, int structY)
        {
            if (TileCheckSafe(structX, structY))
            {
                if (!Chest.NearOtherChests(structX, structY))
                {
                    if (structY < Main.maxTilesY - 220) // wont generate in hell (it shouldnt anyway because the max is above hell still)
                    {
                        bool mirrored = false;
                        if (Main.rand.Next(2) == 0)
                            mirrored = true;

                        PickLab(s, structX, structY, mirrored);
                        s++;
                        if (s > 12)
                        {
                            s = 0; // this should never happen but just to be safe
                        }
                    }
                }
            }
            return s;
        }
        private void PickLab(int s, int structX, int structY, bool mirrored)
        {
            if (s == 0)
            {
                WastelandLab.StructureGen(structX, structY, mirrored); // create the structure first
                WastelandLabPlatforms.StructureGen(structX, structY, mirrored); // then create the platforms (on top of the walls)
                WastelandLabFurniture.StructureGen(structX, structY, mirrored); // then the furniture
            }
            else if (s == 12)
            {
                AncientsLab.Generate(structX, structY, mirrored);
            }
            else
            {
                Lab.StructureGen(structX, structY, mirrored); // create the structure first
                LabPlatforms.StructureGen(structX, structY, mirrored); // create the structure first
                LabFurniture.StructureGen(structX, structY, mirrored, s - 1); // then the furniture. s - 1 so the drives start at 0 again
            }

        }

        public static void PlaceLabLocker(int x, int y, ushort floorType)
        {
            ClearSpaceForChest(x, y, floorType);
            int chestIndex = WorldGen.PlaceChest(x, y, (ushort)ElementsAwoken.instance.TileType("Locker"), false, 0);

            int specialItem = GetLabLoot();
            labPosition++;
            int[] oreLoot = GetOreLoot();
            int[] potionLoot = GetPotionLoot();
            int[] money = GetMoneyLoot();
            int[] miscLoot = GetMiscLoot();

            int[] itemsToPlaceInChests = new int[] { specialItem, oreLoot[0], potionLoot[0], money[0], miscLoot[0] };
            int[] itemCounts = new int[] { 1, oreLoot[1], potionLoot[1], money[1], miscLoot[1] };

            FillChest(chestIndex, itemsToPlaceInChests, itemCounts);
        }
        private static int GetLabLoot()
        {
            int[] labLoot = new int[] {
                ElementsAwoken.instance.ItemType("Coilgun"),
                ElementsAwoken.instance.ItemType("Electrozzitron"),
                ElementsAwoken.instance.ItemType("TeslaRod"),
                ElementsAwoken.instance.ItemType("BassBooster"),
                ElementsAwoken.instance.ItemType("Taser"),
                ElementsAwoken.instance.ItemType("RustedMechanism"),
            };

            if (labPosition < labLoot.GetLength(0))
                return labLoot[labPosition];
            else
            {
                labPosition = 0;
                return labLoot[labPosition];
            }
        }
        private static int[] GetOreLoot()
        {
            int[] oreLoot = new int[] { ItemID.GoldBar, ItemID.PlatinumBar, ItemID.TungstenBar, ItemID.SilverBar };
            int orePos = Main.rand.Next(oreLoot.GetLength(0));
            int oreCount = Main.rand.Next(6, 16);
            int[] ore = { 0, 0 };
            ore[0] = oreLoot[orePos];
            ore[1] = oreCount;
            return ore;
        }

        private static int[] GetPotionLoot()
        {
            int[] potLoot = new int[] { ItemID.InfernoPotion, ItemID.LifeforcePotion, ItemID.WrathPotion };
            int potPos = Main.rand.Next(potLoot.GetLength(0));
            int potCount = Main.rand.Next(2, 5);
            int[] pot = { 0, 0 };
            pot[0] = potLoot[potPos];
            pot[1] = potCount;
            return pot;
        }

        private static int[] GetMoneyLoot()
        {
            int monType = 0;
            int monCount = 0;
            if (Main.rand.Next(2) == 0)
            {
                monType = ItemID.GoldCoin;
                monCount = Main.rand.Next(1, 4);
            }
            else
            {
                monType = ItemID.SilverCoin;
                monCount = Main.rand.Next(60, 99);
            }
            int[] mon = { 0, 0 };
            mon[0] = monType;
            mon[1] = monCount;
            return mon;
        }
        private static int[] GetMiscLoot()
        {
            int[] mscLoot = new int[] { ElementsAwoken.instance.ItemType("Capacitor"), ElementsAwoken.instance.ItemType("CopperWire"), ElementsAwoken.instance.ItemType("GoldWire"), };
            int mscPos = Main.rand.Next(mscLoot.GetLength(0));
            int mscCount = Main.rand.Next(2, 6);
            int[] msc = { 0, 0 };
            msc[0] = mscLoot[mscPos];
            msc[1] = mscCount;
            return msc;
        }
        private void GenVolcanicPlateau()
        {
            Texture2D texture = GetTexture("ElementsAwoken/Structures/VolcanicPlateau/PlateauBase");
            plateauLoc.X = Main.maxTilesX / 2 + 400;
            plateauLoc.Y = Main.maxTilesY - texture.Height - 10;
            VolcanicPlateau.Generate(plateauLoc.X, plateauLoc.Y);
            generatedPlateaus = true;
        }
        private void GenVolcanicPlateauStructures()
        {
            VolcanicPlateau.GenerateStructures(plateauLoc.X, plateauLoc.Y);
            VolcanicPlateau.DrainLava(plateauLoc.X, plateauLoc.Y);
        }
        private static void ClearSpaceForChest(int x, int y, ushort floorType)
        {
            WorldGen.KillTile(x, y);
            WorldGen.KillTile(x, y - 1);
            WorldGen.KillTile(x + 1, y - 1);
            WorldGen.KillTile(x + 1, y);
            WorldGen.PlaceTile(x + 1, y + 1, floorType, true, true);
            WorldGen.PlaceTile(x, y + 1, floorType, true, true);
            Main.tile[x, y].liquid = 0;
            Main.tile[x + 1, y].liquid = 0;
            Main.tile[x, y + 1].liquid = 0;
            Main.tile[x + 1, y + 1].liquid = 0;
        }
        private static void FillChest(int chestIndex, int[] itemsToPlaceInChests, int[] itemCounts)
        {
            if (chestIndex < Main.chest.GetLength(0) && chestIndex >= 0)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null)
                {
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == 0 && inventoryIndex < itemsToPlaceInChests.GetLength(0))
                        {
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInChests[inventoryIndex]);
                            chest.item[inventoryIndex].stack = itemCounts[inventoryIndex];
                        }
                    }
                }
            }
        }
        public static bool TileCheckSafe(int i, int j)
        {
            if (i > 0 && i < Main.maxTilesX && j > 0 && j < Main.maxTilesY)
                return true;
            return false;
        }   
        public static void GenVoidite()
        {
            if (Main.netMode == 1 || WorldGen.noTileActions || WorldGen.gen)
            {
                return;
            }
            string text = "The world has been cursed with Voidite...";
            if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.DeepPink);
            else NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.DeepPink);
            for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 4E-05); k++) // xE-05 x is how many veins will spawn
            {
                int x = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
                int y = WorldGen.genRand.Next((int)(Main.maxTilesY * .4f), Main.maxTilesY - 200);

                WorldGen.OreRunner(x, y, WorldGen.genRand.Next(3, 5), WorldGen.genRand.Next(4, 7), (ushort)(ushort)ElementsAwoken.instance.TileType("Voidite"));
            }
        }
        public static void GenLuminite()
        {
            if (Main.netMode == 1 || WorldGen.noTileActions || WorldGen.gen)
            {
                return;
            }

            string text = "The world has been blessed with Luminite!";
            if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.GreenYellow);
            else NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.GreenYellow);
            for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 5E-05); k++) // xE-05 x is how many veins will spawn
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)(Main.maxTilesY * .6f), Main.maxTilesY - 200); // WorldGen.worldSurfaceLow is actually the highest surface tile. In practice you might want to use WorldGen.rockLayer or other WorldGen values.

                WorldGen.OreRunner(x, y, WorldGen.genRand.Next(3, 5), WorldGen.genRand.Next(4, 7), TileID.LunarOre);
            }
        }
        public static void GenStellorite()
        {
            if (Main.netMode == 1 || WorldGen.noTileActions || WorldGen.gen)
            {
                return;
            }

            // string text = "The world has been blessed with Luminite!";
            //if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.GreenYellow);
            //else NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.GreenYellow);
            int trials = 0;
            int numGen = 0;
            while (trials < 20000 && numGen < 50)
            {
                trials++;
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next(75, (int)(Main.maxTilesY * .8f)); 

                if (Framing.GetTileSafely(x, y).type == TileID.Cloud)
                {
                    WorldGen.KillTile(x, y, noItem: true);
                    WorldGen.PlaceTile(x, y, TileType<Tiles.Stellorite>());
                    numGen++;
                }
            }
            ElementsAwoken.DebugModeText("Stellorite Generated: " + numGen + " / 50");
        }
        public static void RandomisePotions()
        {
            var nums = Enumerable.Range(0, 10).ToArray();

            // Shuffle the array
            for (int i = 0; i < nums.Length; ++i)
            {
                int randomIndex = Main.rand.Next(nums.Length);
                int temp = nums[randomIndex];
                nums[randomIndex] = nums[i];
                nums[i] = temp;
            }
            for (int i = 0; i < nums.Length; ++i)
            {
                switch (nums[i])
                {
                    case 0:
                        mysteriousPotionColours[i] = "Red";
                        break;
                    case 1:
                        mysteriousPotionColours[i] = "Cyan";
                        break;
                    case 2:
                        mysteriousPotionColours[i] = "Lime";
                        break;
                    case 3:
                        mysteriousPotionColours[i] = "Fuschia";
                        break;
                    case 4:
                        mysteriousPotionColours[i] = "Pink";
                        break;
                    case 5:
                        mysteriousPotionColours[i] = "Brown";
                        break;
                    case 6:
                        mysteriousPotionColours[i] = "Orange";
                        break;
                    case 7:
                        mysteriousPotionColours[i] = "Yellow";
                        break;
                    case 8:
                        mysteriousPotionColours[i] = "Blue";
                        break;
                    case 9:
                        mysteriousPotionColours[i] = "Purple";
                        break;
                    default:
                        mysteriousPotionColours[i] = "Red";
                        break;
                }
                ElementsAwoken.DebugModeText(nums[i] + " " + mysteriousPotionColours[i]);
            }
        }
    }
}