using ElementsAwoken.NPCs;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class AncientDecay : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Ancient Decay");
            Description.SetDefault("Your soul is wearing away");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}
		
		public override void Update(NPC npc, ref int buffIndex)
		{
            npc.GetGlobalNPC<NPCsGLOBAL>(mod).ancientDecay = true;
		}
	}
}