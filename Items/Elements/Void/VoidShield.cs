using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Void
{
    [AutoloadEquip(EquipType.Shield)]

    public class VoidShield : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 11;
            item.accessory = true;
            item.defense = 4;

        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Somber Bulwark");
      Tooltip.SetDefault("Grants immunity to knockback\nThe more defense you have the more health and damage you deal");
    }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noKnockback = true;
            if ((player.statDefense >= 40) && (player.statDefense <= 79))  
            {                            
                player.statLifeMax2 += 100;
                player.meleeDamage += 0.05f;
                player.thrownDamage += 0.05f;
                player.rangedDamage += 0.05f;
                player.magicDamage += 0.05f;
                player.minionDamage += 0.05f;
                player.meleeSpeed += 0.05f;
            }
            if ((player.statDefense >= 80) && (player.statDefense <= 119)) 
            {                                                                              
                player.statLifeMax2 += 150;
                player.meleeDamage += 0.1f;
                player.thrownDamage += 0.1f;
                player.rangedDamage += 0.1f;
                player.magicDamage += 0.1f;
                player.minionDamage += 0.1f;
                player.meleeSpeed += 0.1f;
            }
            if (player.statDefense >= 120)   
            {                                       
                player.statLifeMax2 += 200;
                player.meleeDamage += 0.15f;
                player.thrownDamage += 0.15f;
                player.rangedDamage += 0.15f;
                player.magicDamage += 0.15f;
                player.minionDamage += 0.15f;
                player.meleeSpeed += 0.15f;
            }
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoidEssence", 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ItemID.LifeCrystal, 3);
            recipe.AddIngredient(ItemID.LifeFruit, 3);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
