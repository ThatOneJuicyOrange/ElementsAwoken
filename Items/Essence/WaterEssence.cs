using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Essence
{
    public class WaterEssence : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = 50;
            item.rare = 8;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Essence");
            Tooltip.SetDefault("Essence from the creatures of the deep");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(9, 6));
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}
