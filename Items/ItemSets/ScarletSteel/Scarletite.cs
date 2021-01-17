using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.ScarletSteel
{
    public class Scarletite : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            
            item.value = Item.sellPrice(0, 0, 25, 0);
            item.rare = 3;
            item.maxStack = 999;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.createTile = ModContent.TileType<Tiles.VolcanicPlateau.ScarletiteTile>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scarletite");
            Tooltip.SetDefault("");
        }
    }
}
