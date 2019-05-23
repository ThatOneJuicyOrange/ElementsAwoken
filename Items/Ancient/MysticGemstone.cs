using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Ancient
{
    public class MysticGemstone : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;

            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mystic Gemstone");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(7, 53));
        }
    }
}
