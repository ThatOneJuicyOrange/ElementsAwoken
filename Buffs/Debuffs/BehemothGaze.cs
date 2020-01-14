using ElementsAwoken.NPCs;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.Debuffs
{
    public class BehemothGaze : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Behemoth Gaze");
            Description.SetDefault("You have looked upon a being so great you cannot get away");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
		}
	}
}