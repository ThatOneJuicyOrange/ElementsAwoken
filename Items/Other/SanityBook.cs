using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Other
{
    public class SanityBook : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;

            item.value = Item.sellPrice(0, 0, 25, 0);
            item.rare = 3;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Book of Shadows");
            Tooltip.SetDefault("Your mind throbs\nShows all the players sanity regeneration\nRight click in the inventory to open and close");
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>(mod);

            if (MyWorld.awakenedMode)
            {
                if (!modPlayer.openSanityBook)
                {
                    modPlayer.openSanityBook = true;
                }
                else
                {
                    modPlayer.openSanityBook = false;
                }
            }
            else
            {
                modPlayer.openSanityBook = false;
            }
            item.stack++;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddIngredient(ItemID.DemoniteBar, 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void OnCraft(Recipe recipe)
        {
            Player player = Main.player[Main.myPlayer];
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>(mod);
            if (!MyWorld.awakenedMode)
            { 
                Main.NewText("Enable Awakened Mode for this book to have any functionality");
            }
        }
    }
}
