using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System;
using Terraria.GameContent.Events;
using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using ElementsAwoken.Structures;
using Terraria.DataStructures;
using ReLogic.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using System.Linq;
using ElementsAwoken.Items.Consumable.Potions;

namespace ElementsAwoken
{
    public class MyWorld : ModWorld
    {
        public static bool awakenedMode = false;

        public static bool credits = false;
        public static int creditsCounter = 0;

        private const int saveVersion = 0;

        public static int moonlordKills = 0;
        public static int voidLeviathanKills = 0;
        public static int ancientSummons = 0;
        public static int ancientKills = 0;

        public static string[] mysteriousPotionColours = new string[10];

        // one time alerts
        public static bool desertText = false;
        public static bool fireText = false;
        public static bool skyText = false;
        public static bool frostText = false;
        public static bool waterText = false;
        public static bool voidText = false;
        // downed bools
        public static bool downedToySlime = false;
        public static bool downedWasteland = false;
        public static bool downedInfernace = false;
        public static bool downedScourgeFighter = false;
        public static bool downedRegaroth = false;
        public static bool downedCelestial = false;
        public static bool downedPermafrost = false;
        public static bool downedObsidious = false;
        public static bool downedAqueous = false;
        public static bool downedEye = false;
        public static bool downedAncientWyrm = false;
        public static bool downedGuardian = false;
        public static bool downedVoidEvent = false;
        public static bool downedShadeWyrm = false;
        public static bool downedVolcanox = false;
        public static bool downedVoidLeviathan = false;
        public static bool downedAzana = false;
        public static bool downedAncients = false;
        public static bool downedCosmicObserver = false;

        public static bool darkMoon = false;

        // void event
        public static bool voidInvasionUp = false;
        public static bool willStartVoidInvasion = false;
        public static bool voidInvasionJustFinished = false;
        public static int justFinishedTimer = 0;
        // hailstorm
        public static bool hailStorm = false;
        public static int hailStormTime = 0;
        // tiles
        public static int SkyTiles = 0;
        public static int lizardTiles = 0;
        public static int corruptionTiles = 0;
        public static int crimsonTiles = 0;
        public static int hallowedTiles = 0;
        //lab
        public static int labPosition = 0;
        public static int sizeMult = (int)(Math.Floor(Main.maxTilesX / 4200f)); //Small = 2; Medium = ~3; Large = 4;
        public static bool generatedLabs = false;
        // genih statue
        public static bool aggressiveEnemies = false;
        public static bool swearingEnemies = false;
        //abbraxis encounters
        public static bool encounter1 = false;
        public static bool encounter2 = false;
        public static bool encounter3 = false;
        public static bool encounterSetup = false;
        public static int encounterTimer = 0;
        public static int encounterShakeTimer = 0;
        // has obtained the computer drives
        public static bool ancientsDrive = false;
        public static bool aqueousDrive = false;
        public static bool azanaDrive = false;
        public static bool celestialDrive = false;
        public static bool guardianDrive = false;
        public static bool infernaceDrive = false;
        public static bool obsidiousDrive = false;
        public static bool permafrostDrive = false;
        public static bool regarothDrive = false;
        public static bool scourgeFighterDrive = false;
        public static bool voidLeviathanDrive = false;
        public static bool volcanoxDrive = false;
        public static bool wastelandDrive = false;

        public override void Initialize()
        {
            awakenedMode = false;

            credits = false;

            sizeMult = (int)(Math.Floor(Main.maxTilesX / 4200f));
            generatedLabs = false;

            desertText = false;
            fireText = false;
            skyText = false;
            frostText = false;
            waterText = false;
            voidText = false;

            downedToySlime = false;
            downedWasteland = false;
            downedInfernace = false;
            downedScourgeFighter = false;
            downedRegaroth = false;
            downedCelestial = false;
            downedPermafrost = false;
            downedObsidious = false;
            downedAqueous = false;
            downedEye = false;
            downedAncientWyrm = false;
            downedGuardian = false;
            downedVoidEvent = false;
            downedShadeWyrm = false;
            downedVolcanox = false;
            downedVoidLeviathan = false;
            downedAzana = false;
            downedAncients = false;
            downedCosmicObserver = false;

            hailStorm = false;

            Main.invasionSize = 0;

            voidInvasionUp = false;
            willStartVoidInvasion = false;
            voidInvasionJustFinished = false;

            darkMoon = false;

            aggressiveEnemies = false;
            swearingEnemies = false;

            encounter1 = false;
            encounter2 = false;
            encounter3 = false;
            encounterSetup = false;

            aqueousDrive = false;
            azanaDrive = false;
            celestialDrive = false;
            guardianDrive = false;
            infernaceDrive = false;
            obsidiousDrive = false;
            permafrostDrive = false;
            regarothDrive = false;
            scourgeFighterDrive = false;
            voidLeviathanDrive = false;
            volcanoxDrive = false;
            wastelandDrive = false;

        }

        public override TagCompound Save()
        {
            TagCompound tag = new TagCompound();
            tag["awakenedMode"] = awakenedMode;

            tag["desertText"] = desertText;
            tag["fireText"] = fireText;
            tag["skyText"] = skyText;
            tag["frostText"] = frostText;
            tag["waterText"] = waterText;
            tag["voidText"] = voidText;

            tag["downedInfernace"] = downedInfernace;
            tag["downedScourgeFighter"] = downedScourgeFighter;
            tag["downedRegaroth"] = downedRegaroth;
            tag["downedAqueous"] = downedAqueous;
            tag["downedVoidLeviathan"] = downedVoidLeviathan;
            tag["downedCelestial"] = downedCelestial;
            tag["downedWasteland"] = downedWasteland;
            tag["downedPermafrost"] = downedPermafrost;
            tag["downedGuardian"] = downedGuardian;
            tag["downedEye"] = downedEye;
            tag["downedAncientWyrm"] = downedAncientWyrm;
            tag["downedObsidious"] = downedObsidious;
            tag["downedShadeWyrm"] = downedShadeWyrm;
            tag["downedVolcanox"] = downedVolcanox;
            tag["downedToySlime"] = downedToySlime;
            tag["downedAzana"] = downedAzana;
            tag["downedAncients"] = downedAncients;
            tag["downedCosmicObserver"] = downedCosmicObserver;

            tag["downedVoidEvent"] = downedVoidEvent;

            tag["generatedLabs"] = generatedLabs;

            tag["moonlordKills"] = moonlordKills;
            tag["voidLeviathanKills"] = voidLeviathanKills;
            tag["ancientKills"] = ancientKills;
            tag["ancientSummons"] = ancientSummons;

            tag["aqueousDrive"] = aqueousDrive;
            tag["azanaDrive"] = azanaDrive;
            tag["celestialDrive"] = celestialDrive;
            tag["guardianDrive"] = guardianDrive;
            tag["infernaceDrive"] = infernaceDrive;
            tag["obsidiousDrive"] = obsidiousDrive;
            tag["permafrostDrive"] = permafrostDrive;
            tag["regarothDrive"] = regarothDrive;
            tag["scourgeFighterDrive"] = scourgeFighterDrive;
            tag["voidLeviathanDrive"] = voidLeviathanDrive;
            tag["volcanoxDrive"] = volcanoxDrive;
            tag["wastelandDrive"] = wastelandDrive;

            for (int i = 0; i < mysteriousPotionColours.Length; i++)
            {
                tag["potColours" + i] = mysteriousPotionColours[i];
            }
            return tag;
        }

        public override void Load(TagCompound tag)
        {
            awakenedMode = tag.GetBool("awakenedMode");

            desertText = tag.GetBool("desertText");
            fireText = tag.GetBool("fireText");
            skyText = tag.GetBool("skyText");
            frostText = tag.GetBool("frostText");
            waterText = tag.GetBool("waterText");
            voidText = tag.GetBool("voidText");

            downedInfernace = tag.GetBool("downedInfernace");
            downedAqueous = tag.GetBool("downedAqueous");
            downedVoidLeviathan = tag.GetBool("downedVoidLeviathan");
            downedScourgeFighter = tag.GetBool("downedScourgeFighter");
            downedRegaroth = tag.GetBool("downedRegaroth");
            downedCelestial = tag.GetBool("downedCelestial");
            downedWasteland = tag.GetBool("downedWasteland");
            downedPermafrost = tag.GetBool("downedPermafrost");
            downedGuardian = tag.GetBool("downedGuardian");
            downedEye = tag.GetBool("downedEye");
            downedAncientWyrm = tag.GetBool("downedAncientWyrm");
            downedObsidious = tag.GetBool("downedObsidious");
            downedShadeWyrm = tag.GetBool("downedShadeWyrm");
            downedVolcanox = tag.GetBool("downedVolcanox");
            downedToySlime = tag.GetBool("downedToySlime");
            downedAzana = tag.GetBool("downedAzana");
            downedAncients = tag.GetBool("downedAncients");
            downedCosmicObserver = tag.GetBool("downedCosmicObserver");

            downedVoidEvent = tag.GetBool("downedVoidEvent");

            generatedLabs = tag.GetBool("generatedLabs");

            moonlordKills = tag.GetInt("moonlordKills");
            voidLeviathanKills = tag.GetInt("voidLeviathanKills");
            ancientKills = tag.GetInt("ancientKills");
            ancientSummons = tag.GetInt("ancientSummons");

            aqueousDrive = tag.GetBool("aqueousDrive");
            azanaDrive = tag.GetBool("azanaDrive");
            celestialDrive = tag.GetBool("celestialDrive");
            guardianDrive = tag.GetBool("guardianDrive");
            infernaceDrive = tag.GetBool("infernaceDrive");
            obsidiousDrive = tag.GetBool("obsidiousDrive");
            permafrostDrive = tag.GetBool("permafrostDrive");
            regarothDrive = tag.GetBool("regarothDrive");
            scourgeFighterDrive = tag.GetBool("scourgeFighterDrive");
            voidLeviathanDrive = tag.GetBool("voidLeviathanDrive");
            volcanoxDrive = tag.GetBool("volcanoxDrive");
            wastelandDrive = tag.GetBool("wastelandDrive");
            for (int i = 0; i < mysteriousPotionColours.Length; i++)
            {
                mysteriousPotionColours[i] = tag.GetString("potColours" + i);
            }
        }

        public override void NetSend(BinaryWriter writer)
        {
            // downed
            BitsByte flags1 = new BitsByte();
            flags1[0] = downedAqueous;
            flags1[1] = downedVoidLeviathan;
            flags1[2] = downedInfernace;
            flags1[3] = downedScourgeFighter;
            flags1[4] = downedRegaroth;
            flags1[5] = downedCelestial;
            flags1[6] = downedWasteland;
            flags1[7] = downedPermafrost;
            writer.Write(flags1);

            BitsByte flags2 = new BitsByte();
            flags2[0] = downedGuardian;
            flags2[1] = downedEye;
            flags2[2] = downedAncientWyrm;
            flags2[3] = downedObsidious;
            flags2[4] = downedShadeWyrm;
            flags2[5] = downedVolcanox;
            flags2[6] = downedVoidEvent;
            flags2[7] = downedToySlime;
            writer.Write(flags2);

            BitsByte flags3 = new BitsByte();
            flags3[0] = downedAzana;
            flags3[1] = downedAncients;
            flags3[2] = downedCosmicObserver;
            writer.Write(flags3);

            // lab gen & drive obtained
            BitsByte flags4 = new BitsByte();
            flags4[0] = generatedLabs;
            flags4[1] = aqueousDrive;
            flags4[2] = azanaDrive;
            flags2[3] = celestialDrive;
            flags2[4] = guardianDrive;
            flags2[5] = infernaceDrive;
            flags2[6] = obsidiousDrive;
            flags2[7] = permafrostDrive;
            writer.Write(flags4);

            BitsByte flags5 = new BitsByte();
            flags5[0] = regarothDrive;
            flags5[1] = scourgeFighterDrive;
            flags5[2] = voidLeviathanDrive;
            flags5[3] = volcanoxDrive;
            flags5[4] = wastelandDrive;
            writer.Write(flags5);

            // essence notifacations
            BitsByte flags6 = new BitsByte();
            flags6[0] = desertText;
            flags6[1] = fireText;
            flags6[2] = skyText;
            flags6[3] = frostText;
            flags6[4] = waterText;
            flags6[5] = voidText;
            writer.Write(flags6);

            //random
            BitsByte flags7 = new BitsByte();
            flags7[0] = awakenedMode;
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags1 = reader.ReadByte();
            downedAqueous = flags1[0];
            downedVoidLeviathan = flags1[1];
            downedInfernace = flags1[2];
            downedScourgeFighter = flags1[3];
            downedRegaroth = flags1[4];
            downedCelestial = flags1[5];
            downedWasteland = flags1[6];
            downedPermafrost = flags1[7];

            BitsByte flags2 = reader.ReadByte();
            downedGuardian = flags2[0];
            downedEye = flags2[1];
            downedAncientWyrm = flags2[2];
            downedObsidious = flags2[3];
            downedShadeWyrm = flags2[4];
            downedVolcanox = flags2[5];
            downedVoidEvent = flags2[6];
            downedToySlime = flags2[7];

            BitsByte flags3 = reader.ReadByte();
            downedAzana = flags3[0];
            downedAncients = flags3[1];
            downedCosmicObserver = flags3[2];     

            // lab gen & drive obtained
            BitsByte flags4 = reader.ReadByte();
            generatedLabs = flags4[0];
            aqueousDrive = flags4[1];
            azanaDrive = flags4[2];
            celestialDrive = flags4[3];
            guardianDrive = flags4[4];
            infernaceDrive = flags4[5];
            obsidiousDrive = flags4[6];
            permafrostDrive = flags4[7];

            BitsByte flags5 = reader.ReadByte();
            regarothDrive = flags5[0];
            scourgeFighterDrive = flags5[1];
            voidLeviathanDrive = flags5[2];
            volcanoxDrive = flags5[3];
            wastelandDrive = flags5[4];

            // essence notifacations
            BitsByte flags6 = reader.ReadByte();
            desertText = flags6[0];
            fireText = flags6[1];
            skyText = flags6[2];
            frostText = flags6[3];
            waterText = flags6[4];
            voidText = flags6[5];

            //random 
            BitsByte flags7= reader.ReadByte();
            awakenedMode = flags7[0];
        }

        // allows pre tml0.9 content to be loaded
        /*public override void LoadLegacy(BinaryReader reader)
        {
            reader.ReadInt32();
            byte flags = reader.ReadByte();
            downedAqueous = ((flags & 1) == 1);
            downedVoidLeviathan = ((flags & 2) == 2);
            downedInfernace = ((flags & 3) == 3);
            downedScourgeFighter = ((flags & 4) == 4);
            downedRegaroth = ((flags & 5) == 5);
            downedCelestial = ((flags & 6) == 6);
            downedWasteland = ((flags & 7) == 7);
            downedPermafrost = ((flags & 8) == 8);
            downedGuardian = ((flags & 9) == 9);
            downedEye = ((flags & 10) == 10);
            downedAncientWyrm = ((flags & 11) == 11);
            downedObsidious = ((flags & 12) == 12);
            downedShadeWyrm = ((flags & 13) == 13);
            downedVolcanox = ((flags & 14) == 14);
            downedVoidEvent = ((flags & 15) == 15);
            downedToySlime = ((flags & 16) == 16);
            downedAzana = ((flags & 17) == 17);
            downedAncients = ((flags & 18) == 18);
            downedCosmicObserver = ((flags & 19) == 19);
        }*/

        // adding item in chests
        public override void PostWorldGen()
        {
            RandomisePotions();
            int[] dungeonItems = new int[] { mod.ItemType("CrashingWave") };
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 2 * 36)
                {
                    if (Main.rand.Next(5) == 0)
                    {
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                        {

                            if (chest.item[inventoryIndex].type == 0)
                            {
                                chest.item[inventoryIndex].SetDefaults(Main.rand.Next(dungeonItems));
                                break;
                            }
                        }
                    }
                }
                if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 1 * 36)
                {
                    if (Main.rand.Next(5) == 0)
                    {
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                        {
                            if (chest.item[inventoryIndex].type == 0)
                            {
                                chest.item[inventoryIndex].SetDefaults(mod.ItemType("MysteriousPotion"));
                                MysteriousPotion pot = (MysteriousPotion)chest.item[inventoryIndex].modItem;
                                pot.potionNum = Main.rand.Next(10);
                                break;
                            }
                        }
                    }
                }
            }

        }
        /*public bool CalamityModRevengeance
        {
            get { return CalamityMod.CalamityWorld.revenge; }
        }*/ // gimme the fckn windows.dll pls 
        public override void PostUpdate()
        {
            Player player = Main.player[Main.myPlayer];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (ElementsAwoken.calamityEnabled)
            {
                /*if (CalamityModRevengeance)
                {
                    if (!awakenedMode)
                    {
                        Main.NewText("The forces of the world get twisted beyond imagination...", Color.DeepPink);
                        awakenedMode = true;
                    }
                }*/
            }
            if (awakenedMode)
            {
                Main.expertMode = true;

            }
            if (modPlayer.voidEnergyTimer > 0)
            {
                MoonlordShake(1f);
            }

            if (modPlayer.infinityDeath)
            {
                MoonlordShake(0.6f);
            }

            #region credits

            
            #endregion

            #region encounters
            encounterTimer--;
            if (encounterTimer <= 0)
            {
                encounterTimer = 0;
                encounter1 = false;
                encounter2 = false;
                encounter3 = false;
            }
            if (!encounterSetup)
            {
                encounterSetup = true;
                modPlayer.encounterTextTimer = 0;
                if (encounter2 || encounter3)
                {
                    Main.rainTime = 3600;
                    Main.raining = true;
                    Main.maxRaining = 0.8f;
                }
            }
            if (encounter1)
            {
                ElementsAwoken.screenshakeAmount = 5f;
                if (encounterTimer <= 1600)
                {
                    encounterTimer = 0; // cut the time of the event in half 
                }
            }
            if (encounter2)
            {               
                if (player.active)
                {
                    player.AddBuff(BuffID.Darkness, 60);
                }
                if (!Main.gameMenu)
                {
                    Main.time += Main.rand.Next(8, 25);
                    if (Main.time > 32400.0 - 30)
                    {
                        Main.time = 0;
                    }
                }
            }
            if (encounter3)
            {
                if (player.active)
                {
                    player.AddBuff(BuffID.Darkness, 60);
                    //player.AddBuff(BuffID.Blackout, 60);
                }
                Main.time = 16220;
                Main.dayTime = false;
            }
            if (modPlayer.encounterTextTimer > 0 || encounter3)
            {
                float intensity = MathHelper.Clamp((float)Math.Sin((modPlayer.encounterTextTimer / 19.99f)) * 0.5f, 0f, 1f); // sin makes it pulsate. must divided by a float or it goes wonky
                if (encounter3)
                {
                    intensity += 0.3f;
                }
                if (modPlayer.finalText)
                {
                    intensity = 1f;
                }
                //Main.NewText("" + intensity);
                MoonlordShake(intensity);
            }
            #endregion

            #region DotV
            justFinishedTimer--;
            if (justFinishedTimer <= 0)
            {
                justFinishedTimer = 0;
                voidInvasionJustFinished = false;
            }

            if (voidInvasionUp)
            {
                //Updates the invasion while it heads to spawn point and ends it
                VoidEvent.UpdateInvasion();
                // gradual brightness change
                if (!Main.dayTime && Main.time > 12620 && Main.time < 16220)
                {
                    Lighting.brightness = 0.75f; // lower because it doesnt have darkness and blackout too
                }
                if (!Main.dayTime && Main.time > 15620 && Main.time < 15620)
                {
                    Lighting.brightness = 0.4f; // lower because it doesnt have darkness and blackout too
                }
                if (!Main.dayTime && Main.time == 16220)
                {
                    Main.NewText("Darkness seeps through your veins...", 31, 34, 66, false);
                }
                if (!Main.dayTime && Main.time > 16220)
                {
                    MoonlordShake(1f);
                    for (int i = 0; i < Main.player.Length; i++)
                    {
                        Player p = Main.player[i];
                        if (p.active)
                        {
                            //player.AddBuff(BuffID.Darkness, 60);
                            p.AddBuff(BuffID.Blackout, 60);
                            p.AddBuff(BuffID.Slow, 60);
                            Lighting.brightness = 0.5f; // idk if this is bad to do
                            p.AddBuff(mod.BuffType("DeterioratingWings"), 60, false);
                        }
                    }
                    for (int k = 0; k < Main.npc.Length; k++)
                    {
                        NPC other = Main.npc[k];
                        foreach (int invader in VoidEvent.phase1NPCs)
                        {
                            if (other.type == invader)
                            {
                                other.alpha += 5;
                                if (other.alpha >= 255)
                                {
                                    other.active = false;
                                }
                            }
                        }
                    }
                }
                bool allDead = true;
                for (int i = 0; i < Main.player.Length; i++)
                {
                    if (!Main.player[i].dead)
                    {
                        allDead = false;
                    }
                    if (allDead)
                    {
                        Main.NewText("Laughs echo throughout the world", 182, 15, 15, false);
                        Main.PlaySound(29, (int)Main.player[i].position.X, (int)Main.player[i].position.Y, 105, 1f, 0f);
                        voidInvasionUp = false;
                        voidInvasionJustFinished = true;
                        justFinishedTimer = 200;
                    }
                }
            }
            else // no void invasion
            {
                if (voidInvasionJustFinished)
                {
                    for (int k = 0; k < Main.npc.Length; k++)
                    {
                        NPC other = Main.npc[k];
                        foreach (int invader in VoidEvent.phase1NPCs)
                        {
                            if (other.type == invader)
                            {
                                other.alpha += 3;
                                if (other.alpha >= 255)
                                {
                                    other.active = false;
                                }
                            }
                        }
                        foreach (int invader in VoidEvent.phase2NPCs)
                        {
                            if (other.type == invader)
                            {
                                other.alpha += 3;
                                if (other.alpha >= 255)
                                {
                                    other.active = false;
                                }
                            }
                        }
                    }
                }
            }

            if (willStartVoidInvasion)
            {
                if (!Main.dayTime)
                {
                    VoidEvent.StartInvasion();
                    Main.invasionX = Main.spawnTileX; // instantly start
                    willStartVoidInvasion = false;
                    Filters.Scene.Deactivate("MoonLordShake", new object[0]);
                }
                float intensity = MathHelper.Clamp((float)Main.time / 54000f, 0f, 1f);
                MoonlordShake(intensity);
                //Main.NewText("time" + " " + Main.time + " " + "intensity" + " " + intensity, Color.White.R, Color.White.G, Color.White.B);
            }
            #endregion
        }

        // thanks laugic !
        #region lab gen
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int xO = (int)(Main.maxTilesX * 0.75f);
            int yO = (int)(Main.maxTilesY * .7f);
            if (!ModContent.GetInstance<Config>().labsDisabled)
            {
                int genIndex2 = tasks.FindIndex(genpass => genpass.Name.Equals("Buried Chests")); // "Lihzahrd Altars" are basically the last thing to be generated
                tasks.Insert(genIndex2 + 2, new PassLegacy("Generating Labs", delegate (GenerationProgress progress)
                {
                    progress.Message = "Killing Scientists";
                    LabStructures(xO, yO);
                }));
            }
        }
        /*private Point16 AlterStructureLocation(int xO, int yO, int structX, int structY)
        {
            structX += Main.rand.Next(225 * sizeMult);
            if (structX > xO + 225 * sizeMult)
            {
                structX -= 225 * sizeMult * 2;
                structY += Main.rand.Next(275 * sizeMult / 4, 275 * sizeMult / 2);
                if (structY > yO + 275 * sizeMult)
                {
                    structY -= 275 * sizeMult * 2;
                }
            }
            Point16 structurePosition = new Point16(structX, structY);
            return structurePosition;
        }*/
        private void LabStructures(int xO, int yO)
        {
            int s = 0;
            int structX = xO - 225 * sizeMult + Main.rand.Next(225 * sizeMult * 2);
            int structY = yO - 275 * sizeMult + Main.rand.Next(275 * sizeMult / 2);
            generatedLabs = true;
            for (int q = 0; q < 13; q++) // 13 different labs. dont try generating above 200, it runs out of space and crashes(might be fixed)
            {
                int border = 300 * sizeMult;
                int xMin = border;
                int xMax = Main.maxTilesX - border;
                int labPosX = WorldGen.genRand.Next(xMin, xMax);

                int yMin = (int)(WorldGen.worldSurfaceHigh + 200.0);
                int yMax = Main.maxTilesY - 230;
                int labPosY = WorldGen.genRand.Next(yMin, yMax);

                s = GenerateLab(s, labPosX, labPosY);
            }
        }

        private int GenerateLab(int s, int structX, int structY)
        {
            if (TileCheckSafe(structX, structY))
            {
                if (!Chest.NearOtherChests(structX, structY))
                {
                    if (structY < Main.maxTilesY - 220) // wont generate in hell (it shouldnt anyway because the max is above hell still)
                    { 
                        bool mirrored = false;
                        if (Main.rand.Next(2) == 0)
                            mirrored = true;

                        PickLab(s, structX, structY, mirrored);
                        s++;
                        if (s > 12)
                        {
                            s = 0; // this should never happen but just to be safe
                        }
                    }
                }
            }
            return s;
        }
        private void PickLab(int s, int structX, int structY, bool mirrored)
        {
            if (s == 0)
            {
                WastelandLab.StructureGen(structX, structY, mirrored); // create the structure first
                WastelandLabPlatforms.StructureGen(structX, structY, mirrored); // then create the platforms (on top of the walls)
                WastelandLabFurniture.StructureGen(structX, structY, mirrored); // then the furniture
            }
            else if (s == 12)
            {
                AncientsLab.Generate(structX, structY, mirrored);
            }
            else
            {
                Lab.StructureGen(structX, structY, mirrored); // create the structure first
                LabPlatforms.StructureGen(structX, structY, mirrored); // create the structure first
                LabFurniture.StructureGen(structX, structY, mirrored, s-1); // then the furniture. s - 1 so the drives start at 0 again
            }

        }

        public static void PlaceLabLocker(int x, int y, ushort floorType)
        {
            ClearSpaceForChest(x, y, floorType);
            int chestIndex = WorldGen.PlaceChest(x, y, (ushort)ElementsAwoken.instance.TileType("Locker"), false, 0);

            int specialItem = GetLabLoot();
            labPosition++;
            int[] oreLoot = GetOreLoot();
            int[] potionLoot = GetPotionLoot();
            int[] money = GetMoneyLoot();
            int[] miscLoot = GetMiscLoot();

            int[] itemsToPlaceInChests = new int[] { specialItem, oreLoot[0], potionLoot[0], money[0], miscLoot[0] };
            int[] itemCounts = new int[] { 1, oreLoot[1], potionLoot[1], money[1], miscLoot[1] };

            FillChest(chestIndex, itemsToPlaceInChests, itemCounts);
        }
        private static int GetLabLoot()
        {
            int[] labLoot = new int[] {
                ElementsAwoken.instance.ItemType("Coilgun"),
                ElementsAwoken.instance.ItemType("Electrozzitron"),
                ElementsAwoken.instance.ItemType("TeslaRod"),
                ElementsAwoken.instance.ItemType("BassBooster"),
                ElementsAwoken.instance.ItemType("Taser"),
                ElementsAwoken.instance.ItemType("RustedMechanism"),
            };

            if (labPosition < labLoot.GetLength(0))
                return labLoot[labPosition];
            else
            {
                labPosition = 0;
                return labLoot[labPosition];
            }
        }
        private static int[] GetOreLoot()
        {
            int[] oreLoot = new int[] { ItemID.GoldBar, ItemID.PlatinumBar, ItemID.TungstenBar, ItemID.SilverBar };
            int orePos = Main.rand.Next(oreLoot.GetLength(0));
            int oreCount = Main.rand.Next(6, 16);
            int[] ore = { 0, 0 };
            ore[0] = oreLoot[orePos];
            ore[1] = oreCount;
            return ore;
        }

        private static int[] GetPotionLoot()
        {
            int[] potLoot = new int[] {ItemID.InfernoPotion, ItemID.LifeforcePotion, ItemID.WrathPotion};
            int potPos = Main.rand.Next(potLoot.GetLength(0));
            int potCount = Main.rand.Next(2, 5);
            int[] pot = { 0, 0 };
            pot[0] = potLoot[potPos];
            pot[1] = potCount;
            return pot;
        }

        private static int[] GetMoneyLoot()
        {
            int monType = 0;
            int monCount = 0;
            if (Main.rand.Next(2) == 0)
            {
                monType = ItemID.GoldCoin;
                monCount = Main.rand.Next(1, 4);
            }
            else
            {
                monType = ItemID.SilverCoin;
                monCount = Main.rand.Next(60, 99);
            }
            int[] mon = { 0, 0 };
            mon[0] = monType;
            mon[1] = monCount;
            return mon;
        }
        private static int[] GetMiscLoot()
        {
            int[] mscLoot = new int[] { ElementsAwoken.instance.ItemType("Capacitor"), ElementsAwoken.instance.ItemType("CopperWire"), ElementsAwoken.instance.ItemType("GoldWire"), };
            int mscPos = Main.rand.Next(mscLoot.GetLength(0));
            int mscCount = Main.rand.Next(2, 6);
            int[] msc = { 0, 0 };
            msc[0] = mscLoot[mscPos];
            msc[1] = mscCount;
            return msc;
        }
        private static void ClearSpaceForChest(int x, int y, ushort floorType)
        {
            WorldGen.KillTile(x, y);
            WorldGen.KillTile(x, y - 1);
            WorldGen.KillTile(x + 1, y - 1);
            WorldGen.KillTile(x + 1, y);
            WorldGen.PlaceTile(x + 1, y + 1, floorType, true, true);
            WorldGen.PlaceTile(x, y + 1, floorType, true, true);
            Main.tile[x, y].liquid = 0;
            Main.tile[x + 1, y].liquid = 0;
            Main.tile[x, y + 1].liquid = 0;
            Main.tile[x + 1, y + 1].liquid = 0;
        }
        private static void FillChest(int chestIndex, int[] itemsToPlaceInChests, int[] itemCounts)
        {
            if (chestIndex < Main.chest.GetLength(0) && chestIndex >= 0)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null)
                {
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == 0 && inventoryIndex < itemsToPlaceInChests.GetLength(0))
                        {
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInChests[inventoryIndex]);
                            chest.item[inventoryIndex].stack = itemCounts[inventoryIndex];
                        }
                    }
                }
            }
        }
        public static bool TileCheckSafe(int i, int j)
        {
            if (i > 0 && i < Main.maxTilesX && j > 0 && j < Main.maxTilesY)
                return true;
            return false;
        }
        #endregion

        public override void TileCountsAvailable(int[] tileCounts)
        {
            SkyTiles = tileCounts[TileID.Cloud];
            lizardTiles = tileCounts[TileID.LihzahrdBrick];

            corruptionTiles = tileCounts[TileID.CorruptGrass] + tileCounts[TileID.CorruptHardenedSand] + tileCounts[TileID.CorruptIce] + tileCounts[TileID.CorruptSandstone] + tileCounts[TileID.CorruptThorns] + tileCounts[TileID.Ebonsand] + tileCounts[TileID.Ebonstone];
            crimsonTiles = tileCounts[TileID.FleshGrass] + tileCounts[TileID.CrimsonHardenedSand] + tileCounts[TileID.FleshIce] + tileCounts[TileID.CrimsonSandstone] + tileCounts[TileID.CrimtaneThorns] + tileCounts[TileID.Crimsand] + tileCounts[TileID.Crimstone];
            hallowedTiles = tileCounts[TileID.HallowedGrass] + tileCounts[TileID.HallowHardenedSand] + tileCounts[TileID.HallowedIce] + tileCounts[TileID.HallowedPlants] + tileCounts[TileID.HallowedPlants2] + tileCounts[TileID.HallowSandstone] + tileCounts[TileID.Pearlsand] + tileCounts[TileID.Pearlstone];
        }
        private void MoonlordShake(float intensity)
        {
            if (!Filters.Scene["MoonLordShake"].IsActive())
            {
                Filters.Scene.Activate("MoonLordShake", Main.player[Main.myPlayer].position, new object[0]);
            }
            Filters.Scene["MoonLordShake"].GetShader().UseIntensity(intensity);
        }

        public static void RandomisePotions()
        {
            var nums = Enumerable.Range(0, 10).ToArray();

            // Shuffle the array
            for (int i = 0; i < nums.Length; ++i)
            {
                int randomIndex = Main.rand.Next(nums.Length);
                int temp = nums[randomIndex];
                nums[randomIndex] = nums[i];
                nums[i] = temp;
            }
            for (int i = 0; i < nums.Length; ++i)
            {
                switch (nums[i])
                {
                    case 0:
                        mysteriousPotionColours[i] = "Red";
                        break;
                    case 1:
                        mysteriousPotionColours[i] = "Cyan";
                        break;
                    case 2:
                        mysteriousPotionColours[i] = "Lime";
                        break;
                    case 3:
                        mysteriousPotionColours[i] = "Fuschia";
                        break;
                    case 4:
                        mysteriousPotionColours[i] = "Pink";
                        break;
                    case 5:
                        mysteriousPotionColours[i] = "Brown";
                        break;
                    case 6:
                        mysteriousPotionColours[i] = "Orange";
                        break;
                    case 7:
                        mysteriousPotionColours[i] = "Yellow";
                        break;
                    case 8:
                        mysteriousPotionColours[i] = "Blue";
                        break;
                    case 9:
                        mysteriousPotionColours[i] = "Purple";
                        break;
                    default:
                        mysteriousPotionColours[i] = "Red";
                        break;
                }
                ElementsAwoken.DebugModeText(nums[i] + " " + mysteriousPotionColours[i]);
            }
        }
    }
    public class CreditsLight : GlobalWall
    {
        public override void ModifyLight(int i, int j, int type, ref float r, ref float g, ref float b)
        {
            Player player = Main.player[Main.myPlayer];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (Main.tile[i, j].active() == false && MyWorld.credits && MyWorld.creditsCounter >= modPlayer.screenTransDuration / 2)
            {
                r = 0.7f;
                g = 0.7f;
                b = 0.7f;
            }
        }
    }
}