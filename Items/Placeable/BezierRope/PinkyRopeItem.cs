using ElementsAwoken.Tiles.BezierRope;
using ElementsAwoken.Tiles.VolcanicPlateau;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable.BezierRope
{
    public class PinkyRopeItem : BezierRopeItemBase
    {
        public override string Texture { get { return "ElementsAwoken/Items/TODO"; } }
        public PinkyRopeItem() : base(ModContent.TileType<PinkyRope>()) { }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pinky Rope");
        }
    }
}
