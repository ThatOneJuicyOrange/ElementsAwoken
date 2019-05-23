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
    public class WastelandLabFurniture
    {
        private static readonly int[,] StructureArray = new int[,]
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,3,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,2,7,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {0,0,0,0,5,0,0,0,5,0,0,0,6,0,0,0,5,0,0,0,5,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        };

        public static void StructureGen(int xPosO, int yPosO, bool mirrored)
        {
            /**
             * 0 = Do Nothing
             * 1 = Desk, Drive & Computer
             * 2 = Office Chair
             * 3 = Lab Light
             * 4 = Door
             * 5 = Scorpion Pod Small
             * 6 = Scorpion Pod Large
             * 7 = Dead Scientists
             * 9 = Kill Tile
             **/


            for (int i = 0; i < StructureArray.GetLength(1); i++)
            {
                for (int j = 0; j < StructureArray.GetLength(0); j++)
                {
                    if(mirrored)
                    {
                        if (TileCheckSafe((int)(xPosO + StructureArray.GetLength(1) - i), (int)(yPosO + j)))
                        {
                            if (StructureArray[j, i] == 1) // Desk, Computer & Drive
                            {
                                // origin in bottom middle
                                WorldGen.Place3x2(xPosO + StructureArray.GetLength(1) - i, yPosO + j, (ushort)ElementsAwoken.instance.TileType("Desk"), 1);
                                WorldGen.PlaceObject(xPosO + StructureArray.GetLength(1) - i + 1, yPosO + j - 2, (ushort)ElementsAwoken.instance.TileType("Computer"), false, 0, 1, -1, 0);
                                WorldGen.PlaceObject(xPosO + StructureArray.GetLength(1) - i - 1, yPosO + j - 2, ElementsAwoken.instance.TileType("WastelandDrive"), false, 0);
                            }
                            if (StructureArray[j, i] == 2) // Chair
                            {
                                // origin in bottom right
                                WorldGen.PlaceObject(xPosO + StructureArray.GetLength(1) - i + 1, yPosO + j, (ushort)ElementsAwoken.instance.TileType("OfficeChair"), false, 0, 1, -1, 1);
                            }
                            if (StructureArray[j, i] == 3) // Lab Light
                            {
                                WorldGen.PlaceObject(xPosO + StructureArray.GetLength(1) - i, yPosO + j, ElementsAwoken.instance.TileType("LabLight"), false, 0);
                            }
                            if (StructureArray[j, i] == 4) // Door
                            {
                                // WorldGen.KillTile(xPosO + i, yPosO + j);
                                WorldGen.PlaceDoor(xPosO + StructureArray.GetLength(1) - i, yPosO + j, 10, 20);
                            }
                            if (StructureArray[j, i] == 5) // Small Pod
                            {
                                WorldGen.Place3x2(xPosO + StructureArray.GetLength(1) - i, yPosO + j, (ushort)ElementsAwoken.instance.TileType("ScorpionPod"), 0);
                            }
                            if (StructureArray[j, i] == 6) // Large Pod
                            {
                                WorldGen.Place3x4(xPosO + StructureArray.GetLength(1) - i, yPosO + j, (ushort)ElementsAwoken.instance.TileType("ScorpionPodLarge"), 0);
                            }
                            if (StructureArray[j, i] == 7) // Scientist
                            {
                                // origin on left
                                WorldGen.PlaceObject(xPosO + StructureArray.GetLength(1) - i - 2, yPosO + j, ElementsAwoken.instance.TileType("DeadScientist"), false, 0, 1, -1, 1);
                            }
                            if (StructureArray[j, i] == 9) // Destroy Tile
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
                            if (StructureArray[j, i] == 1) // Desk, Computer & Drive
                            {
                                WorldGen.Place3x2(xPosO + i, yPosO + j, (ushort)ElementsAwoken.instance.TileType("Desk"), 0);
                                WorldGen.PlaceObject(xPosO + i, yPosO + j - 2, (ushort)ElementsAwoken.instance.TileType("Computer"), false , 0, 1, -1, 1);
                                WorldGen.PlaceObject(xPosO + i + 1, yPosO + j - 2, ElementsAwoken.instance.TileType("WastelandDrive"), false, 0);
                            }
                            if (StructureArray[j, i] == 2) // Chair
                            {
                                WorldGen.PlaceObject(xPosO + i, yPosO + j, (ushort)ElementsAwoken.instance.TileType("OfficeChair"), false, 0, 1, -1, 0);
                            }
                            if (StructureArray[j, i] == 3) // Lab Light
                            {
                                WorldGen.PlaceObject(xPosO + i, yPosO + j, ElementsAwoken.instance.TileType("LabLight"), false, 0);
                            }
                            if (StructureArray[j, i] == 4) // Door
                            {
                                WorldGen.PlaceDoor(xPosO + i, yPosO + j, 10, 20);
                            }
                            if (StructureArray[j, i] == 5) // Small Pod
                            {
                                WorldGen.Place3x2(xPosO + i, yPosO + j, (ushort)ElementsAwoken.instance.TileType("ScorpionPod"), 0);
                            }
                            if (StructureArray[j, i] == 6) // Large Pod
                            {
                                WorldGen.Place3x4(xPosO + i, yPosO + j, (ushort)ElementsAwoken.instance.TileType("ScorpionPodLarge"), 0);
                            }
                            if (StructureArray[j, i] == 7) // Scientist
                            {
                                WorldGen.PlaceObject(xPosO + i, yPosO + j, ElementsAwoken.instance.TileType("DeadScientist"), false, 0, 1, -1, 0);
                            }
                            if (StructureArray[j, i] == 9) // Destroy Tile
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