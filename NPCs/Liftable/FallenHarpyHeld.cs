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

namespace ElementsAwoken.NPCs.Liftable
{
    public class FallenHarpyHeld : HeldNPCBase
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            npc.friendly = true;

            npc.lifeMax = 30;

            npc.width = 32;
            npc.height = 32;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.immortal = false;
            npc.dontTakeDamage = false;
            npc.GetGlobalNPC<NPCsGLOBAL>().saveNPC = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fallen Harpy");
        }
        public override void ExtraAI(bool held, Player player)
        {
            if (held)
            {
                npc.spriteDirection = -player.direction;
            }
            SpecialQuest quest = (SpecialQuest)QuestSystem.FindQuest("FallenHarpy");
            quest.questLocX = npc.Center.X / 16;
            quest.questLocY = npc.Center.Y / 16;
            for (int n = 0; n < Main.npc.Length; ++n)
            {
                NPC nPC = Main.npc[n];
                if (nPC.type == NPCID.Nurse && nPC.active && Vector2.Distance(npc.Center, nPC.Center) < 200)
                {
                    if (QuestSystem.IsQuestActive("FallenHarpy"))
                    {
                        if (quest != null) quest.thingsDone++;
                        quest.claimed = true;
                        quest.active = false;
                        for (int p = 0; p < Main.maxPlayers; p++)
                        {
                            Player player1 = Main.player[p];
                            if (player1.active)
                            {
                                for (int i = 0; i < quest.itemRewards.Count; i += 2)
                                {
                                    player1.QuickSpawnItem(quest.itemRewards[i], quest.itemRewards[i + 1]);
                                }
                            }
                        }
                        npc.HealEffect(100);
                        NPC harpy = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<FallenHarpyFly>())];
                        harpy.Center = npc.Center;
                        npc.active = false;
                        player.GetModPlayer<MyPlayer>().npcHeld = 0;
                    }
                }
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                SpecialQuest quest = (SpecialQuest)QuestSystem.FindQuest("FallenHarpy");
                if (quest.active && !quest.completed)
                {
                    quest.active = false;
                    CombatText.NewText(Main.LocalPlayer.getRect(), Color.Red, "Quest Failed", true, false);
                }
            }
        }
    }
}
