using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles.VoidStone
{
    public class VoidStone : ModTile
    {
        public override void SetDefaults()
        {
            //AddToArray(ref TileID.Sets.Conversion.Stone);
            //TileID.Sets.Conversion.Stone[Type]=true;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = true;
            drop = mod.ItemType("VoidStone");   //put your CustomBlock name
            AddMapEntry(new Color(51, 51, 51));
            mineResist = 2f;
            //dustType = 6;
            minPick = 30;
            soundType = 21;
            soundStyle = 6;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (Main.rand.Next(300) == 0)
            {
                Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, 54, 0f, 0f, 0, default(Color), 1.0f);
            }
        }
    }
}