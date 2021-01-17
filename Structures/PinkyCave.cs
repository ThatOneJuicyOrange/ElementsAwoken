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
    public class PinkyCave
    {
        public static void Generate()
        {
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(28, 216, 94)] = TileID.Grass;
            colorToTile[new Color(151, 107, 75)] = TileID.Dirt;
            colorToTile[new Color(0, 0, 255)] = TileID.GoldBrick;
            colorToTile[new Color(255, 0, 0)] = TileType<Tiles.PinkyCaveManager>();
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1;

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(30, 80, 48)] = WallID.FlowerUnsafe;
            colorToWall[new Color(150, 150, 150)] = 0; //turn into air
            colorToWall[Color.Black] = -1;

            Texture2D tex = GetTexture("ElementsAwoken/Structures/PinkyCave");
            TexGen gen = BaseWorldGenTex.GetTexGenerator(tex, colorToTile, GetTexture("ElementsAwoken/Structures/PinkyCaveWalls"), colorToWall);

            int x = Main.spawnTileX + (Main.rand.NextBool() ? Main.rand.Next(60, 120) : Main.rand.Next(-120, 60));
            int y = Main.rand.Next((int)Main.worldSurface + 20, (int)Main.rockLayer - 20);

            EAWorldGen.pinkyCaveLoc = new Point(x, y);

            gen.Generate(x, y, true, true, true);

            for (int i = 0; i < tex.Width; i++)
            {
                for (int j = 0; j < tex.Height; j++)
                {
                    int tX = x + i;
                    int tY = y + j;
                    if (Main.tile[tX, tY].type == TileID.GoldBrick)
                    {
                        WorldGen.KillTile(tX, tY);
                        WorldGen.PlaceObject(tX, tY, TileID.LargePiles2, true, WorldGen.genRand.Next(14,17));
                    }
                }
            }
            PlaceBezierRope(new Point(x + 6, y + 9), new Point(x + 13, y + 5), TileType<Tiles.BezierRope.PinkyRope>());
            PlaceBezierRope(new Point(x + 21, y + 7), new Point(x + 10, y + 7), TileType<Tiles.BezierRope.PinkyRope>());
            PlaceBezierRope(new Point(x + 16, y + 5), new Point(x + 24, y + 10), TileType<Tiles.BezierRope.PinkyRope>());
        }
        private static void PlaceBezierRope(Point startTile, Point endTile, int type)
        {
            Tile start = Framing.GetTileSafely(startTile.X, startTile.Y);
            WorldGen.PlaceTile(startTile.X, startTile.Y, type, true, true);
            start.frameX = (short)endTile.X;
            start.frameY = (short)endTile.Y;
        }
    }
}
