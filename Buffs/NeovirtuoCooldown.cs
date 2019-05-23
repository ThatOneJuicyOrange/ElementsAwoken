using ElementsAwoken.NPCs;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class NeovirtuoCooldown : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Neovirtuo Cooldown");
            Description.SetDefault("You cant use Neovirtuo");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
            canBeCleared = false;
        }
	}
}