using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.DataStructures;

namespace ElementsAwoken.Items.Elements.Void
{
    public class VoidNeck : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 11;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Idol of Darkness");
            Tooltip.SetDefault("Life regen increased by 4\nDamage increased by 25%");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += 4;
            player.meleeDamage += 0.25f;
            player.thrownDamage += 0.25f;
            player.rangedDamage += 0.25f;
            player.magicDamage += 0.25f;
            player.minionDamage += 0.25f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoidEssence", 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool DrawBody()
        {
            return false;
        }
    }
}
