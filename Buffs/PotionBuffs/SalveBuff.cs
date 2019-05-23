using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PotionBuffs
{
    public class SalveBuff : ModBuff
    {
        public float healTimer = 12f;
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Healing");
            Description.SetDefault("Healing 50 health over 10 seconds");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (healTimer > 0f)
            {
                healTimer -= 1f;
            }
            if (healTimer == 0f)
            {
                player.statLife += 1;
                player.HealEffect(1);
                healTimer = 12f;
            }
        }
    }
}