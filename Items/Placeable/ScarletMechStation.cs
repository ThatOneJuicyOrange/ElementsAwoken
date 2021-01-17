using ElementsAwoken.Tiles.Crafting;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable
{
    public class ScarletMechStation : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 999;
            item.rare = 3;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            
            item.createTile = ModContent.TileType<ScarletMechStationTile>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scarlet Mech Station");
        }
    }
}
