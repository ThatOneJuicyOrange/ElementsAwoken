using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Structures
{
    public class AzanaLab
    {
        public static void Generate(int x, int y)
        {
            GenTiles(x, y);
            GenFurniture();
        }
        private static void GenTiles(int x, int y)
        {
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(151, 171, 198)] = TileID.PlatinumBrick;
            colorToTile[new Color(223, 216, 187)] = TileID.MarbleBlock;
            colorToTile[new Color(127, 129, 126)] = TileID.Stone;
            colorToTile[new Color(191, 142, 110)] = TileID.Platforms;
            colorToTile[new Color(119, 105, 79)] = TileID.ClosedDoor;
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(48, 116, 193)] = WallID.MartianConduit;
            colorToWall[new Color(116, 178, 26)] = WallID.LunarBrickWall;
            colorToWall[new Color(190, 232, 229)] = WallID.PlatinumBrick;
            colorToWall[Color.Black] = -1; //don't touch when genning				

            TexGen gen = BaseWorldGenTex.GetTexGenerator(GetTexture("ElementsAwoken/Structures/AzanaLab"), colorToTile, GetTexture("ElementsAwoken/Structures/AzanaLabWalls"), colorToWall);
            gen.Generate(x - (gen.width / 2), y - (gen.height / 2), true, true);
        }
        private static void GenFurniture()
        {
        }
    }
}
