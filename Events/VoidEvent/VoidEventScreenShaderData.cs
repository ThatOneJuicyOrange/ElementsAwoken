using Terraria;
using System;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace ElementsAwoken.Events.VoidEvent
{
    public class VoidEventScreenShaderData : ScreenShaderData
    {
        //private int voidEventIndex;

        public VoidEventScreenShaderData(string passName)
            : base(passName)
        {
        }

        private void UpdateVoidEventIndex()
        {
            /*int voidEventType = MyWorld.voidInvasionUp;
            if (voidEventIndex >= 0 && Main.npc[voidEventIndex].active && Main.npc[voidEventIndex].type == voidEventType)
            {
                return;
            }
            voidEventIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (MyWorld.voidInvasionUp && Main.npc[i].type == voidEventType)
                {
                    voidEventIndex = i;
                    break;
                }
            }*/
        }

        public override void Apply()
        {
            UpdateVoidEventIndex();
            if (MyWorld.voidInvasionUp)
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    UseTargetPosition(Main.player[(int)Player.FindClosest(Main.npc[i].position, Main.npc[i].width, Main.npc[i].height)].Center);
                }
            }
            base.Apply();
        }
    }
}