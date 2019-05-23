using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Azana
{
    public class RingOfChaos : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;

            item.rare = 11;
            item.value = Item.sellPrice(0, 35, 0, 0);

            item.accessory = true;
            item.expert = true;

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ring of Chaos");
            Tooltip.SetDefault("Pressing the ability key will create a shield around the player that absorbs damage, buffing the player with the more damage they take\n10% increased crit chance\n5% increased damage");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
            modPlayer.chaosRing = true;

            player.magicCrit += 10;
            player.meleeCrit += 10;
            player.rangedCrit += 10;
            player.thrownCrit += 10;

            player.magicDamage *= 1.05f;
            player.meleeDamage *= 1.05f;
            player.rangedDamage *= 1.05f;
            player.thrownDamage *= 1.05f;
            player.minionDamage *= 1.05f;
        }
    }
}
