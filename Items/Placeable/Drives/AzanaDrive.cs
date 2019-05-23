using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable.Drives
{
    public class AzanaDrive : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 11;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 0;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.createTile = mod.TileType("AzanaDrive");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Azana Drive");
        }
        public override void UpdateInventory(Player player)
        {
            MyWorld.azanaDrive = true;
        }
    }
}
