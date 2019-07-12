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

namespace ElementsAwoken.NPCs.Bosses.CosmicObserver
{

    [AutoloadBossHead]
    public class CosmicObserverSpawn : ModNPC
    {
        public override string Texture { get { return "ElementsAwoken/NPCs/Bosses/CosmicObserver/CosmicObserver"; } }

        public override void SetDefaults()
        {
            npc.lifeMax = 200;

            npc.immortal = true;
            npc.dontTakeDamage = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("A Cosmic Observer");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float spawnchance = MyWorld.downedCosmicObserver ? 0.0f : 0.02f; // around half the spawn chance of the wyvern
            MyPlayer modPlayer = spawnInfo.player.GetModPlayer<MyPlayer>(mod);

            if (modPlayer.increasedObserverChance && !NPC.AnyNPCs(mod.NPCType("CosmicObserver")))
            {
                return 0.4f;
            }
            if (MyWorld.downedCosmicObserver)
            {
                return 0.0f;
            }
            return spawnInfo.player.ZoneSkyHeight && !NPC.AnyNPCs(mod.NPCType("CosmicObserver")) && Main.hardMode ? spawnchance : 0f;
        }

        public override void AI()
        {
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("CosmicObserver"), 150);
            npc.active = false;
        }
       

        public override bool CheckActive()
        {
            return false;
        }
    }
}
