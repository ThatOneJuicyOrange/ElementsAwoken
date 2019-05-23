using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class Infamy : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 54;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 12;
            item.useTurn = true;
            item.useAnimation = 12;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 5;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infamy");
        }

        public override void AddRecipes()
        {   
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cutlass, 1);
            recipe.AddIngredient(ItemID.SoulofMight, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
