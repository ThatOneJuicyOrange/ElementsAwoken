using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Accessories
{
    public class SharktoothShackle : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 2;    
            item.accessory = true;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sharktooth Shackle");
            Tooltip.SetDefault("Increases armor penetration by 5\n2 defense");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.armorPenetration += 7;
            player.statDefense += 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SharkToothNecklace, 1);
            recipe.AddIngredient(ItemID.Shackle, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
