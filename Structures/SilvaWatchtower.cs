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
    public class SilvaWatchtower
    {
        public static void Generate()
        {
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(92, 68, 73)] = TileID.Mudstone;
            colorToTile[new Color(157, 157, 107)] = TileID.BoneBlock;
            colorToTile[new Color(145, 81, 85)] = TileID.RichMahogany;
            colorToTile[new Color(221, 136, 144)] = TileID.LivingMahogany;
            colorToTile[new Color(131, 206, 12)] = TileID.LivingMahoganyLeaves;
            colorToTile[new Color(255, 0, 0)] = TileID.GoldBrick;
            colorToTile[new Color(255, 255, 0)] = TileID.PlatinumBrick;
            colorToTile[new Color(255, 0, 255)] = TileID.CopperBrick;
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1;

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(115, 65, 68)] = WallID.RichMahoganyFence;
            colorToWall[new Color(53, 39, 41)] = WallID.MudstoneBrick;
            colorToWall[new Color(64, 64, 64)] = WallID.MetalFence;
            colorToWall[new Color(121, 60, 128)] = WallID.RichMaogany; //MAOGANY
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

            Texture2D tex = GetTexture("ElementsAwoken/Structures/SilvaWatchtower");
            TexGen gen = BaseWorldGenTex.GetTexGenerator(tex, colorToTile, GetTexture("ElementsAwoken/Structures/SilvaWatchtowerWalls"), colorToWall, null, null, GetTexture("ElementsAwoken/Structures/SilvaWatchtowerSlopes"), colorToSlope);

            int distance = (int)((EAWorldGen.jungleSurfaceWidth / 2) * 0.5f);
            int x = EAWorldGen.jungleCenterSurface + Main.rand.Next(-distance, distance);
            if (x == 0) return;
            int y = 190;

            for (int j = 0; j < 600; j++)
            {
                Tile t = Framing.GetTileSafely(x, y + j);
                if (t.active() && Main.tileSolid[t.type])
                {
                    y += j;
                    break;
                }
            }
            y -= tex.Height;
            gen.Generate(x, y, true, true, false);

            for (int i = 0; i < tex.Width; i++)
            {
                for (int j = 0; j < tex.Height; j++)
                {
                    int tX = x + i;
                    int tY = y + j;
                    if (Main.tile[tX, tY].type == TileID.GoldBrick)
                    {
                        WorldGen.KillTile(tX, tY);
                        WorldGen.PlaceObject(tX, tY, TileID.Tables, true, 2);
                    }
                    if (Main.tile[tX, tY].type == TileID.PlatinumBrick)
                    {
                        WorldGen.KillTile(tX, tY);
                        WorldGen.PlaceObject(tX, tY, TileID.Chairs, true, 3);
                    }
                    if (Main.tile[tX, tY].type == TileID.CopperBrick)
                    {
                        WorldGen.KillTile(tX, tY);
                        WorldGen.PlaceObject(tX, tY, TileType<SilvaFlag>(), true);
                    }
                }
            }
        }
    }
}
