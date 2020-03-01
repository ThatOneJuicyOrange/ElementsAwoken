using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.Cooldowns
{
    public class ChaosShieldCooldown : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Chaos Shield Cooldown");
            Description.SetDefault("You cannot create a chaos shield");
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