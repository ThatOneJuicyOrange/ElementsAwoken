using ElementsAwoken.Tiles.VolcanicPlateau.Objects;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable
{
    public class IgneousTorchItem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;

            item.maxStack = 99;
            item.holdStyle = 1;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;
            item.flame = true;
            item.noWet = true;

            item.value = Item.sellPrice(0,0,0,50);

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;

            item.createTile = ModContent.TileType<IgneousTorch>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Igneous Torch");
        }
    }
}
