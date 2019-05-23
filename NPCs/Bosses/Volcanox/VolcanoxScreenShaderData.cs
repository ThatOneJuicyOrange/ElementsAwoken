using Terraria;
using System;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Volcanox
{
    public class VolcanoxScreenShaderData : ScreenShaderData
    {
        private int volcanoxIndex;

        public VolcanoxScreenShaderData(string passName)
            : base(passName)
        {
        }

        private void UpdateVolcanoxIndex()
        {
            int VolcanoxType = ModLoader.GetMod("ElementsAwoken").NPCType("Volcanox");
            if (volcanoxIndex >= 0 && Main.npc[volcanoxIndex].active && Main.npc[volcanoxIndex].type == VolcanoxType)
            {
                return;
            }
            volcanoxIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == VolcanoxType)
                {
                    volcanoxIndex = i;
                    break;
                }
            }
        }

        public override void Apply()
        {
            UpdateVolcanoxIndex();
            if (volcanoxIndex != -1)
            {
                UseTargetPosition(Main.npc[volcanoxIndex].Center);
            }
            base.Apply();
        }
    }
}