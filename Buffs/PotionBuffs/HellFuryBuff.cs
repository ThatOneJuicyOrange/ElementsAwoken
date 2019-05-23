using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PotionBuffs
{
    public class HellFuryBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Hell Fury");
            Description.SetDefault("Damage is increased by 25% but defense decreased by 10");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.meleeDamage *= 1.25f;
            player.rangedDamage *= 1.25f;
            player.magicDamage *= 1.25f;
            player.minionDamage *= 1.25f;
            player.statDefense -= 10;
            player.AddBuff(BuffID.WeaponImbueFire, 1);  //this is an example of how to add existing buff
        }
    }
}