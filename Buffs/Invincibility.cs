using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class Invincibility : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Invincibility");
            Description.SetDefault("You cant be damaged");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.shadowDodge = true;
            player.immune = true;
            Lighting.AddLight(player.Center, 0.5f, 0.4f, 0.1f);
        }
    }
}