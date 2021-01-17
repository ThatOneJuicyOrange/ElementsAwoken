using System;
using ElementsAwoken.Items.Placeable.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles.Quicksand
{
    public class QuicksandHallow : Quicksand
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            drop = ModContent.ItemType<QuicksandHallowItem>();
            AddMapEntry(new Color(238, 225, 218));
            GlobalTiles.quicksands.Add(Type);
            TileID.Sets.Hallow[Type] = true;
        }
    }
}