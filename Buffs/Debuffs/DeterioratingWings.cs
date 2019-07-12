using ElementsAwoken.NPCs;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.Debuffs
{
    public class DeterioratingWings : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Deteriorating Wings");
            Description.SetDefault("Your wings are crumbling into black dust");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            longerExpertDebuff = true;
            canBeCleared = false;
        }
	
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>(mod).brokenWings = true;
        }
    }
}