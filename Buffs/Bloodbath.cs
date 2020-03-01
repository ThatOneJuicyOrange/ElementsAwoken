using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class Bloodbath : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Bloodbath");
            Description.SetDefault("Damage is increased by 20");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.allDamage *= 1.2f;
        }
    }
}