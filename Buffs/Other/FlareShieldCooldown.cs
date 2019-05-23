﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.Other
{
    public class FlareShieldCooldown : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Flare Shield Cooldown");
            Description.SetDefault("You cannot create a flare shield");
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