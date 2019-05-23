using Terraria;
using System;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.TheGuardian
{
    public class TheGuardianScreenShaderData : ScreenShaderData
    {
        private int theGuardianIndex;

        public TheGuardianScreenShaderData(string passName)
            : base(passName)
        {
        }

        private void UpdateTheGuardianIndex()
        {
            int TheGuardianType = ModLoader.GetMod("ElementsAwoken").NPCType("TheGuardianFly");
            if (theGuardianIndex >= 0 && Main.npc[theGuardianIndex].active && Main.npc[theGuardianIndex].type == TheGuardianType)
            {
                return;
            }
            theGuardianIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == TheGuardianType)
                {
                    theGuardianIndex = i;
                    break;
                }
            }
        }

        public override void Apply()
        {
            UpdateTheGuardianIndex();
            if (theGuardianIndex != -1)
            {
                UseTargetPosition(Main.npc[theGuardianIndex].Center);
            }
            base.Apply();
        }
    }
}