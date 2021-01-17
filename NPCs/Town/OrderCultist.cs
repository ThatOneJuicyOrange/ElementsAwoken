using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Town
{
    [AutoloadHead]
    public class OrderCultist : ModNPC
    {
        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;

            npc.width = 18;
            npc.height = 40;

            npc.aiStyle = 7;

            npc.damage = 10;
            npc.defense = 30;
            npc.lifeMax = 250;
            npc.knockBackResist = 0.5f;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            NPCID.Sets.AttackFrameCount[npc.type] = 1;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 90;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
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
        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            return true;
        }

        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(3))
            {
                case 0:
                    return "Halicyn";
                case 1:
                    return "Kadris";
                case 2:
                    return "Caustia";
                default:
                    return "default";
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 30;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 6;
            randExtraCooldown = 10;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = 712;
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 4f;
            randomOffset = 1f;
        }

        public override string GetChat()
        {
            string text = "hello";
            return text;
        }
        private int questToClaim = -1;
        private bool activeQuest = false;
        private int completedQuests = 0;
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (questToClaim > -1 || !activeQuest)
            {
                //QuestNPC.DrawQuestLight(npc, spriteBatch, questToClaim > -1, new Color(255, 0, 255, 0), new Color(0, 255, 255, 0));
                QuestNPC.DrawQuestIcon(npc, spriteBatch, questToClaim > -1);
            }
        }
        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("Quest");
            int questNum = 0;
            foreach (Quest k in QuestWorld.quests)
            {
                if (k.identifier.Contains("WithOrder"))
                {
                    if (k.claimed) questNum++;
                    if (k.identifier == "WithOrder3") questNum++;
                }
            }
            if (questNum >= 3) button2 = Language.GetTextValue("Purchase Banner");
        }
        public override void PostAI()
        {
            activeQuest = false;
            completedQuests = 0;
            questToClaim = -1;
            int questID = 0;
            foreach (Quest k in QuestWorld.quests)
            {
                if (k.identifier.Contains("WithOrder"))
                {
                    if (k.active) activeQuest = true;
                    if (k.completed && k.active) questToClaim = questID;
                    if (k.claimed) completedQuests++;
                }
                questID++;
            }
        }
        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                if (!activeQuest)
                {
                    if (completedQuests == 0)
                    {
                        Main.npcChatText = "Welcome to The Order of the Shadowed. I have added an entry to your Quest Log";
                        QuestSystem.ActivateQuest("WithOrder1");
                    }
                    else if (completedQuests == 1)
                    {
                        Main.npcChatText = "kill 3 bunnies";
                        QuestSystem.ActivateQuest("WithOrder2");
                    }
                    else if (completedQuests == 2)
                    {
                        Main.npcChatText = "place 3 dirt";
                        Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Placeable.OrderBanner>(),10);
                        QuestSystem.ActivateQuest("WithOrder3");
                    }
                    else if (completedQuests == 3)
                    {
                        Main.npcChatText = "deliver";
                        Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.QuestItems.OrderMessage>());
                        QuestSystem.ActivateQuest("WithOrder4");
                    }
                    else if (completedQuests == 4)
                    {
                        Main.npcChatText = "execute";
                        QuestSystem.ActivateQuest("WithOrder5");
                    }
                    else
                    {
                        Main.npcChatText = "im all out sorry";
                    }
                }
                else if (questToClaim >= 0)
                {
                    QuestWorld.quests[questToClaim].Complete(Main.LocalPlayer);
                    Main.npcChatText = "Well done!";
                }
                else
                {
                    Main.npcChatText = "You have not finished your task";
                }
            }
            else if (Main.LocalPlayer.BuyItem(Item.buyPrice(0, 0, 25, 0) - 1))
            {
                Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Placeable.OrderBanner>());
            }
        }        
    }
}
