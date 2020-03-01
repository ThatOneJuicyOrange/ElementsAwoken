using ElementsAwoken.NPCs;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.Cooldowns
{
    public class AstralTearCooldown : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Astral Tear Cooldown");
            Description.SetDefault("You cant use Astral Tear");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
            canBeCleared = false;
        }
	}
}