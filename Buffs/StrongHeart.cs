using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class StrongHeart : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Strong Heart");
            Description.SetDefault("Your health is increased by 10%");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.statLifeMax2 = (int)(player.statLifeMax2 * 1.1f);
        }
    }
}