using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.Manashard
{
    public class ManashardLongsword : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 70;
            item.height = 70;
            
            item.damage = 51;

            item.melee = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.useTime = 21;
            item.useAnimation = 21;

            item.useStyle = 1;
            item.knockBack = 5;

            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 5;

            item.UseSound = SoundID.Item1;
            item.shoot = ProjectileType<Projectiles.Manaspike>();
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
            recipe.AddIngredient(ItemType<Manashard>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
