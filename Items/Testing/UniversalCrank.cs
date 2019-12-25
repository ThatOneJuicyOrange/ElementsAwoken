using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Testing
{
    public class UniversalCrank : ModItem
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
            DisplayName.SetDefault("Universal Crank");
            Tooltip.SetDefault("Right click in inventory to generate 25 energy\nTESTING ITEM");
        }

        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/HandCrank"), 1, 0);
            modPlayer.energy += 25;
            item.stack++;
        }
    }
}
