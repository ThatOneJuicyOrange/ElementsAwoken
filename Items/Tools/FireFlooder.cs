using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tools
{
    public class FireFlooder : ModItem
    {

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.useTurn = true;
            item.useAnimation = 12;
            item.useTime = 5;
            item.width = 20;
            item.height = 20;
            item.autoReuse = true;
            item.rare = 11;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.tileBoost += 2;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Flooder");
            Tooltip.SetDefault("Contains an endless amount of lava");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VolcanicStone", 8);
            recipe.AddIngredient(ItemID.BottomlessBucket, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(9) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 6);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}
