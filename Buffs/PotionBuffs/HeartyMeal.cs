using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PotionBuffs
{
    public class HeartyMeal : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Hearty Meal");
            Description.SetDefault("Heals 20 health over 10 seconds");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.GetModPlayer<MyPlayer>().generalTimer % 60 == 0)
            { 
                player.statLife += 2;
                player.HealEffect(2);
            }
        }
    }
}