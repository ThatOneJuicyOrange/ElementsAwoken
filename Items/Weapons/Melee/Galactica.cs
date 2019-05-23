using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class Galactica : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 210;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 14;
            item.useTurn = true;
            item.useAnimation = 14;
            item.useStyle = 1;
            item.knockBack = 6.5f;
            item.value = Item.buyPrice(1, 50, 0, 0);
            item.rare = 10;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("GalacticaBlade");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Galactica");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Pyroplasm", 50);
            recipe.AddIngredient(null, "NeutronFragment", 8);
            recipe.AddIngredient(ItemID.TerraBlade, 1);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
