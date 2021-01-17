using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Accessories.Environmental
{
    public class GasMask : ModItem
    {
        public override string Texture { get { return "ElementsAwoken/Items/TODO"; } }
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
            DisplayName.SetDefault("Gas Mask");
            Tooltip.SetDefault("Allows you to breathe during sulfuric storms");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.gasMask = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Goggles, 1);
            recipe.AddIngredient(ItemID.Silk, 8);
            recipe.AddIngredient(ItemType<Materials.DemonicFleshClump>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
