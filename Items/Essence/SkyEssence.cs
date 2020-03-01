using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Essence
{
    public class SkyEssence : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 0, 2, 50);
            item.rare = 6;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sky Essence");
            Tooltip.SetDefault("Essence from the vast empty void of space");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 10));
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}
