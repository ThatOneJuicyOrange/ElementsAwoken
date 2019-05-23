using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PotionBuffs
{
    public class MedicineCooldown : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Medicine Cooldown");
            Description.SetDefault("You cannot use Medicinal Items");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<MyPlayer>(mod).medicineCooldown = true;
		}
	}
}