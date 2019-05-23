﻿using System.Collections.Generic;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Regaroth
{
    [AutoloadEquip(EquipType.Head)]
    public class RegarothMask : ModItem
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
            DisplayName.SetDefault("Regaroth Mask");
        }

        public override bool DrawHead()
        {
            return false;
        }
    }
}
