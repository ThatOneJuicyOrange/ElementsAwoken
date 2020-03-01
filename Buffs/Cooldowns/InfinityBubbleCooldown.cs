using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.Cooldowns
{
    public class InfinityBubbleCooldown : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Projectile Bubbler Cooldown");
            Description.SetDefault("You cannot turn projectiles into bubbles");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            canBeCleared = false;
        }
	}
}