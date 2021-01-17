using System;
using ElementsAwoken.Items.Placeable.Tiles.Plateau;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles.VolcanicPlateau
{
    public class SulfuricQuicksand : Quicksand.Quicksand
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            drop = ModContent.ItemType<SulfuricQuicksandItem>();
            AddMapEntry(new Color(145, 151, 49));
            GlobalTiles.quicksands.Add(Type);
        }
    }
}