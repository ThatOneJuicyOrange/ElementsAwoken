using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Volcanox
{
    public class VolcanicStone : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.buyPrice(0, 2, 50, 0);
            item.rare = 11;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Volcanic Stone");
            Tooltip.SetDefault("The rocks seem to constantly shatter into flames");
        }
    }
}
