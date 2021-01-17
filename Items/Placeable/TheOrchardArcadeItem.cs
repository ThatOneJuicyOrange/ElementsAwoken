using ElementsAwoken.Tiles;
using ElementsAwoken.Tiles.Objects;
using ElementsAwoken.Tiles.VolcanicPlateau.Objects;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable
{
    public class TheOrchardArcadeItem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 999;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;

            item.createTile = ModContent.TileType<TheOrchardArcade>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arcade Machine");
            Tooltip.SetDefault("Runs a game called 'The Orchard'");
        }
    }
}
