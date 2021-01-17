using ElementsAwoken.BaseMod;
using ElementsAwoken.Tiles.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Structures
{
    public class Fountain
    {
        public static void Generate()
        {
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(255, 0, 0)] = TileID.GrayBrick;
            colorToTile[new Color(0, 0, 255)] = TileID.Stone;
            colorToTile[new Color(0, 255, 255)] = TileID.BlueMoss;
            colorToTile[new Color(255, 255, 0)] = TileID.PlatinumBrick;
            colorToTile[new Color(255, 0, 255)] = TileID.Chain;
            colorToTile[new Color(0, 255, 0)] = TileID.Platforms;
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; 

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(255, 0, 0)] = WallID.Stone;
            colorToWall[new Color(0, 255, 0)] = WallID.GrassUnsafe;
            colorToWall[new Color(0, 0, 255)] = WallID.GrayBrick;
            colorToWall[new Color(150, 150, 150)] = 0; //turn into air
            colorToWall[Color.Black] = -1;

            Dictionary<Color, int> colorToSlope = new Dictionary<Color, int>();
            colorToSlope[new Color(60, 255, 0)] = 1;
            colorToSlope[new Color(206, 0, 255)] = 2;
            colorToSlope[new Color(0, 30, 255)] = 3;
            colorToSlope[new Color(0, 255, 255)] = 4;
            colorToSlope[new Color(255, 0, 0)] = 99;
            colorToSlope[new Color(150, 150, 150)] = 0;
            colorToSlope[Color.Black] = -2;

            Texture2D tex = GetTexture("ElementsAwoken/Structures/Fountain");
            TexGen gen = BaseWorldGenTex.GetTexGenerator(tex, colorToTile, GetTexture("ElementsAwoken/Structures/FountainWalls"), colorToWall, GetTexture("ElementsAwoken/Structures/FountainWater"), null, GetTexture("ElementsAwoken/Structures/FountainSlopes"), colorToSlope, platformStyle: 10);
            int num = (int)(Main.maxTilesX / 1000);
            for (int g = 0; g < num; g++)
            {
                int x = WorldGen.genRand.Next(Main.maxTilesX / 4, Main.maxTilesX - Main.maxTilesX / 4);
                int y = Main.maxTilesY - 300 - WorldGen.genRand.Next(0, 150);
                gen.Generate(x, y, true, true, false);

                for (int i = 0; i < tex.Width; i++)
                {
                    for (int j = 0; j < tex.Height; j++)
                    {
                        int tX = x + i;
                        int tY = y + j;
                        if (Main.tile[tX, tY].type == TileID.PlatinumBrick)
                        {
                            WorldGen.KillTile(tX, tY);
                            WorldGen.PlaceTile(tX, tY, TileType<InvigorationFountain>());
                        }
                        /*if (Main.tile[tX, tY].type == TileID.GoldBrick)
                        {
                            WorldGen.KillTile(tX, tY);
                            WorldGen.PlaceTile(tX, tY, TileID.Torches,style:1);
                        }*/
                    }
                }
            }
        }
    }
}
