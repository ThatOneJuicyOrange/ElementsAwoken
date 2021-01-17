using ElementsAwoken.Tiles.VolcanicPlateau;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable
{
    public class AshGrassSeeds : ModItem
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
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ash Grass Seeds");
        }
        public override bool UseItem(Player player)
        {
            Tile tile = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
            if (tile.type == ModContent.TileType<MalignantFlesh>())
            {
                Point digPos = Main.MouseWorld.ToTileCoordinates();
                if (Vector2.Distance(digPos.ToVector2(), player.Center.ToTileCoordinates().ToVector2()) < 10)
                {
                    tile.type = (ushort)ModContent.TileType<AshGrass>();
                    item.stack--;
                    Main.PlaySound(SoundID.Dig, player.position);
                }
            }
            return base.UseItem(player);
        }
    }
}
