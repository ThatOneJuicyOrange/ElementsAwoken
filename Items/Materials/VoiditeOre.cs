using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Materials
{
    public class VoiditeOre : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 2, 0, 0);

            item.consumable = true;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.createTile = mod.TileType("Voidite");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Voidite Ore");
        }
    }
}
