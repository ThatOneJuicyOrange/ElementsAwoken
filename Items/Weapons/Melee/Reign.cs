using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class Reign : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 315;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 28;
            item.useTurn = true;
            item.useAnimation = 28;
            item.useStyle = 1;
            item.knockBack = 10;
            item.value = Item.buyPrice(1, 20, 0, 0);
            item.rare = 10;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("ReignWave");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reign");
            Tooltip.SetDefault("Slow but strong");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(ItemID.EnchantedSword, 1);
            recipe.AddIngredient(ItemID.StarWrath, 1);
            recipe.AddIngredient(ItemID.Meowmere, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
