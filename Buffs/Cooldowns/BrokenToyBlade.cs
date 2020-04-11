using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.Cooldowns
{
    public class BrokenToyBlade : ModBuff
    {
        public override void SetDefaults()
		{
            DisplayName.SetDefault("Broken Toy Blade");
            Description.SetDefault("You cannot use the Toy Blade");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = false;
        }
	}
}