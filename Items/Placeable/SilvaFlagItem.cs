using ElementsAwoken.Tiles;
using ElementsAwoken.Tiles.Objects;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable
{
    public class SilvaFlagItem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 999;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;

            item.createTile = ModContent.TileType<SilvaFlag>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Silvan Flag");
            Tooltip.SetDefault("Used by the Silva to mark their territory");
        }
    }
}
