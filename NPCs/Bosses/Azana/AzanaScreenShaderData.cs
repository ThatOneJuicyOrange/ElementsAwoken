using Terraria;
using System;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Azana
{
    public class AzanaScreenShaderData : ScreenShaderData
    {
        private int azanaIndex;

        public AzanaScreenShaderData(string passName)
            : base(passName)
        {
        }

        private void UpdateAzanaIndex()
        {
            int AzanaType = ModLoader.GetMod("ElementsAwoken").NPCType("Azana");
            if (azanaIndex >= 0 && Main.npc[azanaIndex].active && Main.npc[azanaIndex].type == AzanaType)
            {
                return;
            }
            azanaIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == AzanaType)
                {
                    azanaIndex = i;
                    break;
                }
            }
        }

        public override void Apply()
        {
            UpdateAzanaIndex();
            if (azanaIndex != -1)
            {
                UseTargetPosition(Main.npc[azanaIndex].Center);
            }
            base.Apply();
        }
    }
}