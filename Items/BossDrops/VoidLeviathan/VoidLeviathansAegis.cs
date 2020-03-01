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
            Tooltip.SetDefault("");
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string hotkey;
            var list = ElementsAwoken.dash2.GetAssignedKeys();
            if (list.Count > 0) hotkey = list[0];
            else hotkey = "<Dash Unbound>";

            string baseTooltip = "Life increased by 50\nWhen under half health, defense is increased by 5%\nAllows the player to perform a secondary dash using the " + hotkey + " key\nFor 15 seconds after taking 500 total damage:\n40% increased movement speed\n20% increased damage";
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name.StartsWith("Tooltip"))
                {
                    line2.text = baseTooltip;
                }
            }
        }
    
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.vleviAegis = true;

            player.statLifeMax2 += 50;
            if (player.statLife < player.statLifeMax2 / 2)
            {
                player.statDefense = (int)(player.statDefense * 1.05);
            }
        }
    }
}
