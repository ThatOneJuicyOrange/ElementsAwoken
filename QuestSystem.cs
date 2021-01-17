using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using ElementsAwoken.Items.QuestItems;
using ElementsAwoken.NPCs.Town;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.NPCs.TheOrder;
using Terraria.Graphics.Shaders;
using System.Text.RegularExpressions;

namespace ElementsAwoken
{
    public class QuestSystem
    {
        public static void CompleteQuest(Quest k, Player player, Mod mod)
        {
            k.completed = true;
            CombatText.NewText(player.getRect(), Color.Yellow, "Quest Completed!", true, false);
            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/QuestComplete"));
        }
        public static void ActivateQuest(string identifier)
        {
            foreach (Quest k in QuestWorld.quests)
            {
                if (k.identifier == identifier) k.active = true;
            }
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player player = Main.player[i];
                if (player.active && !player.dead) CombatText.NewText(player.getRect(), Color.Cyan, "Quest Log Updated", true, false);
            }
        }
        public static bool IsQuestActive(string identifier)
        {
            bool flag = false;
            foreach (Quest k in QuestWorld.activeQuests)
            {
                if (k.identifier == identifier && k.active) flag = true;
            }
            return flag;
        }
        public static bool IsQuestCompleted(string identifier)
        {
            bool flag = false;
            foreach (Quest k in QuestWorld.quests)
            {
                if (k.identifier == identifier && k.completed) flag = true;
            }
            return flag;
        }
        public static Quest FindQuest(string identifier)
        {
            foreach (Quest k in QuestWorld.quests)
            {
                if (k.identifier == identifier)
                {
                    return k;
                }
            }
            return null; // may be a problem
        }
    }
    public class QuestPlayer : ModPlayer
    {
        public override void PostUpdate()
        {
            foreach (Quest k in QuestWorld.quests)
            {
                if (k.active && !k.completed)
                {
                    if (k.CheckDone(player))
                    {
                        QuestSystem.CompleteQuest(k, player, mod);
                    }
                    if (k.identifier == "Merchant4")
                    {
                        Tile chest = Framing.GetTileSafely(player.chestX, player.chestY);
                        if (chest.type == 21 && chest.frameX >= 468 && chest.frameX <= 504)
                        {
                            QuestSystem.CompleteQuest(k, player, mod);
                            //SpecialQuest special = (SpecialQuest)k;
                            //special.thingsDone++;
                        }
                    }
                }
            }
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (target.life <= 0) CheckKillQuest(target);
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (target.life <= 0) CheckKillQuest(target);
        }
        private void CheckKillQuest(NPC target)
        {
            foreach (Quest k in QuestWorld.quests)
            {
                if (k is KillQuest quest)
                {
                    for (int q = 0; q < quest.killsGotten.Count; q++)
                    {
                        bool alternateEnemy = false;
                        if (quest.targetType[q] == NPCID.WallofFlesh && target.type == NPCID.WallofFleshEye) alternateEnemy = true;
                        if (target.type == quest.targetType[q] || alternateEnemy)
                        {
                            quest.killsGotten[q]++;
                            if (quest.active && !quest.completed)
                            {
                                if (quest.CheckDone(player))
                                {
                                    QuestSystem.CompleteQuest(k, player, mod);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public class QuestNPC :GlobalNPC
    {
        public string hasQuestAvailable = "";
        public string hasQuestActive = "";
        public int hasQuestToClaim = -1;
        public override bool InstancePerEntity => true;
        public override void ResetEffects(NPC npc)
        {
            hasQuestAvailable = "";
            hasQuestActive = "";
            hasQuestToClaim = -1;
        }
        public void UpdateQuest(bool justText = false)
        {
            if (hasQuestToClaim == -1)
            {
                string textPlaceholder = "I have updated your quest log with the details.";
                if (GetInstance<Config>().debugMode) textPlaceholder = "PLACEHOLDER: " + textPlaceholder;

                string questIdentifier = hasQuestAvailable;
                if (hasQuestAvailable == "") questIdentifier = hasQuestActive;
                string text = QuestSystem.FindQuest(questIdentifier).npcDialogueInfo;
                if (text == null || text == "") text = textPlaceholder;
                Main.npcChatText = text;

                // other stuff that needs to happen after
                if (questIdentifier == "Guide9")
                {
                    Quest k = QuestSystem.FindQuest(questIdentifier);
                    if (WorldGen.oreTier2 == TileID.Mythril) k.description = "Gather 10 Mythril bars and give them to the Guide";
                    else k.description = "Gather 10 Orichalcum bars and give them to the Guide";
                }

                if (!justText) QuestSystem.ActivateQuest(hasQuestAvailable);
            }
            else
            {
                string textPlaceholder = "Thanks! Here, take these as a token of my appreciation.";
                if (GetInstance<Config>().debugMode) textPlaceholder = "PLACEHOLDER: " + textPlaceholder;

                string identifier = QuestWorld.quests[hasQuestToClaim].identifier;
                string text = QuestSystem.FindQuest(identifier).npcDialogueInfo;
                if (text == null || text == "") text = textPlaceholder;
                Main.npcChatText = text;

                QuestWorld.quests[hasQuestToClaim].Complete(Main.LocalPlayer);
            }
        }
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if (hasQuestAvailable != "" || hasQuestToClaim > -1)
            {
                //DrawQuestLight(npc, spriteBatch, hasQuestToClaim > -1, new Color(255, 255, 0, 0), new Color(0, 255, 255, 0));
                DrawQuestIcon(npc, spriteBatch, hasQuestToClaim > -1);
            }
        }
        public static void DrawQuestLight(NPC npc, SpriteBatch spriteBatch,bool hasQuestToClaim,Color hasQuestColor,Color finishedQuestColor)
        {
            Texture2D texture = Main.extraTexture[60];
            Color color = hasQuestToClaim ? finishedQuestColor : hasQuestColor;
            Vector2 origin3 = new Vector2(66f, 86f);
            spriteBatch.Draw(texture, npc.Bottom - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, 4), null, color, 0f, origin3, new Vector2(0.5f, 0.3f), SpriteEffects.None, 0f);
        }
        public static void DrawQuestIcon(NPC npc, SpriteBatch spriteBatch, bool hasQuestToClaim)
        {
            Texture2D texture = hasQuestToClaim ? GetTexture("ElementsAwoken/Extra/QuestToClaim") : GetTexture("ElementsAwoken/Extra/QuestAvailable");
            float scale = MathHelper.Lerp(0.8f, 1f, (float)(1 + Math.Sin((float)MyWorld.generalTimer / 30)) / 2);
            if (npc.type == NPCType<OrderCultist>())
            {
                int shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.ShadowflameHadesDye);

                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                GameShaders.Armor.ApplySecondary(shader, Main.player[Main.myPlayer], null);

                Main.spriteBatch.Draw(texture, npc.Top - Main.screenPosition + new Vector2(0, npc.gfxOffY) - new Vector2(0, 30), null, Color.White, npc.rotation, texture.Size() / 2, scale, SpriteEffects.None, 0);

                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            }
            else spriteBatch.Draw(texture, npc.Top - Main.screenPosition + new Vector2(0, npc.gfxOffY) - new Vector2(0, 30), null, Color.White, 0f, texture.Size() / 2, scale, SpriteEffects.None, 0f);

        }
        public override bool PreAI(NPC npc)
        {
            if (npc.townNPC)
            {
                int qNum = 0;
                foreach (Quest k in QuestWorld.quests)
                {
                    if (npc.type == NPCID.Guide)
                    {
                        if (k.identifier.Contains("Guide"))
                        {
                            int guideQuestNum = int.Parse(Regex.Match(k.identifier, @"\d+$").Value);
                            //int guideQuestNum = int.Parse(k.identifier.Substring(k.identifier.Length - 1));
                            if (guideQuestNum >= 8 && !Main.hardMode) break;
                            if (!k.active && !k.claimed)
                            {
                                hasQuestAvailable = k.identifier;
                                break;
                            }
                            else if (k.active && k.completed)
                            {
                                hasQuestToClaim = qNum;
                                break;
                            }
                            else if (k.active)
                            {
                                hasQuestActive = k.identifier;
                                break;
                            }
                        }
                    }
                    else if (npc.type == NPCID.TaxCollector)
                    {
                    }
                    else if (npc.type == NPCID.Merchant)
                    {
                        if (k.identifier.Contains("Merchant"))
                        {
                            int merchantQuestNum = int.Parse(Regex.Match(k.identifier, @"\d+$").Value);
                            if (!k.active && !k.claimed)
                            {
                                hasQuestAvailable = k.identifier;
                                break;
                            }
                            else if (k.active && k.completed)
                            {
                                hasQuestToClaim = qNum;
                                break;
                            }
                            else if (k.active)
                            {
                                hasQuestActive = k.identifier;
                                break;
                            }
                        }
                    }
                    else if (npc.type == NPCID.Nurse)
                    {
                        if (k.identifier.Contains("Nurse"))
                        {
                            int nurseQuestNum = int.Parse(Regex.Match(k.identifier, @"\d+$").Value);
                            if (nurseQuestNum == 2 && EAWorldGen.pinkyCaveLoc.X == 0) continue;
                            if (nurseQuestNum >= 6 && !NPC.downedMechBossAny) break;
                            if (!k.active && !k.claimed)
                            {
                                hasQuestAvailable = k.identifier;
                                break;
                            }
                            else if (k.active && k.completed)
                            {
                                hasQuestToClaim = qNum;
                                break;
                            }
                            else if (k.active)
                            {
                                hasQuestActive = k.identifier;
                                break;
                            }
                        }
                    }
                    else if (npc.type == NPCID.WitchDoctor)
                    {
                        if (k.identifier == "CureVoidbroken")
                        {
                            if (!k.active && !k.claimed)
                            {
                                hasQuestAvailable = k.identifier;
                                break;
                            }
                            else if (k.active && k.completed)
                            {
                                hasQuestToClaim = qNum;
                                break;
                            }
                            else if (k.active)
                            {
                                hasQuestActive = k.identifier;
                            }
                        }
                    }
                    qNum++;
                }
            }
            return base.PreAI(npc);
        }
    }
    public class QuestWorld : ModWorld
    {
        public static List<Quest> quests = new List<Quest>();
        public static List<Quest> activeQuests = new List<Quest>();
        public static int loadedQuests = 0;

        public static int ritualSiteX = 0;
        public static int ritualSiteY = 0;
        public override void Initialize()
        {
            ritualSiteX = 0;
            ritualSiteY = 0;

            // do not change identifier or the saving will break
            loadedQuests = 0;
            quests.Clear();
            Quest quest = new FetchQuest("WithOrder1", "Initiate", "Bring the Order Cultist 10 Healing Potions", new List<int> { ItemID.HellstoneBar, 10 },ItemID.HealingPotion,10);
            quests.Add(quest);
            quest = new KillQuest("WithOrder2", "Clean", "Deal with a mishap that occured at a ritual site. Location pinned on the map", new List<int> { ItemID.HellstoneBar, 10 }, NPCType<ShadowDemon>(), 1);
            quests.Add(quest);
            quest = new PlaceQuest("WithOrder3", "Influence", "Place 10 Order of the Shadowed banners around the world", new List<int> { ItemID.HellstoneBar, 10 }, TileType<Tiles.Objects.OrderBannerTile>(), 10,true);
            quests.Add(quest);

            int x = 400;
            int y = (int)(Main.maxTilesY * 0.1f);
            for (int j = 0; j < 500; j++)
            {
                if (Main.tile[x, y + j].active() && Main.tileSolid[Main.tile[x, y + j].type])
                {
                    y += j;
                    break;
                }
            }
            y -= 3;
            quest = new DeliverQuest("WithOrder4", "Deliver", "Deliver a secret message to an Order Cultist marked on the map", new List<int> { ItemID.HellstoneBar, 10 }, NPCType<OrderQuest>(), ItemType<OrderMessage>(),1,x,y,true);
            quest.questLocX = x;
            quest.questLocY = y;
            quests.Add(quest);

            int order5X = WorldGen.dungeonX > Main.maxTilesX / 2 ? Main.maxTilesX / 2 - 800 : Main.maxTilesX / 2 + 800;
            int order5Y = (int)(Main.maxTilesY * 0.1f);
            for (int j = 0; j < 500; j++)
            {
                if (Main.tile[order5X, order5Y + j].active() && Main.tileSolid[Main.tile[order5X, order5Y + j].type])
                {
                    order5Y += j;
                    break;
                }
            }
            order5Y -= 10;
            quest = new KillQuest("WithOrder5", "Execute", "Take out a Resistance Commander while they are on a scouting mission. Location pinned on the map", new List<int> { ItemID.HellstoneBar, 10 }, NPCType<ResistanceLeader>(), 1, true);
            quest.questLocX = order5X;
            quest.questLocY = order5Y;
            quests.Add(quest);

            quest = new FetchQuest("WithOrder6", "Excavate", "Dig up an Shadite Ore pocket. Location pinned on the map", new List<int> { ItemID.HellstoneBar, 10 }, ItemID.HealingPotion, 10);
            quests.Add(quest);
            quest = new FetchQuest("WithOrder7", "Fortify", "Craft a Shadow weapon with the Shadite", new List<int> { ItemID.HellstoneBar, 10 }, ItemID.HealingPotion, 10);
            quests.Add(quest);
            quest = new DeliverQuest("WithOrder8", "Transport", "Deliver the object", new List<int> { ItemID.HellstoneBar, 10 }, NPCType<OrderQuest>(), ItemType<OrderMessage>(), x, y);
            quests.Add(quest);
            quest = new KillQuest("WithOrder9", "Train", "Fight the Order Cultist at your town.", new List<int> { ItemID.HellstoneBar, 10 }, NPCID.Bunny, 3);
            quests.Add(quest); 
            quest = new KillQuest("WithOrder10", "Free", "Defeat the Shadow Master", new List<int> { ItemID.HellstoneBar, 10 }, NPCID.Bunny, 3);
            quests.Add(quest);
            quest = new KillQuest("AgainstOrder1", "Demon Slaying", "Kill 15 demons", new List<int> { ItemID.HellstoneBar, 5 }, NPCID.Demon, 15);
            quests.Add(quest);
            quest = new DeliverQuest("AgainstOrder2", "Artifact Hunting", "Steal a valuable artifact from an Order Cultist. Location pinned on the map", new List<int> { ItemID.HellstoneBar, 10 }, NPCID.ArmsDealer, ItemType<OrderMessage>(), x, y);
            quests.Add(quest);

            quest = new FetchQuest("Guide1", "Sticky Situation", "Obtain 30 Gel", new List<int> { ItemID.Torch, 15 }, ItemID.Gel , 30);
            quest.npcDialogueInfo = "An important start to any person’s adventure is to collect enough gel for torches. Collect 30 gel from killing slimes and bring it back to me.";
            quest.npcDialogueComplete = "Good job, these torches should help you in your explorations.";
            quests.Add(quest);
            quest = new FetchQuest("Guide2", "Slice n Dice", "Get a new weapon", new List<int> { ItemID.IronBar, 8 }, 0, 1, false);
            quest.npcDialogueInfo = "It's dangerous to go alone. A copper shortsword wont suffice for much longer. An upgrade is required as soon as possible.";
            quest.npcDialogueComplete = "That looks dangerous, impressive stuff! I gathered some materials to hopefully assist you in future.";
            quests.Add(quest);
            quest = new PlaceQuest("Guide3", "Forge", "Place a furnace and an anvil", new List<int> { ItemID.GoldBar, 8 }, new List<int> { TileID.Furnaces, TileID.Anvils }, new List<int> { 1, 1 }, false, new List<string> { "Terraria/Item_" + ItemID.Furnace, "Terraria/Item_" + ItemID.IronAnvil });
            quest.npcDialogueInfo = "It's time you learn to craft some new weapons with the metal you find. Create a simple forge with a furnace and an anvil.";
            quest.npcDialogueComplete = "Hot stuff! See what you can create with these.";
            quests.Add(quest); 
            quest = new FetchQuest("Guide4", "Getting An Upgrade", "Craft a new pickaxe", new List<int> { ItemID.Rope, 120 }, 0, 1, false);
            quest.npcDialogueInfo = "You wont be getting that much done with that basic pickaxe. Forge an upgrade at your new anvil allowing you to mine harder materials";
            quest.npcDialogueComplete = "Excellent, mining expeditions should be much more successful with your new pickaxe.";
            quests.Add(quest);
            quest = new GoToQuest("Guide5", "The Final Frontier", "Reach space", new List<int> { ItemID.WoodPlatform, 60 }, 0, 0, 0);
            quest.npcDialogueInfo = "Have you seen space before? It is beautiful, looking down upon our small town. However, watch out for the harpies that inhabit the sky.";
            quest.npcDialogueComplete = "You're back! Glad to see you made it back in one piece. Keep an eye out for the sky islands that can be seen up there.";
            quests.Add(quest);
            quest = new GoToQuest("Guide6", "The Below", "Reach the Underworld", new List<int> { ItemID.ObsidianSkinPotion, 1 }, 0, 0, 0);
            quest.npcDialogueInfo = "Deep down below lies a cavity filled with lava and hellish creatures. Through all the danger, the materials found there match no other found outside. You should investigate.";
            quest.npcDialogueComplete = "I can tell you visited the underworld by the ash covering your face. I hope it wasnt too hot down there. Make sure you grab a Hell Forge next time you go down there.";
            quests.Add(quest); 
            quest = new BoolQuest("Guide7", "Sacrifices", "Kill the Wall of Flesh", new List<int> { ItemID.HellstoneBar, 8 }, () => Main.hardMode);
            quest.npcDialogueInfo = "When you are ready to challenge the keeper of the underworld, you will have to make a living sacrifice. Everything you need for it can be found in the underworld.";
            quest.npcDialogueComplete = "Defeating that monsterous creation must have been no easy feat. Well done, even if you had to sacrifice one of my fellow Guides! Here, take these.";
            quests.Add(quest);
            quest = new DestroyQuest("Guide8", "Demon Buster", "Destroy 3 Demon Altars", new List<int> { ItemID.CursedFlame, 8 }, TileID.DemonAltar, 3, false, WorldGen.crimson ? "ElementsAwoken/Extra/Quests/CrimsonAltar" : "ElementsAwoken/Extra/Quests/DemonAltar");
            quest.npcDialogueInfo = "I'm sure you have seen those mysterious altars littering the world. With your new Pwnhammer, you should be able to break them. See if you can destroy 3 of these altars.";
            quest.npcDialogueComplete = "Perfect! Be careful of the wraiths that appear when you break the altars.";
            quests.Add(quest);
            quest = new FetchQuest("Guide9", "Mining Expedition", "Gather 10 Mythril or Orichalcum bars and give them to the Guide", new List<int> { ItemID.CursedTorch, 30 }, ItemID.MythrilBar, 10);
            quest.npcDialogueInfo = "Turn's out the dark energy within the altars blessed the world with new and strong ores. Bring me some of these refined ores so I can learn more about them.";
            quest.npcDialogueComplete = "Interesting, these metals have very unique properties. I'd like to do some more research on these.";
            quests.Add(quest);
            quest = new FetchQuest("Guide10", "Soul Collector", "Collect 10 Souls of Night and 10 Souls of Light", new List<int> { ItemID.GreaterHealingPotion, 10 }, new List<int> { ItemID.SoulofNight, ItemID.SoulofLight }, new List<int> { 10, 10 }, false);
            quests.Add(quest);
            quest = new FetchQuest("Guide11", "Soaring Through Space", "Get Wings", new List<int> { ItemID.MechanicalEye, 10 }, 0, 1, false);
            quests.Add(quest);
            quest = new BoolQuest("Guide12", "The Machines", "Kill The Twins, Skeletron Prime and The Destroyer", new List<int> { ItemID.HallowedBar, 12 }, () => (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3));
            quests.Add(quest);
            quest = new FetchQuest("Guide13", "Living Stone", "Collect 50 Chlorophyte ore", new List<int> { ItemID.ChlorophyteBar, 10 }, ItemID.ChlorophyteOre, 50, false);
            quests.Add(quest);
            quest = new BoolQuest("Guide14", "Overgrowth", "Kill Plantera", new List<int> { ItemType<Items.Materials.InfinityCrys>(), 1 }, () => NPC.downedPlantBoss);
            quests.Add(quest);
            quest = new FetchQuest("Guide15", "Phantom Menace", "Collect 20 Ectoplasm", new List<int> { ItemID.SpectreBar, 10 }, ItemID.Ectoplasm, 20, false);
            quests.Add(quest);
            quest = new BoolQuest("Guide16", "Nightfall", "Kill The Moonlord", new List<int> { ItemID.LunarOre, 15, ItemType<Items.Materials.NeutronFragment>(), 6 }, () => NPC.downedMoonlord);
            quests.Add(quest);
            quest = new FetchQuest("Guide17", "Killer Heat", "Craft 5 Blightfire and give them to the Guide", new List<int> { ItemID.SpectreBar, 10 }, ItemType<Items.ItemSets.Blightfire.Blightfire>(), 5);
            quests.Add(quest);
            quest = new BoolQuest("Guide18", "Insanity", "Kill The Void Leviathan", new List<int> { ItemType< Items.Materials.VoiditeBar> (), 10 }, () => MyWorld.downedVoidLeviathan);
            quests.Add(quest);

            quest = new FetchQuest("Merchant1", "Dragonslayer", "Bring 12 Drakonite Shards and give them to the Merchant", new List<int> { ItemID.GoldCoin, 3 }, ItemType<Items.ItemSets.Drakonite.Regular.Drakonite>(), 12);
            quests.Add(quest);
            quest = new BoolQuest("Merchant2", "Evil Presence", "Kill The Eye of Cthulhu", new List<int> { ItemType<Items.Materials.LensFragment>(), 12, ItemID.GoldCoin, 5 }, () => NPC.downedBoss1);
            quests.Add(quest);
            quest = new FetchQuest("Merchant3", "Riches", "Accumulate 50 gold coins in your inventory", new List<int> { ItemID.HellstoneBar, 15 }, ItemID.GoldCoin, 50 , false);
            quests.Add(quest);
            quest = new SpecialQuest("Merchant4", "Bounty of the Skies", "Reach a sky island and open the chest inside", new List<int> { ItemID.SkyMill, 1 }, 0);
            quests.Add(quest);

            quest = new BoolQuest("Nurse1", "Hearty", "Reach 200 life", new List<int> { ItemID.HealingPotion, 5 }, () => Main.LocalPlayer.statLifeMax >= 200);
            quests.Add(quest);
            quest = new BoolQuest("Nurse2", "Pinky", "Kill the giant pinky that lives under the surface", new List<int> { ItemID.GreaterHealingPotion, 1, ItemID.GoldCoin, 2 }, () => MyWorld.downedGiantPinky);
            quests.Add(quest);
            quest = new FetchQuest("Nurse3", "Medicine", "Craft 5 Salve", new List<int> { ItemID.LifeCrystal, 2 }, ItemType<Items.Consumable.Medicine.Salve>(), 5 ,false);
            quests.Add(quest);
            quest = new FetchQuest("Nurse4", "Restock", "Bring the nurse 20 healing potions", new List<int> { ItemID.LifeCrystal, 2 }, ItemID.HealingPotion, 20);
            quests.Add(quest); 
            quest = new FetchQuest("Nurse5", "Meals", "Obtain 3 different food items that give Well Fed", new List<int> { ItemID.LifeCrystal, 2 }, 0, 3, false);
            quests.Add(quest);
            quest = new FetchQuest("Nurse6", "Sweet Fruits", "Obtain a life fruit", new List<int> { ItemID.LifeFruit, 1 }, ItemID.LifeFruit, 1, false);
            quests.Add(quest);

            quest = new SpecialQuest("CureVoidbroken", "The Cure", "Find an Invigoration Fountain, fill bottles with the water and throw them at the Voidbroken to cure them.", new List<int> { ItemID.GoldCoin, 20 }, 10);
            quest.npcDialogueInfo = "In the depths of hell lies a curse- a sickness taking over the inhabitants. Water from an Invigoration Fountain is able to cure them. If you are able to cure 10 of these creatures, I will have a reward waiting.";
            quest.questIcon = Main.npcHeadTexture[18];
            quests.Add(quest);
            quest = new SpecialQuest("FallenHarpy", "For The Fallen", "Bring this fallen harpy back to the nurse to get healed.", new List<int> { ItemID.GoldCoin, 10, ItemID.GiantHarpyFeather, 1, ItemID.Feather, 15 }, 1);
            quest.npcDialogueInfo = "Thank you for helping me!";
            quests.Add(quest);
        }
        public override TagCompound Save()
        {
            TagCompound tag = new TagCompound();

            for (int i = 0; i < loadedQuests; i++)
            {
                tag["activeQ" + quests[i].identifier] = quests[i].active;
                tag["claimedQ" + quests[i].identifier] = quests[i].claimed;
                tag["completedQ" + quests[i].identifier] = quests[i].completed;
                if (quests[i] is KillQuest quest)
                {
                    tag["killsGotQ" + quests[i].identifier] = quest.killsGotten;
                    /*for (int k = 0; k < quest.killsGotten.Count; k++)
                    {
                        tag["killsGotQ" + k + quests[i].identifier] = quest.killsGotten[k];
                    }*/
                }
                else if (quests[i] is PlaceQuest quest2)
                {
                    tag["placesQ" + quests[i].identifier] = quest2.tilesPlaced;
                    /*for (int k = 0; k < quest2.tilesPlaced.Count; k++)
                    {
                        tag["placesQ" + k + quests[i].identifier] = quest2.tilesPlaced[k];
                    }*/
                }
                else if (quests[i] is DestroyQuest quest3)
                {
                    tag["destroysQ" + quests[i].identifier] = quest3.tilesDestroyed;
                    /*for (int k = 0; k < quest3.tilesDestroyed.Count; k++)
                    {
                        tag["destroysQ" + k + quests[i].identifier] = quest3.tilesDestroyed[k];
                    }*/
                }
            }

            tag["ritualSiteX"] = ritualSiteX;
            tag["ritualSiteY"] = ritualSiteY;
            return tag;
        }
        public override void Load(TagCompound tag)
        {
            for (int i = 0; i < loadedQuests; i++)
            {
                quests[i].active = tag.GetBool("activeQ" + quests[i].identifier);
                quests[i].claimed = tag.GetBool("claimedQ" + quests[i].identifier);
                quests[i].completed = tag.GetBool("completedQ" + quests[i].identifier);
                if (quests[i] is KillQuest quest)
                {
                    quest.killsGotten = (List<int>)tag.GetList<int>("killsGotQ" + quests[i].identifier);
                    /*for (int k = 0; k < quest.killsGotten.Count; k++)
                    {
                        quest.killsGotten[k] = tag.GetInt("killsGotQ" + k + quests[i].identifier);
                    }*/
                }
                else if (quests[i] is PlaceQuest quest2)
                {
                    quest2.tilesPlaced = (List<int>)tag.GetList<int>("placesQ" + quests[i].identifier);

                    /*for (int k = 0; k < quest2.tilesPlaced.Count; k++)
                    {
                        quest2.tilesPlaced[k] = tag.GetInt("placesQ" + k + quests[i].identifier);
                    }*/
                }
                else if (quests[i] is DestroyQuest quest3)
                {
                    quest3.tilesDestroyed = (List<int>)tag.GetList<int>("destroysQ" + quests[i].identifier);

                    /*for (int k = 0; k < quest3.tilesDestroyed.Count; k++)
                    {
                        quest3.tilesDestroyed[k] = tag.GetInt("destroysQ" + k + quests[i].identifier);
                    }*/
                }
            }

            ritualSiteX = tag.GetInt("ritualSiteX");
            ritualSiteY = tag.GetInt("ritualSiteY");
        }
        public override void PreUpdate()
        {
            activeQuests.Clear();
            foreach (Quest k in quests)
            {
                if (k.active)
                {
                    activeQuests.Add(k);
                    if (k is DeliverQuest quest2)
                    {
                        quest2.SpawnNPC();
                        quest2.UpdateLoc();
                    }
                    else if (k is KillQuest quest3)
                    {
                        quest3.SpawnNPC();
                        quest3.UpdateLoc();
                    }
                    if (k.identifier == "WithOrder2" && !k.completed)
                    {
                        if (ritualSiteX == 0)
                        {
                            ritualSiteX = Main.maxTilesX / 2 + Main.rand.Next(-900, 900);
                            ritualSiteY = (int)(Main.maxTilesY * 0.4f) + Main.rand.Next(20, (int)(Main.maxTilesY * 0.3f));
                            Structures.OrderStructures.GenRitualSite(ritualSiteX, ritualSiteY);
                        }
                        else
                        {
                            k.questLocX = ritualSiteX + 13;
                            k.questLocY = ritualSiteY + 13;
                            Quest.SpawnNPCAt(ritualSiteX + 14, ritualSiteY + 9, NPCType<ShadowDemon>());
                        }
                    }
                    if (k.identifier == "Nurse2")
                    {
                        if (EAWorldGen.pinkyCaveLoc.X != 0)
                        {
                            k.questLocX = EAWorldGen.pinkyCaveLoc.X + 15;
                            k.questLocY = EAWorldGen.pinkyCaveLoc.Y + 11;
                        }
                    }
                }
            }
        }
    }
    public class QuestTile : GlobalTile
    {
        public override void PlaceInWorld(int i, int j, Item item)
        {
            if (!Main.gameMenu)
            {
                foreach (Quest k in QuestWorld.quests)
                {
                    if (k is PlaceQuest quest)
                    {
                        for (int e = 0; e < quest.tilesPlaced.Count; e++)
                        {
                            if (Framing.GetTileSafely(i, j).type == quest.tileType[e])
                            {
                                quest.tilesPlaced[e]++;
                                if (quest.active && !quest.completed)
                                {
                                    if (quest.CheckDone(Main.LocalPlayer))
                                    {
                                        QuestSystem.CompleteQuest(k, Main.LocalPlayer, mod);
                                    }
                                }
                            }
                        }
                    }
                    if (k is DestroyQuest quest2)
                    {
                        for (int e = 0; e < quest2.tilesDestroyed.Count; e++)
                        {
                            if (Framing.GetTileSafely(i, j).type == quest2.tileType[e] && quest2.subtractWhenPlaced)
                            {
                                quest2.tilesDestroyed[e]--;
                                if (quest2.active && !quest2.completed)
                                {
                                    if (quest2.CheckDone(Main.LocalPlayer))
                                    {
                                        QuestSystem.CompleteQuest(k, Main.LocalPlayer, mod);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!Main.gameMenu)
            {
                if (!effectOnly && !fail && Main.tile[i,j].frameX == 0 && Main.tile[i, j].frameY == 0)
                {
                    foreach (Quest k in QuestWorld.quests)
                    {
                        if (k is PlaceQuest quest)
                        {
                            if (quest.active && !quest.completed && quest.subtractWhenDestroyed)
                            {
                                for (int e = 0; e < quest.tilesPlaced.Count; e++)
                                {
                                    if (Framing.GetTileSafely(i, j).type == quest.tileType[e])
                                    {
                                        quest.tilesPlaced[e]--;
                                    }
                                }
                            }
                        }
                        else if (k is DestroyQuest quest2)
                        {
                            if (quest2.active && !quest2.completed)
                            {
                                for (int e = 0; e < quest2.tilesDestroyed.Count; e++)
                                {
                                    if (Framing.GetTileSafely(i, j).type == quest2.tileType[e])
                                    {
                                        quest2.tilesDestroyed[e]++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public abstract class Quest
    {
        public string questName;
        public string identifier;
        public string description;
        public List<int> itemRewards = new List<int>();
        public bool completed = false;
        public bool claimed = false;
        public bool active = false;
        public Texture2D questIcon = null;
        public string npcDialogueInfo;
        public string npcDialogueComplete;

        public float questLocX = 0;
        public float questLocY = 0;
        public Quest(string ingameName, string name, string desc, List<int> rewards)
        {
            identifier = ingameName;
            questName = name;
            description = desc;
            itemRewards = rewards;
            QuestWorld.loadedQuests++;
        }
        public abstract bool CheckDone(Player player, bool noEffect = false);
        public virtual void Complete(Player player)
        {
            for (int p = 0; p < Main.maxPlayers; p++)
            {
                player = Main.player[p];
                if (player.active)
                {
                    for (int i = 0; i < itemRewards.Count; i += 2)
                    {
                        if (itemRewards[i] == ItemID.IronBar && WorldGen.IronTierOre != TileID.Iron) itemRewards[i] = ItemID.LeadBar;
                        else if (itemRewards[i] == ItemID.GoldBar && WorldGen.GoldTierOre != TileID.Gold) itemRewards[i] = ItemID.PlatinumBar;
                        else if (itemRewards[i] == ItemID.CobaltBar && WorldGen.oreTier1 != TileID.Cobalt) itemRewards[i] = ItemID.PalladiumBar;
                        else if (itemRewards[i] == ItemID.CursedTorch && WorldGen.crimson) itemRewards[i] = ItemID.IchorTorch;
                        player.QuickSpawnItem(itemRewards[i], itemRewards[i + 1]);
                    }
                    if (identifier == "Merchant4")
                    {
                        int torchItem = Main.rand.Next(new int[] { ItemID.ShinyRedBalloon, ItemID.Starfury, ItemID.LuckyHorseshoe });
                        player.QuickSpawnItem(torchItem, 1);
                    }
                }
            }
            active = false;
            claimed = true;
        }
        public void DrawOnMap()
        {
            if (questLocX == 0 || completed) return;
            float mapX = Main.mapFullscreenPos.X;
            float mapY = Main.mapFullscreenPos.Y;
            mapX *= Main.mapFullscreenScale;
            mapY *= Main.mapFullscreenScale;
            float num = -mapX + (float)(Main.screenWidth / 2);
            float num2 = -mapY + (float)(Main.screenHeight / 2);

            Vector2 targetPos = Vector2.Zero;
            bool draw = true;
            targetPos = new Vector2(questLocX, questLocY);
            if (draw)
            {
                float drawX = targetPos.X * Main.mapFullscreenScale;
                float drawY = targetPos.Y * Main.mapFullscreenScale;
                drawX += num;
                drawY += num2;
                Texture2D tex = GetTexture("ElementsAwoken/Extra/PinIcon");
                Main.spriteBatch.Draw(tex, new Vector2(drawX, drawY), new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), Color.White, 0f, new Vector2((float)(tex.Width / 2), (float)(tex.Height / 2)), Main.UIScale, SpriteEffects.None, 0f);
                DrawMethods.DrawStringOutlined(Main.spriteBatch, Main.fontMouseText, questName, new Vector2(drawX + 6, drawY + 6), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 1f, 2);
            }
        }
        public static void SpawnNPCAt(int x, int y, int npcType)
        {
            Vector2 tile = new Vector2(x, y);
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Vector2 playerTile = Main.LocalPlayer.position / 16;
                if (!NPC.AnyNPCs(npcType))
                {
                    if (Vector2.Distance(playerTile, tile) < 200)
                    {
                        int n = NPC.NewNPC((int)tile.X * 16, (int)tile.Y * 16, npcType);
                        if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
                    }
                }
            }
            else
            {
                for (int p = 0; p < Main.maxPlayers; p++)
                {
                    if (Main.player[p].active && !Main.player[p].dead)
                    {
                        Vector2 playerTile = Main.player[p].position / 16;
                        if (!NPC.AnyNPCs(npcType))
                        {
                            if (Vector2.Distance(playerTile, tile) < 200)
                            {
                                int n = NPC.NewNPC((int)tile.X, (int)tile.Y, npcType);
                                if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
                            }
                        }
                    }
                }
            }
        }
    }
    public class FetchQuest : Quest
    {
        public List<int> itemType = new List<int>();
        public List<int> itemQuantity = new List<int>();

        public bool consume = false;
        public FetchQuest(string ingameName, string name, string desc, List<int> rewards, List<int> itemIDs, List<int> itemAmounts, bool removeItems = true) : base(ingameName, name, desc, rewards)
        {
            itemType = itemIDs;
            itemQuantity = itemAmounts;
            consume = removeItems;
        }
        public FetchQuest(string ingameName, string name, string desc, List<int> rewards, int itemID, int itemAmount = 1, bool removeItems = true) : base(ingameName, name, desc, rewards)
        {
            itemType = new List<int> { itemID };
            itemQuantity = new List<int> { itemAmount };
            consume = removeItems;
        }

        public override bool CheckDone(Player player, bool noEffect = false)
        {
            bool done = true;
            if (identifier == "Guide11")
            {
                done = player.wingTimeMax > 0;
            }
            else if (identifier == "Guide2" || identifier == "Guide4" || identifier == "Nurse5")
            {
                done = false;
                int num = 0;
                List<int> foodTypes = new List<int> { };
                for (int num2 = 0; num2 != 58; num2++)
                {
                    Item item = player.inventory[num2];
                    bool valid = false;
                    if (identifier == "Guide2") valid = item.damage > 0 && item.axe == 0 && item.pick == 0 && item.hammer == 0 && item.type != ItemID.CopperShortsword;
                    else if (identifier == "Guide4") valid = item.pick > 35;
                    else if(identifier == "Nurse5") valid = item.buffType == BuffID.WellFed;
                    if (identifier == "Nurse5")
                    {
                        if (item.stack > 0 && valid && !foodTypes.Contains(item.type))
                        {
                            num += 1;
                            foodTypes.Add(item.type);
                            if (num >= itemQuantity[0])
                            {
                                done = true;
                            }
                        }
                    }
                    else
                    {
                        if (item.stack > 0 && valid)
                        {
                            num += player.inventory[num2].stack;
                            if (num >= itemQuantity[0])
                            {
                                done = true;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int k = 0; k < itemType.Count; k++)
                {
                    int type = itemType[k];
                    if (type == ItemID.MythrilBar && WorldGen.oreTier2 != TileID.Mythril) type = ItemID.OrichalcumBar;
                    if (player.CountItem(type, itemQuantity[k]) < itemQuantity[k])
                    {
                        done = false;
                        break;
                    }
                }
            }
            return done;
        }
        public override void Complete(Player player)
        {
            base.Complete(player);
            if (CheckDone(player) && consume)
            {
                foreach (Item item in player.inventory)
                {
                    for (int k = 0; k < itemType.Count; k++)
                    {
                        bool validItem = item.type == itemType[k];
                        if (itemType[k] == ItemID.MythrilBar && WorldGen.oreTier2 != TileID.Mythril) validItem = item.type == ItemID.OrichalcumBar;
                        if (validItem)
                        {
                            item.stack -= itemQuantity[k];
                            if (item.stack <= 0) item.TurnToAir();
                        }
                    }
                }
            }
        }
    }
    public class KillQuest : Quest
    {
        public List<int> killsGotten = new List<int>();
        public List<int> killsNeeded = new List<int>();
        public List<int> targetType = new List<int>();

        public bool needsNewNPC = false;
        public KillQuest(string ingameName, string name, string desc, List<int> rewards, int targetID, int kills = 1, bool spawnNPC = false) : base(ingameName, name, desc, rewards)
        {
            targetType = new List<int> { targetID };
            killsNeeded = new List<int> { kills };
            needsNewNPC = spawnNPC;
            killsGotten.Add(0);
        }
        public KillQuest(string ingameName, string name, string desc, List<int> rewards, List<int> targetID, List<int> kills) : base(ingameName, name, desc, rewards)
        {
            targetType = targetID;
            killsNeeded = kills;
            needsNewNPC = false; // not compatible
            for (int k = 0; k < killsNeeded.Count; k++)
            {
                killsGotten.Add(0);
            }
        }
        public override bool CheckDone(Player player, bool noEffect = false)
        {
            bool done = true;
            for (int k = 0; k < killsNeeded.Count; k++)
            {
                if (killsGotten[k] < killsNeeded[k])
                {
                    done = false;
                }
            }
            return done;
        }
        public void SpawnNPC()
        {
            if (needsNewNPC && !completed)
            {
                SpawnNPCAt((int)questLocX, (int)questLocY, targetType[0]);
            }
        }
        public void UpdateLoc()
        {
            if (needsNewNPC)
            {
                int npc = NPC.FindFirstNPC(targetType[0]);
                if (npc >= 0)
                {
                    questLocX = Main.npc[npc].Center.X / 16f;
                    questLocY = Main.npc[npc].Center.Y / 16f;
                }
            }
        }
    }
    public class BoolQuest : Quest
    {
        public List<Func<bool>> boolFunc = new List<Func<bool>>();
        public BoolQuest(string ingameName, string name, string desc, List<int> rewards, Func<bool> downedVariable) : base(ingameName, name, desc, rewards)
        {
            boolFunc = new List<Func<bool>> { downedVariable };
        }
        public BoolQuest(string ingameName, string name, string desc, List<int> rewards, List<Func<bool>> downedVariable) : base(ingameName, name, desc, rewards)
        {
            boolFunc = downedVariable;
        }
        public override bool CheckDone(Player player, bool noEffect = false)
        {
            bool done = true;
            for (int k = 0; k < boolFunc.Count; k++)
            {
                if (!boolFunc[k]()) done = false;
            }
            return done;
        }
    }
    public class PlaceQuest : Quest
    {
        public List<int> tileType = new List<int>();
        public List<int> tilesNeeded = new List<int>();
        public List<int> tilesPlaced = new List<int>();
        public List<string> tileTextures = new List<string>();
        public bool subtractWhenDestroyed;
        public PlaceQuest(string ingameName, string name, string desc, List<int> rewards, List<int> tileID, List<int> placesRequired, bool removeWhenDestroyed = false, List<string> textures = null) : base(ingameName, name, desc, rewards)
        {
            tilesNeeded = placesRequired;
            tileType = tileID;
            subtractWhenDestroyed = removeWhenDestroyed;
            if (textures != null) tileTextures = textures;
            for (int k = 0; k < tilesNeeded.Count; k++)
            {
                tilesPlaced.Add(0);
            }
        }
        public PlaceQuest(string ingameName, string name, string desc, List<int> rewards, int tileID, int placesRequired, bool removeWhenDestroyed = false, string texture = "") : base(ingameName, name, desc, rewards)
        {
            tilesNeeded = new List<int>() { placesRequired };
            tileType = new List<int>() { tileID };
            tileTextures = new List<string>() { texture };
            subtractWhenDestroyed = removeWhenDestroyed;
            tilesPlaced.Add(0);
        }
        public override bool CheckDone(Player player, bool noEffect = false)
        {
            bool completed = true;
            for (int k = 0; k < tilesPlaced.Count; k++)
            {
                if (tilesPlaced[k] < tilesNeeded[k])
                {
                    completed = false;
                    break;
                }
            }
            return completed;
        }
    }
    public class DestroyQuest : Quest
    {
        public List<int> tileType = new List<int>();
        public List<int> tilesNeeded = new List<int>();
        public List<int> tilesDestroyed = new List<int>();
        public List<string> tileTextures = new List<string>();
        public bool subtractWhenPlaced;
        public DestroyQuest(string ingameName, string name, string desc, List<int> rewards, List<int> tileID, List<int> destroysRequired, bool removeWhenPlaced = false, List<string> textures = null) : base(ingameName, name, desc, rewards)
        {
            tilesNeeded = destroysRequired;
            tileType = tileID;
            subtractWhenPlaced = removeWhenPlaced;
            if (textures != null) tileTextures = textures;
            for (int k = 0; k < tilesNeeded.Count; k++)
            {
                tilesDestroyed.Add(0);
            }
        }
        public DestroyQuest(string ingameName, string name, string desc, List<int> rewards, int tileID, int destroysRequired, bool removeWhenPlaced = false, string texture = "") : base(ingameName, name, desc, rewards)
        {
            tilesNeeded = new List<int>() { destroysRequired };
            tileType = new List<int>() { tileID };
            tileTextures = new List<string>() { texture };
            subtractWhenPlaced = removeWhenPlaced;
            tilesDestroyed.Add(0);
        }
        public override bool CheckDone(Player player, bool noEffect = false)
        {
            bool completed = true;
            for (int k = 0; k < tilesDestroyed.Count; k++)
            {
                if (tilesDestroyed[k] < tilesNeeded[k])
                {
                    completed = false;
                    break;
                }
            }
            return completed;
        }
    }
    public class GoToQuest : Quest
    {
        public int tileX;
        public int tileY;
        public int maxDist;
        public GoToQuest(string ingameName, string name, string desc, List<int> rewards, int x, int y, int triggerRange) : base(ingameName, name, desc, rewards)
        {
            tileX = x;
            tileY = y;
            maxDist = triggerRange;
        }

        public override bool CheckDone(Player player, bool noEffect = false)
        {
            bool done = false;
            if (identifier == "Guide5") done = player.ZoneSkyHeight;
            else if (identifier == "Guide6") done = player.ZoneUnderworldHeight;
            else done = Vector2.Distance(new Vector2(tileX, tileY) * 16, player.Center) < maxDist;
            return done;
        }
    }
    public class DeliverQuest : Quest
    {
        public int npcType;
        public int npcX;
        public int npcY;
        public int itemType;
        public int amount;
        public bool needsNewNPC = false;
        public DeliverQuest(string ingameName, string name, string desc, List<int> rewards, int npcTypeToDeliver, int itemID, int numberToDeliver = 1, int tileX = 0, int tileY = 0, bool spawnNPC = false) : base(ingameName, name, desc, rewards)
        {
            itemType = itemID;
            npcType = npcTypeToDeliver;
            amount = numberToDeliver;
            npcX = tileX;
            npcY = tileY;
            needsNewNPC = spawnNPC;
        }

        public override bool CheckDone(Player player, bool noEffect = false)
        {
            bool done = false;
            int itemWho = player.FindItem(itemType);
            if (player.talkNPC > -1)
            {
                if (itemWho >= amount && Main.npc[player.talkNPC].type == npcType)
                {
                    if (!noEffect)
                    {
                        Item item = player.inventory[itemWho];
                        item.stack -= amount;
                        if (item.stack <= 0) item.TurnToAir();
                    }
                    done = true;
                }
            }
            return done;
        }

        public void SpawnNPC()
        {
            if (needsNewNPC)
            {
                SpawnNPCAt(npcX,npcY,npcType);
            }
        }
        public void UpdateLoc()
        {
            if (!needsNewNPC)
            {
                int npc = NPC.FindFirstNPC(npcType);
                if (npc >= 0)
                {
                    questLocX = Main.npc[npc].Center.X / 16f;
                    questLocY = Main.npc[npc].Center.Y / 16f;
                }
            }
        }
    }
    public class SpecialQuest : Quest
    {
        public int thingsDone = 0;
        public int thingsNeeded = 0;
        public SpecialQuest(string ingameName, string name, string desc, List<int> rewards, int thingsNeededToDo, float questX = 0, float questY = 0) : base(ingameName, name, desc, rewards)
        {
            thingsNeeded = thingsNeededToDo;
            questLocX = questX;
            questLocY = questY;
        }

        public override bool CheckDone(Player player, bool noEffect = false)
        {
            return thingsDone >= thingsNeeded;
        }
    }
}