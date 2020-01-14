using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Sky
{
    [AutoloadEquip(EquipType.Legs)]
    public class EmpyreanLeggings : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 6;
            item.defense = 12;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Empyrean Leggings");
            Tooltip.SetDefault("20% increased movement speed\n10% increased swing speed");
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.2f;
            player.meleeSpeed *= 1.1f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SkyEssence", 7);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 18);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
