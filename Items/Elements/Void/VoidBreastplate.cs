using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Void
{
    [AutoloadEquip(EquipType.Body)]
    public class VoidBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 11;
            item.defense = 33;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Breastplate");
            Tooltip.SetDefault("12% increased damage\nIncreased max life and mana by 75");
        }
        public override void UpdateEquip(Player player)
        {
            player.thrownDamage *= 1.12f;
            player.meleeDamage *= 1.12f;
            player.magicDamage *= 1.12f;
            player.rangedDamage *= 1.12f;
            player.minionDamage *= 1.12f;
            player.statLifeMax2 += 75;
            player.statManaMax2 += 75;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoidEssence", 16);
            recipe.AddIngredient(ItemID.LunarBar, 18);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
