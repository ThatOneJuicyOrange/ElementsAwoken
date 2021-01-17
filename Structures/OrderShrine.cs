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
    public class OrderShrine
    {
        public static void Generate()
        {
            int x = Main.spawnTileX + (Main.rand.NextBool() ? Main.rand.Next(60,120) : Main.rand.Next(-120, 60));
            int y = (int)(Main.maxTilesY * 0.4f) + Main.rand.Next(20, 80);
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(0, 255, 0)] = TileID.IridescentBrick;
            colorToTile[new Color(255, 0, 0)] = TileID.CopperPlating;
            colorToTile[new Color(150, 150, 150)] = -2;
            colorToTile[Color.Black] = -1;

            Texture2D tex = GetTexture("ElementsAwoken/Structures/OrderShrine");
            TexGen gen = BaseWorldGenTex.GetTexGenerator(tex, colorToTile);
            x -= (gen.width / 2);
            y -= (gen.height / 2);
            gen.Generate(x, y, true, true);
            for (int i = x; i < x + tex.Width; i++)
            {
                for (int j = y + tex.Height; j > y; j--)
                {
                    Tile t = Framing.GetTileSafely(i, j);
                    t.liquid = 0;
                }
            }
            for (int i = x; i < x + tex.Width; i++)
            {
                for (int j = y + tex.Height; j > y; j--)
                {
                    Tile tM = Framing.GetTileSafely(i, j);
                    if (tM.type == TileID.CopperPlating)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceObject(i, j, TileType<Tiles.Objects.OrderStatueTile>(), true);
                    }
                }
            }
        }
    }
}
