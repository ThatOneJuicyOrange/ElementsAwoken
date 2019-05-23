using System.Collections.Generic;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Permafrost
{
    [AutoloadEquip(EquipType.Head)]
    public class PermafrostMask : ModItem
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
            DisplayName.SetDefault("Permafrost Mask");
        }

        public override bool DrawHead()
        {
            return false;
        }
    }
}
