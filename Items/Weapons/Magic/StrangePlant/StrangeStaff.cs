using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Magic.StrangePlant
{
    public class StrangeStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 32;
            item.magic = true;
            item.width = 50;
            item.height = 50;
            item.useTime = 28;
            item.useAnimation = 28;
            Item.staff[item.type] = true;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 1;
            item.mana = 5;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("StrangePlantBall6");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strange Staff");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "StrangeWand1", 1);
            recipe.AddIngredient(null, "StrangeWand2", 1);
            recipe.AddIngredient(null, "StrangeWand3", 1);
            recipe.AddIngredient(null, "StrangeWand4", 1);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddRecipeGroup("EvilBar", 10);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
