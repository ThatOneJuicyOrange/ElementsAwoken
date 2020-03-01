using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.Cooldowns
{
    public class ArchaicProtectionCD : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Archaic Protection Cooldown");
            Description.SetDefault("You cannot encase yourself in crystals");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
            canBeCleared = false;
        }
	}
}