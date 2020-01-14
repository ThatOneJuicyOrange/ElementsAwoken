using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;

namespace ElementsAwoken.Structures
{
    // created with the help of basemod
    public class TexToGen
    {
        public static Dictionary<Color, int> colorToLiquid = colorToLiquid = null;

        //load the texture then the list of colours to tiles
        public static TexGen GetTexGenerator(Texture2D tileTex, Dictionary<Color, int> colorToTile, Texture2D wallTex = null, Dictionary<Color, int> colorToWall = null, Texture2D liquidTex = null)
        {
            if (colorToLiquid == null)
            {
                colorToLiquid = new Dictionary<Color, int>();
                colorToLiquid[new Color(0, 0, 255)] = 0;
                colorToLiquid[new Color(255, 0, 0)] = 1;
                colorToLiquid[new Color(255, 255, 0)] = 2;
            }
            Color[] tileData = new Color[tileTex.Width * tileTex.Height]; // make an array with the total amount of colours in the image
            tileTex.GetData(0, tileTex.Bounds, tileData, 0, tileTex.Width * tileTex.Height);

            Color[] wallData = null;
            if (wallTex != null)
            {
                wallData = new Color[wallTex.Width * wallTex.Height];
                wallTex.GetData(0, wallTex.Bounds, wallData, 0, wallTex.Width * wallTex.Height);
            }
            Color[] liquidData = null;
            if (liquidTex != null)
            {
                liquidData = new Color[liquidTex.Width * liquidTex.Height];
                liquidTex.GetData(0, liquidTex.Bounds, liquidData, 0, liquidTex.Width * liquidTex.Height);
            }

            int x = 0, y = 0;
            TexGen gen = new TexGen(tileTex.Width, tileTex.Height);
            for (int m = 0; m < tileData.Length; m++)
            {
                Color tileColor = tileData[m], wallColor = (wallTex == null ? Color.Black : wallData[m]), liquidColor = (liquidTex == null ? Color.Black : liquidData[m]);
                int tileID = (colorToTile.ContainsKey(tileColor) ? colorToTile[tileColor] : -1); //if no key assume no action
                int wallID = (colorToWall != null && colorToWall.ContainsKey(wallColor) ? colorToWall[wallColor] : -1);
                int liquidID = (colorToLiquid != null && colorToLiquid.ContainsKey(liquidColor) ? colorToLiquid[liquidColor] : -1);
                gen.tileGen[x, y] = new TileInfo(tileID, 0, wallID, liquidID, liquidID == -1 ? 0 : 255);
                x++;
                if (x >= tileTex.Width) { x = 0; y++; }
                if (y >= tileTex.Height) break; //you've somehow reached the end of the texture! (this shouldn't happen!)
            }
            return gen;
        }
    }
    public class TexGen
    {
        public int width, height;
        public TileInfo[,] tileGen;
        public int torchStyle = 0, platformStyle = 0;

        public TexGen(int w, int h)
        {
            width = w; height = h;
            tileGen = new TileInfo[width, height];
        }

        //where x, y is the top-left hand corner of the gen
        public void Generate(int x, int y, bool silent, bool sync)
        {
            for (int x1 = 0; x1 < width; x1++)
            {
                for (int y1 = 0; y1 < height; y1++)
                {
                    int x2 = x + x1, y2 = y + y1;
                    TileInfo info = tileGen[x1, y1];
                    if (info.tileID == -1 && info.wallID == -1 && info.liquidType == -1) continue;
                    if (info.tileID != -1) ReplaceTile(x2, y2, info.tileID);
                    if (info.wallID > -1) ReplaceWall(x2, y2, info.wallID);
                    if (info.liquidType > -1) ReplaceLiquid(x2, y2, info.liquidType);
                }
            }
        }
        private void ReplaceTile(int x, int y, int tileType, int style = 0)
        {
            WorldGen.KillTile(x, y);
            Main.tile[x, y].liquid = 0;
            WorldGen.PlaceTile(x, y, tileType, true, true, -1, style);
        }
        private void ReplaceWall(int x, int y, int wallType)
        {
            WorldGen.KillWall(x, y);
            WorldGen.PlaceWall(x, y, wallType, true);
        }
        private void ReplaceLiquid(int x, int y, int liquidType)
        {
            Main.tile[x, y].liquid = 255; // 255 is the maximum level of liquid
            if (liquidType == 0)
            {
                Main.tile[x, y].lava(false);
                Main.tile[x, y].honey(false);
            }
            else if (liquidType == 1)
            {
                Main.tile[x, y].lava(true);
                Main.tile[x, y].honey(false);
            }
            else if (liquidType == 2)
            {
                Main.tile[x, y].lava(false);
                Main.tile[x, y].honey(true);
            }
        }
    }

    public class TileInfo
    {
        public int tileID = -1, tileStyle = 0, wallID = -1;
        public int liquidType = -1, liquidAmt = 0; //liquidType can be 0 (water), 1 (lava), 2 (honey)
        public int slope = -2, wire = -1;

        public TileInfo(int id, int style, int wid = -1, int lType = -1, int lAmt = 0, int sl = -2, int w = -1)
        {
            tileID = id; tileStyle = style; wallID = wid; liquidType = lType; liquidAmt = lAmt; slope = sl; wire = w;
        }
    }
}
