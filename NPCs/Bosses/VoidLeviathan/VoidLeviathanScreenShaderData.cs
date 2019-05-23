using Terraria;
using System;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.VoidLeviathan
{
    public class VoidLeviathanScreenShaderData : ScreenShaderData
    {
        private int voidLeviathanIndex;

        public VoidLeviathanScreenShaderData(string passName)
            : base(passName)
        {
        }

        private void UpdateVoidLeviathanIndex()
        {
            int VoidLeviathanType = ModLoader.GetMod("ElementsAwoken").NPCType("VoidLeviathanHead");
            if (voidLeviathanIndex >= 0 && Main.npc[voidLeviathanIndex].active && Main.npc[voidLeviathanIndex].type == VoidLeviathanType)
            {
                return;
            }
            voidLeviathanIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == VoidLeviathanType)
                {
                    voidLeviathanIndex = i;
                    break;
                }
            }
        }

        public override void Apply()
        {
            UpdateVoidLeviathanIndex();
            if (voidLeviathanIndex != -1)
            {
                UseTargetPosition(Main.npc[voidLeviathanIndex].Center);
            }
            base.Apply();
        }
    }
}