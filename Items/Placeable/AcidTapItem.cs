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
    public class AcidTapItem : ModItem
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

            item.createTile = ModContent.TileType<AcidTap>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid Tap");
            Tooltip.SetDefault("Place on an acid geyser to start collecting the acid\nMust be placed on the top left corner of a geyser");
        }
        public override bool CanUseItem(Player player)
        {
            if (PlayerUtils.InTileRange(player, item))
            {
                Tile t = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
                if (t.type == ModContent.TileType<AcidGeyser>() && t.frameX == 0 && t.frameY == 0)
                {
                    WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, false, false, true);
                    return base.CanUseItem(player);
                }
            }
            return false;
        }
        /*public override bool UseItem(Player player)
        {
            if (PlayerUtils.InTileRange(player, item))
            {
                Tile t = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
                if (t.type == ModContent.TileType<AcidGeyser>())
                {
                    int frameX = t.frameX;
                    int frameY = t.frameY;
                    WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, false, false, true);
                    WorldGen.PlaceTile(Player.tileTargetX - frameX / 18, Player.tileTargetY - frameY / 18, ModContent.TileType<AcidTap>(), true);
                }
            }
            return true;
        }*/
    }
}
