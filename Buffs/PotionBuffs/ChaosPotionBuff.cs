using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PotionBuffs
{
    public class ChaosPotionBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Chaos");
            Description.SetDefault("Spawnrates increased by 7.5x");
        }
        public override void Update(Player player, ref int buffIndex)
        {
        }
    }
}