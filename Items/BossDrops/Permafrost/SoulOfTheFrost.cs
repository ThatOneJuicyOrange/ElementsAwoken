using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Permafrost
{
    public class SoulOfTheFrost : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.buyPrice(0, 46, 0, 0);
            item.rare = 5;
            item.accessory = true;
            item.expert = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of the Frost");
            Tooltip.SetDefault("The icy power source of Permafrost\n10% increased damage\nImmunity to chilled, frostburn and frozen\nArmor penetration increased by 10");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeDamage *= 1.10f;
            player.thrownDamage *= 1.10f;
            player.rangedDamage *= 1.10f;
            player.magicDamage *= 1.10f;
            player.minionDamage *= 1.10f;
            player.armorPenetration += 10;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[BuffID.Chilled] = true;
        }
    }
}
