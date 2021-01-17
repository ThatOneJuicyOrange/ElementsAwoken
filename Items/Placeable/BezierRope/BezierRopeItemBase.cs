using ElementsAwoken.Tiles.BezierRope;
using ElementsAwoken.Tiles.VolcanicPlateau;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable.BezierRope
{
    public abstract class BezierRopeItemBase : ModItem
    {
        private int tileID;
        private Tile target;
        protected BezierRopeItemBase(int tileType)
        {
            tileID = tileType;
        }
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
        }

        public override bool UseItem(Player player)
        {
            if (target == null)
            {
                WorldGen.PlaceTile((int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16), tileID);

                Tile t = Framing.GetTileSafely((int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16));
                if (t.type == tileID) target = t;
            }
            else
            {
                // idea of using frame to store pos from starlight river
                target.frameX = (short)(Main.MouseWorld.X / 16);
                target.frameY = (short)(Main.MouseWorld.Y / 16);
                target = null;
            }
            return true;
        }
    }
}
