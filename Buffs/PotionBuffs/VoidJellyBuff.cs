using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PotionBuffs
{
    public class VoidJellyBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Void Jelly");
            Description.SetDefault("Damage is increased by 15%");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.meleeDamage *= 1.15f;
            player.rangedDamage *= 1.15f;
            player.magicDamage *= 1.15f;
            player.minionDamage *= 1.15f;
        }
    }
}