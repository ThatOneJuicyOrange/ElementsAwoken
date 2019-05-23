using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.TileBuffs
{
    public class StatueBuffAmadis : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            DisplayName.SetDefault("Amadis Presence");
            Description.SetDefault("15% increased melee damage and speed\n30% reduced health");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.meleeDamage *= 1.15f;
            player.meleeSpeed *= 1.15f;

            player.statLifeMax2 = (int)(player.statLifeMax2 * 0.7f) + 1;
        }
    }
}