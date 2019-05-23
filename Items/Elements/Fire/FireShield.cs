using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Fire
{
    [AutoloadEquip(EquipType.Shield)]
    public class FireShield : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.rare = 4;
            item.value = Item.buyPrice(0, 7, 0, 0);
            item.accessory = true;
            item.defense = 4;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inferno Bulwark");
            Tooltip.SetDefault("Grants immunity to knockback, fire blocks, and On Fire");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noKnockback = true;
            player.buffImmune[24] = true;
            player.fireWalk = true;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddIngredient(ItemID.ObsidianShield, 1);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
