using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.GameInput;
using System.Linq;
using Terraria.ModLoader.IO;
using ReLogic.Graphics;
using Terraria.Graphics.Effects;
using ElementsAwoken.NPCs;
using Terraria.GameContent.Achievements;
using Microsoft.Xna.Framework.Input;
using ElementsAwoken.Effects;
using ElementsAwoken.Projectiles;
using ElementsAwoken.Buffs;

namespace ElementsAwoken
{
    public class MyPlayer : ModPlayer
    {
        bool calamityEnabled = ModLoader.GetMod("CalamityMod") != null;

        public bool voidBlood = false;
        public int generalTimer = 0;

        public bool talkToAzana = false;

        public int saveAmmo = 0;

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
        public bool fireHarpy = false;
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
        #endregion   
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
        #endregion
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
        #endregion   
        #region buffs
        public bool extinctionCurseImbue = false;
        public bool discordantPotion = false;
        public bool superSpeed = false;
        public bool vilePower = false;
        #endregion
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

        public int noDamageCounter = 0;
        //amulet of despair
        public int voidEnergyCharge = 0;
        public int voidEnergyTimer = 0;
        //infinity guantlet and stones
        public int overInfinityCharged = 0;
        public bool infinityDeath = false;
        #endregion
        #region credits
        public Vector2 desiredScPos = new Vector2();
        public Vector2 playerStartPos = new Vector2();
        public Vector2[] creditPoints = new Vector2[20];
        public int pointsNotFound = 0;
        public int startTime = 0;
        public bool startDayTime = false;
        public bool screenTransition = false;
        public float screenTransAlpha = 0f;
        public float screenTransTimer = 0f;
        public int screenTransDuration = 60; // in frames
        public int screenDuration = 60 * 9;
        public int escHeldTimer = 0;
        #endregion
        #region skyline whirlwind
        public bool skylineFlying = false;
        public float skylineAlpha = 0f;
        public int skylineFrameTimer = 0;
        public int skylineFrame = 0;
        #endregion

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
        #endregion
        // zones
        public static bool zoneTemple = false;
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
        #endregion

        public int voidTimeChangeTime = 0;


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
        #endregion

        // oinite statue
        public bool oiniteStatue = false;
        public bool[] oiniteDoubledBuff = new bool[Player.MaxBuffs];
        public bool[] discordPotBuff = new bool[Player.MaxBuffs];
        // info
        public int buffDPSCount = 0;
        public int buffDPS = 0;
        public bool alchemistTimer = false;
        public bool[] hideEAInfo = new bool[2];
        public bool dryadsRadar = false;
        public string nearbyEvil = "No evil";

        //encounters
        public string encounterText = "";
        public int encounterTextAlpha = 0;
        public int encounterTextTimer = 0;
        public bool finalText = false;

        // screenshake
        public float screenshakeAmount = 0; // dont go above 15 unless you want to have a seizure
        public int screenshakeTimer = 0;

        //double tapping down
        public bool doubleTappedDown = false;
        public int doubleDownWindow = 0;

        // left and right arrows
        public bool controlLeftArrow = false;
        public bool controlRightArrow = false;

        public int eaDash = 0;
        public int eaDashTime = 0;
        public int eaDashDelay = 0;

        // awakened mode itmes
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

        public Vector2 archaicProtectionPos = new Vector2();
        public int archaicProtectionTimer = 0;
        public override void Initialize()
        {
            mysteriousPotionsDrank = new bool[10];
            voidHeartsUsed = 0;
            chaosHeartsUsed = 0;
            lunarStarsUsed = 0;
        }
        public override void ResetEffects()
        {
            talkToAzana = false;
            saveAmmo = 0;
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
            fireHarpy = false;
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
            #endregion
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
            #endregion
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
            #endregion

            dashCooldown = false;
            medicineCooldown = false;
            frozenGauntlet = false;
            ancientDecayWeapon = false;
            lightningCloud = false;
            lightningCloudHidden = false;

            extinctionCurseImbue = false;
            discordantPotion = false;
            vilePower = false;
            superSpeed = false;


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

            oiniteStatue = false;

            alchemistTimer = false;
            dryadsRadar = false;
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
            {"voidBlood", voidBlood}
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
            //Tile orbTile = Framing.GetTileSafely((int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16));
            //if (orbTile.wall != 0) Main.NewText("f");
            EAWallSlide();
               // timers
               generalTimer++;
            skylineFrameTimer++;
            cosmicGlassCD--;
            toySlimeClawCD--;
            doubleDownWindow--;
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
            if (flare) flareShieldCD--; // so the cooldonw doesnt decrease when the player isnt using it
            if (aeroflakTimer <= 0) aeroflakHits = 0;
            if (bleedingHeart)
            {
                if (player.dashDelay != 0 || eaDashDelay != 0)
                {
                    if (Main.rand.NextBool(8))
                    {
                        Projectile.NewProjectile(player.Center, Main.rand.NextVector2Square(-6, 6), ModContent.ProjectileType<BloodbathDashP>(), 30, 6f, player.whoAmI);
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
                    Projectile.NewProjectile(player.Center + Main.rand.NextVector2Square(-300,300), Main.rand.NextVector2Square(-1, 1), ModContent.ProjectileType<AbyssalPortal>(), 300, 6f, player.whoAmI);
                }
                if (abyssalRage > 0)
                {
                    if (player.dead || !player.active) abyssalRage = 0;
                    player.allDamage *= 1.3f;
                    player.moveSpeed *= 1.5f;
                    player.accRunSpeed *= 1.5f;

                    int num = ModContent.GetInstance<Config>().lowDust ? 1 : 3;
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
                        Projectile.NewProjectile(player.Center, speed, ModContent.ProjectileType<AbyssalTentacle>(), 300, 6f, player.whoAmI, randAi0, randAi1);
                    }
                }
            }

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
            if (!NPC.AnyNPCs(mod.NPCType("VoidLeviathanHead"))) behemothGazeTimer = 0;
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
                if (ModContent.GetInstance<Config>().lowDust) num = 1;
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
            #region dashes
            if (eaDashDelay > 0)
            {
                eaDashDelay--;
                //return;
            }
            if (eaDashDelay < 0)
            {
                float maxDashSpeed = 18f;
                float maxSpeed = Math.Max(player.accRunSpeed, player.maxRunSpeed);
                float slowdown1 = 0.985f;
                float slowdown2 = 0.94f;
                int dashDelayAmount = 20;
                if (eaDash == 1 || eaDash == 2)
                {
                    int dustID = 127;
                    if (eaDash == 2)
                    {
                        dustID = 63;
                        maxDashSpeed = 24f;
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
                    eaDashDelay = dashDelayAmount;
                    if (player.velocity.X < 0f)
                    {
                        player.velocity.X = -maxSpeed;
                        //return;
                    }
                    if (player.velocity.X > 0f)
                    {
                        player.velocity.X = maxSpeed;
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
                if (flag)
                {
                    if (eaDash == 1)
                    {
                        player.velocity.X = 26f * (float)dashDir;
                        Point point = (player.Center + new Vector2((float)(dashDir * player.width / 2 + 2), player.gravDir * (float)(-(float)player.height) / 2f + player.gravDir * 2f)).ToTileCoordinates();
                        Point point2 = (player.Center + new Vector2((float)(dashDir * player.width / 2 + 2), 0f)).ToTileCoordinates();
                        if (WorldGen.SolidOrSlopedTile(point.X, point.Y) || WorldGen.SolidOrSlopedTile(point2.X, point2.Y))
                        {
                            player.velocity.X = player.velocity.X / 2f;
                        }
                        eaDashDelay = -1;
                        for (int i = 0; i < 20; i++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 127, 0f, 0f, 100, default(Color), 2f)];
                            dust.position.X = dust.position.X + (float)Main.rand.Next(-5, 6);
                            dust.position.Y = dust.position.Y + (float)Main.rand.Next(-5, 6);
                            dust.velocity *= 3f;
                            dust.scale *= 1f + (float)Main.rand.NextFloat(-0.4f, 0.4f);
                            dust.shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
                            dust.noGravity = true;
                        }
                    }
                    if (eaDash == 2)
                    {
                        player.velocity.X = 26f * (float)dashDir;
                        Point point = (player.Center + new Vector2((float)(dashDir * player.width / 2 + 2), player.gravDir * (float)(-(float)player.height) / 2f + player.gravDir * 2f)).ToTileCoordinates();
                        Point point2 = (player.Center + new Vector2((float)(dashDir * player.width / 2 + 2), 0f)).ToTileCoordinates();
                        if (WorldGen.SolidOrSlopedTile(point.X, point.Y) || WorldGen.SolidOrSlopedTile(point2.X, point2.Y))
                        {
                            player.velocity.X = player.velocity.X / 2f;
                        }
                        eaDashDelay = -1;
                        for (int i = 0; i < 20; i++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 127, 0f, 0f, 100, default(Color), 2f)];
                            dust.position.X = dust.position.X + (float)Main.rand.Next(-5, 6);
                            dust.position.Y = dust.position.Y + (float)Main.rand.Next(-5, 6);
                            dust.velocity *= 3f;
                            dust.scale *= 1f + (float)Main.rand.NextFloat(-0.4f, 0.4f);
                            dust.shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
                            dust.noGravity = true;
                        }
                    }
                }
            }
            #endregion
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

            if (skylineFlying)
            {
                if (skylineAlpha < 1) skylineAlpha += 0.05f;
            }
            else
            {
                if (skylineAlpha > 0) skylineAlpha -= 0.05f;
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
            #endregion

            ComputerText();

            if (MyWorld.credits)
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
                for (int k = 0; k < Main.projectile.Length; k++)
                {
                    Main.projectile[k].Kill();
                }
                for (int k = 0; k < Main.dust.Length; k++)
                {
                    Dust dust = Main.dust[k];
                    if (Vector2.Distance(dust.position, player.Center) < 90)
                        dust.active = false;
                }
                // find keypoints
                if (creditPoints[0].X == 0)
                {
                    for (int x = 0; x < Main.tile.GetLength(0); ++x)
                    {
                        for (int y = 0; y < Main.tile.GetLength(1); ++y)
                        {
                            //temple
                            if (creditPoints[1].X != 0 && creditPoints[2].X != 0 && creditPoints[3].X != 0 && creditPoints[4].X != 0 && creditPoints[5].X != 0 && creditPoints[6].X != 0 && creditPoints[7].X != 0 && creditPoints[8].X != 0)
                                break;
                            if (Main.tile[x, y] == null) continue;
                            else if (Main.tile[x, y].type == 237 && creditPoints[1].X == 0) // altar
                            {
                                creditPoints[1] = new Vector2(x * 16, y * 16);
                            }
                            // jungle with hive
                            else if (Main.tile[x, y].type == 225 && creditPoints[2].X == 0) // hive
                            {
                                creditPoints[2] = new Vector2(x * 16, y * 16);
                            }
                            // spidernest
                            else if (Main.tile[x, y].wall == 62 && creditPoints[3].X == 0) // spider wall
                            {
                                creditPoints[3] = new Vector2(x * 16, y * 16);
                            }
                            // sky island
                            else if (Main.tile[x, y].type == 202 && creditPoints[4].X == 0) // sunplate
                            {
                                creditPoints[4] = new Vector2(x * 16, y * 16);
                            }
                            // hallow
                            else if (Main.tile[x, y].type == 110 && creditPoints[5].X == 0) // hallowed grass
                            {
                                creditPoints[5] = new Vector2(x * 16, y * 16);
                            }
                            // evil
                            else if (Main.tile[x, y].type == 201 && creditPoints[6].X == 0) // crim grass
                            {
                                if (WorldGen.crimson) creditPoints[6] = new Vector2(x * 16, y * 16);
                            }
                            else if (Main.tile[x, y].type == 24 && creditPoints[6].X == 0) // corrupt grass bits
                            {
                                if (!WorldGen.crimson) creditPoints[6] = new Vector2(x * 16, y * 16);
                            }
                            // mushroom 
                            else if (Main.tile[x, y].type == 70 && creditPoints[7].X == 0) // mushroom grass
                            {
                                creditPoints[7] = new Vector2(x * 16, y * 16);
                            }
                            //snow
                            else if (Main.tile[x, y].type == 147 && creditPoints[8].X == 0)
                            {
                                creditPoints[8] = new Vector2(x * 16, y * 16);
                            }
                            else if (Main.tile[x, y].type == TileID.Marble && creditPoints[12].X == 0 && x > Main.spawnTileX - 200)
                            {
                                creditPoints[12] = new Vector2(x * 16, y * 16);
                            }
                            else if (Main.tile[x, y].type == TileID.Granite && creditPoints[13].X == 0 && x > Main.spawnTileX - 200)
                            {
                                creditPoints[13] = new Vector2(x * 16, y * 16);
                            }
                            else if (Main.tile[x, y].type == TileID.LivingMahogany && creditPoints[14].X == 0)
                            {
                                creditPoints[14] = new Vector2(x * 16, y * 16);
                            }
                            else if (Main.tile[x, y].type == TileID.LeafBlock && creditPoints[15].X == 0 && x > Main.spawnTileX - 200)
                            {
                                creditPoints[15] = new Vector2(x * 16, y * 16);
                            }
                            else continue;
                        }
                    }
                    creditPoints[9] = new Vector2(Main.spawnTileX * 16 + 600, (Main.maxTilesY - 200) * 16); // hell
                    creditPoints[10] = new Vector2(Main.dungeonX * 16, Main.dungeonY * 16); // dungeon
                    creditPoints[11] = new Vector2(1800, (float)Main.worldSurface * 16 - 16 * 150); // ocean, doesnt run unless one is missing

                    player.FindSpawn();
                    creditPoints[0] = new Vector2(player.SpawnX * 16, player.SpawnY * 16);
                    if (creditPoints[0].X < 0 || creditPoints[0].Y < 0) creditPoints[0] = new Vector2(Main.spawnTileX * 16, Main.spawnTileY * 16);
                }
                // spawn
                if (MyWorld.creditsCounter == 0)
                {
                    playerStartPos = player.Center;
                    startTime = (int)Main.time;
                    screenTransition = true;
                }
                if (MyWorld.creditsCounter == screenTransDuration / 2)
                {
                    desiredScPos = creditPoints[0] - Vector2.One * 50;
                    for (int k = 0; k < Main.npc.Length; k++)
                    {
                        NPC nPC = Main.npc[k];
                        if (nPC.active && !nPC.friendly && nPC.damage > 0) Main.npc[k].active = false;
                    }
                }
                if (MyWorld.creditsCounter > screenTransDuration / 2)
                {
                    player.Center = desiredScPos;
                    Main.dayTime = true;
                    Main.time = 27000;
                }
                if (MyWorld.creditsCounter > screenTransDuration / 2 && MyWorld.creditsCounter < screenDuration + screenTransDuration / 2) desiredScPos += new Vector2(1, 1);

                int creditsLength = screenDuration * 11;
                if (MyWorld.creditsCounter >= screenDuration && MyWorld.creditsCounter < creditsLength)
                {
                    int screenNum = pointsNotFound + (int)Math.Floor((decimal)(MyWorld.creditsCounter / screenDuration));
                    if (creditPoints[screenNum].X == 0)
                    {
                        pointsNotFound++;
                        screenNum = pointsNotFound + (int)Math.Floor((decimal)(MyWorld.creditsCounter / screenDuration));
                    }
                    Vector2 scroll = new Vector2(1, -1);
                    if (screenNum == 1) scroll = new Vector2(0, -1);
                    else if (screenNum == 2) scroll = new Vector2(1, 1);
                    else if (screenNum == 3) scroll = new Vector2(1, -1);
                    else if (screenNum == 4) scroll = new Vector2(-1, 1);
                    else if (screenNum == 5) scroll = new Vector2(1, 1);
                    else if (screenNum == 6) scroll = new Vector2(0, 1);
                    else if (screenNum == 7) scroll = new Vector2(1, 0);
                    else if (screenNum == 8) scroll = new Vector2(-0.5f, 2);
                    else if (screenNum == 9) scroll = new Vector2(1, 1);
                    else if (screenNum == 10) scroll = new Vector2(0, 1);
                    else if (screenNum == 11) scroll = new Vector2(1, 0);
                    else if (screenNum == 12) scroll = new Vector2(1, 0.5f);
                    else if (screenNum == 13) scroll = new Vector2(1, -0.25f);
                    else if (screenNum == 14) scroll = new Vector2(2, 1);
                    else if (screenNum == 15) scroll = new Vector2(0.2f, 1);
                    CreditsScroll(screenNum, scroll, screenDuration);
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
                if (escHeldTimer > 60) MyWorld.creditsCounter = creditsLength - 1;

                MyWorld.creditsCounter++;
                if (MyWorld.creditsCounter > creditsLength)
                {
                    screenTransition = true;
                    if (MyWorld.creditsCounter - creditsLength == screenTransDuration / 2)
                    {
                        MyWorld.credits = false;
                        MyWorld.creditsCounter = 0;
                        player.hideMisc[0] = false;
                        player.hideMisc[1] = false;
                        desiredScPos = player.Center;
                        player.Center = playerStartPos;
                        Main.time = startTime;
                        Main.dayTime = startDayTime;
                    }
                }
            }
            else
            {
                MyWorld.creditsCounter = 0;
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
            if (!ModContent.GetInstance<Config>().promptsDisabled)
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
                            if (Main.rand.Next(3) == 0 && !ModContent.GetInstance<Config>().lowDust)
                            {
                                /*Dust ash = Main.dust[Dust.NewDust(new Vector2(player.Center.X - Main.screenWidth / 2, player.Center.Y - Main.screenHeight / 2), Main.screenWidth, Main.screenHeight, 31, speed.X, speed.Y, 0, default(Color), 1f)];
                                ash.scale = 1.2f;
                                ash.fadeIn += num14 * 0.2f;
                                ash.velocity *= 1f + num13 * 0.5f;
                                ash.velocity *= 1f + num13;*/
                            }

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
                    #endregion
                }
            }
        }
            private static bool HotTile(Tile t)
            {
                if (t.type == TileID.Campfire ||
                    t.type == TileID.Fireplace) return true;
                return false;
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
            /*if (discordantPotion)
            {
                for (int l = 0; l < Player.MaxBuffs; l++)
                {
                    if (player.buffTime[l] <= 0)
                    {
                        discordPotBuff[l] = false;
                        continue;
                    }
                    if (!discordPotBuff[l])
                    {
                        if (player.buffType[l] == BuffID.ChaosState) player.buffTime[l] *= (int)(player.buffTime[l] * 0.75);
                        discordPotBuff[l] = true;        
                    }
                }
            }*/
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
                                        int num240 = (int)Main.tile[xPos, yPos].liquid; // which liquid type
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

        public override void UpdateBiomes()
        {
            zoneTemple = (MyWorld.lizardTiles > 50);
        }
        private void CreditsScroll(int screenNum, Vector2 scroll, int screenDuration)
        {
            int counterN = screenDuration * (screenNum - pointsNotFound);
            if (MyWorld.creditsCounter == counterN) screenTransition = true;
            if (MyWorld.creditsCounter - counterN == screenTransDuration / 2) desiredScPos = creditPoints[screenNum];
            if (MyWorld.creditsCounter - counterN > screenTransDuration / 2) player.Center = desiredScPos;
            if (MyWorld.creditsCounter - counterN > screenTransDuration / 2 && MyWorld.creditsCounter > counterN && MyWorld.creditsCounter < screenDuration * (screenNum + 1) + screenTransDuration / 2) desiredScPos += scroll;
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
            if (eaDashDelay < 0)
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
            return true;
        }
        public override void PreUpdate()
        {
            /*// to fix broken players
            {
                lunarStarsUsed = 0;
                chaosHeartsUsed = 0;
                voidHeartsUsed = 0;
            }*/
            if (oiniteDoubledBuff.Length != Player.MaxBuffs)
            {
                Array.Resize(ref oiniteDoubledBuff, Player.MaxBuffs);
            }
            if (discordPotBuff.Length != Player.MaxBuffs)
            {
                Array.Resize(ref discordPotBuff, Player.MaxBuffs);
            }
        }
        public override void UpdateBadLifeRegen()
        {
            neovirtuoTimer--; // wtf is this doin ghere
            // poison - 4
            // on fire - 8
            // frostburn - 12

            if (acidBurn)
            {
                player.lifeRegen -= 16;
            }
            if (dragonfire)
            {
                player.lifeRegen -= 20;
            }
            if (extinctionCurse || handsOfDespair)
            {
                player.lifeRegen -= 30;
            }
            if (chaosBurn || discordDebuff)
            {
                player.lifeRegen -= 40;
            }

            if (behemothGazeTimer > 600)
            {
                int amount = (int)MathHelper.Lerp(0, 80, (float)(leviathanDist - 3000) / 9000f);
                player.lifeRegen -= amount;
            }
            if (endlessTears)
            {
                player.velocity *= 0.8f;
            }
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
            if (player.lifeRegen < 0 && theAntidote)
            {
                player.lifeRegen /= 2;
            }
        }
        public override void UpdateLifeRegen()
        {
            if (MyWorld.credits) player.lifeRegen = 0; // to stop suffocation in sand and other things            
            if (archaicProtectionTimer > 0) player.lifeRegen = 0;
            if (voidBlood && player.lifeRegen > 0) player.lifeRegen = 0;
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



            //Point point = player.Center.ToTileCoordinates();
            bool useblizzard = MyWorld.hailStormTime > 0 && player.ZoneOverworldHeight && !player.ZoneDesert && !ActiveBoss() && !ModContent.GetInstance<Config>().lowDust;
            player.ManageSpecialBiomeVisuals("Blizzard", useblizzard, default(Vector2));

            bool useInfWrath = MyWorld.firePrompt > ElementsAwoken.bossPromptDelay && !ActiveBoss();
            player.ManageSpecialBiomeVisuals("ElementsAwoken:AshShader", useInfWrath);
            player.ManageSpecialBiomeVisuals("ElementsAwoken:AshSky", useInfWrath);

            if (useInfWrath)
            {
                SkyManager.Instance.Activate("ElementsAwoken:InfernacesWrath", player.Center);
                if (!ModContent.GetInstance<Config>().lowDust) Overlays.Scene.Activate("ElementsAwoken:AshParticles", player.Center);
            }
            else 
            {
                SkyManager.Instance.Deactivate("ElementsAwoken:InfernacesWrath");
                Overlays.Scene.Deactivate("ElementsAwoken:AshParticles");
            }
            if (ModContent.GetInstance<Config>().lowDust) Overlays.Scene.Deactivate("ElementsAwoken:AshParticles");
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
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            MiscEffects.visible = true;
            layers.Add(MiscEffects);
            if (MyWorld.credits && MyWorld.creditsCounter > screenTransDuration / 2)
            {
                foreach (PlayerLayer layer in layers)
                {
                    layer.visible = false;
                }
            }
        }
        public static readonly PlayerLayer MiscEffects = new PlayerLayer("ElementsAwoken", "MiscEffects", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawInfo drawInfo) {
            if (drawInfo.shadow != 0f)
            {
                return;
            }
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            MyPlayer modPlayer = drawPlayer.GetModPlayer<MyPlayer>();
            if (drawInfo.drawPlayer.active &&!drawInfo.drawPlayer.dead)
            {
                if (modPlayer.skylineAlpha > 0)
                {
                    Texture2D texture = mod.GetTexture("Extra/SkylineWhirlwind");
                    int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
                    int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f + 89 - Main.screenPosition.Y);
                    Color color = Lighting.GetColor((int)(drawPlayer.Center.X / 16), (int)(drawPlayer.Center.Y / 16)) * modPlayer.skylineAlpha;
                    Rectangle rect = new Rectangle(0, (texture.Height / 4) * modPlayer.skylineFrame, texture.Width, texture.Height / 4);
                    DrawData data = new DrawData(texture, new Vector2(drawX, drawY), rect, color, 0f, new Vector2(texture.Width / 2f, texture.Height / 2f), 1.3f, drawPlayer.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
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
            }
        });
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            PlayerUtils playerUtils = player.GetModPlayer<PlayerUtils>();

            if (ancientDecayWeapon)
            {
                target.AddBuff(mod.BuffType("AncientDecay"), 360, false);
            }
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
            if (dragonmailGreathelm)
            {
                target.AddBuff(mod.BuffType("Dragonfire"), 300, false);
            }
            if (sufferWithMe)
            {
                target.AddBuff(mod.BuffType("ChaosBurn"), 300, false);
            }
            if (voidWalkerArmor == 1)
            {
                target.AddBuff(mod.BuffType("ExtinctionCurse"), 300, false);
            }
            if (extinctionCurseImbue)
            {
                target.AddBuff(mod.BuffType("ExtinctionCurse"), 360, false);
            }
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
            if (noDamageCounter > 0)
            {
                noDamageCounter = 0;
            }
            if (bleedingHeart)
            {
                if (target.life <= 0 && playerUtils.enemiesKilledLast10Secs >= 4 && !target.SpawnedFromStatue)
                {
                    player.AddBuff(ModContent.BuffType<Bloodbath>(), 600, false);
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
                if (ModContent.GetInstance<Config>().lowDust) makeDust = makeDust = Main.rand.Next(8) == 0;
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
            damage = (int)(damage * damageTaken);

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
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, projVel.X, projVel.Y, ModContent.ProjectileType<MechLightning>(), 200, 0f, Main.myPlayer, 0f, 0f);
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
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("LegoBrickFriendly"), 5, 2f, 0);
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
                player.AddBuff(ModContent.BuffType<AwokenHealing>(), 180);
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
            if (neovirtuoBonus)
            {
                if (ElementsAwoken.neovirtuo.JustPressed)
                {
                    if (player.FindBuffIndex(mod.BuffType("NeovirtuoCooldown")) == -1 && !player.dead)
                    {
                        Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
                        vector2.X = (float)Main.mouseX + Main.screenPosition.X;
                        vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;
                        Projectile.NewProjectile(vector2.X, vector2.Y, 0f, 0f, mod.ProjectileType("NeovirtuoPortal"), 0, 0, player.whoAmI, 0f, 0f);
                        Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 113);
                        player.AddBuff(mod.BuffType("NeovirtuoCooldown"), 1800);
                    }
                }
            }
            if (chaosRing)
            {
                if (ElementsAwoken.specialAbility.JustPressed)
                {
                    if (player.FindBuffIndex(mod.BuffType("ChaosShieldCooldown")) == -1 && !player.dead)
                    {
                        player.AddBuff(mod.BuffType("ChaosShield"), 900);
                        player.AddBuff(mod.BuffType("ChaosShieldCooldown"), 3600);
                    }
                }
            }
            if (flare)
            {
                if (ElementsAwoken.specialAbility.JustPressed)
                {
                    if (!player.dead)
                    {
                        if (flareShieldCD <= 0)
                        {
                            player.AddBuff(mod.BuffType("FlareShield"), 900);
                            flareShieldCD = 3600;
                        }
                        else
                        {
                            Main.NewText(flareShieldCD / 60 + " seconds left until you can use Flare");
                        }
                    }
                }
            }
            if (voidEnergyCharge > 600)
            {
                if (ElementsAwoken.specialAbility.JustPressed)
                {
                    voidEnergyTimer = voidEnergyCharge / 4;
                    voidEnergyCharge = 0;
                    Main.PlaySound(29, (int)player.position.X, (int)player.position.Y, 96);
                }
            }
            if (vleviAegis)
            {
                if (ElementsAwoken.dash2.JustPressed && aegisDashCooldown <= 0)
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
            if (player.controlDown && player.releaseDown)
            {
                if (doubleDownWindow > 0)
                {
                    doubleTappedDown = true;
                    doubleDownWindow = 0;
                }
                else
                {
                    doubleDownWindow = 15;
                }
            }

            if (doubleTappedDown && forgedArmor)
            {
                forgedShackled = 600;
            }
            if (doubleTappedDown && awokenWood)
            {
                Projectile.NewProjectile(player.Center, Vector2.Zero, ModContent.ProjectileType<LifesAura>(), 0, 0, player.whoAmI);
            }
            if (player.controlDown && player.releaseDown && forgedShackled > 0)
            {
                flingToShackle = true;
            }

            if (crystallineLocket && ElementsAwoken.specialAbility.JustPressed && player.FindBuffIndex(mod.BuffType("CrystallineLocketCD")) == -1)
            {
                crystallineLocketCrit = 600;
                if (!ModContent.GetInstance<Config>().debugMode) player.AddBuff(mod.BuffType("CrystallineLocketCD"), 3600);
                else player.AddBuff(mod.BuffType("CrystallineLocketCD"), 60);

            }

            if (toySlimeClaw && toySlimeClawCD <= 0&& ElementsAwoken.specialAbility.JustPressed)
            {
                Main.PlaySound(SoundID.Item95, player.Center);

                float speed = 5f;
                float rotation = (float)Math.Atan2(player.Center.Y - Main.MouseWorld.Y, player.Center.X - Main.MouseWorld.X);
                Vector2 projVel = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1));
                ElementsAwoken.DebugModeText(rotation);
                if (rotation < -(Math.PI / 2 - Math.PI / 8) && rotation > -(Math.PI / 2 + Math.PI / 8))
                {
                    player.velocity.Y -= 16f;
                    int numberProjectiles = Main.rand.Next(3, 7);
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = projVel.RotatedByRandom(MathHelper.ToRadians(35));
                        Projectile.NewProjectile(player.Center,perturbedSpeed, ModContent.ProjectileType<SlimeClawBall>(), 20, 3f, player.whoAmI);
                    }
                }
                Projectile.NewProjectile(player.Center, projVel, ModContent.ProjectileType<SlimeClawBall>(), 20, 3f, player.whoAmI);
                toySlimeClawCD = 60;
            }
            if (greatLens && player.FindBuffIndex(mod.BuffType("GreatLensCD")) == -1 && ElementsAwoken.specialAbility.JustPressed)
            {
                greatLensTimer = 600;
                if (!ModContent.GetInstance<Config>().debugMode) player.AddBuff(mod.BuffType("GreatLensCD"), 3600);
                else player.AddBuff(mod.BuffType("GreatLensCD"), 60);
            }
            if (ElementsAwoken.armorAbility.JustPressed && voidWalkerCooldown <= 0 && voidWalkerArmor > 0)
            {
                voidWalkerAura = 300;
            }
            doubleTappedDown = false;
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
            player.sliding = false;
            toySlimeClawSliding = false;
            if (player.slideDir != 0 && player.spikedBoots == 0  && toySlimeClaw && !player.mount.Active && ((player.controlLeft && player.slideDir == -1) || (player.controlRight && player.slideDir == 1)))
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
    }
}