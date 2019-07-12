using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable
{
    public class ChaoticCrucible : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 11;
            item.maxStack = 999;

            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.createTile = mod.TileType("ChaoticCrucible");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaotic Crucible");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ElementalForge", 1);
            recipe.AddIngredient(null, "DiscordantOre", 30);
            recipe.AddIngredient(null, "ChaoticFlare", 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
