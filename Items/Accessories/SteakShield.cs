using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]

    public class SteakShield : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 1;    
            item.accessory = true;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meat Shield");
            Tooltip.SetDefault("What?\n4 defense\nEnemies are more likely to target the player");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 4;
            player.aggro += 250;
        }
    }
}
