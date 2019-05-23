using Terraria;
using System;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Ancients
{
    public class AncientsScreenShaderData : ScreenShaderData
    {
        private int ancientIndex;
        private int[] ancients = new int[5];

        public AncientsScreenShaderData(string passName)
            : base(passName)
        {
        }

        private void UpdateAncientIndex()
        {
            int[] ancients = new int[5];
            ancients[0] = ModLoader.GetMod("ElementsAwoken").NPCType("Izaris");
            ancients[1] = ModLoader.GetMod("ElementsAwoken").NPCType("Kirvein");
            ancients[2] = ModLoader.GetMod("ElementsAwoken").NPCType("Krecheus");
            ancients[3] = ModLoader.GetMod("ElementsAwoken").NPCType("Xernon");
            ancients[4] = ModLoader.GetMod("ElementsAwoken").NPCType("AncientAmalgam");

            if (ancientIndex >= 0 && Main.npc[ancientIndex].active && CheckIfAncient())
            {
                return;
            }
            ancientIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        if (Main.npc[ancientIndex].type == ancients[k])
                        {
                            ancientIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        private bool CheckIfAncient()
        {
            for (int i = 0; i < 5; i++)
            {
                if (Main.npc[ancientIndex].type == ancients[i])
                {
                    return true;
                }
            }
            return false;
        }

        public override void Apply()
        {
            UpdateAncientIndex();
            if (ancientIndex != -1)
            {
                UseTargetPosition(Main.npc[ancientIndex].Center);
            }
            base.Apply();
        }
    }
}