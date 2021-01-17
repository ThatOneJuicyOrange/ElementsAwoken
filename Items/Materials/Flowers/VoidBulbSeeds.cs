using ElementsAwoken.Tiles;
using ElementsAwoken.Tiles.VolcanicPlateau.Flora;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Materials.Flowers
{
    public class VoidBulbSeeds : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.createTile = ModContent.TileType<VoidBulbTile>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Bulb Seeds");
        }
    }
}
