using ElementsAwoken.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable
{
    public class ObsidiousArenaManagerItem : ModItem
    {
        public override string Texture { get { return "ElementsAwoken/Items/TODO"; } }
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

            item.createTile = ModContent.TileType<ObsidiousArenaManager>();
            item.GetGlobalItem<EATooltip>().unobtainable = true;
            item.color = Color.Orange;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidious Arena");
        }
    }
}
