using ElementsAwoken.BaseMod;
using ElementsAwoken.Tiles;
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
    public class ObsidiousArena
    {
        public static int yValue = Main.maxTilesY - 30;
        public static void GenerateCaves()
        {
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(0, 255, 255)] = TileID.Ash;
            colorToTile[new Color(255, 0, 0)] = TileID.GoldBrick;
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1;

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(0, 0, 255)] = WallType<ObsidiousBrickWall>();
            colorToWall[new Color(150, 150, 150)] = 0; //turn into air
            colorToWall[Color.Black] = -1;

            Texture2D tex = GetTexture("ElementsAwoken/Structures/AshenCaves");
            TexGen gen = BaseWorldGenTex.GetTexGenerator(tex, colorToTile, liquidTex: GetTexture("ElementsAwoken/Structures/AshenCavesLava"));
            int x = 500 - tex.Width / 2;
            if (ElementsAwoken.calamityEnabled) x = Main.maxTilesX - tex.Width - 100;
            int y = yValue - tex.Height;
            gen.Generate(x, y, true, true, true, ElementsAwoken.calamityEnabled);
            for (int i = 0; i < tex.Width; i++)
            {
                for (int j = 0; j < tex.Height; j++)
                {
                    int tX = x + i;
                    int tY = y + j;
                    if (Main.tile[tX, tY].type == TileID.GoldBrick)
                    {
                        WorldGen.KillTile(tX, tY);
                        WorldGen.PlaceTile(tX, tY, TileID.Platforms, true, true, style: 13);
                    }
                }
            }
        }
        public static void Generate()
        {
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(0, 0, 255)] = TileType<ObsidiousBrick>();
            colorToTile[new Color(0, 255, 255)] = TileID.Ash;
            colorToTile[new Color(255, 0, 0)] = TileID.GoldBrick;
            colorToTile[new Color(255, 0, 255)] = TileID.PlatinumBrick;
            colorToTile[new Color(255, 255, 0)] = TileType<ObsidiousArenaManager>();
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; 

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(0, 0, 255)] = WallType<ObsidiousBrickWall>();
            colorToWall[new Color(255, 0, 0)] = WallID.Lavafall;
            colorToWall[new Color(150, 150, 150)] = 0; //turn into air
            colorToWall[Color.Black] = -1;

            Texture2D tex = GetTexture("ElementsAwoken/Structures/ObsidiousArena");
            TexGen gen = BaseWorldGenTex.GetTexGenerator(tex, colorToTile, GetTexture("ElementsAwoken/Structures/ObsidiousArenaWalls"), colorToWall);
            int x = 500 - tex.Width / 2;
            if (ElementsAwoken.calamityEnabled) x = Main.maxTilesX - tex.Width - 100 - 51;
            int y = yValue - 69 - tex.Height;
            EAWorldGen.obsidiousTempleLoc.X = x;
            EAWorldGen.obsidiousTempleLoc.Y = y;
            gen.Generate(x, y, true, true, true, ElementsAwoken.calamityEnabled);
            
            for (int i = 0; i < tex.Width; i++)
            {
                for (int j = 0; j < tex.Height; j++)
                {
                    int tX = x + i;
                    int tY = y + j;
                    if (Main.tile[tX, tY].type == TileID.GoldBrick)
                    {
                        WorldGen.KillTile(tX, tY);
                        WorldGen.PlaceTile(tX, tY, TileID.Platforms, true, true, style: 13);
                    }
                    if (Main.tile[tX, tY].type == TileID.PlatinumBrick)
                    {
                        WorldGen.KillTile(tX, tY);
                        WorldGen.PlaceObject(tX, tY, TileType<Tiles.Objects.ObsidiousDoor>());
                    }
                }
            }
        }
    }
}
