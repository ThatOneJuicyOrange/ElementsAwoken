using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Batteries
{
    public class AABattery : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;

            item.maxStack = 1;

            item.value = Item.sellPrice(0, 0, 25, 0);

            item.rare = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("AA Battery");
            Tooltip.SetDefault("Increases the players maximum energy by 25");
        }
        public override void UpdateInventory(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            modPlayer.batteryEnergy += 25;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("ElementsAwoken:CopperBar", 8);
            recipe.AddRecipeGroup("IronBar", 5);
            recipe.AddIngredient(null, "CopperWire", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
