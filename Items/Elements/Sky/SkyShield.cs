using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Sky
{
    [AutoloadEquip(EquipType.Shield)]
    public class SkyShield : ModItem
    { 
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.buyPrice(0, 25, 0, 0);
            item.rare = 6;
            item.accessory = true;
            item.rare = 6;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ward of The Clouds");
            Tooltip.SetDefault("The clouds accept you\nGrants immunity to knockback\nAllows double jump\nIncreases wing time\nWhen in the sky:\n10 defense");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noKnockback = true;
            player.doubleJumpCloud = true;
            player.wingTimeMax += 50;
            if (Main.player[Main.myPlayer].ZoneSkyHeight)
            {
                player.statDefense += 10;
            }
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SkyEssence", 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddIngredient(ItemID.CloudinaBottle, 1);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
