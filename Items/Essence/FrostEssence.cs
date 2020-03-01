using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Essence
{
    public class FrostEssence : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 7;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Essence");
            Tooltip.SetDefault("Essence from the frosty wasteland");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 11));
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}
