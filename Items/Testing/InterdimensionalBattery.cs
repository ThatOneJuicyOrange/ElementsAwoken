using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Testing
{
    public class InterdimensionalBattery : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 44;

            item.rare = 11;

            item.maxStack = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Interdimensional Battery");
            Tooltip.SetDefault("Increases maximum energy by 500\nTESTING ITEM");
        }

        public override void UpdateInventory(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            modPlayer.maxEnergy += 500;
        }
    }
}
