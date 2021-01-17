using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PotionBuffs
{
    public class LavaFishingBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Infernal Fisherman");
            Description.SetDefault("Allows you to fish in lava");
        }
        public override void Update(Player player, ref int buffIndex)
        {
        }
    }
}