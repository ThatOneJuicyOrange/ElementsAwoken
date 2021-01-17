using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class Invigorated : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Invigorated");
            Description.SetDefault("'You feel new growth take hold'\nGreatly increased life regeneration\n5% increased movement speed");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 3;
            player.accRunSpeed *= 1.05f;
            player.moveSpeed *= 1.05f;
        }
    }
}