using System.Collections.Generic;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.ToySlime
{
    [AutoloadEquip(EquipType.Head)]
    public class ToySlimeMask : ModItem
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
            DisplayName.SetDefault("Toy Slime Mask");
        }


        public override bool DrawHead()
        {
            return false;
        }
    }
}
