using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Batteries
{
    public class ChaoticEnergyCapacitor : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;

            item.maxStack = 1;

            item.value = Item.sellPrice(0, 20, 0, 0);

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaotron Energy Capacitor");
            Tooltip.SetDefault("Increases the players maximum energy by 1750");
        }
        public override void UpdateInventory(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            modPlayer.batteryEnergy += 1750;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "LunarEnergyHarnesser", 2);
            recipe.AddIngredient(null, "DiscordantBar", 8);
            recipe.AddIngredient(null, "ChaoticFlare", 4);
            recipe.AddTile(null, "ChaoticCrucible");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
