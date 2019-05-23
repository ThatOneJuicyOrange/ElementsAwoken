using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Buffs.Other
{
    public class DeletionMark : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("will be deactivated");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCsGLOBAL>(mod).delete = true;
        }
    }
}