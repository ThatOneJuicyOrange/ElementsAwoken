using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable
{
    public class ElementalForge : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 11;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 2;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("ElementalForge");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elemental Forge");
            Tooltip.SetDefault("Used to craft the elemental items");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Furnace, 1);
            recipe.AddRecipeGroup("SandGroup", 4);
            recipe.AddIngredient(ItemID.Torch, 4);
            recipe.AddIngredient(ItemID.Cloud, 4);
            recipe.AddRecipeGroup("IceGroup", 4);
            recipe.AddIngredient(ItemID.WaterBucket, 4);
            recipe.AddIngredient(ItemID.EbonstoneBlock, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Furnace, 1);
            recipe.AddRecipeGroup("SandGroup", 4);
            recipe.AddIngredient(ItemID.Torch, 4);
            recipe.AddIngredient(ItemID.Cloud, 4);
            recipe.AddRecipeGroup("IceGroup", 4);
            recipe.AddIngredient(ItemID.WaterBucket, 4);
            recipe.AddIngredient(ItemID.CrimstoneBlock, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}