using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ElementsAwoken.Items.BossDrops.VoidLeviathan
{
    public class VoidAshes : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 0, 50, 0);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Ashes");
            Tooltip.SetDefault("The grounded essence of death");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoidLeviathanHeart", 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 12);
            recipe.AddRecipe();
        }
    }
}
