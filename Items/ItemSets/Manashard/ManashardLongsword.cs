using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Manashard
{
    public class ManashardLongsword : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 51;
            item.melee = true;
            item.width = 70;
            item.height = 70;
            item.useTime = 21;
            item.useTurn = true;
            item.useAnimation = 12;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 5;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Manaspike");
            item.shootSpeed = 12f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Manashard Longsword");
            Tooltip.SetDefault("");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Manashard", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
