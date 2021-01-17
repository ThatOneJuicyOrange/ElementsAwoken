using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ElementsAwoken.Items.Accessories
{
    public class BalletShoes : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 3;
            item.value = Item.sellPrice(0, 0, 2, 50);
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ballet Shoes");
            Tooltip.SetDefault("Jump speed increased by 40%\nWhen the player jumps they get a speed boost in the direction they are moving");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.jumpSpeedBoost += 1.4f;
            float speed = 8f;
            if(player.controlJump && player.releaseJump && player.velocity.Y == 0&& player.velocity.X != 0 && Math.Abs(player.velocity.X) < speed)
            {
                player.velocity.X = player.direction * speed;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PinkThread, 15);
            recipe.AddRecipeGroup("IronBar", 6);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
