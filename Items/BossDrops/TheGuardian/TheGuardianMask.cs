using System.Collections.Generic;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.TheGuardian
{
    [AutoloadEquip(EquipType.Head)]
    public class TheGuardianMask : ModItem
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
            DisplayName.SetDefault("The Guardian Mask");
        }

        public override bool DrawHead()
        {
            return false;
        }
    }
}
