using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ElementsAwoken.Items.Elements.Desert
{
    public class DesertNeck : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 3;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Badge of the Desert");
            Tooltip.SetDefault("Harness the power of the desert winds\n25% increased movement and melee speed");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed *= 1.25f;
            player.meleeSpeed *= 1.25f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertEssence", 4);
            recipe.AddRecipeGroup("ElementsAwoken:SandGroup", 25);
            recipe.AddRecipeGroup("ElementsAwoken:SandstoneGroup", 10);
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
