using ElementsAwoken.Effects;
using ElementsAwoken.Effects.Eyes;
using ElementsAwoken.Events.RadiantRain.Enemies;
using ElementsAwoken.Events.VoidEvent;
using ElementsAwoken.Items.BossDrops.RadiantMaster;
using ElementsAwoken.Items.BossSummons;
using ElementsAwoken.Items.ItemSets.Radia;
using ElementsAwoken.Items.Tech.Weapons.Tier6;
using ElementsAwoken.NPCs;
using ElementsAwoken.NPCs.Bosses.Aqueous;
using ElementsAwoken.NPCs.Bosses.Infernace;
using ElementsAwoken.NPCs.Bosses.Permafrost;
using ElementsAwoken.NPCs.Bosses.Regaroth;
using ElementsAwoken.NPCs.Bosses.TheGuardian;
using ElementsAwoken.NPCs.Bosses.VoidLeviathan;
using ElementsAwoken.NPCs.Town;
using ElementsAwoken.NPCs.VolcanicPlateau;
using ElementsAwoken.NPCs.VolcanicPlateau.Bosses;
using ElementsAwoken.NPCs.VolcanicPlateau.Sulfur;
using ElementsAwoken.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.GameContent.Liquid;
using Terraria.GameContent.Shaders;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace ElementsAwoken
{
    public class ElementsAwoken : Mod
    {
        public static DynamicSpriteFont encounterFont;

        internal UserInterface AlchemistUserInterface;
        internal PromptInfoUI PromptUI;
        private UserInterface PromptInfoUserInterface;
        internal BootWingsUI BootUI;
        private UserInterface BootUserInterface;
        internal QuestListUI QuestUI;
        private UserInterface QuestUserInterface;
        internal MechCraftingUI MechUI;
        private UserInterface MechUserInterface;
        internal UI.AcidTapUI AcidTapUI;
        private UserInterface AcidTapUserInterface;

        public static bool usingScrollbar = false;

        public static ModHotKey specialAbility;
        public static ModHotKey questLog;
        public static ModHotKey dash2;
        public static ModHotKey sandstormA;
        public static ModHotKey timeA;
        public static ModHotKey rainA;
        public static ModHotKey wispA;

        public static ElementsAwoken instance;

        public static bool calamityEnabled;
        public static bool thoriumEnabled;
        public static bool bossChecklistEnabled;
        public static bool ancientsAwakenedEnabled;
        public static bool eaMusicEnabled;
        public static bool eaRetroMusicEnabled;

        public static bool[,] glowMap = new bool[100, 100]; // could store all the tiles but lets cheat

        public static int[] screenTextTimer = new int[30];
        public static int[] screenTextDuration = new int[30];
        public static float[] screenTextAlpha = new float[30];
        public static float[] screenTextScale = new float[30];
        public static string[] screenText = new string[30];
        public static Vector2[] screenTextPos = new Vector2[30];

        /*public static Vector2 currentScreenPos = new Vector2();
        public static Vector2 desiredScreenPos = new Vector2();
        public static Vector2 startScreenPos = new Vector2();
        public static float screenLerp = 0;
        public static float screenLerpState = 0;
        public static int screenLerpTimer = 0;
        public static int screenLerpStickDuration = 0;
        public static int screenLerpDuration = 0;*/
        //public static Dictionary<int, Color> RarityColors = new Dictionary<int, Color>();

        public static Texture2D AADeathBall;
        public static Texture2D insanityTex;
        public static Texture2D heartGlowTex;
        public static Texture2D plateauLavaTrans;
        public static Texture2D plateauLavaTransTile;
        public static Texture2D lavaTrans;
        public static Texture2D lavaTransTile;

        public static List<int> instakillImmune = new List<int>();

        private static float logoRotation;
        private static float logoRotationDirection = 1f;
        private static float logoRotationSpeed = 1f;

        private static float logoScale = 1f;
        private static float logoScaleDirection = 1f;
        private static float logoScaleSpeed = 1f;

        public static bool aprilFools = false;

        public static bool mouseLeftReleasedDuringChat = false;

        public static int encounter = 0;
        public static bool encounterSetup = false;
        public static int encounterTimer = 0;
        public static int encounterShakeTimer = 0;

        public const int bossPromptDelay = 108000;

        // for updating textures
        public static bool blackCloudsEnabled;
        public static int currentRainTex = 0;
        public static int currentHeartTex = 0;
        public static int currentManaTex = 0;
        public static int currentLavaTex = 0;

        public static int generalTimer = 0;

        public static int plateauFlowerGrowChance = 12;

        public static int[] dustTimer = new int[Main.maxDust];

        public ElementsAwoken()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
        }

        public static void PremultiplyTexture(Texture2D texture)
        {
            Color[] buffer = new Color[texture.Width * texture.Height];
            texture.GetData(buffer);
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Color.FromNonPremultiplied(buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A);
            }
            texture.SetData(buffer);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(this);
            recipe.AddIngredient(null, "Stardust", 5);
            recipe.SetResult(ItemID.FallenStar, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(null, "ThrowerEmblem");
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.SetResult(ItemID.AvengerEmblem, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(null, "SunFragment", 5);
            recipe.AddIngredient(ItemID.StoneBlock, 100);
            recipe.SetResult(ItemID.LihzahrdBrick, 100);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(null, "Stardust", 13);
            recipe.AddIngredient(ItemID.Glass, 12);
            recipe.AddIngredient(ItemID.RecallPotion, 6);
            recipe.AddRecipeGroup("ElementsAwoken:EvilBar", 8);
            recipe.SetResult(ItemID.MagicMirror, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(null, "CInfinityCrys", 1);
            recipe.AddIngredient(null, "Pyroplasm", 30);
            recipe.AddIngredient(ItemID.LunarBar, 18);
            recipe.AddIngredient(ItemID.CrystalShard, 10);
            recipe.AddIngredient(ItemID.PixieDust, 6);
            recipe.AddIngredient(ItemID.UnicornHorn, 2);
            recipe.SetResult(ItemID.RodofDiscord, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(null, "DeathwishFlame", 10);
            recipe.AddIngredient(ItemID.Torch, 1);
            recipe.SetResult(ItemID.WaterCandle, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddRecipeGroup("Wood", 2);
            recipe.AddIngredient(ItemID.StoneBlock, 5);
            recipe.SetResult(ItemID.ThrowingKnife, 25);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddRecipe();

            // accessories
            recipe = new ModRecipe(this);
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddIngredient(ItemID.Bone, 16);
            recipe.SetResult(ItemID.WaterWalkingBoots, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddRecipeGroup("ElementsAwoken:GoldBar", 12);
            recipe.AddIngredient(ItemID.SoulofFlight, 3);
            recipe.SetResult(ItemID.LuckyHorseshoe, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddRecipeGroup("ElementsAwoken:CobaltBar", 12);
            recipe.AddIngredient(ItemID.Bone, 30);
            recipe.SetResult(ItemID.CobaltShield, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.LifeCrystal, 1);
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddIngredient(ItemID.Ruby, 1);
            recipe.SetResult(ItemID.BandofRegeneration, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.ManaCrystal, 1);
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddIngredient(ItemID.Sapphire, 1);
            recipe.SetResult(ItemID.BandofStarpower, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.HellstoneBar, 12);
            recipe.AddIngredient(ItemID.SoulofLight, 4);
            recipe.AddIngredient(null, "MagmaCrystal", 5);
            recipe.SetResult(ItemID.LavaCharm, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddIngredient(ItemID.SwiftnessPotion, 1);
            recipe.AddIngredient(null, "LensFragment", 8);
            recipe.SetResult(ItemID.Aglet, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.JungleRose);
            recipe.AddIngredient(ItemID.ManaCrystal, 5);
            recipe.SetResult(ItemID.NaturesGift);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Vine, 3);
            recipe.AddIngredient(ItemID.SwiftnessPotion, 3);
            recipe.AddIngredient(ItemID.JungleSpores, 15);
            recipe.AddIngredient(ItemID.Stinger, 5);
            recipe.SetResult(ItemID.AnkletoftheWind);
            recipe.AddTile(TileID.Anvils);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Bone, 30);
            recipe.AddIngredient(ItemID.Feather, 9);
            recipe.AddIngredient(null, "Stardust", 12);
            recipe.SetResult(ItemID.HermesBoots);
            recipe.AddTile(TileID.Anvils);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.ManaCrystal, 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddIngredient(ItemID.NaturesGift, 1);
            recipe.SetResult(ItemID.CelestialMagnet, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Silk, 18);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.SetResult(ItemID.IceSkates, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.AddRecipe();
        }

        public override void AddRecipeGroups()
        {
            //RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + (" Wings"), new int[]

            RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Wings"), new int[]
            {
                492,
                493,
                665,
                749,
                761,
                785,
                786,
                821,
                822,
                823,
                948,
                1162,
                1165,
                1515,
                1583,
                1584,
                1585,
                1586,
                1797,
                1830,
                1871,
                2280,
                2494,
                2609,
                2770,
                3468,
                3469,
                3470,
                3471,
                3580,
                3582,
                3588,
                3592,
                ItemID.ArkhalisWings,
                ItemID.LeinforsWings,
                ItemType("VoidWings"),
                ItemType("BubblePack"),
                ItemType("SkylineWings")
            });
            RecipeGroup.RegisterGroup("ElementsAwoken:WingGroup", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Lunar Wings"), new int[]
            {
                3468,
                3469,
                3470,
                3471
            });
            RecipeGroup.RegisterGroup("ElementsAwoken:LunarWings", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Silver or Tungsten Sword"), new int[]
            {
                ItemID.SilverBroadsword,
                ItemID.TungstenBroadsword,
            });
            RecipeGroup.RegisterGroup("ElementsAwoken:SilverSword", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Sand"), new int[]
            {
                ItemID.SandBlock,
                ItemID.CrimsandBlock,
                ItemID.EbonsandBlock,
                ItemID.PearlsandBlock,
            });
            RecipeGroup.RegisterGroup("ElementsAwoken:SandGroup", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Sandstone"), new int[]
            {
                ItemID.Sandstone,
                ItemID.CrimsonSandstone,
                ItemID.CorruptSandstone,
                ItemID.HallowSandstone,
            });
            RecipeGroup.RegisterGroup("ElementsAwoken:SandstoneGroup", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Ice Block"), new int[]
            {
                ItemID.IceBlock,
                ItemID.RedIceBlock,
                ItemID.PurpleIceBlock,
                ItemID.PinkIceBlock,
            });
            RecipeGroup.RegisterGroup("ElementsAwoken:IceGroup", group);

            // bars
            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Copper Bar"), new int[]
            {
                ItemID.CopperBar,
                ItemID.TinBar,
            });
            RecipeGroup.RegisterGroup("ElementsAwoken:CopperBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Silver Bar"), new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar,
            });
            RecipeGroup.RegisterGroup("ElementsAwoken:SilverBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Gold Bar"), new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar,
            });
            RecipeGroup.RegisterGroup("ElementsAwoken:GoldBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Evil Bar"), new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar,
            });
            RecipeGroup.RegisterGroup("ElementsAwoken:EvilBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Evil Ore"), new int[]
            {
                ItemID.DemoniteOre,
                ItemID.CrimtaneOre,
            });
            RecipeGroup.RegisterGroup("ElementsAwoken:EvilOre", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Cobalt Bar"), new int[]
            {
                ItemID.CobaltBar,
                ItemID.PalladiumBar,
            });
            RecipeGroup.RegisterGroup("ElementsAwoken:CobaltBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Mythril Bar"), new int[]
            {
                ItemID.MythrilBar,
                ItemID.OrichalcumBar,
            });
            RecipeGroup.RegisterGroup("ElementsAwoken:MythrilBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Adamantite Bar"), new int[]
            {
                ItemID.AdamantiteBar,
                ItemID.TitaniumBar,
            });
            RecipeGroup.RegisterGroup("ElementsAwoken:AdamantiteBar", group);
        }

        // screenshaders, boss heads, config, loaded mod checks and hotkeys
        public override void Unload()
        {
            instance = null;
            AADeathBall = null;
            insanityTex = null;
            heartGlowTex = null;
            plateauLavaTrans = null;
            plateauLavaTransTile = null;
            lavaTrans = null;
            lavaTransTile = null;
            Main.OnTick -= Update;
            On.Terraria.Main.DrawMenu -= DrawOnMenu;
            On.Terraria.NPC.UpdateCollision -= CollisionNPC;
            On.Terraria.Player.SlopeDownMovement -= SolidNPC;

            if (!Main.dedServ)
            {
                Main.heartTexture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Heart");
                Main.heart2Texture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Heart2");
                Main.manaTexture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Mana");
                Main.rainTexture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Rain");

                Main.tileSpelunker[TileID.LunarOre] = false;

                Main.logoTexture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Logo");
                Main.logo2Texture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Logo2");

                /*TileID.Sets.Platforms = TileID.Sets.Factory.CreateBoolSet(new int[]
                 {
                     19,
                     427,
                     435,
                     436,
                     437,
                     438,
                     439
                 });*/
                TileID.Sets.Platforms[TileID.PlanterBox] = false;

                ResetCloudTexture();
            }
        }

        public override void Load()
        {
            Main.OnTick += Update;
            On.Terraria.Main.DrawMenu += DrawOnMenu;
            On.Terraria.NPC.UpdateCollision += CollisionNPC;
            On.Terraria.Player.SlopeDownMovement += SolidNPC;
            On.Terraria.Player.LookForTileInteractions += CustomGravity;
            On.Terraria.Player.WingMovement += CustomGravity2;

            IL.Terraria.Player.Update += RemoveManaCap;

            DateTime now = DateTime.Today;
            if (now.Day == 1 && now.Month == 4) aprilFools = true;

            calamityEnabled = ModLoader.GetMod("CalamityMod") != null;
            calamityEnabled = ModLoader.GetMod("ThoriumMod") != null;
            bossChecklistEnabled = ModLoader.GetMod("BossChecklist") != null;
            ancientsAwakenedEnabled = ModLoader.GetMod("AncientsAwakened") != null;
            eaMusicEnabled = ModLoader.GetMod("EAMusic") != null;
            eaRetroMusicEnabled = ModLoader.GetMod("EARetroMusic") != null;

            instance = this;

            //HOTKEYS
            specialAbility = RegisterHotKey("Special Ability", "Z");
            questLog = RegisterHotKey("Open/Close Quest Book", "X");
            dash2 = RegisterHotKey("Secondary Dash", "F");
            sandstormA = RegisterHotKey("Sandstorm Ability", "C");
            timeA = RegisterHotKey("Time Ability", "V");
            rainA = RegisterHotKey("Rain Ability", "B");
            wispA = RegisterHotKey("Wisp Ability", "N");

            if (!Main.dedServ)
            {
                Filters.Scene["ElementsAwoken:VoidLeviathanHead"] = new Filter(new VoidLeviathanScreenShaderData("FilterMiniTower").UseColor(1.0f, 0.2f, 0.55f).UseOpacity(0.4f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:Aqueous"] = new Filter(new AqueousScreenShaderData("FilterMiniTower").UseColor(0.4f, 0.7f, 1.0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                SkyManager.Instance["ElementsAwoken:AqueousSky"] = new AqueousSky();
                Filters.Scene["ElementsAwoken:Infernace"] = new Filter(new InfernaceScreenShaderData("FilterMiniTower").UseColor(1f, 0.4f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:Permafrost"] = new Filter(new PermafrostScreenShaderData("FilterMiniTower").UseColor(0.6f, 0.7f, 1.0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:TheGuardianFly"] = new Filter(new TheGuardianScreenShaderData("FilterMiniTower").UseColor(1f, 0.4f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:Volcanox"] = new Filter(new TheGuardianScreenShaderData("FilterMiniTower").UseColor(1f, 0.4f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:Azana"] = new Filter(new TheGuardianScreenShaderData("FilterMiniTower").UseColor(1f, 0.2f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:Ancients"] = new Filter(new TheGuardianScreenShaderData("FilterMiniTower").UseColor(0f, 0.8f, 0.4f).UseOpacity(0.3f), EffectPriority.VeryHigh);

                Filters.Scene["Blizzard"] = new Filter(new BlizzardShaderData("FilterBlizzardForeground").UseColor(1f, 1f, 1f).UseSecondaryColor(0.7f, 0.7f, 1f).UseImage("Images/Misc/noise", 0, null).UseIntensity(0.4f).UseImageScale(new Vector2(3f, 0.75f), 0), EffectPriority.High);
                Overlays.Scene["Blizzard"] = new SimpleOverlay("Images/Misc/noise", new BlizzardShaderData("FilterBlizzardBackground").UseColor(1f, 1f, 1f).UseSecondaryColor(0.7f, 0.7f, 1f).UseImage("Images/Misc/noise", 0, null).UseIntensity(0.4f).UseImageScale(new Vector2(3f, 0.75f), 0), EffectPriority.High, RenderLayers.Landscape);

                Filters.Scene["ElementsAwoken:VoidEvent"] = new Filter(new VoidEventScreenShaderData("FilterMiniTower").UseColor(0.8f, 0.2f, 0.0f).UseOpacity(0.3f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:VoidEventDark"] = new Filter(new VoidEventScreenShaderData("FilterMiniTower").UseColor(0.0f, 0.0f, 0.2f).UseOpacity(0.5f), EffectPriority.VeryHigh); // 0.9 opacity for a very dark spooky effect where u can barely see

                Filters.Scene["ElementsAwoken:Encounter1"] = new Filter(new EncountersScreenShaderData("FilterMiniTower").UseColor(1f, 0.2f, 0.3f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:Encounter2"] = new Filter(new EncountersScreenShaderData("FilterMiniTower").UseColor(0.1f, 0.1f, 0.3f).UseOpacity(0.2f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:Encounter3"] = new Filter(new EncountersScreenShaderData("FilterMiniTower").UseColor(0.0f, 0.0f, 0.2f).UseOpacity(0.5f), EffectPriority.VeryHigh); // 0.9 opacity for a very dark spooky effect where u can barely see

                Filters.Scene["ElementsAwoken:Despair"] = new Filter(new VoidEventScreenShaderData("FilterMiniTower").UseColor(0.2f, 0.0f, 0.3f).UseOpacity(0.4f), EffectPriority.VeryHigh);

                Filters.Scene["ElementsAwoken:Regaroth"] = new Filter(new RegarothScreenShaderData("FilterMiniTower").UseColor(0.2f, 0.4f, 0.7f).UseOpacity(0.3f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:Regaroth2"] = new Filter(new RegarothScreenShaderData("FilterMiniTower").UseColor(0.9f, 0.3f, 0.7f).UseOpacity(0.3f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:RegarothIntense"] = new Filter(new RegarothScreenShaderData("FilterMiniTower").UseColor(0.2f, 0.4f, 0.7f).UseOpacity(0.75f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:Regaroth2Intense"] = new Filter(new RegarothScreenShaderData("FilterMiniTower").UseColor(0.9f, 0.3f, 0.7f).UseOpacity(0.75f), EffectPriority.VeryHigh);

                Filters.Scene["ElementsAwoken:RadiantRain"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.9f, 0.3f, 0.7f).UseOpacity(0.4f), EffectPriority.VeryHigh);

                SkyManager.Instance["ElementsAwoken:InfernacesWrath"] = new InfernacesWrathSky();
                Overlays.Scene["ElementsAwoken:AshParticles"] = new AshOverlay(EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:AshBlizzardEffect"] = new Filter((new BlizzardShaderData("FilterBlizzardForeground")).UseColor(0.2f, 0.2f, 0.2f).UseSecondaryColor(0.05f, 0.05f, 0.05f).UseImage("Images/Misc/noise", 0, null).UseOpacity(0.04f).UseImageScale(new Vector2(3f, 0.75f), 0), EffectPriority.High);
                Filters.Scene["ElementsAwoken:AshShader"] = new Filter(new InfernaceScreenShaderData("FilterMiniTower").UseColor(1f, 0.4f, 0f).UseOpacity(0.2f), EffectPriority.VeryHigh);

                SkyManager.Instance["ElementsAwoken:StarShower"] = new StarShowerSky();

                Filters.Scene["ElementsAwoken:PlateauDistortion"] = new Filter(new ScreenShaderData("FilterHeatDistortion").UseImage("Images/Misc/noise", 0, null).UseIntensity(2f), EffectPriority.Low);
                Filters.Scene["ElementsAwoken:EruptionDistortion"] = new Filter(new ScreenShaderData("FilterHeatDistortion").UseImage("Images/Misc/noise", 0, null).UseIntensity(4f), EffectPriority.Low);
                Filters.Scene["ElementsAwoken:RiftDistortion"] = new Filter(new ScreenShaderData("FilterHeatDistortion").UseImage("Images/Misc/noise", 0, null).UseIntensity(8f), EffectPriority.Low);
                Filters.Scene["ElementsAwoken:PlateauGrey"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.2f, 0.2f, 0.2f).UseOpacity(0.6f), EffectPriority.Low);
                Filters.Scene["ElementsAwoken:plateauLoc.Yellow"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(1f, 1f, 0.3f).UseOpacity(0.45f), EffectPriority.Low);
                Filters.Scene["ElementsAwoken:PlateauGreen"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.7f, 1f, 0.5f).UseOpacity(0.45f), EffectPriority.Low);
                Filters.Scene["ElementsAwoken:PlateauRed"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(1f, 0.5f, 0.5f).UseOpacity(0.45f), EffectPriority.Low);
                Filters.Scene["ElementsAwoken:PlateauPurple"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.3f, 0.1f, 0.4f).UseOpacity(0.6f), EffectPriority.Low);

                Ref<Effect> screenRef = new Ref<Effect>(GetEffect("Effects/ShockwaveEffect"));
                Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
                Filters.Scene["Shockwave"].Load();

                Filters.Scene["ElementsAwoken:InsanityWave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:InsanityWave"].Load();
                Filters.Scene["ElementsAwoken:InsanityWave2"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:InsanityWave2"].Load();
                Overlays.Scene["ElementsAwoken:Eyes"] = new EyesOverlay(EffectPriority.VeryHigh);
                
                Ref<Effect> screenDesatRef = new Ref<Effect>(GetEffect("Effects/DesaturateScreenShader"));
                Filters.Scene["ElementsAwoken:DesaturateScreen"] = new Filter(new ScreenShaderData(screenDesatRef, "DesaturateScreen"), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:DesaturateScreen"].Load();

                Ref<Effect> screenDistortRef = new Ref<Effect>(GetEffect("Effects/DistortShader"));
                Filters.Scene["ElementsAwoken:DistortScreen"] = new Filter(new ScreenShaderData(screenDistortRef, "DistortScreen"), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:DistortScreen"].Load();

                Ref<Effect> screenHueShiftRef = new Ref<Effect>(GetEffect("Effects/HueShiftScreenShader"));
                Filters.Scene["ElementsAwoken:HueShiftScreen"] = new Filter(new ScreenShaderData(screenHueShiftRef, "HueShiftScreen"), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:HueShiftScreen"].Load();

                GameShaders.Misc["ElementsAwoken:SwirlSprite"] = new MiscShaderData(new Ref<Effect>(GetEffect("Effects/SwirlShader")), "SwirlSprite");

                AADeathBall = instance.GetTexture("Projectiles/NPCProj/Ancients/Gores/LightBall");
                PremultiplyTexture(AADeathBall);
                insanityTex = instance.GetTexture("Effects/Insanity");
                PremultiplyTexture(insanityTex);
                heartGlowTex = instance.GetTexture("Extra/HeartGlow");
                PremultiplyTexture(heartGlowTex);
                plateauLavaTrans = instance.GetTexture("Extra/PlateauLavaTrans");
                PremultiplyTexture(plateauLavaTrans);
                plateauLavaTransTile = instance.GetTexture("Extra/PlateauLavaTransTile");
                PremultiplyTexture(plateauLavaTransTile);
                lavaTrans = instance.GetTexture("Extra/LavaTrans");
                PremultiplyTexture(lavaTrans);
                lavaTransTile = instance.GetTexture("Extra/LavaTransTile");
                PremultiplyTexture(lavaTransTile);

                AlchemistUserInterface = new UserInterface();

                PromptUI = new PromptInfoUI();
                PromptUI.Activate();
                PromptInfoUserInterface = new UserInterface();
                PromptInfoUserInterface.SetState(PromptUI);

                BootUI = new BootWingsUI();
                BootUI.Activate();
                BootUserInterface = new UserInterface();
                BootUserInterface.SetState(BootUI);

                QuestUI = new QuestListUI();
                QuestUI.Activate();
                QuestUserInterface = new UserInterface();
                QuestUserInterface.SetState(QuestUI);

                MechUI = new MechCraftingUI();
                MechUI.Activate();
                MechUserInterface = new UserInterface();
                MechUserInterface.SetState(MechUI);

                AcidTapUI = new UI.AcidTapUI();
                AcidTapUI.Activate();
                AcidTapUserInterface = new UserInterface();
                AcidTapUserInterface.SetState(AcidTapUI);

                TileID.Sets.Platforms[TileID.PlanterBox] = true;

                for (int i = 0; i < glowMap.GetLength(1); i++)
                {
                    for (int j = 0; j < glowMap.GetLength(0); j++)
                    {
                        glowMap[i, j] = Main.rand.NextBool();
                    }
                }
            }

            Mod yabhb = ModLoader.GetMod("FKBossHealthBar");
            if (yabhb != null)
            {
                // Set up a normal Small health bar
                Call("RegisterHealthBarMini", ModContent.NPCType<VoidLeviathanOrb>());
                //Call("RegisterHealthBarMini", NPCType("ElderShadeWyrmHead"));
            }

            // go away null
            for (int i = 0; i < screenText.Length; i++)
            {
                if (screenText[i] == null)
                {
                    screenText[i] = "";
                }
            }

            #region instakill immunities

            instakillImmune.Add(NPCID.EyeofCthulhu);
            instakillImmune.Add(NPCID.EaterofWorldsHead);
            instakillImmune.Add(NPCID.EaterofWorldsBody);
            instakillImmune.Add(NPCID.EaterofWorldsTail);
            instakillImmune.Add(NPCID.BrainofCthulhu);
            instakillImmune.Add(NPCID.Creeper);
            instakillImmune.Add(NPCID.SkeletronHead);
            instakillImmune.Add(NPCID.SkeletronHand);
            instakillImmune.Add(NPCID.QueenBee);
            instakillImmune.Add(NPCID.KingSlime);
            instakillImmune.Add(NPCID.WallofFlesh);
            instakillImmune.Add(NPCID.WallofFleshEye);
            instakillImmune.Add(NPCID.TheDestroyer);
            instakillImmune.Add(NPCID.TheDestroyerBody);
            instakillImmune.Add(NPCID.TheDestroyerTail);
            instakillImmune.Add(NPCID.Retinazer);
            instakillImmune.Add(NPCID.Spazmatism);
            instakillImmune.Add(NPCID.SkeletronPrime);
            instakillImmune.Add(NPCID.PrimeCannon);
            instakillImmune.Add(NPCID.PrimeSaw);
            instakillImmune.Add(NPCID.PrimeVice);
            instakillImmune.Add(NPCID.PrimeLaser);
            instakillImmune.Add(NPCID.Plantera);
            instakillImmune.Add(NPCID.PlanterasTentacle);
            instakillImmune.Add(NPCID.Golem);
            instakillImmune.Add(NPCID.GolemHead);
            instakillImmune.Add(NPCID.GolemFistLeft);
            instakillImmune.Add(NPCID.GolemFistRight);
            instakillImmune.Add(NPCID.GolemHeadFree);
            instakillImmune.Add(NPCID.DukeFishron);
            instakillImmune.Add(NPCID.CultistBoss);
            instakillImmune.Add(NPCID.MoonLordHead);
            instakillImmune.Add(NPCID.MoonLordHand);
            instakillImmune.Add(NPCID.MoonLordCore);
            instakillImmune.Add(NPCID.MoonLordFreeEye);
            instakillImmune.Add(NPCID.DungeonGuardian);
            instakillImmune.Add(NPCID.IceGolem);
            instakillImmune.Add(NPCID.WyvernHead);
            instakillImmune.Add(NPCID.WyvernLegs);
            instakillImmune.Add(NPCID.WyvernTail);
            instakillImmune.Add(NPCID.WyvernBody);
            instakillImmune.Add(NPCID.WyvernBody2);
            instakillImmune.Add(NPCID.WyvernBody3);
            instakillImmune.Add(NPCID.Mothron);
            instakillImmune.Add(NPCID.PlanterasHook);
            instakillImmune.Add(NPCID.PlanterasTentacle);
            instakillImmune.Add(NPCID.Paladin);
            instakillImmune.Add(NPCID.HeadlessHorseman);
            instakillImmune.Add(NPCID.MourningWood);
            instakillImmune.Add(NPCID.Pumpking);
            instakillImmune.Add(NPCID.PumpkingBlade);
            instakillImmune.Add(NPCID.Yeti);
            instakillImmune.Add(NPCID.Everscream);
            instakillImmune.Add(NPCID.IceQueen);
            instakillImmune.Add(NPCID.Krampus);
            instakillImmune.Add(NPCID.MartianSaucer);
            instakillImmune.Add(NPCID.MartianSaucerCannon);
            instakillImmune.Add(NPCID.MartianSaucerCore);
            instakillImmune.Add(NPCID.MartianSaucerTurret);
            instakillImmune.Add(NPCID.MoonLordCore);
            instakillImmune.Add(NPCID.LunarTowerVortex);
            instakillImmune.Add(NPCID.LunarTowerStardust);
            instakillImmune.Add(NPCID.LunarTowerSolar);
            instakillImmune.Add(NPCID.LunarTowerNebula);
            instakillImmune.Add(NPCID.CultistArcherBlue);
            instakillImmune.Add(NPCID.CultistArcherWhite);
            instakillImmune.Add(NPCID.CultistDevote);
            instakillImmune.Add(NPCID.CultistDragonBody1);
            instakillImmune.Add(NPCID.CultistDragonBody2);
            instakillImmune.Add(NPCID.CultistDragonBody3);
            instakillImmune.Add(NPCID.CultistDragonBody4);
            instakillImmune.Add(NPCID.CultistDragonHead);
            instakillImmune.Add(NPCID.CultistDragonTail);
            instakillImmune.Add(NPCID.CultistTablet);
            instakillImmune.Add(NPCID.GoblinSummoner);
            instakillImmune.Add(NPCID.BigMimicJungle);
            instakillImmune.Add(NPCID.BigMimicCorruption);
            instakillImmune.Add(NPCID.BigMimicHallow);
            instakillImmune.Add(NPCID.BigMimicCrimson);
            instakillImmune.Add(NPCID.PirateShip);
            instakillImmune.Add(NPCID.PirateShipCannon);
            instakillImmune.Add(NPCID.SandElemental);
            instakillImmune.Add(NPCID.DD2Betsy);
            instakillImmune.Add(NPCID.DD2DarkMageT1);
            instakillImmune.Add(NPCID.DD2DarkMageT3);
            instakillImmune.Add(NPCID.SolarCrawltipedeBody);
            instakillImmune.Add(NPCID.SolarCrawltipedeHead);
            instakillImmune.Add(NPCID.DD2OgreT2);
            instakillImmune.Add(NPCID.DD2OgreT3);
            instakillImmune.Add(NPCID.RainbowSlime);
            instakillImmune.Add(NPCID.PirateCaptain);
            instakillImmune.Add(NPCID.TargetDummy);
            instakillImmune.Add(NPCType("CosmicObserver"));
            instakillImmune.Add(NPCType("ToySlime"));
            instakillImmune.Add(NPCType("AncientWyrmArms"));
            instakillImmune.Add(NPCType("AncientWyrmBody"));
            instakillImmune.Add(NPCType("AncientWyrmHead"));
            instakillImmune.Add(NPCType("AncientWyrmTail"));
            instakillImmune.Add(NPCType("AndromedaHead"));
            instakillImmune.Add(NPCType("AndromedaBody"));
            instakillImmune.Add(NPCType("AndromedaTail"));
            instakillImmune.Add(NPCType("BarrenSoul"));
            instakillImmune.Add(NPCType("Furosia"));
            instakillImmune.Add(NPCType("ObsidiousHand"));
            instakillImmune.Add(NPCType("RegarothHead"));
            instakillImmune.Add(NPCType("RegarothBody"));
            instakillImmune.Add(NPCType("RegarothTail"));
            instakillImmune.Add(NPCType("ShadeWyrmHead"));
            instakillImmune.Add(NPCType("ShadeWyrmBody"));
            instakillImmune.Add(NPCType("ShadeWyrmTail"));
            instakillImmune.Add(NPCType("SolarFragment"));
            instakillImmune.Add(NPCType("SoulOfInfernace"));
            instakillImmune.Add(NPCType("VoidLeviathanHead"));
            instakillImmune.Add(NPCType("VoidLeviathanBody"));
            instakillImmune.Add(NPCType("VoidLeviathanBodyWeak"));
            instakillImmune.Add(NPCType("VoidLeviathanTail"));
            instakillImmune.Add(NPCType("VolcanoxHook"));
            instakillImmune.Add(NPCType("VolcanoxTentacle"));

            #endregion instakill immunities
        }
        public static void Update()
        {
            generalTimer++;

            if (Main.gameMenu)
            {
                //Main.logoTexture = ModContent.GetTexture("ElementsAwoken/Extra/ElementsAwoken");
                Main.logo2Texture = ModContent.GetTexture("ElementsAwoken/Extra/lol");
            }
            else
            {
                if (!Main.mouseLeft) mouseLeftReleasedDuringChat = true;
                else mouseLeftReleasedDuringChat = false;
            }
        }
        public static void DrawOnMenu(On.Terraria.Main.orig_DrawMenu orig, Main self, GameTime gameTime)
        {
            #region extra logo
            /*logoRotation += logoRotationSpeed * 3E-05f;
            if ((double)logoRotation > 0.1)
            {
                logoRotationDirection = -1f;
            }
            else if ((double)logoRotation < -0.1)
            {
                logoRotationDirection = 1f;
            }
            if (logoRotationSpeed < 20f & logoRotationDirection == 1f)
            {
                logoRotationSpeed += 1f;
            }
            else if (logoRotationSpeed > -20f & logoRotationDirection == -1f)
            {
                logoRotationSpeed -= 1f;
            }
            logoScale += logoScaleSpeed * 1E-05f;
            if ((double)logoScale > 1.1)
            {
                logoScaleDirection = -1f;
            }
            else if ((double)logoScale < 0.9)
            {
                logoScaleDirection = 1f;
            }
            if (logoScaleSpeed < 50f & logoScaleDirection == 1f)
            {
                logoScaleSpeed += 1f;
            }
            else if (logoScaleSpeed > -50f & logoScaleDirection == -1f)
            {
                logoScaleSpeed -= 1f;
            }

            Texture2D tex = ModContent.GetTexture("ElementsAwoken/Extra/ElementsAwoken");
            Main.spriteBatch.Draw(tex, new Vector2((float)(Main.screenWidth / 2) + 150, 200f), null, Color.White, logoRotation, tex.Size(), logoScale * 0.5f, SpriteEffects.None, 0);*/
            #endregion

            if (Main.gameMenu)
            {
                Texture2D tex = ModContent.GetTexture("ElementsAwoken/Extra/Discord1");
                Vector2 pos = new Vector2(10, Main.screenHeight - 110);
                Color color = Color.White;
                float scale = 0.75f;
                Rectangle rect = new Rectangle((int)pos.X, (int)pos.Y, (int)(tex.Width * scale), (int)(tex.Height * scale));
                bool hover = false;
                if (rect.Contains(new Point(Main.mouseX, Main.mouseY)))
                {
                    color *= 0.7f;
                    if (Main.mouseLeft && Main.mouseLeftRelease) System.Diagnostics.Process.Start("https://discord.gg/ETJ6NBV");
                    hover = true;
                }
                Main.spriteBatch.Draw(tex, pos, null, new Color(color.R, color.G, color.B, 255), 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                if (hover) ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, "Elements Awoken Discord", new Vector2(Main.mouseX + 17, Main.mouseY + 17), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
            }
            orig(self, gameTime);
        }
        public static void CollisionNPC(On.Terraria.NPC.orig_UpdateCollision orig, NPC self)
        {
            // to stop npcs falling through quicksand
            for (int i = 0; i < Tiles.GlobalTiles.quicksands.Count; i++)
            {
                Main.tileSolid[Tiles.GlobalTiles.quicksands[i]] = true;
            }
  
            orig(self);
        }
        public static void SolidNPC(On.Terraria.Player.orig_SlopeDownMovement orig, Player self)
        {
            // solid npcs
            /*
                if (self.controlDown) self.GetModPlayer<StarlightPlayer>().platformTimer = 5;
                if (self.controlDown || self.GetModPlayer<StarlightPlayer>().platformTimer > 0 || self.GoingDownWithGrapple) { orig(self); return; }
                foreach (NPC npc in Main.npc.Where(n => n.active && n.modNPC != null && n.modNPC is NPCs.MovingPlatform))
                {
                    if (new Rectangle((int)self.position.X, (int)self.position.Y + (self.height), self.width, 1).Intersects
                    (new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, 8 + (self.velocity.Y > 0 ? (int)self.velocity.Y : 0))) && self.position.Y <= npc.position.Y)
                    {
                        if (!self.justJumped && self.velocity.Y >= 0)
                        {
                            self.gfxOffY = npc.gfxOffY;
                            self.velocity.Y = 0;
                            self.fallStart = (int)(self.position.Y / 16f);
                            self.position.Y = npc.position.Y - self.height + 4;
                            orig(self);
                        }
                    }
                }*/

            for (int n = 0; n < Main.maxNPCs; n++)
            {
                NPC npc = Main.npc[n];
                if (npc.active)
                {
                    NPCsGLOBAL modNPC = npc.GetGlobalNPC<NPCsGLOBAL>();

                    /*Rectangle playerRect = self.getRect();
                    Rectangle npcRect = npc.getRect();

                    /if (self.Top.Y > npc.Bottom.Y)
                    {
                        self.position.Y += Rectangle.Intersect(playerRect, npcRect).Height;
                    }
                    else if (self.Bottom.Y > npc.Top.Y)
                    {
                        self.position.Y -= Rectangle.Intersect(playerRect, npcRect).Height;
                    }
                    else if(self.Left.X > npc.Right.X)
                    {
                        self.position.X += Rectangle.Intersect(playerRect, npcRect).Width;
                    }
                    else if (self.Right.X > npc.Left.X)
                    {
                        self.position.X -= Rectangle.Intersect(playerRect, npcRect).Width;
                    }*/
                    bool collideY = false;
                    if (modNPC.platformNPC)
                    {
                        int height = (int)Math.Abs(self.velocity.Y) + 8;
                        if (self.gravDir == 1 && self.GetModPlayer<GravityPlayer>().forceGrav != -1)
                        {
                            Rectangle playerRect = new Rectangle((int)self.position.X, (int)self.Bottom.Y, self.width, 1);
                            Rectangle npcRectTop = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, height);
                            if (playerRect.Intersects(npcRectTop))
                            {
                                if (!self.justJumped && self.velocity.Y >= 0 && !self.GoingDownWithGrapple && !self.controlDown && self.GetModPlayer<MyPlayer>().platformDropTimer <= 0)
                                {
                                    self.position.Y = npc.position.Y - self.height + 4;
                                    if (npc.oldPosition != npc.position) self.position += npc.position - npc.oldPosition;
                                    else self.position += npc.velocity;
                                    self.gfxOffY = npc.gfxOffY;
                                    self.velocity.Y = 0;
                                    self.fallStart = (int)(self.position.Y / 16f);
                                }
                            }
                        }
                        else
                        {
                            Rectangle playerRect = new Rectangle((int)self.position.X, (int)self.position.Y, self.width, 1);
                            Rectangle npcRectTop = new Rectangle((int)npc.position.X, (int)npc.Bottom.Y - height + 4, npc.width, height);
                            /*for (int j = 0; j < 10; j++)
                            {
                                Dust dust = Main.dust[Dust.NewDust(npcRectTop.TopLeft(), npcRectTop.Width, npcRectTop.Height, 75)];
                                dust.velocity = Vector2.Zero;
                                dust.noGravity = true;
                            }*/
                            if (playerRect.Intersects(npcRectTop))
                            {
                                if (!self.justJumped && self.velocity.Y <= 0 && !self.GoingDownWithGrapple && !self.controlDown)
                                {
                                    self.position.Y = npc.Bottom.Y - 4 - npc.gfxOffY;
                                    if (npc.oldPosition != npc.position) self.position += npc.position - npc.oldPosition;
                                    else self.position += npc.velocity;
                                    self.gfxOffY = npc.gfxOffY;
                                    self.velocity.Y = 0;
                                    self.fallStart = (int)(self.position.Y / 16f);
                                    self.GetModPlayer<GravityPlayer>().onPlatform = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (modNPC.solidY)
                        {
                            int height = (int)Math.Abs(self.velocity.Y) + 8;
                            Rectangle playerRectHead = new Rectangle((int)self.position.X, (int)self.position.Y, self.width, 1);
                            Rectangle playerRectFeet = new Rectangle((int)self.position.X, (int)self.Bottom.Y, self.width, 1);
                            Rectangle npcRectTop = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, height);
                            Rectangle npcRectBottom = new Rectangle((int)npc.position.X, (int)npc.Bottom.Y - height, npc.width, height);

                            for (int j = 0; j < 10; j++)
                            {
                                Dust dust = Main.dust[Dust.NewDust(npcRectTop.TopLeft(), npcRectTop.Width, npcRectTop.Height, 75)];
                                dust.velocity = Vector2.Zero;
                                dust.noGravity = true;
                                Dust dust2 = Main.dust[Dust.NewDust(npcRectBottom.TopLeft(), npcRectBottom.Width, npcRectBottom.Height, 135)];
                                dust2.velocity = Vector2.Zero;
                                dust2.noGravity = true;
                            }

                            if (playerRectFeet.Intersects(npcRectTop))
                            {
                                if (self.velocity.Y >= 0 && !self.GoingDownWithGrapple)
                                {
                                    self.position.Y = npc.position.Y - self.height + 4;
                                    self.position += npc.position - npc.oldPosition;
                                    self.gfxOffY = npc.gfxOffY;
                                    self.velocity.Y = 0;
                                    self.fallStart = (int)(self.position.Y / 16f);
                                    collideY = true;
                                }
                            }
                            if (playerRectHead.Intersects(npcRectBottom))
                            {
                                if (!self.GoingDownWithGrapple)
                                {
                                    self.position.Y = npc.Bottom.Y;
                                    self.gfxOffY = npc.gfxOffY;
                                    self.velocity.Y = 0;
                                    collideY = true;
                                }
                            }
                        }
                        if (modNPC.solidX && !collideY)
                        {
                            int width = (int)Math.Abs(self.velocity.X) + 16;
                            Rectangle playerRect = new Rectangle((int)self.position.X, (int)self.position.Y, self.width, self.height);
                            Rectangle npcRectLeft = new Rectangle((int)npc.position.X + (int)npc.velocity.X, (int)npc.position.Y + 4, width, npc.height - 4);
                            Rectangle npcRectRight = new Rectangle((int)npc.Right.X + (int)npc.velocity.X - width, (int)npc.position.Y + 4, width, npc.height - 4);

                            /*for (int j = 0; j < 10; j++)
                            {
                                Dust dust = Main.dust[Dust.NewDust(npcRectLeft.TopLeft(), npcRectLeft.Width, npcRectLeft.Height, DustID.PinkFlame)];
                                dust.velocity = Vector2.Zero;
                                dust.noGravity = true;
                                Dust dust2 = Main.dust[Dust.NewDust(npcRectRight.TopLeft(), npcRectRight.Width, npcRectRight.Height, 6)];
                                dust2.velocity = Vector2.Zero;
                                dust2.noGravity = true;
                            }*/

                            if (playerRect.Intersects(npcRectLeft))
                            {
                                //if (self.velocity.X >= 0 || npc.velocity.X > self.velocity.X)
                                {
                                    if (self.velocity.X >= 0) self.velocity.X = 0;
                                    self.position.X = npc.position.X - self.width;
                                    self.position.X += npc.position.X - npc.oldPosition.X;
                                    self.dashDelay = 0;
                                    self.dashTime = 0;
                                    self.GetModPlayer<MyPlayer>().eaDashDelay = 0;
                                    self.GetModPlayer<MyPlayer>().eaDashTime = 0;
                                }
                            }
                            if (playerRect.Intersects(npcRectRight))
                            {
                                //if (self.velocity.X <= 0 || npc.velocity.X < self.velocity.X)
                                {
                                    if (self.velocity.X <= 0) self.velocity.X = 0;
                                    self.position.X = npc.Right.X;
                                    self.position.X += npc.position.X - npc.oldPosition.X;
                                    self.dashDelay = 0;
                                    self.dashTime = 0;
                                    self.GetModPlayer<MyPlayer>().eaDashDelay = 0;
                                    self.GetModPlayer<MyPlayer>().eaDashTime = 0;
                                }
                            }
                        }
                    }
                }
            }

            orig(self);
        }
        public static void CustomGravity(On.Terraria.Player.orig_LookForTileInteractions orig, Player self)
        {
            if (self.GetModPlayer<GravityPlayer>().forceGrav == -1)
            {
                self.gravDir = 1;
                self.wingTime = self.GetModPlayer<GravityPlayer>().gravWingTime;
            }
            orig(self);
        }
        public static void CustomGravity2(On.Terraria.Player.orig_WingMovement orig, Player self)
        {
            if (self.GetModPlayer<GravityPlayer>().forceGrav == -1)
            {
                self.gravDir = -1;
            }
            orig(self);
        }
        public static void DebugModeText(object text, int r = 255, int g = 255, int b = 255)
        {
            Color color = new Color(r, g, b);
            if (ModContent.GetInstance<Config>().debugMode)
            {
                Main.NewText("DEBUG: " + text, color);
            }
        }

        public static void DebugModeNetworkText(string text, int r = 255, int g = 255, int b = 255)
        {
            Color color = new Color(r, g, b);
            if (ModContent.GetInstance<Config>().debugMode)
            {
                NetworkText f = NetworkText.FromLiteral(text);
                NetMessage.BroadcastChatMessage(f, color);
            }
        }

        public static void DebugModeCombatText(string text, Rectangle rect, int r = 255, int g = 255, int b = 255)
        {
            Color color = new Color(r, g, b);
            if (ModContent.GetInstance<Config>().debugMode)
            {
                CombatText.NewText(rect, color, text, true, false);
            }
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {

            Mod mod = ModLoader.GetMod("ElementsAwoken");
            Player player = Main.player[Main.myPlayer];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            AwakenedPlayer awakenedPlayer = player.GetModPlayer<AwakenedPlayer>();

            if (blackCloudsEnabled) ResetCloudTexture();
            if (!Main.gameMenu)
            {
                var inventoryLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));

                #region vanilla texture changes
                if (modPlayer.zonePlateau && Main.hardMode)
                {
                    if ((player.lavaWet && modPlayer.drakoniteGoggles == 1) || modPlayer.drakoniteGoggles == 2)
                    {
                        if (currentLavaTex != 3)
                        {
                            LiquidRenderer.Instance._liquidTextures[1] = plateauLavaTrans;
                            Main.liquidTexture[1] = plateauLavaTransTile;
                            currentLavaTex = 3;
                        }
                    }
                    else if (currentLavaTex != 2)
                    {
                        LiquidRenderer.Instance._liquidTextures[1] = mod.GetTexture("Extra/PlateauLava");
                        Main.liquidTexture[1] = mod.GetTexture("Extra/PlateauLava1");
                        currentLavaTex = 2;
                    }
                }
                else
                {
                    if ((player.lavaWet && modPlayer.drakoniteGoggles == 1) || modPlayer.drakoniteGoggles == 2)
                    {
                        if (currentLavaTex != 1)
                        {
                            LiquidRenderer.Instance._liquidTextures[1] = lavaTrans;
                            Main.liquidTexture[1] = lavaTransTile;
                            currentLavaTex = 1;
                        }
                    }
                    else if (currentLavaTex != 0)
                    {
                        LiquidRenderer.Instance._liquidTextures[1] = TextureManager.Load("Images/Misc/water_1");
                        Main.liquidTexture[1] = TextureManager.Load("Images/Liquid_1");
                        currentLavaTex = 0;
                    }
                }
                // rain texture
                if (encounter == 3)
                {
                    if (currentRainTex != 3)
                    {
                        Main.rainTexture = GetTexture("Extra/Rain3");
                        currentRainTex = 3;
                    }
                }
                else if (MyWorld.radiantRain)
                {
                    if (currentRainTex != 4)
                    {
                        Main.rainTexture = GetTexture("Extra/Rain4");
                        currentRainTex = 4;
                    }
                }
                else if (currentRainTex != 0)
                {
                    Main.rainTexture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Rain");
                    currentRainTex = 0;
                }
                // infernace clouds
                if (MyWorld.firePrompt > bossPromptDelay && !blackCloudsEnabled) BlackCloudTexture();
                else if (blackCloudsEnabled) ResetCloudTexture();
                // hearts
                if (!calamityEnabled)
                {
                    // to stop hearts being underneath
                    if (modPlayer.voidHeartsUsed == 10)
                    {
                        if (currentHeartTex == 0)
                        {
                            Main.heart2Texture = GetTexture("Extra/Blank");
                            currentHeartTex = 1;
                        }
                    }
                    else if (currentHeartTex == 1)
                    {
                        Main.heart2Texture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Heart2");
                        currentHeartTex = 0;
                    }
                    if (modPlayer.lunarStarsUsed == 1)
                    {
                        if (currentManaTex == 0)
                        {
                            Main.manaTexture = GetTexture("Extra/Mana2");
                            currentManaTex = 1;
                        }
                    }
                    else if (currentManaTex == 1)
                    {
                        Main.manaTexture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Mana");
                        currentManaTex = 0;
                    }
                }
                #endregion  

                if (modPlayer.creditsTimer >= 0)
                {
                    layers.Clear();

                    var creditsState = new LegacyGameInterfaceLayer("ElementsAwoken: Credits",
                        delegate
                        {
                            Credits();
                            return true;
                        },
                        InterfaceScaleType.UI);
                    layers.Insert(0, creditsState); // because all layers are deleted, 0 is the next one
                }
                else if (!player.ghost)
                {
                    // computer
                    if (modPlayer.inComputer)
                    {
                        var computerState = new LegacyGameInterfaceLayer("ElementsAwoken: UI",
                            delegate
                            {
                                DrawMethods.DrawComputer(Main.spriteBatch);
                                return true;
                            },
                            InterfaceScaleType.UI);
                        layers.Insert(inventoryLayer, computerState);
                    }


                    var criticalHeat = new LegacyGameInterfaceLayer("ElementsAwoken: Critical Heat",
                        delegate
                        {
                            if (modPlayer.criticalHeatTimer != 0) DrawMethods.DrawCriticalHeatBar();
                            if (modPlayer.fireResistance != 0) DrawMethods.DrawFireResistance();
                            return true;
                        },
                        InterfaceScaleType.UI);
                    layers.Insert(inventoryLayer, criticalHeat);

                    // text
                    var text1State = new LegacyGameInterfaceLayer("ElementsAwoken: UI",
                            delegate
                            {
                                if (encounter != 0) DrawMethods.DrawEncounterText(Main.spriteBatch);
                                if (modPlayer.aboveHeadTimer > 0) DrawMethods.DrawAboveHeadText();
                                if (modPlayer.tomeUI) DrawMethods.DrawLorekeepersUI();
                                if (player.talkNPC != -1) DrawMethods.AvailableQuestText();
                                return true;
                            },
                            InterfaceScaleType.UI);
                    layers.Insert(inventoryLayer, text1State);

                    // The Orchard
                    if (player.GetModPlayer<TheOrchardPlayer>().inGame)
                    {
                        var orchardState = new LegacyGameInterfaceLayer("ElementsAwoken: UI",
                            delegate
                            {
                                TheOrchard.DrawOrchardGame();
                                return true;
                            },
                            InterfaceScaleType.UI);
                        layers.Insert(inventoryLayer, orchardState);
                    }
                    //Voidblood glow
                    if (modPlayer.voidBlood)
                    {
                        var heartState = new LegacyGameInterfaceLayer("ElementsAwoken: UI2",
                            delegate
                            {
                                DrawMethods.DrawVoidBloodGlow();
                                return true;
                            },
                            InterfaceScaleType.UI);
                        layers.Insert(inventoryLayer, heartState);
                    }
                    if (modPlayer.sulphurBreath > 0)
                    {
                        var sulphurState = new LegacyGameInterfaceLayer("ElementsAwoken: UI2",
                            delegate
                            {
                                DrawMethods.DrawSulphurBreath();
                                return true;
                            },
                            InterfaceScaleType.UI);
                        layers.Insert(inventoryLayer, sulphurState);
                    }
                    // hearts & mana
                    if (!calamityEnabled)
                    {
                        var heartState = new LegacyGameInterfaceLayer("ElementsAwoken: UI2",
                            delegate
                            {
                                DrawMethods.DrawHearts();
                                return true;
                            },
                            InterfaceScaleType.UI);
                        layers.Insert(inventoryLayer, heartState);
                    }

                    // wisp form
                    if (modPlayer.wispForm)
                    {
                        player.statManaMax2 = 0; // removes mana bar
                        layers.Insert(inventoryLayer, new LegacyGameInterfaceLayer("ElementsAwoken: UI2",
                                delegate
                                {
                                    DrawMethods.DrawSpirit();
                                    return true;
                                },
                                InterfaceScaleType.UI));
                    }

                    #region shield hearts

                    var shieldState = new LegacyGameInterfaceLayer("ElementsAwoken: UI2",
                        delegate
                        {
                            DrawMethods.DrawShield();
                            return true;
                        },
                        InterfaceScaleType.UI);
                    layers.Insert(inventoryLayer, shieldState);

                    #endregion shield hearts

                    #region energy & insanity UI

                    var BarState = new LegacyGameInterfaceLayer("ElementsAwoken: UI",
                        delegate
                        {
                            if (ModContent.GetInstance<Config>().resourceBars)
                            {
                                DrawMethods.DrawEnergyBar();
                                DrawMethods.DrawInsanityBar();
                            }
                            else
                            {
                                DrawMethods.DrawEnergyUI();
                                DrawMethods.DrawInsanityUI();
                            }
                            return true;
                        },
                        InterfaceScaleType.UI);
                    layers.Insert(inventoryLayer, BarState);

                    var insanityOverlayState = new LegacyGameInterfaceLayer("ElementsAwoken: Interface Logic 1",
                        delegate
                        {
                            DrawMethods.DrawInsanityOverlay();
                            return true;
                        },
                        InterfaceScaleType.UI);
                    layers.Insert(inventoryLayer, insanityOverlayState);

                    #endregion energy & insanity UI

                    // sanity book
                    if (awakenedPlayer.openSanityBook)
                    {
                        var bookState = new LegacyGameInterfaceLayer("ElementsAwoken: UI",
                            delegate
                            {
                                DrawMethods.DrawSanityBook();
                                return true;
                            },
                            InterfaceScaleType.UI);
                        layers.Insert(inventoryLayer, bookState);
                    }

                    #region info accessories

                    var infoLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
                    var infoState = new LegacyGameInterfaceLayer("ElementsAwoken: UI",
                        delegate
                        {
                            DrawMethods.DrawInfoAccs();
                            return true;
                        },
                        InterfaceScaleType.UI);
                    layers.Insert(infoLayer, infoState);

                    #endregion info accessories

                    if (player.HeldItem.type == ModContent.ItemType<Railgun>())
                    {
                        var heatBarState = new LegacyGameInterfaceLayer("ElementsAwoken: UI2",
                            delegate
                            {
                                DrawMethods.DrawHeatBar(player.HeldItem);
                                return true;
                            },
                            InterfaceScaleType.UI);
                        layers.Insert(inventoryLayer, heatBarState);
                    }
                }

                var textState = new LegacyGameInterfaceLayer("ElementsAwoken: Text",
                    delegate
                    {
                        for (int i = 0; i < screenText.Length - 1; i++)
                        {
                            if (screenText[i] != "" && screenText[i] != null && Main.hasFocus)
                            {
                                screenTextTimer[i]++;
                                if (screenTextTimer[i] < screenTextDuration[i] / 8) screenTextAlpha[i] += 1f / ((float)screenTextDuration[i] / 8f);// 1/4 of the time fading
                                else if (screenTextTimer[i] > screenTextDuration[i] - (screenTextDuration[i] / 8)) screenTextAlpha[i] -= 1f / ((float)screenTextDuration[i] / 8f);
                                else screenTextAlpha[i] = 1;
                                if (screenTextTimer[i] > screenTextDuration[i]) screenText[i] = "";

                                DrawMethods.DrawStringOutlined(Main.spriteBatch, Main.fontDeathText, screenText[i], screenTextPos[i], Color.White * screenTextAlpha[i], screenTextScale[i]);
                            }
                        }
                        return true;
                    },
                    InterfaceScaleType.UI);
                if (modPlayer.creditsTimer < 0) layers.Insert(inventoryLayer, textState);
                else layers.Insert(1, textState); // in credits all layers r deleted

                if (modPlayer.screenTransition)
                {
                    var transLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Interface Logic 4"));
                    var transState = new LegacyGameInterfaceLayer("ElementsAwoken: Transitions",
                        delegate
                        {
                            DrawMethods.BlackScreenTrans();
                            return true;
                        },
                        InterfaceScaleType.UI);
                    if (modPlayer.creditsTimer < 0) layers.Insert(transLayer, transState);
                    else layers.Insert(1, transState); // in credits all layers r deleted
                }

                if (inventoryLayer != -1 && inventoryLayer <= layers.Count())
                {
                    layers.Insert(inventoryLayer, new LegacyGameInterfaceLayer(
                        "ElementsAwoken: Alchemist UI",
                        delegate
                        {
                            AlchemistUserInterface.Draw(Main.spriteBatch, new GameTime());
                            if (PromptInfoUI.Visible) PromptInfoUserInterface.Draw(Main.spriteBatch, new GameTime());
                            if (BootWingsUI.Visible) BootUserInterface.Draw(Main.spriteBatch, new GameTime());
                            if (QuestListUI.Visible) QuestUserInterface.Draw(Main.spriteBatch, new GameTime());
                            if (MechCraftingUI.Visible) MechUserInterface.Draw(Main.spriteBatch, new GameTime());
                            if (UI.AcidTapUI.Visible) AcidTapUserInterface.Draw(Main.spriteBatch, new GameTime());
                            return true;
                        },
                        InterfaceScaleType.UI)
                    );
                }
                if (modPlayer.cantSeeHoverText)
                {
                    int test = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Over"));
                    layers.RemoveAt(test);
                }
            }
        }
        public override void PostDrawFullscreenMap(ref string mouseText)
        {
            foreach (Quest k in QuestWorld.activeQuests)
            {
                 k.DrawOnMap();
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {

            AlchemistUserInterface?.Update(gameTime);
            if (PromptInfoUI.Visible) PromptInfoUserInterface?.Update(gameTime);
            if (BootWingsUI.Visible) BootUserInterface?.Update(gameTime);
            if (QuestListUI.Visible) QuestUserInterface?.Update(gameTime);
            if (MechCraftingUI.Visible) MechUserInterface?.Update(gameTime);
            if (AcidTapUI.Visible) AcidTapUserInterface?.Update(gameTime);
        }
        public override void MidUpdateTimeWorld()
        {
            usingScrollbar = false;

            for (int i = 0; i < Tiles.GlobalTiles.liquidBlockers.Count; i++)
            {
                Main.tileSolid[Tiles.GlobalTiles.liquidBlockers[i]] = true;
            }
            Player player = Main.player[Main.myPlayer];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            //Main.NewText(encounter + " " + encounterTimer + " " + encounterSetup);
            if (!Main.gameMenu)
            {
                if (encounter != 0)
                {
                    encounterTimer--;
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
                        if (encounter == 3) intensity += 0.3f;
                        if (modPlayer.finalText) intensity = 1f;
                        //Main.NewText("" + intensity);
                        //MyWorld.MoonlordShake(intensity, player);
                    }
                }
            }
        }
        public override void MidUpdateInvasionNet()
        {
            // return quicksand to normal. doing it here also allows it to block water
            for (int i = 0; i < Tiles.GlobalTiles.quicksands.Count; i++)
            {
                Main.tileSolid[Tiles.GlobalTiles.quicksands[i]] = false;
            }
            for (int i = 0; i < Tiles.GlobalTiles.liquidBlockers.Count; i++)
            {
                Main.tileSolid[Tiles.GlobalTiles.liquidBlockers[i]] = false;
            }
        }
        private static void ResetCloudTexture()
        {
            if (Main.netMode == NetmodeID.Server) return;
            for (int num25 = 0; num25 < 22; num25++)
            {
                Main.cloudTexture[num25] = Main.instance.OurLoad<Texture2D>(string.Concat(new object[]
                {
                    "Images",
                    Path.DirectorySeparatorChar.ToString(),
                    "Cloud_",
                    num25
                }));
            }
            blackCloudsEnabled = false;
        }

        private static void BlackCloudTexture()
        {
            if (Main.netMode == NetmodeID.Server) return;
            for (int cloud = 0; cloud < 22; cloud++)
            {
                Main.cloudTexture[cloud] = ModContent.GetTexture(string.Concat(new object[]
                {
                    "ElementsAwoken/Extra/Clouds/Cloud_",
                    cloud
                }));
            }
            blackCloudsEnabled = true;
        }

        public static int CountAvailableInfo()
        {
            Player player = Main.player[Main.myPlayer];
            int num = 0;
            if (!player.hideInfo[0] && player.accWatch > 0)
            {
                num++;
            }
            if (!player.hideInfo[1] && player.accWeatherRadio)
            {
                num++;
            }
            if (!player.hideInfo[2] && player.accFishFinder)
            {
                num++;
            }
            if (!player.hideInfo[3] && player.accCompass > 0)
            {
                num++;
            }
            if (!player.hideInfo[4] && player.accDepthMeter > 0)
            {
                num++;
            }
            if (!player.hideInfo[5] && player.accThirdEye)
            {
                num++;
            }
            if (!player.hideInfo[6] && player.accJarOfSouls)
            {
                num++;
            }
            if (!player.hideInfo[7] && player.accCalendar)
            {
                num++;
            }
            // where tf is 8?
            if (!player.hideInfo[9] && player.accStopwatch)
            {
                num++;
            }
            if (!player.hideInfo[10] && player.accOreFinder)
            {
                num++;
            }
            if (!player.hideInfo[11] && player.accCritterGuide)
            {
                num++;
            }
            if (!player.hideInfo[12] && player.accDreamCatcher)
            {
                num++;
            }
            return num;
        }

        public static int CountEquippedInfo()
        {
            Player player = Main.player[Main.myPlayer];
            int num = 0;
            if (player.accWatch > 0)
            {
                num++;
            }
            if (player.accWeatherRadio)
            {
                num++;
            }
            if (player.accFishFinder)
            {
                num++;
            }
            if (player.accCompass > 0)
            {
                num++;
            }
            if (player.accDepthMeter > 0)
            {
                num++;
            }
            if (player.accThirdEye)
            {
                num++;
            }
            if (player.accJarOfSouls)
            {
                num++;
            }
            if (player.accCalendar)
            {
                num++;
            }
            if (player.accStopwatch)
            {
                num++;
            }
            if (player.accOreFinder)
            {
                num++;
            }
            if (player.accCritterGuide)
            {
                num++;
            }
            if (player.accDreamCatcher)
            {
                num++;
            }
            return num;
        }

        public void Credits()
        {
            Vector2 monitorScale = new Vector2((float)Main.screenWidth / 1920f, (float)Main.screenHeight / 1080f);
            var mod = ModLoader.GetMod("ElementsAwoken");
            var player = Main.player[Main.myPlayer].GetModPlayer<MyPlayer>();
            if (player.creditsTimer > player.screenDuration * 2) DrawMethods.DrawStringOutlined(Main.spriteBatch, Main.fontDeathText, "Hold 'Escape' to skip", new Vector2(Main.screenWidth - 220 * monitorScale.X, Main.screenHeight - 35 * monitorScale.Y), Color.White * (player.escHeldTimer > 0 ? 1 : 0.4f), 0.5f * monitorScale.Y);
            if (player.creditsTimer > player.screenTransDuration / 2 && ModContent.GetInstance<Config>().debugMode)
            {
                int screenNum = (int)Math.Floor((decimal)(player.creditsTimer / player.screenDuration));

               if (player.creditPoints.Count > 0) DrawMethods.DrawStringOutlined(Main.spriteBatch, Main.fontDeathText, "DEBUG: " + player.creditPoints[screenNum].name, new Vector2(10 * monitorScale.X, Main.screenHeight - 35 * monitorScale.Y), Color.White * 0.5f, 0.5f * monitorScale.Y);

            }
            if (player.creditsTimer > 180 && player.creditsTimer < 480)
            {
                var logo = GetTexture("Extra/ElementsAwoken");
                float scale = 1.4f * monitorScale.Y;
                Color color = Color.White * GetFadeAlpha(player.creditsTimer - 180, 300); // old: (float)Math.Sin(MathHelper.Lerp(0, (float)Math.PI, ((float)player.creditsTimer - 300f) / 180f))
                Main.spriteBatch.Draw(logo, new Vector2(Main.screenWidth / 2 - ((logo.Width * scale) / 2), Main.screenHeight / 2 - 200 - ((logo.Height * scale) / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
            int dist = 60;

            int slideMusic = 2;
            int slideLore = 3;
            int slideSilva = 4;
            int slideMoon = 5;
            int slideMayhemm = 6;
            int slideSpriters = 7;
            int slideDonators = 8;
            int slideHelpers = 9;
            int slideSpecial = 10;
            int slideThanks = 11;

            int statueDuration = player.screenDuration - 60 * 3;
            int statueOffset = (player.screenDuration - statueDuration) / 2;

            if (player.creditsTimer >= player.screenDuration + 60)
            {
                if (player.creditsTimer == player.screenDuration + 60)
                {
                    string text = "Created by ThatOneJuicyOrange_";
                    float scale = 1.3f * monitorScale.Y;
                    Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 300 * monitorScale.Y);
                    DrawScreenText(text, 7 * 60, scale, pos);
                }

                if (player.creditsTimer > player.screenDuration + statueOffset && player.creditsTimer < player.screenDuration * 2 - statueOffset)
                {
                    var statue = GetTexture("Extra/Credits/ThatOneJuicyOrangeStatue");
                    float scale = 1.5f * monitorScale.Y;
                    Color color = Color.White * GetFadeAlpha(player.creditsTimer - player.screenDuration - statueOffset, statueDuration);
                    Main.spriteBatch.Draw(statue, new Vector2(Main.screenWidth - 200 * monitorScale.X - (player.creditsTimer - player.screenDuration) / 2 - ((statue.Width * scale) / 2), Main.screenHeight / 2 - ((statue.Height * scale) / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                }
            }
            if (player.creditsTimer >= player.screenDuration * slideMusic + 60)
            {
                if (player.creditsTimer == player.screenDuration * slideMusic + 60) CreateCreditsText("Music by", monitorScale, 330, 1.3f, 7 * 60);
                if (player.creditsTimer == player.screenDuration * slideMusic + 90) CreateCreditsText("Ranipla", monitorScale, 220, 1f, 7 * 60 - 60);
                if (player.creditsTimer == player.screenDuration * slideMusic + 120) CreateCreditsText("GENIH WAT", monitorScale, 220 - dist, 1f, 7 * 60 - 90);
                if (player.creditsTimer == player.screenDuration * slideMusic + 150) CreateCreditsText("Universe", monitorScale, 220 - dist * 2, 1f, 7 * 60 - 120);
                if (player.creditsTimer > player.screenDuration * slideMusic + statueOffset && player.creditsTimer < player.screenDuration * (slideMusic + 1) - statueOffset)
                {
                    var statue = GetTexture("Extra/Credits/RaniplaStatue");
                    var statue2 = GetTexture("Extra/Credits/GenihWatStatue");
                    float scale = 1.5f * monitorScale.Y;
                    Color color = Color.White * GetFadeAlpha(player.creditsTimer - player.screenDuration * 2 - statueOffset, statueDuration);
                    Main.spriteBatch.Draw(statue, new Vector2(Main.screenWidth - 200 * monitorScale.X - (player.creditsTimer - player.screenDuration * slideMusic) / 2 - ((statue.Width * scale) / 2), Main.screenHeight / 2 - ((statue.Height * scale) / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(statue2, new Vector2(200 * monitorScale.X + (player.creditsTimer - player.screenDuration * slideMusic) / 2 - ((statue2.Width * scale) / 2), Main.screenHeight / 2 - ((statue2.Height * scale) / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.FlipHorizontally, 0f);
                }
            }
            if (player.creditsTimer >= player.screenDuration * slideLore + 60)
            {
                if (player.creditsTimer == player.screenDuration * slideLore + 60) CreateCreditsText("Lore by", monitorScale, 330, 1.3f, 7 * 60);
                if (player.creditsTimer == player.screenDuration * slideLore + 90) CreateCreditsText("Burst", monitorScale, 220, 1f, 7 * 60 - 60);
                if (player.creditsTimer == player.screenDuration * slideLore + 120) CreateCreditsText("Amadis", monitorScale, 220 - dist, 1f, 7 * 60 - 90);
                if (player.creditsTimer > player.screenDuration * slideLore + statueOffset && player.creditsTimer < player.screenDuration * (slideLore + 1) - statueOffset)
                {
                    var statue = GetTexture("Extra/Credits/BurstStatue");
                    var statue2 = GetTexture("Extra/Credits/AmadisStatue");
                    float scale = 1.5f * monitorScale.Y;
                    Color color = Color.White * GetFadeAlpha(player.creditsTimer - player.screenDuration * 3 - statueOffset, statueDuration);
                    Main.spriteBatch.Draw(statue, new Vector2(Main.screenWidth - 200 * monitorScale.X - (player.creditsTimer - player.screenDuration * slideLore) / 2 - ((statue.Width * scale) / 2), Main.screenHeight / 2 - ((statue.Height * scale) / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(statue2, new Vector2(200 * monitorScale.X + (player.creditsTimer - player.screenDuration * slideLore) / 2 - ((statue2.Width * scale) / 2), Main.screenHeight / 2 - ((statue2.Height * scale) / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.FlipHorizontally, 0f);
                }
            }
            if (player.creditsTimer >= player.screenDuration * slideSilva + 60)
            {
                if (player.creditsTimer == player.screenDuration * slideSilva + 60) CreateCreditsText("Sprites by", monitorScale, 330, 1.3f, 7 * 60);
                if (player.creditsTimer == player.screenDuration * slideSilva + 90) CreateCreditsText("Silvestre", monitorScale, 220, 1f, 7 * 60 - 60);
                if (player.creditsTimer > player.screenDuration * slideSilva + statueOffset && player.creditsTimer < player.screenDuration * (slideSilva + 1) - statueOffset)
                {
                    var statue = GetTexture("Extra/Credits/Azana");
                    float scale = 1.2f * monitorScale.Y;
                    Color color = Color.White * GetFadeAlpha(player.creditsTimer - player.screenDuration * 4 - statueOffset, statueDuration);
                    Main.spriteBatch.Draw(statue, new Vector2(200 * monitorScale.X + (player.creditsTimer - player.screenDuration * slideSilva) / 2, Main.screenHeight / 2 - ((statue.Height * scale) / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.FlipHorizontally, 0f);
                }
            }
            if (player.creditsTimer >= player.screenDuration * slideMoon + 60)
            {
                if (player.creditsTimer == player.screenDuration * slideMoon + 60) CreateCreditsText("Sprites by", monitorScale, 330, 1.3f, 7 * 60);
                if (player.creditsTimer == player.screenDuration * slideMoon + 90) CreateCreditsText("Moonburn", monitorScale, 220, 1f, 7 * 60 - 60);
                if (player.creditsTimer > player.screenDuration * slideMoon + statueOffset && player.creditsTimer < player.screenDuration * (slideMoon + 1) - statueOffset)
                {
                    float scale = 1.5f * monitorScale.Y;
                    int offset = 30;
                    Main.spriteBatch.Draw(GetTexture("Extra/Credits/Moonburn0"), new Vector2(600 * monitorScale.X + (player.creditsTimer - player.screenDuration * slideMoon) / 3, Main.screenHeight / 2 + 100 * monitorScale.Y), null, Color.White * GetFadeAlpha(player.creditsTimer - player.screenDuration * slideMoon - statueOffset, statueDuration), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(GetTexture("Extra/Credits/Moonburn1"), new Vector2(Main.screenWidth - 600 * monitorScale.X - (player.creditsTimer - player.screenDuration * slideMoon) / 3, Main.screenHeight / 2 - 200 * monitorScale.Y), null, Color.White * GetFadeAlpha(player.creditsTimer - offset - player.screenDuration * slideMoon - statueOffset, statueDuration - offset), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(GetTexture("Extra/Credits/Moonburn2"), new Vector2(150 * monitorScale.X + (player.creditsTimer - player.screenDuration * slideMoon) / 3, Main.screenHeight / 2 - 200), null, Color.White * GetFadeAlpha(player.creditsTimer - offset * 2 - player.screenDuration * slideMoon - statueOffset, statueDuration - offset * 2), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(GetTexture("Extra/Credits/Moonburn3"), new Vector2(Main.screenWidth - 900 * monitorScale.X - (player.creditsTimer - player.screenDuration * slideMoon) / 3, Main.screenHeight / 2 + 300), null, Color.White * GetFadeAlpha(player.creditsTimer - offset * 3 - player.screenDuration * slideMoon - statueOffset, statueDuration - offset * 2), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    /*Main.spriteBatch.Draw(GetTexture("Extra/Credits/Shimmerspark"), new Vector2(200 * monitorScale.X + (player.creditsTimer - player.screenDuration * 5) / 3, Main.screenHeight / 2 + 200 * monitorScale.Y), null, Color.White * GetFadeAlpha(player.creditsTimer - player.screenDuration * 5 - statueOffset, statueDuration), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(GetTexture("Extra/Credits/SolarGeneratorIV"), new Vector2(Main.screenWidth - 500 * monitorScale.X - (player.creditsTimer - player.screenDuration * 5) / 3, Main.screenHeight / 2 - 200 * monitorScale.Y), null, Color.White * GetFadeAlpha(player.creditsTimer - offset - player.screenDuration * 5 - statueOffset, statueDuration - offset), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(GetTexture("Extra/Credits/DesertGun"), new Vector2(500 * monitorScale.X + (player.creditsTimer - player.screenDuration * 5) / 3, Main.screenHeight / 2), null, Color.White * GetFadeAlpha(player.creditsTimer - offset * 2 - player.screenDuration * 5 - statueOffset, statueDuration - offset * 2), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(GetTexture("Extra/Credits/AncientsSword"), new Vector2(Main.screenWidth - 400 * monitorScale.X - (player.creditsTimer - player.screenDuration * 5) / 3, Main.screenHeight / 2 + 100 * monitorScale.Y), null, Color.White * GetFadeAlpha(player.creditsTimer - offset * 3 - player.screenDuration * 5 - statueOffset, statueDuration - offset * 3), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(GetTexture("Extra/Credits/BurnerGenerator"), new Vector2(900 * monitorScale.X + (player.creditsTimer - player.screenDuration * 5) / 3, Main.screenHeight / 2 + 300 * monitorScale.Y), null, Color.White * GetFadeAlpha(player.creditsTimer - offset * 4 - player.screenDuration * 5 - statueOffset, statueDuration - offset * 4), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(GetTexture("Extra/Credits/DesertTrailers"), new Vector2(150 * monitorScale.X + (player.creditsTimer - player.screenDuration * 5) / 2, 100 * monitorScale.Y), null, Color.White * GetFadeAlpha(player.creditsTimer - offset * 5 - player.screenDuration * 5 - statueOffset, statueDuration - offset * 5), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);*/
                }
            }
            if (player.creditsTimer >= player.screenDuration * slideMayhemm + 60)
            {
                if (player.creditsTimer == player.screenDuration * slideMayhemm + 60) CreateCreditsText("Sprites by", monitorScale, 330, 1.3f, 7 * 60);
                if (player.creditsTimer == player.screenDuration * slideMayhemm + 90) CreateCreditsText("Mayhemm", monitorScale, 220, 1f, 7 * 60 - 60);
                if (player.creditsTimer > player.screenDuration * slideMayhemm + statueOffset && player.creditsTimer < player.screenDuration * (slideMayhemm + 1) - statueOffset)
                {
                    float scale = 1.5f * monitorScale.Y;
                    int offset = 30;
                    Main.spriteBatch.Draw(GetTexture("Extra/Credits/Mayhemm0"), new Vector2(200 * monitorScale.X + (player.creditsTimer - player.screenDuration * slideMayhemm) / 3, Main.screenHeight / 2 + 200 * monitorScale.Y), null, Color.White * GetFadeAlpha(player.creditsTimer - player.screenDuration * slideMayhemm - statueOffset, statueDuration), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(GetTexture("Extra/Credits/Mayhemm1"), new Vector2(Main.screenWidth - 500 * monitorScale.X - (player.creditsTimer - player.screenDuration * slideMayhemm) / 3, Main.screenHeight / 2 - 200 * monitorScale.Y), null, Color.White * GetFadeAlpha(player.creditsTimer - offset - player.screenDuration * slideMayhemm - statueOffset, statueDuration - offset), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(GetTexture("Extra/Credits/Mayhemm2"), new Vector2(500 * monitorScale.X + (player.creditsTimer - player.screenDuration * slideMayhemm) / 3, Main.screenHeight / 2 - 300), null, Color.White * GetFadeAlpha(player.creditsTimer - offset * 2 - player.screenDuration * slideMayhemm - statueOffset, statueDuration - offset * 2), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(GetTexture("Extra/Credits/Mayhemm3"), new Vector2(Main.screenWidth - 400 * monitorScale.X - (player.creditsTimer - player.screenDuration * slideMayhemm) / 3, Main.screenHeight / 2 + 100 * monitorScale.Y), null, Color.White * GetFadeAlpha(player.creditsTimer - offset * 3 - player.screenDuration * slideMayhemm - statueOffset, statueDuration - offset * 3), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(GetTexture("Extra/Credits/Mayhemm4"), new Vector2(400 * monitorScale.X + (player.creditsTimer - player.screenDuration * slideMayhemm) / 3, Main.screenHeight / 2 + 100 * monitorScale.Y), null, Color.White * GetFadeAlpha(player.creditsTimer - offset * 4 - player.screenDuration * slideMayhemm - statueOffset, statueDuration - offset * 3), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(GetTexture("Extra/Credits/Mayhemm5"), new Vector2(Main.screenWidth / 2 + (player.creditsTimer - player.screenDuration * slideMayhemm) / 3, Main.screenHeight / 2 + 300 * monitorScale.Y), null, Color.White * GetFadeAlpha(player.creditsTimer - offset * 5 - player.screenDuration * slideMayhemm - statueOffset, statueDuration - offset * 3), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                }
            }
            if (player.creditsTimer >= player.screenDuration * slideSpriters + 60)
            {
                if (player.creditsTimer == player.screenDuration * slideSpriters + 60) CreateCreditsText("Sprites by", monitorScale, 330, 1.3f, 7 * 60);
                if (player.creditsTimer == player.screenDuration * slideSpriters + 90) CreateCreditsText("NnickykunN", monitorScale, 220, 1f, 7 * 60 - 60);
                if (player.creditsTimer == player.screenDuration * slideSpriters + 90 + 15) CreateCreditsText("Darkpuppey", monitorScale, 220 - dist, 1f, 7 * 60 - 60);
                if (player.creditsTimer == player.screenDuration * slideSpriters + 90 + 15 * 2) CreateCreditsText("Skeletony", monitorScale, 220 - dist * 2, 1f, 7 * 60 - 60);
                if (player.creditsTimer == player.screenDuration * slideSpriters + 90 + 15 * 3) CreateCreditsText("daimgamer", monitorScale, 220 - dist * 3, 1f, 7 * 60 - 60);
                if (player.creditsTimer == player.screenDuration * slideSpriters + 90 + 15 * 4) CreateCreditsText("Aloe", monitorScale, 220 - dist * 4, 1f, 7 * 60 - 60);
                if (player.creditsTimer > player.screenDuration * slideSpriters + statueOffset && player.creditsTimer < player.screenDuration * (slideSpriters + 1) - statueOffset)
                {
                    float scale = 1.1f * monitorScale.Y;
                    int offset = 45;
                    Main.spriteBatch.Draw(GetTexture("Extra/Credits/AncientAmalgam"), new Vector2(200 * monitorScale.X + (player.creditsTimer - player.screenDuration * slideSpriters) / 3, Main.screenHeight / 2 + 200 * monitorScale.Y), null, Color.White * GetFadeAlpha(player.creditsTimer - player.screenDuration * slideSpriters - statueOffset, statueDuration), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(GetTexture("Extra/Credits/ScourgeFighter"), new Vector2(Main.screenWidth - 500 * monitorScale.X - (player.creditsTimer - player.screenDuration * slideSpriters) / 3, Main.screenHeight / 2 - 200 * monitorScale.Y), null, Color.White * GetFadeAlpha(player.creditsTimer - offset - player.screenDuration * slideSpriters - statueOffset, statueDuration - offset), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(GetTexture("Extra/Credits/Permafrost"), new Vector2(300 * monitorScale.X + (player.creditsTimer - player.screenDuration * slideSpriters) / 3, Main.screenHeight / 2 - 250 * monitorScale.Y), null, Color.White * GetFadeAlpha(player.creditsTimer - offset * 2 - player.screenDuration * slideSpriters - statueOffset, statueDuration - offset * 2), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                }
            }
            if (player.creditsTimer >= player.screenDuration * slideDonators + 60)
            {
                int dist2 = (int)(dist * 0.7f);
                if (player.creditsTimer == player.screenDuration * slideDonators + 60) CreateCreditsText("Donators", monitorScale, 330, 1.3f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideDonators + 90) CreateCreditsText("Eoite", monitorScale, 220, 0.7f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideDonators + 100) CreateCreditsText("ChamCham", monitorScale, 220 - dist2, 0.7f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideDonators + 110) CreateCreditsText("Aegida", monitorScale, 220 - dist2 * 2, 0.7f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideDonators + 120) CreateCreditsText("Buildmonger", monitorScale, 220 - dist2 * 3, 0.7f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideDonators + 130) CreateCreditsText("YukkiKun", monitorScale, 220 - dist2 * 4, 0.7f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideDonators + 140) CreateCreditsText("Superbaseball101", monitorScale, 220 - dist2 * 5, 0.7f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideDonators + 150) CreateCreditsText("Crow", monitorScale, 220 - dist2 * 6, 0.7f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideDonators + 160) CreateCreditsText("Lantard", monitorScale, 220 - dist2 * 7, 0.7f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideDonators + 170) CreateCreditsText("Megaswave", monitorScale, 220 - dist2 * 8, 0.7f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideDonators + 180) CreateCreditsText("Keydrian", monitorScale, 220 - dist2 * 9, 0.7f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideDonators + 190) CreateCreditsText("InstaFiz", monitorScale, 220 - dist2 * 10, 0.7f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideDonators + 200) CreateCreditsText("kREEpDABoom", monitorScale, 220 - dist2 * 11, 0.7f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideDonators + 210) CreateCreditsText("Big Spoon", monitorScale, 220 - dist2 * 12, 0.7f, 6 * 60);
            }

            if (player.creditsTimer >= player.screenDuration * slideHelpers + 60)
            {
                if (player.creditsTimer == player.screenDuration * slideHelpers + 60) CreateCreditsText("Helpers", monitorScale, 330, 1.3f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideHelpers + 90) CreateCreditsText("Alpaca121 (Wiki)", monitorScale, 220, 1f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideHelpers + 100) CreateCreditsText("thecherrynuke (Wiki)", monitorScale, 220 - dist, 1f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideHelpers + 110) CreateCreditsText("ReedemtheD3ad! (Wiki)", monitorScale, 220 - dist * 2, 1f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideHelpers + 120) CreateCreditsText("Dradonhunter11 (Code)", monitorScale, 220 - dist * 3, 1f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideHelpers + 130) CreateCreditsText("jopojelly (Code)", monitorScale, 220 - dist * 4, 1f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideHelpers + 140) CreateCreditsText("Seraph (Code)", monitorScale, 220 - dist * 5, 1f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideHelpers + 150) CreateCreditsText("Superbaseball101", monitorScale, 220 - dist * 6, 1f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideHelpers + 160) CreateCreditsText("Pomegranate (Discord Mod)", monitorScale, 220 - dist * 7, 1f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideHelpers + 170) CreateCreditsText("Oinite12 (Ex-Discord Mod)", monitorScale, 220 - dist * 8, 1f, 6 * 60);
                if (player.creditsTimer == player.screenDuration * slideHelpers + 180) CreateCreditsText("And many more!", monitorScale, 220 - dist * 9, 1f, 6 * 60);
            }

            if (player.creditsTimer >= player.screenDuration * slideSpecial + 60)
            {
                if (player.creditsTimer == player.screenDuration * slideSpecial + 60) CreateCreditsText("Special Thanks To", monitorScale, 330, 1.3f, 7 * 60);
                if (player.creditsTimer == player.screenDuration * slideSpecial + 90) CreateCreditsText("ReLogic for creating the amazing Terraria", monitorScale, 220, 1f, 7 * 60);
                if (player.creditsTimer == player.screenDuration * slideSpecial + 100) CreateCreditsText("Jofairden, jopojelly & bluemagic for creating tModloader", monitorScale, 220 - dist, 1f, 7 * 60);
                if (player.creditsTimer == player.screenDuration * slideSpecial + 110) CreateCreditsText("Gameraiders101 for getting me into modding", monitorScale, 220 - dist * 2, 1f, 7 * 60);
                if (player.creditsTimer == player.screenDuration * slideSpecial + 120) CreateCreditsText("FuryForged for showcasing the mod", monitorScale, 220 - dist * 3, 1f, 7 * 60);
                if (player.creditsTimer == player.screenDuration * slideSpecial + 130) CreateCreditsText("ChippyGaming for showcasing the mod", monitorScale, 220 - dist * 4, 1f, 7 * 60);
                if (player.creditsTimer == player.screenDuration * slideSpecial + 140) CreateCreditsText("Gameraiders101 again for showcasing the mod", monitorScale, 220 - dist * 5, 1f, 7 * 60);
            }
            if (player.creditsTimer >= player.screenDuration * slideThanks + 60)
            {
                if (player.creditsTimer == player.screenDuration * slideThanks + 60) CreateCreditsText("Biggest Thanks", monitorScale, 330, 1.3f, 10 * 60);
                if (player.creditsTimer == player.screenDuration * slideThanks + 90) CreateCreditsText("To YOU", monitorScale, 220, 1.4f, 10 * 60);
                if (player.creditsTimer == player.screenDuration * slideThanks + 120) CreateCreditsText("Seriously, thank you so much for playing Elements Awoken.", monitorScale, 160, 1f, 10 * 60);
                if (player.creditsTimer == player.screenDuration * slideThanks + 150) CreateCreditsText("It means a lot to the whole team that you can enjoy", monitorScale, 160 - dist, 1f, 10 * 60);
                if (player.creditsTimer == player.screenDuration * slideThanks + 180) CreateCreditsText("something we spent so much time on.", monitorScale, 160 - dist * 2, 1f, 10 * 60);
                if (player.creditsTimer == player.screenDuration * slideThanks + 210) CreateCreditsText("From- ThatOneJuicyOrange_ and the team <3", monitorScale, 160 - dist * 3, 1f, 10 * 60);
            }
        }
        private static void CreateCreditsText(string text, Vector2 monitorScale, float yOffset, float textScale, int duration)
        {
            float scale = textScale * monitorScale.Y;
            Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - yOffset * monitorScale.Y);
            DrawScreenText(text, duration, scale, pos);
        }

        private static float FindTextCenterX(string text, float scale)
        {
            Vector2 textSize = Main.fontDeathText.MeasureString(text) * scale;
            float textPositionLeft = Main.screenWidth / 2 - textSize.X / 2;
            return textPositionLeft;
        }

        public static void DrawScreenText(string text, int duration, float scale, Vector2 pos, bool centerText = false)
        {
            for (int i = 0; i < screenText.Length - 1; i++)
            {
                if (screenText[i] == "")
                {
                    screenText[i] = text;
                    screenTextAlpha[i] = 0;
                    screenTextTimer[i] = 0;
                    screenTextDuration[i] = duration;
                    screenTextScale[i] = scale;
                    if (centerText) pos = new Vector2(FindTextCenterX(text, scale), pos.Y);
                    screenTextPos[i] = pos;
                    break;
                }
            }
        }

        public float GetFadeAlpha(float timer, float duration)
        {
            if (timer < duration / 8) return timer / (duration / 8f);
            else if (timer > duration - (duration / 8)) return 1 - (timer - (duration - duration / 8f)) / (duration / 8f); // probably a better way to do this
            else return 1f;
        }

        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {

            if (Main.gameMenu) return;
            if (Main.myPlayer != -1 && Main.LocalPlayer.active)
            {
                MyPlayer modPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();
                if (Main.invasionX == Main.spawnTileX && MyWorld.voidInvasionUp)
                {
                    if (!Main.dayTime && Main.time > 16220)
                    {
                        music = MusicID.LunarBoss;
                    }
                    else
                    {
                        music = MusicID.TheTowers;
                    }
                }
                if (encounterTimer > 0)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Blank");
                }

                if (modPlayer.creditsTimer >= 0)
                {
                    music = MusicID.OverworldDay;
                }
            }
        }

        public override void PostSetupContent()
        {
            float wasteland = 2.5f;
            float mineBoss = 4.5f;
            float toySlime = 5.1f;
            float infernace = 5.2f;
            float erius = 5.5f;
            float observer = 6.1f;
            float scourge = 9.3f;
            float regaroth = 9.4f;
            //float celestials = 10.99f;
            float permafrost = 11.1f;
            float obsidious = 11.2f;
            float aqueous = 12.1f;
            float keepers = 14.1f;
            float guardian = 14.2f;
            float dotv = 14.4f;
            float shadeWyrm = 14.4f;
            float volcanox = 14.5f;
            float vlevi = 15f;
            float azana = 15.5f;
            float radiantRain = 15.75f;
            float ancients = 16f;
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                /** Vanilla boss numbers
                * SlimeKing = 1f;
                * EyeOfCthulhu = 2f;
                * EaterOfWorlds = 3f;
                * QueenBee = 4f;
                * Skeletron = 5f;
                * WallOfFlesh = 6f;
                * TheTwins = 7f;
                * TheDestroyer = 8f;
                * SkeletronPrime = 9f;
                * Plantera = 10f;
                * Golem = 11f;
                * DukeFishron = 12f;
                * LunaticCultist = 13f;
                * Moonlord = 14f;
                **/
                /*bossChecklist.Call("AddMiniBoss", toySlime, NPCType("ToySlime"), this, "Toy Slime", (Func<bool>)(() => MyWorld.downedToySlime), ItemType("ToySlimeSummon"),
                    default,
                    new List<int>() { ItemType("ToyBlade"), ItemType("ToyBow"), ItemType("ToyWand"), ItemType("ToyRobotControlRod"), ItemType("ToyPickaxe"), ItemType("ToyHelm"), ItemType("ToyBreastplate"), ItemType("ToyLeggings") },
                    "The Toy Slime spawns naturally if it has not been defeated already and the player has atleast 140 life and 7 defence. Use a[i: " + ItemType("ToySlimeSummon") + "] to increase the chance of it spawning",
                    "The Toy Slime hops away.", default, default, true);*/

                bossChecklist.Call("AddBoss", wasteland, NPCType("Wasteland"), this, "Wasteland", (Func<bool>)(() => MyWorld.downedWasteland), ItemType("WastelandSummon"),
                    new List<int>() { ItemType("WastelandMask"), ItemType("WastelandTrophy") },
                    new List<int>() { ItemType("TheAntidote"), ItemType("Pincer"), ItemType("ScorpionBlade"), ItemType("Stinger"), ItemType("ChitinStaff"), ItemType("VenomSample"), ItemType("WastelandBag") },
                    "Catch and use a [i:" + ItemType("WastelandSummon") + "] in the desert. They spawn naturally in the desert after the Eye of Cthulhu has been defeated",
                    "Wasteland burrows back into the sand...", default, default, true);

                bossChecklist.Call("AddMiniBoss", mineBoss, ModContent.NPCType<MineBoss>(), this, "Abandoned Automatron", (Func<bool>)(() => MyWorld.downedMineBoss), default,
                    default,
                    new List<int>() { },
                    "The Abandoned Automatron resides in a cavern in the Scarlet Mines underneath the Volcanic Plateaus in hell",
                    "The Abandoned Automatron powers down...", default, default, true);

                bossChecklist.Call("AddBoss", infernace, NPCType("Infernace"), this, "Infernace", (Func<bool>)(() => MyWorld.downedInfernace), ItemType("InfernaceSummon"),
                    new List<int>() { ItemType("InfernaceMask"), ItemType("InfernaceTrophy") },
                    new List<int>() { ItemType("FireBlaster"), ItemType("FireHarpyStaff"), ItemType("FireHeart"), ItemType("FlareSword"), ItemType("InfernaceBag") },
                    "Use the [i:" + ItemType("InfernaceSummon") + "] in the underworld. An obsidian platform arena is reccomended on expert mode because of the lava slimes.",
                    "Infernace fades into the heatwaves...", default, default, true);

                bossChecklist.Call("AddMiniBoss", erius, ModContent.NPCType<Erius>(), this, "Erius", (Func<bool>)(() => MyWorld.downedErius), default,
                   default,
                   new List<int>() { },
                   "Erius spawns in the temple under the Toxic Dunes in hell. Open the door and enter the door to provoke her.",
                   "Erius returns to her slumber...", default, default, true);

                bossChecklist.Call("AddMiniBoss", observer, NPCType("CosmicObserver"), this, "Cosmic Observer", (Func<bool>)(() => MyWorld.downedCosmicObserver), ItemType("CosmicObserverSummon"),
                    default,
                    new List<int>() { ItemType("CosmicGlass"), ItemType("ChargeRifle"), ItemType("CosmicCrusher"), ItemType("CosmicObserverStaff"), ItemType("Demolecularizer"), ItemType("EnergyFork"), ItemType("OrionsBelt"), ItemType("PlanetaryWave"), ItemType("CosmicalusVisor"), ItemType("CosmicalusBreastplate"), ItemType("CosmicalusLeggings") },
                    "The Cosmic Observer spawns naturally in the sky in hardmode. Use a[i: " + ItemType("CosmicObserverSummon") + "] to increase the chance of it spawning",
                    "The Cosmic Observer retreats into the clouds...", default, default, true);

                bossChecklist.Call("AddBoss", scourge, NPCType("ScourgeFighter"), this, "Scourge Fighter", (Func<bool>)(() => MyWorld.downedScourgeFighter), ItemType("ScourgeFighterSummon"),
                    new List<int>() { ItemType("ScourgeFighterTrophy") },
                    new List<int>() { ItemType("ScourgeDrive"), ItemType("ScourgeSword"), ItemType("ScourgeFighterMachineGun"), ItemType("ScourgeFighterRocketLauncher"), ItemType("SignalBooster"), ItemType("ScourgeFighterBag") },
                    "Use the [i:" + ItemType("ScourgeFighterSummon") + "] at nighttime.",
                    "The Scourge Fighter flies into the night...", default, default, true);

                bossChecklist.Call("AddBoss", regaroth, NPCType("RegarothHead"), this, "Regaroth", (Func<bool>)(() => MyWorld.downedRegaroth), ItemType("RegarothSummon"),
                    new List<int>() { ItemType("RegarothMask"), ItemType("RegarothTrophy") },
                    new List<int>() { ItemType("EnergyStaff"), ItemType("EyeOfRegaroth"), ItemType("Starstruck"), ItemType("StoneOfHope"), ItemType("EnergyWeaversHelm"), ItemType("EnergyWeaversBreastplate"), ItemType("EnergyWeaversLeggings"), ItemType("RegarothBag") },
                    "Use a [i:" + ItemType("RegarothSummon") + "] on a sky island",
                    "Regaroth retreats into the clouds", default, default, true);

                /*bossChecklist.Call("AddBoss", celestials, NPCType("Astra"), this, "The Celestials", (Func<bool>)(() => MyWorld.downedCelestial), ItemType("CelestialSummon"),
                    new List<int>() { ItemType("CelestialsMask"), ItemType("TheCelestialTrophy"), ItemType("CelestialCrown") },
                    new List<int>() { ItemType("Celestia"), ItemType("CelestialInferno"), ItemType("EyeballStaff"), ItemType("Solus"), ItemType("CelestialFlame"), ItemType("RegarothBag") },
                    "Use an [i:" + ItemType("CelestialSummon") + "] at nighttime",
                    "The Celestials dissapate into energy...", default, default, true);*/

                bossChecklist.Call("AddBoss", obsidious, NPCType("Obsidious"), this, "Obsidious", (Func<bool>)(() => MyWorld.downedObsidious), ItemType("ObsidiousSummon"),
                    new List<int>() { ItemType("ObsidiousMask"), ItemType("ObsidiousTrophy"), ItemType("ObsidiousRobes"), ItemType("ObsidiousPants") },
                    new List<int>() { ItemType("ObsidiousWings"), ItemType("Magmarox"), ItemType("TerreneScepter"), ItemType("Ultramarine"), ItemType("VioletEdge"), ItemType("SacredCrystal"), ItemType("ObsidiousBag") },
                    "Use an [i:" + ItemType("ObsidiousSummon") + "] at nighttime",
                    "Obsidious siezes the crystal...", default, default, true);

                bossChecklist.Call("AddBoss", permafrost, NPCType("Permafrost"), this, "Permafrost", (Func<bool>)(() => MyWorld.downedPermafrost), ItemType("PermafrostSummon"),
                    new List<int>() { ItemType("PermafrostMask"), ItemType("PermafrostTrophy") },
                    new List<int>() { ItemType("Flurry"), ItemType("Frigidblaster"), ItemType("IceReaver"), ItemType("IceWrath"), ItemType("Snowdrift"), ItemType("SoulOfTheFrost"), ItemType("PermafrostBag") },
                    "Use an[i: " + ItemType("PermafrostSummon") + "] in the snow",
                    "Permafrost fades into a blizzard...", default, default, true);

                bossChecklist.Call("AddBoss", aqueous, NPCType("Aqueous"), this, "Aqueous", (Func<bool>)(() => MyWorld.downedAqueous), ItemType("AqueousSummon"),
                    new List<int>() { ItemType("AqueousTrophy") },
                    new List<int>() { ItemType("BrinyBuster"), ItemType("BubblePopper"), ItemType("HighTide"), ItemType("OceansRazor"), ItemType("TheWave"), ItemType("Varee"), ItemType("AqueousMask"), ItemType("AqueousBag") },
                    "Use a [i:" + ItemType("AqueousSummon") + "] in the ocean",
                    "Aqueous sinks deep into the ocean...", default, default, true);

                bossChecklist.Call("AddBoss", keepers, new List<int>() { NPCType("TheEye"), NPCType("AncientWyrmHead") }, this, "Temple Keepers", (Func<bool>)(() => (MyWorld.downedAncientWyrm && MyWorld.downedEye)), ItemType("AncientDragonSummon"),
                    default,
                    new List<int>() { ItemType("TemplesCrystal"), ItemType("TheAllSeer"), ItemType("WyrmClaw"), ItemType("GazeOfInferno"), ItemType("Flare"), ItemType("TempleKeepersBag") },
                    "Use the [i:" + ItemType("AncientDragonSummon") + "] at nighttime",
                    default, default, default, true);

                bossChecklist.Call("AddBoss", guardian, new List<int>() { NPCType("TheGuardian"), NPCType("TheGuardianFly") }, this, "The Guardian", (Func<bool>)(() => MyWorld.downedGuardian), ItemType("GuardianSummon"),
                    new List<int>() { ItemType("TheGuardianMask"), ItemType("TheGuardianTrophy") },
                    new List<int>() { ItemType("Godslayer"), ItemType("InfernoStorm"), ItemType("TemplesWrath"), ItemType("FieryCore"), ItemType("GuardianBag") },
                    "Use an [i:" + ItemType("GuardianSummon") + "] at nighttime",
                    "The Guardian vanishes in a scene of flames", default, default, true);

                bossChecklist.Call("AddEvent", dotv,
                    new List<int>() { NPCType("Immolator"), NPCType("ReaverSlime"), NPCType("ZergCaster"), NPCType("VoidKnight"), NPCType("EtherealHunter"), NPCType("VoidCrawler"), NPCType("VoidGolem") },
                    this, "Dawn of the Void", (Func<bool>)(() => MyWorld.downedVoidEvent), ItemType("VoidEventSummon"), ItemType("ShadeEgg"),
                    new List<int>() { ItemType("CastersCurse"), ItemType("CrimsonShade"), ItemType("LifesLament"), ItemType("CrimsonShade"), ItemType("VoidJelly") },
                    "Use a [i:" + ItemType("VoidEventSummon") + "] at nighttime before 10pm. The item can be used during the day but the event wont start until night time, where it will start instantly.",
                    default, default, /*"ElementsAwoken/NPCs/VoidEventEnemies/Phase2/ShadeWyrm/ShadeWyrmHead_Head_Boss"*/default, default);

                bossChecklist.Call("AddMiniBoss", shadeWyrm, NPCType("ShadeWyrmHead"), this, "Shade Wyrm", (Func<bool>)(() => MyWorld.downedShadeWyrm), ItemType("VoidEventSummon"),
                    ItemType("ShadeEgg"),
                    new List<int>() { ItemType("LifesLament"), ItemType("CrimsonShade") },
                    "The Shade Wyrm spawns during the Dawn of the Void after midnight",
                    default, default, default, true);

                bossChecklist.Call("AddBoss", volcanox, NPCType("Volcanox"), this, "Volcanox", (Func<bool>)(() => MyWorld.downedVolcanox), ItemType("VolcanoxSummon"),
                    new List<int>() { ItemType("VolcanoxMask"), ItemType("VolcanoxTrophy") },
                    new List<int>() { ItemType("Combustia"), ItemType("EmberBurst"), ItemType("FatesFlame"), ItemType("FirestarterStaff"), ItemType("Hearth"), ItemType("CharredInsignia"), ItemType("VolcanoxBag") },
                    "Use a [i:" + ItemType("VolcanoxSummon") + "] in the underworld",
                    "Volcanox sinks into the lava...", default, default, true);

                bossChecklist.Call("AddBoss", vlevi, NPCType("VoidLeviathanHead"), this, "Void Leviathan", (Func<bool>)(() => MyWorld.downedVoidLeviathan), ItemType("VoidLeviathanSummon"),
                    new List<int>() { ItemType("VoidLeviathanMask"), ItemType("VoidLeviathanTrophy") },
                    new List<int>() { ItemType("BladeOfTheNight"), ItemType("BreathOfDarkness"), ItemType("CosmicWrath"), ItemType("EndlessAbyssBlaster"), ItemType("ExtinctionBow"), ItemType("LightsAffliction"), ItemType("PikeOfEternalDespair"), ItemType("Reaperstorm"), ItemType("VoidLeviathansAegis"), ItemType("AmuletOfDestruction"), ItemType("VoidLeviathanBag"), ItemType("VoidWalkersGreatmask"), ItemType("VoidWalkersHelm"), ItemType("VoidWalkersVisage"), ItemType("VoidWalkersHood"), ItemType("VoidWalkersBreastplate"), ItemType("VoidWalkersLeggings") },
                    "Use a [i:" + ItemType("VoidLeviathanSummon") + "] at nighttime",
                    "The Void Leviathan descends into shadows...", default, default, true);

                bossChecklist.Call("AddBoss", azana, new List<int>() { NPCType("Azana"), NPCType("AzanaEye") }, this, "Azana", (Func<bool>)(() => (MyWorld.downedAzana || MyWorld.sparedAzana)), ItemType("AzanaSummon"),
                    new List<int>() { ItemType("AzanaMask"), ItemType("AzanaTrophy") },
                    new List<int>() { ItemType("Anarchy"), ItemType("ChaoticGaze"), ItemType("ChaoticImpaler"), ItemType("EntropicCoating"), ItemType("GleamOfAnnhialation"), ItemType("Pandemonium"), ItemType("PurgeRifle"), ItemType("RingOfChaos") },
                    "Use a [i:" + ItemType("AzanaSummon") + "] in at nighttime",
                    default, default, default, true);

                bossChecklist.Call("AddEvent", radiantRain,
                    new List<int>() { ModContent.NPCType<AllKnowerHead>(), ModContent.NPCType<RadiantWarrior>(), ModContent.NPCType<SparklingSlime>(), ModContent.NPCType<StarlightGlobule>(), ModContent.NPCType<StellarStarfish>() },
                    this, "Radiant Rain", (Func<bool>)(() => MyWorld.completedRadiantRain), ModContent.ItemType<RadiantRainSummon>(), default,
                    new List<int>() { ModContent.ItemType<GlobuleCannon>(), ModContent.ItemType<GlobuleStaff>(), ModContent.ItemType<LightMine>(), ModContent.ItemType<RadiantConcentrator>(), ModContent.ItemType<RadiantGlove>(), ModContent.ItemType<RadiantKatana>(), ModContent.ItemType<RadiantMortar>(), ModContent.ItemType<Radia>() },
                    "Has a 25% chance to occur instead of rain or can be summoned using a [i:" + ModContent.ItemType<RadiantRainSummon>() + "]",
                    default, default, default, default);

                bossChecklist.Call("AddMiniBoss", radiantRain, ModContent.NPCType<RadiantMaster>(), this, "Radiant Master", (Func<bool>)(() => MyWorld.downedRadiantMaster), default,
                    default,
                    new List<int>() { ModContent.ItemType<Majesty>(), ModContent.ItemType<RadiantBomb>(), ModContent.ItemType<RadiantBow>(), ModContent.ItemType<RadiantCrown>(), ModContent.ItemType<RadiantSword>() },
                    "The Radiant Master spawns near the end of the Radiant Rain",
                    "The Radiant Master bursts into stars", default, default, true);

                bossChecklist.Call("AddBoss", ancients, new List<int>() { NPCType("AncientAmalgam"), NPCType("Izaris"), NPCType("Kirvein"), NPCType("Xernon"), NPCType("Krecheus") }, this, "The Ancients", (Func<bool>)(() => MyWorld.downedAncients), ItemType("AncientsSummon"),
                    default,
                    new List<int>() { ItemType("GiftOfTheArchaic"), ItemType("Chromacast"), ItemType("Shimmerspark"), ItemType("TheFundamentals"), ItemType("CrystallineLocket"), ItemType("AncientsBag") },
                    "Speak to the storyteller or use a [i:" + ItemType("AncientsSummon") + "]",
                    "The Ancients return to their slumber", default, default, true);
            }
            Mod fargos = ModLoader.GetMod("Fargowiltas");
            if (fargos != null)
            {
                // AddSummon, order or value in terms of vanilla bosses, your mod internal name, summon item internal name, inline method for retrieving downed value, price to sell for in copper
                fargos.Call("AddSummon", wasteland, "ElementsAwoken", "WastelandSummon", (Func<bool>)(() => MyWorld.downedWasteland), Item.buyPrice(0, 10, 0, 0));
                fargos.Call("AddSummon", toySlime, "ElementsAwoken", "ToySlimeSummon", (Func<bool>)(() => MyWorld.downedToySlime), Item.buyPrice(0, 2, 50, 0));
                fargos.Call("AddSummon", infernace, "ElementsAwoken", "InfernaceSummon", (Func<bool>)(() => MyWorld.downedInfernace), Item.buyPrice(0, 17, 50, 0));
                fargos.Call("AddSummon", observer, "ElementsAwoken", "CosmicObserverSummon", (Func<bool>)(() => MyWorld.downedCosmicObserver), Item.buyPrice(0, 20, 0, 0));
                fargos.Call("AddSummon", scourge, "ElementsAwoken", "ScourgeFighterSummon", (Func<bool>)(() => MyWorld.downedScourgeFighter), Item.buyPrice(0, 45, 0, 0));
                fargos.Call("AddSummon", regaroth, "ElementsAwoken", "RegarothSummon", (Func<bool>)(() => MyWorld.downedRegaroth), Item.buyPrice(0, 50, 0, 0));
                //fargos.Call("AddSummon", celestials, "ElementsAwoken", "CelestialSummon", (Func<bool>)(() => MyWorld.downedCelestial), Item.buyPrice(0, 55, 0, 0));
                fargos.Call("AddSummon", permafrost, "ElementsAwoken", "PermafrostSummon", (Func<bool>)(() => MyWorld.downedPermafrost), Item.buyPrice(0, 60, 0, 0));
                fargos.Call("AddSummon", obsidious, "ElementsAwoken", "ObsidiousSummon", (Func<bool>)(() => MyWorld.downedObsidious), Item.buyPrice(0, 60, 0, 0));
                fargos.Call("AddSummon", aqueous, "ElementsAwoken", "AqueousSummon", (Func<bool>)(() => MyWorld.downedAqueous), Item.buyPrice(0, 67, 50, 0));
                fargos.Call("AddSummon", keepers, "ElementsAwoken", "AncientDragonSummon", (Func<bool>)(() => (MyWorld.downedAncientWyrm && MyWorld.downedEye)), Item.buyPrice(0, 80, 0, 0));
                //fargos.Call("AddSummon", guardian, "ElementsAwoken", "GuardianSummon", (Func<bool>)(() => MyWorld.downedToySlime), Item.buyPrice(0, 2, 50, 0));
                //fargos.Call("AddSummon", dotv, "ElementsAwoken", "VoidEventSummon", (Func<bool>)(() => MyWorld.downedToySlime), Item.buyPrice(0, 2, 50, 0));
                fargos.Call("AddSummon", volcanox, "ElementsAwoken", "VolcanoxSummon", (Func<bool>)(() => MyWorld.downedVolcanox), Item.buyPrice(1, 0, 50, 0));
                //fargos.Call("AddSummon", vlevi, "ElementsAwoken", "Summon", (Func<bool>)(() => MyWorld.downedToySlime), Item.buyPrice(0, 2, 50, 0));
                //fargos.Call("AddSummon", azana, "ElementsAwoken", "Summon", (Func<bool>)(() => (MyWorld.downedAzana || MyWorld.sparedAzana)), Item.buyPrice(0, 2, 50, 0));
                fargos.Call("AddSummon", ancients, "ElementsAwoken", "AncientsSummon", (Func<bool>)(() => MyWorld.downedAncients), Item.buyPrice(1, 25, 0, 0));
            }
            Mod mystaria = ModLoader.GetMod("Mystaria");
            if (mystaria != null)
            {
                mystaria.Call("AddSpellCombo", ItemType("FrostMine"), 1, 1, 2, 2, 3, 1, 0, 0, 4, 2);
            }
            Mod censusMod = ModLoader.GetMod("Census");
            if (censusMod != null)
            {
                //censusMod.Call("TownNPCCondition", NPCType("Example Person"), $"Have [i:{ItemType<Items.ExampleItem>()}] or [i:{ItemType<Items.Placeable.ExampleBlock>()}] in inventory and build a house out of [i:{ItemType<Items.Placeable.ExampleBlock>()}] and [i:{ItemType<Items.Placeable.ExampleWall>()}]");
                censusMod.Call("TownNPCCondition", NPCType("Alchemist"), "Defeat the Brain of Cthulhu or Eater of Worlds");
                censusMod.Call("TownNPCCondition", NPCType("Storyteller"), "Always available");
            }
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            ElementsAwokenMessageType msgType = (ElementsAwokenMessageType)reader.ReadByte();
            switch (msgType)
            {
                case ElementsAwokenMessageType.StarHeartSync:
                    byte playernumber = reader.ReadByte();
                    MyPlayer starHeartPlayer = Main.player[playernumber].GetModPlayer<MyPlayer>();
                    int voidHeartsUsed = reader.ReadInt32();
                    int chaosHeartsUsed = reader.ReadInt32();
                    int lunarStarsUsed = reader.ReadInt32();
                    starHeartPlayer.voidHeartsUsed = voidHeartsUsed;
                    starHeartPlayer.chaosHeartsUsed = chaosHeartsUsed;
                    starHeartPlayer.lunarStarsUsed = lunarStarsUsed;
                    break;

                case ElementsAwokenMessageType.AwakenedSync:
                    playernumber = reader.ReadByte();
                    AwakenedPlayer awakenedPlayer = Main.player[playernumber].GetModPlayer<AwakenedPlayer>();
                    int sanity = reader.ReadInt32();
                    awakenedPlayer.sanity = sanity;
                    break;

                case ElementsAwokenMessageType.EnergySync:
                    playernumber = reader.ReadByte();
                    PlayerEnergy energyPlayer = Main.player[playernumber].GetModPlayer<PlayerEnergy>();
                    int energy = reader.ReadInt32();
                    energyPlayer.energy = energy;
                    break;

                case ElementsAwokenMessageType.Storyteller:
                    if (Main.npc[reader.ReadInt32()].modNPC is Storyteller townNPC && townNPC.npc.active)
                    {
                        townNPC.HandlePacket(reader);
                    }
                    break;

                case ElementsAwokenMessageType.EliteSync:
                    int npcNum = reader.ReadInt32();
                    AwakenedModeNPC awakNPC = Main.npc[npcNum].GetGlobalNPC<AwakenedModeNPC>();
                    int eliteSync = reader.ReadInt32();
                    awakNPC.elite = eliteSync;
                    break;

                default:
                    Logger.WarnFormat("Elements Awoken: Unknown Message type: {0}", msgType);
                    break;
            }
        }

        // from mana unbound
        private void RemoveManaCap(ILContext il)
        {
            ILCursor cursor = new ILCursor(il);

            if (!cursor.TryGotoNext(MoveType.Before,
                                    i => i.MatchLdfld("Terraria.Player", "statManaMax2"),
                                    i => i.MatchLdcI4(400)))
            {
                Logger.Fatal("Could not find instruction to patch");
                return;
            }

            cursor.Next.Next.Operand = int.MaxValue;
        }
    }

    internal enum ElementsAwokenMessageType : byte
    {
        StarHeartSync,
        AwakenedSync,
        EnergySync,
        Storyteller,
        EliteSync,
    }
}