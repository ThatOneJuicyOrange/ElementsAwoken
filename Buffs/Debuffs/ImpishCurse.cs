using ElementsAwoken.NPCs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.Debuffs
{
    public class ImpishCurse : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Impish Curse");
            Description.SetDefault("");
        }
		public override void Update(NPC npc, ref int buffIndex)
        {
            int num1 = Dust.NewDust(npc.position, npc.width, npc.height, 5);  
            Main.dust[num1].scale = 1f;
            Main.dust[num1].velocity *= 0.3f;
            Main.dust[num1].noGravity = true;

            npc.GetGlobalNPC<NPCsGLOBAL>().impishCurse = true;
        }
    }
}