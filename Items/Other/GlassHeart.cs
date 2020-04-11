using ElementsAwoken.NPCs.Bosses.Azana;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Other
{
    public class GlassHeart : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glass Heart");
            Tooltip.SetDefault("Causes you to die if you take damage but bosses drop 2x loot\nOnly needs to be in the inventory");
        }
        public override void UpdateInventory(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.glassHeart = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Glass, 16);
            recipe.AddIngredient(ItemID.LifeCrystal, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
