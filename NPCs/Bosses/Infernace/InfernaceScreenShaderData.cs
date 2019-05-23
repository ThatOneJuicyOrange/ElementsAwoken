using Terraria;
using System;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Infernace
{
    public class InfernaceScreenShaderData : ScreenShaderData
    {
        private int infernaceIndex;

        public InfernaceScreenShaderData(string passName)
            : base(passName)
        {
        }

        private void UpdateInfernaceIndex()
        {
            int InfernaceType = ModLoader.GetMod("ElementsAwoken").NPCType("Infernace");
            if (infernaceIndex >= 0 && Main.npc[infernaceIndex].active && Main.npc[infernaceIndex].type == InfernaceType)
            {
                return;
            }
            infernaceIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == InfernaceType)
                {
                    infernaceIndex = i;
                    break;
                }
            }
        }

        public override void Apply()
        {
            UpdateInfernaceIndex();
            if (infernaceIndex != -1)
            {
                UseTargetPosition(Main.npc[infernaceIndex].Center);
            }
            base.Apply();
        }
    }
}