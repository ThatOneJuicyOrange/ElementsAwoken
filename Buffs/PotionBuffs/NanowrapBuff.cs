using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PotionBuffs
{
    public class NanowrapBuff : ModBuff
    {
        public float healTimer = 6f;
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Healing");
            Description.SetDefault("Healing 100 health over 10 seconds");
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
                healTimer = 6f;
            }
        }
    }
}