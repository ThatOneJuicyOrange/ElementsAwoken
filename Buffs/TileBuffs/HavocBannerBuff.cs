using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.TileBuffs
{
    public class HavocBannerBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            DisplayName.SetDefault("Havoc Banner");
            Description.SetDefault("Spawnrates increased by 2x");
        }
        public override void Update(Player player, ref int buffIndex)
        {
        }
    }
}