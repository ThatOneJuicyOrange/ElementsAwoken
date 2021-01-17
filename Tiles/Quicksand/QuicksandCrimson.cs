using System;
using ElementsAwoken.Items.Placeable.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles.Quicksand
{
    public class QuicksandCrimson : Quicksand
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            drop = ModContent.ItemType<QuicksandCrimsonItem>();
            AddMapEntry(new Color(53, 44, 41));
            GlobalTiles.quicksands.Add(Type);
            TileID.Sets.Crimson[Type] = true;
        }
    }
}