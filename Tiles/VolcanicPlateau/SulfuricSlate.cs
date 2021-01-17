using System;
using System.Collections.Generic;
using ElementsAwoken.Items.Placeable.Tiles.Plateau;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles.VolcanicPlateau
{
    public class SulfuricSlate : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<SulfuricSediment>()] = true;
            Main.tileMerge[Type][ModContent.TileType<IgneousRock>()] = true;
            Main.tileMerge[Type][ModContent.TileType<MalignantFlesh>()] = true;
            Main.tileMerge[Type][ModContent.TileType<ActiveIgneousRock>()] = true;
            Main.tileMerge[Type][ModContent.TileType<SulfuricBricks>()] = true;

            drop = ModContent.ItemType<SulfuricSlateItem>();
            AddMapEntry(new Color(115, 111, 42));
            soundType = 21;
            soundStyle = 6;

            minPick = 65;
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
        public override void RandomUpdate(int i, int j)
        {
            SulfuricSediment.GrowVine(i, j);
            SulfuricSediment.GrowGeyser(i, j);


        }
    }
}