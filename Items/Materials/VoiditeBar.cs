using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Materials
{
    public class VoiditeBar : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 2, 0, 0);

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;

            item.useAnimation = 15;
            item.useTime = 15;
            item.useStyle = 1;
            item.createTile = ModContent.TileType<Tiles.Objects.VoiditeBarPlaced>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Voidite Bar");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoiditeOre", 4);
            recipe.AddIngredient(null, "VoidAshes", 1);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
