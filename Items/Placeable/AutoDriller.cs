using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable
{
    public class AutoDriller : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 40;

            item.maxStack = 999;

            item.rare = 4;       
            item.value = Item.sellPrice(0, 1, 0, 0);

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;

            item.createTile = mod.TileType("AutoDriller");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Auto Driller");
            Tooltip.SetDefault("Digs a 3 block wide tunnel underneath it\nConsumes 3 energy from the nearest player for each dig");
        }
        public override bool CanUseItem(Player player)
        {
            PlayerUtils modPlayer = player.GetModPlayer<PlayerUtils>(mod);
            modPlayer.placingAutoDriller = 4;
            return base.CanUseItem(player);
        }
        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Glass, 10);
            recipe.AddRecipeGroup("GoldBar", 15);
            recipe.AddIngredient(null, "Stardust", 16);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
}
