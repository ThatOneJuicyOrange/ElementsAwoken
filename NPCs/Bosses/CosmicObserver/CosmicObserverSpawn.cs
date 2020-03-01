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
            Main.npcFrameCount[npc.type] = 4;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            MyPlayer modPlayer = spawnInfo.player.GetModPlayer<MyPlayer>();
            float spawnchance = 0.04f / 6; //0.04 is the wyvern chance

            if (modPlayer.observerChanceTimer > 0)
            {
                spawnchance = 0.4f;
            }
            else if (MyWorld.downedCosmicObserver)
            {
                spawnchance = 0.00005f;
            }
            return spawnInfo.player.ZoneSkyHeight && !spawnInfo.playerInTown && !NPC.AnyNPCs(mod.NPCType("CosmicObserver")) && Main.hardMode ? spawnchance : 0f;
        }

        public override void AI()
        {
            Player P = Main.player[npc.target];
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("CosmicObserver"), 150);
            Main.PlaySound(15, (int)P.position.X, (int)P.position.Y, 0);
            Main.NewText("A Cosmic Observer roams the skies", 175, 75, 255, false);
            npc.active = false;
        }
       

        public override bool CheckActive()
        {
            return false;
        }
    }
}
