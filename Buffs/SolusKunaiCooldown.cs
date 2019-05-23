using ElementsAwoken.NPCs;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class SolusKunaiCooldown : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Solus Kunai Cooldown");
            Description.SetDefault("You cant use Solus Kunais");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
            canBeCleared = false;
        }
	}
}