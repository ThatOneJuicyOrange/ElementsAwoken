using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PotionBuffs
{
    public class SilkySerumBuff : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Silky Serum");
            Description.SetDefault("Reduces contact damage and cuts fall damage in half");
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
            player.endurance += 0.1f;
			player.GetModPlayer<MyPlayer>().puffFall = true;
		}
	}
}