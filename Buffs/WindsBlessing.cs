using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class WindsBlessing : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Winds Blessing");
            Description.SetDefault("The wind is behind you");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            //player.GetModPlayer<MyPlayer>().superSpeed = true;
            player.moveSpeed *= 1.6f;
        }
    }
}