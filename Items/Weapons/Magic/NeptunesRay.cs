using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Magic
{
    public class NeptunesRay : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 24;
            item.magic = true;
            item.width = 50;
            item.height = 50;
            item.useTime = 32;
            item.useAnimation = 32;
            Item.staff[item.type] = true;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 1;
            item.mana = 5;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("NeptuneRay");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ocean's Ray");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Seashell, 6);
            recipe.AddIngredient(ItemID.Coral, 8);
            recipe.AddIngredient(ItemID.Starfish, 3);
            recipe.AddRecipeGroup("ElementsAwoken:EvilBar", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
