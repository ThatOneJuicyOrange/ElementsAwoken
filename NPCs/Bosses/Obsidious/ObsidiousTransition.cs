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

namespace ElementsAwoken.NPCs.Bosses.Obsidious
{
    public class ObsidiousTransition : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 48;
            npc.height = 48;

            npc.lifeMax = 20;
            npc.knockBackResist = 0f;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.immortal = true;
            npc.dontTakeDamage = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ObsidiousTheme");

            npc.scale = 1f;

            npc.npcSlots = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidious");
            Main.npcFrameCount[npc.type] = 16;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;
            if (npc.frameCounter > 4)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y > frameHeight * 15)
            {
                npc.frame.Y = 0;
            }
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void AI()
        {
            npc.ai[0]++;
            if (npc.ai[0] == 150)
            {
                Main.NewText("I didnt want to have to do this...", new Color(188, 58, 49));
            }
            if (npc.ai[0] == 200)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Obsidious"));
                npc.active = false;
            }
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}
