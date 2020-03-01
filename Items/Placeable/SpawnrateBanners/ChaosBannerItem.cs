using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable.SpawnrateBanners
{
    public class ChaosBannerItem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 11;
            item.maxStack = 999;

            item.value = Item.sellPrice(0, 0, 15, 0);
            item.rare = 6;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.createTile = mod.TileType("ChaosBanner");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Banner");
            Tooltip.SetDefault("Increases spawn rates by 5x");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(null, "ChaosPotion", 10);
            recipe.AddIngredient(null, "DeathwishFlame", 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
