using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.Other
{
    public class FlareShieldCooldown : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Drakonian Guard Cooldown");
            Description.SetDefault("You cannot use the Drakonian Guard");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
            canBeCleared = false;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
		}
	}
}