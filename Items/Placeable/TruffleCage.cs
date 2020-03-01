using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable
{
    public class TruffleCage : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 11;

            item.maxStack = 999;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;

            item.createTile = mod.TileType("TruffleCageTile");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Truffle Worm Cage");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TruffleWorm);
            recipe.AddIngredient(ItemID.Terrarium);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }   
    }
}
