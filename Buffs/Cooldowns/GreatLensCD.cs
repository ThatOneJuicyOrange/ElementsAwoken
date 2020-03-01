using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.Cooldowns
{
    public class GreatLensCD : ModBuff
    {
        public override void SetDefaults()
		{
            DisplayName.SetDefault("Great Lens Cooldown");
            Description.SetDefault("You cannot use the Great Lens");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = false;
        }
	}
}