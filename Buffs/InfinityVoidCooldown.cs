using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class InfinityVoidCooldown : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Void Cooldown");
            Description.SetDefault("You cannot erase life... yet");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            canBeCleared = false;
        }
	}
}