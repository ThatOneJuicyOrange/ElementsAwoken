using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Testing
{
    public class NoRespawn : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 10;
            item.height = 10;
            item.accessory = true;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("No Respawn Time");
            Tooltip.SetDefault("i'm a bad bitch you can't kill me");
        }

        public override void UpdateInventory(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.noRespawnTime = true;
        }
    }
}