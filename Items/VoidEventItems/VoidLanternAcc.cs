using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.VoidEventItems
{
    public class VoidLanternAcc : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.buyPrice(0, 20, 0, 0);
            item.rare = 10;    
            item.accessory = true;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Lantern");
            Tooltip.SetDefault("Increases the brightness during the Dawn of the Void");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
            modPlayer.voidLantern = true;
            if (!MyWorld.voidInvasionUp)
            {
                Lighting.AddLight(player.Center, 2f, 0.5f, 0.0f);
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoidEssence", 25);
            recipe.AddIngredient(null, "VoidStone", 50);
            recipe.AddIngredient(null, "Pyroplasm", 50);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
