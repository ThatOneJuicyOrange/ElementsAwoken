using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.TileBuffs
{
    public class OrdersInfluence : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            DisplayName.SetDefault("Order's Influence");
            Description.SetDefault("Reduces damage taken by 2%");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.endurance += 0.02f;
        }
    }
}