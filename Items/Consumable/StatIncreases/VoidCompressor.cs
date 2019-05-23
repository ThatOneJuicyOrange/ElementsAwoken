using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Consumable.StatIncreases
{
    public class VoidCompressor : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.maxStack = 999;

            item.consumable = true;

            item.useStyle = 4;
            item.useTime = 30;
            item.useAnimation = 30;
            item.UseSound = SoundID.Item4;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;
            item.value = Item.sellPrice(0, 2, 0, 0);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Compressor");
            Tooltip.SetDefault("Made to refine and compress your inner darkness into the ultimate void\nAllows you to change the appearance of your hearts, but nothing else\nFighters of The Calamity need not apply");
        }
        public override bool CanUseItem(Player player)
        {
            bool calamityEnabled = ModLoader.GetMod("CalamityMod") != null;
            return !calamityEnabled;
        }

        public override bool UseItem(Player player)
        {
            MyPlayer mPlayer = player.GetModPlayer<MyPlayer>();
            if (!mPlayer.voidCompressor)
            {
                mPlayer.voidCompressor = true;
            }
            else
            {
                mPlayer.voidCompressor = false;
            }
            return true;
        }
    }
}
