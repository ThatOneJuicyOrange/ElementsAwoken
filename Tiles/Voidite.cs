using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles
{
    public class Voidite : ModTile
    {
        public override void SetDefaults()
        {
            //AddToArray(ref TileID.Sets.Conversion.Stone);
            //TileID.Sets.Conversion.Stone[Type]=true;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = true;
            drop = mod.ItemType("VoiditeOre");
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Voidite");
            AddMapEntry(new Color(255, 127, 227), name);
            mineResist = 2f;
            //dustType = 6;
            minPick = 225;
            soundType = 21;
            soundStyle = 6;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (Main.rand.Next(500) == 0)
            {
                Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.PinkFlame, 0f, 0f, 0, default(Color), 1.0f);
            }
        }
    }
}