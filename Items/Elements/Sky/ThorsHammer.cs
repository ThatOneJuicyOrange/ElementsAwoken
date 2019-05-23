using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Sky
{
    public class ThorsHammer : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 30;
            item.damage = 50;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useAnimation = 18;
            item.useStyle = 1;
            item.useTime = 18;
            item.knockBack = 10f;
            item.UseSound = SoundID.Item1;
            item.thrown = true;
            item.height = 68;
            item.value = Item.buyPrice(0, 25, 0, 0);
            item.rare = 6;
            item.shoot = mod.ProjectileType("ThorsHammerP");
            item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storm Hammer");
            Tooltip.SetDefault("Pure energy in the palm of your hand");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SkyEssence", 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
