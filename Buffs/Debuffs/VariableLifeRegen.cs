using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Buffs.Debuffs
{
    public class VariableLifeRegen : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Life Drain");
            Description.SetDefault("Suck the life out of you.");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCsGLOBAL>().variableLifeDrain = true;
        }
    }
}