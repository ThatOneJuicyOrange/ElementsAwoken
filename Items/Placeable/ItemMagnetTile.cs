using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable
{
    public class ItemMagnetTile : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 11;
            item.maxStack = 999;

            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 5;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.createTile = mod.TileType("ItemMagnet");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Item Magnet");
            Tooltip.SetDefault("Sucks nearby items towards it\nIf there is a chest next to it, items will be placed in the chest");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 16);
            recipe.AddIngredient(null, "GoldWire", 6);
            recipe.AddIngredient(null, "CopperWire", 12);
            recipe.AddIngredient(null, "Transistor", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
