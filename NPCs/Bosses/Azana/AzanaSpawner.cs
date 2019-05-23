using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Azana
{
    public class AzanaSpawner : ModNPC
    {
        public override void SetDefaults()
        {
            npc.lifeMax = 1000;
            npc.width = 2;
            npc.height = 2;
            npc.noGravity = true;
            npc.scale = 1f;
            npc.npcSlots = 1f;

            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AzanaThemeBreak");
            npc.netAlways = true;
            npc.immortal = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void AI()
        {
            npc.ai[1]++;
            if (npc.ai[1] >= 600)
            {
                npc.active = false;

                Vector2 spawnAt = npc.Center + new Vector2(0f, (float)npc.height / 2f);
                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("Azana"));
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}
