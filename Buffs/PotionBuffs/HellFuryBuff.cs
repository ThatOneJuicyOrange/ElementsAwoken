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
            Description.SetDefault("Damage is increased by 20% but doubles damage taken");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.meleeDamage *= 1.2f;
            player.rangedDamage *= 1.2f;
            player.magicDamage *= 1.2f;
            player.minionDamage *= 1.2f;
            player.AddBuff(BuffID.WeaponImbueFire, 1);
        }
    }
}