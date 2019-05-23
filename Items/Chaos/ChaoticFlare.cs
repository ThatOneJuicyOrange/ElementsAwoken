using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Chaos
{
    public class ChaoticFlare : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 11;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaotic Flare");
            Tooltip.SetDefault("Even holding this sends pain through your body");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(9, 6));
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}
