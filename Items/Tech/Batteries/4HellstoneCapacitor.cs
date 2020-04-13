using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Batteries
{
    public class HellstoneCapacitor : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;

            item.maxStack = 1;

            item.value = Item.sellPrice(0, 0, 75, 0);

            item.rare = 4;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellstone Capacitor");
            Tooltip.SetDefault("Increases the players maximum energy by 150");
        }
        public override void UpdateInventory(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            modPlayer.batteryEnergy += 150;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DemonicPowerBank", 2);
            recipe.AddIngredient(ItemID.HellstoneBar, 12);
            recipe.AddIngredient(ItemID.Obsidian, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
