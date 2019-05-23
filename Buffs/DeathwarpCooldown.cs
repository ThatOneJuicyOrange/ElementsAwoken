using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class DeathwarpCooldown : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Deathwarp Cooldown");
            Description.SetDefault("You can't activate deathwarp");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            canBeCleared = false;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
		}
	}
}