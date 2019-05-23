using System.Collections.Generic;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Wasteland
{
    [AutoloadEquip(EquipType.Head)]
    public class WastelandMask : ModItem
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
            DisplayName.SetDefault("Wasteland Mask");
        }
    }
}
