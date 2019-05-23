using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Consumable.StatIncreases
{
    public class ChaosHeart : ModItem
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
            DisplayName.SetDefault("Chaos Heart");
            Tooltip.SetDefault("Fighters of The Calamity need not apply\nPermanently increases maximum life by 10");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 5));
        }

        public override bool CanUseItem(Player player)
        {
            bool calamityEnabled = ModLoader.GetMod("CalamityMod") != null;
            return !calamityEnabled && player.GetModPlayer<MyPlayer>().voidHeartsUsed == 10 && player.GetModPlayer<MyPlayer>().chaosHeartsUsed < 10;
        }

        public override bool UseItem(Player player)
        {
            player.statLifeMax2 += 10;
            player.statLife += 10;
            if (Main.myPlayer == player.whoAmI)
            {
                player.HealEffect(10, true);
            }
            player.GetModPlayer<MyPlayer>().chaosHeartsUsed += 1;
            return true;
        }
    }
}
