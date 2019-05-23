using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Void
{
    public class VoidBlaster : ModItem
    {

        public override void SetDefaults()
        {
            item.damage = 123;
            item.ranged = true;
            item.width = 92;
            item.height = 28;
            item.useTime = 6;
            item.useAnimation = 6;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1.75f;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item91;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("VoidBlast");
            item.shootSpeed = 24f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Blaster");
            Tooltip.SetDefault("Unleash a storm of homing void blasts");
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
    }
}
