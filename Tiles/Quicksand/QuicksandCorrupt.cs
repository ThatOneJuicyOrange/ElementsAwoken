using System;
using ElementsAwoken.Items.Placeable.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles.Quicksand
{
    public class QuicksandCorrupt : Quicksand
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            drop = ModContent.ItemType<QuicksandCorruptItem>();
            AddMapEntry(new Color(103, 98, 122));
            GlobalTiles.quicksands.Add(Type);
            TileID.Sets.Corrupt[Type] = true;
        }
    }
}