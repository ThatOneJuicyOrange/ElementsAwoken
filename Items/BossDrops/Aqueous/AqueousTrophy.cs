using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Aqueous
{
    public class AqueousTrophy : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 50000;
            item.rare = 1;
            item.createTile = mod.TileType("AqueousTrophy");
            item.placeStyle = 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aqueous Trophy");
            Tooltip.SetDefault("");
        }
    }
}
