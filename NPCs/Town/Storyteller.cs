using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Town
{
    [AutoloadHead]
    public class Storyteller : ModNPC
    {
        public float shadowTimer = 0f;
        public float shadowAlpha = 0.25f;
        public int shadowDirection = 0;
        public int shadowAI = 0;
        public override bool Autoload(ref string name)
        {
            return mod.Properties.Autoload;
        }
        public override string Texture
        {
            get
            {
                return "ElementsAwoken/NPCs/Town/Storyteller";
            }
        }
        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;

            npc.width = 18;
            npc.height = 40;

            npc.aiStyle = 7;
            npc.damage = 10;
            npc.defense = 30;
            npc.lifeMax = 500;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 90;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
            animationType = NPCID.Merchant;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storyteller");
            Main.npcFrameCount[npc.type] = 25;
        }
        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            return true;
        }
        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            if (MyWorld.downedAncients)
            {
                return false;
            }
            return true;
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.type == ProjectileID.RottenEgg)
            {
                damage = 80 + Main.rand.Next(-2, 2);
                if (npc.life < npc.lifeMax / 5)
                {
                    npc.life = npc.lifeMax;
                    float projSpeed = 12f;
                    float rotation = (float)Math.Atan2(npc.Center.Y - Main.player[projectile.owner].Center.Y, npc.Center.X - Main.player[projectile.owner].Center.X);
                    Projectile attack = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * projSpeed) * -1), (float)((Math.Sin(rotation) * projSpeed) * -1) - 2f, mod.ProjectileType("CrystallineKunai"), 1200, 5f, 0)];
                    attack.friendly = false;
                    attack.hostile = true;
                }
            }
            base.ModifyHitByProjectile(projectile, ref damage, ref knockback, ref crit, ref hitDirection);
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            if (MyWorld.downedAzana)
            {
                Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
                for (int k = 0; k < 4; k++)
                {
                    Vector2 drawPos = npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, -2);
                    float multipler = 1;
                    if (k == 1 || k == 3)
                    {
                        multipler = 1.5f;
                    }
                    int switchDir = 1;
                    if (k == 0 || k == 2)
                    {
                        switchDir = -1;
                    }
                    drawPos.X += shadowTimer * multipler * switchDir;
                    Color color = npc.GetAlpha(lightColor) * shadowAlpha;
                    Texture2D texture = mod.GetTexture("NPCs/Town/StorytellerShadow" + k);
                    SpriteEffects effects = npc.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                    sb.Draw(texture, drawPos, null, color, npc.rotation, drawOrigin, npc.scale, effects, 0f);
                }
            }
            return true;
        }
        public override void AI()
        {
            if (MyWorld.downedAzana)
            {
                if (shadowDirection == 0)
                {
                    shadowTimer -= 1;
                }
                else
                {
                    shadowTimer += 1;
                }
                if (shadowTimer <= -10)
                {
                    shadowDirection = 1;
                }
                else if (shadowTimer >= 10)
                {
                    shadowDirection = 0;
                }

                shadowAI--;
                if (shadowAI < 300)
                {
                    shadowAlpha += 0.005f;
                    if (shadowAlpha > 0.25f)
                    {
                        shadowAlpha = 0.25f;
                    }
                }
                else
                {
                    shadowAlpha -= 0.005f;
                    if (shadowAlpha < 0f)
                    {
                        shadowAlpha = 0f;
                    }
                }
                if (shadowAI <= 0)
                {
                    shadowAI = Main.rand.Next(300, 900);
                }
            }
            else
            {
                shadowAlpha = 0f;
            }
        }
        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(4))
            {
                case 0:
                    return "Neivirk"; // Kirvein anagram
                case 1:
                    return "Herckeus"; // Krecheus anagram 
                case 2:
                    return "Nornex"; // xernon anagram
                case 3:
                    return "Zairis"; // Izaris anagram
                default:
                    return "default";
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 20;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 2;
            randExtraCooldown = 20;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = mod.ProjectileType("CrystallineKunai");
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 15f;
            randomOffset = 2f;
        }
        public string buttonText = "";
        public int buttonMode = 0;
        public int buttonPressed = 0;
        public override void SetChatButtons(ref string button, ref string button2)
        {
            if (buttonMode == 0)
            {
                if (!MyWorld.downedAzana)
                {
                    buttonText = "Story";
                }
                else
                {
                    buttonText = "Question";
                    buttonPressed = 0;
                }
            }
            if (buttonMode == 1)
            {
                buttonText = "Fight";
                buttonPressed = 1;
            }
            button = buttonText;
            if (NPC.downedBoss1)
            {
                button2 = Language.GetTextValue("LegacyInterface.28"); ;
            }
        }
        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            Player player = Main.player[Main.myPlayer];
            if (firstButton)
            {
                if (buttonMode == 0)
                {
                    if (!MyWorld.downedAzana)
                    {
                        #region story
                        bool toySlime = MyWorld.downedToySlime;
                        bool kingSlime = NPC.downedSlimeKing;
                        bool eyeOfCthulhu = NPC.downedBoss1;
                        bool wasteland = MyWorld.downedWasteland;
                        bool eaterOrBrain = NPC.downedBoss2;
                        bool queenBee = NPC.downedQueenBee;
                        bool skeletron = NPC.downedBoss3;
                        bool infernace = MyWorld.downedInfernace;
                        bool wallOfFlesh = Main.hardMode;
                        bool destroyer = NPC.downedMechBoss1;
                        bool twins = NPC.downedMechBoss2;
                        bool skeletronPrime = NPC.downedMechBoss3;
                        bool scourgeFighter = MyWorld.downedScourgeFighter;
                        bool regaroth = MyWorld.downedRegaroth;
                        bool plantera = NPC.downedPlantBoss;
                        bool golem = NPC.downedGolemBoss;
                        bool permafrost = MyWorld.downedPermafrost;
                        bool celestial = MyWorld.downedCelestial;
                        bool obsidious = MyWorld.downedObsidious;
                        bool dukeFishron = NPC.downedFishron;
                        bool aqueous = MyWorld.downedAqueous;
                        bool theGuardian = MyWorld.downedGuardian;
                        bool cultist = NPC.downedAncientCultist;
                        bool moonLord = NPC.downedMoonlord;
                        bool volcanox = MyWorld.downedVolcanox;
                        bool voidLeviathan = MyWorld.downedVoidLeviathan;
                        bool azana = MyWorld.downedAzana;

                        if (!toySlime)
                        {
                            Main.npcChatText = "Back then, when the dreaded frost moon rose, a slime filled with toys has happened to appear. A 'Large Slimeball' is enough to summon it.";
                            Main.npcChatCornerItem = mod.ItemType("ToySlimeSummon");
                        }
                        else if (!kingSlime)
                        {
                            Main.npcChatText = "The king of the slimes roams the fields with it's followers, causing earthquakes. A 'Slime Crown' may provoke him.";
                            Main.npcChatCornerItem = ItemID.SlimeCrown;
                        }
                        else if (!eyeOfCthulhu)
                        {
                            Main.npcChatText = "A giant eyeball is terrorizing the night, which hides sharp teeth underneath his lens. You found this 'Suspicious Looking Eye'...maybe it'll be just perfect for the eyeball to appear.";
                            Main.npcChatCornerItem = ItemID.SuspiciousLookingEye;
                        }
                        else if (!wasteland)
                        {
                            Main.npcChatText = "Dust is spreading out within the desert, storming. Scorpions suddenly arise from the sands. No doubt, it's Wasteland hunting it's next victim. Catch one of the 'Mutated Scorpions' with a net to get his attention.";
                            Main.npcChatCornerItem = mod.ItemType("WastelandSummon");
                        }
                        else if (!eaterOrBrain)
                        {
                            if(WorldGen.crimson)
                            {
                                Main.npcChatText = "A loud roar halled through the dead lands of the Crimson. It's guardian roams free again, it seems. Destroy three of these 'Crimson Hearts' within the caves. Alternatively, provoke it with a 'Bloody Spine'.";
                                Main.npcChatCornerItem = ItemID.BloodySpine;
                            }
                            else
                            {
                                Main.npcChatText = "A loud roar halled through the dead lands of the Corruption. It's guardian roams free again, it seems. Destroy three of these 'Shadow Orbs' within the caves. Alternatively, provoke it with some 'Worm Food'";
                                Main.npcChatCornerItem = ItemID.WormFood;
                            }
                        }
                        else if (!queenBee)
                        {
                            Main.npcChatText = "The queen of all bees buzzes through the jungle, trying to build it's hives at every corner. Smash one of the hive's larva or use an 'Abeemination' to put an end to that madness.";
                            Main.npcChatCornerItem = ItemID.Abeemination;
                        }
                        else if (!skeletron)
                        {
                            Main.npcChatText = "An ancient curse has befallen the old man near the dungeon. This curse will cause a giant skull to emerge. Kill him with a 'Clothier Voodoo Doll' or visit the halls of the dungeon to summon it.";
                            Main.npcChatCornerItem = ItemID.ClothierVoodooDoll;
                        }
                        else if (!infernace)
                        {
                            Main.npcChatText = "After the skull has fallen, ash and fire rains down upon the lands. The fire lord Infernace seems to be enraged. Create a 'Fire Core' and use it within the underworld to get him to show up.";
                            Main.npcChatCornerItem = mod.ItemType("InfernaceSummon");
                        }
                        else if (!wallOfFlesh)
                        {
                            Main.npcChatText = "I've heared that the guide is fully aware of his fate and deems you challenging to fight the underworld's master. Heh, what a fool. Little does he know what it's defeat will bring over the world...";
                            Main.npcChatCornerItem = ItemID.GuideVoodooDoll;
                        }
                        else if (!twins)
                        {
                            Main.npcChatText = "Do you remember that giant eyeball? Some group of scientists created an mechanical copy of him - and this time, there are two of them. Use a 'Mechanical Eye'. As it's similiar to the eye you've made back then, it may cause them to appear.";
                            Main.npcChatCornerItem = ItemID.MechanicalEye;
                        }
                        else if (!destroyer)
                        {
                            Main.npcChatText = "You feel those earthquakes? They are way worse than the ones of the King Slime back then. Create an 'Mechanical Worm' and use it, maybe the source of the earthquake will show up then.";
                            Main.npcChatCornerItem = ItemID.MechanicalWorm;
                        }
                        else if (!skeletronPrime)
                        {
                            Main.npcChatText = "The skull has been reborn as a four armed mechanoid. And he causes more havoc than ever before. A 'Mechanical Skull' will cause it to appear.";
                            Main.npcChatCornerItem = ItemID.MechanicalSkull;
                        }
                        else if (!scourgeFighter)
                        {
                            Main.npcChatText = "Those scientists, which created the mechanical trio, also created a jet robot called the Scourge Fighter. It has an AI to destroy everything. You can create a duplicate of his 'Scourge Remote' to signal it your coordinates";
                            Main.npcChatCornerItem = mod.ItemType("ScourgeFighterSummon");
                        }
                        else if (!regaroth)
                        {
                            Main.npcChatText = "The mechanical trio went rogue after shocking thunders hit them. It was caused by the cosmic thunder serpent, Regaroth. If you are able to get ahold of a piece of his 'Swirling Energies', he may come to reclaim it. Prepare for the worst.";
                            Main.npcChatCornerItem = mod.ItemType("RegarothSummon");
                        }
                        else if (!plantera)
                        {
                            Main.npcChatText = "Flowers are always pretty, however, not when they're giant and have tentacles and spikes. I'm talking about the jungle guardian Plantera. Destroy her bulb to awaken her.";
                        }
                        else if (!golem)
                        {
                            Main.npcChatText = "The Golem is a ancient automaton within the depths of the jungle temple. Right now it's deactivated, but you can reactivate it by using one of it's 'Lihzahrd Power Cells' at an Lihzahrd Altar.";
                            Main.npcChatCornerItem = ItemID.LihzahrdPowerCell;
                        }
                        else if (!permafrost)
                        {
                            Main.npcChatText = "The once friendly ice wizard Permafrost returned as a hatred filled spirit, taking revenge upon those who forgot him. An 'Ancient Ice Crystal' will call upon him.";
                            Main.npcChatCornerItem = mod.ItemType("PermafrostSummon");
                        }
                        else if (!celestial)
                        {
                            Main.npcChatText = "The celestial forces once created a weaker amalgamation of their powers. That amalgamation sees a lot of things as a threat, so you better get a 'Stone of the Stars' and take it's sight in return.";
                            Main.npcChatCornerItem = mod.ItemType("CelestialSummon");
                        }
                        else if (!obsidious)
                        {
                            Main.npcChatText = "Forbidden artifact only cause calamitous things, yet some do not want to listen. The resulted, obsidian-plated monster, Obsidious, can be summoned with a 'Ulticore.'";
                            Main.npcChatCornerItem = mod.ItemType("ObsidiousSummon");
                        }
                        else if (!dukeFishron)
                        {
                            Main.npcChatText = "The king of the ocean, Duke Fishron recently scared me when I was looking for some fresh fish to have a nice dinner. He nearly devoured one of my arms. He is highly dangerous, so it's better to catch him with a 'Truffle Worm' and get rid of him.";
                            Main.npcChatCornerItem = ItemID.TruffleWorm;
                        }
                        else if (!aqueous)
                        {
                            Main.npcChatText = "No bad action remains unpunished. The knight of the ocean has come to take your life for taking Fishron's. Get ready for a tough battle. The knight will be further provoked by holding a 'Strange Shell' at the ocean.";
                            Main.npcChatCornerItem = mod.ItemType("AqueousSummon");
                        }
                        else if (!cultist)
                        {
                            Main.npcChatText = "A group of cultists stand at the entrance of the dungeon, praising something that they shouldn't. You better take them out before they summon anything chaotic.";
                        }
                        else if (!moonLord)
                        {
                            Main.npcChatText = "You've caused a great mess. Because of you, the moon lord and the 4 lunar pillars awaken in the land once more. This creature could mean the end of everything. Are you ready to take on him? If so, kill those pillars or use a 'Celestial Sigil' to awake him for real.";
                            Main.npcChatCornerItem = ItemID.CelestialSigil;
                        }
                        else if (!volcanox)
                        {
                            Main.npcChatText = "The divine being of the underworld, Volcanox, has been freed from it's prison. To calm the underworld down once again, you will have to make sure that he will fall. A 'Charred Core will be enough to unleash him.";
                            Main.npcChatCornerItem = mod.ItemType("VolcanoxSummon");
                        }
                        else if (!voidLeviathan)
                        {
                            Main.npcChatText = "The town is full of hallucinations and fatigue and the void glows with a sinister power - the Void Leviathan has returned from it's slumber. He is after your soul, so he can reach his full potenial once again. Take him down by summoning him with the 'Beacon of the Abyss.'";
                            Main.npcChatCornerItem = mod.ItemType("VoidLeviathanSummon");
                        }
                        else if (!azana)
                        {
                            Main.npcChatText = "The spirit of chaos, Azana has reawoken and will cause annihilation over the entirety of Terraria, if you don't stop her. This is one of the final challenges before the ultimatum, so you best prepare. Use her 'Mystic Plating to cause her appearance.";
                            Main.npcChatCornerItem = mod.ItemType("AzanaSummon");
                        }
                        else
                        {
                            Main.npcChatText = "Sorry, I'm all out of tales"; // never actually displayed anymore
                        }
                        #endregion
                    }
                    else
                    {
                        Main.npcChatText = "You question my origin? I am a celestial being, born within the core of Terraria. For so long, you mindlessly did what I said, without questioning. You did your job well. Now, it's time for you to perish. I do not need you any longer.";
                        buttonMode = 1;
                    }
                }
                if (buttonPressed == 1) // if we go straight to checking buttonMode then it does this when Question is pressed
                {
                    npc.active = false;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("AncientSpawn"), 0, 0f, Main.myPlayer, 0f, 0f);
                }
            }
            if (!firstButton)
            {
                shop = true;
                // shop button only appears after EoC   
                /*
                if (NPC.downedBoss1)
                {
                    shop = true;
                }
                else
                {
                    Main.npcChatText = "Uh, sorry i dont have anything to sell. Defeat a boss first";
                }
                */
            }
        }
        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            #region weapons
            if (NPC.downedBoss1)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("Sanguine"));
                shop.item[nextSlot].shopCustomPrice = 200000; // 20 gold
                nextSlot++;
            }
            if (NPC.downedBoss2)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("Wormer"));
                shop.item[nextSlot].shopCustomPrice = 250000;
                nextSlot++;
            }
            if (NPC.downedBoss3)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("Soulsword"));
                shop.item[nextSlot].shopCustomPrice = 300000;
                nextSlot++;
            }
            if (Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("Nihongo"));
                shop.item[nextSlot].shopCustomPrice = 350000;
                nextSlot++;
            }
            if (NPC.downedMechBoss1)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("ForeverSword"));
                shop.item[nextSlot].shopCustomPrice = 500000;
                nextSlot++;
            }
            if (NPC.downedMechBoss2)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("SearingBlaze"));
                shop.item[nextSlot].shopCustomPrice = 500000;
                nextSlot++;
            }
            if (NPC.downedMechBoss3)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("MasterSword"));
                shop.item[nextSlot].shopCustomPrice = 500000;
                nextSlot++;
            }
            if (NPC.downedPlantBoss)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("Gladiolus"));
                shop.item[nextSlot].shopCustomPrice = 650000;
                nextSlot++;
            }
            if (NPC.downedGolemBoss)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("Mjolnir"));
                shop.item[nextSlot].shopCustomPrice = 750000;
                nextSlot++;
            }
            if (NPC.downedFishron)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("PoseidonsTrident"));
                shop.item[nextSlot].shopCustomPrice = 1000000;
                nextSlot++;
            }
            if (NPC.downedMoonlord)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("EmptyGauntlet"));
                shop.item[nextSlot].shopCustomPrice = 1250000;
                nextSlot++;
            }
            #endregion
            #region drives
            nextSlot += 9; // to get it to the next row
            if (MyWorld.wastelandDrive)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("WastelandDrive"));
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1, 0, 0);
                nextSlot++;
            }
            if (MyWorld.infernaceDrive)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("InfernaceDrive"));
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1, 0, 0);
                nextSlot++;
            }
            if (MyWorld.scourgeFighterDrive)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("ScourgeFighterDrive"));
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1, 0, 0);
                nextSlot++;
            }
            if (MyWorld.regarothDrive)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("RegarothDrive"));
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1, 0, 0);
                nextSlot++;
            }
            if (MyWorld.celestialDrive)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("CelestialDrive"));
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1, 0, 0);
                nextSlot++;
            }
            if (MyWorld.obsidiousDrive)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("ObsidiousDrive"));
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1, 0, 0);
                nextSlot++;
            }
            if (MyWorld.permafrostDrive)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("PermafrostDrive"));
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1, 0, 0);
                nextSlot++;
            }
            if (MyWorld.aqueousDrive)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("AqueousDrive"));
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1, 0, 0);
                nextSlot++;
            }
            if (MyWorld.guardianDrive)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("GuardianDrive"));
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1, 0, 0);
                nextSlot++;
            }
            if (MyWorld.volcanoxDrive)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("VolcanoxDrive"));
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1, 0, 0);
                nextSlot++;
            }
            if (MyWorld.voidLeviathanDrive)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("VoidLeviathanDrive"));
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1, 0, 0);
                nextSlot++;
            }
            if (MyWorld.azanaDrive)
            {
                shop.item[nextSlot].SetDefaults(ModLoader.GetMod("ElementsAwoken").ItemType("AzanaDrive"));
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1, 0, 0);
                nextSlot++;
            }
            #endregion
        }

        public override string GetChat()
        {
            // on open reset button mode
            buttonMode = 0;

            if (MyWorld.downedAzana) // if azana is defeated stop talking
            {
                switch (Main.rand.Next(3))
                {
                    case 0: return "The flare of chaos is now extinct... that such a great warrior did fall with ease is truly interesting...";
                    case 1: return "You did put the ex-goddess out of her misery, huh? I'm curious... what do you seek?";
                    case 2: return "Little do you know what power you've set free...you're just as foolish as you look...";
                    default:
                        return "potaot";
                }
            }

            Player player = Main.player[Main.myPlayer];
            int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
            int merchant = NPC.FindFirstNPC(NPCID.Merchant);
            int guide = NPC.FindFirstNPC(NPCID.Guide);
            int dryad = NPC.FindFirstNPC(NPCID.Dryad);
            int armsDealer = NPC.FindFirstNPC(NPCID.ArmsDealer);
            int nurse = NPC.FindFirstNPC(NPCID.Nurse);
            int truffle = NPC.FindFirstNPC(NPCID.Truffle);
            if (Main.rand.Next(249) == 0)
            {
                return "Gah, perhaps I should just kill " + player.name + " now. Oh! I didn't see you there " + player.name + "... You didn't hear anything did you?";
            }
            if (Main.hardMode && Main.rand.Next(30) == 0)
            {
                return "Say, is it really you who makes these decisions? Or is it the puppeteer behind the curtain who pulls your strings?";
            }
            if (Main.bloodMoon && Main.rand.Next(5) == 0)
            {
                return "What a great night to kill monsters. The red moon makes it even way more fun.";
            }
            if (Main.pumpkinMoon && Main.rand.Next(5) == 0)
            {
                return "I never could understand humanity's satisfaction at that time of the year. They clearly didn't see real monsters. And the beings of this Pumpkin Moon are nothing compared to true ones, either.";
            }
            if (Main.snowMoon && Main.rand.Next(5) == 0)
            {
                return "At least this cold night ended in a good fight and not into an ice queen singing about her doubts. Ugh.";
            }
            if (partyGirl >= 0 && Main.rand.Next(5) == 0)
            {
                return "Go tell " + Main.npc[partyGirl].GivenName + " to tone down the noise, I'm sick of her partying.";
            }
            if (dryad >= 0 && Main.rand.Next(10) == 0)
            {
                return Main.npc[dryad].GivenName + " calls herself old. She has barely been born.";
            }
            if (truffle >= 0 && Main.rand.Next(10) == 0)
            {
                return "What on Earth is " + Main.npc[truffle].GivenName + "? In all my time here, I have never seen a being so peculiar...";
            }
            if (armsDealer >= 0 && Main.rand.Next(5) == 0)
            {
                return Main.npc[armsDealer].GivenName + "'s weapons are feeble little creations.";
            }
            if (guide >= 0 && Main.rand.Next(5) == 0)
            {
                return "Hah, " + Main.npc[guide].GivenName + " is a useless fool! No one requires his little 'tips' he has. Petty...";
            }
            if (ElementsAwoken.bossChecklistEnabled && Main.rand.Next(20) == 0)
            {
                return "Really? Boss Checklist? Am I seriously not good enough... Well then, guess I'll have to prove I'm better. You'll see.";
            }
            if (NPC.downedMoonlord && Main.rand.Next(15) == 0)
            {
                return "Ah, the Moon Lord is defeated... Well done, you are almost as strong as I.";
            }
            if (!NPC.downedBoss1 && Main.rand.Next(10) == 0)
            {
                return "Old " + Main.npc[merchant].GivenName + " is afraid of some puny eyeball! I would slay it in seconds if I believed it was even worth the effort. Hah!";
            }
            if (NPC.downedBoss1 && !MyWorld.downedWasteland && Main.rand.Next(10) == 0)
            {
                return "Argh! These damn scorpions keep getting in my house! I heard of a genetics lab meltdown after that giant eye was slain. Imbeciles cant even contain a scorpion!";
            }
            if (NPC.downedBoss3 && !Main.hardMode && Main.rand.Next(10) == 0)
            {
                return "Strange floral creatures have started appearing in the jungle... I saw one with a shell made of petals!";
            }
            if (NPC.downedMoonlord && !MyWorld.downedVoidLeviathan && Main.rand.Next(10) == 0)
            {
                return "The skies brighten once more, creatures that harbour an ore of immense power reside up there now.";
            }
            if (NPC.downedMoonlord && !MyWorld.downedVoidLeviathan && Main.rand.Next(10) == 0)
            {
                return "Death spreads. The critters are looking sickly. I watched one get eaten from the inside out by a giant purple tick...";
            }
            if (NPC.downedMoonlord && !MyWorld.downedVolcanox && Main.rand.Next(10) == 0)
            {
                return "Burning spirits have began to appear in the dungeon. I suspect that the lunar surge may have caused Volcanox to release them into the world.";
            }
            if (player.statLife <= (player.statLifeMax2 / 5) && Main.rand.Next(5) == 0)
            {
                if (nurse >= 0)
                {
                    return "You look weary, " + player.name + ". Go pay a visit to " + Main.npc[nurse].GivenName;
                }
                else
                {
                    return "You look weary, " + player.name + ". Go pay a visit to the nurse. Oh wait... We dont have one";
                }
            }

            switch (Main.rand.Next(8))
            {
                case 0: return "I wouldn't reccomend getting into a fight with me. I may be old but trust me, I have plenty of strength.";
                case 1: return "The beasts of Terraria are rough, I'll sell you some nasty weapons to aid in your combat.";
                case 2: return "These weapons are all made from 100% natural resources!";
                case 3: return "Hope you enjoy what I'm selling. Who am I kidding, of course you will!";
                case 4: return "Hmph. Sometimes standing around and mingling with all these people gets boring. I miss the days of war.";
                case 5: return "You want help crafting? I'm a storyteller! I deal legends not knowledge.";
                case 6: return "Why dont I kill the monsters myself? Uh, I'm awfully busy and you look like a strong young man.";
                case 7: return "I despise cookies. They are way too sweet for me. Honestly, I prefer the spicy stuff.";
                default:
                    return "potaot";
            }
        }
    }
}
