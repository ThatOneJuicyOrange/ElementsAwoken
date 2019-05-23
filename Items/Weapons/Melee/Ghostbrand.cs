using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class Ghostbrand : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 75;
            item.melee = true;
            item.width = 58;
            item.height = 58;
            item.useTime = 17;
            item.useTurn = true;
            item.useAnimation = 17;
            item.useStyle = 1;
            item.knockBack = 7;
            item.value = Item.buyPrice(0, 15, 0, 0);
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ghostbrand");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Ectoplasm, 20);
            recipe.AddIngredient(ItemID.Keybrand, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
