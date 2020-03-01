using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Text;
using System.IO;
using Terraria.ModLoader.IO;

namespace ElementsAwoken.Items.BossDrops.ToySlime
{
    public class ToySlimeClaw : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 1;
            item.accessory = true;
            item.expert = true;
            item.GetGlobalItem<EARarity>().awakened = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slimy Toy Claw");
            Tooltip.SetDefault("");
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string hotkey;
            var list = ElementsAwoken.specialAbility.GetAssignedKeys();
            if (list.Count > 0) hotkey = list[0];
            else hotkey = "<Special Ability Unbound>";

            string baseTooltip = "Allows the ability to slide down walls\nPressing the special ability key (" + hotkey + ") key will throw slime at the cursor\nIf it is thrown down, the player will get launched up into the air and throw multiple projectiles down";
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
            modPlayer.toySlimeClaw = true;
        }
    }
}
