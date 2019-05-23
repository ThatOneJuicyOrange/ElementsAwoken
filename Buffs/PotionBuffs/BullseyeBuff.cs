using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PotionBuffs
{
    public class BullseyeBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Bullseye");
            Description.SetDefault("Critical Strike chance increased by 15%\nRanged damage increased by 25%");
        }
        public override void Update(Player player, ref int buffIndex)
        { 
            player.magicCrit += 15;
            player.meleeCrit += 15;
            player.rangedCrit += 15;
            player.rangedCrit += 15;
            player.rangedDamage *= 1.25f;
        }
    }
}