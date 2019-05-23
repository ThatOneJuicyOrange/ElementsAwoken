using Terraria;
using System;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.TheCelestial
{
    public class TheCelestialScreenShaderData : ScreenShaderData
    {
        private int celestialIndex;

        public TheCelestialScreenShaderData(string passName)
            : base(passName)
        {
        }

        private void UpdateCelestialIndex()
        {
            int CelestialType = ModLoader.GetMod("ElementsAwoken").NPCType("Infernace");
            if (celestialIndex >= 0 && Main.npc[celestialIndex].active && Main.npc[celestialIndex].type == CelestialType)
            {
                return;
            }
            celestialIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == CelestialType)
                {
                    celestialIndex = i;
                    break;
                }
            }
        }

        public override void Apply()
        {
            UpdateCelestialIndex();
            if (celestialIndex != -1)
            {
                UseTargetPosition(Main.npc[celestialIndex].Center);
            }
            base.Apply();
        }
    }
}