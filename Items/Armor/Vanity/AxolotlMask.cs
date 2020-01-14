using System.Collections.Generic;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Armor.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class AxolotlMask : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.rare = 2;
            item.vanity = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Axolotl Mask");
        }

        public override bool DrawHead()
        {
            return true;
        }
    }
}
