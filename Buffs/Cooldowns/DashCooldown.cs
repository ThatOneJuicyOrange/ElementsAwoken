using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.Cooldowns
{
    public class DashCooldown : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Ninja Dash Cooldown");
            Description.SetDefault("Your dash is recharging");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            canBeCleared = false;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<MyPlayer>().dashCooldown = true;
		}
	}
}