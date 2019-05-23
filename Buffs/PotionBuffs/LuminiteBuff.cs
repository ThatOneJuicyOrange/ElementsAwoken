using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PotionBuffs
{
    public class LuminiteBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Celestial Empowerment");
            Description.SetDefault("Gain the strength of the Moon Lord\nIncreased defense by 8, Damage increased by 15%");
        }
        public override void Update(Player player, ref int buffIndex)
        { 
            player.statDefense += 8;
            player.meleeDamage += 0.15f;
            player.thrownDamage += 0.15f;
            player.rangedDamage += 0.15f;
            player.magicDamage += 0.15f;
            player.minionDamage += 0.15f;
        }
    }
}