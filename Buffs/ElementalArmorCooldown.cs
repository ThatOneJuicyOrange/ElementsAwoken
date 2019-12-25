using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class ElementalArmorCooldown : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Elemental Revive Cooldown");
            Description.SetDefault("Your revive is recharging");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            canBeCleared = false;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<MyPlayer>().elementalArmorCooldown = true;
		}
	}
}