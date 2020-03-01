using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class VilePower : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Vile Power");
            Description.SetDefault("Damage is increased by 5%\nAttacks inflict poison and venom");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().vilePower = true;
            player.allDamage *= 1.05f;
        }
    }
}