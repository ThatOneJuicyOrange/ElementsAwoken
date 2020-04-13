using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ElementsAwoken.Items.ItemSets.HiveCrate
{
    public class HoneyCocoon : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 1;
            item.value = Item.sellPrice(0, 0, 20, 0);
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Honey Cocoon");
            Tooltip.SetDefault("");
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string hotkey;
            var list = ElementsAwoken.specialAbility.GetAssignedKeys();
            if (list.Count > 0) hotkey = list[0];
            else hotkey = "<Special Ability Unbound>";

            string baseTooltip = "Pressing the special ability key (" + hotkey + ") will encase the player in a honey cocoon for 15 seconds\nThe cocoon can absorb up to 100 damage before breaking\nPress (" + hotkey + ") again to get out\nBeing in the cocoon prevents movement\n1 minute cooldown";
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
            modPlayer.honeyCocoon = true;
        }
    }
}
