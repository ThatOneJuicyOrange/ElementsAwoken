using ElementsAwoken.BaseMod;
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
    public class QuicksandGeneration
    {
        public static void Generate()
        {
            int desertWidth = WorldGen.UndergroundDesertLocation.Width;
            int genSpot = WorldGen.UndergroundDesertLocation.X;
            while (genSpot < WorldGen.UndergroundDesertLocation.X + desertWidth)
            {
                int radiusX = WorldGen.genRand.Next(6, 15);
                int radiusY = WorldGen.genRand.Next(4, 7);

                int i = genSpot;
                genSpot += WorldGen.genRand.Next(radiusX + 20, radiusX + 60);
                int j = WorldGen.UndergroundDesertLocation.Y - 100;
                for (int t = 0; t < 200; t++)
                {
                    Tile tile = Framing.GetTileSafely(i, j + t);
                    if (tile.active() && tile.type == TileID.Sand)
                    {
                        j += t - radiusY / 2;
                        break;
                    }
                }

                for (int z = 0; z < radiusY * 2; z++) // we work downwards
                {
                    float ratio = z / (float)(radiusY * 2); // This will give us a ratio from 0 to 1, depending on how far down we are
                    int width = (int)(Math.Sin(ratio * Math.PI) * radiusX * 2); // This will give us the width of the ellipsoid at height y

                    for (int q = -width / 2; q < width / 2; q++)
                    {
                        Vector2 tilePosition = new Vector2(i, j) + new Vector2(q, z);
                        Tile t = Framing.GetTileSafely((int)tilePosition.X, (int)tilePosition.Y);
                        if (t.type == TileID.Sand)
                        {
                            WorldGen.KillTile((int)tilePosition.X, (int)tilePosition.Y);
                            WorldGen.PlaceTile((int)tilePosition.X, (int)tilePosition.Y, TileType<Tiles.Quicksand.Quicksand>());
                        }
                    }
                }
            }
        }
    }
}
