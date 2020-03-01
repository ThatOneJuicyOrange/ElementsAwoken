using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tools
{
    public class DemonicMagnet : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;

            item.rare = 5;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.tileBoost += 2;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demonic Magnet");
            Tooltip.SetDefault("Pulls in nearby items");
        }

        public override void HoldItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                for (int i = 0; i < Main.maxItems; i++)
                {
                    Item succ = Main.item[i];
                    if (Vector2.Distance(succ.Center, player.Center) < 800 && item.active && item.type != 0)
                    {
                        Vector2 toTarget = new Vector2(player.Center.X - succ.Center.X, player.Center.Y - succ.Center.Y);
                        toTarget.Normalize();
                        succ.velocity += toTarget *= 0.3f;

                        int dust = Dust.NewDust(succ.Center, 8, 8, 6);
                        Main.dust[dust].velocity *= 0.1f;
                        Main.dust[dust].scale *= 1.5f;
                        Main.dust[dust].noGravity = true;
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DemonicFleshClump", 8);
            recipe.AddRecipeGroup("IronBar", 18);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
