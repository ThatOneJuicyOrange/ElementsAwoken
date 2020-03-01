using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.VoidLeviathan
{
    public class AbyssalMatter : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;

            item.rare = 11;
            item.value = Item.sellPrice(0, 15, 0, 0);

            item.accessory = true;
            item.GetGlobalItem<EARarity>().awakened = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssal Matter");
            Tooltip.SetDefault("Creates Abyss Portals around the player\nIf the player is hit for above 40% life, they enter Abyssal Rage granting\n  30% increased damage\n  50% increased movement speed");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.abyssalMatter = true;
        }
    }
}
