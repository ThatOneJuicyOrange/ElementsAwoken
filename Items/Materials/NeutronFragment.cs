using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Materials
{
    public class NeutronFragment : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 9;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Neutron Fragment");
            Tooltip.SetDefault("The power of collapsed stars rests within this fragment. Use it wisely");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentNebula, 2);
            recipe.AddIngredient(ItemID.FragmentSolar, 2);
            recipe.AddIngredient(ItemID.FragmentStardust, 2);
            recipe.AddIngredient(ItemID.FragmentVortex, 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
