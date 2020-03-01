using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.zVanilla.Awakened
{
    public class GreatLens : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;

            item.rare = 1;
            item.value = Item.sellPrice(0, 2, 50, 0);

            item.accessory = true;
            item.GetGlobalItem<EARarity>().awakened = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Great Lens");
            Tooltip.SetDefault("");
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string hotkey;
            var list = ElementsAwoken.specialAbility.GetAssignedKeys();
            if (list.Count > 0) hotkey = list[0];
            else hotkey = "<Special Ability Unbound>";

            string baseTooltip = "Pressing the special ability key (" + hotkey + ") will encase the player in a giant lens which reflects 20% of the damage from projectiles and enemies\nIf the player is hit by an enemy the damage is reflected to them";
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
            modPlayer.greatLens = true;
        }
    }
}
