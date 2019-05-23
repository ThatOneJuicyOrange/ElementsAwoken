using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Puff
{
    public class SilkenSword : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 10;
            item.magic = true;
            item.width = 58;
            item.height = 58;
            item.useTime = 20;
            item.useTurn = true;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 1;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Silken Sword");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 25);
            recipe.AddIngredient(null, "Puffball", 10);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
