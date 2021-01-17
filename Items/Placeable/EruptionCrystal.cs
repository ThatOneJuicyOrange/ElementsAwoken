using ElementsAwoken.Tiles.VolcanicPlateau;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable
{
    public class EruptionCrystal : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;

            item.value = Item.sellPrice(0, 0, 20, 0);
            item.rare = 3;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.createTile = ModContent.TileType<EruptionCrystalTile>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eruption Crystal");
            Tooltip.SetDefault("It has a bright glow");
        }
    }
}
