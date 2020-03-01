using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Utilities;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.CosmicObserver
{
    public class CosmicGlass : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 26;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.GetGlobalItem<EARarity>().awakened = true;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Glass");
            Tooltip.SetDefault("15% increases critical strike chance\nCritical strikes fire a cosmic beam at the enemy");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.magicCrit += 15;
            player.meleeCrit += 15;
            player.rangedCrit += 15;
            player.thrownCrit += 15;
            modPlayer.cosmicGlass = true;
        }
    }
}
