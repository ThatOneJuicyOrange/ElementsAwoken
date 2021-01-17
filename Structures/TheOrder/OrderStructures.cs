using BaseMod;
using ElementsAwoken.BaseMod;
using ElementsAwoken.Tiles.VolcanicPlateau;
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
    public class OrderStructures
    {
        public static void GenRitualSite(int x, int y)
        {
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(0, 0, 255)] = TileID.IridescentBrick;
            colorToTile[new Color(34, 75, 86)] = TileID.Marble; // fucking furniture
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(50, 20, 80)] = WallID.IridescentBrick;
            colorToWall[new Color(150, 150, 150)] = 0; //turn into air
            colorToWall[Color.Black] = -1; //don't touch when genning

            Texture2D tex = GetTexture("ElementsAwoken/Structures/TheOrder/RitualSite");
            Texture2D wallsTex = GetTexture("ElementsAwoken/Structures/TheOrder/RitualSiteWalls");
            TexGen gen = BaseWorldGenTex.GetTexGenerator(tex, colorToTile, wallsTex, colorToWall);
            gen.Generate(x, y, true, true, true);
            for (int i = x; i < x + gen.width; i++)
            {
                for (int j = y; j < y + gen.height; j++)
                {
                    Tile t = Framing.GetTileSafely(i, j);
                    t.liquid = 0;
                }
            }
        }
    }
}
