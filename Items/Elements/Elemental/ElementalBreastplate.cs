using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Elemental
{
    [AutoloadEquip(EquipType.Body)]
    public class ElementalBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 20, 0, 0);

            item.defense = 40;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elemental Breastplate");
            Tooltip.SetDefault("20% increased damage\nIncreased max life and mana by 100\n10% damage endurance");
        }
        public override void UpdateEquip(Player player)
        {
            player.thrownDamage *= 1.2f;
            player.meleeDamage *= 1.2f;
            player.magicDamage *= 1.2f;
            player.rangedDamage *= 1.2f;
            player.minionDamage *= 1.2f;
            player.statLifeMax2 += 100;
            player.statManaMax2 += 150;
            player.endurance += 0.1f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ElementalEssence", 5);
            recipe.AddIngredient(null, "VoidAshes", 12);
            recipe.AddIngredient(ItemID.LunarBar, 16);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
