using System.Collections.Generic;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Armor.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class GenihWatMask : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.rare = 9;
            item.vanity = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Genih Wat's Mask");
            Tooltip.SetDefault("Doesn't actually look like the Genih Wat, but instead the Wat's preferred goddess\nGenih claims this goddess resides in something called a 'hyperdimension'");
        }

        public override bool DrawHead()
        {
            return false;
        }
    }
}
