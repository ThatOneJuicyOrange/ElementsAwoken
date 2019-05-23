using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Pets
{
    public class AzanaChibi : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 0, 0, 5);
            item.rare = 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Tomato");
            Tooltip.SetDefault("now you have a slightly angry tomato, what of it?\nWill be replaced with a different item later");
        }
    }
}
