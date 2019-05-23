using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Puff
{
    [AutoloadEquip(EquipType.Body)]
    public class ComfyShirt : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 0, 2, 0);
            item.rare = 1;
            item.defense = 3;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Comfy Shirt");
            Tooltip.SetDefault("Feels really nice");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Puffball", 10);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
