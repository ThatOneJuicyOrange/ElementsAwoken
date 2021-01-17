using ElementsAwoken.Buffs;
using ElementsAwoken.Buffs.Cooldowns;
using ElementsAwoken.Buffs.Debuffs;
using ElementsAwoken.Buffs.PotionBuffs;
using ElementsAwoken.Items.ItemSets.HiveCrate;
using ElementsAwoken.Mounts;
using ElementsAwoken.NPCs;
using ElementsAwoken.NPCs.Bosses.VoidLeviathan;
using ElementsAwoken.Projectiles;
using ElementsAwoken.Projectiles.Other;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken
{
    public class MyPlayer : ModPlayer
    {
        private bool calamityEnabled = ModLoader.GetMod("CalamityMod") != null;

        public bool voidBlood = false;
        public int generalTimer = 0;

        public bool hoveringLiftableNPC = false;

        public bool cantUseItems = false;

        public int flareberryDrugged = 0;
        public int flareberryDruggedDuration = 3600;

        public int stickyBreak = 0;

        public int platformDropTimer = 0;


        public int npcHeld = 0;
        public int npcHeldThowCD = 0;

        // plateau stuff
        public bool gasMask = false;

        public int ironBoots = 0;
        public int ironBootsSoundDelay = 0;
        public int drakoniteGoggles = 0;

        // lorekeepers book
        public bool tomeUI = false;

        public string tomeText = "";
        public Texture2D tomeTex = null;
        public int tomeTexHeight = 0;

        public bool glassHeart = false;

        public bool inMech = false;
        public Vector2 drillEnd = Vector2.Zero;
        public Vector2 gunEnd = Vector2.Zero;

        public bool cantSeeHoverText = false;

        #region boot wings
        public bool flyingBoots = false;
        public Item attachedWings = null;
        public float wingTimeMult = 1f;
        public float wingSpdXMult = 1f;
        public float wingAccXMult = 1f;
        public float wingSpdYMult = 1f;
        public float wingAccYMult = 1f;
        #endregion

        public int sansUseCD = 0;
        public int sansNote = 0;

        public int platformWalkFrame = 0;
        public double platformWalkCounter = 0;

        public bool talkToAzana = false;

        public int saveAmmo = 0;

        public bool criticalHeat = false;
        public int criticalHeatCD = 0;
        public int criticalHeatTimer = 0;
        public int criticalHeatMax = 900;
        public float fireResistance = 0;

        public int toySlimed = 0;
        public int toySlimedID = -1;

        public bool plateauCinematic = false;
        public int plateauCinematicCounter = 0;

        public int lastChest;

        #region minions

        public bool fireElemental = false;
        public bool miniatureSandStorm = false;
        public bool babyPuff = false;
        public bool happyCloud = false;
        public bool bubble = false;
        public bool enchantedTrio = false;
        public bool gWorm = false;
        public bool soulSkull = false;
        public bool aqueousMinions = false;
        public bool bloodDiamond = false;
        public bool iceAxe = false;
        public bool eyeballMinion = false;
        public bool scorpionMinion = false;
        public bool hearthMinion = false;
        public bool energySpirit = false;
        public bool icicleMinion = false;
        public bool deathwatcher = false;
        public bool phantomHook = false;
        public bool volcanicTentacle = false;
        public bool abyssCultist = false;
        public bool miniDragon = false;
        public bool azanaMinions = false;
        public bool coalescedOrb = false;
        public bool cosmicObserver = false;
        public bool kirovAirship = false;
        public bool wokeMinion = false;
        public bool corruptPenguin = false;
        public bool toyRobot = false;
        public bool miniVlevi = false;
        public bool corroder = false;
        public bool crystalEntity = false;
        public bool putridRipper = false;
        public bool globule = false;
        #endregion minions

        #region pets

        public bool voidCrawler = false;
        public bool lilOrange = false;
        public bool woke = false;
        public bool royalEye = false;
        public bool possessedHand = false;
        public bool babyShadeWyrm = false;
        public bool turboDoge = false;
        public bool wyvernPet = false;
        public bool chamchamRat = false;
        public bool stellate = false;

        #endregion pets

        #region debuffs

        public bool iceBound = false;
        public bool endlessTears = false;
        public bool extinctionCurse = false;
        public bool handsOfDespair = false;
        public bool dragonfire = false;
        public bool discordDebuff = false;
        public bool brokenWings = false;
        public bool chaosBurn = false;
        public bool acidBurn = false;
        public bool superSlow = false;
        public int behemothGazeTimer = 0;
        public int leviathanDist = 0;
        public bool starstruck = false;
        public int starstruckCounter = 0;
        public bool incineration = false;
        public bool choking = false;
        public bool acidWebbed = false;
        public int acidWebbedID = -1;

        public bool inQuicksand = false;
        public bool voidMossStanding = false;

        #endregion debuffs

        #region buffs

        public bool extinctionCurseImbue = false;
        public bool starstruckImbue = false;
        public bool discordantPotion = false;
        public bool superSpeed = false;
        public bool vilePower = false;
        public bool hellFury = false;

        #endregion buffs

        #region other

        public bool dashCooldown = false;
        public bool venomSample = false;
        public bool ancientDecayWeapon = false;
        public bool medicineCooldown = false;
        public bool lightningCloud = false;
        public bool lightningCloudHidden = false;
        public float lightningCloudCharge = 0;
        public bool frozenGauntlet = false;
        public bool cantFly = false;
        public bool cantROD = false;
        public bool cantMagicMirror = false;
        public bool cantGrapple = false;
        public bool puffFall = false;
        public bool replenishRing = false;
        public bool neovirtuoBonus = false;
        public bool immortalResolve = false;
        public bool chaosRing = false;
        public bool voidLantern = false;
        public bool heartContainer = false;
        public bool noRespawnTime = false;
        public bool flare = false;
        public int flareShieldCD = 0;
        public bool scourgeDrive = false;
        public bool scourgeSpeed = false;
        public bool spikeBoots = false;
        public bool templeSpikeBoots = false;
        public bool sonicArm = false;
        public bool nyanBoots = false;
        public bool voidBoots = false;
        public bool theAntidote = false;
        public bool cosmicGlass = false;
        public int cosmicGlassCD = 0;
        public bool sufferWithMe = false;
        public bool strangeUkulele = false;
        public bool crystallineLocket = false;
        public bool eaMagmaStone = false;
        public bool meteoricPendant = false;
        public int crystallineLocketCrit = 0;
        public bool prismPolish = false;
        public bool fireAcc = false;
        public int fireAccCD = 0;
        public int boostDrive = 0;
        public int boostDriveTimer = 0;

        public bool honeyCocoon = false;
        public int honeyCocooned = 0;
        public int honeyCocoonDamage = 0;

        public bool wispForm = false;
        public bool forceWisp = false;
        public int wispDust = 6;
        public int spirit = 0;
        public int spiritRegenCD = 0;
        public int spiritTimer = 0;
        public int spiritMax = 100;
        public int wispAttackCD = 0;

        public int noDamageCounter = 0;

        //amulet of despair
        public int voidEnergyCharge = 0;

        public int voidEnergyTimer = 0;

        //infinity guantlet and stones
        public int overInfinityCharged = 0;

        public bool infinityDeath = false;

        #endregion other

        #region cinematics

        // credits
        public int creditsTimer = -1;

        public Vector2 desiredScPos = new Vector2();

        public List<CreditPoint> creditPoints = new List<CreditPoint>();

        //public List<Vector2> creditPoints = new List<Vector2>();
        //public List<Vector2> creditPointsScroll = new List<Vector2>();
        //public List<string> creditPointsName = new List<string>();
        //public List<int> creditPointsValue = new List<int>();
        public int pointsNotFound = 0;
        public int startTime = 0;
        public bool startDayTime = false;

        public bool screenTransition = false;
        public float screenTransAlpha = 0f;
        public float screenTransTimer = 0f;
        public int screenTransDuration = 60; // in frames

        public int screenDuration = 60 * 9;
        public int escHeldTimer = 0;

        // obsidious
        public Vector2 startScreen = new Vector2();
        #endregion cinematics

        #region skyline whirlwind

        public bool skylineFlying = false;
        public float skylineAlpha = 0f;
        public int skylineFrameTimer = 0;
        public int skylineFrame = 0;

        #endregion skyline whirlwind

        public bool[] mysteriousPotionsDrank;

        #region armor bonuses

        public int empyreanCloudCD = 0;
        public bool oceanicArmor = false;
        public bool voidArmor = false;
        public int voidArmorHealCD = 0;
        public bool dragonmailGreathelm = false;
        public bool dragonmailHood = false;
        public bool dragonmailMask = false;
        public bool dragonmailVisage = false;
        public bool elementalArmor = false;
        public bool elementalArmorCooldown = false;
        public bool superbaseballDemon = false;
        public bool gelticConqueror = false;
        public bool crowsArmor = false;
        public int crowsArmorCooldown = 0;
        public bool toyArmor = false;
        public int toyArmorCooldown = 0;
        public bool forgedArmor = false;
        public bool flingToShackle = false;
        public int forgedShackled = 0;
        public int shackleFlingCooldown = 0;
        public int voidWalkerArmor = 0; // for multiple helms
        public int voidWalkerCooldown = 0;
        public int voidWalkerAura = 0;
        public bool voidWalkerChest = false;
        public int voidWalkerRegen = 0;
        public bool energyWeaverArmor = false;
        public int energyWeaverTimer = 0;
        public bool cosmicalusArmor = false;
        public bool mechArmor = false;
        public int mechArmorCD = 0;
        public bool awokenWood = false;
        public bool arid = false;
        public int aridTimer = 0;
        public float aridFalling = 0;
        public bool putridArmour = false;

        #endregion armor bonuses

        #region biome or region zones
        public bool zoneTemple = false;

        public bool zonePlateau = false;
        public bool zoneSulphur = false;
        public bool zoneErius = false;
        public bool zoneMineBoss = false;
        #endregion
        // dash & hypothermia
        public bool ninjaDash = false;

        public bool viridiumDash = false;
        public bool canGetHypo = false;
        public int hypoChillTimer = 0;
        public int dashDustTimer = 0;

        #region timers and cooldowns

        public int neovirtuoTimer = 0;
        public float chaosBoost = 0;
        public float chaosDamageBoost = 0;
        public int masterSwordCharge = 0;
        public float masterSwordCountdown = 0;
        public float immortalResolveCooldown = 0;
        public float hellsReflectionTimer = 0;
        public float hellsReflectionCD = 0;
        public float voidPortalCooldown = 0;

        #endregion timers and cooldowns

        public int voidTimeChangeTime = 0;

        public Vector2 currentMechStation = new Vector2();

        // aegis
        public bool vleviAegis = false;

        public int vleviAegisDamage = 0;
        public int vleviAegisBoost = 0;
        public int aegisDashTimer = 0;
        public int aegisDashCooldown = 0;
        public int aegisDashDir = 1;

        // computer
        public bool inComputer = false;

        public Vector2 computerPos = new Vector2();
        public int computerTextNo = 0;
        public int guardianEntryNo = 0;
        public int azanaEntryNo = 0;
        public int ancientsEntryNo = 0;
        public string computerText = "";

        // toy slime
        public int toySlimeChanceTimer = 0;

        public int observerChanceTimer = 0;

        // damage modifiers
        public float damageTaken = 1f;

        #region stat increases

        public int voidHeartsUsed = 0;
        public int chaosHeartsUsed = 0;
        public int lunarStarsUsed = 0;
        public int statManaMax3 = 0;

        public int shieldHearts = 0;
        public int shieldLife = 0;

        public bool extraAccSlot = false;

        public bool voidCompressor = false;

        #endregion stat increases

        // oinite statue
        public bool oiniteStatue = false;

        public bool[] oiniteDoubledBuff = new bool[Player.MaxBuffs];

        #region info accessories
        public int buffDPSCount = 0;

        public int buffDPS = 0;
        public bool alchemistTimer = false;
        public bool[] hideEAInfo = new bool[3];
        public bool dryadsRadar = false;
        public string nearbyEvil = "No evil";
        public bool rainMeter = false;
        #endregion

        #region encounters
        public string encounterText = "";

        public int encounterTextAlpha = 0;
        public int encounterTextTimer = 0;
        public bool finalText = false;
        #endregion
        // screenshake
        public float screenshakeAmount = 0; // dont go above 15 unless you want to have a seizure
        public int screenshakeTimer = 0;

        // abilities
        public bool canSandstormA;
        public bool canTimeA;
        public bool canRainA;
        public bool canWispA;
        public int abilityTimer = 0;
        public int timeAbilityTimer = 0;

        public string aboveHeadText = "";
        public int aboveHeadTimer = 0;
        public int aboveHeadDuration = 420;

        public int eaDash = 0;
        public int eaDashTime = 0;
        public int eaDashDelay = 0;

        #region awakened mode itmes
        public bool toySlimeClaw = false;
        public int toySlimeClawCD = 0;
        public bool toySlimeClawSliding = false;

        public bool slimeBooster = false;

        public bool greatLens = false;
        public int greatLensTimer = 0;

        public bool bleedingHeart = false;

        public bool crystalNectar = false;

        public bool fadedCloth = false;

        public bool hellHeart = false;

        public bool icyHeart = false;
        public int icyHeartTimer = 0;
        public float icyHeartDR = 1;

        public int aeroflakHits = 0;
        public int aeroflakTimer = 0;

        public bool abyssalMatter = false;
        public int abyssalRage = 0;

        public bool radiantCrown = false;

        public Vector2 archaicProtectionPos = new Vector2();
        public int archaicProtectionTimer = 0;
        #endregion

        // plateau
        public float sulphurBreath = 0;

        public float sulphurBreathMax = 200;

        public static bool[] tempCanFishInLava = null;

        public override void Initialize()
        {
            mysteriousPotionsDrank = new bool[10];
            voidHeartsUsed = 0;
            chaosHeartsUsed = 0;
            lunarStarsUsed = 0;

            creditsTimer = -1;

            attachedWings = null;
        }

        public override void ResetEffects()
        {
            cantUseItems = false;

            talkToAzana = false;
            saveAmmo = 0;

            hoveringLiftableNPC = false;

            fireResistance = 0;

            #region minions

            fireElemental = false;
            miniatureSandStorm = false;
            babyPuff = false;
            bubble = false;
            happyCloud = false;
            enchantedTrio = false;
            gWorm = false;
            soulSkull = false;
            aqueousMinions = false;
            bloodDiamond = false;
            venomSample = false;
            iceAxe = false;
            eyeballMinion = false;
            scorpionMinion = false;
            hearthMinion = false;
            energySpirit = false;
            icicleMinion = false;
            deathwatcher = false;
            phantomHook = false;
            volcanicTentacle = false;
            abyssCultist = false;
            miniDragon = false;
            azanaMinions = false;
            coalescedOrb = false;
            cosmicObserver = false;
            kirovAirship = false;
            wokeMinion = false;
            corruptPenguin = false;
            toyRobot = false;
            miniVlevi = false;
            corroder = false;
            crystalEntity = false;
            putridRipper = false;
            globule = false;

            #endregion minions

            #region pets

            lilOrange = false;
            voidCrawler = false;
            woke = false;
            royalEye = false;
            possessedHand = false;
            babyShadeWyrm = false;
            turboDoge = false;
            wyvernPet = false;
            chamchamRat = false;
            stellate = false;

            #endregion pets

            #region debuffs

            iceBound = false;
            endlessTears = false;
            extinctionCurse = false;
            handsOfDespair = false;
            dragonfire = false;
            discordDebuff = false;
            brokenWings = false;
            chaosBurn = false;
            acidBurn = false;
            superSlow = false;
            starstruck = false;
            incineration = false;
            choking = false;
            acidWebbed = false;

            #endregion debuffs


            criticalHeat = false;
            glassHeart = false;

            inMech = false;

            cantSeeHoverText = false;

            flyingBoots = false;
            wingTimeMult = 1f;
            wingSpdXMult = 1f;
            wingAccXMult = 1f;
            wingSpdYMult = 1f;
            wingAccYMult = 1f;

            dashCooldown = false;
            medicineCooldown = false;
            frozenGauntlet = false;
            ancientDecayWeapon = false;
            lightningCloud = false;
            lightningCloudHidden = false;

            extinctionCurseImbue = false;
            starstruckImbue = false;
            discordantPotion = false;
            vilePower = false;
            superSpeed = false;
            hellFury = false;

            cantFly = false;
            cantROD = false;
            cantMagicMirror = false;
            cantGrapple = false;
            puffFall = false;
            replenishRing = false;
            neovirtuoBonus = false;
            immortalResolve = false;
            chaosRing = false;
            voidLantern = false;
            noRespawnTime = false;
            flare = false;
            scourgeDrive = false;
            spikeBoots = false;
            templeSpikeBoots = false;
            sonicArm = false;
            nyanBoots = false;
            voidBoots = false;
            skylineFlying = false;
            vleviAegis = false;
            theAntidote = false;
            cosmicGlass = false;
            sufferWithMe = false;
            strangeUkulele = false;
            crystallineLocket = false;
            eaMagmaStone = false;
            meteoricPendant = false;
            prismPolish = false;
            fireAcc = false;
            boostDrive = 0;
            wispForm = false;
            honeyCocoon = false;

            gasMask = false;
            ironBoots = 0;
            drakoniteGoggles = 0;

            oceanicArmor = false;
            voidArmor = false;
            dragonmailGreathelm = false;
            dragonmailHood = false;
            dragonmailMask = false;
            dragonmailVisage = false;
            elementalArmor = false;
            elementalArmorCooldown = false;
            superbaseballDemon = false;
            gelticConqueror = false;
            crowsArmor = false;
            toyArmor = false;
            forgedArmor = false;
            voidWalkerArmor = 0;
            voidWalkerChest = false;
            energyWeaverArmor = false;
            cosmicalusArmor = false;
            mechArmor = false;
            arid = false;
            putridArmour = false;

            oiniteStatue = false;

            alchemistTimer = false;
            dryadsRadar = false;
            rainMeter = false;
            nearbyEvil = "No evil";

            damageTaken = 1f;
            if (!calamityEnabled)
            {
                player.statLifeMax2 += (voidHeartsUsed * 10) + (chaosHeartsUsed * 10);
                player.statManaMax2 += lunarStarsUsed * 100;

                if (Main.expertMode)
                {
                    if (player.extraAccessory)
                    {
                        player.extraAccessorySlots = 1;
                        if (extraAccSlot)
                        {
                            player.extraAccessorySlots = 2;
                        }
                    }
                }
            }
            shieldHearts = shieldLife / 5;
            player.statLifeMax2 += shieldLife;

            buffDPS = buffDPSCount;
            buffDPSCount = 0;

            eaDash = 0;

            toySlimeClaw = false;
            slimeBooster = false;
            greatLens = false;
            bleedingHeart = false;
            crystalNectar = false;
            fadedCloth = false;
            hellHeart = false;
            icyHeart = false;
            abyssalMatter = false;
            radiantCrown = false;
        }
        // for the bonus life
        public override void clientClone(ModPlayer clientClone)
        {
            MyPlayer clone = clientClone as MyPlayer;
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = mod.GetPacket();
            packet.Write((byte)ElementsAwokenMessageType.StarHeartSync);
            packet.Write((byte)player.whoAmI);
            packet.Write(voidHeartsUsed);
            packet.Write(chaosHeartsUsed);
            packet.Write(lunarStarsUsed);
            packet.Send(toWho, fromWho);
        }

        public override TagCompound Save()
        {
            TagCompound tag = new TagCompound {
            {"voidHeartsUsed", voidHeartsUsed},
            {"chaosHeartsUsed", chaosHeartsUsed},
            {"lunarStarsUsed", lunarStarsUsed},
            {"voidCompressor", voidCompressor},
            {"extraAccSlot", extraAccSlot},
            {"voidBlood", voidBlood},
            {"canSandstormA", canSandstormA},
            {"canTimeA", canTimeA},
            {"canRainA", canRainA},
            {"canWispA", canWispA},
            {"attachedWings", ItemIO.Save(UI.BootWingsUI.itemSlot.Item)}
            };
            var list = new List<bool>(mysteriousPotionsDrank);
            tag.Add("mysteriousPotionsDrankList", list); // lists are easier to save and load than arrays
            return tag;
        }

        public override void Load(TagCompound tag)
        {
            voidHeartsUsed = tag.GetInt("voidHeartsUsed");
            chaosHeartsUsed = tag.GetInt("chaosHeartsUsed");
            lunarStarsUsed = tag.GetInt("lunarStarsUsed");
            voidCompressor = tag.GetBool("voidCompressor");
            extraAccSlot = tag.GetBool("extraAccSlot");
            voidBlood = tag.GetBool("voidBlood");
            canSandstormA = tag.GetBool("canSandstormA");
            canTimeA = tag.GetBool("canTimeA");
            canRainA = tag.GetBool("canRainA");
            canWispA = tag.GetBool("canWispA");
            canWispA = tag.GetBool("canWispA");
            attachedWings = ItemIO.Load(tag.GetCompound("attachedWings"));
            if (tag.ContainsKey("mysteriousPotionsDrankList"))
            {
                var list = tag.GetList<bool>("mysteriousPotionsDrankList");
                mysteriousPotionsDrank = new List<bool>(list).ToArray();
            }
        }

        public void TryGettingDevArmor()
        {
            player.TryGettingDevArmor();
            if (Main.rand.NextBool(20))
            {
                if (Main.rand.NextBool(20)) player.QuickSpawnItem(mod.ItemType("KawaiiOrangesMask"));
                else player.QuickSpawnItem(mod.ItemType("OrangesMask"));
                player.QuickSpawnItem(mod.ItemType("OrangesBreastplate"));
                player.QuickSpawnItem(mod.ItemType("OrangesLeggings"));
            }
        }
        public override void PostUpdateMiscEffects()
        {
            bool debugMode = GetInstance<Config>().debugMode;
            if (debugMode)
            {
                if (player.wet) Main.musicFade[Main.curMusic] = 0.05f; // makes the music quieter underwater

                //Tile mouseTile = Framing.GetTileSafely((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16);
                //Main.NewText(Main.tile[(int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16].frameX);
                //Main.NewText(Main.tileSolid[mouseTile.type]);
                //    Main.NewText(player.mount._type);
                //Main.NewText(ElementsAwoken.glowMap[(int)((Main.MouseWorld.X / 16) % 100), (int)((Main.MouseWorld.Y / 16) % 100)]);
                //Main.NewText(Main.tile[(int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16)].active() && Main.tileSolid[Main.tile[(int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16)].type]);
                //Main.NewText(Lighting.GetColor((int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16)));
                // for growing plateau herbs
                if (player.controlUseItem && player.releaseUseItem)
                {
                    Tile t = Main.tile[(int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16];
                    if (t.type == TileType<Tiles.VolcanicPlateau.Flora.AshwillowTile>() ||
                     t.type == TileType<Tiles.VolcanicPlateau.Flora.CinderlilyTile>() ||
                     t.type == TileType<Tiles.VolcanicPlateau.Flora.ToxithornTile>() ||
                     t.type == TileType<Tiles.VolcanicPlateau.Flora.VoidBulbTile>())
                    {
                        if (t.frameX == 0 || t.frameX == 18) t.frameX += 18;
                    }
                }
            }

            UI.BootWingsUI.Visible = (flyingBoots || UI.BootWingsUI.itemSlot.Item.type != 0) && Main.playerInventory;
            if (attachedWings != null)
            {
                UI.BootWingsUI.itemSlot.Item = attachedWings;
                attachedWings = null;
            }
            if (UI.AcidTapUI.Visible)
            {
                if (player.controlInv) UI.AcidTapUI.Visible = false;
            }
            //Main.NewText(Framing.GetTileSafely((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16).frameX);
            //Main.NewText(player.Center.Y / 16);

            //Tile orbTile = Framing.GetTileSafely((int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16));
            //if (orbTile.wall != 0) Main.NewText("f");
            EAWallSlide();
            // timers
            generalTimer++;
            skylineFrameTimer++;
            cosmicGlassCD--;
            toySlimeClawCD--;
            hellsReflectionTimer--;
            hellsReflectionCD--;
            voidPortalCooldown--;
            toyArmorCooldown--;
            crowsArmorCooldown--;
            crystallineLocketCrit--;
            voidArmorHealCD--;
            abyssalRage--;
            immortalResolveCooldown--;
            toySlimeChanceTimer--;
            observerChanceTimer--;
            greatLensTimer--;
            icyHeartTimer++;
            aeroflakTimer--;
            mechArmorCD--;
            toySlimed--;
            honeyCocooned--;
            ironBootsSoundDelay--;
            npcHeldThowCD--;
            platformDropTimer--;
            if (player.controlDown) platformDropTimer = 5;
            if (npcHeld != 0)
            {
                NPC holding = Main.npc[npcHeld];
                holding.Bottom = player.Top + player.velocity;
            }

            if (player.HasBuff(BuffID.ObsidianSkin))
            {
                SetFireResistance(0.2f);
            }
            if (flareberryDrugged > 0)
            {
                if (!player.HasBuff(BuffType<Drugged>())) flareberryDrugged = 0;
                flareberryDrugged--;
                // distortion
                float fade = 300;
                float amount = 1f - MathHelper.Clamp((float)(flareberryDrugged - (flareberryDruggedDuration - fade)) / fade, 0, 1);
                float wobble = (float)Math.Sin((float)flareberryDrugged / 180f);
                wobble *= 2f;
                if (!Filters.Scene["ElementsAwoken:DistortScreen"].IsActive()) Filters.Scene.Activate("ElementsAwoken:DistortScreen").GetShader().UseProgress(wobble).UseIntensity(amount);
                Filters.Scene["ElementsAwoken:DistortScreen"].GetShader().UseProgress(wobble).UseIntensity(amount);
                // hueshift
                float hue = (float)Math.Sin((float)flareberryDrugged / 30f);
                if (!Filters.Scene["ElementsAwoken:HueShiftScreen"].IsActive()) Filters.Scene.Activate("ElementsAwoken:HueShiftScreen").GetShader().UseProgress(hue).UseIntensity(amount);
                Filters.Scene["ElementsAwoken:HueShiftScreen"].GetShader().UseProgress(hue).UseIntensity(amount);
            }
            else
            {
                if (Filters.Scene["ElementsAwoken:DistortScreen"].IsActive()) Filters.Scene["ElementsAwoken:DistortScreen"].Deactivate();
                if (Filters.Scene["ElementsAwoken:HueShiftScreen"].IsActive()) Filters.Scene["ElementsAwoken:HueShiftScreen"].Deactivate();
            }
            if (flare) flareShieldCD--; // so the cooldonw doesnt decrease when the player isnt using it
            if (abilityTimer < 0) abilityTimer++;
            if (honeyCocooned <= 0)
            {
                honeyCocoonDamage = 0;
            }
            else if (honeyCocooned > 0)
            {
                player.velocity.X = 0;
                CantMove();
            }
            if (glassHeart)
            {
                for (int l = 0; l < Player.MaxBuffs; l++)
                {
                    if (player.buffType[l] == BuffID.ShadowDodge)
                    {
                        player.DelBuff(l);
                        break;
                    }
                }
            }
            if (!MyWorld.downedErius)
            {
                if ((NPC.AnyNPCs(NPCType<NPCs.VolcanicPlateau.Sulfur.Erius>()) && zoneErius))
                {
                    player.noBuilding = true;
                    player.AddBuff(BuffID.NoBuilding, 3, true);
                }
            }
            if (!MyWorld.downedMineBoss)
            {
                if ((NPC.AnyNPCs(NPCType<NPCs.VolcanicPlateau.Bosses.MineBoss>()) && zoneMineBoss))
                {
                    player.noBuilding = true;
                    player.AddBuff(BuffID.NoBuilding, 3, true);
                }
            }
            Rectangle obsidiousArena = new Rectangle(EAWorldGen.obsidiousTempleLoc.X + 33, EAWorldGen.obsidiousTempleLoc.Y + 2, 83, 65); // not fixed for flipping
            if (obsidiousArena.Contains(player.Center.ToTileCoordinates()))
            {
                player.noBuilding = true;
                player.AddBuff(BuffID.NoBuilding, 3, true);
            }
            if (acidWebbedID != -1)
            {
                player.Center = Main.projectile[acidWebbedID].Center - new Vector2(0, 30);
                cantUseItems = true;
                cantGrapple = true;
                CantMove();
                player.velocity = Vector2.Zero;
                int damage = Main.expertMode ? MyWorld.awakenedMode ? 75 : 60 : 30;
                if (generalTimer % 60 == 0) player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " couldn't handle the sticky situation."), damage, 0);
            }
            if (!acidWebbed) acidWebbedID = -1;
            bool inAcidWeb = false;
            inQuicksand = false;
            bool quicksandSulfuric = false;
            bool inQuicksandHead = false;
            int stickyX = -1;
            int stickyY = -1;
            Point playerTile = player.position.ToTileCoordinates();
            // checking for quicksand or acid web
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Tile t = Framing.GetTileSafely(playerTile.X + i, playerTile.Y + j);
                    if (t.type == TileType<Tiles.VolcanicPlateau.AcidWeb>() && t.active())
                    {
                        inAcidWeb = true;
                        stickyX = playerTile.X + i;
                        stickyY = playerTile.Y + j;
                        break;
                    }
                    if (Tiles.GlobalTiles.quicksands.Contains(t.type) && t.active())
                    {
                        if (j == 0) inQuicksandHead = true;
                        inQuicksand = true;
                        if (t.type == TileType<Tiles.VolcanicPlateau.SulfuricQuicksand>()) quicksandSulfuric = true;
                        break;
                    }

                }
            }
            // checking for quicksand below to pull player into it
            /*if (player.velocity.Y == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Tile t = Framing.GetTileSafely(playerTile.X + i, playerTile.Y + 3);
                    if (Tiles.GlobalTiles.quicksands.Contains(t.type) && t.active())
                    {
                        player.position.Y += 1;
                    }
                }
            }*/
            if (inAcidWeb)
            {
                player.AddBuff(BuffType<AcidBurn>(), 60);
                bool flag = (player.mount.Type == 6 || player.mount.Type == 13 || player.mount.Type == 11) && Math.Abs(player.velocity.X) > 5f;

                player.velocity *= 0.1f;
                stickyBreak++;
                if (stickyBreak > Main.rand.Next(20, 100) | flag)
                {
                    stickyBreak = 0;
                    WorldGen.KillTile(stickyX, stickyY, false, false, false);
                    if (Main.netMode == 1 && !Main.tile[stickyX, stickyY].active() && Main.netMode == 1)
                    {
                        NetMessage.SendData(17, -1, -1, null, 0, (float)stickyX, (float)stickyY, 0f, 0, 0, 0);
                    }
                }
            }
            else stickyBreak = 0;
            if (inQuicksand)
            {
                if (quicksandSulfuric) player.AddBuff(BuffType<AcidBurn>(), 60);

                player.velocity.X *= 0.1f;
                player.maxFallSpeed = 0.1f;
                if (player.velocity.Y < 0) player.velocity.Y *= 0.1f;
                player.pickSpeed *= 3f;
                if (inQuicksandHead)
                {
                    player.AddBuff(68, 1, true);
                }
            }
            /*
            if (generalTimer % 60 == 0)
            {
                for (int k = 0; k < Main.maxNPCs; k++)
                {
                    NPC other = Main.npc[k];
                    if (other.CanBeChasedBy(this) && Vector2.Distance(other.Center, player.Center) < 500)
                    {
                        float time = 60;
                        float grav = 0.16f;
                        float MyWorld.generalTimer = (other.Center.X - player.Center.X) / time;
                        float ySPD = (other.Center.Y - player.Center.Y - 0.5f * grav * time * time) / time;
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, MyWorld.generalTimer, ySPD, ProjectileType<MortarTest>(), 200, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }*/
            player.wingTimeMax = (int)(player.wingTimeMax * wingTimeMult);
            if (player.FindBuffIndex(BuffType<LavaFishingBuff>()) == -1)
            {
                if (tempCanFishInLava != null) ItemID.Sets.CanFishInLava = tempCanFishInLava;
                tempCanFishInLava = null;
            }
            else
            {
                if (tempCanFishInLava == null) tempCanFishInLava = ItemID.Sets.CanFishInLava;
                if (player.HeldItem.fishingPole > 0) ItemID.Sets.CanFishInLava[player.HeldItem.type] = true;
            }
            if (MyWorld.plateauWeather == 1 && MyWorld.plateauWeatherTime > 0 && zonePlateau && !gasMask) player.AddBuff(BuffType<Choking>(), 2);
            if (MyWorld.plateauWeather == 2 && MyWorld.plateauWeatherTime > 0 && zonePlateau) player.AddBuff(BuffType<CriticalHeat>(), 2);
            if (criticalHeat)
            {
                criticalHeatCD--;
                if (criticalHeatCD <= 0 && criticalHeatTimer < criticalHeatMax)
                {
                    criticalHeatTimer++;
                    criticalHeatCD = (int)MathHelper.Lerp(0, 10, fireResistance);
                }
            }
            else
            {
                if (criticalHeatTimer > 0) criticalHeatTimer -= 2;
                else criticalHeatTimer = 0;
            }
            if (choking)
            {
                int maxTime = (int)(1.5f * 60 * 60);
                int num = maxTime / (int)sulphurBreathMax;
                if (generalTimer % num == 0 && sulphurBreath < sulphurBreathMax) sulphurBreath++;
            }
            else
            {
                if (sulphurBreath > 0 && generalTimer % 5 == 0) sulphurBreath--;
            }
            if (MyWorld.plateauWeather == 2 && zonePlateau)
            {
                screenshakeAmount = 2;
                if (ironBoots == 0 && !player.mount.CanHover)
                {
                    float amount = 0.39f;
                    if (player.velocity.Y < 0) amount *= 0.85f;
                    if (player.wet) amount *= 0.7f;
                    if (player.lavaWet) amount *= 0.7f;
                    if (player.honey) amount *= 0.4f;

                    if (player.velocity.Y != 0) player.velocity.Y -= amount;
                }
                if (Main.rand.Next(3) == 0 && !GetInstance<Config>().lowDust)
                {
                    Vector2 speed = new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-13, -10));
                    Dust ash = Main.dust[Dust.NewDust(new Vector2(player.Center.X - Main.screenWidth / 2, player.Center.Y - Main.screenHeight / 2), Main.screenWidth, Main.screenHeight, 76)];
                    ash.scale = 1.3f;
                    ash.velocity.X = speed.X;
                    ash.velocity.Y = speed.Y;
                    ash.fadeIn = 2f;
                    //ash.noGravity = true;
                    ash.color = new Color(200, 200, 200);
                }
            }
            if (ironBoots > 0)
            {
                if (ironBoots == 1)
                {
                    player.accRunSpeed *= 0.75f;
                    player.moveSpeed *= 0.75f;

                    if (player.velocity.X != 0 && player.velocity.Y == 0)
                    {
                        if ((player.legFrame.Y == player.legFrame.Height * 10 || player.legFrame.Y == player.legFrame.Height * 17) && ironBootsSoundDelay <= 0)
                        {
                            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 37, 1, -0.4f);
                            ironBootsSoundDelay = 5;
                        }
                    }
                    if (player.velocity.Y == 0 && player.oldVelocity.Y != 0) Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 37, 1, -0.4f);
                    player.oldVelocity.Y = player.velocity.Y;
                }
                if (!zonePlateau || MyWorld.plateauWeather != 2)
                {
                    player.jumpSpeedBoost -= 1f;
                }
            }
            if (zonePlateau)
            {
                if (!GetInstance<Config>().lowDust)
                {
                    int rand = MyWorld.awakenedPlateau ? 3 : zoneSulphur ? 15 : 5;
                    if (Main.rand.NextBool(rand))
                    {
                        int type = 0;
                        switch (Main.rand.Next(2))
                        {
                            case 0:
                                type = DustType<Dusts.PlateauDust>();
                                break;

                            case 1:
                                type = DustType<Dusts.PlateauDustTileCollide>();
                                break;
                        }
                        Dust ash = Main.dust[Dust.NewDust(new Vector2(player.Center.X - Main.screenWidth / 2, player.Center.Y - Main.screenHeight / 2), Main.screenWidth, Main.screenHeight, type, 0, 0, 0, default(Color), 1f)];
                        ash.scale = 0.02f;
                    }
                }
            }

            if (wispForm)
            {
                int width = 18;
                float maxDist = width / 2;
                int amount = 3;
                for (int i = 0; i < amount; i++)
                {
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                    Dust dust = Main.dust[Dust.NewDust(player.Center + offset - new Vector2(4, 4), 0, 0, wispDust, 0, 0, 100)];
                    dust.noGravity = true;
                    dust.velocity *= 0.5f;
                    dust.velocity += player.velocity;
                }
                if (Main.rand.NextBool(5))
                {
                    Dust dust2 = Main.dust[Dust.NewDust(player.position, player.width, player.height, wispDust)];
                    dust2.noGravity = true;
                    dust2.scale *= 1.6f;
                    dust2.velocity.Y = -Main.rand.NextFloat(5, 10);
                }

                player.noKnockback = false;
                spiritTimer++;
                if (spirit < 0) spirit = 0;
                if (spirit < spiritMax)
                {
                    if (spiritTimer > 180)
                    {
                        spiritRegenCD--;
                        if (spiritRegenCD <= 0)
                        {
                            spirit++;
                            spiritRegenCD = (int)MathHelper.Lerp(3, 10, (float)spiritRegenCD / 600);
                        }
                    }
                }
                else spirit = spiritMax;
                wispAttackCD--;
            }
            if (arid)
            {
                if (player.controlJump && aridTimer < 600)
                {
                    aridTimer++;
                    if (player.velocity.Y > -4)
                    {
                        player.velocity.Y -= 0.6f;
                        Dust dust = Main.dust[Dust.NewDust(player.BottomLeft - new Vector2(0, 6), player.width, 6, 32, 0f, 0f)];
                        dust.velocity = new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(3, 5));
                    }
                    if (player.velocity.Y < 4)
                    {
                        player.fallStart = (int)player.position.Y / 16;
                    }
                }
                if (player.velocity.Y == 0)
                {
                    aridTimer = 0;
                }
            }
            if (boostDriveTimer > 0 && boostDrive > 0)
            {
                boostDriveTimer--;
                if (boostDrive == 1)
                {
                    player.moveSpeed *= 1.5f;
                    player.accRunSpeed *= 1.5f;
                    player.runAcceleration *= 2f;
                    Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, 226, 0f, 0f, 100, default(Color), 1.4f)];
                    dust.noGravity = true;
                }
                else if (boostDrive == 2)
                {
                    player.moveSpeed *= 2.5f;
                    player.accRunSpeed *= 2.5f;
                    player.runAcceleration *= 3f;
                    Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, 205, 0f, 0f, 100, default(Color), 1.4f)];
                    dust.noGravity = true;
                }
            }

            if (putridArmour)
            {
                int num = player.velocity.Y == 0 ? 30 : 60;
                if (generalTimer % num == 0)
                {
                    Vector2 startVec = player.Center + Main.rand.NextVector2Square(-60, 60);
                    for (int i = 0; i < 6; i++)
                    {
                        float distance = i < 3 ? 30 : 15;
                        Projectile proj = Main.projectile[Projectile.NewProjectile(startVec + Main.rand.NextVector2Square(-distance, distance), Vector2.Zero, ProjectileType<PutridCloud>(), 0, 0, player.whoAmI, i < 3 ? 0 : 1)];
                        proj.localAI[0] = Main.rand.NextBool() ? -1 : 1;
                        proj.rotation = Main.rand.NextFloat((float)Math.PI * 2);
                    }
                }
            }
            if (toySlimedID != -1)
            {
                CantMove();
                if (Main.npc[toySlimedID].active)
                {
                    player.Center = Main.npc[toySlimedID].Center;
                    player.immune = true;
                    if (toySlimed <= 0)
                    {
                        toySlimedID = -1;
                        player.velocity.X = Main.rand.NextFloat(-8, 8);
                        player.velocity.Y = Main.rand.NextFloat(-12, -20);
                        Main.PlaySound(SoundID.Item95, player.Center);
                    }
                    player.AddBuff(BuffID.Slimed, 20);
                }
                else toySlimedID = -1;
            }
            if (aeroflakTimer <= 0) aeroflakHits = 0;
            if (bleedingHeart)
            {
                if ((player.dashDelay != 0 || eaDashDelay != 0) && player.velocity != Vector2.Zero)
                {
                    if (Main.rand.NextBool(8))
                    {
                        Projectile.NewProjectile(player.Center, Main.rand.NextVector2Square(-6, 6), ProjectileType<BloodbathDashP>(), 30, 6f, player.whoAmI);
                    }
                }
            }

            if (icyHeart)
            {
                icyHeartDR = MathHelper.Lerp(1f, 0f, MathHelper.Clamp(icyHeartTimer, 0f, 1200f) / 1200f);
                int numDust = (int)MathHelper.Lerp(1, 20, MathHelper.Clamp(icyHeartTimer, 0f, 1200f) / 1200f);
                int width = (int)(player.width * 1.2f);
                int height = (int)(player.height * 1.1f);
                for (int i = 0; i < numDust; i++)
                {
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    Vector2 offset = new Vector2((float)Math.Sin(angle) * width, (float)Math.Cos(angle) * height);
                    Dust dust = Main.dust[Dust.NewDust(player.Center + offset - Vector2.One * 4, 0, 0, 135, 0, 0, 100)];
                    dust.noGravity = true;
                    dust.velocity = player.velocity;
                }
            }
            if (abyssalMatter)
            {
                if (Main.rand.NextBool(60))
                {
                    Projectile.NewProjectile(player.Center + Main.rand.NextVector2Square(-300, 300), Main.rand.NextVector2Square(-1, 1), ProjectileType<AbyssalPortal>(), 300, 6f, player.whoAmI);
                }
                if (abyssalRage > 0)
                {
                    if (player.dead || !player.active) abyssalRage = 0;
                    player.allDamage *= 1.3f;
                    player.moveSpeed *= 1.5f;
                    player.accRunSpeed *= 1.5f;

                    int num = GetInstance<Config>().lowDust ? 1 : 3;
                    for (int l = 0; l < num; l++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, DustID.PinkFlame, 0f, 0f, 100, default(Color), 1.4f)];
                        dust.noGravity = true;
                    }

                    if (abyssalRage % 60 == 0)
                    {
                        Vector2 speed = Main.rand.NextVector2Square(-2.5f, 2.5f);
                        float randAi0 = Main.rand.Next(10, 80) * 0.001f;
                        if (Main.rand.Next(2) == 0) randAi0 *= -1f;
                        float randAi1 = Main.rand.Next(10, 80) * 0.001f;
                        if (Main.rand.Next(2) == 0) randAi1 *= -1f;
                        Projectile.NewProjectile(player.Center, speed, ProjectileType<AbyssalTentacle>(), 300, 6f, player.whoAmI, randAi0, randAi1);
                    }
                }
            }
            if (!starstruck) starstruckCounter = 0;

            if (voidBlood && player.statLife < player.statLifeMax2 * 0.3f && generalTimer % 60 == 0)
            {
                int damage = 10;
                if (NPC.downedBoss1) damage = 15;
                if (NPC.downedBoss3) damage = 20;
                if (Main.hardMode) damage = 25;
                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3) damage = 30;
                if (NPC.downedPlantBoss) damage = 35;
                if (NPC.downedAncientCultist) damage = 40;
                if (NPC.downedMoonlord) damage = 60;
                Projectile proj = Main.projectile[Projectile.NewProjectile(player.Center.X, player.Center.Y, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), mod.ProjectileType("VoidBlood"), damage, 0f, Main.myPlayer, 0f, 0f)];
            }
            if (!NPC.AnyNPCs(NPCType<VoidLeviathanHead>())) behemothGazeTimer = 0;
            if (behemothGazeTimer > 600)
            {
                float amount = MathHelper.Clamp((float)(leviathanDist - 3000) / 9000f, 0, 1);
                player.accRunSpeed *= MathHelper.Lerp(0, 1.5f, amount);
                player.moveSpeed *= MathHelper.Lerp(0, 2, amount);
                player.statDefense = (int)(player.statDefense * MathHelper.Lerp(1, 0.2f, amount));
                player.allDamage *= MathHelper.Lerp(1, 0.2f, amount);
            }
            else if (behemothGazeTimer > 0)
            {
                int num = (int)MathHelper.Lerp(1, 8, (float)(leviathanDist - 3000) / 9000f);
                if (GetInstance<Config>().lowDust) num = 1;
                for (int l = 0; l < num; l++)
                {
                    Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, DustID.PinkFlame, 0f, 0f, 100, default(Color), 1.4f)];
                    dust.noGravity = true;
                }
            }

            if (superSpeed)
            {
                player.moveSpeed *= 3;
                player.runAcceleration *= 10;
            }
            if (superSlow)
            {
                player.moveSpeed *= 0.1f;
            }
            // need to fix this up, its messy and makes no sense. modifed vanilla code

            #region dashes

            if (eaDashDelay > 0)
            {
                eaDashDelay--;
                //return;
            }
            if (eaDashDelay > 0)
            {
                float maxDashSpeed = 15f;
                float maxSpeed = Math.Max(player.accRunSpeed, player.maxRunSpeed);
                float slowdown1 = 0.985f;
                float slowdown2 = 0.94f;
                if (eaDash == 1 || eaDash == 2)
                {
                    int dustID = 127;
                    if (eaDash == 2)
                    {
                        dustID = 63;
                        maxDashSpeed = 18f;
                    }
                    for (int k = 0; k < 3; k++)
                    {
                        int num12;
                        if (player.velocity.Y == 0f)
                        {
                            num12 = Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float)player.height - 4f), player.width, 8, dustID, 0f, 0f, 100, default(Color), 2f);
                        }
                        else
                        {
                            num12 = Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float)(player.height / 2) - 8f), player.width, 16, dustID, 0f, 0f, 100, default(Color), 2f);
                        }
                        Dust dust = Main.dust[num12];
                        dust.velocity *= 0.1f;
                        dust.scale *= 1f + (float)Main.rand.NextFloat(-0.4f, 0.4f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
                        dust.noGravity = true;
                        if (eaDash == 2) dust.color = Main.DiscoColor;
                    }
                }
                if (eaDash > 0)
                {
                    player.vortexStealthActive = false;
                    if (player.velocity.X > maxDashSpeed || player.velocity.X < -maxDashSpeed)
                    {
                        player.velocity.X = player.velocity.X * slowdown1;
                        //return;
                    }
                    if (player.velocity.X > maxSpeed || player.velocity.X < -maxSpeed)
                    {
                        player.velocity.X = player.velocity.X * slowdown2;
                        //return;
                    }
                    if (player.velocity.X < 0f)
                    {
                        player.velocity.X = -maxDashSpeed;
                        //return;
                    }
                    if (player.velocity.X > 0f)
                    {
                        player.velocity.X = maxDashSpeed;
                        //return;
                    }
                }
            }
            else if (eaDash > 0 && !player.mount.Active)
            {
                int dashDir = 0;
                bool flag = false;
                if (eaDashTime > 0) eaDashTime--;
                if (eaDashTime < 0) eaDashTime++;
                if (eaDashDelay <= 0)
                {
                    if (player.controlRight && player.releaseRight)
                    {
                        if (eaDashTime > 0)
                        {
                            dashDir = 1;
                            flag = true;
                            eaDashTime = 0;
                        }
                        else eaDashTime = 15;
                    }
                    else if (player.controlLeft && player.releaseLeft)
                    {
                        if (eaDashTime < 0)
                        {
                            dashDir = -1;
                            flag = true;
                            eaDashTime = 0;
                        }
                        else eaDashTime = -15;
                    }
                }
                if (flag)
                {
                    int dashDelayAmount = 40;
                    if (eaDash == 1 || eaDash == 2)
                    {
                        int dustID = 127;
                        if (eaDash == 2)
                        {
                            dustID = 63;
                        }
                        player.velocity.X = 26f * (float)dashDir;
                        Point point = (player.Center + new Vector2((float)(dashDir * player.width / 2 + 2), player.gravDir * (float)(-(float)player.height) / 2f + player.gravDir * 2f)).ToTileCoordinates();
                        Point point2 = (player.Center + new Vector2((float)(dashDir * player.width / 2 + 2), 0f)).ToTileCoordinates();
                        if (WorldGen.SolidOrSlopedTile(point.X, point.Y) || WorldGen.SolidOrSlopedTile(point2.X, point2.Y))
                        {
                            player.velocity.X = player.velocity.X / 2f;
                        }
                        for (int i = 0; i < 20; i++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, dustID, 0f, 0f, 100, default(Color), 2f)];
                            dust.position.X = dust.position.X + (float)Main.rand.Next(-5, 6);
                            dust.position.Y = dust.position.Y + (float)Main.rand.Next(-5, 6);
                            dust.velocity *= 3f;
                            dust.scale *= 1f + (float)Main.rand.NextFloat(-0.4f, 0.4f);
                            dust.shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
                            dust.noGravity = true;
                            if (eaDash == 2) dust.color = Main.DiscoColor;
                        }
                        eaDashDelay = dashDelayAmount;
                    }
                }
            }

            #endregion dashes

            if (archaicProtectionTimer > 0)
            {
                archaicProtectionTimer--;
                player.immune = true;
                CantMove();
                player.velocity = Vector2.Zero;
                if (archaicProtectionPos == Vector2.Zero) archaicProtectionPos = player.Center;
                else player.Center = archaicProtectionPos;
                if (player.ownedProjectileCounts[mod.ProjectileType("ArchaicProtection")] == 0)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("ArchaicProtection"), 0, 0f, player.whoAmI);
                }
            }
            else archaicProtectionPos = Vector2.Zero;

            if (brokenWings) player.wingTimeMax = 1;

            if (dryadsRadar)
            {
                if (MyWorld.corruptionTiles > 0)
                {
                    nearbyEvil = "Corruption";
                }
                if (MyWorld.crimsonTiles > 0)
                {
                    if (MyWorld.corruptionTiles > 0 && MyWorld.hallowedTiles == 0)
                    {
                        nearbyEvil += " and Crimson";
                    }
                    else if (MyWorld.corruptionTiles > 0)
                    {
                        nearbyEvil += ", Crimson";
                    }
                    else
                    {
                        nearbyEvil = "Crimson";
                    }
                }
                if (MyWorld.hallowedTiles > 0)
                {
                    if (MyWorld.corruptionTiles > 0 && MyWorld.crimsonTiles > 0)
                    {
                        nearbyEvil = "All evils";
                    }
                    else if (MyWorld.corruptionTiles > 0 || MyWorld.crimsonTiles > 0)
                    {
                        nearbyEvil += " and Hallowed";
                    }
                    else
                    {
                        nearbyEvil = "Hallowed";
                    }
                }
            }

            if (forgedShackled > 0)
            {
                if (player.ownedProjectileCounts[mod.ProjectileType("ShackledBase")] == 0)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("ShackledBase"), 0, 0f, player.whoAmI, player.whoAmI, 0f);
                }
                player.moveSpeed *= 3;
                shackleFlingCooldown--;
                forgedShackled--;
            }
            if (flingToShackle)
            {
                if (forgedShackled <= 0)
                {
                    flingToShackle = false;
                }
                for (int l = 0; l < 5; l++)
                {
                    Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, 6, 0f, 0f, 100, default(Color), 1.4f)];
                    dust.noGravity = true;
                }
                CollideWithNPCs(player.getRect(), 40, 10f, 30, 6);
                shackleFlingCooldown = 60;
            }

            if (skylineAlpha > 0)
            {
                if (skylineFrameTimer % 6 == 0) skylineFrame++;
                if (skylineFrame > 3) skylineFrame = 0;
            }
            //Main.NewText("alpha " + skylineAlpha + " flying " + skylineFlying);
            if (shieldLife > 100) shieldLife = 100;
            if (shieldLife > 0 && Main.time % 60 == 0) shieldLife--;

            if (player.statLife <= player.statLifeMax2 - 5 && shieldHearts > 0) shieldHearts--;

            if (player.mount.Active && player.mount.Type == mod.MountType("ElementalDragonBunny") && Math.Abs(player.velocity.X) > player.mount.DashSpeed - player.mount.RunSpeed / 3f)
            {
                Rectangle rect = player.getRect();
                if (player.direction == 1)
                {
                    rect.Offset(player.width - 1, 0);
                }
                rect.Width = 2;
                rect.Inflate(6, 12);
                float damage = 60;
                float knockback = 10f;
                int nPCImmuneTime = 30;
                int playerImmuneTime = 6;
                CollideWithNPCs(rect, damage, knockback, nPCImmuneTime, playerImmuneTime);
            }

            if (crystallineLocketCrit > 0)
            {
                player.magicCrit = 100;
                player.meleeCrit = 100;
                player.rangedCrit = 100;
                player.thrownCrit = 100;
                int dustID = mod.DustType("AncientRed");
                switch (Main.rand.Next(4))
                {
                    case 0:
                        dustID = mod.DustType("AncientRed");
                        break;

                    case 1:
                        dustID = mod.DustType("AncientGreen");
                        break;

                    case 2:
                        dustID = mod.DustType("AncientBlue");
                        break;

                    case 3:
                        dustID = mod.DustType("AncientPink");
                        break;

                    default:
                        dustID = mod.DustType("AncientRed");
                        break;
                }
                for (int i = 0; i < 2; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, dustID, 0f, 0f, 100, default(Color), 1.5f)];
                    dust.noGravity = true;
                    dust.velocity *= 0.75f;
                    dust.fadeIn = 1.3f;
                }
            }

            if (lightningCloud)
            {
                if (!lightningCloudHidden)
                {
                    if (player.ownedProjectileCounts[mod.ProjectileType("LightningCloud")] == 0)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y - 50, 0f, 0f, mod.ProjectileType("LightningCloud"), 0, 0f, player.whoAmI, 0f, 0f);
                    }
                    if (player.ownedProjectileCounts[mod.ProjectileType("LightningCloudSwirl")] == 0 && lightningCloudCharge >= 300)
                    {
                        int orbitalCount = 3;
                        for (int l = 0; l < orbitalCount; l++)
                        {
                            int distance = 360 / orbitalCount;
                            int orbital = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("LightningCloudSwirl"), 0, 0f, player.whoAmI, l * distance, player.whoAmI);
                        }
                    }
                }
                if (lightningCloudCharge < 300)
                {
                    lightningCloudCharge++;
                    int rand = 20;
                    if (lightningCloudCharge >= 60)
                    {
                        rand = 15;
                    }
                    if (lightningCloudCharge >= 120)
                    {
                        rand = 12;
                    }
                    if (lightningCloudCharge >= 180)
                    {
                        rand = 8;
                    }
                    if (lightningCloudCharge >= 240)
                    {
                        rand = 5;
                    }
                    if (Main.rand.Next(rand) == 0)
                    {
                        int num5 = Dust.NewDust(player.position, player.width, player.height, 15, 0f, 0f, 200, default(Color), 0.5f);
                        Main.dust[num5].noGravity = true;
                        Main.dust[num5].velocity *= 0.75f;
                        Main.dust[num5].fadeIn = 1.3f;
                        Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                        vector.Normalize();
                        vector *= (float)Main.rand.Next(50, 100) * 0.04f;
                        Main.dust[num5].velocity = vector;
                        vector.Normalize();
                        vector *= 34f;
                        Main.dust[num5].position = player.Center - vector;
                    }
                }
                if (lightningCloudCharge > 300)
                {
                    lightningCloudCharge = 300;
                }
            }

            if (scourgeDrive)
            {
                float pVelX = player.velocity.X;

                if (pVelX < 0)
                {
                    pVelX *= -1;
                }
                scourgeSpeed = pVelX >= 12;
                if (scourgeSpeed)
                {
                    player.magicDamage *= 1.25f;
                    player.rangedDamage *= 1.25f;
                    player.meleeDamage *= 1.25f;
                    player.minionDamage *= 1.25f;
                    player.thrownDamage *= 1.25f;
                }
            }

            if (MyWorld.aggressiveEnemies)
            {
                player.aggro += 500;
            }
            if (player.FindBuffIndex(mod.BuffType("StatueBuffGenihWat")) == -1)
            {
                MyWorld.aggressiveEnemies = false; // resets so enemies stop being affected
                MyWorld.swearingEnemies = false;
            }

            if (superbaseballDemon)
            {
                if (player.ownedProjectileCounts[mod.ProjectileType("Demon")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("Demon"), 30, 1.25f, Main.myPlayer, 0f, 0f);
                }
            }

            if (MyWorld.voidInvasionUp)
            {
                if (!Main.dayTime && Main.time > 16220)
                {
                    if (voidLantern)
                    {
                        Lighting.AddLight(player.Center, 60f, 12f, 0.0f);
                    }
                    else
                    {
                        Lighting.AddLight(player.Center, 20f, 4f, 0.0f);
                    }
                }
            }

            if (masterSwordCharge > 0)
            {
                if (masterSwordCharge > 50)
                {
                    masterSwordCharge = 50;
                }
                masterSwordCountdown--;
                if (masterSwordCountdown <= 0)
                {
                    masterSwordCharge = 0;
                    masterSwordCountdown = 0;
                }
                if (masterSwordCountdown == 1)
                {
                    CombatText.NewText(player.getRect(), Color.Red, "Discharged", true, false);
                }
            }

            if (empyreanCloudCD < 0)
            {
                Projectile.NewProjectile(player.Center.X + Main.rand.Next(-120, 120), player.Center.Y - Main.rand.Next(15, 50), 0f, 0f, mod.ProjectileType("AeroStorm"), 30, 1.25f, Main.myPlayer, 0f, 0f);
                empyreanCloudCD = Main.rand.Next(60, 300);
            }

            if (ninjaDash)
            {
                for (int l = 0; l < 1; l++)
                {
                    int dust;
                    if (player.velocity.Y == 0f)
                    {
                        dust = Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float)player.height - 4f), player.width, 8, 31, 0f, 0f, 100, default(Color), 1.4f);
                    }
                    else
                    {
                        dust = Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float)(player.height / 2) - 8f), player.width, 16, 31, 0f, 0f, 100, default(Color), 1.4f);
                    }
                    Main.dust[dust].velocity *= 0.1f;
                    Main.dust[dust].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
                    Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
                }
                dashDustTimer--;
                if (dashDustTimer <= 0)
                {
                    ninjaDash = false;
                }
            }

            if (viridiumDash)
            {
                for (int l = 0; l < 1; l++)
                {
                    int dust = Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float)player.height - 4f), player.width, 8, 222, 0f, 0f, 100, default(Color), 1.4f);
                    Main.dust[dust].velocity *= 0.1f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
                    Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
                }
                if (Main.rand.Next(5) == 0)
                {
                    Projectile.NewProjectile(player.Center.X, player.Bottom.Y, 0f, 0f, mod.ProjectileType("ViridiumBomb"), 0, 0f, player.whoAmI, 0.0f, 0.0f);
                }
                dashDustTimer--;
                if (dashDustTimer <= 0)
                {
                    viridiumDash = false;
                }
            }

            // aegis
            if (vleviAegis)
            {
                aegisDashCooldown--;
                if (vleviAegisDamage >= 500)
                {
                    vleviAegisBoost = 900;
                    vleviAegisDamage = 0;
                }
            }
            if (vleviAegisBoost > 0)
            {
                vleviAegisBoost--;

                player.moveSpeed *= 1.4f;

                player.magicDamage *= 1.2f;
                player.meleeDamage *= 1.2f;
                player.minionDamage *= 1.2f;
                player.rangedDamage *= 1.2f;
                player.thrownDamage *= 1.2f;

                if (player.ownedProjectileCounts[mod.ProjectileType("VleviAegisSwirl")] == 0)
                {
                    int orbitalCount = 3;
                    for (int l = 0; l < orbitalCount; l++)
                    {
                        int distance = 360 / orbitalCount;
                        int orbital = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("VleviAegisSwirl"), 0, 0f, Main.myPlayer, l * distance, player.whoAmI);
                    }
                    for (int l = 0; l < orbitalCount; l++)
                    {
                        int distance = 360 / orbitalCount;
                        int orbital = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("VleviAegisSwirl"), 0, 0f, Main.myPlayer, l * distance, player.whoAmI);
                        Main.projectile[orbital].localAI[0] = 1;
                    }
                    Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 113);
                }
            }
            if (aegisDashTimer > 0)
            {
                aegisDashTimer--;

                player.velocity.X = 25 * aegisDashDir;
                player.velocity.Y = 0;

                player.direction = aegisDashDir;

                player.controlJump = false; // to prevent jumping
                for (int l = 0; l < 2; l++)
                {
                    Vector2 position = player.Center + Vector2.Normalize(player.velocity) * 10f;
                    Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, DustID.PinkFlame, 0f, 0f, 0, default(Color), 1.5f)];
                    dust.position = position;
                    dust.velocity = player.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * 0.33f + player.velocity / 4f;
                    dust.velocity.X -= player.velocity.X / 10f * l;
                    dust.position += player.velocity.RotatedBy(1.5707963705062866, default(Vector2));
                    dust.fadeIn = 0.5f;
                    dust.noGravity = true;
                    Dust dust1 = Main.dust[Dust.NewDust(player.position, player.width, player.height, DustID.PinkFlame, 0f, 0f, 0, default(Color), 1.5f)];
                    dust1.position = position;
                    dust1.velocity = player.velocity.RotatedBy(-1.5707963705062866, default(Vector2)) * 0.33f + player.velocity / 4f;
                    dust1.velocity.X -= player.velocity.X / 10f * l;
                    dust1.position += player.velocity.RotatedBy(-1.5707963705062866, default(Vector2));
                    dust1.fadeIn = 0.5f;
                    dust1.noGravity = true;
                }
            }

            //amulet of despair
            if (voidEnergyCharge > 0)
            {
                //Main.NewText(voidEnergyCharge);
                if (voidEnergyCharge < 2200)
                {
                    int num = (int)MathHelper.Lerp(10f, 1f, voidEnergyCharge / 2200f);
                    if (Main.rand.Next(num) == 0)
                    {
                        int num5 = Dust.NewDust(player.position, player.width, player.height, DustID.PinkFlame, 0f, 0f, 200, default(Color), 0.5f);
                        Main.dust[num5].noGravity = true;
                        Main.dust[num5].velocity *= 0.75f;
                        Main.dust[num5].fadeIn = 1.3f;
                        Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                        vector.Normalize();
                        vector *= (float)Main.rand.Next(50, 100) * 0.04f;
                        Main.dust[num5].velocity = vector;
                        vector.Normalize();
                        vector *= 34f;
                        Main.dust[num5].position = player.Center - vector;
                    }
                }
                else if (voidEnergyCharge > 2200 && voidEnergyCharge < 3600)
                {
                    int num = (int)MathHelper.Lerp(1f, 3f, (voidEnergyCharge - 2200) / (3600f - 2200f));
                    for (int i = 0; i < num; i++)
                    {
                        int num5 = Dust.NewDust(player.position, player.width, player.height, DustID.PinkFlame, 0f, 0f, 200, default(Color), 0.5f);
                        Main.dust[num5].noGravity = true;
                        Main.dust[num5].velocity *= 0.75f;
                        Main.dust[num5].fadeIn = 1.3f;
                        Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                        vector.Normalize();
                        vector *= (float)Main.rand.Next(50, 100) * 0.04f;
                        Main.dust[num5].velocity = vector;
                        vector.Normalize();
                        vector *= 34f;
                        Main.dust[num5].position = player.Center - vector;
                    }
                }
                else
                {
                    EyeDust(player, DustID.PinkFlame);
                }
            }
            if (voidEnergyTimer > 0)
            {
                player.moveSpeed *= 0.2f;
                player.magicDamage *= 1.2f;
                player.meleeDamage *= 1.2f;
                player.minionDamage *= 1.2f;
                player.rangedDamage *= 1.2f;
                player.thrownDamage *= 1.2f;
                voidEnergyTimer--;

                if (Main.rand.Next(4) == 0)
                {
                    Vector2 perturbedSpeed = new Vector2(5f, 5f).RotatedByRandom(MathHelper.ToRadians(360));
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("VoidSinewave"), 300, 0f, 0);
                }
            }

            if (energyWeaverArmor)
            {
                energyWeaverTimer--;
                if (energyWeaverTimer <= 0)
                {
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        NPC nPC = Main.npc[i];
                        if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= 250)
                        {
                            float Speed = 9f;
                            float rotation = (float)Math.Atan2(player.Center.Y - nPC.Center.Y, player.Center.X - nPC.Center.X);
                            if (energyWeaverTimer <= 0)
                            {
                                Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                                Projectile.NewProjectile(player.Center.X, player.Center.Y, speed.X, speed.Y, mod.ProjectileType("EnergyWeaverBeam"), 60, 5f, Main.myPlayer);
                                energyWeaverTimer = 90;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int dust = Main.rand.Next(2) == 0 ? 135 : 242;
                        int num5 = Dust.NewDust(player.position, player.width, player.height, dust, 0f, 0f, 200, default(Color), 0.5f);
                        Main.dust[num5].noGravity = true;
                        Main.dust[num5].velocity *= 0.75f;
                        Main.dust[num5].fadeIn = 1.3f;

                        Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                        vector.Normalize();
                        vector *= (float)Main.rand.Next(50, 100) * 0.04f;
                        Main.dust[num5].velocity = vector;
                        vector.Normalize();
                        vector *= 34f;
                        Main.dust[num5].position = player.Center - vector;
                    }
                }
            }

            if (overInfinityCharged > 0)
            {
                int num5 = Dust.NewDust(player.position, player.width, player.height, 66, 0f, 0f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 0.5f);
                Main.dust[num5].noGravity = true;
                Main.dust[num5].velocity *= 0.75f;
                Main.dust[num5].fadeIn = 1.3f;

                Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                vector.Normalize();
                vector *= (float)Main.rand.Next(50, 100) * 0.04f;
                Main.dust[num5].velocity = vector;
                vector.Normalize();
                vector *= 34f;
                Main.dust[num5].position = player.Center - vector;

                if (overInfinityCharged > 1800)
                {
                    infinityDeath = true;
                }
                else
                {
                    infinityDeath = false;
                }
                if (infinityDeath)
                {
                    player.lifeRegen -= 150;
                }
            }
            else
            {
                infinityDeath = false;
            }

            if (player.FindBuffIndex(mod.BuffType("ChaosShield")) == -1 && !player.dead)
            {
                chaosBoost = 0;
                chaosDamageBoost = 0;
            }

            if (chaosBoost > 0)
            {
                chaosDamageBoost = 1f + (chaosBoost / 5000);
                player.magicDamage *= chaosDamageBoost;
                player.meleeDamage *= chaosDamageBoost;
                player.minionDamage *= chaosDamageBoost;
                player.rangedDamage *= chaosDamageBoost;
                player.thrownDamage *= chaosDamageBoost;
            }

            if (voidWalkerAura > 0)
            {
                voidWalkerAura--;
                voidWalkerCooldown = 1800;
                EyeDust(player, DustID.PinkFlame);
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (!nPC.friendly && !nPC.boss && Vector2.Distance(player.Center, nPC.Center) <= 2000)
                    {
                        nPC.AddBuff(BuffID.Confused, 30);
                        nPC.AddBuff(mod.BuffType("ExtinctionCurse"), 30);
                    }
                }
            }
            if (voidWalkerCooldown > 0)
            {
                voidWalkerCooldown--;
            }
            if (voidWalkerRegen > 0)
            {
                voidWalkerRegen--;
                player.lifeRegen += 75;
            }

            #region encounters

            if (ElementsAwoken.encounter != 0)
            {
                if (ElementsAwoken.encounter == 1)
                {
                    screenshakeAmount = 5f;
                }
                if (ElementsAwoken.encounter >= 2)
                {
                    if (player.active && !player.dead) player.AddBuff(BuffID.Darkness, 60);
                }
                if (ElementsAwoken.encounterTimer == 3200)
                {
                    encounterTextTimer = 300;
                    encounterTextAlpha = 0;
                    if (ElementsAwoken.encounter == 1) encounterText = "I see it now.";
                    else if (ElementsAwoken.encounter == 2) encounterText = "Who are you really?";
                    else if (ElementsAwoken.encounter == 3) encounterText = "You seek power...";
                }
                else if (ElementsAwoken.encounterTimer == 2700)
                {
                    encounterTextTimer = 300;
                    encounterTextAlpha = 0;
                    if (ElementsAwoken.encounter == 1) encounterText = "A new challenger rises.";
                    else if (ElementsAwoken.encounter == 2) encounterText = "A simple lost soul?";
                    else if (ElementsAwoken.encounter == 3) encounterText = "But you haven't asked why.";
                }
                else if (ElementsAwoken.encounterTimer == 2200)
                {
                    encounterTextTimer = 300;
                    encounterTextAlpha = 0;
                    if (ElementsAwoken.encounter == 1) encounterText = "You have a long way to go.";
                    else if (ElementsAwoken.encounter == 2) encounterText = "The path ahead is long.";
                    else if (ElementsAwoken.encounter == 3) encounterText = "Does it matter?";
                }
                else if (ElementsAwoken.encounterTimer == 1700)
                {
                    if (ElementsAwoken.encounter != 1)
                    {
                        encounterTextTimer = 300;
                        encounterTextAlpha = 0;
                    }
                    if (ElementsAwoken.encounter == 2) encounterText = "You are getting closer.";
                    else if (ElementsAwoken.encounter == 3)
                    {
                        encounterText = "You are almost there.";
                        Main.PlaySound(29, (int)player.position.X, (int)player.position.Y, 105, 1f, 0f);
                        finalText = true;
                    }
                }
                encounterTextTimer--;
                if (encounterTextTimer <= 0)
                {
                    encounterTextTimer = 0;
                    finalText = false;
                }

                if (encounterTextTimer > 60)
                {
                    if (encounterTextAlpha > 255) encounterTextAlpha = 255;
                    else encounterTextAlpha += (int)Math.Ceiling(255f / 60f);
                }
                else
                {
                    if (encounterTextAlpha < 0) encounterTextAlpha = 0;
                    else encounterTextAlpha -= (int)Math.Ceiling(255f / 60f);
                }
            }

            #endregion encounters

            ComputerText();

            if (aboveHeadTimer > 0) aboveHeadTimer--;
            else aboveHeadText = "";
            if (plateauCinematic)
            {
                CinematicSequence();
                if (plateauCinematicCounter == 0)
                {
                    screenTransition = true;
                }
                if (plateauCinematicCounter == screenTransDuration / 2)
                {
                    desiredScPos = new Vector2((EAWorldGen.plateauLoc.X + 75) * 16, (EAWorldGen.plateauLoc.Y + 75) * 16);
                    for (int k = 0; k < Main.npc.Length; k++)
                    {
                        NPC nPC = Main.npc[k];
                        if (nPC.active && !nPC.friendly && nPC.damage > 0) nPC.active = false;
                    }
                }
                if (plateauCinematicCounter > screenTransDuration / 2)
                {
                    desiredScPos += new Vector2(2, 0);
                }
                int duration = 600;
                if (plateauCinematicCounter == duration / 2)
                {
                    MyWorld.awakenedPlateau = true;
                    Projectile.NewProjectile(Main.screenPosition.X + Main.screenWidth / 2, Main.screenPosition.Y + Main.screenHeight / 2, 0f, 0f, ProjectileType<PlateauShockwave>(), 0, 0, Main.myPlayer, 0f, 0f);
                    Main.PlaySound(29, -1, -1, 21, 1.3f, -0.9f);
                }
                plateauCinematicCounter++;
                if (plateauCinematicCounter > duration)
                {
                    screenTransition = true;
                    if (plateauCinematicCounter - duration == screenTransDuration / 2)
                    {
                        plateauCinematic = false;
                        plateauCinematicCounter = 0;
                        player.hideMisc[0] = false;
                        player.hideMisc[1] = false;
                        desiredScPos = player.Center;
                    }
                }
            }
            else
            {
                plateauCinematicCounter = 0;
            }
            // credits code
            if (creditsTimer >= 0)
            {
                CinematicSequence();

                if (creditPoints.Count == 0)
                {
                    // find key points
                    for (int x = 0; x < Main.tile.GetLength(0); ++x)
                    {
                        for (int y = 0; y < Main.tile.GetLength(1); ++y)
                        {
                            if (Main.tile[x, y] == null) continue;
                            //temple
                            else if (Main.tile[x, y].type == TileID.LihzahrdAltar) RegisterCreditsPoint(new Vector2(x * 16, y * 16), new Vector2(0, -1), "Jungle Temple", 1);
                            // jungle with hive
                            else if (Main.tile[x, y].type == TileID.Hive) RegisterCreditsPoint(new Vector2(x * 16, y * 16), new Vector2(1, 1), "Hive", 2);
                            // spidernest
                            else if (Main.tile[x, y].wall == WallID.SpiderUnsafe) RegisterCreditsPoint(new Vector2(x * 16, y * 16), new Vector2(1, -1), "Spider Caves", 3);
                            // sky island
                            else if (Main.tile[x, y].type == TileID.Sunplate) RegisterCreditsPoint(new Vector2(x * 16, y * 16), new Vector2(-1, 1), "Sky Island", 4);
                            // hallow
                            else if (Main.tile[x, y].type == TileID.HallowedGrass) RegisterCreditsPoint(new Vector2(x * 16, y * 16), new Vector2(1, 1), "Hallowed Grass", 6);
                            // evil
                            else if (Main.tile[x, y].type == TileID.FleshGrass && WorldGen.crimson) RegisterCreditsPoint(new Vector2(x * 16, y * 16), new Vector2(1, -1), "Crimson", 7);
                            else if (Main.tile[x, y].type == TileID.CorruptGrass && !WorldGen.crimson) RegisterCreditsPoint(new Vector2(x * 16, y * 16), new Vector2(1, -1), "Corruption", 7);
                            // mushroom
                            else if (Main.tile[x, y].type == TileID.MushroomGrass) RegisterCreditsPoint(new Vector2(x * 16, y * 16), new Vector2(1, 1), "Glowing Mushroom", 8);
                            //snow
                            else if (Main.tile[x, y].type == TileID.SnowBlock) RegisterCreditsPoint(new Vector2(x * 16, y * 16), new Vector2(1, -1), "Snow", 9);
                            else if (Main.tile[x, y].type == TileID.Marble && x > Main.spawnTileX - 200) RegisterCreditsPoint(new Vector2(x * 16, y * 16), new Vector2(1, 1), "Marble", 13);
                            else if (Main.tile[x, y].type == TileID.Granite && x > Main.spawnTileX - 200) RegisterCreditsPoint(new Vector2(x * 16, y * 16), new Vector2(1, 1), "Granite", 14);
                            else if (Main.tile[x, y].type == TileID.LivingMahogany) RegisterCreditsPoint(new Vector2(x * 16, y * 16), new Vector2(1, -1), "Living Mahogany Tree", 15);
                            else if (Main.tile[x, y].type == TileID.LeafBlock && x > Main.spawnTileX - 200) RegisterCreditsPoint(new Vector2(x * 16, y * 16), new Vector2(0, -1), "Big Tree", 16);
                            else continue;
                        }
                    }
                    if (EAWorldGen.generatedPlateaus) RegisterCreditsPoint(new Vector2((EAWorldGen.plateauLoc.X + 300) * 16, (EAWorldGen.plateauLoc.Y + 100) * 16), new Vector2(1.5f, 0.25f), "Volcanic Plateau", 5);
                    RegisterCreditsPoint(new Vector2(Main.spawnTileX * 16 + 600, (Main.maxTilesY - 200) * 16), new Vector2(1, 0), "Hell", 10);
                    RegisterCreditsPoint(new Vector2(Main.dungeonX, Main.dungeonY) * 16, new Vector2(1.25f, 1), "Dungeon", 11);
                    RegisterCreditsPoint(new Vector2(1800, (float)(Main.worldSurface - 150) * 16), new Vector2(1, 0), "Ocean", 12);
                    player.FindSpawn();
                    Vector2 spawn = new Vector2(player.SpawnX * 16, player.SpawnY * 16);
                    if (spawn.X < 0 || spawn.Y < 0) spawn = new Vector2(Main.spawnTileX * 16, Main.spawnTileY * 16);
                    RegisterCreditsPoint(spawn, new Vector2(1, -1), "Spawn", 0);

                    creditPoints.Sort((x, y) => x.value.CompareTo(y.value));
                }
                if (creditsTimer == 0)
                {
                    startTime = (int)Main.time;
                    startDayTime = Main.dayTime;
                    screenTransition = true;
                }
                if (creditsTimer == screenTransDuration / 2)
                {
                    desiredScPos = creditPoints[0].pos - Vector2.One * 50;
                    for (int k = 0; k < Main.npc.Length; k++)
                    {
                        NPC nPC = Main.npc[k];
                        if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.GetGlobalNPC<NPCsGLOBAL>().saveNPC) Main.npc[k].active = false;
                    }
                }
                if (creditsTimer > screenTransDuration / 2)
                {
                    Main.dayTime = true;
                    Main.time = 27000;
                }
                if (creditsTimer > screenTransDuration / 2 && creditsTimer < screenDuration + screenTransDuration / 2) desiredScPos += new Vector2(1, 1);

                int creditsLength = screenDuration * 12 + 300;
                if (creditsTimer >= screenDuration && creditsTimer < creditsLength)
                {
                    int screenNum = (int)Math.Floor((decimal)(creditsTimer / screenDuration));
                    CreditsScroll(screenNum, creditPoints[screenNum].scroll, screenDuration);
                }

                Keys[] pressedKeys = Main.keyState.GetPressedKeys();

                bool escPressed = false;
                for (int j = 0; j < pressedKeys.Length; j++)
                {
                    Keys key = pressedKeys[j];
                    if (key == Keys.Escape) escPressed = true;
                }
                if (escPressed && escHeldTimer <= 60) escHeldTimer++;
                if (!escPressed && escHeldTimer > 0) escHeldTimer--;
                if (escHeldTimer > 60) creditsTimer = creditsLength - 1;

                creditsTimer++;
                if (creditsTimer > creditsLength)
                {
                    screenTransition = true;
                    if (creditsTimer - creditsLength == screenTransDuration / 2)
                    {
                        creditsTimer = -1;
                        player.hideMisc[0] = false;
                        player.hideMisc[1] = false;
                        desiredScPos = player.Center;
                        Main.time = startTime;
                        Main.dayTime = startDayTime;
                    }
                }
            }
            else
            {
                creditsTimer = -1;
                escHeldTimer = 0;
            }
            if (screenTransition)
            {
                screenTransTimer += (float)(Math.PI / screenTransDuration);
                screenTransAlpha = (float)Math.Sin(screenTransTimer);
                if (screenTransTimer >= Math.PI)
                {
                    screenTransTimer = 0;
                    screenTransition = false;
                }
            }

            #region PROMPTS!!

            if (!GetInstance<Config>().promptsDisabled)
            {
                bool activeBoss = false;
                bool inTown = false;
                for (int i = 0; i < Main.npc.Length; ++i)
                {
                    NPC nPC = Main.npc[i];

                    if (nPC.townNPC && nPC.active && Vector2.Distance(player.Center, nPC.Center) <= 2000)
                    {
                        inTown = true;
                    }
                    if (nPC.boss && nPC.active)
                    {
                        activeBoss = true;
                    }
                }
                bool underground = player.Center.Y / 16 > Main.maxTilesY * 0.225;

                if (!activeBoss)
                {
                    // only happens after 30 minutes
                    if (MyWorld.desertPrompt > ElementsAwoken.bossPromptDelay)
                    {
                        player.AddBuff(mod.BuffType("ScorpionBreakout"), 60);
                        // spawn code in the scorpion code
                    }
                    if (MyWorld.firePrompt > ElementsAwoken.bossPromptDelay)
                    {
                        player.AddBuff(mod.BuffType("InfernacesWrath"), 60);
                        if (!underground)
                        {
                            float num13 = MathHelper.Lerp(0.2f, 0.35f, 0.5f);
                            float num14 = MathHelper.Lerp(0.5f, 0.7f, 0.5f);
                            Vector2 speed = new Vector2(Main.windSpeed * 10f, 3f);
                            /*if (Main.rand.Next(3) == 0 && !GetInstance<Config>().lowDust)
                            {
                                Dust ash = Main.dust[Dust.NewDust(new Vector2(player.Center.X - Main.screenWidth / 2, player.Center.Y - Main.screenHeight / 2), Main.screenWidth, Main.screenHeight, 31, speed.X, speed.Y, 0, default(Color), 1f)];
                                ash.scale = 1.2f;
                                ash.fadeIn += num14 * 0.2f;
                                ash.velocity *= 1f + num13 * 0.5f;
                                ash.velocity *= 1f + num13;
                             }*/

                            if (Main.rand.Next(120) == 0 && !inTown)
                            {
                                int add2 = Main.rand.Next(1000, 2000); // y
                                int add1 = 0;
                                float ai0 = Main.rand.NextFloat(1, 2); // x

                                int choice = Main.rand.Next(2);
                                if (choice == 0)
                                {
                                    add1 = Main.rand.Next(650, 1500);
                                }
                                if (choice == 1)
                                {
                                    add1 = Main.rand.Next(-1500, -650);
                                }

                                int npc = NPC.NewNPC((int)player.Center.X + add1, (int)player.Center.Y - add2, mod.NPCType("SolarFragment"), player.whoAmI); // type 519

                                Vector2 newVelocity = -Vector2.UnitY.RotatedByRandom(0.78539818525314331) * (7f + Main.rand.NextFloat() * 5f);
                                Main.npc[npc].velocity = newVelocity;
                                if (!Main.expertMode)
                                {
                                    Main.npc[npc].damage = 30;
                                }
                                else
                                {
                                    Main.npc[npc].damage = 60;
                                }
                            }
                        }
                    }
                    if (MyWorld.skyPrompt > ElementsAwoken.bossPromptDelay)
                    {
                        player.AddBuff(mod.BuffType("DarkenedSkies"), 60);
                        if (!underground)
                        {
                            if (Main.raining && Main.rainTime > 0 && Main.rand.Next(250) == 0 && !inTown)
                            {
                                int add2 = Main.rand.Next(1000, 2000);
                                int add1 = 0;
                                float ai0 = Main.rand.NextFloat(1, 2);

                                int choice = Main.rand.Next(2);
                                if (choice == 0)
                                {
                                    add1 = Main.rand.Next(300, 1500);
                                }
                                if (choice == 1)
                                {
                                    add1 = Main.rand.Next(-1500, -300);
                                }
                                Projectile.NewProjectile(player.Center.X + add1, player.Center.Y - add2, 0f, 6f, 580, 40, 10f, player.whoAmI, ai0, 0.0f);
                            }
                        }
                    }
                    if (MyWorld.frostPrompt > ElementsAwoken.bossPromptDelay)
                    {
                        hypoChillTimer--;
                        //dividing by 16 gets the TILE position
                        Point topLeft = ((player.position - new Vector2(112f, 112f)) / 16).ToPoint();
                        Point bottomRight = ((player.BottomRight + new Vector2(112f, 112f)) / 16).ToPoint();

                        // draws dust where the hitbox is
                        /*for (int i = 0; i < 20; i++)
                        {
                            int dust = Dust.NewDust(new Vector2(topLeft.X * 16, topLeft.Y * 16), (bottomRight.X - topLeft.X) * 16, (bottomRight.Y - topLeft.Y) * 16, 57, 0f, 0f, 100);
                            Main.dust[dust].velocity *= 0.01f;
                        }*/
                        // needs to check all of the tiles
                        bool nearHotTile = false;
                        for (int i = topLeft.X; i <= bottomRight.X; i++)
                        {
                            for (int j = topLeft.Y; j <= bottomRight.Y; j++)
                            {
                                Tile t = Framing.GetTileSafely(i, j);
                                if (HotTile(t) || t.lava())
                                {
                                    nearHotTile = true;
                                    //Main.NewText("AAAAA TOO HOT", 182, 15, 15, false);
                                }
                            }
                        }
                        if (!nearHotTile)
                        {
                            player.AddBuff(mod.BuffType("Hypothermia"), 30);
                            if (hypoChillTimer <= 0)
                            {
                                player.AddBuff(BuffID.Chilled, Main.rand.Next(60, 600));
                                hypoChillTimer = Main.rand.Next(900, 1200);
                            }
                        }
                        //HAILSTORM
                        if (MyWorld.hailStormTime > 0 && !player.ZoneDesert)
                        {
                            if (!underground)
                            {
                                player.AddBuff(BuffID.WindPushed, 60);
                                if (Main.rand.Next(5) == 0)
                                {
                                    Vector2 speed = new Vector2(Main.windSpeed * 12f, 14f);
                                    int damage = Main.expertMode ? 30 : 45;
                                    Projectile.NewProjectile(player.Center.X + Main.rand.Next(-2000, 2000), player.Center.Y - 1000, speed.X * 8, speed.Y, mod.ProjectileType("HailPellet"), damage, 10f, player.whoAmI);
                                }
                            }
                        }
                    }
                    if (MyWorld.waterPrompt > ElementsAwoken.bossPromptDelay)
                    {
                        player.AddBuff(mod.BuffType("StormSurge"), 60);

                        if (!Main.raining && Main.rand.Next(5000) == 0)
                        {
                            Main.raining = true;
                            Main.rainTime = 18000;
                        }

                        if (!underground)
                        {
                            if (player.ZoneBeach)
                            {
                                if (Main.rand.Next(100) == 0)
                                {
                                    float ai0 = Main.rand.NextFloat(1, 2);
                                    int add1 = 0;
                                    int choice = Main.rand.Next(2);
                                    if (choice == 0)
                                    {
                                        add1 = Main.rand.Next(750, 2000);
                                    }
                                    if (choice == 1)
                                    {
                                        add1 = Main.rand.Next(-2000, -750);
                                    }
                                    Projectile.NewProjectile(player.Center.X + add1, player.Center.Y - 300, 0f, 6f, mod.ProjectileType("WaternadoBolt"), 90, 10f, player.whoAmI, ai0, 0.0f);
                                }
                            }
                            else if (!inTown)
                            {
                                if (Main.rand.Next(3000) == 0)
                                {
                                    float ai0 = Main.rand.NextFloat(1, 2);
                                    int add1 = 0;
                                    int choice = Main.rand.Next(2);
                                    if (choice == 0)
                                    {
                                        add1 = Main.rand.Next(750, 2000);
                                    }
                                    if (choice == 1)
                                    {
                                        add1 = Main.rand.Next(-2000, -750);
                                    }
                                    Projectile.NewProjectile(player.Center.X + add1, player.Center.Y - 300, 0f, 6f, mod.ProjectileType("WaternadoBolt"), 90, 10f, player.whoAmI, ai0, 0.0f);
                                }
                            }
                        }
                    }
                    if (MyWorld.voidPrompt > ElementsAwoken.bossPromptDelay)
                    {
                        player.AddBuff(mod.BuffType("Psychosis"), 60);

                        if (Main.rand.Next(20) == 0)
                        {
                            int num1 = Dust.NewDust(player.position, player.width, player.height, 14);
                            Main.dust[num1].scale = 1.5f;
                            Main.dust[num1].velocity *= 3f;
                            Main.dust[num1].noGravity = true;
                        }

                        if (Main.rand.Next(2000) == 0)
                        {
                            int choice = Main.rand.Next(12);
                            if (choice == 0)
                            {
                                Main.NewText("Death will consume all.", Color.Purple.R, Color.Purple.G, Color.Purple.B);
                                Main.NewText("Void Leviathan has awoken!", Color.MediumPurple.R, Color.MediumPurple.G, Color.MediumPurple.B);
                                Main.PlaySound(15, (int)player.Center.X, (int)player.Center.Y, 0, 1f, 0f);
                            }
                            else if (choice == 1) Main.NewText("Impending doom approaches...", Color.PaleGreen.R, Color.PaleGreen.G, Color.PaleGreen.B);
                            else if (choice == 2) Main.NewText("You're not ready for this", Color.Red.R, Color.Red.G, Color.Red.B);
                            else if (choice == 3) Main.NewText("Leave", Color.Red.R, Color.Red.G, Color.Red.B);
                            else if (choice == 4) Main.PlaySound(4, (int)player.Center.X, (int)player.Center.Y, 62, 1f, 0f);
                            else if (choice == 5) Main.PlaySound(4, (int)player.Center.X, (int)player.Center.Y, 59, 1f, 0f);
                            else if (choice == 6) Main.PlaySound(4, (int)player.Center.X, (int)player.Center.Y, 51, 1f, 0f);
                            else if (choice == 7) Main.PlaySound(15, (int)player.Center.X, (int)player.Center.Y, 0, 1f, 0f);
                            else if (choice == 8) Main.PlaySound(15, (int)player.Center.X, (int)player.Center.Y, 2, 1f, 0f);
                            else if (choice == 9) Main.PlaySound(29, (int)player.Center.X, (int)player.Center.Y, 105, 1f, 0f);
                            else if (choice == 10) Main.PlaySound(29, (int)player.Center.X, (int)player.Center.Y, 104, 1f, 0f);
                            else if (choice == 11)
                            {
                                int guide = NPC.FindFirstNPC(NPCID.Guide);
                                if (guide >= 0 && Main.rand.Next(5) == 0)
                                {
                                    Main.NewText(Main.npc[guide].GivenName + "the Guide was slain...", Color.Red.R, Color.Red.G, Color.Red.B);
                                }
                            }
                        }
                        if (Main.rand.Next(1500) == 0)
                        {
                            int add1 = 0;
                            int choice = Main.rand.Next(2);
                            if (choice == 0) add1 = Main.rand.Next(750, 2000);
                            else if (choice == 1) add1 = Main.rand.Next(-2000, -750);

                            int type = 0;
                            int choice2 = Main.rand.Next(6);
                            if (choice2 == 0) type = 524; // ghoul
                            else if (choice2 == 1) type = 524; // ghoul
                            else if (choice2 == 2) type = 258; // ladybug
                            else if (choice2 == 3) type = 93; // giant bat
                            else if (choice2 == 4) type = 78; // mummy
                            else if (choice2 == 5) type = 34; // cursed skull
                            NPC npc = Main.npc[NPC.NewNPC((int)player.Center.X + add1, (int)player.Center.Y - 800, type)];
                            npc.color = new Color(66, 66, 66);
                            npc.alpha = 200;
                            npc.damage = 0;
                            npc.lifeMax = 10;
                            npc.life = 10;
                            npc.DeathSound = SoundID.NPCDeath6;
                        }
                    }
                }
            }

            #endregion PROMPTS!!

            if (!cantGrapple) player.controlHook = false;
        }
        private void RegisterCreditsPoint(Vector2 pos, Vector2 scroll, string name, int value)
        {
            for (int i = 0; i < creditPoints.Count; i++)
            {
                if (creditPoints[i].name == name) return;
            }
            CreditPoint temp = new CreditPoint();
            temp.pos = pos;
            temp.scroll = scroll;
            temp.name = name;
            temp.value = value;
            creditPoints.Add(temp);
        }
        public override void PreUpdateMovement()
        {
            if (arid && !Main.tileSolid[Framing.GetTileSafely((int)(player.Bottom.X / 16), (int)(player.Bottom.Y / 16)).type])
            {
                aridFalling = player.velocity.Y;
            }
            /*int npcID = NPC.FindFirstNPC(NPCType<FloatyBoi>());
            if (npcID >= 0)
            {
                Vector2 nextPos = player.position + player.velocity;
                NPC npc = Main.npc[npcID];
                if (player.Top.Y >= npc.Bottom.Y || player.Bottom.Y <= npc.Top.Y)
                {
                    if (nextPos.X + (float)player.width > npc.position.X && nextPos.X < npc.position.X + (float)npc.width && nextPos.Y + (float)player.height > npc.position.Y && nextPos.Y < npc.position.Y + (float)npc.height)
                    {
                        //player.velocity = Vector2.Zero;
                        Vector2 move = Vector2.Zero;

                        int pushDirY = Math.Sign(player.Center.Y - npc.Center.Y);
                        float pushY = (player.Center.Y + -pushDirY * (player.height / 2)) - (npc.Center.Y + pushDirY * (npc.height / 2));
                        player.position.Y -= pushY * pushDirY;
                        if (pushDirY == -1) player.velocity.Y = 0;
                    }
                }
                if (player.Left.X < npc.Left.X || player.Right.X > npc.Right.X)
                {
                    if (nextPos.X + (float)player.width > npc.position.X && nextPos.X < npc.position.X + (float)npc.width && nextPos.Y + (float)player.height > npc.position.Y && nextPos.Y < npc.position.Y + (float)npc.height)
                    {
                        int pushDirX = Math.Sign(player.Center.X - npc.Center.X);
                        float pushX = (player.Center.X + -pushDirX * (player.width / 2)) - (npc.Center.X + pushDirX * (npc.width / 2));
                        player.position.X -= pushX;
                    }
                }
            }*/
        }

        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            Item wings = UI.BootWingsUI.itemSlot.Item;

            if (wings.stack > 0 && flyingBoots)
            {
                player.VanillaUpdateAccessory(player.whoAmI, wings, false, ref wallSpeedBuff, ref tileSpeedBuff, ref tileRangeBuff);
                player.VanillaUpdateEquip(wings);
            }
        }


        private void CinematicSequence()
        {
            player.immune = true;
            player.statLife = player.statLifeMax2;
            for (int i = 0; i < 22; i++)
            {
                player.DelBuff(i);
            }
            player.hideMisc[0] = true;
            player.hideMisc[1] = true;
            player.mount._active = false;

            CantMove();
            player.controlInv = false;
            player.controlMap = false;
            player.releaseInventory = false;
            Main.playerInventory = false;
            Main.inputTextEnter = false;
            Main.menuMode = 0;
            for (int k = 0; k < Main.maxProjectiles; k++)
            {
                if (Main.projectile[k].type != ProjectileType<PlateauShockwave>()) Main.projectile[k].Kill();
            }
            for (int k = 0; k < Main.dust.Length; k++)
            {
                Dust dust = Main.dust[k];
                if (Vector2.Distance(dust.position, player.Center) < 90)
                    dust.active = false;
            }
        }

        private void CreditsScroll(int screenNum, Vector2 scroll, int screenDuration)
        {
            int counterN = screenDuration * (screenNum - pointsNotFound);
            if (creditsTimer == counterN) screenTransition = true;
            if (creditsTimer - counterN == screenTransDuration / 2) desiredScPos = creditPoints[screenNum].pos;
            //if (creditsTimer - counterN > screenTransDuration / 2) player.Center = desiredScPos;
            if (creditsTimer - counterN > screenTransDuration / 2 && creditsTimer > counterN && creditsTimer < screenDuration * (screenNum + 1) + screenTransDuration / 2) desiredScPos += scroll;
        }

        private static bool HotTile(Tile t)
        {
            if (t.type == TileID.Campfire ||
                t.type == TileID.Fireplace) return true;
            return false;
        }

        public override void PostUpdate() // misc effects is called before wing update and other stuff
        {
            if (skylineFlying)
            {
                if (skylineAlpha < 1) skylineAlpha += 0.05f;
            }
            else
            {
                if (skylineAlpha > 0) skylineAlpha -= 0.05f;
            }
        }

        public override void PostUpdateBuffs()
        {
            if (oiniteStatue)
            {
                // it is always checking all 22 slots no matter if its active, so we check if its active by player.buffTime[l] <= 0 - angry orang
                for (int l = 0; l < Player.MaxBuffs; l++)
                {
                    if (player.buffTime[l] <= 0)
                    {
                        oiniteDoubledBuff[l] = false;
                        continue;
                    }
                    if (!oiniteDoubledBuff[l])
                    {
                        if (player.buffType[l] != BuffID.PotionSickness &&
                            player.buffType[l] != BuffID.ManaSickness &&
                            !Main.buffNoTimeDisplay[l])
                            player.buffTime[l] *= 2;
                        oiniteDoubledBuff[l] = true; // set the buff to doubled anyway so it stops checking
                    }
                }
            }
            else
            {
                for (int l = 0; l < Player.MaxBuffs; l++)
                {
                    if (oiniteDoubledBuff[l])
                    {
                        if (player.buffType[l] != BuffID.PotionSickness &&
                            player.buffType[l] != BuffID.ManaSickness &&
                            !Main.buffNoTimeDisplay[l])
                            player.buffTime[l] /= 2;
                        oiniteDoubledBuff[l] = false;
                    }
                }
            }
            if (discordantPotion)
            {
                for (int l = 0; l < Player.MaxBuffs; l++)
                {
                    if (player.buffType[l] == BuffID.ChaosState)
                    {
                        if (player.buffTime[l] >= 180) player.buffTime[l] = 180;
                        break;
                    }
                }
            }
        }

        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
        {
            Item item = new Item();
            item.SetDefaults(mod.ItemType("ElementalCapsule"));
            items.Add(item);
            Item item2 = new Item();
            item2.SetDefaults(mod.ItemType("MysticGemstone"));
            items.Add(item2);
            Item item3 = new Item();
            item3.SetDefaults(mod.ItemType("VoidbloodHeart"));
            items.Add(item3);
        }

        public override void OnEnterWorld(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                if (!InstalledPack())
                {
                    Main.NewText("You dont have a music pack enabled! All EA bosses will have vanilla music themes. Consider installing:", Color.Red.R, Color.Red.G, Color.Red.B);
                    Main.NewText("Elements Awoken Music", Color.Purple.R, Color.Purple.G, Color.Purple.B);
                    Main.NewText("EA Retro Music", Color.Purple.R, Color.Purple.G, Color.Purple.B);
                }
                if (!EAWorldGen.generatedPlateaus)
                {
                    Main.NewText("The EA Volcanic Plateaus has not been generated. You will miss out on lots of content", Color.Red.R, Color.Red.G, Color.Red.B);
                }
                Mod yabhb = ModLoader.GetMod("ExtensibleInventory");
                if (yabhb != null)
                {
                    Main.NewText("You have 'Extensible Inventory' enabled. The energy system will not work with this mod.", Color.Red.R, Color.Red.G, Color.Red.B);
                }
            }
            observerChanceTimer = 0;
            toySlimeChanceTimer = 0;

            encounterTextAlpha = 0;
            encounterTextTimer = 0;
        }

        private bool InstalledPack()
        {
            if (ElementsAwoken.eaMusicEnabled)
            {
                return true;
            }
            if (ElementsAwoken.eaRetroMusicEnabled)
            {
                return true;
            }
            return false;
        }

        public override void PostItemCheck()
        {
            Item item = player.inventory[player.selectedItem];
            if (item.type != 0)
            {
                if (item.type == mod.ItemType("LavaLeecher") || item.type == mod.ItemType("FireFlooder"))
                {
                    if (!Main.GamepadDisableCursorItemIcon)
                    {
                        player.showItemIcon = true;
                        Main.ItemIconCacheUpdate(item.type);
                    }
                    if (player.itemTime == 0 && player.itemAnimation > 0 && player.controlUseItem)
                    {
                        if (item.type == mod.ItemType("LavaLeecher") && Main.tile[Player.tileTargetX, Player.tileTargetY].liquidType() == 1)
                        {
                            int num233 = (int)Main.tile[Player.tileTargetX, Player.tileTargetY].liquidType();
                            int num234 = 0;
                            for (int num235 = Player.tileTargetX - 1; num235 <= Player.tileTargetX + 1; num235++)
                            {
                                for (int num236 = Player.tileTargetY - 1; num236 <= Player.tileTargetY + 1; num236++)
                                {
                                    if ((int)Main.tile[num235, num236].liquidType() == num233)
                                    {
                                        num234 += (int)Main.tile[num235, num236].liquid;
                                    }
                                }
                            }
                            if (Main.tile[Player.tileTargetX, Player.tileTargetY].liquid > 0 && (num234 > 100 || item.type == mod.ItemType("LavaLeecher")))
                            {
                                int liquidType = (int)Main.tile[Player.tileTargetX, Player.tileTargetY].liquidType();

                                Main.PlaySound(19, (int)player.position.X, (int)player.position.Y, 1, 1f, 0f);
                                player.itemTime = item.useTime;
                                int num237 = (int)Main.tile[Player.tileTargetX, Player.tileTargetY].liquid;
                                Main.tile[Player.tileTargetX, Player.tileTargetY].liquid = 0;
                                Main.tile[Player.tileTargetX, Player.tileTargetY].lava(false);
                                Main.tile[Player.tileTargetX, Player.tileTargetY].honey(false);
                                WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, false);
                                if (Main.netMode == 1)
                                {
                                    NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
                                }
                                else
                                {
                                    Liquid.AddWater(Player.tileTargetX, Player.tileTargetY);
                                }
                                for (int xPos = Player.tileTargetX - 1; xPos <= Player.tileTargetX + 1; xPos++)
                                {
                                    for (int yPos = Player.tileTargetY - 1; yPos <= Player.tileTargetY + 1; yPos++)
                                    {
                                        if (num237 < 256 && (int)Main.tile[xPos, yPos].liquidType() == num233)
                                        {
                                            int num240 = (int)Main.tile[xPos, yPos].liquid; // how much liquid
                                            if (num240 + num237 > 255)
                                            {
                                                num240 = 255 - num237;
                                            }
                                            num237 += num240;
                                            Tile tile = Main.tile[xPos, yPos];
                                            tile.liquid -= (byte)num240;
                                            Main.tile[xPos, yPos].liquidType(liquidType);
                                            if (Main.tile[xPos, yPos].liquid == 0)
                                            {
                                                Main.tile[xPos, yPos].lava(false);
                                                Main.tile[xPos, yPos].honey(false);
                                            }
                                            WorldGen.SquareTileFrame(xPos, yPos, false);
                                            if (Main.netMode == 1)
                                            {
                                                NetMessage.sendWater(xPos, yPos);
                                            }
                                            else
                                            {
                                                Liquid.AddWater(xPos, yPos);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        // place lava
                        else if (item.type == mod.ItemType("FireFlooder") && Main.tile[Player.tileTargetX, Player.tileTargetY].liquid < 200 && (!Main.tile[Player.tileTargetX, Player.tileTargetY].nactive() || !Main.tileSolid[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type] || Main.tileSolidTop[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type]))
                        {
                            if (Main.tile[Player.tileTargetX, Player.tileTargetY].liquid == 0 || Main.tile[Player.tileTargetX, Player.tileTargetY].liquidType() == 1)
                            {
                                Main.PlaySound(19, (int)player.position.X, (int)player.position.Y, 1, 1f, 0f);
                                Main.tile[Player.tileTargetX, Player.tileTargetY].liquidType(1);
                                Main.tile[Player.tileTargetX, Player.tileTargetY].liquid = 255;
                                WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, true);
                                player.itemTime = item.useTime;
                                if (Main.netMode == 1)
                                {
                                    NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
                                }
                            }
                        }
                    }
                }
                /*if (item.GetGlobalItem<Items.ItemsGlobal>().quicksandItem)
                {
                    if (player.itemTime == 0 && player.itemAnimation > 0 && player.controlUseItem && !Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY).active())
                    {
                        Tile below = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY + 1);
                        Tile above = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY - 1);
                        Tile right = Framing.GetTileSafely(Player.tileTargetX + 1, Player.tileTargetY);
                        Tile left = Framing.GetTileSafely(Player.tileTargetX - 1, Player.tileTargetY);
                        if (((Tiles.GlobalTiles.quicksands.Contains(below.type) || Main.tileSolid[below.type]) && below.active()) ||
                            ((Tiles.GlobalTiles.quicksands.Contains(above.type) || Main.tileSolid[above.type]) && above.active()) ||
                            ((Tiles.GlobalTiles.quicksands.Contains(right.type) || Main.tileSolid[right.type]) && right.active()) ||
                            ((Tiles.GlobalTiles.quicksands.Contains(left.type) || Main.tileSolid[left.type]) && left.active()))
                        {
                            WorldGen.PlaceTile(Player.tileTargetX, Player.tileTargetY, item.createTile);
                            player.itemTime = item.useTime;
                        }

                    }
                }*/
            }
            //catching the mystic bunny
            if (item.type == 1991 || item.type == 3183)
            {
                Rectangle rectangle = new Rectangle((int)player.itemLocation.X, (int)player.itemLocation.Y, 32, 32);

                for (int n = 0; n < Main.npc.Length; n++)
                {
                    NPC bunny = Main.npc[n];
                    if (bunny.active && bunny.type == mod.NPCType("MysticBunny"))
                    {
                        Rectangle value10 = new Rectangle((int)bunny.position.X, (int)bunny.position.Y, bunny.width, bunny.height);
                        if (rectangle.Intersects(value10) && (bunny.noTileCollide || player.CanHit(bunny)))
                        {
                            if (!bunny.active)
                            {
                                return;
                            }
                            if (Main.rand.Next(29) == 0)
                            {
                                new Item().SetDefaults(mod.ItemType("PiohsPresent"), false);
                                Item.NewItem((int)player.Center.X, (int)player.Center.Y, 0, 0, mod.ItemType("PiohsPresent"), 1, false, 0, true, false);
                            }
                            else if (Main.rand.Next(9) == 0)
                            {
                                new Item().SetDefaults(ItemID.FuzzyCarrot, false);
                                Item.NewItem((int)player.Center.X, (int)player.Center.Y, 0, 0, ItemID.FuzzyCarrot, 1, false, 0, true, false);
                            }
                            else
                            {
                                Vector2 vector = bunny.Center - new Vector2(20f);
                                Utils.PoofOfSmoke(vector);
                                NetMessage.SendData(106, -1, -1, null, (int)vector.X, vector.Y, 0f, 0f, 0, 0, 0);
                            }
                            bunny.active = false;
                            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n, 0f, 0f, 0f, 0, 0, 0);
                        }
                    }
                }
            }
        }

        public override bool PreItemCheck()
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active)
                    if (Main.npc[i].GetGlobalNPC<NPCsGLOBAL>().liftable && Main.npc[i].getRect().Contains(Main.MouseWorld.ToPoint())) hoveringLiftableNPC = true;
            }
            if (inMech)
            {
                //player.noItems = true;
                if (Main.mouseLeft || Main.mouseRight)
                {
                    if (Main.MouseWorld.X > player.Center.X) player.direction = 1;
                    if (Main.MouseWorld.X < player.Center.X) player.direction = -1;
                }
                if (Main.mouseLeft && generalTimer % 20 == 0)
                {
                    Point drillTilePos = Main.MouseWorld.ToTileCoordinates();
                    if (Vector2.Distance(drillTilePos.ToVector2(), player.Center.ToTileCoordinates().ToVector2()) < 10)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                if (!Main.tileAxe[(int)Main.tile[drillTilePos.X + i, drillTilePos.Y + j].type] && !Main.tileHammer[(int)Main.tile[drillTilePos.X + i, drillTilePos.Y + j].type])
                                {
                                    player.PickTile(drillTilePos.X + i, drillTilePos.Y + j, 60);
                                }
                            }
                        }
                    }
                }
                if (Main.mouseRight && !Main.mouseLeft && generalTimer % 6 == 0)
                {
                    Vector2 toMouse = Main.MouseWorld - player.Center;
                    toMouse.Normalize();
                    Projectile proj = Main.projectile[Projectile.NewProjectile(gunEnd, toMouse * 6, ProjectileType<Projectiles.NPCProj.AccursedBreath>(), 30, 0f, player.whoAmI, player.whoAmI)];
                    proj.friendly = true;
                    proj.hostile = false;
                }
                cantUseItems = true;
            }
            if (wispForm)
            {
                if (player.releaseUseItem && player.controlUseItem && wispAttackCD <= 0 && spirit >= 20)
                {
                    wispAttackCD = 30;
                    spirit -= 20;
                    spiritTimer = 0;

                    Vector2 shootFrom = player.Center + player.velocity * 6;

                    Main.PlaySound(SoundID.DD2_BetsyFireballShot, player.position);
                    Vector2 toMouse = Main.MouseWorld - player.Center;
                    toMouse.Normalize();
                    toMouse *= 8;
                    Projectile proj = Main.projectile[Projectile.NewProjectile(shootFrom, toMouse, ProjectileType<WispBoltReflector>(), 30, 7f, player.whoAmI, player.whoAmI)];
                    int numberProjectiles = 6;
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = toMouse.RotatedByRandom(MathHelper.ToRadians(12));
                        Projectile proj2 = Main.projectile[Projectile.NewProjectile(shootFrom, perturbedSpeed, ProjectileType<WispBolt>(), 0, 0, player.whoAmI)];
                        proj2.timeLeft = Main.rand.Next(15, 40);
                    }
                }
            }
            if (npcHeldThowCD > 0)
            {
                cantUseItems = true;
            }
            if (npcHeld != 0)
            {
                NPC holding = Main.npc[npcHeld];
                if ((player.releaseUseItem && player.controlUseItem) || (Main.mouseRight && Main.mouseRightRelease))
                {
                    if (Main.mouseRight)
                    {
                        holding.velocity.X = player.velocity.X;
                        holding.velocity.Y = player.velocity.Y;
                    }
                    else if (player.releaseUseItem)
                    {
                        holding.velocity.X = 5 * player.direction + player.velocity.X;
                        holding.velocity.Y = -4 + player.velocity.Y;
                    }
                    npcHeld = 0;
                    npcHeldThowCD = 20;
                }
                return false;
            }
            return base.PreItemCheck();
        }
        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
        {
            if (Main.rand.NextBool(3) && liquidType == 2) caughtType = ItemType<MajesticHivefish>();
            //crate chance
            if (Main.rand.Next(100) < (10 + (player.cratePotion ? 10 : 0)))
            {
                if (liquidType == 2) caughtType = ItemType<HiveCrate>();
            }
        }

        public override void UpdateBiomes()
        {
            zoneTemple = (MyWorld.lizardTiles > 50);
            Vector2 plateauPos = new Vector2(EAWorldGen.plateauLoc.X, EAWorldGen.plateauLoc.Y);
            Vector2 playerTile = player.position / 16;
            if (plateauCinematic) playerTile =(Main.screenPosition + new Vector2(Main.screenWidth / 2, Main.screenHeight / 2)) / 16;
            zonePlateau = playerTile.X > plateauPos.X - 20 && playerTile.Y > plateauPos.Y - 20 && playerTile.X < plateauPos.X + EAWorldGen.plateauWidth + 20 && EAWorldGen.generatedPlateaus;
            zoneSulphur = playerTile.X > plateauPos.X + 850 && playerTile.Y > plateauPos.Y - 20 && playerTile.X < plateauPos.X + EAWorldGen.plateauWidth - 30 && EAWorldGen.generatedPlateaus;
            Rectangle temple = new Rectangle(EAWorldGen.spiderTempleLoc.X + 4, EAWorldGen.spiderTempleLoc.Y + 4, 53, 49);
            zoneErius = temple.Contains(player.Center.ToTileCoordinates());
            zoneMineBoss = playerTile.X > EAWorldGen.mineBossArenaLoc.X && playerTile.X < EAWorldGen.mineBossArenaLoc.X + 100 && playerTile.Y > EAWorldGen.mineBossArenaLoc.Y && playerTile.Y < EAWorldGen.mineBossArenaLoc.Y + 18;
        }

        public override void UpdateAutopause()
        {
            lastChest = player.chest;
        }

        public override void PreUpdateBuffs()
        {
            if (Main.netMode != 1)
            {
                if (player.chest == -1 && lastChest >= 0 && Main.chest[lastChest] != null)
                {
                    int x2 = Main.chest[lastChest].x;
                    int y2 = Main.chest[lastChest].y;
                    ChestItemSummonCheck(x2, y2, mod);
                }
                lastChest = player.chest;
            }
        }

        public static bool ChestItemSummonCheck(int x, int y, Mod mod)
        {
            if (Main.netMode == 1 || !Main.hardMode)
            {
                return false;
            }
            int num = Chest.FindChest(x, y);
            if (num < 0)
            {
                return false;
            }
            int numKeys = 0;
            int numberOtherItems = 0;
            ushort tileType = Main.tile[Main.chest[num].x, Main.chest[num].y].type;
            int tileStyle = (int)(Main.tile[Main.chest[num].x, Main.chest[num].y].frameX / 36);
            if (TileID.Sets.BasicChest[tileType] && (tileStyle < 5 || tileStyle > 6))
            {
                for (int i = 0; i < 40; i++)
                {
                    if (Main.chest[num].item[i] != null && Main.chest[num].item[i].type > 0)
                    {
                        if (Main.chest[num].item[i].type == ItemType<Items.Other.KeyOfBright>())
                        {
                            numKeys += Main.chest[num].item[i].stack;
                        }
                        else
                        {
                            numberOtherItems++;
                        }
                    }
                }
            }
            if (numberOtherItems == 0 && numKeys == 1)
            {
                if (TileID.Sets.BasicChest[Main.tile[x, y].type])
                {
                    if (Main.tile[x, y].frameX % 36 != 0)
                    {
                        x--;
                    }
                    if (Main.tile[x, y].frameY % 36 != 0)
                    {
                        y--;
                    }
                    int number = Chest.FindChest(x, y);
                    for (int j = x; j <= x + 1; j++)
                    {
                        for (int k = y; k <= y + 1; k++)
                        {
                            if (TileID.Sets.BasicChest[Main.tile[j, k].type])
                            {
                                Main.tile[j, k].active(false);
                            }
                        }
                    }
                    for (int l = 0; l < 40; l++)
                    {
                        Main.chest[num].item[l] = new Item();
                    }
                    Chest.DestroyChest(x, y);
                    NetMessage.SendData(34, -1, -1, null, 1, (float)x, (float)y, 0f, number, 0, 0);
                    NetMessage.SendTileSquare(-1, x, y, 3);
                }
                int npcToSpawn = NPCType<NPCs.VolcanicPlateau.PlateauMimic>();
                int npcIndex = NPC.NewNPC(x * 16 + 16, y * 16 + 32, npcToSpawn, 0, 0f, 0f, 0f, 0f, 255);
                Main.npc[npcIndex].whoAmI = npcIndex;
                NetMessage.SendData(23, -1, -1, null, npcIndex, 0f, 0f, 0f, 0, 0, 0);
                Main.npc[npcIndex].BigMimicSpawnSmoke();
            }
            return false;
        }

        private void ComputerText()
        {
            int num16 = (int)(((double)player.position.X + (double)player.width * 0.5) / 16.0);
            int num17 = (int)(((double)player.position.Y + (double)player.height * 0.5) / 16.0);
            if (num16 < computerPos.X - Player.tileRangeX || num16 > computerPos.X + Player.tileRangeX + 1 || num17 < computerPos.Y - Player.tileRangeY || num17 > computerPos.Y + Player.tileRangeY + 1)
            {
                inComputer = false;
            }
            if (Main.playerInventory == true || player.sign != -1 || player.talkNPC != -1)
            {
                inComputer = false;
            }
            switch (computerTextNo)
            {
                case 0:
                    //no drive
                    computerText = "ERROR- DRIVE NOT LOCATED";
                    break;

                case 1:
                    //wasteland
                    computerText = "What is this monstrosity, what have we created?!\nThis... beast, it has killed the others, I hear it trying to \nbreak the door to my room... Its coming for me... \nAnybody who finds this note, END THIS MADNESS, \nI beg of you... Oh no.. Its her-";
                    break;

                case 2:
                    //infernace
                    computerText = "My colleagues found something very interesting within the\ndeepest part of this world. A sentient being controlling fire\nat will...unfortunately, we can't go down there, as it may\nkill us if we approach it, but...we will continue watching it.";
                    break;

                case 3:
                    //scourge fighter
                    computerText = "Our other team managed to lose control of the 4 robots\nthey've built. Seems like some people are better off being\nfired. Those things now roam the surface, I am not sure\nof what will happen...";
                    break;

                case 4:
                    //regaroth
                    computerText = "A massive amount of energy surged through one of our\nmachines, overloading it and causing it to blow up. The\nenergy readings were... outstanding, to say the least,\nalmost supernatural. It has probably to do with that\nthunder serpent, far up in the skies...";
                    break;

                case 5:
                    //the celestial
                    computerText = "When we attempted to find a source for the cultists to\nrelay celestial power from, we stumbled across an\nimperfect reflection of the celestial forces... However, it\nwill be hard to find a source of power from that creature.";
                    break;

                case 6:
                    //obsidious
                    computerText = "We've heard of some, artifact lost in the underworld but\nnever bothered to look for it. Recently we checked and it\nwas gone. I suppose someone else found it first, ah well...";
                    break;

                case 7:
                    //permafrost
                    computerText = "There appear to be signs of magic in the coldest parts of\nthe land, we have sent someone to look into it, but\nunfortunately we have not heard from them as of yet...\nI fear he has been frozen due to the cold. After all, these\nlands are rather, harsh, to take on alone.";
                    break;

                case 8:
                    //aqueous
                    computerText = "The tides have risen rather high, and there have been\nrather heavy amounts of rain lately, with terrifying\nconsistency no less. On top of that, there are multiple\nreports of tornados composed almost entirely of water.\nThe wrath of the ocean may be upon us soon..";
                    break;

                case 9:
                    //the guardian
                    if (guardianEntryNo == 0)
                    {
                        computerText = "001: This element, drakonite, is rather interesting to say\nthe least. We've taken a shard of it into our lab for\nfurther research. However, we should be prepared for\nwhatever danger lurks within this shard...";
                    }
                    else
                    {
                        computerText = "002: The drakonite shard, it had quite the reaction to lava\nand then right before us, a giant...thing began to\nbuild up. It was entirely made of drakonite! However it\nwas not friendly, we had to quickly evacuate to save\nourselves.";
                    }
                    break;

                case 10:
                    //volcanox
                    computerText = "After extensive research via our machines, we came to the\nconclusion that, if the moon lord were to be felled,\nthe underworld would burst out in extreme heat, and even \nthe most fire resistant gear would not last long... What \nsort of a being could withstand this heatwave?";
                    break;

                case 11:
                    //void leviathan
                    computerText = "My colleagues have been constantly hallucinating, and were\nshowing signs of sickness, as well as being easily\ntired. They are barely able to breathe. I am trying to find\nsome way to save them, however... I feel this will hit\nme as well. I need to get this cure done -at all costs!";
                    break;

                case 12:
                    //azana
                    if (azanaEntryNo == 0)
                    {
                        computerText = "001: This is going to be my final entry...\nI've lost everything. I had, all my colleagues... everything\nis gone and I'm alone. The chaos ravages the lands, and I\nam unable to do anything. There is no reason for me to\nlive anymore...";
                    }
                    else
                    {
                        computerText = "002: Whoever finds this...I beg of you, do not make the\nsame mistakes as we did...Do not..";
                    }
                    break;

                case 13:
                    //ancients
                    if (ancientsEntryNo == 0)
                    {
                        computerText = "003: We just witnissed something dangerous...some old\nman entered our laboratory and split into four crystalline\nmonsters, which roamed the entire place. Almost the\nentirety of our team is missing and all we found was some\ncrystal shards on the ground. We have to make some tests.";
                    }
                    else
                    {
                        computerText = "004: Upon further investigation, the shards belong to\na long forgotten race, thought to be eliminated.\nNo doubt, they could bring more chaos than anything\ncurrently ever could. And that there are four survivors\ngives me an awful feeling...";
                    }
                    break;

                default:
                    computerText = "muffins";
                    return;
            }
        }

        public override void FrameEffects()
        {
            if (player.mount.Active && player.mount.Type == mod.MountType("ElementalDragonBunny") && Math.Abs(player.velocity.X) > player.mount.DashSpeed - player.mount.RunSpeed / 3f)
            {
                player.armorEffectDrawShadow = true;
            }
            if (eaDashDelay > 0)
            {
                player.armorEffectDrawShadow = true;
            }
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (acidBurn)
            {
                r *= 0.69f;
                g *= 1f;
                b *= 0.48f;
            }
            if (toySlimeClawSliding)
            {
                player.bodyFrame.Y = player.bodyFrame.Height * 3;
            }
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (elementalArmor && !elementalArmorCooldown)
            {
                Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 113);
                Projectile.NewProjectile(player.position.X, player.position.Y, 0f, 0f, mod.ProjectileType("AsteroxShieldBase"), 0, 10f, player.whoAmI, 0.0f, 0.0f);
                int life = player.statLifeMax / 2;
                player.statLife += life;
                player.HealEffect(life);
                player.AddBuff(mod.BuffType("ElementalArmorCooldown"), 2700);
                return false;
            }
            if (noRespawnTime)
            {
                //player.respawnTimer = 0;
                player.statLife += player.statLifeMax;
                return false;
            }
            if (player.HasItem(mod.ItemType("DeathMirror")))
            {
                hellsReflectionTimer = 1800;
            }
            if (damageSource.SourceOtherIndex == 8)
            {
                string reason = "";
                if (sulphurBreath > sulphurBreathMax) reason = player.name + " choked to death.";
                else if (acidBurn) reason = player.name + " got liquefied.";
                else if (incineration) reason = player.name + " became ash.";
                else if (starstruck) reason = player.name + " is seeing stars.";
                else if (inQuicksand) reason = player.name + " sunk in the sand.";
                else if (voidMossStanding) reason = player.name + " got consumed by the void.";
                if (reason != "") damageSource = PlayerDeathReason.ByCustomReason(reason);
            }
            return true;
        }

        public override void PreUpdate()
        {
            int height = player.height;
            if (player.waterWalk) height -= 6;
            bool touchingLava = Collision.LavaCollision(player.position, player.width, height);
            if (touchingLava && zonePlateau && MyWorld.awakenedPlateau)
            {
                if (player.lavaTime > 0) player.lavaTime--;
                else if (!player.immune && player.HasBuff(BuffID.ObsidianSkin) && !player.HasBuff(BuffType<DrakoniteSkinBuff>()))
                {
                    player.Hurt(PlayerDeathReason.ByOther(2), 40, 0);
                }
            }
            if (forceWisp)
            {
                player.mount.SetMount(MountType<WispForm>(), player, false);
                player.releaseMount = false;
                player.controlHook = false;
                player.releaseHook = false;
                player.grappling[0] = -1;
                player.grapCount = 0;
            }
            /*// to fix broken players
            {
                lunarStarsUsed = 0;
                chaosHeartsUsed = 0;
                voidHeartsUsed = 0;
            }*/
            if (glassHeart)
            {
                player.immune = false;
                player.immuneTime = 0;
                player.shadowDodgeTimer = 0;
                player.shadowDodge = false;
                player.shadowDodgeCount = 0;
            }
            if (oiniteDoubledBuff.Length != Player.MaxBuffs)
            {
                Array.Resize(ref oiniteDoubledBuff, Player.MaxBuffs);
            }
        }

        public override void UpdateBadLifeRegen()
        {
            neovirtuoTimer--; // wtf is this doin ghere
                              // poison - 4
                              // on fire - 8
                              // frostburn - 12

            if (boostDriveTimer > 0 && boostDrive == 2)
            {
                if (Math.Abs(player.velocity.X) > 25)
                {
                    player.lifeRegen -= (int)MathHelper.Lerp(0, 30, (Math.Abs(player.velocity.X) - 25) / 5);
                }
            }

            if (acidBurn) player.lifeRegen -= 16;
            if (toySlimed > 0) player.lifeRegen -= 18;
            if (dragonfire || starstruck) player.lifeRegen -= 20;
            if (extinctionCurse || handsOfDespair) player.lifeRegen -= 30;
            if (chaosBurn || discordDebuff) player.lifeRegen -= 40;

            if (behemothGazeTimer > 600)
            {
                int amount = (int)MathHelper.Lerp(0, 80, (float)(leviathanDist - 3000) / 9000f);
                player.lifeRegen -= amount;
            }
            if (endlessTears) player.velocity *= 0.8f;
            if (iceBound)
            {
                player.velocity.Y = 0f;
                player.velocity.X = 0f;
            }
            if (cantFly)
            {
                if (player.wingTimeMax <= 0)
                {
                    player.wingTimeMax = 0;
                }
                player.wingTimeMax = 0;
                player.wingTime = 0;
            }
            if (incineration)
            {
                int amount = 20;
                if (NPC.downedMoonlord) amount = 35;
                if (zonePlateau && MyWorld.plateauWeather == 2) amount *= 2;
                else if (zonePlateau) amount = (int)(amount * 1.5f);
                player.lifeRegen -= amount;
            }
            if (sulphurBreath >= sulphurBreathMax)
            {
                player.accRunSpeed *= 0.5f;
                player.moveSpeed *= 0.5f;
                player.lifeRegen -= 60;
            }
            if (Framing.GetTileSafely((int)player.position.X / 16, (int)player.position.Y / 16 + 3).type == TileType<Tiles.VolcanicPlateau.VoidMoss>() ||
                Framing.GetTileSafely((int)player.position.X / 16 + 1, (int)player.position.Y / 16 + 3).type == TileType<Tiles.VolcanicPlateau.VoidMoss>())
            {
                voidMossStanding = true;
                player.lifeRegen -= 16;
            }
            if (player.lifeRegen < 0 && theAntidote)
            {
                player.lifeRegen /= 2;
            }
        }

        public override void UpdateLifeRegen()
        {
            if (creditsTimer >= 0) player.lifeRegen = 0; // to stop suffocation in sand and other things
            if (archaicProtectionTimer > 0) player.lifeRegen = 0;
            if (voidBlood && player.lifeRegen > 0) player.lifeRegen = 0;
            if (choking && player.lifeRegen > 1) player.lifeRegen = 1;
        }

        public override void NaturalLifeRegen(ref float regen)
        {
            if (voidBlood && regen > 0) regen = 0;
        }

        public override void UpdateBiomeVisuals()
        {
            bool useLeviathan = NPC.AnyNPCs(mod.NPCType("VoidLeviathanHead"));
            player.ManageSpecialBiomeVisuals("ElementsAwoken:VoidLeviathanHead", useLeviathan);

            bool useInfernace = NPC.AnyNPCs(mod.NPCType("Infernace"));
            player.ManageSpecialBiomeVisuals("ElementsAwoken:Infernace", useInfernace);

            bool usePermafrost = NPC.AnyNPCs(mod.NPCType("Permafrost"));
            player.ManageSpecialBiomeVisuals("ElementsAwoken:Permafrost", usePermafrost);

            bool useGuardian = NPC.AnyNPCs(mod.NPCType("TheGuardianFly"));
            player.ManageSpecialBiomeVisuals("ElementsAwoken:TheGuardianFly", useGuardian);

            bool useVolcanox = NPC.AnyNPCs(mod.NPCType("Volcanox"));
            player.ManageSpecialBiomeVisuals("ElementsAwoken:Volcanox", useVolcanox);

            bool useAzana = NPC.AnyNPCs(mod.NPCType("Azana"));
            player.ManageSpecialBiomeVisuals("ElementsAwoken:Azana", useAzana);

            bool useAncients = NPC.AnyNPCs(mod.NPCType("Izaris")) || NPC.AnyNPCs(mod.NPCType("Kirvein")) || NPC.AnyNPCs(mod.NPCType("Krecheus")) || NPC.AnyNPCs(mod.NPCType("Xernon")) || NPC.AnyNPCs(mod.NPCType("AncientAmalgam"));
            player.ManageSpecialBiomeVisuals("ElementsAwoken:Ancients", useAncients);

            bool useVoidEvent = MyWorld.voidInvasionUp && Main.time <= 16220 && !Main.dayTime;
            player.ManageSpecialBiomeVisuals("ElementsAwoken:VoidEvent", useVoidEvent);
            bool useVoidEventDark = MyWorld.voidInvasionUp && Main.time > 16220 && !Main.dayTime;
            player.ManageSpecialBiomeVisuals("ElementsAwoken:VoidEventDark", useVoidEventDark);

            bool useRadRain = MyWorld.radiantRain && player.position.Y / 16 < Main.worldSurface;
            player.ManageSpecialBiomeVisuals("ElementsAwoken:RadiantRain", useRadRain);

            int useRegaroth = 0;
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].active && Main.npc[i].type == mod.NPCType("RegarothHead"))
                {
                    if (Main.npc[i].life > Main.npc[i].lifeMax / 2)
                    {
                        useRegaroth = 1;
                        if (Main.npc[i].localAI[1] == 1)
                        {
                            useRegaroth = 3;
                        }
                    }
                    else
                    {
                        useRegaroth = 2;
                        if (Main.npc[i].localAI[1] == 1)
                        {
                            useRegaroth = 4;
                        }
                    }
                }
            }

            player.ManageSpecialBiomeVisuals("ElementsAwoken:Regaroth", useRegaroth == 1);
            player.ManageSpecialBiomeVisuals("ElementsAwoken:Regaroth2", useRegaroth == 2);

            player.ManageSpecialBiomeVisuals("ElementsAwoken:RegarothIntense", useRegaroth == 3);
            player.ManageSpecialBiomeVisuals("ElementsAwoken:Regaroth2Intense", useRegaroth == 4);

            bool useEncounter1 = ElementsAwoken.encounter == 1;
            player.ManageSpecialBiomeVisuals("ElementsAwoken:Encounter1", useEncounter1);
            bool useEncounter2 = ElementsAwoken.encounter == 2;
            player.ManageSpecialBiomeVisuals("ElementsAwoken:Encounter2", useEncounter2);
            bool useEncounter3 = ElementsAwoken.encounter == 3;
            player.ManageSpecialBiomeVisuals("ElementsAwoken:Encounter3", useEncounter3);

            bool useDespair = voidEnergyTimer > 0 || voidWalkerAura > 0;
            player.ManageSpecialBiomeVisuals("ElementsAwoken:Despair", useDespair);

            // plateau
            bool preHMPlateau = zonePlateau && !MyWorld.awakenedPlateau;
            bool HMPlateau = zonePlateau && MyWorld.awakenedPlateau;
            player.ManageSpecialBiomeVisuals("ElementsAwoken:PlateauDistortion", HMPlateau && Main.UseHeatDistortion && MyWorld.plateauWeather != 2);
            player.ManageSpecialBiomeVisuals("ElementsAwoken:EruptionDistortion", HMPlateau && Main.UseHeatDistortion && MyWorld.plateauWeather == 2);
            player.ManageSpecialBiomeVisuals("ElementsAwoken:RiftDistortion", HMPlateau && Main.UseHeatDistortion && MyWorld.plateauWeather == 3);
            player.ManageSpecialBiomeVisuals("ElementsAwoken:PlateauGrey", preHMPlateau);
            player.ManageSpecialBiomeVisuals("ElementsAwoken:plateauLoc.Yellow", HMPlateau && MyWorld.plateauWeather < 2);
            player.ManageSpecialBiomeVisuals("ElementsAwoken:PlateauGreen", HMPlateau && zoneSulphur && (MyWorld.plateauWeather != 1 || MyWorld.plateauWeatherTime < 0));
            player.ManageSpecialBiomeVisuals("ElementsAwoken:PlateauRed", HMPlateau && zoneSulphur && MyWorld.plateauWeather == 2);
            player.ManageSpecialBiomeVisuals("ElementsAwoken:PlateauPurple", HMPlateau && MyWorld.plateauWeather == 3);

            bool useblizzard = MyWorld.hailStormTime > 0 && player.ZoneOverworldHeight && !player.ZoneDesert && !ActiveBoss() && !GetInstance<Config>().lowDust;
            player.ManageSpecialBiomeVisuals("Blizzard", useblizzard, default(Vector2));

            bool useInfWrath = MyWorld.firePrompt > ElementsAwoken.bossPromptDelay && !ActiveBoss() && !GetInstance<Config>().promptsDisabled;
            player.ManageSpecialBiomeVisuals("ElementsAwoken:AshShader", useInfWrath);
            player.ManageSpecialBiomeVisuals("ElementsAwoken:AshBlizzardEffect", useInfWrath && player.position.Y / 16 < Main.worldSurface);

            //bool useInsanity = MyWorld.firePrompt > ElementsAwoken.bossPromptDelay && !ActiveBoss() && !GetInstance<Config>().promptsDisabled;
            Overlays.Scene.Activate("ElementsAwoken:Eyes", player.Center);

            if (useInfWrath)
            {
                SkyManager.Instance.Activate("ElementsAwoken:InfernacesWrath", player.Center);
                if (!GetInstance<Config>().lowDust) Overlays.Scene.Activate("ElementsAwoken:AshParticles", player.Center);
            }
            else
            {
                SkyManager.Instance.Deactivate("ElementsAwoken:InfernacesWrath");
                Overlays.Scene.Deactivate("ElementsAwoken:AshParticles");
            }

            if (MyWorld.starShowerTime > 0)
            {
                if (!SkyManager.Instance["ElementsAwoken:StarShower"].IsActive()) SkyManager.Instance.Activate("ElementsAwoken:StarShower", player.Center);
            }
            else
            {
                SkyManager.Instance.Deactivate("ElementsAwoken:StarShower");
            }
            bool useStarShower = MyWorld.starShowerTime > 0;
            //player.ManageSpecialBiomeVisuals("ElementsAwoken:StarShower", useStarShower, player.Center);


            if (GetInstance<Config>().lowDust) Overlays.Scene.Deactivate("ElementsAwoken:AshParticles");
            NPC aqueous = null;
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.type == mod.NPCType("Aqueous"))
                {
                    aqueous = npc;
                    break;
                }
            }

            bool useAqueous = NPC.AnyNPCs(mod.NPCType("Aqueous"));
            player.ManageSpecialBiomeVisuals("ElementsAwoken:Aqueous", useAqueous);
            if (aqueous != null)
            {
                bool useAqueousSky = aqueous.life <= aqueous.lifeMax * 0.65f;
                if (useAqueousSky)
                {
                    SkyManager.Instance.Activate("ElementsAwoken:AqueousSky", player.Center);
                }
                else
                {
                    SkyManager.Instance.Deactivate("ElementsAwoken:AqueousSky");
                }
            }
            else
            {
                SkyManager.Instance.Deactivate("ElementsAwoken:AqueousSky");
            }

            /*bool useCelestial = NPC.AnyNPCs(mod.NPCType("TheCelestial"));
            player.ManageSpecialBiomeVisuals("ElementsAwoken:TheCelestial", useCelestial);*/
        }

        private bool ActiveBoss()
        {
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].boss)
                {
                    return true;
                }
            }
            return false;
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (target.GetGlobalNPC<NPCsGLOBAL>().impishCurse)
            {
                damage = (int)(damage * 1.75f);
            }
            if (fadedCloth)
            {
                float scale = 1.5f;
                if (Main.hardMode) scale = 1.75f;
                if (NPC.downedPlantBoss) scale = 2f;
                if (NPC.downedMoonlord) scale = 4f;
                damage = (int)(damage * scale);
            }
        }

        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            if (flyingBoots)
            {
                int dye = 0;
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (player.armor[i].GetGlobalItem<Items.EATooltip>().flyingBoots)
                    {
                        dye = player.dye[i].dye;
                        break;
                    }
                }
                drawInfo.wingShader = dye;
            }
        }

        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            MiscEffects.visible = true;
            layers.Add(MiscEffects);

            MechArmsFront.visible = true;
            layers.Add(MechArmsFront);
            MechArmsBack.visible = true;
            int mountBackLayer = layers.FindIndex(layer => layer.Name.Contains("MountBack"));
            layers.Insert(mountBackLayer, MechArmsBack);

            WispForm.visible = true;
            layers.Add(WispForm);

            if (skylineFlying || nyanBoots)
            {
                foreach (PlayerLayer layer in layers)
                {
                    if (layer == PlayerLayer.Wings) layer.visible = false;
                }
            }
            if ((creditsTimer >= 0 && creditsTimer > screenTransDuration / 2) || (plateauCinematic && plateauCinematicCounter > screenTransDuration / 2))
            {
                foreach (PlayerLayer layer in layers)
                {
                    layer.visible = false;
                }
            }
            if (wispForm)
            {
                foreach (PlayerLayer layer in layers)
                {
                    if (!layer.Name.Contains("WispForm")) layer.visible = false;
                }
            }
        }

        public static readonly PlayerLayer MiscEffects = new PlayerLayer("ElementsAwoken", "MiscEffects", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            MyPlayer modPlayer = drawPlayer.GetModPlayer<MyPlayer>();
            if (drawPlayer.active && !drawPlayer.dead)
            {
                if (modPlayer.skylineAlpha > 0) // drawPlayer.wings == mod.GetEquipSlot("SkylineWhirlwind", EquipType.Wings)
                {
                    int dye = 0;
                    int maxAccessoryIndex = 5 + drawPlayer.extraAccessorySlots;
                    for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                    {
                        if (drawPlayer.armor[i].type == ItemType<Items.Elements.Sky.SkylineWhirlwind>())
                        {
                            dye = drawPlayer.dye[i].dye;
                            break;
                        }
                    }

                    Texture2D texture = mod.GetTexture("Extra/SkylineWhirlwind");
                    int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
                    int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f + 89 - Main.screenPosition.Y);
                    Color color = Lighting.GetColor((int)(drawPlayer.Center.X / 16), (int)(drawPlayer.Center.Y / 16)) * modPlayer.skylineAlpha;
                    Rectangle rect = new Rectangle(0, (texture.Height / 4) * modPlayer.skylineFrame, texture.Width, texture.Height / 4);
                    DrawData data = new DrawData(texture, new Vector2(drawX, drawY), rect, color, 0f, new Vector2(texture.Width / 2f, texture.Height / 2f), 1.3f, drawPlayer.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                    data.shader = dye;
                    Main.playerDrawData.Add(data);
                }
                if (modPlayer.greatLensTimer > 0)
                {
                    Texture2D texture = mod.GetTexture("Extra/GreatLens");
                    int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
                    int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y);
                    Color color = Lighting.GetColor((int)(drawPlayer.Center.X / 16), (int)(drawPlayer.Center.Y / 16)) * 0.7f;
                    DrawData data = new DrawData(texture, new Vector2(drawX, drawY), null, color, 0f, new Vector2(texture.Width / 2f, texture.Height / 2f), 1.3f, drawPlayer.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                    data.shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.ReflectiveDye);
                    Main.playerDrawData.Add(data);
                }
                else if (modPlayer.honeyCocooned > 0)
                {
                    Texture2D texture = mod.GetTexture("Extra/HoneyCocoonOverlay");
                    int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
                    int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y);
                    Color color = Lighting.GetColor((int)(drawPlayer.Center.X / 16), (int)(drawPlayer.Center.Y / 16)) * 0.7f;
                    DrawData data = new DrawData(texture, new Vector2(drawX, drawY), null, color, 0f, new Vector2(texture.Width / 2f, texture.Height / 2f), 1.1f, drawPlayer.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                    Main.playerDrawData.Add(data);
                }
                if (modPlayer.acidWebbed)
                {
                    Texture2D texture = mod.GetTexture("Extra/SulfurWebbed");
                    int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
                    int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y) + 16;
                    Color color = Lighting.GetColor((int)(drawPlayer.Center.X / 16), (int)(drawPlayer.Center.Y / 16));
                    DrawData data = new DrawData(texture, new Vector2(drawX, drawY), null, color, 0f, new Vector2(texture.Width / 2f, texture.Height / 2f), 1.1f, drawPlayer.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                    Main.playerDrawData.Add(data);
                }
            }
        });
        public static readonly PlayerLayer WispForm = new PlayerLayer("ElementsAwoken", "WispForm", null, delegate (PlayerDrawInfo drawInfo)
        {

            if (drawInfo.shadow != 0f)
            {
                return;
            }
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            MyPlayer modPlayer = drawPlayer.GetModPlayer<MyPlayer>();
            if (drawPlayer.active && !drawPlayer.dead)
            {
                if (modPlayer.wispForm)
                {
                    Texture2D texture = mod.GetTexture("NPCs/VolcanicPlateau/ForgottenWisp");
                    int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
                    int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y) + 30;
                    Color color = Lighting.GetColor((int)(drawPlayer.Center.X / 16), (int)(drawPlayer.Center.Y / 16));
                    DrawData data = new DrawData(texture, new Vector2(drawX, drawY), null, color, 0f, new Vector2(texture.Width / 2f, texture.Height / 2f), 1f, drawPlayer.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                    Main.playerDrawData.Add(data);
                }

            }
        });
        public static readonly PlayerLayer MechArmsFront = new PlayerLayer("ElementsAwoken", "MechArmsFront", PlayerLayer.MountFront, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            MyPlayer modPlayer = drawPlayer.GetModPlayer<MyPlayer>();
            if (drawPlayer.active && !drawPlayer.dead)
            {
                if (modPlayer.inMech)
                {
                    Color color = Lighting.GetColor((int)(drawPlayer.Center.X / 16), (int)(drawPlayer.Center.Y / 16));
                    if (Main.mouseLeft)
                    {
                        Texture2D texture = mod.GetTexture("Extra/MechArmFrontActive");
                        int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X) - 16 * drawPlayer.direction + Main.rand.Next(-2, 2);
                        int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y) + 36 + Main.rand.Next(-2, 2);

                        Vector2 vector = drawPlayer.RotatedRelativePoint(drawPlayer.MountedCenter, true);
                        Vector2 vector11 = vector;
                        Vector2 value4 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - vector11;
                        Vector2 vector12 = Vector2.Normalize(value4);

                        float rot = (float)Math.Atan2((double)vector12.Y, (double)vector12.X) - (float)(Math.PI / 2);
                        modPlayer.drillEnd = vector + new Vector2(0, 90).RotatedBy(rot);

                        DrawData data = new DrawData(texture, new Vector2(drawX, drawY), null, color, rot, new Vector2(texture.Width / 2f, 8), 1f, drawPlayer.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                        Main.playerDrawData.Add(data);
                    }
                    else
                    {
                        Texture2D texture = mod.GetTexture("Extra/MechArmFrontIdle");
                        int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X) - 16 * drawPlayer.direction + (drawPlayer.direction == 1 ? 14 : 0);
                        int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y) + 36;

                        DrawData data = new DrawData(texture, new Vector2(drawX, drawY), null, color, 0, new Vector2(36, 8), 1f, drawPlayer.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                        Main.playerDrawData.Add(data);
                    }
                }
            }
        });

        public static readonly PlayerLayer MechArmsBack = new PlayerLayer("ElementsAwoken", "MechArmsBack", PlayerLayer.MountBack, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            MyPlayer modPlayer = drawPlayer.GetModPlayer<MyPlayer>();
            if (drawPlayer.active && !drawPlayer.dead)
            {
                if (modPlayer.inMech)
                {
                    Color color = Lighting.GetColor((int)(drawPlayer.Center.X / 16), (int)(drawPlayer.Center.Y / 16));
                    if (Main.mouseRight && !Main.mouseLeft)
                    {
                        Texture2D texture = mod.GetTexture("Extra/MechArmBackActive");
                        int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X) + 12 * drawPlayer.direction;
                        int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y) + 36;

                        Vector2 vector = drawPlayer.RotatedRelativePoint(drawPlayer.MountedCenter, true);
                        Vector2 vector11 = vector;
                        Vector2 value4 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - vector11;
                        Vector2 vector12 = Vector2.Normalize(value4);

                        float rot = (float)Math.Atan2((double)vector12.Y, (double)vector12.X) - (float)(Math.PI / 2);
                        modPlayer.gunEnd = vector + new Vector2(0, 90).RotatedBy(rot);

                        DrawData data = new DrawData(texture, new Vector2(drawX, drawY), null, color, rot, new Vector2(texture.Width / 2f, 8), 1f, drawPlayer.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                        Main.playerDrawData.Add(data);
                    }
                    else
                    {
                        Texture2D texture = mod.GetTexture("Extra/MechArmBackIdle");
                        int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X) + 16 * drawPlayer.direction;
                        int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y) + 36;

                        DrawData data = new DrawData(texture, new Vector2(drawX, drawY), null, color, 0, new Vector2(36, 8), 1f, drawPlayer.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                        Main.playerDrawData.Add(data);
                    }
                }
            }
            // 52, 8
        });

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            PlayerUtils playerUtils = player.GetModPlayer<PlayerUtils>();

            if (ancientDecayWeapon) target.AddBuff(mod.BuffType("AncientDecay"), 360, false);
            if (eaMagmaStone)
            {
                if (Main.rand.Next(4) == 0)
                {
                    target.AddBuff(BuffID.OnFire, 360);
                }
                else if (Main.rand.Next(2) == 0)
                {
                    target.AddBuff(BuffID.OnFire, 240);
                }
                else
                {
                    target.AddBuff(BuffID.OnFire, 120);
                }
            }
            if (venomSample || vilePower)
            {
                target.AddBuff(BuffID.Venom, 120);
                target.AddBuff(BuffID.Poisoned, 120);
            }
            if (dragonmailGreathelm) target.AddBuff(mod.BuffType("Dragonfire"), 300, false);
            if (sufferWithMe) target.AddBuff(mod.BuffType("ChaosBurn"), 300, false);
            if (voidWalkerArmor == 1) target.AddBuff(BuffType<ExtinctionCurse>(), 300, false);
            if (extinctionCurseImbue) target.AddBuff(BuffType<ExtinctionCurse>(), 360, false);
            if (starstruckImbue) target.AddBuff(BuffType<Starstruck>(), 360, false);
            if (frozenGauntlet)
            {
                target.AddBuff(BuffID.Chilled, 300);
                target.AddBuff(BuffID.Frostburn, 300);
            }
            if (replenishRing)
            {
                if (target.life <= 0)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        int randLife = Main.rand.Next(1, 4);
                        player.statLife += randLife;
                        player.HealEffect(randLife);
                    }
                }
            }
            if (neovirtuoBonus && Main.rand.Next(9) == 0)
            {
                if (neovirtuoTimer <= 0)
                {
                    Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 84);
                    int neoDamage = 200;
                    int speed = 8;
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, speed, speed, mod.ProjectileType("NeovirtuoHoming"), neoDamage, 1.25f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, speed, -speed, mod.ProjectileType("NeovirtuoHoming"), neoDamage, 1.25f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, -speed, speed, mod.ProjectileType("NeovirtuoHoming"), neoDamage, 1.25f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, -speed, -speed, mod.ProjectileType("NeovirtuoHoming"), neoDamage, 1.25f, Main.myPlayer, 0f, 0f);
                    neovirtuoTimer = 15;
                }
            }
            if (immortalResolve)
            {
                if (crit && immortalResolveCooldown <= 0)
                {
                    int randLife = 0;
                    if (player.magicCrit < 10 && player.meleeCrit < 10 && player.rangedCrit < 10 && player.thrownCrit < 10)
                    {
                        randLife = Main.rand.Next(1, 18);
                    }
                    if (player.magicCrit >= 10 && player.meleeCrit >= 10 && player.rangedCrit >= 10 && player.thrownCrit >= 10 && player.magicCrit < 25 && player.meleeCrit < 25 && player.rangedCrit < 25 && player.thrownCrit < 25)
                    {
                        randLife = Main.rand.Next(1, 15);
                    }
                    if (player.magicCrit >= 25 && player.meleeCrit >= 25 && player.rangedCrit >= 25 && player.thrownCrit >= 25 && player.magicCrit < 75 && player.meleeCrit < 75 && player.rangedCrit < 75 && player.thrownCrit < 75)
                    {
                        randLife = Main.rand.Next(1, 10);
                    }
                    if (player.magicCrit >= 50 && player.meleeCrit >= 50 && player.rangedCrit >= 50 && player.thrownCrit >= 50)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            randLife = Main.rand.Next(1, 10);
                        }
                    }
                    player.statLife += randLife;
                    player.HealEffect(randLife);
                    immortalResolveCooldown = 10;
                }
            }
            if (crowsArmor && crowsArmorCooldown <= 0)
            {
                float lightningSpeed = 8f;
                Vector2 spawnpoint = new Vector2(target.Center.X, target.Center.Y - 100);
                float rotation = -(float)Math.Atan2(spawnpoint.X - target.Center.Y, spawnpoint.X - target.Center.X);
                Vector2 speed = new Vector2((float)((Math.Cos(rotation) * lightningSpeed) * -1), (float)((Math.Sin(rotation) * lightningSpeed) * -1));

                Vector2 vector94 = new Vector2(speed.X, speed.Y);
                float ai = (float)Main.rand.Next(100);
                Vector2 vector95 = Vector2.Normalize(vector94) * 2f;
                Projectile.NewProjectile(spawnpoint.X, spawnpoint.Y, vector95.X, vector95.Y, mod.ProjectileType("CrowLightning"), 100, 0f, Main.myPlayer, vector94.ToRotation(), ai);
                Projectile.NewProjectile(spawnpoint.X, spawnpoint.Y, 0f, 0f, mod.ProjectileType("CrowStorm"), 0, 0f, Main.myPlayer);
                crowsArmorCooldown = 30;
            }
            if (cosmicGlass && crit && cosmicGlassCD <= 0)
            {
                if (target.active && !target.friendly && target.damage > 0 && !target.dontTakeDamage)
                {
                    float Speed = 9f;
                    float rotation = (float)Math.Atan2(player.Center.Y - target.Center.Y, player.Center.X - target.Center.X);

                    Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                    Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 12);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, speed.X, speed.Y, mod.ProjectileType("ChargeRifleHalf"), 30, 3f, player.whoAmI, 0f);
                    cosmicGlassCD = 3;
                }
            }
            int strikeChance = 10;
            if (NPC.downedBoss3) strikeChance = 7;
            if (Main.hardMode) strikeChance = 5;
            if (NPC.downedPlantBoss) strikeChance = 4;
            if (NPC.downedMoonlord) strikeChance = 2;
            if (strangeUkulele && Main.rand.Next(strikeChance) == 0)
            {
                List<int> availableNPCs = new List<int>();
                for (int k = 0; k < Main.npc.Length; k++)
                {
                    NPC other = Main.npc[k];
                    if (other.active && !other.friendly && other.damage > 0 && !other.dontTakeDamage && Vector2.Distance(other.Center, player.Center) < 300)
                    {
                        availableNPCs.Add(other.whoAmI);
                    }
                }
                if (availableNPCs.Count > 0)
                {
                    NPC arcTarget = Main.npc[availableNPCs[Main.rand.Next(availableNPCs.Count)]];
                    if (arcTarget.active && !arcTarget.friendly && arcTarget.damage > 0 && !arcTarget.dontTakeDamage)
                    {
                        Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ElectricArcing"));

                        float Speed = 9f;
                        float rotation = (float)Math.Atan2(player.Center.Y - target.Center.Y, player.Center.X - target.Center.X);
                        rotation += MathHelper.ToRadians(Main.rand.Next(-60, 60));
                        Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, speed.X, speed.Y, mod.ProjectileType("UkuleleArc"), (int)(item.damage * 0.5f), 3f, player.whoAmI, arcTarget.whoAmI);
                    }
                }
            }
            if (noDamageCounter > 0) noDamageCounter = 0;
            if (bleedingHeart)
            {
                if (target.life <= 0 && playerUtils.enemiesKilledLast10Secs >= 4 && !target.SpawnedFromStatue)
                {
                    player.AddBuff(BuffType<Bloodbath>(), 600, false);
                }
            }
        }

        public override void MeleeEffects(Item item, Rectangle hitbox)
        {
            if (ancientDecayWeapon && item.melee && !item.noMelee && !item.noUseGraphic)
            {
                if (Main.rand.Next(4) == 0)
                {
                    int num280 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, mod.DustType("AncientDust"), player.velocity.X * 0.2f + (float)(player.direction * 3), player.velocity.Y * 0.2f, 100, default(Color), 0.75f);
                    Main.dust[num280].scale = 1f;
                    Main.dust[num280].noGravity = true;
                }
            }
            if (extinctionCurseImbue && item.melee && !item.noMelee && !item.noUseGraphic)
            {
                if (Main.rand.Next(3) == 0)
                {
                    int num1 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, DustID.PinkFlame, player.velocity.X * 0.2f + (float)(player.direction * 3), player.velocity.Y * 0.2f, 100, default(Color), 0.75f);
                    Main.dust[num1].scale = 2f;
                    Main.dust[num1].noGravity = true;
                }
            }
            if (frozenGauntlet && item.melee && !item.noMelee && !item.noUseGraphic)
            {
                if (Main.rand.Next(3) == 0)
                {
                    int num1 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 135, player.velocity.X * 0.2f + (float)(player.direction * 3), player.velocity.Y * 0.2f, 100, default(Color), 0.75f);
                    Main.dust[num1].scale = 2f;
                    Main.dust[num1].noGravity = true;
                }
            }
            if (eaMagmaStone && item.melee && !item.noMelee && !item.noUseGraphic)
            {
                bool makeDust = Main.rand.Next(3) == 0;
                if (GetInstance<Config>().lowDust) makeDust = makeDust = Main.rand.Next(8) == 0;
                if (makeDust)
                {
                    int num311 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 6, player.velocity.X * 0.2f + (float)(player.direction * 3), player.velocity.Y * 0.2f, 100, default(Color), 2.5f);
                    Main.dust[num311].noGravity = true;
                    Dust expr_F239_cp_0_cp_0 = Main.dust[num311];
                    expr_F239_cp_0_cp_0.velocity.X = expr_F239_cp_0_cp_0.velocity.X * 2f;
                    Dust expr_F258_cp_0_cp_0 = Main.dust[num311];
                    expr_F258_cp_0_cp_0.velocity.Y = expr_F258_cp_0_cp_0.velocity.Y * 2f;
                }
            }
        }

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (greatLensTimer > 0)
            {
                player.ApplyDamageToNPC(npc, (int)(damage * 0.2f), 2f, Math.Sign(player.Center.X - npc.Center.X), false);
                damage = (int)(damage * 0.8f);
            }
            if (honeyCocooned > 0)
            {
                npc.velocity.X = Math.Sign(npc.Center.X - player.Center.X) * 4 * npc.knockBackResist;
                npc.velocity.Y = Math.Sign(npc.Center.Y - (player.Center.Y + 16)) * 4 * npc.knockBackResist;
            }
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            if (greatLensTimer > 0)
            {
                damage = (int)(damage * 0.8f);
            }
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (wispForm && !player.immune)
            {
                playSound = false;
                Main.PlaySound(SoundID.NPCHit5, player.position);
                for (int i = 0; i < 16; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, wispDust)];
                    dust.noGravity = true;
                    dust.velocity.X = (hitDirection * 12) * Main.rand.NextFloat(0.8f, 1.2f);
                    dust.velocity.Y = -6f * Main.rand.NextFloat(0.8f, 1.2f);
                }
            }
            damage = (int)(damage * damageTaken);

            if (honeyCocooned > 0)
            {
                if (!player.immune)
                {
                    honeyCocoonDamage += damage;
                    CombatText.NewText(player.getRect(), Color.Orange, honeyCocoonDamage);
                    if (honeyCocoonDamage >= 100)
                    {
                        honeyCocooned = 0;
                        Main.PlaySound(SoundID.NPCDeath1, player.position);

                        Vector2 pos = player.Center;
                        int numDusts = 36;
                        for (int i = 0; i < numDusts; i++)
                        {
                            Vector2 position = Vector2.One.RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + pos;
                            Vector2 velocity = position - pos;
                            Vector2 spawnPos = position + velocity;
                            Dust dust = Main.dust[Dust.NewDust(spawnPos, 0, 0, 153, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1f)];
                            dust.noGravity = true;
                            dust.noLight = true;
                            dust.velocity = Vector2.Normalize(velocity) * 6f * Main.rand.NextFloat(0.8f, 1.2f);
                        }
                    }
                    Main.PlaySound(3, (int)player.position.X, (int)player.position.Y, 1, 1, -0.2f);
                }
                player.immuneTime = 30;
                player.immune = true;
                return false;
            }

            if (zonePlateau && MyWorld.awakenedPlateau && damageSource.SourceOtherIndex == 2)
            {
                player.AddBuff(BuffType<Incineration>(), 420);
                damage = (int)(damage * 1.5f);
            }
            if (scourgeDrive)
            {
                if (scourgeSpeed)
                {
                    explosionEffect(player, mod.ProjectileType("Explosion"), 100, 0f, DustID.PinkFlame);
                }
            }
            if (lightningCloud && lightningCloudCharge > 60)
            {
                if (damage > 0 && damageSource.SourceOtherIndex != 0)
                {
                    Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 93);

                    int lightningDamage = 10;
                    float speed = 6;
                    if (Main.hardMode)
                    {
                        lightningDamage = 40;
                        speed = 12;
                    }
                    if (NPC.downedMoonlord)
                    {
                        lightningDamage = 100;
                        speed = 18;
                    }
                    float lightningMultiplier = lightningCloudCharge / 300f;

                    speed = speed * lightningMultiplier;
                    lightningDamage = (int)(lightningDamage * lightningMultiplier);
                    if (player.whoAmI == Main.myPlayer)
                    {
                        float numberProjectiles = 8;
                        float rotation = MathHelper.ToRadians(360);
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(speed, speed).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 2f;
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("LightningExplosion"), lightningDamage, 2f, 0);
                        }
                    }

                    lightningCloudCharge = 0;
                }
            }
            if (mechArmor && mechArmorCD <= 0)
            {
                int numLightning = 0;
                for (int k = 0; k < Main.maxNPCs; k++)
                {
                    NPC other = Main.npc[k];
                    if (other.CanBeChasedBy(this) && Vector2.Distance(other.Center, player.Center) < 500)
                    {
                        float Speed = 6f;
                        float rotation = (float)Math.Atan2(player.Center.Y - other.Center.Y, player.Center.X - other.Center.X);

                        Vector2 projVel = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, projVel.X, projVel.Y, ProjectileType<MechLightning>(), 200, 0f, Main.myPlayer, 0f, 0f);
                        numLightning++;
                        if (numLightning > 3) break;
                    }
                }
                mechArmorCD = 20;
            }
            if (player.FindBuffIndex(mod.BuffType("ChaosShield")) != -1 && !player.dead)
            {
                chaosBoost += damage;
                damage /= 5;
            }
            if (puffFall)
            {
                if (damageSource.SourceOtherIndex == 0)
                {
                    damage /= 2;
                }
            }
            if (arid && damageSource.SourceOtherIndex == 0)
            {
                damage = (int)(damage * (MathHelper.Clamp(aridFalling, 0, player.maxFallSpeed) / player.maxFallSpeed));
            }
            if (gelticConqueror)
            {
                if (damageSource.SourceOtherIndex == 0 && !player.controlDown)
                {
                    damage = (int)(damage * 0.25);
                }
            }
            if (oceanicArmor)
            {
                if (damage > 25)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        float speed = Main.rand.Next(-6, 6);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, speed, speed, mod.ProjectileType("PoisonWater"), 60, 1.25f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
            if (viridiumDash)
            {
                explosionEffect(player, mod.ProjectileType("Explosion"), 1000, 10f, 21);
                return false;
            }
            if (spikeBoots && damageSource.SourceOtherIndex == 3 && damage <= 46) return false;
            if (templeSpikeBoots && damageSource.SourceOtherIndex == 3) return false;
            if (aegisDashTimer > 0)
            {
                damage = (int)(damage * 0.1f);
            }
            if (voidEnergyTimer > 0)
            {
                damage = (int)(damage * 0.5f);
            }
            if (flingToShackle)
            {
                return false;
            }
            if (icyHeart)
            {
                damage = (int)(damage * icyHeartDR);
                icyHeartTimer = -600;
                if (damage == 0) return false;
            }
            if (starstruck)
            {
                damage = (int)(damage * MathHelper.Lerp(1, 4, (float)starstruckCounter / 20));
            }
            if (hellFury) damage *= 2;
            if (glassHeart)
            {
                playSound = false;
                Main.PlaySound(SoundID.Shatter, player.position);
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " shattered"), 1, 1);
                return true;
            }
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }

        public override bool ConsumeAmmo(Item weapon, Item ammo)
        {
            if (Main.rand.Next(101) < saveAmmo) return false;
            return base.ConsumeAmmo(weapon, ammo);
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (vleviAegis)
            {
                vleviAegisDamage += (int)damage;
            }
            if (abyssalMatter && abyssalRage <= 0)
            {
                if (damage > player.statLifeMax2 * 0.4f)
                {
                    abyssalRage = 600;
                }
            }
            if (toyArmor && toyArmorCooldown <= 0)
            {
                for (int i = 0; i < Main.rand.Next(1, 4); i++)
                {
                    Projectile brick = Main.projectile[Projectile.NewProjectile(player.Center.X, player.Center.Y, Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-3, -1), ProjectileType<LegoBrickFriendly>(), 25, 0, player.whoAmI)];
                }
                toyArmorCooldown = 60;
            }
            if (voidWalkerChest && damage > player.statLifeMax2 / 2)
            {
                voidWalkerRegen = 180;
            }
            if (voidBlood)
            {
                int bloodDamage = 10;
                if (NPC.downedBoss1) bloodDamage = 15;
                if (NPC.downedBoss3) bloodDamage = 20;
                if (Main.hardMode) bloodDamage = 25;
                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3) bloodDamage = 30;
                if (NPC.downedPlantBoss) bloodDamage = 35;
                if (NPC.downedAncientCultist) bloodDamage = 40;
                if (NPC.downedMoonlord) bloodDamage = 60; for (int i = 0; i < 3; i++)
                {
                    Projectile proj = Main.projectile[Projectile.NewProjectile(player.Center.X, player.Center.Y, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), mod.ProjectileType("VoidBlood"), bloodDamage, 0f, Main.myPlayer, 0f, 0f)];
                }
            }
            if (slimeBooster)
            {
                for (int i = 0; i < 7; i++)
                {
                    Vector2 vector2 = new Vector2((float)(i - 2), -4f);
                    vector2.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
                    vector2.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
                    vector2.Normalize();
                    vector2 *= 4f + (float)Main.rand.Next(-50, 51) * 0.01f;
                    Projectile proj = Main.projectile[Projectile.NewProjectile(player.Top.X, player.Top.Y, vector2.X, vector2.Y, ProjectileID.SpikedSlimeSpike, 30, 0f, Main.myPlayer, 0f, 0f)];
                    proj.friendly = true;
                    proj.hostile = false;
                }
            }
            if (awokenWood)
            {
                player.AddBuff(BuffType<AwokenHealing>(), 180);
            }
            if (starstruck && starstruckCounter < 20)
            {
                starstruckCounter++;
                CombatText.NewText(player.getRect(), Color.HotPink, starstruckCounter, true, false);
            }
        }

        public override void ModifyNursePrice(NPC nurse, int health, bool removeDebuffs, ref int price)
        {
            if (voidBlood)
            {
                price *= 3;
            }
        }

        public override bool ModifyNurseHeal(NPC nurse, ref int health, ref bool removeDebuffs, ref string chatText)
        {
            if (voidBlood)
            {
                if (AnyBoss())
                {
                    chatText = "I'm sorry, I cant operate on your bloodtype under such urgent conditions.";
                    return false;
                }
                if (nurse.life < nurse.lifeMax)
                {
                    chatText = "Cant you see I'm a little busy fixing myself up?";
                    return false;
                }
            }
            return base.ModifyNurseHeal(nurse, ref health, ref removeDebuffs, ref chatText);
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            bool debugMode = GetInstance<Config>().debugMode;
            PlayerEnergy energyPlayer = player.GetModPlayer<PlayerEnergy>();
            if (ElementsAwoken.specialAbility.JustPressed)
            {
                if (chaosRing && player.FindBuffIndex(BuffType<ChaosShieldCooldown>()) == -1 && !player.dead)
                {
                    player.AddBuff(BuffType<ChaosShield>(), 900);
                    player.AddBuff(BuffType<ChaosShieldCooldown>(), 3600);
                }
                if (honeyCocoon)
                {
                    if (honeyCocooned <= 0)
                    {
                        if (player.FindBuffIndex(BuffType<HoneyCocoonCD>()) == -1 && !player.dead)
                        {
                            int duration = GetInstance<Config>().debugMode ? 300 : 3600;
                            player.AddBuff(BuffType<HoneyCocoonCD>(), duration);
                            honeyCocooned = 900;
                        }
                    }
                    else if (honeyCocooned > 0) honeyCocooned = 0;
                }
                if (neovirtuoBonus && player.FindBuffIndex(BuffType<NeovirtuoCooldown>()) == -1 && !player.dead)
                {
                    Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ProjectileType<NeovirtuoPortal>(), 0, 0, player.whoAmI, 0f, 0f);
                    Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 113);
                    player.AddBuff(BuffType<NeovirtuoCooldown>(), 1800);
                }
                if (boostDrive != 0 && player.FindBuffIndex(BuffType<BoostDriveCD>()) == -1)
                {
                    bool hasEnergy = false;
                    int dustID = 226;
                    if (boostDrive == 1 && energyPlayer.energy >= 50)
                    {
                        energyPlayer.energy -= 50;
                        hasEnergy = true;
                    }
                    else if (boostDrive == 2 && energyPlayer.energy >= 150)
                    {
                        energyPlayer.energy -= 150;
                        hasEnergy = true;
                        dustID = 205;
                    }
                    if (hasEnergy)
                    {
                        int duration = GetInstance<Config>().debugMode ? 420 : 2700;
                        player.AddBuff(BuffType<BoostDriveCD>(), duration);
                        boostDriveTimer = 300;

                        int numDusts = 30;
                        for (int p = 0; p < numDusts; p++)
                        {
                            Vector2 position = (Vector2.One * new Vector2((float)player.width / 2f, (float)player.height) * 0.3f * 0.5f).RotatedBy((double)((float)(p - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + player.Center;
                            Vector2 velocity = position - player.Center;
                            int dust = Dust.NewDust(position + velocity, 0, 0, dustID, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].velocity = Vector2.Normalize(velocity) * 2f;
                        }
                    }
                    else CombatText.NewText(player.getRect(), Color.Red, "Insufficient Energy", true, false);
                }
                if (toySlimeClaw && toySlimeClawCD <= 0)
                {
                    Main.PlaySound(SoundID.Item95, player.Center);

                    float speed = 5f;
                    float rotation = (float)Math.Atan2(player.Center.Y - Main.MouseWorld.Y, player.Center.X - Main.MouseWorld.X);
                    Vector2 projVel = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1));
                    ElementsAwoken.DebugModeText(rotation);
                    if (rotation < -(Math.PI / 2 - Math.PI / 8) && rotation > -(Math.PI / 2 + Math.PI / 8))
                    {
                        float jumpSpd = 16f;
                        if (player.slowFall) jumpSpd *= 0.5f;
                        if (player.HeldItem.type == 946) jumpSpd *= 0.5f;
                        player.velocity.Y -= jumpSpd;
                        int numberProjectiles = Main.rand.Next(3, 7);
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = projVel.RotatedByRandom(MathHelper.ToRadians(35));
                            Projectile.NewProjectile(player.Center, perturbedSpeed, ProjectileType<SlimeClawBall>(), 20, 3f, player.whoAmI);
                        }
                    }
                    Projectile.NewProjectile(player.Center, projVel, ProjectileType<SlimeClawBall>(), 20, 3f, player.whoAmI);
                    toySlimeClawCD = 60;
                }
                if (crystallineLocket && player.FindBuffIndex(BuffType<CrystallineLocketCD>()) == -1)
                {
                    crystallineLocketCrit = 600;
                    if (!GetInstance<Config>().debugMode) player.AddBuff(BuffType<CrystallineLocketCD>(), 3600);
                    else player.AddBuff(BuffType<CrystallineLocketCD>(), 60);
                }
                if (greatLens && player.FindBuffIndex(BuffType<GreatLensCD>()) == -1)
                {
                    greatLensTimer = 600;
                    if (!GetInstance<Config>().debugMode) player.AddBuff(BuffType<GreatLensCD>(), 3600);
                    else player.AddBuff(BuffType<GreatLensCD>(), 60);
                }
                if (flare && !player.dead)
                {
                    if (flareShieldCD <= 0)
                    {
                        player.AddBuff(BuffType<Buffs.Other.FlareShield>(), 900);
                        flareShieldCD = 3600;
                    }
                    else Main.NewText(flareShieldCD / 60 + " seconds left until you can use Flare");
                }
                if (voidEnergyCharge > 600)
                {
                    voidEnergyTimer = voidEnergyCharge / 4;
                    voidEnergyCharge = 0;
                    Main.PlaySound(29, (int)player.position.X, (int)player.position.Y, 96);
                }
            }
            if (ElementsAwoken.dash2.JustPressed)
            {
                if (vleviAegis && aegisDashCooldown <= 0)
                {
                    aegisDashTimer = 45;
                    aegisDashCooldown = 300;
                    aegisDashDir = player.direction;

                    // make dust in an expanding circle
                    int numDusts = 56;
                    for (int i = 0; i < numDusts; i++)
                    {
                        Vector2 position = (Vector2.Normalize(player.velocity) * new Vector2((float)player.width / 2f, (float)player.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + player.Center;
                        Vector2 velocity = position - player.Center;
                        int dust = Dust.NewDust(position + velocity, 0, 0, DustID.PinkFlame, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].noLight = true;
                        Main.dust[dust].velocity = Vector2.Normalize(velocity) * 9f;
                    }
                }
            }
            if (player.controlDown && player.releaseDown) // double tap down
            {
                if (forgedArmor) forgedShackled = 600;
                if (awokenWood) Projectile.NewProjectile(player.Center, Vector2.Zero, ProjectileType<LifesAura>(), 0, 0, player.whoAmI);
                if (voidWalkerCooldown <= 0 && voidWalkerArmor > 0) voidWalkerAura = 300;
                if (forgedShackled > 0) flingToShackle = true;
                if (fireAccCD < 0 && fireAcc)
                {
                    var mod = ModLoader.GetMod("ElementsAwoken");
                    Projectile exp = Main.projectile[Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ProjectileType<Explosion>(), 40, 12, player.whoAmI, 0f, 0f)];
                    exp.width = 100;
                    exp.height = 20;
                    exp.Center = player.Bottom + new Vector2(0, -10);
                    Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 14);
                    int num = GetInstance<Config>().lowDust ? 10 : 20;
                    for (int i = 0; i < num; i++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 31, 0f, 0f, 100, default(Color), 1.5f)];
                        if (dust.position.X < player.Center.X) dust.velocity.X = Main.rand.NextFloat(0.8f, 1.2f) * -3f;
                        else dust.velocity.X = Main.rand.NextFloat(0.8f, 1.2f) * 3f;
                        dust.velocity.Y = -2f;
                    }
                    int num2 = GetInstance<Config>().lowDust ? 5 : 10;
                    for (int i = 0; i < num2; i++)
                    {
                        int dustID = 6;
                        Dust dust = Main.dust[Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, dustID, 0f, 0f, 100, default(Color), 2.5f)];
                        dust.noGravity = true;
                        dust.velocity *= 5f;
                        if (dust.position.X < player.Center.X) dust.velocity.X = Main.rand.NextFloat(0.8f, 1.2f) * -3f;
                        else dust.velocity.X = Main.rand.NextFloat(0.8f, 1.2f) * 3f;
                        dust.velocity.Y = -2f; int dustID2 = 6;
                        dust = Main.dust[Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, dustID2, 0f, 0f, 100, default(Color), 1.5f)];
                        dust.velocity *= 3f;
                        if (dust.position.X < player.Center.X) dust.velocity.X = Main.rand.NextFloat(0.8f, 1.2f) * -3f;
                        else dust.velocity.X = Main.rand.NextFloat(0.8f, 1.2f) * 3f;
                        dust.velocity.Y = -2f;
                    }
                    int num373 = Gore.NewGore(new Vector2(player.position.X, player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[num373].velocity *= 0.4f;
                    Gore gore85 = Main.gore[num373];
                    gore85.velocity.X = gore85.velocity.X + 1f;
                    Gore gore86 = Main.gore[num373];
                    gore86.velocity.Y = gore86.velocity.Y + 1f;
                    num373 = Gore.NewGore(new Vector2(player.position.X, player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[num373].velocity *= 0.4f;
                    Gore gore87 = Main.gore[num373];
                    gore87.velocity.X = gore87.velocity.X - 1f;
                    Gore gore88 = Main.gore[num373];
                    gore88.velocity.Y = gore88.velocity.Y + 1f;
                    num373 = Gore.NewGore(new Vector2(player.position.X, player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[num373].velocity *= 0.4f;
                    Gore gore89 = Main.gore[num373];
                    gore89.velocity.X = gore89.velocity.X + 1f;
                    Gore gore90 = Main.gore[num373];
                    gore90.velocity.Y = gore90.velocity.Y - 1f;
                    num373 = Gore.NewGore(new Vector2(player.position.X, player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[num373].velocity *= 0.4f;
                    Gore gore91 = Main.gore[num373];
                    gore91.velocity.X = gore91.velocity.X - 1f;
                    Gore gore92 = Main.gore[num373];
                    gore92.velocity.Y = gore92.velocity.Y - 1f;

                    player.velocity.Y -= 8;
                    fireAccCD = 60;
                }
                if (radiantCrown)
                {
                    Vector2 toPos = Main.MouseWorld;
                    //Tile tileTest = Framing.GetTileSafely((int)(toPos.X / 16), (int)(toPos.Y / 16));
                    if (!Collision.SolidCollision(toPos, player.width, player.height) && toPos.X > 50f && toPos.X < (float)(Main.maxTilesX * 16 - 50) && toPos.Y > 50f && toPos.Y < (float)(Main.maxTilesY * 16 - 50))
                    {
                        if (player.chaosState)
                        {
                            player.statLife -= player.statLifeMax2 / 7;
                            if (player.statLife < 0) player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " became one with the stars"), 1, 1);
                        }
                        int numProj = 5;
                        Vector2 distance = (toPos - player.Center) / numProj;
                        for (int k = 0; k < numProj; k++)
                        {
                            Projectile proj = Main.projectile[Projectile.NewProjectile(player.Center + distance - new Vector2(0, 23), Vector2.Zero, ProjectileType<RadiantPTeleport>(), 0, 0f, Main.myPlayer)];
                            proj.spriteDirection = player.direction;
                            distance += (toPos - player.Center) / numProj;
                        }
                        float num2 = Vector2.Distance(player.position, toPos);
                        player.Center = toPos;
                        NetMessage.SendData(65, -1, -1, null, 0, (float)player.whoAmI, toPos.X, toPos.Y, 1, 0, 0);

                        // screen stuff
                        player.fallStart = (int)(player.position.Y / 16f);
                        if (player.whoAmI == Main.myPlayer)
                        {
                            if (num2 < new Vector2((float)Main.screenWidth, (float)Main.screenHeight).Length() / 2f + 100f)
                            {
                                int time = 10;
                                Main.SetCameraLerp(0.1f, time);
                            }
                            else
                            {
                                Main.BlackFadeIn = 255;
                                Lighting.BlackOut();
                                Main.screenLastPosition = Main.screenPosition;
                                Main.screenPosition.X = player.position.X + (float)(player.width / 2) - (float)(Main.screenWidth / 2);
                                Main.screenPosition.Y = player.position.Y + (float)(player.height / 2) - (float)(Main.screenHeight / 2);
                                Main.quickBG = 10;
                            }
                            if (Main.mapTime < 5)
                            {
                                Main.mapTime = 5;
                            }
                            Main.maxQ = true;
                            Main.renderNow = true;
                        }

                        player.AddBuff(BuffID.ChaosState, 300);
                    }
                }
            }
            if (ElementsAwoken.wispA.Current && abilityTimer >= 0 && (canWispA || debugMode))
            {
                abilityTimer++;
                if (abilityTimer >= 180)
                {
                    if (forceWisp) player.QuickMount();
                    if (forceWisp) player.width = 20;
                    forceWisp = !forceWisp;
                    int numDusts = 56;
                    for (int i = 0; i < numDusts; i++)
                    {
                        Vector2 position = Vector2.One.RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + player.Center;
                        Vector2 velocity = position - player.Center;
                        Dust dust = Main.dust[Dust.NewDust(position + velocity, 0, 0, wispDust, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                        dust.velocity = Vector2.Normalize(velocity) * 9f;
                    }
                    abilityTimer = -60;
                    for (int p = 0; p < Main.maxProjectiles; p++)
                    {
                        if (Main.projectile[p].active && Main.projectile[p].owner == player.whoAmI && Main.projectile[p].aiStyle == 7)
                        {
                            Main.projectile[p].Kill();
                        }
                    }
                }
                else
                {
                    Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, wispDust, 0f, 0f, 200, default(Color), 0.5f)];
                    dust.noGravity = true;
                    dust.fadeIn = 1.3f;
                    Vector2 vector = Main.rand.NextVector2Square(-1, 1f);
                    vector.Normalize();
                    vector *= 3f;
                    dust.velocity = vector;
                    dust.position = player.Center - vector * 15;
                }
            }
            if (ElementsAwoken.timeA.Current && abilityTimer >= 0 && (canTimeA || debugMode))
            {
                timeAbilityTimer++;
                int amount = 30;
                if (timeAbilityTimer > 180) amount = 120;
                else if (timeAbilityTimer > 60) amount = 60;
                Main.time += amount;
                if (player.ownedProjectileCounts[ProjectileType<TimeCircle>()] == 0)
                {
                    Projectile proj = Main.projectile[Projectile.NewProjectile(player.Center, Vector2.Zero, ProjectileType<TimeCircle>(), 0, 0f, player.whoAmI, player.whoAmI)];
                }
            }
            else timeAbilityTimer = 0;
            if (ElementsAwoken.sandstormA.Current && abilityTimer >= 0 && (canSandstormA || debugMode))
            {
                player.velocity.X *= 0.9f;

                abilityTimer++;
                float length = 90;
                Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, 32, 0f, 0f, 0, default(Color), 0.8f)];
                dust.noGravity = true;
                dust.velocity.Y = Main.rand.NextFloat(-3f, -0.5f);
                for (int j = 0; j < 2; j++)
                {
                    int dustLength = GetInstance<Config>().lowDust ? 1 : 3;
                    float reduce = (1 - ((float)abilityTimer / length)) * 30;
                    for (int i = 0; i < dustLength; i++)
                    {
                        float X = ((float)Math.Sin((float)abilityTimer / 10f) * reduce) * (j % 2 == 0 ? 1 : -1) + (j % 2 == 0 ? 1 : -1) * 10;
                        float Y = -(abilityTimer / length) * player.height;
                        Vector2 dustPos = new Vector2(X, Y);

                        Dust dust2 = Main.dust[Dust.NewDust(player.Bottom + dustPos - Vector2.One * 4f, 8, 8, 75)];
                        dust2.velocity = Vector2.Zero;
                        dust2.position -= player.velocity / dustLength * (float)i;
                        dust2.noGravity = true;
                    }
                }
                if (abilityTimer > length)
                {
                    abilityTimer = -60;
                    for (int i = 0; i < 10; i++)
                    {
                        Dust dust3 = Main.dust[Dust.NewDust(player.Top - new Vector2(4, 0), 8, 2, 75, 0f, 0f, 0, default(Color), 1.5f)];
                        dust3.noGravity = true;
                        dust3.velocity.Y = Main.rand.NextFloat(-6f, -3f);
                    }
                    int numDusts = 56;
                    for (int i = 0; i < numDusts; i++)
                    {
                        Vector2 position = Vector2.One.RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + player.Center;
                        Vector2 velocity = position - player.Center;
                        Dust dust4 = Main.dust[Dust.NewDust(position + velocity, 0, 0, 32, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f)];
                        dust4.noGravity = true;
                        dust4.noLight = true;
                        dust4.velocity = Vector2.Normalize(velocity) * 9f;
                    }
                    if (Sandstorm.Happening)
                    {
                        Main.NewText("The sandstorm settles...", 227, 200, 93, false);
                        Sandstorm.Happening = false;
                        Sandstorm.TimeLeft = 0;
                        SandstormStuff();
                    }
                    else if (!Sandstorm.Happening)
                    {
                        Main.NewText("The desert winds pick up!", 227, 200, 93, false);
                        Sandstorm.Happening = true;
                        Sandstorm.TimeLeft = (int)(3600.0 * (8.0 + (double)Main.rand.NextFloat() * 16.0));
                        SandstormStuff();
                    }
                }
            }
            if (ElementsAwoken.rainA.Current && abilityTimer >= 0 && (canRainA || debugMode))
            {
                if ((!MyWorld.radiantRain || MyWorld.completedRadiantRain))
                {
                    if (player.ownedProjectileCounts[ProjectileType<RainAbility>()] == 0)
                    {
                        Projectile.NewProjectile(player.Center, Vector2.Zero, ProjectileType<RainAbility>(), 0, 0f, player.whoAmI, player.whoAmI);
                    }
                    abilityTimer++;
                    if (abilityTimer >= 120)
                    {
                        SwitchRain(player);
                        abilityTimer = -60;
                    }
                }
                else
                {
                    Main.NewText("You dont yet have the power to dissipate the radiance...", Color.HotPink);
                    abilityTimer = -120;
                }
            }

            if (abilityTimer > 0 && !ElementsAwoken.sandstormA.Current && !ElementsAwoken.rainA.Current && !ElementsAwoken.wispA.Current) abilityTimer = 0;

            if (ElementsAwoken.questLog.JustPressed)
            {
                if (UI.QuestListUI.Visible)
                {
                    Main.PlaySound(SoundID.MenuClose);
                    UI.QuestListUI.Visible = false;
                }
                else
                {
                    Main.PlaySound(SoundID.MenuOpen);

                    //int uiWidth = 200;
                    int uiHeight = 212;
                    int infoWidth = 250;
                    int posX = infoWidth + 30;
                    int posY = Main.screenHeight / 2 - uiHeight;

                    UI.QuestListUI.Visible = true;
                    UI.QuestListUI.MainDisplay.Left.Set(posX, 0f);
                    //UI.QuestListUI.InfoPanel.Left.Set(posX - infoWidth - 10, 0f);
                    //UI.QuestListUI.InfoPanel.Top.Set(posY, 0f);
                    //UI.QuestListUI.BackPanel.Left.Set(posX, 0f);
                    //UI.QuestListUI.BackPanel.Top.Set(posY, 0f);
                }
            }
        }

        public override void ModifyScreenPosition()
        {
            // credits
            if (creditsTimer >= 0 || plateauCinematic)
            {
                if (!Main.gameMenu)
                {
                    if (creditsTimer > screenTransDuration / 2 || plateauCinematicCounter > screenTransDuration / 2) // so the screen doesnt go to the top corner before the transition happens
                    {
                        Main.screenPosition = desiredScPos - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2); // t he player gets stuck on blocks so this makes it smooth
                    }
                }
            }
            // screenshake
            if (!GetInstance<Config>().screenshakeDisabled)
            {
                if (screenshakeAmount >= 0)
                {
                    screenshakeTimer++;
                    if (screenshakeTimer >= 5) screenshakeAmount -= 0.1f;
                }
                if (screenshakeAmount < 0)
                {
                    screenshakeAmount = 0;
                    screenshakeTimer = 0;
                }
                Main.screenPosition += new Vector2(screenshakeAmount * Main.rand.NextFloat(), screenshakeAmount * Main.rand.NextFloat());
            }
            // obsidious intro
            int obsidious = NPC.FindFirstNPC(NPCType<NPCs.Bosses.Obsidious.Obsidious>());
            if (obsidious >= 0)
            {
                NPC obby = Main.npc[obsidious];
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    if (obby.ai[1] == 2 && obby.ai[3] < 180)
                    {
                        Vector2 desiredPos = obby.Center - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                        Vector2 desiredEndPos = player.Center - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                        if (obby.ai[3] == 0)
                        {
                            startScreen = Main.screenPosition;
                        }
                        if (obby.ai[3] < 45)
                        {
                            Main.screenPosition = Vector2.Lerp(startScreen, desiredPos, obby.ai[3] / 45f);
                        }
                        else if (obby.ai[3] < 135)
                        {
                            Main.screenPosition = desiredPos;
                        }
                        else
                        {
                            Main.screenPosition = Vector2.Lerp(desiredPos, desiredEndPos, (obby.ai[3] - 135) / 45f);
                        }
                    }
                }
            }
        }
        // stuff
        public static void SandstormStuff()
        {
            Sandstorm.IntendedSeverity = !Sandstorm.Happening ? (Main.rand.Next(3) != 0 ? Main.rand.NextFloat() * 0.3f : 0.0f) : 0.4f + Main.rand.NextFloat();
            if (Main.netMode == 1)
                return;
            //NetMessage.SendData(7, -1, -1, "", 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        }

        public void SetFireResistance(float resistance)
        {
            if (resistance > fireResistance) fireResistance = resistance;
        }

        public static void SwitchRain(Player player)
        {
            if (Main.raining)
            {
                Main.rainTime = 0;
                Main.raining = false;
                Main.maxRaining = 0f;
                CombatText.NewText(player.getRect(), Color.Aqua, "Clear", true, false);
            }
            else if (!Main.raining)
            {
                CombatText.NewText(player.getRect(), Color.Aqua, "Raining", true, false);

                int num = 86400;
                int num2 = num / 24;
                Main.rainTime = Main.rand.Next(num2 * 8, num);
                if (Main.rand.Next(3) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2);
                }
                if (Main.rand.Next(4) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 2);
                }
                if (Main.rand.Next(5) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 2);
                }
                if (Main.rand.Next(6) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 3);
                }
                if (Main.rand.Next(7) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 4);
                }
                if (Main.rand.Next(8) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 5);
                }
                float num3 = 1f;
                if (Main.rand.Next(2) == 0)
                {
                    num3 += 0.05f;
                }
                if (Main.rand.Next(3) == 0)
                {
                    num3 += 0.1f;
                }
                if (Main.rand.Next(4) == 0)
                {
                    num3 += 0.15f;
                }
                if (Main.rand.Next(5) == 0)
                {
                    num3 += 0.2f;
                }
                Main.rainTime = (int)((float)Main.rainTime * num3);
                Main.raining = true;
                Main.maxRaining = Main.rand.NextFloat(0.2f, 0.8f);
                MyWorld.prevTickRaining = true; // to stop radiant rain from happening from this
            }
        }

        public static void explosionEffect(Player player, int type, int damage, float knockback, int dust)
        {
            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, type, damage, knockback, Main.myPlayer, 0f, 0f);
            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 14);
            for (int num369 = 0; num369 < 20; num369++)
            {
                int num370 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, dust, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num370].velocity *= 1.4f;
            }
            for (int num371 = 0; num371 < 10; num371++)
            {
                int num372 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, dust, 0f, 0f, 100, default(Color), 2.5f);
                Main.dust[num372].noGravity = true;
                Main.dust[num372].velocity *= 5f;
                num372 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, dust, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num372].velocity *= 3f;
            }
            int num373 = Gore.NewGore(new Vector2(player.position.X, player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore85 = Main.gore[num373];
            gore85.velocity.X = gore85.velocity.X + 1f;
            Gore gore86 = Main.gore[num373];
            gore86.velocity.Y = gore86.velocity.Y + 1f;
            num373 = Gore.NewGore(new Vector2(player.position.X, player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore87 = Main.gore[num373];
            gore87.velocity.X = gore87.velocity.X - 1f;
            Gore gore88 = Main.gore[num373];
            gore88.velocity.Y = gore88.velocity.Y + 1f;
            num373 = Gore.NewGore(new Vector2(player.position.X, player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore89 = Main.gore[num373];
            gore89.velocity.X = gore89.velocity.X + 1f;
            Gore gore90 = Main.gore[num373];
            gore90.velocity.Y = gore90.velocity.Y - 1f;
            num373 = Gore.NewGore(new Vector2(player.position.X, player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore91 = Main.gore[num373];
            gore91.velocity.X = gore91.velocity.X - 1f;
            Gore gore92 = Main.gore[num373];
            gore92.velocity.Y = gore92.velocity.Y - 1f;
        }

        private static void EyeDust(Player player, int dust)
        {
            int num = 0;
            num += player.bodyFrame.Y / 56;
            if (num >= Main.OffsetsPlayerHeadgear.Length)
            {
                num = 0;
            }
            Vector2 vector = new Vector2((float)(3 * player.direction - ((player.direction == 1) ? 1 : 0)), -11.5f * player.gravDir) + Vector2.UnitY * player.gfxOffY + player.Size / 2f + Main.OffsetsPlayerHeadgear[num];
            Vector2 vector2 = new Vector2((float)(3 * player.shadowDirection[1] - ((player.direction == 1) ? 1 : 0)), -11.5f * player.gravDir) + player.Size / 2f + Main.OffsetsPlayerHeadgear[num];
            Vector2 vector3 = Vector2.Zero;
            if (player.mount.Active && player.mount.Cart)
            {
                int num2 = Math.Sign(player.velocity.X);
                if (num2 == 0)
                {
                    num2 = player.direction;
                }
                vector3 = new Vector2(MathHelper.Lerp(0f, -8f, player.fullRotation / 0.7853982f), MathHelper.Lerp(0f, 2f, Math.Abs(player.fullRotation / 0.7853982f))).RotatedBy((double)player.fullRotation, default(Vector2));
                if (num2 == Math.Sign(player.fullRotation))
                {
                    vector3 *= MathHelper.Lerp(1f, 0.6f, Math.Abs(player.fullRotation / 0.7853982f));
                }
            }
            if (player.fullRotation != 0f)
            {
                vector = vector.RotatedBy((double)player.fullRotation, player.fullRotationOrigin);
                vector2 = vector2.RotatedBy((double)player.fullRotation, player.fullRotationOrigin);
            }
            float num3 = 0f;
            if (player.mount.Active)
            {
                num3 = (float)player.mount.PlayerOffset;
            }
            Vector2 vector4 = player.position + vector + vector3;
            Vector2 vector5 = player.oldPosition + vector2 + vector3;
            vector5.Y -= num3 / 2f;
            vector4.Y -= num3 / 2f;

            int num5 = (int)Vector2.Distance(vector4, vector5) / 3 + 1;
            if (Vector2.Distance(vector4, vector5) % 3f != 0f)
            {
                num5++;
            }
            float num4 = 1f;
            for (float num6 = 1f; num6 <= (float)num5; num6 += 1f)
            {
                Dust expr_3D9 = Main.dust[Dust.NewDust(player.Center, 0, 0, dust, 0f, 0f, 0, default(Color), 1f)];
                expr_3D9.position = Vector2.Lerp(vector5, vector4, num6 / (float)num5);
                expr_3D9.noGravity = true;
                expr_3D9.velocity = Vector2.Zero;
                expr_3D9.customData = player;
                expr_3D9.scale = num4;
                expr_3D9.shader = GameShaders.Armor.GetSecondaryShader(player.cYorai, player);
            }
        }

        private int CollideWithNPCs(Rectangle myRect, float Damage, float Knockback, int NPCImmuneTime, int PlayerImmuneTime)
        {
            int num = 0;
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.dontTakeDamage && !nPC.friendly && nPC.immune[player.whoAmI] == 0)
                {
                    Rectangle rect = nPC.getRect();
                    if (myRect.Intersects(rect) && (nPC.noTileCollide || Collision.CanHit(player.position, player.width, player.height, nPC.position, nPC.width, nPC.height)))
                    {
                        int direction = player.direction;
                        if (player.velocity.X < 0f)
                        {
                            direction = -1;
                        }
                        if (player.velocity.X > 0f)
                        {
                            direction = 1;
                        }
                        if (player.whoAmI == Main.myPlayer)
                        {
                            player.ApplyDamageToNPC(nPC, (int)Damage, Knockback, direction, false);
                        }
                        nPC.immune[player.whoAmI] = NPCImmuneTime;
                        player.immune = true;
                        player.immuneNoBlink = true;
                        player.immuneTime = PlayerImmuneTime;
                        num++;
                        break;
                    }
                }
            }
            return num;
        }

        private void EAWallSlide()
        {
            if (toySlimeClaw)
            {
                player.sliding = false;
                toySlimeClawSliding = false;
                if (player.slideDir != 0 && player.spikedBoots == 0 && !player.mount.Active && ((player.controlLeft && player.slideDir == -1) || (player.controlRight && player.slideDir == 1)))
                {
                    bool flag = false;
                    float num = player.position.X;
                    if (player.slideDir == 1)
                    {
                        num += (float)player.width;
                    }
                    num += (float)player.slideDir;
                    float num2 = player.position.Y + (float)player.height + 1f;
                    if (player.gravDir < 0f)
                    {
                        num2 = player.position.Y - 1f;
                    }
                    num /= 16f;
                    num2 /= 16f;
                    if (WorldGen.SolidTile((int)num, (int)num2) && WorldGen.SolidTile((int)num, (int)num2 - 1))
                    {
                        flag = true;
                    }
                    if ((flag && (double)player.velocity.Y > 0.5 && player.gravDir == 1f) || ((double)player.velocity.Y < -0.5 && player.gravDir == -1f))
                    {
                        player.fallStart = (int)(player.position.Y / 16f);
                        if (player.controlDown)
                        {
                            player.velocity.Y = 4f * player.gravDir;
                        }
                        else
                        {
                            player.velocity.Y = 0.5f * player.gravDir;
                        }
                        player.sliding = true;
                        toySlimeClawSliding = true;
                        int num5 = Dust.NewDust(new Vector2(player.position.X + (float)(player.width / 2) + (float)((player.width / 2 - 4) * player.slideDir), player.position.Y + (float)(player.height / 2) + (float)(player.height / 2 - 4) * player.gravDir), 8, 8, 4, 0f, 0f, 150, new Color(0, 220, 40, 100), 1f);
                        if (player.slideDir < 0)
                        {
                            Dust expr_48D_cp_0_cp_0 = Main.dust[num5];
                            expr_48D_cp_0_cp_0.position.X = expr_48D_cp_0_cp_0.position.X - 10f;
                        }
                        if (player.gravDir < 0f)
                        {
                            Dust expr_4B5_cp_0_cp_0 = Main.dust[num5];
                            expr_4B5_cp_0_cp_0.position.Y = expr_4B5_cp_0_cp_0.position.Y - 12f;
                        }
                        Main.dust[num5].velocity *= 0.1f;
                        Main.dust[num5].scale *= 1.2f;
                        Main.dust[num5].noGravity = true;
                        Main.dust[num5].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
                    }
                }
            }
        }

        public void ApplyDamageToNPC(NPC npc, int damage, float knockback, int direction, bool crit)
        {
            npc.StrikeNPC(damage, knockback, direction, crit, false, false);
            if (Main.netMode != 0)
            {
                NetMessage.SendData(28, -1, -1, null, npc.whoAmI, (float)damage, knockback, (float)direction, crit.ToInt(), 0, 0);
            }
            int num = Item.NPCtoBanner(npc.BannerID());
            if (num >= 0)
            {
                player.lastCreatureHit = num;
            }
        }

        private void CantMove()
        {
            player.controlUp = false;
            player.controlLeft = false;
            player.releaseLeft = false;
            player.controlRight = false;
            player.releaseRight = false;
            player.controlDown = false;
            player.controlJump = false;

            eaDashDelay = 0;
            eaDashTime = 0;
        }

        private bool AnyBoss()
        {
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                NPC nPC = Main.npc[i];
                if (nPC.boss && nPC.active)
                {
                    return true;
                }
            }
            return false;
        }
        public struct CreditPoint
        {
            public Vector2 pos;
            public Vector2 scroll;
            public string name;
            public int value;
        }
    }
    public class GravityPlayer : ModPlayer
    {
        public int forceGrav = 1;
        public int gravWalkFrame = 0;
        public bool onPlatform = false;
        public bool onGround = false;
        public bool noWings = false;
        public double gravWalkCounter = 0;
        public int gravWingTime = 1; // ugh hardcode messing up
        public override void Initialize()
        {
            forceGrav = 1;
        }
        public override void PostUpdateBuffs()
        {
            if (forceGrav == -1) player.gravControl = true;

        }
        public override void UpdateDead()
        {
            forceGrav = 1;
        }
        public override void PostUpdateMiscEffects()
        {
            if (forceGrav == 0) forceGrav = 1;
            float rotateSpeed = 0.1f;
            if (forceGrav == -1)
            {
                if (noWings)
                {
                    player.wingTime = 0;
                    player.wingTimeMax = 0;
                    gravWingTime = 0;
                }
                // wing fix
                if (player.controlJump)
                {
                    player.velocity.Y -= 0.01f;
                    if (gravWingTime > 0) gravWingTime--;
                    if (player.wingTime <= 0 && player.velocity.Y < -3.33f)
                    {
                        player.velocity.Y = -3.33f;
                    }
                }

                float target = MathHelper.ToRadians(180);

                player.fullRotation = player.fullRotation.AngleLerp(target, rotateSpeed);
                player.fullRotationOrigin = player.Size / 2;
                player.gravity = -0.4f;
                if (player.velocity.Y < -10) player.velocity.Y = -10;
                Tile tBelow = Framing.GetTileSafely((int)player.Bottom.X / 16, (int)player.Bottom.Y / 16);
                Tile tAbove = Framing.GetTileSafely((int)player.position.X / 16, (int)player.position.Y / 16 - 1);
                if (Math.Abs(player.velocity.Y) <= 0.001f && (((Main.tileSolid[tAbove.type] || Main.tileSolidTop[tAbove.type]) && tAbove.active()) || onPlatform))
                {
                    onGround = true;
                    gravWingTime = player.wingTimeMax;
                    player.wingTime = player.wingTimeMax;
                    noWings = false;
                }
                else onGround = false;
            }
            else
            {
                float target = 0;

                player.fullRotation = player.fullRotation.AngleLerp(target, rotateSpeed);
                player.fullRotationOrigin = player.Size / 2;
            }
            if (forceGrav == -1)
            {
                player.controlUp = false; // to stop gravity switching back
            }
            onPlatform = false;
        }
        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (forceGrav == -1)
            {
                if (player.velocity.X != 0) player.direction = -Math.Sign(player.velocity.X);
                if (onGround)
                {
                    if (player.velocity.X != 0)
                    {
                        gravWalkCounter += (double)(Math.Abs(player.velocity.X) * 0.5f);
                        while (gravWalkCounter > 8.0)
                        {
                            gravWalkCounter -= 8.0;
                            gravWalkFrame = gravWalkFrame + player.bodyFrame.Height;
                        }
                        if (gravWalkFrame < player.bodyFrame.Height * 7)
                        {
                            gravWalkFrame = player.bodyFrame.Height * 19;
                        }
                        if (gravWalkFrame > player.bodyFrame.Height * 19)
                        {
                            gravWalkFrame = player.bodyFrame.Height * 7;
                        }
                        player.bodyFrame.Y = gravWalkFrame;
                        player.legFrame.Y = gravWalkFrame;
                    }
                    else
                    {
                        player.bodyFrame.Y = 0;
                        player.legFrame.Y = 0;
                    }
                    player.wingFrame = 0;

                }
                else
                {
                    if (player.itemAnimation == 0)
                    {
                        player.bodyFrame.Y = player.bodyFrame.Height * 1;
                    }
                    player.legFrame.Y = player.bodyFrame.Height * 5;
                    if (player.controlJump && player.wingTime <= 0)
                    {
                        player.wingFrame = 2;
                    }
                }
            }
        }
    }
}