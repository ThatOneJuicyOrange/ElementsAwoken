using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.TileBuffs
{
    public class StatueBuffBurst : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            DisplayName.SetDefault("Burst Presence");
            Description.SetDefault("15% increased ranged damage\n15% decreased movement speed");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.rangedDamage *= 1.15f;
            player.moveSpeed *= 0.85f;
        }
    }
}