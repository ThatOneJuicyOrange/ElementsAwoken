using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Frost
{
    public class SnowflakeStaff : ModItem
    {

        public override void SetDefaults()
        {
            item.damage = 53;
            item.magic = true;
            item.channel = true;
            item.mana = 13;
            item.width = 66;
            item.height = 66;
            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = 1;      
            item.noMelee = true;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 7;
            item.UseSound = SoundID.Item46;
            item.shoot = mod.ProjectileType("Snowflake");
            item.shootSpeed = 3f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snowflake Staff");
            Tooltip.SetDefault("Shoots a magical snowflake that follows the cursor");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FrostEssence", 7);
            recipe.AddRecipeGroup("ElementsAwoken:IceGroup", 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddIngredient(ItemID.MagicMissile);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
