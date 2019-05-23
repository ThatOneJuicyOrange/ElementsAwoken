using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class BarrierCooldown : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Barrier Cooldown");
            Description.SetDefault("You cannot create a barrier");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            canBeCleared = false;
        }
	}
}