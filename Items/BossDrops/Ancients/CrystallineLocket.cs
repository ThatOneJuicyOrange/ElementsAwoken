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

namespace ElementsAwoken.Items.BossDrops.Ancients
{
    public class CrystallineLocket : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 11;
            item.accessory = true;
            item.expert = true;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystalline Locket");
            Tooltip.SetDefault("Press the special ability key to make the player always critically strike for 10 seconds");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.crystallineLocket = true;
        }
    }
}
