using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PotionBuffs
{
    public class CalamityPotionBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Calamity");
            Description.SetDefault("Spawnrates increased by 30");
        }
        public override void Update(Player player, ref int buffIndex)
        {
        }
    }
}