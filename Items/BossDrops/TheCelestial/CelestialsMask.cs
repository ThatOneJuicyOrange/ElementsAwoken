using System.Collections.Generic;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.TheCelestial
{
    [AutoloadEquip(EquipType.Head)]
    public class CelestialsMask : ModItem
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
            DisplayName.SetDefault("The Celestials Mask");
        }


        public override bool DrawHead()
        {
            return false;
        }
    }
}
