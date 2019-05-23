using System.Collections.Generic;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Armor.Vanity.TOJO
{
    [AutoloadEquip(EquipType.Head)]
    public class OrangesMask : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 36;
            item.rare = 9;
            item.vanity = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ThatOneJuicyOrange's Mask");
            Tooltip.SetDefault("So juicy");
        }

        public override bool DrawHead()
        {
            return false;
        }
    }
}
