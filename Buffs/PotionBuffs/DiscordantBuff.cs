using ElementsAwoken;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PotionBuffs
{
    public class DiscordantBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Discord");
            Description.SetDefault("Your body finds a way to embrace the Discord");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().discordantPotion = true;
        }
    }
}