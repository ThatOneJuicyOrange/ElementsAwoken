using Terraria;
using System;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Permafrost
{
    public class PermafrostScreenShaderData : ScreenShaderData
    {
        private int permafrostIndex;

        public PermafrostScreenShaderData(string passName)
            : base(passName)
        {
        }

        private void UpdatePermafrostIndex()
        {
            int PermafrostType = ModLoader.GetMod("ElementsAwoken").NPCType("Permafrost");
            if (permafrostIndex >= 0 && Main.npc[permafrostIndex].active && Main.npc[permafrostIndex].type == PermafrostType)
            {
                return;
            }
            permafrostIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == PermafrostType)
                {
                    permafrostIndex = i;
                    break;
                }
            }
        }

        public override void Apply()
        {
            UpdatePermafrostIndex();
            if (permafrostIndex != -1)
            {
                UseTargetPosition(Main.npc[permafrostIndex].Center);
            }
            base.Apply();
        }
    }
}