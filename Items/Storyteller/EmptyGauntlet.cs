using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Storyteller
{
    public class EmptyGauntlet : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.buyPrice(0, 20, 0, 0);
            item.rare = 8;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Empty Gauntlet");
            Tooltip.SetDefault("Odd slots are built into the gauntlet");
        }

    }
}
