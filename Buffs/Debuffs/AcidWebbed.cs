using ElementsAwoken.NPCs;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.Debuffs
{
    public class AcidWebbed : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Corrosive Web");
            Description.SetDefault("Your limbs are burning against the acidic web");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().acidWebbed = true;
        }
    }
}