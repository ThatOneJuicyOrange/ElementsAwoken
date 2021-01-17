using System;
using ElementsAwoken.Items.Placeable.Tiles.Plateau;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles.VolcanicPlateau
{
    public class PyroclasticRock : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            EAUtils.MergeOtherPlateauTiles(Type);

            drop = ModContent.ItemType<PyroclasticRockItem>();
            AddMapEntry(new Color(22, 25, 36));
            soundType = 21;
            soundStyle = 6;

            minPick = 20;
        }
    }
}