using System.Collections.Generic;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Volcanox
{
    [AutoloadEquip(EquipType.Head)]
    public class VolcanoxMask : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 20;
            item.rare = 1;
            item.vanity = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Volcanox Mask");
        }


        public override bool DrawHead()
        {
            return false;
        }
    }
}
