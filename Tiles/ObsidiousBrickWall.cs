using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles
{
    public class ObsidiousBrickWall : ModWall
    {
        public override void SetDefaults()
        {
            //drop = ModContent.ItemType<SulfuricBrickWallItem>();
            AddMapEntry(new Color(57, 51, 74));
        }
    }
}