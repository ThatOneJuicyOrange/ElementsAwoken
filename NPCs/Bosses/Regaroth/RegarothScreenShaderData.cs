using Terraria;
using System;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Regaroth
{
    public class RegarothScreenShaderData : ScreenShaderData
    {
        private int regarothIndex;

        public RegarothScreenShaderData(string passName)
            : base(passName)
        {
        }

        private void UpdateRegarothIndex()
        {
            int RegarothType = ModLoader.GetMod("ElementsAwoken").NPCType("RegarothHead");
            if (regarothIndex >= 0 && Main.npc[regarothIndex].active && Main.npc[regarothIndex].type == RegarothType)
            {
                return;
            }
            regarothIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == RegarothType)
                {
                    regarothIndex = i;
                    break;
                }
            }
        }

        public override void Apply()
        {
            UpdateRegarothIndex();
            if (regarothIndex != -1)
            {
                UseTargetPosition(Main.npc[regarothIndex].Center);
            }
            base.Apply();
        }
    }
}