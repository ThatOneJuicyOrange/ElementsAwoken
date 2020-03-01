using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable.SpawnrateBanners
{
    public class CalamityBannerItem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 11;
            item.maxStack = 999;

            item.value = Item.sellPrice(0, 0, 25, 0);
            item.rare = 10;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.createTile = mod.TileType("CalamityBanner");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Calamity Banner");
            Tooltip.SetDefault("Increases spawn rates by 7.5x");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(null, "CalamityPotion", 10);
            recipe.AddIngredient(null, "DeathwishFlame", 30);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
