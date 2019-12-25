using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.Other
{
    public class CrystallineLocketCD : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Crystalline Locket Cooldown");
            Description.SetDefault("You cannot use the crystalline locket");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
            canBeCleared = false;
        }
	}
}