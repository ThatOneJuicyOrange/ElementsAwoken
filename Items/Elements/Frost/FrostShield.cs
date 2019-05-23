using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Frost
{
    [AutoloadEquip(EquipType.Shield)]
    public class FrostShield : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 7;
            item.accessory = true;
            item.defense = 4;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Buckler");
            Tooltip.SetDefault("Grants immunity to knockback\n30 increased maximum life\nIf the player is in the snow:\n5 defense\n30 increased maximum life\n10% increased damage");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noKnockback = true;
            player.statLifeMax2 += 30;
            player.statDefense += 5;
            player.statLifeMax2 += 30;
            if (Main.player[Main.myPlayer].ZoneSnow)
            {
                player.statDefense += 5;
                player.statLifeMax2 += 30;
                player.meleeDamage += 0.1f;
                player.thrownDamage += 0.1f;
                player.rangedDamage += 0.1f;
                player.magicDamage += 0.1f;
                player.minionDamage += 0.1f;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FrostEssence", 7);
            recipe.AddRecipeGroup("IceGroup", 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddIngredient(ItemID.LifeCrystal, 3);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
