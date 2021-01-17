using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Town
{
    public class OrderQuest : ModNPC
    {
        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;

            npc.width = 18;
            npc.height = 40;

            npc.damage = 10;
            npc.defense = 30;
            npc.lifeMax = 250;
            npc.knockBackResist = 0.5f;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            animationType = NPCID.Wizard;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Order Cultist");
            Main.npcFrameCount[npc.type] = 22;
        }
        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            return false;
        }

        public override string GetChat()
        {
            string text = "";
            Quest deliverQuest = null;
            foreach (Quest k in QuestWorld.activeQuests)
            {
                if (k.identifier == "WithOrder4") deliverQuest = k;
            }
            if (deliverQuest != null)
            {
                if (deliverQuest.CheckDone(Main.LocalPlayer,true)) text = "Thank you. Your assistance will not go unheard.";
                else text = "Have you got the message?";
            }
            else text = "Praise the Shadowed One";
            return text;
        }
        public override void PostAI()
        {
            bool tooFar = true;
            for (int p = 0; p < Main.maxPlayers; p++)
            { 
                if (Main.player[p].active && Vector2.Distance(Main.player[p].position / 16,npc.position / 16) < 200)
                {
                    tooFar = false;
                    break;
                }
            }
            if (tooFar)
            {
                npc.townNPC = false;
                npc.active = false;
            }
        }
    }
}
