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
using Terraria.Localization;
using ElementsAwoken.Events.VoidEvent;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.NPCProj;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using ElementsAwoken.Events.RadiantRain.Enemies;
using ElementsAwoken.Tiles.VolcanicPlateau;
using ElementsAwoken.NPCs.VolcanicPlateau;
using ElementsAwoken.NPCs.VolcanicPlateau.Lake;
using ElementsAwoken.NPCs.Town;
using ElementsAwoken.Structures.VolcanicPlateau;

namespace ElementsAwoken
{
    public class MyWorld : ModWorld
    {
        public static bool awakenedMode = false;

        public static int generalTimer = 0;

        public static int moonlordKills = 0;
        public static int voidLeviathanKills = 0;
        public static int ancientSummons = 0;
        public static int ancientKills = 0;

        public static int fastRandUpdate = 0;

        // downed bools
        public static bool downedToySlime = false;
        public static bool downedWasteland = false;
        public static bool downedInfernace = false;
        public static bool downedScourgeFighter = false;
        public static bool downedRegaroth = false;
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
        public static bool sparedAzana = false;
        public static bool downedAncients = false;
        public static bool downedCosmicObserver = false;
        public static bool completedRadiantRain = false;
        public static bool downedRadiantMaster = false;
        public static bool downedErius = false;
        public static bool downedMineBoss = false;
        public static bool downedKeeper = false;
        public static bool downedGiantPinky = false;

        public static bool cultArrived = false;

        // events
        public static bool darkMoon = false;

        public static bool voidInvasionUp = false;
        public static bool voidInvasionWillStart = false;
        public static int voidInvasionFinished = 0;

        // star shower
        public static int starShowerTime = 0;
        // hailstorm
        public static int hailStormTime = 0;
        // tiles
        public static int SkyTiles = 0;
        public static int lizardTiles = 0;
        public static int plateauTiles = 0;
        public static int corruptionTiles = 0;
        public static int crimsonTiles = 0;
        public static int hallowedTiles = 0;

        // genih statue
        public static bool aggressiveEnemies = false;
        public static bool swearingEnemies = false;

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

        // boss prompts
        public static int desertPrompt = 0;
        public static int firePrompt = 0;
        public static int skyPrompt = 0;
        public static int frostPrompt = 0;
        public static int waterPrompt = 0;
        public static int voidPrompt = 0;

        public static int skyPromptRainCD = 0;

        public static bool radiantRain = false;
        public static bool prevTickRaining = false;

        public static int plateauWeather = 0; // sulphurous storm = 1, eruption = 2, rifting  = 3;
        public static int plateauWeatherTime = 0;

        public static bool awakenedPlateau = false;
        public static int volcShrineActivated = 0;
        public static int mechBlueprints = 1;
        public static bool spokenToCindari = false;

        public override void Initialize() // called when the world is loaded
        {
            awakenedMode = false;

            voidLeviathanKills = 0;
            moonlordKills = 0;
            ancientKills = 0;
            ancientSummons = 0;

            downedToySlime = false;
            downedWasteland = false;
            downedInfernace = false;
            downedScourgeFighter = false;
            downedRegaroth = false;
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
            sparedAzana = false;
            downedAncients = false;
            downedCosmicObserver = false;
            completedRadiantRain = false;
            downedRadiantMaster = false;
            downedErius = false;
            downedMineBoss = false;
            downedKeeper = false;
            downedGiantPinky = false;

            cultArrived = false;

            Main.invasionSize = 0;

            voidInvasionUp = false;
            voidInvasionWillStart = false;
            voidInvasionFinished = 0;

            darkMoon = false;

            aggressiveEnemies = false;
            swearingEnemies = false;

            ElementsAwoken.encounter = 0;
            ElementsAwoken.encounterTimer = 0;
            ElementsAwoken.encounterSetup = false;

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

            desertPrompt = 0;
            firePrompt = 0;
            skyPrompt = 0;
            frostPrompt = 0;
            waterPrompt = 0;
            voidPrompt = 0;

            skyPromptRainCD = 0;

            hailStormTime = 0;

            starShowerTime = 0;

            radiantRain = false;
            prevTickRaining = false;

            spokenToCindari = false;

            plateauWeather = 0;
            plateauWeatherTime = 0;
            awakenedPlateau = false;

            volcShrineActivated = 0;
            mechBlueprints = 1;
        }
        public override TagCompound Save()
        {
            TagCompound tag = new TagCompound();
            tag["awakenedMode"] = awakenedMode;

            tag["downedInfernace"] = downedInfernace;
            tag["downedScourgeFighter"] = downedScourgeFighter;
            tag["downedRegaroth"] = downedRegaroth;
            tag["downedAqueous"] = downedAqueous;
            tag["downedVoidLeviathan"] = downedVoidLeviathan;
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
            tag["completedRadiantRain"] = completedRadiantRain;
            tag["downedRadiantMaster"] = downedRadiantMaster;
            tag["downedErius"] = downedErius;
            tag["downedMineBoss"] = downedMineBoss;
            tag["downedKeeper"] = downedKeeper;
            tag["downedGiantPinky"] = downedGiantPinky;

            tag["cultArrived"] = cultArrived;

            tag["downedVoidEvent"] = downedVoidEvent;

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

            tag["desertPrompt"] = desertPrompt;
            tag["firePrompt"] = firePrompt;
            tag["skyPrompt"] = skyPrompt;
            tag["frostPrompt"] = frostPrompt;
            tag["waterPrompt"] = waterPrompt;
            tag["voidPrompt"] = voidPrompt;

            tag["radiantRain"] = radiantRain;

            tag["starShowerTime"] = starShowerTime;


            tag["spokenToCindari"] = spokenToCindari;
            tag["plateauWeather"] = plateauWeather;
            tag["plateauWeatherTime"] = plateauWeatherTime;
            tag["awakenedPlateau"] = awakenedPlateau;
            tag["mechBlueprints"] = mechBlueprints;

            //save npcs- dont know if this works properly
            int numSaved = 0;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active)
                {
                    if (nPC.GetGlobalNPC<NPCs.NPCsGLOBAL>().saveNPC)
                    {
                        //tag["netID"] = nPC.netID;
                        tag["npcType" + numSaved] = nPC.type;
                        tag["givenName" + numSaved] = nPC.GivenName;
                        tag["posX" + numSaved] = nPC.position.X;
                        tag["posY" + numSaved] = nPC.position.Y;
                        numSaved++;
                    }
                }
            }
            tag["numSaved"] = numSaved;

            return tag;
        }
        public override void Load(TagCompound tag)
        {
            awakenedMode = tag.GetBool("awakenedMode");

            downedInfernace = tag.GetBool("downedInfernace");
            downedAqueous = tag.GetBool("downedAqueous");
            downedVoidLeviathan = tag.GetBool("downedVoidLeviathan");
            downedScourgeFighter = tag.GetBool("downedScourgeFighter");
            downedRegaroth = tag.GetBool("downedRegaroth");
            completedRadiantRain = tag.GetBool("completedRadiantRain");
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
            sparedAzana = tag.GetBool("sparedAzana");
            downedAncients = tag.GetBool("downedAncients");
            downedCosmicObserver = tag.GetBool("downedCosmicObserver");
            downedRadiantMaster = tag.GetBool("downedRadiantMaster");
            downedErius = tag.GetBool("downedErius");
            downedMineBoss = tag.GetBool("downedMineBoss");
            downedKeeper = tag.GetBool("downedKeeper");
            downedGiantPinky = tag.GetBool("downedGiantPinky");

            cultArrived = tag.GetBool("cultArrived");

            downedVoidEvent = tag.GetBool("downedVoidEvent");

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

            desertPrompt = tag.GetInt("desertPrompt");
            firePrompt = tag.GetInt("firePrompt");
            skyPrompt = tag.GetInt("skyPrompt");
            frostPrompt = tag.GetInt("frostPrompt");
            waterPrompt = tag.GetInt("waterPrompt");
            voidPrompt = tag.GetInt("voidPrompt");

            radiantRain = tag.GetBool("radiantRain");

            starShowerTime = tag.GetInt("starShowerTime");

            spokenToCindari = tag.GetBool("spokenToCindari");
            plateauWeather = tag.GetInt("plateauWeather");
            plateauWeatherTime = tag.GetInt("plateauWeatherTime");
            awakenedPlateau = tag.GetBool("awakenedPlateau");
            mechBlueprints = tag.GetInt("mechBlueprints");

            //load npcs
            int toLoad = tag.GetInt("numSaved");
            for (int i = 0; i < toLoad; i++)
            {
                int type = tag.GetInt("npcType" + i);
                NPC nPC = Main.npc[NPC.NewNPC(0, 0, type)];
                nPC.GivenName = tag.GetString("givenName" + i);
                nPC.position.X = tag.GetFloat("posX" + i);
                nPC.position.Y = tag.GetFloat("posY" + i);
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
            flags1[5] = completedRadiantRain;
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
            flags3[3] = sparedAzana;
            flags3[4] = downedRadiantMaster;
            flags3[5] = downedErius;
            flags3[6] = downedMineBoss;
            flags3[7] = downedKeeper;
            writer.Write(flags3);

            BitsByte downed4 = new BitsByte();
            downed4[0] = downedGiantPinky;
            writer.Write(downed4);

            // lab gen & drive obtained
            BitsByte flags4 = new BitsByte();
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

            //random
            BitsByte flags6 = new BitsByte();
            flags6[0] = awakenedMode;
            flags6[3] = radiantRain;
            flags6[4] = prevTickRaining;
            flags6[5] = voidInvasionUp;
            flags6[6] = cultArrived;
            writer.Write(flags6);
            // plateaus
            BitsByte flags7 = new BitsByte();
            flags7[0] = spokenToCindari;
            flags7[1] = awakenedPlateau;
            writer.Write(flags7);
            writer.Write(volcShrineActivated);
            writer.Write(mechBlueprints);
            writer.Write(plateauWeather);
            writer.Write(plateauWeatherTime);



            // prompts
            writer.Write(desertPrompt);
            writer.Write(firePrompt);
            writer.Write(skyPrompt);
            writer.Write(frostPrompt);
            writer.Write(waterPrompt);
            writer.Write(voidPrompt);

            writer.Write(starShowerTime);

            //kills
            writer.Write(moonlordKills);
            writer.Write(voidLeviathanKills);

            //writer.Write(encounter);
            //writer.Write(encounterShakeTimer);
            //writer.Write(encounterTimer);

        }
        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags1 = reader.ReadByte();
            downedAqueous = flags1[0];
            downedVoidLeviathan = flags1[1];
            downedInfernace = flags1[2];
            downedScourgeFighter = flags1[3];
            downedRegaroth = flags1[4];
            completedRadiantRain = flags1[5];
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
            sparedAzana = flags3[3];
            downedRadiantMaster = flags3[4];
            downedErius = flags3[5];
            downedMineBoss = flags3[6];
            downedKeeper = flags3[7];

            BitsByte downed4 = reader.ReadByte();
            downedGiantPinky = downed4[0];

            // lab gen & drive obtained
            BitsByte flags4 = reader.ReadByte();
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

            //random 
            BitsByte flags6 = reader.ReadByte();
            awakenedMode = flags6[0];
            radiantRain = flags6[3];
            prevTickRaining = flags6[4];
            voidInvasionUp = flags6[5];
            cultArrived = flags6[6];

            BitsByte flags7 = reader.ReadByte();
            spokenToCindari = flags7[0];
            awakenedPlateau = flags7[1];

            volcShrineActivated = reader.ReadInt32();
            mechBlueprints = reader.ReadInt32();
            plateauWeather = reader.ReadInt32();
            plateauWeatherTime = reader.ReadInt32();

            desertPrompt = reader.ReadInt32();
            firePrompt = reader.ReadInt32();
            skyPrompt = reader.ReadInt32();
            frostPrompt = reader.ReadInt32();
            waterPrompt = reader.ReadInt32();
            voidPrompt = reader.ReadInt32();

            starShowerTime = reader.ReadInt32();

            moonlordKills = reader.ReadInt32();
            voidLeviathanKills = reader.ReadInt32();
        }
       
        public override void PostUpdate()
        {
            Player player = Main.player[Main.myPlayer];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            generalTimer++;
            fastRandUpdate--;

            bool anyBoss = false;
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                NPC nPC = Main.npc[i];
                if (nPC.boss && nPC.active) anyBoss = true;
            }
            // star shower
            int showerRate = 16000;
            float worldScale = (float)(Main.maxTilesX / 4200);
            if (starShowerTime > 0)
            {
                starShowerTime--;
                if (Main.dayTime)
                {
                    starShowerTime = 1;
                }
                if (starShowerTime == 1)
                {
                    Main.NewText("The star shower comes to an end...");
                    starShowerTime = Main.rand.Next(-8, -1); // most amount of nights that can happen before it starts
                }
                showerRate = 1000;
                // fallen star code again so twice as many stars
                if ((float)Main.rand.Next(8000) < 10f * worldScale)
                {
                    int num138 = Main.rand.Next(Main.maxTilesX - 50) + 100;
                    num138 *= 16;
                    int num139 = Main.rand.Next((int)((double)Main.maxTilesY * 0.05));
                    num139 *= 16;
                    Vector2 vector = new Vector2((float)num138, (float)num139);
                    float num140 = (float)Main.rand.Next(-100, 101);
                    float num141 = (float)(Main.rand.Next(200) + 100);
                    float num142 = (float)Math.Sqrt((double)(num140 * num140 + num141 * num141));
                    num142 = 12f / num142;
                    num140 *= num142;
                    num141 *= num142;
                    Projectile.NewProjectile(vector.X, vector.Y, num140, num141, 12, 1000, 10f, Main.myPlayer, 0f, 0f);
                }
            }
            else if (starShowerTime <= 0)
            {
                if (!Main.dayTime && Main.time == 0 && NPC.downedMoonlord)
                {
                    starShowerTime++;
                    if (starShowerTime == 0)
                    {
                        starShowerTime = Main.rand.Next(3600 * 2, 3600 * 9);
                        Main.NewText("The heavens rain gifts down upon the land...");
                    }
                }
            }
            if (!Main.dayTime && NPC.downedMoonlord)
            {
                if ((float)Main.rand.Next(showerRate) < 10f * worldScale)
                {
                    int num138 = Main.rand.Next(Main.maxTilesX - 50) + 100;
                    num138 *= 16;
                    int num139 = Main.rand.Next((int)((double)Main.maxTilesY * 0.05));
                    num139 *= 16;
                    Vector2 vector = new Vector2((float)num138, (float)num139);
                    Projectile.NewProjectile(vector.X, vector.Y, -12, 12, ProjectileType<Projectiles.Environmental.StellariumMeteor>(), 1000, 10f, Main.myPlayer, 0f, 0f);
                }

            }



            if (!GetInstance<Config>().promptsDisabled)
            {
                if (NPC.downedBoss1 && !downedWasteland) desertPrompt++;
                else desertPrompt = 0;
                if (NPC.downedBoss3 && !downedInfernace) firePrompt++;
                else firePrompt = 0;
                if (NPC.downedMechBossAny && !downedRegaroth) skyPrompt++;
                else skyPrompt = 0;
                if (NPC.downedPlantBoss && !downedPermafrost) frostPrompt++;
                else frostPrompt = 0;
                if (NPC.downedFishron && !downedAqueous) waterPrompt++;
                else waterPrompt = 0;
                if (downedVolcanox && !downedVoidLeviathan) voidPrompt++;
                else voidPrompt = 0;

                if (skyPrompt > ElementsAwoken.bossPromptDelay)
                {
                    if (!Main.raining && skyPromptRainCD <= 0)
                    {
                        skyPromptRainCD = 3600;
                        Main.raining = true;
                        Main.rainTime = 18000;
                        ElementsAwoken.DebugModeText("Sky Prompt Rain Started");
                    }
                    if (Main.raining) skyPromptRainCD--;
                }
                if (frostPrompt > ElementsAwoken.bossPromptDelay)
                {
                    if (hailStormTime <= 0 && Main.rand.Next(40000) == 0)
                    {
                        hailStormTime = Main.rand.Next(1800, 7200);
                    }
                }

                if (hailStormTime > 0)
                {
                    hailStormTime--;
                }
            }
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

            #region encounters
            //if (encounter != 0)
            {
                /*encounterTimer--;
                if (encounterTimer <= 0)
                {
                    encounterTimer = 0;
                    encounter = 0;
                }
                if (!encounterSetup)
                {
                    encounterSetup = true;
                    modPlayer.encounterTextTimer = 0;
                    if (encounter >= 2)
                    {
                        Main.rainTime = 3600;
                        Main.raining = true;
                        Main.maxRaining = 0.8f;
                    }
                }
                if (encounter == 1)
                {
                    if (encounterTimer <= 1600) encounterTimer = 0; // cut the time of the event in half 
                }
                if (encounter == 2)
                { 
                    if (!Main.gameMenu)
                    {
                        Main.time += Main.rand.Next(8, 25);
                        if (Main.time > 32400.0 - 30)
                        {
                            Main.time = 0;
                        }
                    }
                }
                if (encounter == 3)
                {
                    Main.time = 16220;
                    Main.dayTime = false;
                }
                if (modPlayer.encounterTextTimer > 0 || encounter == 3)
                {
                    ElementsAwoken.DebugModeText("Encounter Text Timer: " + modPlayer.encounterTextTimer);
                    //if (encounterTimer % 3 == 0) ElementsAwoken.DebugModeText("encounter moonlord shake. encounter 3: " + encounter3);
                    float intensity = MathHelper.Clamp((1 + (float)Math.Sin((float)modPlayer.encounterTextTimer / 20f)) * 0.25f, 0f, 1f);
                    if (encounter == 3)intensity += 0.3f;
                    if (modPlayer.finalText)intensity = 1f;
                    //Main.NewText("" + intensity);
                    MoonlordShake(intensity,player);
                }*/
            }
            #endregion

            if (cultArrived)
            {
                if (Main.time == 0 && !Main.dayTime && !NPC.AnyNPCs(NPCType<OrderCultist>()))
                {
                    int n = NPC.NewNPC(Main.spawnTileX * 16, Main.spawnTileY * 16, NPCType<OrderCultist>());
                    OrderCultist cultist = (OrderCultist)Main.npc[n].modNPC;
                    Main.npc[n].GivenName = cultist.TownNPCName();
                    Main.NewText(Main.npc[n].GivenName + " of the Order appears...", new Color(41, 33, 46));
                    if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
                }
            }

            #region DotV
            voidInvasionFinished--;
            if (voidInvasionFinished <= 0)
            {
                voidInvasionFinished = 0;
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
                if (!NPC.AnyNPCs(mod.NPCType("ShadeWyrmHead")) && (Main.time == 19800 || Main.time == 27000)) NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("ShadeWyrmHead")); // at 1 and 3
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
                        if (VoidEvent.phase1NPCs.Contains(other.type))
                        {
                            other.alpha += 5;
                            if (other.alpha >= 255)
                            {
                                other.active = false;
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
                        if (Main.netMode == 0) Main.NewText("Laughs echo throughout the world", 182, 15, 15, false);
                        if (Main.netMode == 2) NetMessage.BroadcastChatMessage(NetworkText.FromLiteral("Laughs echo throughout the world"), new Color(182, 15, 15));
                        Main.PlaySound(29, (int)Main.player[i].position.X, (int)Main.player[i].position.Y, 105, 1f, 0f);
                        voidInvasionUp = false;
                        voidInvasionFinished = 200;
                    }
                }
            }
            else if (voidInvasionFinished > 0)
            {
                for (int k = 0; k < Main.npc.Length; k++)
                {
                    NPC other = Main.npc[k];
                    if (VoidEvent.phase1NPCs.Contains(other.type) || VoidEvent.phase2NPCs.Contains(other.type))
                    {
                        other.alpha += 3;
                        if (other.alpha >= 255)
                        {
                            other.active = false;
                        }
                    }
                }
            }

            if (volcShrineActivated != 0)
            {
                volcShrineActivated++;
                if (volcShrineActivated >= 600)
                {
                    volcShrineActivated = 0;
                }
            }

            if (voidInvasionWillStart)
            {
                if (!Main.dayTime)
                {
                    VoidEvent.StartInvasion();
                    Main.invasionX = Main.spawnTileX; // instantly start
                    voidInvasionWillStart = false;
                    Filters.Scene.Deactivate("MoonLordShake", new object[0]);
                }
                float intensity = MathHelper.Clamp((float)Main.time / 54000f, 0f, 1f);
                MoonlordShake(intensity);
                //Main.NewText("time" + " " + Main.time + " " + "intensity" + " " + intensity, Color.White.R, Color.White.G, Color.White.B);
            }
            #endregion
            if (downedAzana || sparedAzana)
            {
                if (Main.raining && !prevTickRaining)
                {
                    if (Main.rand.NextBool(4) || !completedRadiantRain)
                    {
                        radiantRain = true;
                        Main.NewText("The rain glistens with unknown magic...", Color.HotPink);
                    }
                }
            }
            if (radiantRain)
            {
                if (Main.rainTime == 7200) NPC.SpawnOnPlayer(player.whoAmI, NPCType<RadiantMaster>());
                if (!Main.raining)
                {
                    radiantRain = false;
                    completedRadiantRain = true;
                    Main.NewText("The radiant rain dissipates...", Color.HotPink);
                }
                int amount = Main.expertMode ? awakenedMode ? 100 : 120 : 240;
                if (Main.rand.NextBool(120))
                {
                    for (int i = 0; i < Main.maxPlayers; i++)
                    {
                        Player starShowerP = Main.player[i];
                        if (starShowerP.active)
                        {
                            Vector2 pos = new Vector2(starShowerP.Center.X - Main.rand.Next(-1000, 1000), starShowerP.Center.Y - 1000);
                            Vector2 vel = new Vector2(Main.windSpeed * 10, 10f);
                            Projectile proj = Main.projectile[Projectile.NewProjectile(pos.X, pos.Y, vel.X, vel.Y, ProjectileType<RadiantStarRain>(), Main.expertMode ? awakenedMode ? 150 : 100 : 75, 10f, Main.myPlayer, 0f, 0f)];
                            proj.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                        }
                    }
                }
            }
            prevTickRaining = Main.raining;
            if (!awakenedPlateau && Main.netMode != NetmodeID.SinglePlayer) awakenedPlateau = Main.hardMode;
            if (!awakenedPlateau)
            {
                plateauWeather = 0;
                plateauWeatherTime = 0;
            }
            if (plateauWeatherTime > 0 && plateauWeather >= 0 && !anyBoss)
            {
                plateauWeatherTime--;
            }
            else if (plateauWeatherTime == 0 && plateauWeather > 0)
            {
                plateauWeather = -1;
                plateauWeatherTime = 3600;
            }
            else if (plateauWeather < 0)
            {
                plateauWeatherTime--;
                if (plateauWeatherTime <= 0)
                    plateauWeather = 0;
            }
            else if (plateauWeatherTime <= 0 && plateauWeather == 1)
            {
                plateauWeatherTime++;
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    MyPlayer plateauPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();
                    if (plateauPlayer.zonePlateau) plateauPlayer.screenshakeAmount = 5f;
                }
                else
                {
                    for (int i = 0; i < Main.player.Length; i++)
                    {
                        MyPlayer plateauPlayer = Main.player[i].GetModPlayer<MyPlayer>();
                        if (plateauPlayer.zonePlateau) plateauPlayer.screenshakeAmount = 5f;
                    }
                }
                if (plateauWeatherTime >= 0) plateauWeatherTime = Main.rand.Next(8000, 16000);
            }
            if (awakenedPlateau && plateauWeather == 0)
            {
                if (Main.rand.NextBool(18000)) // 20% every minute
                {
                    string text = "";
                    Color color = Color.Orange;
                    int riftingChance = (downedVolcanox && !downedVoidLeviathan) ? 5 : 50;
                    if (Main.rand.NextBool(riftingChance) && !downedVoidLeviathan)
                    {
                        text = "Hell grows quiet...";
                        plateauWeatherTime = Main.rand.Next(3600 * 2, 3600 * 3);
                        plateauWeather = 3;
                        color = Color.Purple;
                    }
                    else
                    {
                        int choice = Main.rand.Next(2);
                        if (choice == 0)
                        {
                            text = "Sulfur chokes the air...";
                            plateauWeatherTime = -200;
                            plateauWeather = 1;
                        }
                        else
                        {
                            text = "The lake rumbles violently...";
                            plateauWeatherTime = Main.rand.Next(8000, 16000);
                            plateauWeather = 2;
                        }
                    }
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        MyPlayer plateauPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();
                        if (plateauPlayer.zonePlateau) Main.NewText(text, color);
                    }
                    else
                    {
                        for (int i = 0; i < Main.player.Length; i++)
                        {
                            MyPlayer plateauPlayer = Main.player[i].GetModPlayer<MyPlayer>();
                            if (plateauPlayer.zonePlateau) Main.NewText(text, color);
                        }
                    }

                }
            }
        }
        public override void PreUpdate()
        {
            int spawnRate = 0;
            int maxSpawns = 0;
            if (EAWorldGen.generatedPlateaus) // i want to spawn NPCs in lava why is this so annoying
            {
                Point lakePos = new Point(EAWorldGen.plateauLoc.X + 510, EAWorldGen.plateauLoc.Y + 87);
                bool playerNearLake = false;
                Player player = null;
                for (int p = 0; p < Main.maxPlayers; p++)
                {
                    if (!Main.player[p].active) continue;
                    player = Main.player[p];
                    Vector2 playerTile = player.Center / 16;
                    NPCLoader.EditSpawnRate(player, ref spawnRate, ref maxSpawns);
                    if (playerTile.X > lakePos.X - 60 && playerTile.X < lakePos.X + 60 + 329 && playerTile.Y > Main.maxTilesY - 200)
                    {
                        playerNearLake = true;
                        break;
                    }
                }
                if (playerNearLake)
                {
                    // spawn platforms in lake
                    int numPlats = NPC.CountNPCS(NPCType<NPCs.MovingPlatforms.LavaPlatform>());
                    while (numPlats < 9)
                    {
                        Point spawnPos = GetLakeSpawnPos(player);
                        Tile spawnTile = Framing.GetTileSafely(spawnPos);
                        if (spawnTile.lava())
                        {
                            NPC npc = Main.npc[NPC.NewNPC(spawnPos.X * 16, spawnPos.Y * 16, NPCType<NPCs.MovingPlatforms.LavaPlatform>())];
                            //ElementsAwoken.DebugModeText("platform " + numPlats);
                            numPlats++;
                        }
                    }
                    if (spawnRate > 0)
                    {
                        if (Main.rand.NextBool(spawnRate * 2) && player.activeNPCs < maxSpawns)
                        {
                            Point spawnPos = GetLakeSpawnPos(player);
                            Tile spawnTile = Framing.GetTileSafely(spawnPos);
                            if (spawnTile.lava())
                            {
                                NPC npc = Main.npc[NPC.NewNPC(spawnPos.X * 16, spawnPos.Y * 16, ChooseLakeEnemy())];
                                ElementsAwoken.DebugModeText("Spawned " + npc.FullName + " in lake");
                                //Main.LocalPlayer.position = spawnPos.ToWorldCoordinates();
                            }
                        }
                    }
                }
            }
        }
        private static Point GetLakeSpawnPos(Player player)
        {
            Point lakePos = new Point(EAWorldGen.plateauLoc.X + 510, EAWorldGen.plateauLoc.Y + 87);

            Point spawnPos = player.Center.ToTileCoordinates();
            int trials = 0;
            while (PlayerUtils.OnScreen(spawnPos.ToWorldCoordinates()) && trials < 500)
            {
                spawnPos = new Point(Main.rand.Next(329), Main.rand.Next(79));
                spawnPos.X += lakePos.X;
                spawnPos.Y += lakePos.Y;
                trials++;
            }
            return spawnPos;
        }
        private List<int> LakeEnemy = new List<int>();
        private List<int> LakeEnemyWeights = new List<int>();
        private int numEnemies = 0;
        private int ChooseLakeEnemy()
        {
            LakeEnemy = new List<int>();
            LakeEnemyWeights = new List<int>();
            numEnemies = 0;
            int type = 0;
            AddLakeEnemy(NPCType<CharredCod>(), 30);
            AddLakeEnemy(NPCType<Sparkeye>(), 30);
            //if (NPC.CountNPCS(NPCType<NPCs.MovingPlatforms.LavaPlatform>()) < 10) AddLakeEnemy(NPCType<NPCs.MovingPlatforms.LavaPlatform >(), 60);
            if (awakenedPlateau) AddLakeEnemy(NPCType<InsidiousScorchfin>(), 30);
            int totalWeight = LakeEnemyWeights.Sum();
            int num = Main.rand.Next(0, totalWeight + 1);
            for (int i = 0; i < numEnemies; i++)
            {
                if (num < LakeEnemyWeights[i])
                    return LakeEnemy[i];
                num -= LakeEnemyWeights[i];
            }
                //int num = Main.rand.Next(0, totalWeight + 1);
                //if (num < 30) type = NPCType<CharredCod>();
                //else if (num < 60) type = NPCType<Sparkeye>();
                //else if (num < 90) type = NPCType<InsidiousScorchfin>();
                return type;
        }

        private void AddLakeEnemy(int type, int weight)
        {
            LakeEnemy.Add(type);
            LakeEnemyWeights.Add(weight);
            numEnemies++;
        }

        public override void TileCountsAvailable(int[] tileCounts)
        {
            SkyTiles = tileCounts[TileID.Cloud];
            lizardTiles = tileCounts[TileID.LihzahrdBrick];
            plateauTiles = tileCounts[TileType<MalignantFlesh>()];

            corruptionTiles = tileCounts[TileID.CorruptGrass] + tileCounts[TileID.CorruptHardenedSand] + tileCounts[TileID.CorruptIce] + tileCounts[TileID.CorruptSandstone] + tileCounts[TileID.CorruptThorns] + tileCounts[TileID.Ebonsand] + tileCounts[TileID.Ebonstone];
            crimsonTiles = tileCounts[TileID.FleshGrass] + tileCounts[TileID.CrimsonHardenedSand] + tileCounts[TileID.FleshIce] + tileCounts[TileID.CrimsonSandstone] + tileCounts[TileID.CrimtaneThorns] + tileCounts[TileID.Crimsand] + tileCounts[TileID.Crimstone];
            hallowedTiles = tileCounts[TileID.HallowedGrass] + tileCounts[TileID.HallowHardenedSand] + tileCounts[TileID.HallowedIce] + tileCounts[TileID.HallowedPlants] + tileCounts[TileID.HallowedPlants2] + tileCounts[TileID.HallowSandstone] + tileCounts[TileID.Pearlsand] + tileCounts[TileID.Pearlstone];
        }
        private void MoonlordShake(float intensity)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Player player = Main.LocalPlayer;
                if (!Filters.Scene["MoonLordShake"].IsActive())
                {
                    Filters.Scene.Activate("MoonLordShake", player.position, new object[0]);
                }
            }
            else
            {
                for (int i = 0; i < Main.player.Length; i++)
                {
                    Player p = Main.player[i];
                    if (p.active)
                    {
                        if (!Filters.Scene["MoonLordShake"].IsActive())
                        {
                            Filters.Scene.Activate("MoonLordShake", p.position, new object[0]);
                        }
                    }
                }
            }
            Filters.Scene["MoonLordShake"].GetShader().UseIntensity(intensity);
        }      
    }
    public class CreditsLight : GlobalWall
    {
        public override void ModifyLight(int i, int j, int type, ref float r, ref float g, ref float b)
        {
            Player player = Main.player[Main.myPlayer];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.creditsTimer >= 0)
            {
                if (Main.tile[i, j].active() == false && modPlayer.creditsTimer >= modPlayer.screenTransDuration / 2)
                {
                    r = 0.7f;
                    g = 0.7f;
                    b = 0.7f;
                }
            }
        }
    }
}