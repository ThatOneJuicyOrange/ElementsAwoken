using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.VoidLeviathan
{
    [AutoloadEquip(EquipType.Shield)]
    public class VoidLeviathansAegis : ModItem
    {
        public int damageTaken = 0;
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 10, 0, 0);

            item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Leviathan's Aegis");
            Tooltip.SetDefault("Life increased by 50\nWhen under half health defense is increased by 5%\nAllows the player to perform a secondary dash\nFor 15 seconds after taking 500 total damage:\nMovement speed increased by 40%\nDamage increased by 20%");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
            modPlayer.vleviAegis = true;

            player.statLifeMax2 += 50;
            if (player.statLife < player.statLifeMax2 / 2)
            {
                player.statDefense = (int)(player.statDefense * 1.05);
            }
        }
    }
}
