using ElementsAwoken.NPCs;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class Glowing : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Glowing");
            Description.SetDefault("");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(NPC npc, ref int buffIndex)
		{
            npc.GetGlobalNPC<NPCsGLOBAL>(mod).glowing = true;
        }
    }
}