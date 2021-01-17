using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Materials
{
    public class Pyroplasm : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 10;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pyroplasm");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(9, 8));
        }
    }
}
