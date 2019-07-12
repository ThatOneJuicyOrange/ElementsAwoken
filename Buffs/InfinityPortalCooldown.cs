using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class InfinityPortalCooldown : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Portal Cooldown");
            Description.SetDefault("You cannot create a portal");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            canBeCleared = false;
        }
	}
}