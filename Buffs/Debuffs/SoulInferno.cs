using ElementsAwoken.NPCs;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.Debuffs
{
    public class SoulInferno : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Soul Inferno");
            Description.SetDefault("You glow with a strange radiation");
        }
		public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCsGLOBAL>().soulInferno = true;
            int num1 = Dust.NewDust(npc.position, npc.width, npc.height, 173);  
            Main.dust[num1].scale = 1f;
            Main.dust[num1].velocity *= 0.5f;
            Main.dust[num1].noGravity = true;
        }
    }
}