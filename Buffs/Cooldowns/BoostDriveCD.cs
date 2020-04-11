using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.Cooldowns
{
    public class BoostDriveCD : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Boost Drive Cooldown");
            Description.SetDefault("You cannot use the Boost Drive");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = false;
        }
	}
}