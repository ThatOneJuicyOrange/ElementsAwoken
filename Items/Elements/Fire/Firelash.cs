using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Fire
{
    public class Firelash : ModItem
    {

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Code2);
            item.useStyle = 5;
            item.damage = 30;
            item.width = 16;
            item.height = 16;
            item.rare = 4;
            item.value = Item.buyPrice(0, 7, 0, 0);
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.shoot = 541;
            item.useAnimation = 25;
            item.useTime = 25;
            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("FirelashP");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellstorm");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
