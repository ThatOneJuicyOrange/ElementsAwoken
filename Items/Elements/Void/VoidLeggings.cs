using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Void
{
    [AutoloadEquip(EquipType.Legs)]
    public class VoidGreaves : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 11;
            item.defense = 22;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Greaves");
            Tooltip.SetDefault("10% increased melee speed\nGreater speed when low health");
        }
        public override void UpdateEquip(Player player)
        {
            player.meleeSpeed *= 1.1f;
            float speedBoost = 0f;
            speedBoost = (player.statLifeMax2 - player.statLifeMax2) / 5;
            player.moveSpeed += speedBoost;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoidEssence", 14);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
