using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable.SpawnrateBanners
{
    public class HavocBannerItem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 11;
            item.maxStack = 999;

            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 3;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.createTile = mod.TileType("HavocBanner");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Havoc Banner");
            Tooltip.SetDefault("Increases spawn rates by 2x");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddIngredient(null, "HavocPotion", 10);
            recipe.AddIngredient(null, "DeathwishFlame", 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
