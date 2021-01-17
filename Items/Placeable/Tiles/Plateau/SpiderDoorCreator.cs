using ElementsAwoken.Tiles.VolcanicPlateau;
using ElementsAwoken.Tiles.VolcanicPlateau.ObjectSpawners;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable.Tiles.Plateau
{
    public class SpiderDoorCreator : ModItem
    {
        public override string Texture { get { return "ElementsAwoken/Items/TODO"; } }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

            item.useAnimation = 15;
            item.useTime = 15;
            item.useStyle = 1;

            item.createTile = ModContent.TileType<SpiderDoorSpawner>();
            item.GetGlobalItem<EATooltip>().unobtainable = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spider Door");
            Tooltip.SetDefault("");
        }
    }
}
