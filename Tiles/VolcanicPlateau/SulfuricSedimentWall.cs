using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles.VolcanicPlateau
{
    public class SulfuricSedimentWall : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            //drop = ModContent.ItemType<SulfuricBrickWallItem>();
            AddMapEntry(new Color(57, 82, 24));
        }
    }
}