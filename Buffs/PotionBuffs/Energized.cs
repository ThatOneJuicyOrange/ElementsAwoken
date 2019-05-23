using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PotionBuffs
{
    public class Energized : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Energized");
            Description.SetDefault("50% increased movement speed\nLife regen increased by 5");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed *= 1.5f;
            player.lifeRegen += 5;
        }
    }
}