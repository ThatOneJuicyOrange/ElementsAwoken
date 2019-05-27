using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Structures
{
    public class AncientsLab
    {
        private static readonly int[,] StructureArray = new int[,]
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,3,3,3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,5,5,5,5,5,5,5,5,5,5,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,5,7,7,7,7,7,7,7,7,5,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,5,7,6,6,6,6,6,6,5,5,5,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,5,7,6,6,6,6,6,6,5,5,1,1,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,5,7,6,6,6,6,6,6,1,1,1,1,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,3,3,3,3,3,3,3,3,3,3,3,3,3,3,2,2,2,2,2,6,6,2,2,2,2,1,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,3,5,5,5,5,5,5,5,5,5,3,4,4,4,4,4,4,3,7,6,6,7,3,9,1,1,1,0,0,0,0,0,1,1,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,3,5,5,5,5,5,5,5,5,5,3,3,3,3,3,3,3,3,7,6,6,7,3,1,1,1,1,0,0,0,0,1,1,1,1,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,3,5,5,5,5,5,5,5,5,7,7,7,7,7,7,7,7,7,7,6,6,7,3,1,1,1,0,0,0,0,1,1,1,1,1,1},
            {0,3,3,3,3,3,3,3,3,3,3,3,3,5,5,7,7,7,7,5,7,7,5,5,5,7,5,5,7,5,7,6,6,7,3,3,3,3,0,0,0,1,1,1,1,1,3,3},
            {3,3,3,5,5,5,5,5,5,5,3,3,3,7,7,7,6,6,7,7,7,5,5,5,5,7,5,5,7,5,7,6,6,7,3,5,9,0,0,0,0,1,1,1,3,3,3,3},
            {3,3,3,7,7,7,7,7,7,7,3,3,3,5,5,7,6,6,7,5,5,5,5,5,5,2,2,2,2,2,2,6,6,2,3,7,7,9,9,0,0,1,1,1,1,9,9,3},
            {9,9,5,5,7,6,6,6,7,5,5,5,5,5,5,7,6,6,7,5,5,5,5,5,5,7,7,7,7,2,7,6,6,7,5,5,9,9,9,9,1,1,1,1,1,9,5,3},
            {9,9,5,5,7,6,6,6,7,5,5,5,5,5,5,7,6,6,7,5,5,5,5,5,5,7,6,6,7,2,7,6,6,7,5,9,9,9,1,1,1,1,1,1,9,9,5,3},
            {9,9,5,5,7,6,6,6,7,5,5,5,5,5,5,7,6,6,7,5,5,5,5,5,5,7,6,6,7,2,7,6,6,7,5,9,1,1,1,1,1,1,1,9,9,5,5,3},
            {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        };
        private static readonly int[,] PlatformArray = new int[,]
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        };
        private static readonly int[,] FurnitureArray = new int[,]
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,3,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,4,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        };
        public static void Generate(int xPosO, int yPosO, bool mirrored)
        {
            StructureGen(xPosO, yPosO, mirrored);
            PlatformGen(xPosO, yPosO, mirrored);
            FurnitureGen(xPosO, yPosO, mirrored);
        }

        public static void StructureGen(int xPosO, int yPosO, bool mirrored)
        {
            /**
             * 0 = Do Nothing
             * 1 = Stone
             * 2 = Platinum Brick - 177
             * 3 = Smooth Marble - 357
             * 4 = Martian Conduit Plating
             * 5 = Platinum brick wall - 47
             * 6 = Martian conduit wall - 176
             * 7 = Luminite Wall - 224
             * 9 = Kill tile
             **/


            for (int i = 0; i < StructureArray.GetLength(1); i++)
            {
                for (int j = 0; j < StructureArray.GetLength(0); j++)
                {
                    if(mirrored)
                    {
                        if (TileCheckSafe((int)(xPosO + StructureArray.GetLength(1) - i), (int)(yPosO + j)))
                        {
                            if (StructureArray[j, i] == 1) // stone
                            {
                                WorldGen.KillTile(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + StructureArray.GetLength(1) - i, yPosO + j, TileID.Stone, true, true);
                                /*MyWorld.PlaceLabLocker(xPosO + StructureArray.GetLength(1) - i, yPosO + j, 177);
                                WorldGen.KillWall(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                                WorldGen.PlaceWall(xPosO + StructureArray.GetLength(1) - i, yPosO + j, WallID.PlatinumBrick, true);*/
                            }
                            if (StructureArray[j, i] == 2) // Platinum Brick
                            {
                                WorldGen.KillTile(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + StructureArray.GetLength(1) - i, yPosO + j, TileID.PlatinumBrick, true, true);
                            }
                            if (StructureArray[j, i] == 3) // Marble Block
                            {
                                WorldGen.KillTile(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + StructureArray.GetLength(1) - i, yPosO + j, TileID.MarbleBlock, true, true, -1, 13);
                            }
                            if (StructureArray[j, i] == 4) //  Martian Conduit Plating
                            {
                                WorldGen.KillTile(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + StructureArray.GetLength(1) - i, yPosO + j, TileID.MartianConduitPlating, true, true, -1, 13);
                            }
                            if (StructureArray[j, i] == 5) // Platinum Brick Wall
                            {
                                WorldGen.KillTile(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                                Main.tile[xPosO + StructureArray.GetLength(1) - i, yPosO + j].liquid = 0;
                                WorldGen.KillWall(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                                WorldGen.PlaceWall(xPosO + StructureArray.GetLength(1) - i, yPosO + j, WallID.PlatinumBrick, true);
                            }
                            if (StructureArray[j, i] == 6) // martian wall
                            {
                                WorldGen.KillTile(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                                Main.tile[xPosO + StructureArray.GetLength(1) - i, yPosO + j].liquid = 0;
                                WorldGen.KillWall(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                                WorldGen.PlaceWall(xPosO + StructureArray.GetLength(1) - i, yPosO + j, WallID.MartianConduit, true);
                            }
                            if (StructureArray[j, i] == 7) // luminite wall
                            {
                                WorldGen.KillTile(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                                Main.tile[xPosO + StructureArray.GetLength(1) - i, yPosO + j].liquid = 0;
                                WorldGen.KillWall(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                                WorldGen.PlaceWall(xPosO + StructureArray.GetLength(1) - i, yPosO + j, WallID.LunarBrickWall, true);
                            }
                            if (StructureArray[j, i] == 9) // destroy everything >:)
                            {
                                Main.tile[xPosO + StructureArray.GetLength(1) - i, yPosO + j].liquid = 0;
                                WorldGen.KillTile(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                            }
                        }
                    }
                    else
                    {
                        if (TileCheckSafe((int)(xPosO + i), (int)(yPosO + j)))
                        {
                            if (StructureArray[j, i] == 1)
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + i, yPosO + j, TileID.Stone, true, true);
                            }
                            if (StructureArray[j, i] == 2)
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + i, yPosO + j, TileID.PlatinumBrick, true, true);
                            }
                            if (StructureArray[j, i] == 3)
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + i, yPosO + j, TileID.MarbleBlock, true, true);
                            }
                            if (StructureArray[j, i] == 4)
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + i, yPosO + j, TileID.MartianConduitPlating, true, true);
                            }
                            if (StructureArray[j, i] == 5)
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                Main.tile[xPosO + i, yPosO + j].liquid = 0;
                                WorldGen.KillWall(xPosO + i, yPosO + j);
                                WorldGen.PlaceWall(xPosO + i, yPosO + j, WallID.PlatinumBrick, true);
                            }
                            if (StructureArray[j, i] == 6)
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                Main.tile[xPosO + i, yPosO + j].liquid = 0;
                                WorldGen.KillWall(xPosO + i, yPosO + j);
                                WorldGen.PlaceWall(xPosO + i, yPosO + j, WallID.MartianConduit, true);
                            }
                            if (StructureArray[j, i] == 7)
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                Main.tile[xPosO + i, yPosO + j].liquid = 0;
                                WorldGen.KillWall(xPosO + i, yPosO + j);
                                WorldGen.PlaceWall(xPosO + i, yPosO + j, WallID.LunarBrickWall, true);
                            }
                            if (StructureArray[j, i] == 9)
                            {
                                Main.tile[xPosO + i, yPosO + j].liquid = 0;
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                            }
                        }
                    }
                }
            }
        }
        public static void PlatformGen(int xPosO, int yPosO, bool mirrored)
        {
            /**
             * 1 = Martian Platform - 19 (27)
             * 2 = Martian Platform Stairs - 19 (27)
             **/


            for (int i = 0; i < PlatformArray.GetLength(1); i++)
            {
                for (int j = 0; j < PlatformArray.GetLength(0); j++)
                {
                    if (mirrored)
                    {
                        if (TileCheckSafe((int)(xPosO + PlatformArray.GetLength(1) - i), (int)(yPosO + j)))
                        {
                            if (PlatformArray[j, i] == 1)
                            {
                                WorldGen.KillTile(xPosO + PlatformArray.GetLength(1) - i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + PlatformArray.GetLength(1) - i, yPosO + j, 19, true, true, -1, 26);
                            }
                            if (PlatformArray[j, i] == 2) // platform
                            {
                                WorldGen.KillTile(xPosO + PlatformArray.GetLength(1) - i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + PlatformArray.GetLength(1) - i, yPosO + j, 19, true, true, -1, 26);
                                WorldGen.SlopeTile(xPosO + PlatformArray.GetLength(1) - i, yPosO + j, 1);
                            }
                            if (PlatformArray[j, i] == 9) // destroy everything >:)
                            {
                                Main.tile[xPosO + PlatformArray.GetLength(1) - i, yPosO + j].liquid = 0;
                                WorldGen.KillTile(xPosO + PlatformArray.GetLength(1) - i, yPosO + j);
                            }
                        }
                    }
                    else
                    {
                        if (TileCheckSafe((int)(xPosO + i), (int)(yPosO + j)))
                        {
                            if (PlatformArray[j, i] == 1)
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + i, yPosO + j, 19, true, true, -1, 26);
                            }
                            if (PlatformArray[j, i] == 2) // platform
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + i, yPosO + j, 19, true, true, -1, 26);
                                WorldGen.SlopeTile(xPosO + i, yPosO + j, 2);
                            }
                            if (PlatformArray[j, i] == 9)
                            {
                                Main.tile[xPosO + i, yPosO + j].liquid = 0;
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                            }
                        }
                    }
                }
            }
        }
        public static void FurnitureGen(int xPosO, int yPosO, bool mirrored)
        {
            /**
             * 0 = Do Nothing
             * 1 = Desk, Drive & Computer
             * 2 = Office Chair
             * 3 = Lab Light
             * 4 = Door
             * 5 = Locker
             * 6 = Bench & Crystal
             * 7 = Dead Scientist
             * 9 = Kill Tile
             **/

            for (int i = 0; i < FurnitureArray.GetLength(1); i++)
            {
                for (int j = 0; j < FurnitureArray.GetLength(0); j++)
                {
                    if (mirrored)
                    {
                        if (TileCheckSafe((int)(xPosO + FurnitureArray.GetLength(1) - i), (int)(yPosO + j)))
                        {
                            if (FurnitureArray[j, i] == 1) // Desk
                            {
                                // origin in bottom middle
                                WorldGen.Place3x2(xPosO + FurnitureArray.GetLength(1) - i, yPosO + j, (ushort)ElementsAwoken.instance.TileType("Desk"), 1);
                                WorldGen.PlaceObject(xPosO + FurnitureArray.GetLength(1) - i + 1, yPosO + j - 2, (ushort)ElementsAwoken.instance.TileType("Computer"), false, 0, 1, -1, 0);
                                WorldGen.PlaceObject(xPosO + FurnitureArray.GetLength(1) - i - 1, yPosO + j - 2, ElementsAwoken.instance.TileType("AncientsDrive"), false, 0);
                            }
                            if (FurnitureArray[j, i] == 2) // Chair
                            {
                                // origin in bottom right
                                WorldGen.PlaceObject(xPosO + FurnitureArray.GetLength(1) - i + 1, yPosO + j, (ushort)ElementsAwoken.instance.TileType("OfficeChair"), false, 0, 1, -1, 1);
                            }
                            if (FurnitureArray[j, i] == 3) // Lab Light
                            {
                                WorldGen.PlaceObject(xPosO + FurnitureArray.GetLength(1) - i, yPosO + j, ElementsAwoken.instance.TileType("LabLight"), false, 0);
                            }
                            if (FurnitureArray[j, i] == 4) // Door
                            {
                                WorldGen.PlaceDoor(xPosO + FurnitureArray.GetLength(1) - i, yPosO + j, 10, 20);
                            }
                            if (FurnitureArray[j, i] == 5) // Locker
                            {
                                MyWorld.PlaceLabLocker(xPosO + FurnitureArray.GetLength(1) - i - 1, yPosO + j, 177);
                            }
                            if (FurnitureArray[j, i] == 6) // Desk
                            {
                                // origin in bottom middle
                                WorldGen.Place4x2(xPosO + FurnitureArray.GetLength(1) - i - 1, yPosO + j, (ushort)ElementsAwoken.instance.TileType("LabBench"));
                                WorldGen.PlaceObject(xPosO + FurnitureArray.GetLength(1) - i, yPosO + j - 2, (ushort)ElementsAwoken.instance.TileType("CrystalContainer"), false, 0, 1, -1, 0);
                            }
                            if (FurnitureArray[j, i] == 7) // Scientist
                            {
                                // origin on left
                                WorldGen.PlaceObject(xPosO + FurnitureArray.GetLength(1) - i - 2, yPosO + j, ElementsAwoken.instance.TileType("DeadScientist"), false, 0, 1, -1, 1);
                            }
                            if (FurnitureArray[j, i] == 9) // destroy everything >:)
                            {
                                Main.tile[xPosO + FurnitureArray.GetLength(1) - i, yPosO + j].liquid = 0;
                                WorldGen.KillTile(xPosO + FurnitureArray.GetLength(1) - i, yPosO + j);
                            }
                        }
                    }
                    else
                    {
                        if (TileCheckSafe((int)(xPosO + i), (int)(yPosO + j)))
                        {
                            if (FurnitureArray[j, i] == 1) // Desk
                            {
                                WorldGen.Place3x2(xPosO + i, yPosO + j, (ushort)ElementsAwoken.instance.TileType("Desk"), 0);
                                WorldGen.PlaceObject(xPosO + i, yPosO + j - 2, (ushort)ElementsAwoken.instance.TileType("Computer"), false, 0, 1, -1, 1);
                                WorldGen.PlaceObject(xPosO + i + 1, yPosO + j - 2, ElementsAwoken.instance.TileType("AncientsDrive"), false, 0);
                            }
                            if (FurnitureArray[j, i] == 2) // Chair
                            {
                                WorldGen.PlaceObject(xPosO + i, yPosO + j, (ushort)ElementsAwoken.instance.TileType("OfficeChair"), false, 0, 1, -1, 0);
                            }
                            if (FurnitureArray[j, i] == 3) // Lab Light
                            {
                                WorldGen.PlaceObject(xPosO + i, yPosO + j, ElementsAwoken.instance.TileType("LabLight"), false, 0);
                            }
                            if (FurnitureArray[j, i] == 4) // Door
                            {
                                WorldGen.PlaceDoor(xPosO + i, yPosO + j, 10, 20);
                            }
                            if (FurnitureArray[j, i] == 5) // Locker
                            {
                                MyWorld.PlaceLabLocker(xPosO + i, yPosO + j, 177);
                            }
                            if (FurnitureArray[j, i] == 6) // Desk
                            {
                                WorldGen.Place4x2(xPosO + i, yPosO + j, (ushort)ElementsAwoken.instance.TileType("LabBench"));
                                WorldGen.PlaceObject(xPosO + i + 1, yPosO + j - 2, (ushort)ElementsAwoken.instance.TileType("CrystalContainer"), false, 0, 1, -1, 0);
                            }
                            if (FurnitureArray[j, i] == 7) // Scientist
                            {
                                WorldGen.PlaceObject(xPosO + i, yPosO + j, ElementsAwoken.instance.TileType("DeadScientist"), false, 0, 1, -1, 0);
                            }
                            if (FurnitureArray[j, i] == 9)
                            {
                                Main.tile[xPosO + i, yPosO + j].liquid = 0;
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                            }
                        }
                    }
                }
            }
        }
            //Making sure tiles arent out of bounds
            private static bool TileCheckSafe(int i, int j)
        {
            if (i > 0 && i < Main.maxTilesX && j > 0 && j < Main.maxTilesY)
                return true;
            return false;
        }
    }
}