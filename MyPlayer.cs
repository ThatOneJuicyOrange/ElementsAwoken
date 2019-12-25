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

namespace ElementsAwoken
{
    public class MyPlayer : ModPlayer
    {
        private const int saveVersion = 0;
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
        #endregion   
        #region buffs
        public bool extinctionCurseImbue = false;
        public bool discordantPotion = false;
        public bool superSpeed = false;
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
        public bool scourgeDrive = false;
        public bool scourgeSpeed = false;
        public bool spikeBoots = false;
        public bool sonicArm = false;
        public bool nyanBoots = false;
        public bool theAntidote = false;
        public bool cosmicGlass = false;
        public int cosmicGlassCD = 0;
        public bool sufferWithMe = false;
        public bool strangeUkulele = false;
        public bool crystallineLocket = false;
        public int crystallineLocketCrit = 0;
        public int archaicProtectionTimer = 0;
        public Vector2 archaicProtectionPos = new Vector2();
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

        //skyline whirlwind
        public bool skylineFlying = false;
        public float skylineAlpha = 0f;
        public int skylineFrameTimer = 0;
        public int skylineFrame = 0;

        public bool[] mysteriousPotionsDrank;

        #region armor bonuses
        public bool aeroArmor = false;
        public bool oceanicArmor = false;
        public bool voidArmor = false;
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
        #endregion
        // zones
        public static bool zoneTemple = false;
        // dash & hypothermia
        public bool ninjaDash = false;
        public bool viridiumDash = false;
        public bool canGetHypo = false;
        public int canGetHypoTimer = 0;
        public int dashDustTimer = 0;
        #region timers and cooldowns
        public int neovirtuoTimer = 0;
        public float chaosBoost = 0;
        public float chaosDamageBoost = 0;
        public int masterSwordCharge = 0;
        public float masterSwordCountdown = 0;
        public float immortalResolveCooldown = 0;
        public float hellsReflectionTimer = 0;
        public float voidPortalCooldown = 0;
        #endregion
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
        public bool increasedToySlimeChance = false;
        public int toySlimeChanceTimer = 0;
        public bool increasedObserverChance = false;
        public int observerChanceTimer = 0;
        // damage modifiers
        public float damageTaken = 1f;
        // life fruits
        //private const int maxEmptyVessels = 10;
        //private const int maxChaosHearts = 10;
        public int voidHeartsUsed = 0;
        public int chaosHeartsUsed = 0;

        public bool extraAccSlot = false;

        public bool voidCompressor = false;

        public int lunarStarsUsed = 0;
        public int statManaMax3 = 0;

        public int shieldHearts = 0;
        public int shieldLife = 0;

        // oinite statue
        public bool oiniteStatue = false;
        public bool[] doubledBuff = new bool[22];
        // info
        public int buffDPSCount = 0;
        public int buffDPS = 0;
        public bool alchemistTimer = false;
        public bool[] hideEAInfo = new bool[2];
        public bool dryadsRadar = false;
        public string nearbyEvil = "No evil";

        //encounters
        public int encounterTextTimer = 0;
        public string encounterText = "";
        public int encounterTextAlpha = 0;
        public int encounterTextAlphaChange = 0;
        public bool finalText = false;

        //double tapping down
        public bool doubleTappedDown = false;
        public int doubleDownWindow = 0;

        // left and right arrows
        public bool controlLeftArrow = false;
        public bool controlRightArrow = false;

        bool calamityEnabled = ModLoader.GetMod("CalamityMod") != null;

        public override void Initialize()
        {
            mysteriousPotionsDrank = new bool[10];
        }
        public override void ResetEffects()
        {
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
            sonicArm = false;
            nyanBoots = false;
            skylineFlying = false;
            vleviAegis = false;
            theAntidote = false;
            cosmicGlass = false;
            sufferWithMe = false;
            strangeUkulele = false;
            crystallineLocket = false;

            aeroArmor = false;
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
        }
        // for the bonus life
        public override void clientClone(ModPlayer clientClone)
        {
            MyPlayer clone = clientClone as MyPlayer;
        }
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = mod.GetPacket();
            packet.Write((byte)player.whoAmI);
            packet.Write((byte)ElementsAwokenMessageType.StarHeartSync);
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
            {"extraAccSlot", extraAccSlot}
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
            if (tag.ContainsKey("mysteriousPotionsDrankList"))
            {
                var list = tag.GetList<bool>("mysteriousPotionsDrankList");
                mysteriousPotionsDrank = new List<bool>(list).ToArray();
            }
        }
    
        public override void PostUpdateMiscEffects()
        {
            if (superSpeed)
            {
                player.moveSpeed *= 3;
                player.runAcceleration *= 10;
            }
            if (superSlow)
            {
                player.moveSpeed *= 0.1f;
            }
            // timers
            if (cosmicGlass) cosmicGlassCD--;
            doubleDownWindow--;
            hellsReflectionTimer--;
            voidPortalCooldown--;
            toyArmorCooldown--;
            crowsArmorCooldown--;
            crystallineLocketCrit--;
            skylineFrameTimer++;

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

            if (aeroArmor)
            {
                if (Main.rand.Next(120) == 0)
                {
                    Projectile.NewProjectile(player.Center.X + Main.rand.Next(-120, 120), player.Center.Y - Main.rand.Next(15, 50), 0f, 0f, mod.ProjectileType("AeroStorm"), 30, 1.25f, Main.myPlayer, 0f, 0f);
                }
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

            if (increasedToySlimeChance)
            {
                toySlimeChanceTimer--;
                if (toySlimeChanceTimer <= 0)
                {
                    increasedToySlimeChance = false;
                }
            }

            if (increasedObserverChance)
            {
                observerChanceTimer--;
                if (observerChanceTimer <= 0)
                {
                    increasedObserverChance = false;
                }
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
            if (MyWorld.encounterTimer == 3200)
            {
                encounterTextTimer = 300;
                encounterTextAlpha = 0;
                if (MyWorld.encounter1)
                {
                    encounterText = "I see it now.";
                }
                if (MyWorld.encounter2)
                {
                    encounterText = "Who are you really?";
                }
                if (MyWorld.encounter3)
                {
                    encounterText = "You seek power...";
                }
            }
            else if (MyWorld.encounterTimer == 2700)
            {
                encounterTextTimer = 300;
                encounterTextAlpha = 0;
                if (MyWorld.encounter1)
                {
                    encounterText = "A new challenger rises.";
                }
                if (MyWorld.encounter2)
                {
                    encounterText = "A simple lost soul?";
                }
                if (MyWorld.encounter3)
                {
                    encounterText = "But you haven't asked why.";
                }
            }
            else if (MyWorld.encounterTimer == 2200)
            {
                encounterTextTimer = 300;
                encounterTextAlpha = 0;
                if (MyWorld.encounter1)
                {
                    encounterText = "You have a long way to go.";
                }
                if (MyWorld.encounter2)
                {
                    encounterText = "The path ahead is long.";
                }
                if (MyWorld.encounter3)
                {
                    encounterText = "Does it matter?";
                }
            }
            else if (MyWorld.encounterTimer == 1700)
            {
                if (!MyWorld.encounter1)
                {
                    encounterTextTimer = 300;
                    encounterTextAlpha = 0;
                }
                if (MyWorld.encounter2)
                {
                    encounterText = "You are getting closer.";
                }
                if (MyWorld.encounter3)
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

            // changing the alpha (not the alpha lol)
            if (encounterTextTimer > 60)
            {
                encounterTextAlphaChange = 0;
            }
            else
            {
                encounterTextAlphaChange = 1;
            }
            if (encounterTextAlphaChange == 0)
            {
                encounterTextAlpha += 5;
                if (encounterTextAlpha >= 255)
                {
                    encounterTextAlpha = 255;
                }
            }
            else
            {
                encounterTextAlpha -= 5;
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
                NPC bossCheck = Main.npc[0];
                for (int i = 0; i < Main.npc.Length; ++i)
                {
                    if (Main.npc[i].boss && Main.npc[i].active)
                    {
                        bossCheck = Main.npc[i];
                        break;
                    }
                }
                bool underground = player.Center.Y / 16 > Main.maxTilesY * 0.225;

                bool inTown = false;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (nPC.townNPC && Vector2.Distance(player.Center, nPC.Center) <= 2000)
                    {
                        inTown = true;
                    }
                }

                if (!bossCheck.boss)
                {
                    // wasteland prompt
                    if (NPC.downedBoss1 && !MyWorld.downedWasteland)
                    {
                        player.AddBuff(mod.BuffType("ScorpionBreakout"), 60);
                        // spawn code in the scorpion code
                    }
                    // infernace prompt
                    if (NPC.downedBoss3 && !MyWorld.downedInfernace)
                    {
                        player.AddBuff(mod.BuffType("InfernacesWrath"), 60);
                        if (!underground)
                        {
                            float num13 = MathHelper.Lerp(0.2f, 0.35f, 0.5f);
                            float num14 = MathHelper.Lerp(0.5f, 0.7f, 0.5f);
                            Vector2 speed = new Vector2(Main.windSpeed * 10f, 3f);
                            if (Main.rand.Next(3) == 0)
                            {
                                Dust ash = Main.dust[Dust.NewDust(new Vector2(player.Center.X - Main.screenWidth / 2, player.Center.Y - Main.screenHeight / 2), Main.screenWidth, Main.screenHeight, 31, speed.X, speed.Y, 0, default(Color), 1f)];
                                ash.scale = 1.2f;
                                ash.fadeIn += num14 * 0.2f;
                                ash.velocity *= 1f + num13 * 0.5f;
                                ash.velocity *= 1f + num13;
                            }

                            if (Main.rand.Next(120) == 0)
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
                    // regaroth prompt
                    if (NPC.downedMechBossAny && !MyWorld.downedRegaroth)
                    {
                        player.AddBuff(mod.BuffType("DarkenedSkies"), 60);

                        if (!Main.raining && Main.rand.Next(3500) == 0)
                        {
                            Main.raining = true;
                            Main.rainTime = 18000;
                        }
                        if (!underground)
                        {
                            if (Main.raining && Main.rainTime > 0 && Main.rand.Next(250) == 0)
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
                    // permafrost prompt
                    if (NPC.downedPlantBoss && !MyWorld.downedPermafrost)
                    {
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
                        for (int i = topLeft.X; i <= bottomRight.X; i++)
                        {
                            for (int j = topLeft.Y; j <= bottomRight.Y; j++)
                            {
                                Tile t = Framing.GetTileSafely(i, j);
                                if (t.type == TileID.Campfire || t.lava())
                                {
                                    canGetHypo = false;
                                    canGetHypoTimer = 240;
                                    //Main.NewText("AAAAA TOO HOT", 182, 15, 15, false);
                                }
                                else
                                {
                                    if (canGetHypoTimer <= 0)
                                    {
                                        canGetHypo = true;
                                    }
                                }
                            }
                        }
                        canGetHypoTimer--;
                        if (canGetHypo)
                        {
                            player.AddBuff(mod.BuffType("Hypothermia"), 30);
                        }
                        if (!MyWorld.hailStorm && Main.rand.Next(40000) == 0 && !player.ZoneDesert)
                        {
                            MyWorld.hailStorm = true;
                            MyWorld.hailStormTime = 3600 + Main.rand.Next(0, 3600);
                        }
                    }
                    // aqueous prompt
                    if (NPC.downedFishron && !MyWorld.downedAqueous)
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
                    // void leviathan prompt
                    if (MyWorld.downedVolcanox && !MyWorld.downedVoidLeviathan)
                    {
                        player.AddBuff(mod.BuffType("Psychosis"), 60);
                    }

                    //HAILSTORM
                    if (MyWorld.hailStorm && !player.ZoneDesert)
                    {
                        MyWorld.hailStormTime--;

                        if (MyWorld.hailStormTime <= 0)
                        {
                            MyWorld.hailStorm = false;
                        }
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
                #endregion
            }
        }

        public override void PostUpdateBuffs()
        {
            if (oiniteStatue)
            {
                // it is always checking all 22 slots no matter if its active, so we check if its active by player.buffTime[l] <= 0 - angry orang
                for (int l = 0; l < Player.MaxBuffs; l++)
                {
                    if (!doubledBuff[l])
                    {
                        if (player.buffType[l] != BuffID.PotionSickness &&
                            player.buffType[l] != BuffID.ManaSickness &&
                            !Main.buffNoTimeDisplay[l])
                            player.buffTime[l] *= 2;
                        doubledBuff[l] = true; // set the buff to doubled anyway so it stops checking          
                    }
                    if (player.buffTime[l] <= 0)
                    {
                        doubledBuff[l] = false;
                    }
                }
            }
            if (discordantPotion)
            {
                for (int l = 0; l < Player.MaxBuffs; l++)
                {
                    if (!doubledBuff[l])
                    {
                        if (player.buffType[l] == BuffID.ChaosState)
                            player.buffTime[l] = (int)(player.buffTime[l] * 0.75);
                        doubledBuff[l] = true; // set the buff to doubled anyway so it stops checking                 
                    }
                    if (player.buffTime[l] <= 0)
                    {
                        doubledBuff[l] = false;
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
            item.SetDefaults(mod.ItemType("MysticGemstone"));
            items.Add(item);
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
            }
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
                            NetMessage.SendData(23, -1, -1, null, n, 0f, 0f, 0f, 0, 0, 0);
                            bunny.active = false;

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
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (acidBurn)
            {
                r *= 0.69f;
                g *= 1f;
                b *= 0.48f;
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

            bool useEncounter1 = MyWorld.encounter1;
            player.ManageSpecialBiomeVisuals("ElementsAwoken:Encounter1", useEncounter1);
            bool useEncounter2 = MyWorld.encounter2;
            player.ManageSpecialBiomeVisuals("ElementsAwoken:Encounter2", useEncounter2);
            bool useEncounter3 = MyWorld.encounter3;
            player.ManageSpecialBiomeVisuals("ElementsAwoken:Encounter3", useEncounter3);

            bool useDespair = voidEnergyTimer > 0 || voidWalkerAura > 0;
            player.ManageSpecialBiomeVisuals("ElementsAwoken:Despair", useDespair);



            //Point point = player.Center.ToTileCoordinates();
            bool useblizzard = MyWorld.hailStorm && player.ZoneOverworldHeight && !player.ZoneDesert && !ActiveBoss();
            player.ManageSpecialBiomeVisuals("Blizzard", useblizzard, default(Vector2));

            // thanks misaro
            bool useInfWrath = NPC.downedBoss3 && !MyWorld.downedInfernace && !ActiveBoss();
            if (useInfWrath)
            {
                SkyManager.Instance.Activate("ElementsAwoken:InfernacesWrath", player.Center);
            }
            else
            {
                SkyManager.Instance.Deactivate("ElementsAwoken:InfernacesWrath");
            }

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
            if (modPlayer.skylineAlpha > 0 && drawInfo.drawPlayer.active &&!drawInfo.drawPlayer.dead)
            {
                Texture2D texture = mod.GetTexture("Extra/SkylineWhirlwind");
                int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
                int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f + 89 - Main.screenPosition.Y);
                Color color = Lighting.GetColor((int)(drawPlayer.Center.X / 16), (int)(drawPlayer.Center.Y / 16)) * modPlayer.skylineAlpha;
                Rectangle rect = new Rectangle(0, (texture.Height / 4) * modPlayer.skylineFrame, texture.Width, texture.Height / 4);
                DrawData data = new DrawData(texture, new Vector2(drawX, drawY), rect, color, 0f, new Vector2(texture.Width / 2f, texture.Height / 2f), 1.3f, drawPlayer.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                Main.playerDrawData.Add(data);
            }
        });
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (ancientDecayWeapon)
            {
                target.AddBuff(mod.BuffType("AncientDecay"), 360, false);
            }
            if (venomSample)
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
                immortalResolveCooldown--;
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

            bool hardMode = Main.hardMode;

            if (lightningCloud && lightningCloudCharge > 60)
            {
                if (damage > 0 && damageSource.SourceOtherIndex != 0)
                {
                    Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 93);


                    int lightningDamage = 10;
                    float speed = 6;
                    if (hardMode)
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
                if (damageSource.SourceOtherIndex == 0)
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

            if (spikeBoots)
            {
                if (damageSource.SourceOtherIndex == 3)
                {
                    return false;
                }
            }

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
            return true;
            //return true;
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (vleviAegis)
            {
                vleviAegisDamage += (int)damage;
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
                    if (player.FindBuffIndex(mod.BuffType("FlareShieldCooldown")) == -1 && !player.dead)
                    {
                        player.AddBuff(mod.BuffType("FlareShield"), 900);
                        player.AddBuff(mod.BuffType("FlareShieldCooldown"), 3600);
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
            player.controlDown = false;
            player.controlRight = false;
            player.controlJump = false;
        }
    }
}