using ElementsAwoken.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable.Tiles
{
    public class Shadite : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;
            item.questItem = true;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.createTile = ModContent.TileType<ShaditeTile>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadite");
        }
    }
}
