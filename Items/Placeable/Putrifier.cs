using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable
{
    public class Putrifier : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 11;

            item.maxStack = 999;

            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 8;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;

            item.createTile = mod.TileType("Putrifier");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Putrifier");
            Tooltip.SetDefault("Throw iron ore and sun fragments in\nDont throw too many in at once or it will fully digest it");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ItemID.LihzahrdBrick, 10);
            recipe.AddIngredient(null, "SunFragment", 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }   
    }
}
