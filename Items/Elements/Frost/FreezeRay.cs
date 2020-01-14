using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Frost
{
    public class FreezeRay : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 20;

            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.damage = 60;
            item.knockBack = 5f;
            item.mana = 4;

            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 5;
            item.UseSound = SoundID.Item12;

            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 7;

            item.shootSpeed = 15f;
            item.shoot = mod.ProjectileType("FreezeBeam");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Freeze Ray");
            Tooltip.SetDefault("Shoots ice beams that completely freeze enemies");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FrostEssence", 7);
            recipe.AddRecipeGroup("ElementsAwoken:IceGroup", 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddIngredient(ItemID.HeatRay);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
