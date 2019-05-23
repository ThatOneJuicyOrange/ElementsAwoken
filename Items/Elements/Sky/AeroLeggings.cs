using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Sky
{
    [AutoloadEquip(EquipType.Legs)]
    public class AeroLeggings : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 25, 0, 0);
            item.rare = 6;
            item.defense = 12;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aero Leggings");
            Tooltip.SetDefault("10% increased movement speed, swing speed and damage");
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.1f;
            player.meleeSpeed *= 1.1f;
            player.meleeDamage *= 1.1f;
            player.magicDamage *= 1.1f;
            player.rangedDamage *= 1.1f;
            player.minionDamage *= 1.1f;
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
