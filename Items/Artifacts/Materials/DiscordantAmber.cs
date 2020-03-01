using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Artifacts.Materials
{
    public class DiscordantAmber : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 3;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unstable Amber");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(9, 4));
        }
    }
}
