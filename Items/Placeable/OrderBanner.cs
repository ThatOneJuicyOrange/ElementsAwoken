using ElementsAwoken.Tiles;
using ElementsAwoken.Tiles.Objects;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable
{
    public class OrderBanner : ModItem
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

            item.createTile = ModContent.TileType<OrderBannerTile>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Order of the Shadowed Banner");
            Tooltip.SetDefault("It spreads the Order's influence");
        }
    }
}
