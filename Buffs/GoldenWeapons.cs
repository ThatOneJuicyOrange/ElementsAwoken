using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class GoldenWeapons : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Golden Weapons");
            Description.SetDefault("You deal 10% extra damage");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.allDamage *= 1.1f;
            Lighting.AddLight(player.Center, 0.5f, 0.4f, 0.1f);
        }
    }
}