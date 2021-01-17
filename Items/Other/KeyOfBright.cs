using ElementsAwoken.NPCs.Bosses.Azana;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Other
{
    public class KeyOfBright : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;

            item.maxStack = 999;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Key of Bright");
            Tooltip.SetDefault("'Charged with the essence of many souls'");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Materials.SoulOfBright>(), 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
