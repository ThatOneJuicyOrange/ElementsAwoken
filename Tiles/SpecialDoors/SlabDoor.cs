using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.NPCProj;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace ElementsAwoken.Tiles.SpecialDoors
{
    public class SlabDoor : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 128;

            npc.aiStyle = -1;

            npc.lifeMax = 1;

            npc.knockBackResist = 0f;

            npc.lavaImmune = true;
            npc.dontTakeDamage = true;
            npc.immortal = true;
            npc.friendly = true;
            npc.noGravity = true;
            npc.behindTiles = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void AI()
        {
            if (npc.ai[0] == 0) // open
            {
                //npc.ai[1] = npc.Left.X;
                npc.ai[2] = npc.Top.Y + 16;
                int boss = NPC.FindFirstNPC(NPCType<NPCs.TheOrder.ResistanceLeader>());
                if (boss >= 0)
                {
                    if (Main.npc[boss].ai[0] != -1) npc.ai[0] = 1;
                }
            }
            else if (npc.ai[0] == 1) // closing
            {
                if (npc.Bottom.Y > npc.ai[2])
                {
                    npc.position.Y -= 4;
                }
                else
                {
                    npc.ai[0] = 2;
                }
            }
            else if (npc.ai[0] == 2)// closed
            {
                EAUtils.SpecialDoorPushX(npc);
                int boss = NPC.FindFirstNPC(NPCType<NPCs.TheOrder.ResistanceLeader>());
                if (boss >= 0)
                {
                    if (Main.npc[boss].ai[0] == -1) npc.ai[0] = 3;
                }
                else npc.ai[0] = 3;
            }
            else // closing
            {
                if (npc.Top.Y < npc.ai[2] - 16)
                {
                    npc.position.Y += 1;
                }
                else npc.ai[0] = 0;
            }

        }
    }
}
