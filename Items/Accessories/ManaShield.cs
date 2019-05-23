using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]

    public class ManaShield : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.buyPrice(0, 2, 0, 0);
            item.rare = 8;
            item.accessory = true;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shield of Magic");
            Tooltip.SetDefault("Shield of a true mage\nKnockback immunity\nRestores mana when damaged\nIncreases pickup range for stars\nRestores mana automatically");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noKnockback = true;
            player.magicCuffs = true;
            player.manaMagnet = true;
            player.manaFlower = true;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CobaltShield, 1);
            recipe.AddIngredient(ItemID.CelestialCuffs, 1);
            recipe.AddIngredient(ItemID.ManaFlower, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
