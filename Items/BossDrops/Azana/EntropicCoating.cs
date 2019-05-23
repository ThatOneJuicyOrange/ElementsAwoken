using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Azana
{
    public class EntropicCoating : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;
            item.value = Item.sellPrice(0, 35, 0, 0);

            item.accessory = true;

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Entropic Coating");
            Tooltip.SetDefault("Grants immunity to Chaos Burn");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(9, 4));
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[mod.BuffType("ChaosBurn")] = true;
        }
    }
}
