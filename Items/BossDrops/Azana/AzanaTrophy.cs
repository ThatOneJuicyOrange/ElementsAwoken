using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Azana
{
    public class AzanaTrophy : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;

            item.maxStack = 99;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;

            item.consumable = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 1;

            item.createTile = mod.TileType("AzanaTrophy");
            item.placeStyle = 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Azana Trophy");
        }

    }
}
