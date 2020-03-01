using System;
using System.IO;
using ElementsAwoken.Items.Placeable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles
{
    public class Firebrick : ModTile
    {
        public override void SetDefaults()
        {
            //AddToArray(ref TileID.Sets.Conversion.Stone);
            //TileID.Sets.Conversion.Stone[Type]=true;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = true;

            Main.tileSpelunker[Type] = true;
            TileID.Sets.Ore[Type] = true;
            Main.tileValue[Type] = 1500;

            drop = ModContent.ItemType<FirebrickItem>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Voidite");
            AddMapEntry(new Color(255, 127, 227), name);
            soundType = 21;
            soundStyle = 6;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) 
        {
            r = 0.25f;
            g = 0f;
            b = 0.05f;
        }
    }
}