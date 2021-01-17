using System;
using ElementsAwoken.Items.Placeable.Tiles.Plateau;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles.VolcanicPlateau
{
    public class MalignantFlesh : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            EAUtils.MergeOtherPlateauTiles(Type);
            Main.tileMerge[Type][Terraria.ID.TileID.BoneBlock] = true;
            Main.tileMerge[Type][Terraria.ID.TileID.Ash] = true;

            drop = ModContent.ItemType<MalignantFleshItem>();
            AddMapEntry(new Color(82, 49, 60));
            soundType = 0;
            soundStyle = 1;

            minPick = 50;
        }
    }
}