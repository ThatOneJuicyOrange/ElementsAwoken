using Terraria;
using System;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Aqueous
{
    public class AqueousScreenShaderData : ScreenShaderData
    {
        private int aqueousIndex;

        public AqueousScreenShaderData(string passName)
            : base(passName)
        {
        }

        private void UpdateAqueousIndex()
        {
            int AqueousType = ModLoader.GetMod("ElementsAwoken").NPCType("Aqueous");
            if (aqueousIndex >= 0 && Main.npc[aqueousIndex].active && Main.npc[aqueousIndex].type == AqueousType)
            {
                return;
            }
            aqueousIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == AqueousType)
                {
                    aqueousIndex = i;
                    break;
                }
            }
        }

        public override void Apply()
        {
            UpdateAqueousIndex();
            if (aqueousIndex != -1)
            {
                UseTargetPosition(Main.npc[aqueousIndex].Center);
            }
            base.Apply();
        }
    }
}