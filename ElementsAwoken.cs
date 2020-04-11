using ElementsAwoken.NPCs.Bosses.Aqueous;
using ElementsAwoken.NPCs.Bosses.Infernace;
using ElementsAwoken.NPCs.Bosses.Permafrost;
using ElementsAwoken.NPCs.Bosses.Regaroth;
using ElementsAwoken.NPCs.Bosses.TheGuardian;
using ElementsAwoken.NPCs.Bosses.VoidLeviathan;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Shaders;
using Terraria.Graphics;
using Terraria.Localization;
using ReLogic.Graphics;
using System.Reflection;
using Terraria.UI.Chat;
using ElementsAwoken.Effects;
using Terraria.UI.Gamepad;
using Terraria.GameInput;
using ElementsAwoken.NPCs.Town;
using ElementsAwoken.Events.VoidEvent;
using MonoMod.Cil;
using Terraria.Graphics.Shaders;
using ElementsAwoken.Items.Tech.Weapons.Tier6;
using ElementsAwoken.UI;
using ElementsAwoken.Events.RadiantRain.Enemies;
using ElementsAwoken.Items.BossSummons;

namespace ElementsAwoken
{
    public class ElementsAwoken : Mod
    {
        public static DynamicSpriteFont encounterFont;

        internal UserInterface AlchemistUserInterface;
        internal UserInterface VoidTimerChangerUI;
        internal PromptInfoUI PromptUI;
        private UserInterface PromptInfoUserInterface;

        public static ModHotKey neovirtuo;
        public static ModHotKey specialAbility;
        public static ModHotKey armorAbility;
        public static ModHotKey dash2;

        public static ElementsAwoken instance;

        public static bool calamityEnabled;
        public static bool bossChecklistEnabled;
        public static bool ancientsAwakenedEnabled;
        public static bool eaMusicEnabled;
        public static bool eaRetroMusicEnabled;

        public static int[] screenTextTimer = new int[10];
        public static int[] screenTextDuration = new int[10];
        public static float[] screenTextAlpha = new float[10];
        public static float[] screenTextScale = new float[10];
        public static string[] screenText = new string[10];
        public static Vector2[] screenTextPos = new Vector2[10];

        //public static Dictionary<int, Color> RarityColors = new Dictionary<int, Color>();

        public static Texture2D AADeathBall;
        public static Texture2D insanityTex;
        public static Texture2D heartGlowTex;

        public static List<int> instakillImmune = new List<int>();

        public static bool aprilFools = false;

        public static int encounter = 0;
        public static bool encounterSetup = false;
        public static int encounterTimer = 0;
        public static int encounterShakeTimer = 0;

        public const int bossPromptDelay = 108000;
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
            Main.OnTick += Update;
            if (!Main.dedServ)
            {
                Main.heartTexture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Heart");
                Main.heart2Texture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Heart2");
                Main.manaTexture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Mana");
                Main.rainTexture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Rain");

                Main.tileSpelunker[TileID.LunarOre] = false;

                ResetCloudTexture();
            }
        }

        public override void Load()
        {
            Main.OnTick -= Update;

            IL.Terraria.Player.Update += RemoveManaCap;

            DateTime now = DateTime.Today;
            if (now.Day == 1 && now.Month == 4) aprilFools = true;

                calamityEnabled = ModLoader.GetMod("CalamityMod") != null;
            bossChecklistEnabled = ModLoader.GetMod("BossChecklist") != null;
            ancientsAwakenedEnabled = ModLoader.GetMod("AncientsAwakened") != null;
            eaMusicEnabled = ModLoader.GetMod("EAMusic") != null;
            eaRetroMusicEnabled = ModLoader.GetMod("EARetroMusic") != null;

            instance = this;

            //HOTKEYS
            neovirtuo = RegisterHotKey("Neovirtuo", "C");
            specialAbility = RegisterHotKey("Special Ability", "Z");
            armorAbility = RegisterHotKey("Armor Ability", "X");
            dash2 = RegisterHotKey("Secondary Dash", "F");
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

                Filters.Scene["ElementsAwoken:HeatDistortion"] = new Filter(new ScreenShaderData("FilterHeatDistortion").UseImage("Images/Misc/noise", 0, null).UseIntensity(4f), EffectPriority.Low);

                Ref<Effect> screenRef = new Ref<Effect>(GetEffect("Effects/ShockwaveEffect"));
                Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
                Filters.Scene["Shockwave"].Load();

                AADeathBall = instance.GetTexture("Projectiles/NPCProj/Ancients/Gores/LightBall");
                PremultiplyTexture(AADeathBall);
                insanityTex = instance.GetTexture("Effects/Insanity");
                PremultiplyTexture(insanityTex);
                heartGlowTex = instance.GetTexture("Extra/HeartGlow");
                PremultiplyTexture(heartGlowTex);

                AlchemistUserInterface = new UserInterface();
                VoidTimerChangerUI = new UserInterface();
                PromptUI = new PromptInfoUI();
                PromptUI.Activate();
                PromptInfoUserInterface = new UserInterface();
                PromptInfoUserInterface.SetState(PromptUI);
            }
            // config
            //Config.Load();

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
            #endregion
        }


        public static bool prevMenuState;
        public static void Update()
        {
            if (Main.gameMenu != prevMenuState)
            {
                if (Main.gameMenu)
                {
                    //Load on menu
                    ResetCloudTexture();
                }
                prevMenuState = Main.gameMenu;
            }
        }

        public static void DebugModeText(object text, int r = 255, int g = 255, int b = 255)
        {
            Color color = new Color(r, g, b);
            if (ModContent.GetInstance<Config>().debugMode)
            {
                Main.NewText(text, color);
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

            if (!Main.gameMenu)
            {
                if (MyWorld.credits)
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
                else
                {
                    if (!player.ghost)
                    {
                        // computer
                        if (modPlayer.inComputer)
                        {
                            var computerLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                            var computerState = new LegacyGameInterfaceLayer("ElementsAwoken: UI",
                                delegate
                                {
                                    DrawComputer(Main.spriteBatch);
                                    return true;
                                },
                                InterfaceScaleType.UI);
                            layers.Insert(computerLayer, computerState);
                        }
                        // encounter text
                        if (encounter != 0)
                        {
                            var encounterLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                            var encounterState = new LegacyGameInterfaceLayer("ElementsAwoken: UI",
                                delegate
                                {
                                    DrawEncounterText(Main.spriteBatch);
                                    return true;
                                },
                                InterfaceScaleType.UI);
                            layers.Insert(encounterLayer, encounterState);
                        }
                        //Voidblood glow
                        if (modPlayer.voidBlood)
                        {
                            var heartLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                            var heartState = new LegacyGameInterfaceLayer("ElementsAwoken: UI2",
                                delegate
                                {
                                    DrawVoidBloodGlow();
                                    return true;
                                },
                                InterfaceScaleType.UI);
                            layers.Insert(heartLayer, heartState);
                        }
                        // hearts & mana
                        if (!calamityEnabled)
                        {
                            var heartLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                            var heartState = new LegacyGameInterfaceLayer("ElementsAwoken: UI2",
                                delegate
                                {
                                    DrawHearts();
                                    return true;
                                },
                                InterfaceScaleType.UI);
                            layers.Insert(heartLayer, heartState);

                            // to stop hearts being underneath
                            if (modPlayer.voidHeartsUsed == 10)
                            {
                                Main.heart2Texture = GetTexture("Extra/Blank");
                            }
                            else
                            {
                                Main.heart2Texture = GetTexture("Extra/Heart2");
                            }
                            if (modPlayer.lunarStarsUsed == 1)
                            {
                                Main.manaTexture = GetTexture("Extra/Mana2");
                            }
                            else
                            {
                                Main.manaTexture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Mana");
                            }
                        }
                        #region shield hearts
                        var shieldLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                        var shieldState = new LegacyGameInterfaceLayer("ElementsAwoken: UI2",
                            delegate
                            {
                                DrawShield();
                                return true;
                            },
                            InterfaceScaleType.UI);
                        layers.Insert(shieldLayer, shieldState);
                        #endregion
                        #region energy & insanity UI

                            var BarLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                            var BarState = new LegacyGameInterfaceLayer("ElementsAwoken: UI",
                                delegate
                                {
                                    if (ModContent.GetInstance<Config>().resourceBars)
                                    {
                                        DrawEnergyBar();
                                        DrawInsanityBar();
                                    }
                                    else
                                    {
                                        DrawEnergyUI();
                                        DrawInsanityUI();
                                    }
                                    return true;
                                },
                                InterfaceScaleType.UI);
                            layers.Insert(BarLayer, BarState);

                            var insanityOverlayLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                            var insanityOverlayState = new LegacyGameInterfaceLayer("ElementsAwoken: Interface Logic 1",
                                delegate
                                {
                                    DrawInsanityOverlay();
                                    return true;
                                },
                                InterfaceScaleType.UI);
                            layers.Insert(insanityOverlayLayer, insanityOverlayState);
                        
                        #endregion
                        // sanity book
                        if (awakenedPlayer.openSanityBook)
                        {
                            var bookLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                            var bookState = new LegacyGameInterfaceLayer("ElementsAwoken: UI",
                                delegate
                                {
                                    DrawSanityBook();
                                    return true;
                                },
                                InterfaceScaleType.UI);
                            layers.Insert(bookLayer, bookState);
                        }
                        #region info accessories
                        var infoLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
                        var infoState = new LegacyGameInterfaceLayer("ElementsAwoken: UI",
                            delegate
                            {
                                DrawInfoAccs();
                                return true;
                            },
                            InterfaceScaleType.UI);
                        layers.Insert(infoLayer, infoState);
                        #endregion
                        if (player.HeldItem.type == ModContent.ItemType<Railgun>())
                        {
                            var heatBarLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                            var heatBarState = new LegacyGameInterfaceLayer("ElementsAwoken: UI2",
                                delegate
                                {
                                    DrawHeatBar(player.HeldItem);
                                    return true;
                                },
                                InterfaceScaleType.UI);
                            layers.Insert(heatBarLayer, heatBarState);
                        }
                    }
                }
                // rain texture
                if (encounter == 3)
                {
                    Main.rainTexture = GetTexture("Extra/Rain3");
                }
                else if (MyWorld.radiantRain)
                {
                    Main.rainTexture = GetTexture("Extra/Rain4");
                }
                else
                {
                    Main.rainTexture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Rain");
                }
                // infernace clouds
                if (MyWorld.firePrompt > bossPromptDelay)
                {
                    for (int cloud = 0; cloud < 22; cloud++)
                    {
                        Main.cloudTexture[cloud] = GetTexture(string.Concat(new object[]
                        {
                    "Extra/Clouds/Cloud_",
                    cloud
                        }));
                    }
                }
                else
                {
                    ResetCloudTexture();
                }

                var textLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                var textState = new LegacyGameInterfaceLayer("ElementsAwoken: Text",
                    delegate
                    {
                        for (int i = 0; i < screenText.Length - 1; i++)
                        {
                            if (screenText[i] != "" && screenText[i] != null)
                            {
                                screenTextTimer[i]++;
                                if (screenTextTimer[i] < screenTextDuration[i] / 8) screenTextAlpha[i] += 1f / ((float)screenTextDuration[i] / 8f);// 1/4 of the time fading 
                                else if (screenTextTimer[i] > screenTextDuration[i] - (screenTextDuration[i] / 8)) screenTextAlpha[i] -= 1f / ((float)screenTextDuration[i] / 8f);
                                else screenTextAlpha[i] = 1;
                                if (screenTextTimer[i] > screenTextDuration[i]) screenText[i] = "";

                                DrawStringOutlined(Main.spriteBatch, screenText[i], screenTextPos[i], Color.White * screenTextAlpha[i], screenTextScale[i]);
                            }
                        }
                        return true;
                    },
                    InterfaceScaleType.UI);
                if (!MyWorld.credits) layers.Insert(textLayer, textState);
                else layers.Insert(1, textState); // in credits all layers r deleted

                if (modPlayer.screenTransition)
                {
                    var transLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Interface Logic 4"));
                    var transState = new LegacyGameInterfaceLayer("ElementsAwoken: Transitions",
                        delegate
                        {
                            BlackScreenTrans();
                            return true;
                        },
                        InterfaceScaleType.UI);
                    if (!MyWorld.credits) layers.Insert(transLayer, transState);
                    else layers.Insert(1, transState); // in credits all layers r deleted
                }
            }

            int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (inventoryIndex != -1)
            {
                layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
                    "ElementsAwoken: Alchemist UI",
                    delegate {
                        AlchemistUserInterface.Draw(Main.spriteBatch, new GameTime());
                        VoidTimerChangerUI.Draw(Main.spriteBatch, new GameTime());
                        if (PromptInfoUI.Visible) PromptInfoUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
            }
        public override void UpdateUI(GameTime gameTime)
        {
            AlchemistUserInterface?.Update(gameTime);
            VoidTimerChangerUI?.Update(gameTime);
            if (PromptInfoUI.Visible)
            {
                PromptInfoUserInterface?.Update(gameTime);
            }
        }
        public override void MidUpdateTimeWorld()
        {
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
        private static void ResetCloudTexture()
        {
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
        }
        #region draw methods
        private static void DrawHearts()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");

            Player player = Main.player[Main.myPlayer];
            var modPlayer = player.GetModPlayer<MyPlayer>();
            float lifePerHeart = 10f;
            if (Main.LocalPlayer.ghost)
            {
                return;
            }

            int lifeForHeart = player.statLifeMax / 20;

            int voidHeartLife = modPlayer.voidHeartsUsed * 2; // multiply by however many hearts you want it to make when you use one
            if (voidHeartLife < 0)
            {
                voidHeartLife = 0;
            }
            if (voidHeartLife > 0)
            {
                lifeForHeart = player.statLifeMax / (20 + voidHeartLife / 4);
                lifePerHeart = player.statLifeMax / 20f;
            }

            int chaosHeartLife = modPlayer.chaosHeartsUsed * 2;
            if (chaosHeartLife < 0)
            {
                chaosHeartLife = 0;
            }
            if (chaosHeartLife > 0)
            {
                lifeForHeart = player.statLifeMax / (20 + chaosHeartLife / 4);
                lifePerHeart = player.statLifeMax / 20f;
            }
            // statLifeMax2 is the actual player life, it equals statLifeMax plus bonuses
            int playerLife = player.statLifeMax2 - player.statLifeMax;
            if (lifeForHeart == 0) lifeForHeart = 1; // shouldnt happen but failsafe
            lifePerHeart += playerLife / lifeForHeart;

            var info = typeof(Main).GetField("UI_ScreenAnchorX", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            int screenAnchorX = (int)info.GetValue(null);

            for (int heartNum = 1; heartNum < (int)(player.statLifeMax2 / lifePerHeart) + 1; heartNum++)
            {
                float scale = 1f;
                bool lastHeart = false;
                int lifeStat;
                if (player.statLife >= heartNum * lifePerHeart)
                {
                    lifeStat = 255;
                    if (player.statLife == heartNum * lifePerHeart)
                    {
                        lastHeart = true;
                    }
                }
                else
                {
                    float num7 = (player.statLife - (heartNum - 1) * lifePerHeart) / lifePerHeart;
                    lifeStat = (int)(30f + 225f * num7);
                    if (lifeStat < 30)
                    {
                        lifeStat = 30;
                    }
                    scale = num7 / 4f + 0.75f;
                    if (scale < 0.75)
                    {
                        scale = 0.75f;
                    }
                    if (num7 > 0f)
                    {
                        lastHeart = true;
                    }
                }
                if (lastHeart)
                {
                    scale += Main.cursorScale - 1f;
                }
                int xPos = 0;
                int yPos = 0;
                if (heartNum > 10)
                {
                    xPos -= 260;
                    yPos += 26;
                }
                if (heartNum > 20)
                {
                    xPos = 0;
                    yPos = 0;
                }
                int a = (int)(lifeStat * 0.9f);
                if (!player.ghost)
                {
                    if (chaosHeartLife > 0)
                    {
                        chaosHeartLife--;
                        Texture2D heart4Texture = mod.GetTexture("Extra/Heart4");
                        Main.spriteBatch.Draw(heart4Texture, new Vector2((float)(500 + 26 * (heartNum - 1) + xPos + screenAnchorX + Main.heartTexture.Width / 2), 32f + ((float)Main.heartTexture.Height - (float)Main.heartTexture.Height * scale) / 2f + (float)yPos + (float)(Main.heartTexture.Height / 2)), new Rectangle?(new Rectangle(0, 0, Main.heartTexture.Width, Main.heartTexture.Height)), new Color(lifeStat, lifeStat, lifeStat, a), 0f, new Vector2((float)(Main.heartTexture.Width / 2), (float)(Main.heartTexture.Height / 2)), scale, SpriteEffects.None, 0f);
                    }
                    else if (voidHeartLife > 0)
                    {
                        voidHeartLife--;
                        Texture2D heart3Texture = mod.GetTexture("Extra/Heart3");
                        if (modPlayer.voidCompressor)
                        {
                            heart3Texture = mod.GetTexture("Extra/Heart3Alt");
                        }
                        else
                        {
                            heart3Texture = mod.GetTexture("Extra/Heart3");
                        }
                        Main.spriteBatch.Draw(heart3Texture, new Vector2((float)(500 + 26 * (heartNum - 1) + xPos + screenAnchorX + Main.heartTexture.Width / 2), 32f + ((float)Main.heartTexture.Height - (float)Main.heartTexture.Height * scale) / 2f + (float)yPos + (float)(Main.heartTexture.Height / 2)), new Rectangle?(new Rectangle(0, 0, Main.heartTexture.Width, Main.heartTexture.Height)), new Color(lifeStat, lifeStat, lifeStat, a), 0f, new Vector2((float)(Main.heartTexture.Width / 2), (float)(Main.heartTexture.Height / 2)), scale, SpriteEffects.None, 0f);
                        //Main.spriteBatch.Draw(heart3Texture, new Vector2((float)(500 + 26 * (heartNum - 1) + xPos + screenAnchorX + heart3Texture.Width / 2), 32f + ((float)Main.heartTexture.Height - (float)Main.heartTexture.Height * scale) / 2f + (float)yPos + (float)(Main.heartTexture.Height / 2)), new Rectangle?(new Rectangle(0, 0, heart3Texture.Width, heart3Texture.Height)), new Color(lifeStat, lifeStat, lifeStat, a), 0.0f, new Vector2((float)(heart3Texture.Width / 2), (float)(heart3Texture.Height / 2)), scale, SpriteEffects.None, 0.0f);

                    }
                }
            }
        }
        private static void DrawVoidBloodGlow()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");

            Player player = Main.player[Main.myPlayer];
            float lifePerHeart = 20f;
            if (player.ghost)
            {
                return;
            }

            int num = player.statLifeMax / 20;
            int num2 = (player.statLifeMax - 400) / 5;
            if (num2 < 0)
            {
                num2 = 0;
            }
            if (num2 > 0)
            {
                num = player.statLifeMax / (20 + num2 / 4);
                lifePerHeart = (float)player.statLifeMax / 20f;
            }
            int num3 = player.statLifeMax2 - player.statLifeMax;
            lifePerHeart += (float)(num3 / num);


            var info = typeof(Main).GetField("UI_ScreenAnchorX", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            int screenAnchorX = (int)info.GetValue(null);

            for (int heartNum = 1; heartNum < (int)(player.statLifeMax2 / lifePerHeart) + 1; heartNum++)
            {
                float scale = 1f;
                bool lastHeart = false;
                int lifeStat;
                if (player.statLife >= heartNum * lifePerHeart)
                {
                    lifeStat = 255;
                    if (player.statLife == heartNum * lifePerHeart)
                    {
                        lastHeart = true;
                    }
                }
                else
                {
                    float num7 = (player.statLife - (heartNum - 1) * lifePerHeart) / lifePerHeart;
                    lifeStat = (int)(30f + 225f * num7);
                    if (lifeStat < 30)
                    {
                        lifeStat = 30;
                    }
                    scale = num7 / 4f + 0.75f;
                    if (scale < 0.75)
                    {
                        scale = 0.75f;
                    }
                    if (num7 > 0f)
                    {
                        lastHeart = true;
                    }
                }
                if (lastHeart)
                {
                    scale += Main.cursorScale - 1f;
                }
                int xPos = 0;
                int yPos = 0;
                if (heartNum > 10)
                {
                    xPos -= 260;
                    yPos += 26;
                }
                if (heartNum > 20)
                {
                    xPos = 0;
                    yPos = 0;
                }
                xPos -= (heartGlowTex.Width-Main.heartTexture.Width) / 2;
                yPos -= (heartGlowTex.Height-Main.heartTexture.Height) / 2;

                int a = (int)(lifeStat * 0.9f);
                if (!player.ghost)
                {
                        Main.spriteBatch.Draw(heartGlowTex, new Vector2((float)(500 + 26 * (heartNum - 1) + xPos + screenAnchorX + heartGlowTex.Width / 2), 32f + ((float)heartGlowTex.Height - (float)heartGlowTex.Height * scale) / 2f + (float)yPos + (float)(heartGlowTex.Height / 2)), new Rectangle?(new Rectangle(0, 0, heartGlowTex.Width, heartGlowTex.Height)), new Color(lifeStat, lifeStat, lifeStat, a), 0f, new Vector2((float)(heartGlowTex.Width / 2), (float)(heartGlowTex.Height / 2)), scale, SpriteEffects.None, 0f);
                }
            }
        }
        private static void DrawShield()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");

            Player player = Main.player[Main.myPlayer];
            var modPlayer = player.GetModPlayer<MyPlayer>();
            float lifePerHeart = 5f;
            if (Main.LocalPlayer.ghost)
            {
                return;
            }

            int lifeForHeart = player.statLifeMax / 20;

            int shieldLife = modPlayer.shieldHearts; // multiply by however many hearts you want it to make when you use one
            if (shieldLife < 0)
            {
                shieldLife = 0;
            }
            if (shieldLife > 0)
            {
                lifeForHeart = player.statLifeMax / (20 + shieldLife / 4);
                lifePerHeart = player.statLifeMax / 20f;
            }

            // statLifeMax2 is the actual player life, it equals statLifeMax plus bonuses
            int playerLife = modPlayer.shieldHearts * 5;
            lifePerHeart += playerLife / lifeForHeart;

            var info = typeof(Main).GetField("UI_ScreenAnchorX", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            int screenAnchorX = (int)info.GetValue(null);

            for (int heartNum = 1; heartNum < (int)(player.statLifeMax2 / lifePerHeart) + 1; heartNum++)
            {
                float scale = 1f;
                bool lastHeart = false;
                int lifeStat;
                if (player.statLife >= heartNum * lifePerHeart)
                {
                    lifeStat = 255;
                    if (player.statLife == heartNum * lifePerHeart)
                    {
                        lastHeart = true;
                    }
                }
                else
                {
                    float num7 = (player.statLife - (heartNum - 1) * lifePerHeart) / lifePerHeart;
                    lifeStat = (int)(30f + 225f * num7);
                    if (lifeStat < 30)
                    {
                        lifeStat = 30;
                    }
                    scale = num7 / 4f + 0.75f;
                    if (scale < 0.75)
                    {
                        scale = 0.75f;
                    }
                    if (num7 > 0f)
                    {
                        lastHeart = true;
                    }
                }
                if (lastHeart)
                {
                    scale += Main.cursorScale - 1f;
                }
                int xPos = 0;
                int yPos = 0;
                if (heartNum > 10)
                {
                    xPos -= 260;
                    yPos += 26;
                }
                if (heartNum > 20)
                {
                    xPos = 0;
                    yPos = 0;
                }
                int a = (int)(lifeStat * 0.9f);
                if (!player.ghost)
                {
                    if (shieldLife > 0)
                    {
                        shieldLife--;
                        Texture2D heart4Texture = mod.GetTexture("Extra/ShieldHeart");
                        Main.spriteBatch.Draw(heart4Texture, new Vector2((float)(500 + 26 * (heartNum - 1) + xPos + screenAnchorX + Main.heartTexture.Width / 2), 32f + ((float)Main.heartTexture.Height - (float)Main.heartTexture.Height * scale) / 2f + (float)yPos + (float)(Main.heartTexture.Height / 2)), new Rectangle?(new Rectangle(0, 0, Main.heartTexture.Width, Main.heartTexture.Height)), new Color(lifeStat, lifeStat, lifeStat, a), 0f, new Vector2((float)(Main.heartTexture.Width / 2), (float)(Main.heartTexture.Height / 2)), scale, SpriteEffects.None, 0f);
                    }
                }
            }
        }

        private static void DrawEnergyBar()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            Player player = Main.player[Main.myPlayer];
            var modPlayer = player.GetModPlayer<PlayerEnergy>();
            if (Main.LocalPlayer.ghost)
            {
                return;
            }
            if (!player.ghost && modPlayer.maxEnergy > 0)
            {
                var info = typeof(Main).GetField("UI_ScreenAnchorX", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
                int screenAnchorX = (int)info.GetValue(null);

                Texture2D backgroundTexture = mod.GetTexture("Extra/EnergyUI");
                int barPosLeft = 415 + screenAnchorX + (MyWorld.awakenedMode ? -134 : 0);
                Main.spriteBatch.Draw(backgroundTexture, new Rectangle(barPosLeft, 48, backgroundTexture.Width, backgroundTexture.Height), null, Color.White, 0f, new Vector2(backgroundTexture.Width / 2, backgroundTexture.Height / 2), SpriteEffects.None, 0f);

                Texture2D barTexture = mod.GetTexture("Extra/EnergyBar");
                Rectangle barDest = new Rectangle(barPosLeft + 10, 48, (int)(barTexture.Width * ((float)modPlayer.energy / (float)modPlayer.maxEnergy)), barTexture.Height);
                Rectangle barLength = new Rectangle(0, 0, (int)(barTexture.Width * ((float)modPlayer.energy / (float)modPlayer.maxEnergy)), barTexture.Height);
                Main.spriteBatch.Draw(barTexture, barDest, barLength, Color.White, 0f, new Vector2(barTexture.Width / 2, barTexture.Height / 2), SpriteEffects.None, 0f);

                DynamicSpriteFont fontType = Main.fontMouseText;
                string text = "Energy: " + modPlayer.energy + "/" + modPlayer.maxEnergy;
                Vector2 textSize = fontType.MeasureString(text);
                float textPositionLeft = barPosLeft - textSize.X / 2;
                DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, text, new Vector2(textPositionLeft, 6f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            }
        }
        private static void DrawHeatBar(Item item)
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            Player player = Main.player[Main.myPlayer];
            Railgun railItem = (Railgun)item.modItem;
            if (Main.LocalPlayer.ghost || player.dead)
            {
                return;
            }

            float heat = MathHelper.Clamp(railItem.heat, 0, 1300);
            float maxHeatDisplayed = 1300;

            var info = typeof(Main).GetField("UI_ScreenAnchorX", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            int screenAnchorX = (int)info.GetValue(null);

            Texture2D backgroundTexture = mod.GetTexture("Extra/HeatBarUI");
            Vector2 UIPos = new Vector2(Main.screenWidth / 2 + 40, Main.screenHeight /2 -backgroundTexture.Width - 10);
            Vector2 UISize = new Vector2(backgroundTexture.Width, backgroundTexture.Height);

            Main.spriteBatch.Draw(backgroundTexture, new Rectangle((int)UIPos.X, (int)UIPos.Y, backgroundTexture.Width, backgroundTexture.Height), null, Color.White, 0f,Vector2.Zero, SpriteEffects.None, 0f);

            Texture2D barTexture = mod.GetTexture("Extra/HeatBar");
            int barHeight = (int)(barTexture.Height * (heat / maxHeatDisplayed));
            Rectangle barDest = new Rectangle((int)UIPos.X, (int)UIPos.Y + barTexture.Height - barHeight + 4, barTexture.Width,  barHeight);
            Rectangle barLength = new Rectangle(0, 0, barTexture.Width,  barHeight);
            Main.spriteBatch.Draw(barTexture, barDest, barLength, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);

            if (!Main.mouseText)
            {
                if (Main.mouseX > UIPos.X && Main.mouseX < UIPos.X + UISize.X && Main.mouseY > UIPos.Y && Main.mouseY < UIPos.Y + UISize.Y)
                {
                    string mouseText = (int)railItem.heat + "";
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, mouseText, new Vector2(Main.mouseX + 17, Main.mouseY + 17), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                }
            }
        }
        private static void DrawInsanityUI()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            Texture2D UITex = mod.GetTexture("Extra/InsanityUIIcon");
            Player player = Main.player[Main.myPlayer];
            var modPlayer = player.GetModPlayer<AwakenedPlayer>();
            float sanityPerEye = 30f;
            if (player.ghost || !MyWorld.awakenedMode)
            {
                return;
            }


            var info = typeof(Main).GetField("UI_ScreenAnchorX", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            int screenAnchorX = (int)info.GetValue(null);
            Vector2 UIPos = new Vector2(500f - Main.heartTexture.Width * 5 - 32, 32f);

            Vector2 UISize = new Vector2(6 * Main.heartTexture.Width, Main.heartTexture.Height);
            for (int eyeNum = 1; eyeNum < (int)(modPlayer.sanityMax / sanityPerEye) + 1; eyeNum++)
            {
                float scale = 1f;
                bool lastHeart = false;
                int insanityStat;
                if (modPlayer.sanity >= eyeNum * sanityPerEye)
                {
                    insanityStat = 255;
                    if (modPlayer.sanity == eyeNum * sanityPerEye)
                    {
                        lastHeart = true;
                    }
                }
                else
                {
                    float num7 = (modPlayer.sanity - (eyeNum - 1) * sanityPerEye) / sanityPerEye;
                    insanityStat = (int)(30f + 225f * num7);
                    if (insanityStat < 30)
                    {
                        insanityStat = 30;
                    }
                    scale = num7 / 4f + 0.75f;
                    if (scale < 0.75)
                    {
                        scale = 0.75f;
                    }
                    if (num7 > 0f)
                    {
                        lastHeart = true;
                    }
                }
                if (lastHeart)
                {
                    scale += Main.cursorScale - 1f;
                }
                int xPos = 0;
                int yPos = 0;
                if (eyeNum > 5)
                {
                    xPos -= 130;
                    yPos += 26;
                    UISize.Y = Main.heartTexture.Height * 2;
                }
                xPos -= (UITex.Width - Main.heartTexture.Width) / 2;
                yPos -= (UITex.Height - Main.heartTexture.Height) / 2;

                if (modPlayer.sanity < modPlayer.sanityMax * 0.4f)
                {
                    int amount = 1;
                    if (modPlayer.sanity < modPlayer.sanityMax * 0.3f) amount = 2;
                    else if (modPlayer.sanity < modPlayer.sanityMax * 0.2f) amount = 3;
                    else if(modPlayer.sanity < modPlayer.sanityMax * 0.1f) amount = 4;
                    if (modPlayer.sanityGlitchFrame != 0)
                    {
                        xPos += Main.rand.Next(-amount, amount);
                        yPos += Main.rand.Next(-amount, amount);
                    }
                }

                int a = (int)(insanityStat * 0.9f);
                if (!player.ghost)
                {
                    Main.spriteBatch.Draw(UITex, new Vector2((float)(UIPos.X + 26 * (eyeNum - 1) + xPos + screenAnchorX + Main.heartTexture.Width / 2), UIPos.Y + ((float)Main.heartTexture.Height - (float)Main.heartTexture.Height * scale) / 2f + (float)yPos + (float)(Main.heartTexture.Height / 2)), new Rectangle?(new Rectangle(0, 0, UITex.Width, UITex.Height)), new Color(insanityStat, insanityStat, insanityStat, a), 0f, new Vector2((float)(UITex.Width / 2), (float)(UITex.Height / 2)), scale, SpriteEffects.None, 0f);
                }
            }

            DynamicSpriteFont fontType = Main.fontMouseText;
            string text = "Sanity: " + modPlayer.sanity + "/" + modPlayer.sanityMax;
            Vector2 textSize = fontType.MeasureString(text);
            float textPositionLeft = UIPos.X + UISize.X / 2 +  screenAnchorX - textSize.X / 2;
            DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, text, new Vector2(textPositionLeft, 6f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);

            if (!Main.mouseText)
            {
                if (Main.mouseX > UIPos.X + screenAnchorX && Main.mouseX < UIPos.X + UISize.X + screenAnchorX && Main.mouseY > 32 && Main.mouseY < 32 + Main.heartTexture.Height + UISize.Y)
                {          
                    string mouseText = modPlayer.sanity + "/" + modPlayer.sanityMax;
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, mouseText, new Vector2(Main.mouseX + 17,Main.mouseY + 17), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                }
            }

            if (modPlayer.sanityRegen != 0)
            {
                Texture2D arrowTexture = mod.GetTexture("Extra/SanityArrow");
                int arrowHeight = 26;
                Rectangle arrowDest = new Rectangle((int)(textPositionLeft + textSize.X + arrowTexture.Width / 2) + 4, 6 + arrowHeight / 2, arrowTexture.Width, arrowHeight);
                Rectangle arrowLength = new Rectangle(0, arrowHeight * modPlayer.sanityArrowFrame, arrowTexture.Width, arrowHeight);
                SpriteEffects doFlip = modPlayer.sanityRegen < 0 ? SpriteEffects.None : SpriteEffects.FlipVertically;
                Main.spriteBatch.Draw(arrowTexture, arrowDest, arrowLength, Color.White, 0f, new Vector2(arrowTexture.Width / 2, arrowHeight / 2), doFlip, 0f);
            }
        }
        private static void DrawEnergyUI()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            Texture2D UITex = mod.GetTexture("Extra/EnergyUIIcon");
            Player player = Main.player[Main.myPlayer];
            var modPlayer = player.GetModPlayer<PlayerEnergy>();
            if (player.ghost || modPlayer.maxEnergy == 0)
            {
                return;
            }
            float energyPerEye = modPlayer.maxEnergy / 10;

            var info = typeof(Main).GetField("UI_ScreenAnchorX", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            int screenAnchorX = (int)info.GetValue(null);
            Vector2 UIPos = new Vector2(500f - Main.heartTexture.Width * 5 - 32, 32f);
            Vector2 UISize = new Vector2(6 * Main.heartTexture.Width, Main.heartTexture.Height);
            if (MyWorld.awakenedMode) UIPos.X -= UISize.X + 32;
            for (int eyeNum = 1; eyeNum <= 10; eyeNum++)
            {
                float scale = 1f;
                bool lastHeart = false;
                int energyStat;
                if (modPlayer.energy >= eyeNum * energyPerEye)
                {
                    energyStat = 255;
                    if (modPlayer.energy == eyeNum * energyPerEye)
                    {
                        lastHeart = true;
                    }
                }
                else
                {
                    float num7 = (modPlayer.energy - (eyeNum - 1) * energyPerEye) / energyPerEye;
                    energyStat = (int)(30f + 225f * num7);
                    if (energyStat < 30)
                    {
                        energyStat = 30;
                    }
                    scale = num7 / 4f + 0.75f;
                    if (scale < 0.75)
                    {
                        scale = 0.75f;
                    }
                    if (num7 > 0f)
                    {
                        lastHeart = true;
                    }
                }
                if (lastHeart)
                {
                    scale += Main.cursorScale - 1f;
                }
                int xPos = 0;
                int yPos = 0;
                if (eyeNum > 5)
                {
                    xPos -= 130;
                    yPos += 26;
                    UISize.Y = Main.heartTexture.Height * 2;
                }
                xPos -= (UITex.Width - Main.heartTexture.Width) / 2;
                yPos -= (UITex.Height - Main.heartTexture.Height) / 2;


                int a = (int)(energyStat * 0.9f);
                if (!player.ghost)
                {
                    Main.spriteBatch.Draw(UITex, new Vector2((float)(UIPos.X + 26 * (eyeNum - 1) + xPos + screenAnchorX + Main.heartTexture.Width / 2), UIPos.Y + ((float)Main.heartTexture.Height - (float)Main.heartTexture.Height * scale) / 2f + (float)yPos + (float)(Main.heartTexture.Height / 2)), new Rectangle?(new Rectangle(0, 0, UITex.Width, UITex.Height)), new Color(energyStat, energyStat, energyStat, a), 0f, new Vector2((float)(UITex.Width / 2), (float)(UITex.Height / 2)), scale, SpriteEffects.None, 0f);
                }
            }

            DynamicSpriteFont fontType = Main.fontMouseText;
            string text = "Energy: " + modPlayer.energy + "/" + modPlayer.maxEnergy;
            Vector2 textSize = fontType.MeasureString(text);
            float textPositionLeft = UIPos.X + UISize.X / 2 + screenAnchorX - textSize.X / 2;
            DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, text, new Vector2(textPositionLeft, 6f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);

            if (!Main.mouseText)
            {
                if (Main.mouseX > UIPos.X + screenAnchorX && Main.mouseX < UIPos.X + UISize.X + screenAnchorX && Main.mouseY > 32 && Main.mouseY < 32 + Main.heartTexture.Height + UISize.Y)
                {
                    string mouseText = modPlayer.energy + "/" + modPlayer.maxEnergy;
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, mouseText, new Vector2(Main.mouseX + 17, Main.mouseY + 17), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                }
            }
        }
        private static void DrawInsanityBar()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            Player player = Main.player[Main.myPlayer];
            var modPlayer = player.GetModPlayer<AwakenedPlayer>();
            if (Main.LocalPlayer.ghost)
            {
                return;
            }
            if (!player.ghost && MyWorld.awakenedMode)
            {
                var info = typeof(Main).GetField("UI_ScreenAnchorX", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
                int screenAnchorX = (int)info.GetValue(null);

                Texture2D backgroundTexture = mod.GetTexture("Extra/InsanityUI");
                int barPosLeft = 415 + screenAnchorX;
                Main.spriteBatch.Draw(backgroundTexture, new Rectangle(barPosLeft, 48, backgroundTexture.Width, backgroundTexture.Height), null, Color.White, 0f, new Vector2(backgroundTexture.Width / 2, backgroundTexture.Height / 2), SpriteEffects.None, 0f);

                // above 40% sanity
                if (modPlayer.sanity >= modPlayer.sanityMax * 0.4f)
                {
                    Texture2D barTexture = mod.GetTexture("Extra/InsanityBar");
                    Rectangle barDest = new Rectangle(barPosLeft + 11, 48, (int)(barTexture.Width * ((float)modPlayer.sanity / (float)modPlayer.sanityMax)), barTexture.Height);
                    Rectangle barLength = new Rectangle(0, 0, (int)(barTexture.Width * ((float)modPlayer.sanity / (float)modPlayer.sanityMax)), barTexture.Height);
                    Main.spriteBatch.Draw(barTexture, barDest, barLength, Color.White, 0f, new Vector2(barTexture.Width / 2, barTexture.Height / 2), SpriteEffects.None, 0f);
                }
                else
                {
                    Texture2D barTexture = mod.GetTexture("Extra/InsanityBarDistorted");
                    int barHeight = 28;
                    Rectangle barDest = new Rectangle(barPosLeft + 11, 48, (int)(barTexture.Width * ((float)modPlayer.sanity / (float)modPlayer.sanityMax)), barHeight);
                    Rectangle barLength = new Rectangle(0, barHeight * modPlayer.sanityGlitchFrame, (int)(barTexture.Width * ((float)modPlayer.sanity / (float)modPlayer.sanityMax)), barHeight);
                    Main.spriteBatch.Draw(barTexture, barDest, barLength, Color.White, 0f, new Vector2(barTexture.Width / 2, barHeight / 2), SpriteEffects.None, 0f);
                }
                if (modPlayer.sanityRegen != 0)
                {
                    Texture2D arrowTexture = mod.GetTexture("Extra/SanityArrow");
                    int arrowHeight = 26;
                    Rectangle barDest = new Rectangle(barPosLeft + 74, 46, arrowTexture.Width, arrowHeight);
                    Rectangle barLength = new Rectangle(0, arrowHeight * modPlayer.sanityArrowFrame, arrowTexture.Width, arrowHeight);
                    SpriteEffects doFlip = modPlayer.sanityRegen < 0 ? SpriteEffects.None : SpriteEffects.FlipVertically;
                    Main.spriteBatch.Draw(arrowTexture, barDest, barLength, Color.White, 0f, new Vector2(arrowTexture.Width / 2, arrowHeight / 2), doFlip, 0f);
                }
                DynamicSpriteFont fontType = Main.fontMouseText;
                string text = "Sanity: " + modPlayer.sanity + "/" + modPlayer.sanityMax;
                Vector2 textSize = fontType.MeasureString(text);
                float textPositionLeft = barPosLeft - textSize.X / 2;
                DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, text, new Vector2(textPositionLeft, 6f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            }
        }

        private static void DrawInsanityOverlay()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            Player player = Main.player[Main.myPlayer];
            var modPlayer = player.GetModPlayer<AwakenedPlayer>();

            if (modPlayer.sanity > modPlayer.sanityMax * 0.50f) return;

            Color color = new Color(255, InsanityOverlay.gbValues, InsanityOverlay.gbValues) * InsanityOverlay.transparency;
            int width = insanityTex.Width;
            int num = 10;
            Rectangle rect = Main.player[Main.myPlayer].getRect();
            rect.Inflate((width - rect.Width) / 2, (width - rect.Height) / 2 + num / 2);
            rect.Offset(-(int)Main.screenPosition.X, -(int)Main.screenPosition.Y + (int)Main.player[Main.myPlayer].gfxOffY - num);
            Rectangle destinationRectangle1 = Rectangle.Union(new Rectangle(0, 0, 1, 1), new Rectangle(rect.Right - 1, rect.Top - 1, 1, 1));
            Rectangle destinationRectangle2 = Rectangle.Union(new Rectangle(Main.screenWidth - 1, 0, 1, 1), new Rectangle(rect.Right, rect.Bottom - 1, 1, 1));
            Rectangle destinationRectangle3 = Rectangle.Union(new Rectangle(Main.screenWidth - 1, Main.screenHeight - 1, 1, 1), new Rectangle(rect.Left, rect.Bottom, 1, 1));
            Rectangle destinationRectangle4 = Rectangle.Union(new Rectangle(0, Main.screenHeight - 1, 1, 1), new Rectangle(rect.Left - 1, rect.Top, 1, 1));
            Main.spriteBatch.Draw(Main.magicPixel, destinationRectangle1, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
            Main.spriteBatch.Draw(Main.magicPixel, destinationRectangle2, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
            Main.spriteBatch.Draw(Main.magicPixel, destinationRectangle3, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
            Main.spriteBatch.Draw(Main.magicPixel, destinationRectangle4, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
            Main.spriteBatch.Draw(insanityTex, rect, color);
        }
        private static void BlackScreenTrans()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");

            Player player = Main.player[Main.myPlayer];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            Color color = Color.Black * modPlayer.screenTransAlpha;
            Rectangle rect = new Rectangle(0, 0, Main.screenWidth, Main.screenHeight);
            Main.spriteBatch.Draw(Main.magicPixel, rect, color);
        }
        public void DrawComputer(SpriteBatch spriteBatch)
        {
            var mod = ModLoader.GetMod("ElementsAwoken");
            var background = mod.GetTexture("Extra/ComputerText");
            var player = Main.player[Main.myPlayer].GetModPlayer<MyPlayer>();
            string text = player.computerText;
            spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
            Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, text, Main.screenWidth / 2 - 230, 86, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
        }
        public void DrawSanityBook()
        {
            Player player = Main.player[Main.myPlayer];
            AwakenedPlayer awakenedPlayer = player.GetModPlayer<AwakenedPlayer>();

            var background = GetTexture("Extra/InsanityBookUI");
            Main.spriteBatch.Draw(background, new Rectangle(Main.screenWidth - 350, Main.screenHeight - 250, background.Width, background.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            // draw the positive
            Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, "Regens:", Main.screenWidth - 330, Main.screenHeight - 220, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
            for (int i = 0; i < awakenedPlayer.sanityRegens.Count; i++)
            {
                string text = awakenedPlayer.sanityRegensName[i] + ": " + awakenedPlayer.sanityRegens[i];
                int yPos = Main.screenHeight - 200 + 25 * i;
                Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, text, Main.screenWidth - 330, yPos, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
            }
            // draw the negative
            Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, "Drains:", Main.screenWidth - 150, Main.screenHeight - 220, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
            for (int i = 0; i < awakenedPlayer.sanityDrains.Count; i++)
            {
                string text = awakenedPlayer.sanityDrainsName[i] + ": " + awakenedPlayer.sanityDrains[i];
                int textLength = (int)Main.fontMouseText.MeasureString(text).X;
                    int xPos = Main.screenWidth - 150;
                if (Main.screenWidth - 150 + textLength > Main.screenWidth) xPos = Main.screenWidth - textLength - 35;
                int yPos = Main.screenHeight - 200 + 25 * i;
                Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, text, xPos, yPos, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
            }
        }
        public void DrawInfoAccs()
        {
            Player player = Main.player[Main.myPlayer];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            int amountOfInfoActive = CountAvailableInfo() - 1; // - 1 so it starts at 0 when 1 is equipped
            int amountOfInfoEquipped = CountEquippedInfo() - 1;

            float num4 = 215f;
            int whichInfoDrawing = -1;
            string text = "";

            for (int infoNum = 0; infoNum < 3; infoNum++)
            {
                string text2 = "";
                string hoverText = "";

                if (infoNum == 0 && modPlayer.alchemistTimer)
                {
                    if ((!modPlayer.hideEAInfo[0] || Main.playerInventory))
                    {
                        hoverText = "Buff Damage Per Second";
                        whichInfoDrawing = infoNum;

                        text2 = modPlayer.buffDPS + " buff damage per second";
                        if (modPlayer.buffDPS <= 0)
                        {
                            text2 = Language.GetTextValue("GameUI.NoDPS");
                        }
                    }
                    amountOfInfoEquipped++;
                    if (!modPlayer.hideEAInfo[0])
                    {
                        amountOfInfoActive++;
                    }
                }
                else if (infoNum == 1 && modPlayer.dryadsRadar)
                {
                    if ((!modPlayer.hideEAInfo[1] || Main.playerInventory))
                    {
                        hoverText = "Nearby Evil Biomes";
                        whichInfoDrawing = infoNum;

                        text2 = modPlayer.nearbyEvil + " nearby";
                    }
                    amountOfInfoEquipped++;
                    if (!modPlayer.hideEAInfo[1])
                    {
                        amountOfInfoActive++;
                    }
                }
                else if (infoNum == 2 && modPlayer.rainMeter)
                {
                    if ((!modPlayer.hideEAInfo[2] || Main.playerInventory))
                    {
                        hoverText = "Rain Time";
                        whichInfoDrawing = infoNum;

                        text2 = Main.rainTime / 60 + " seconds remaining";
                        if (Main.rainTime == 0) text2 = "Clear";
                    }
                    amountOfInfoEquipped++;
                    if (!modPlayer.hideEAInfo[2])
                    {
                        amountOfInfoActive++;
                    }
                }
                if (text2 != "")
                {
                    int mH = (int)((typeof(Main).GetField("mH", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)).GetValue(null));
                    if ((Main.npcChatText == null || Main.npcChatText == "") && player.sign < 0)
                    {
                        int distBetweenInfo = 22;
                        if (Main.screenHeight < 650)
                        {
                            distBetweenInfo = 20;
                        }


                        int iconPosX;
                        int iconPosY;
                        if (!Main.playerInventory)
                        {
                            iconPosX = Main.screenWidth - 280;
                            iconPosY = -32;
                            if (Main.mapStyle == 1 && Main.mapEnabled)
                            {
                                iconPosY += 254;
                            }
                        }
                        else if (Main.ShouldDrawInfoIconsHorizontally)
                        {
                            iconPosX = Main.screenWidth - 280 + 20 * amountOfInfoEquipped - 10;
                            iconPosY = 94;
                            if (Main.mapStyle == 1 && Main.mapEnabled)
                            {
                                iconPosY += 254;
                            }
                            if (amountOfInfoEquipped + 1 > 12)
                            {
                                iconPosX -= 20 * 12;
                                iconPosY += 26;
                            }
                        }
                        else
                        {
                            int num28 = (int)(52f * Main.inventoryScale);
                            iconPosX = 697 - num28 * 4 + Main.screenWidth - 800 + 20 * (amountOfInfoEquipped % 2);
                            iconPosY = 114 + mH + num28 * 7 + num28 / 2 + 20 * (amountOfInfoEquipped / 2) + 8 * (amountOfInfoEquipped / 4) - 20;
                            if (Main.EquipPage == 2)
                            {
                                iconPosX += num28 + num28 / 2;
                                iconPosY -= num28;
                            }
                        }
                        if (whichInfoDrawing >= 0)
                        {
                            Texture2D tex = GetTexture("Extra/EAInfo" + whichInfoDrawing);
                            Vector2 vector = new Vector2((float)iconPosX, (float)(iconPosY + 74 + distBetweenInfo * amountOfInfoActive + 52));

                            Color white = Color.White;
                            bool flag14 = false;
                            if (Main.playerInventory)
                            {
                                vector = new Vector2((float)iconPosX, (float)iconPosY);
                                if ((float)Main.mouseX >= vector.X && (float)Main.mouseY >= vector.Y && (float)Main.mouseX <= vector.X + (float)tex.Width && (float)Main.mouseY <= vector.Y + (float)tex.Height && !PlayerInput.IgnoreMouseInterface)
                                {
                                    flag14 = true;
                                    player.mouseInterface = true;
                                    if (Main.mouseLeft && Main.mouseLeftRelease)
                                    {
                                        Main.PlaySound(12, -1, -1, 1, 1f, 0f);
                                        Main.mouseLeftRelease = false;
                                        modPlayer.hideEAInfo[whichInfoDrawing] = !modPlayer.hideEAInfo[whichInfoDrawing];
                                    }
                                    if (!Main.mouseText)
                                    {
                                        text = hoverText;
                                        Main.mouseText = true;
                                    }
                                }
                                if (modPlayer.hideEAInfo[whichInfoDrawing])
                                {
                                    white = new Color(80, 80, 80, 70);
                                }

                            }
                            else if ((float)Main.mouseX >= vector.X && (float)Main.mouseY >= vector.Y && (float)Main.mouseX <= vector.X + (float)tex.Width && (float)Main.mouseY <= vector.Y + (float)tex.Height && !Main.mouseText)
                            {
                                Main.mouseText = true;
                                text = hoverText;
                            }
                            //UILinkPointNavigator.SetPosition(1558 + amountOfInfoEquipped - 1, vector + tex.Size() * 0.75f);
                            if (!Main.playerInventory && modPlayer.hideEAInfo[whichInfoDrawing])
                            {
                                white = Color.Transparent;
                            }
                            Main.spriteBatch.Draw(tex, vector, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), white, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                            if (flag14)
                            {
                                Texture2D outline = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "UI" + Path.DirectorySeparatorChar.ToString() + "InfoIcon_13");
                                Main.spriteBatch.Draw(outline, vector - Vector2.One * 2f, null, Main.OurFavoriteColor, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, hoverText, new Vector2(Main.mouseX, Main.mouseX), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                            }
                            iconPosX += 20;
                        }
                        if (!Main.playerInventory)
                        {
                            Vector2 vector2 = new Vector2(1f);

                            Vector2 vector3 = Main.fontMouseText.MeasureString(text2);
                            if (vector3.X > num4)
                            {
                                vector2.X = num4 / vector3.X;
                            }
                            if (vector2.X < 0.58f)
                            {
                                vector2.Y = 1f - vector2.X / 3f;
                            }
                            for (int num31 = 0; num31 < 5; num31++)
                            {
                                int num32 = 0;
                                int num33 = 0;
                                Color black = Color.Black;
                                if (num31 == 0)
                                {
                                    num32 = -2;
                                }
                                if (num31 == 1)
                                {
                                    num32 = 2;
                                }
                                if (num31 == 2)
                                {
                                    num33 = -2;
                                }
                                if (num31 == 3)
                                {
                                    num33 = 2;
                                }
                                if (num31 == 4)
                                {
                                    black = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
                                }
                                /*if (i > num2 && i < num2 + 2)
                                {
                                    black = new Color((int)(black.R / 3), (int)(black.G / 3), (int)(black.B / 3), (int)(black.A / 3));
                                }*/

                                DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, Main.fontMouseText, text2, new Vector2((float)(iconPosX + num32), (float)(iconPosY + 74 + distBetweenInfo * amountOfInfoActive + num33 + 48)), black, 0f, default(Vector2), vector2, SpriteEffects.None, 0f);
                            }
                        }
                        if (!string.IsNullOrEmpty(text))
                        {
                            if (Main.playerInventory)
                            {
                                Main.player[Main.myPlayer].mouseInterface = true;
                            }
                            Vector2 drawTextPos = new Vector2(Main.mouseX, Main.mouseY) + new Vector2(16.0f);
                            if (drawTextPos.X + Main.fontMouseText.MeasureString(text).X > Main.screenWidth)
                            {
                                drawTextPos.X -= Main.fontMouseText.MeasureString(text).X; // to stop it drawing off the side
                            }

                            Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, text, drawTextPos.X, drawTextPos.Y, new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), Color.Black, new Vector2());
                        }
                    }
                }
            }
        }
        private int CountAvailableInfo()
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
        private int CountEquippedInfo()
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
        internal static void DrawStringOutlined(SpriteBatch spriteBatch, string text, Vector2 position, Color color, float scale)
        {
            // outlines
            spriteBatch.DrawString(Main.fontDeathText, text, new Vector2(position.X - 1, position.Y), Color.Black * (color.A / 255f), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(Main.fontDeathText, text, new Vector2(position.X + 1, position.Y), Color.Black * (color.A / 255f), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(Main.fontDeathText, text, new Vector2(position.X, position.Y - 1), Color.Black * (color.A / 255f), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(Main.fontDeathText, text, new Vector2(position.X, position.Y + 1), Color.Black * (color.A / 255f), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            // actual text
            spriteBatch.DrawString(Main.fontDeathText, text, new Vector2(position.X, position.Y), color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
        public void DrawEncounterText(SpriteBatch spriteBatch)
        {
            var mod = ModLoader.GetMod("ElementsAwoken");
            var player = Main.player[Main.myPlayer].GetModPlayer<MyPlayer>();
            string text = player.encounterText;
            if (player.encounterTextTimer > 0)
            {
                Vector2 textSize = Main.fontDeathText.MeasureString(text);
                float textPositionLeft = Main.screenWidth / 2 - textSize.X / 2;

                Vector2 pos = new Vector2(textPositionLeft, Main.screenHeight / 2 - 200);
                float rand = player.finalText ? 3.5f : 2f;
                pos.X += Main.rand.NextFloat(-rand, rand);
                pos.Y += Main.rand.NextFloat(-rand, rand);
                Color color = player.finalText ? new Color(player.encounterTextAlpha, 0, 0, player.encounterTextAlpha) : new Color(player.encounterTextAlpha, player.encounterTextAlpha, player.encounterTextAlpha, player.encounterTextAlpha);
                DrawStringOutlined(spriteBatch, text, pos, color, 1f);
            }
        }
        #endregion
        public void Credits()
        {
            Vector2 monitorScale =  new Vector2((float)Main.screenWidth / 1920f, (float)Main.screenHeight / 1080f);
            var mod = ModLoader.GetMod("ElementsAwoken");
            var player = Main.player[Main.myPlayer].GetModPlayer<MyPlayer>();
            if (MyWorld.creditsCounter > player.screenDuration * 2) DrawStringOutlined(Main.spriteBatch, "Hold 'Escape' to skip", new Vector2 (Main.screenWidth - 220 * monitorScale.X, Main.screenHeight - 35 * monitorScale.Y), Color.White * (player.escHeldTimer > 0 ? 1 : 0.4f), 0.5f * monitorScale.Y);
            if (MyWorld.creditsCounter > 180 && MyWorld.creditsCounter < 480)
            {
                var logo = GetTexture("Extra/ElementsAwoken");
                float scale = 1.4f * monitorScale.Y;
                Color color = Color.White * GetFadeAlpha(MyWorld.creditsCounter - 180, 300); // old: (float)Math.Sin(MathHelper.Lerp(0, (float)Math.PI, ((float)MyWorld.creditsCounter - 300f) / 180f))
                Main.spriteBatch.Draw(logo, new Vector2(Main.screenWidth / 2 - ((logo.Width * scale) / 2), Main.screenHeight / 2 - 200 - ((logo.Height * scale) / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
            #region slide 1
            if (MyWorld.creditsCounter == player.screenDuration + 60)
            {
                string text = "Created by ThatOneJuicyOrange_";
                float scale = 1.3f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 300 * monitorScale.Y);
                DrawScreenText(text, 7 * 60, scale, pos);
            }
            int statueDuration = player.screenDuration - 60 * 3;
            int statueOffset = (player.screenDuration - statueDuration)/2;
            if (MyWorld.creditsCounter > player.screenDuration + statueOffset && MyWorld.creditsCounter < player.screenDuration * 2 - statueOffset)
            {
                var statue = GetTexture("Extra/Credits/ThatOneJuicyOrangeStatue");
                float scale = 1.5f * monitorScale.Y;
                Color color = Color.White * GetFadeAlpha(MyWorld.creditsCounter - player.screenDuration - statueOffset, statueDuration);
                Main.spriteBatch.Draw(statue, new Vector2(Main.screenWidth - 200 * monitorScale.X - (MyWorld.creditsCounter - player.screenDuration) / 2 - ((statue.Width * scale) / 2), Main.screenHeight / 2 - ((statue.Height * scale) / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
            #endregion
            #region slide 2
            if (MyWorld.creditsCounter == player.screenDuration * 2 + 60)
            {
                string text = "Music by";
                float scale = 1.3f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 330 * monitorScale.Y);
                DrawScreenText(text, 7 * 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 2 + 90)
            {
                string text = "Ranipla";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 220 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 2 + 120)
            {
                string text = "GENIH WAT";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 160 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 90, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 2 + 150)
            {
                string text = "Universe";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 100 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 120, scale, pos);
            }
            if (MyWorld.creditsCounter > player.screenDuration * 2 + statueOffset && MyWorld.creditsCounter < player.screenDuration * 3 - statueOffset)
            {
                var statue = GetTexture("Extra/Credits/RaniplaStatue");
                var statue2 = GetTexture("Extra/Credits/GenihWatStatue");
                float scale = 1.5f * monitorScale.Y;
                Color color = Color.White * GetFadeAlpha(MyWorld.creditsCounter - player.screenDuration * 2 - statueOffset, statueDuration);
                Main.spriteBatch.Draw(statue, new Vector2(Main.screenWidth - 200 * monitorScale.X - (MyWorld.creditsCounter - player.screenDuration * 2) / 2 - ((statue.Width * scale) / 2), Main.screenHeight / 2 - ((statue.Height * scale) / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(statue2, new Vector2(200 * monitorScale.X + (MyWorld.creditsCounter - player.screenDuration * 2) / 2 - ((statue2.Width * scale) / 2), Main.screenHeight / 2 - ((statue2.Height * scale) / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.FlipHorizontally, 0f);
            }
            #endregion
            #region slide 3
            if (MyWorld.creditsCounter == player.screenDuration * 3 + 60)
            {
                string text = "Lore by";
                float scale = 1.3f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 330 * monitorScale.Y);
                DrawScreenText(text, 7 * 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 3 + 90)
            {
                string text = "Burst";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 220 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 3 + 120)
            {
                string text = "Amadis";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 160 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 90, scale, pos);
            }
            if (MyWorld.creditsCounter > player.screenDuration * 3 + statueOffset && MyWorld.creditsCounter < player.screenDuration * 4 - statueOffset)
            {
                var statue = GetTexture("Extra/Credits/BurstStatue");
                var statue2 = GetTexture("Extra/Credits/AmadisStatue");
                float scale = 1.5f * monitorScale.Y;
                Color color = Color.White * GetFadeAlpha(MyWorld.creditsCounter - player.screenDuration * 3 - statueOffset, statueDuration);
                Main.spriteBatch.Draw(statue, new Vector2(Main.screenWidth - 200 * monitorScale.X - (MyWorld.creditsCounter - player.screenDuration * 3) / 2 - ((statue.Width * scale) / 2), Main.screenHeight / 2 - ((statue.Height * scale) / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(statue2, new Vector2(200 * monitorScale.X + (MyWorld.creditsCounter - player.screenDuration * 3) / 2 - ((statue2.Width * scale) / 2), Main.screenHeight / 2 - ((statue2.Height * scale) / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.FlipHorizontally, 0f);
            }
            #endregion
            #region slide 4
            if (MyWorld.creditsCounter == player.screenDuration * 4 + 60)
            {
                string text = "Sprites By";
                float scale = 1.3f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 330 * monitorScale.Y);
                DrawScreenText(text, 7 * 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 4 + 90)
            {
                string text = "Silvestre";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 220 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 60, scale, pos);
            }
            if (MyWorld.creditsCounter > player.screenDuration * 4 + statueOffset && MyWorld.creditsCounter < player.screenDuration * 5 - statueOffset)
            {
                var statue = GetTexture("Extra/Credits/Azana");
                float scale = 1.2f * monitorScale.Y;
                Color color = Color.White * GetFadeAlpha(MyWorld.creditsCounter - player.screenDuration * 4 - statueOffset, statueDuration);
                Main.spriteBatch.Draw(statue, new Vector2(200 * monitorScale.X + (MyWorld.creditsCounter - player.screenDuration * 4) / 2, Main.screenHeight / 2 - ((statue.Height * scale) / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.FlipHorizontally, 0f);
            }
            #endregion
            #region slide 5
            if (MyWorld.creditsCounter == player.screenDuration * 5 + 60)
            {
                string text = "Sprites By";
                float scale = 1.3f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 330 * monitorScale.Y);
                DrawScreenText(text, 7 * 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 5 + 90)
            {
                string text = "Aloe";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 220 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 60, scale, pos);
            }
            if (MyWorld.creditsCounter > player.screenDuration * 5 + statueOffset && MyWorld.creditsCounter < player.screenDuration * 6 - statueOffset)
            {
                float scale = 1.5f * monitorScale.Y;
                int offset = 30;
                Main.spriteBatch.Draw(GetTexture("Extra/Credits/Shimmerspark"), new Vector2(200 * monitorScale.X + (MyWorld.creditsCounter - player.screenDuration * 5) / 3, Main.screenHeight / 2 + 200 * monitorScale.Y), null, Color.White * GetFadeAlpha(MyWorld.creditsCounter - player.screenDuration * 5 - statueOffset, statueDuration), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(GetTexture("Extra/Credits/SolarGeneratorIV"), new Vector2(Main.screenWidth - 500 * monitorScale.X - (MyWorld.creditsCounter - player.screenDuration * 5) / 3, Main.screenHeight / 2 - 200 * monitorScale.Y), null, Color.White * GetFadeAlpha(MyWorld.creditsCounter - offset - player.screenDuration * 5 - statueOffset, statueDuration - offset), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(GetTexture("Extra/Credits/DesertGun"), new Vector2(500 * monitorScale.X + (MyWorld.creditsCounter - player.screenDuration * 5) / 3, Main.screenHeight / 2), null, Color.White * GetFadeAlpha(MyWorld.creditsCounter - offset * 2- player.screenDuration * 5 - statueOffset, statueDuration - offset * 2), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(GetTexture("Extra/Credits/AncientsSword"), new Vector2(Main.screenWidth - 400 * monitorScale.X - (MyWorld.creditsCounter - player.screenDuration * 5) / 3, Main.screenHeight / 2 + 100 * monitorScale.Y), null, Color.White * GetFadeAlpha(MyWorld.creditsCounter - offset * 3 - player.screenDuration * 5 - statueOffset, statueDuration - offset * 3), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(GetTexture("Extra/Credits/BurnerGenerator"), new Vector2(900 * monitorScale.X + (MyWorld.creditsCounter - player.screenDuration * 5) / 3, Main.screenHeight / 2 + 300 * monitorScale.Y), null, Color.White * GetFadeAlpha(MyWorld.creditsCounter - offset * 4 - player.screenDuration * 5 - statueOffset, statueDuration - offset * 4), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(GetTexture("Extra/Credits/DesertTrailers"), new Vector2(150 * monitorScale.X + (MyWorld.creditsCounter - player.screenDuration * 5) / 2, 100 * monitorScale.Y), null, Color.White * GetFadeAlpha(MyWorld.creditsCounter - offset * 5 - player.screenDuration * 5 - statueOffset, statueDuration - offset * 5), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
            #endregion
            #region slide 6
            if (MyWorld.creditsCounter == player.screenDuration * 6 + 60)
            {
                string text = "Sprites By";
                float scale = 1.3f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 330 * monitorScale.Y);
                DrawScreenText(text, 7 * 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 6 + 90)
            {
                string text = "NnickykunN";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 220 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 6 + 105)
            {
                string text = "Darkpuppey";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 160 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 75, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 6 + 120)
            {
                string text = "Skeletony";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 100 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 90, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 6 + 135)
            {
                string text = "Mayhemm";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 40 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 105, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 6 + 150)
            {
                string text = "daimgamer";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 + 20 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 120, scale, pos);
            }
            if (MyWorld.creditsCounter > player.screenDuration * 6 + statueOffset && MyWorld.creditsCounter < player.screenDuration * 7 - statueOffset)
            {
                float scale = 1.1f * monitorScale.Y;
                int offset = 45;
                Main.spriteBatch.Draw(GetTexture("Extra/Credits/AncientAmalgam"), new Vector2(200 * monitorScale.X + (MyWorld.creditsCounter - player.screenDuration * 6) / 3, Main.screenHeight / 2 + 200 * monitorScale.Y), null, Color.White * GetFadeAlpha(MyWorld.creditsCounter - player.screenDuration * 6 - statueOffset, statueDuration), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(GetTexture("Extra/Credits/ScourgeFighter"), new Vector2(Main.screenWidth - 500 * monitorScale.X - (MyWorld.creditsCounter - player.screenDuration * 6) / 3, Main.screenHeight / 2 - 200 * monitorScale.Y), null, Color.White * GetFadeAlpha(MyWorld.creditsCounter - offset - player.screenDuration * 6 - statueOffset, statueDuration - offset), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(GetTexture("Extra/Credits/Permafrost"), new Vector2(300 * monitorScale.X + (MyWorld.creditsCounter - player.screenDuration * 6) / 3, Main.screenHeight / 2 - 250 * monitorScale.Y), null, Color.White * GetFadeAlpha(MyWorld.creditsCounter - offset * 2 - player.screenDuration * 6 - statueOffset, statueDuration - offset * 2), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);               
            }
            #endregion
            #region slide 7
            if (MyWorld.creditsCounter == player.screenDuration * 7 + 60)
            {
                string text = "Donators";
                float scale = 1.3f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 330 * monitorScale.Y);
                DrawScreenText(text, 7 * 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 7 + 90)
            {
                string text = "Eoite\n" +
                    "ChamCham\n" +
                    "Aegida\n" +
                    "Buildmonger\n" +
                    "YukkiKun\n" +
                    "Superbaseball101\n" +
                    "Crow\n" +
                    "Lantard\n" +
                    "Megaswave\n" +
                    "Keydrian\n" +
                    "InstaFiz\n" +
                    "kREEpDABoom\n" +
                    "Big Spoon";
                float scale = 0.7f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale) + 45, Main.screenHeight / 2 - 220 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 60, scale, pos);
            }
            #endregion
            #region slide 8
            if (MyWorld.creditsCounter == player.screenDuration * 8 + 60)
            {
                string text = "Helpers";
                float scale = 1.3f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 330 * monitorScale.Y);
                DrawScreenText(text, 7 * 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 8 + 90)
            {
                string text = "Dradonhunter11\n" +
                    "jopojelly\n" +
                    "Misaro\n" +
                    "Alpaca121\n" +
                    "ReedemtheD3ad!\n" +
                    "Oinite12\n" +
                    "And many more";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 220 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 60, scale, pos);
            }
            #endregion
            #region slide 9
            if (MyWorld.creditsCounter == player.screenDuration * 9 + 60)
            {
                string text = "Special Thanks To";
                float scale = 1.3f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 330 * monitorScale.Y);
                DrawScreenText(text, 7 * 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 9 + 90)
            {
                string text = "ReLogic for creating the amazing Terraria\n" +
                    "Jofairden, jopojelly & bluemagic for creating tModloader\n" +
                    "Gameraiders101 for getting me into modding\n" +
                    "FuryForged for showcasing the mod\n" +
                    "ChippyGaming for showcasing the mod\n" +
                    "Gameraiders101 again for showcasing the mod";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale) + 60, Main.screenHeight / 2 - 220 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 60, scale, pos);
            }
            #endregion
            #region slide 10
            if (MyWorld.creditsCounter == player.screenDuration * 10 + 60)
            {
                string text = "Biggest Thanks";
                float scale = 1.3f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 360 * monitorScale.Y);
                DrawScreenText(text, 12 * 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 10 + 90)
            {
                string text = "To YOU";
                float scale = 1.4f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 290 * monitorScale.Y);
                DrawScreenText(text, 12 * 60 - 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 10 + 120)
            {
                string text = "Seriously, thank you so much for playing Elements Awoken.\nIt means a lot to me and all of the dev team that you can enjoy\nsomething we spent so much time on.";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 220 * monitorScale.Y);
                DrawScreenText(text, 12 * 60 - 90, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 10 + 180)
            {
                string text = "From- ThatOneJuicyOrange_ and the team <3";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale) + 300, Main.screenHeight / 2 + 40 * monitorScale.Y);
                DrawScreenText(text, 10 * 60, scale, pos);
            }
            #endregion
        }

        private float FindTextCenterX(string text, float scale)
        {
            Vector2 textSize = Main.fontDeathText.MeasureString(text) * scale;
            float textPositionLeft = Main.screenWidth / 2 - textSize.X / 2;
            return textPositionLeft;
        }
        public void DrawScreenText(string text, int duration, float scale, Vector2 pos)
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
                    screenTextPos[i] = pos;
                    break;
                }
            }
        }
        public float GetFadeAlpha(float timer, float duration)
        {
            if (timer < duration / 8) return timer / (duration / 8f);
            else if (timer > duration - (duration / 8)) return 1 - (timer- (duration - duration / 8f)) / (duration / 8f); // probably a better way to do this
            else return 1f;
        }
        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.myPlayer != -1 && !Main.gameMenu && Main.LocalPlayer.active)
            {
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
                if (ElementsAwoken.encounterTimer > 0)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Blank");
                }

                if (MyWorld.credits)
                {
                    music = MusicID.OverworldDay;
                }
            }
        }
        //boss checklist
        public override void PostSetupContent()
        {
            float wasteland = 2.5f;
            float toySlime = 5.1f;
            float infernace = 5.5f;
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

                bossChecklist.Call("AddBoss", infernace, NPCType("Infernace"), this, "Infernace", (Func<bool>)(() => MyWorld.downedInfernace), ItemType("InfernaceSummon"),
                    new List<int>() { ItemType("InfernaceMask"), ItemType("InfernaceTrophy") },
                    new List<int>() { ItemType("FireBlaster"), ItemType("FireHarpyStaff"), ItemType("FireHeart"), ItemType("FlareSword"), ItemType("InfernaceBag") },
                    "Use the [i:" + ItemType("InfernaceSummon") + "] in the underworld. An obsidian platform arena is reccomended on expert mode because of the lava slimes.",
                    "Infernace fades into the heatwaves...", default, default, true);

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
                    new List<int>() { ItemType("ObsidiousMask"), ItemType("ObsidiousTrophy"), ItemType("ObsidiousRobes"), ItemType("ObsidiousPants")},
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
                    new List<int>() { ItemType("Godslayer"), ItemType("InfernoStorm"), ItemType("TemplesWrath"), ItemType("FieryCore"), ItemType("GuardianBag")},
                    "Use an [i:" + ItemType("GuardianSummon") + "] at nighttime",
                    "The Guardian vanishes in a scene of flames", default, default, true);

                bossChecklist.Call("AddEvent", dotv, new List<int>() { NPCType("Immolator"), NPCType("ReaverSlime"), NPCType("ZergCaster"), NPCType("VoidKnight"), NPCType("EtherealHunter"), NPCType("VoidCrawler"), NPCType("VoidGolem") },
                    this, "Dawn of the Void", (Func<bool>)(() => MyWorld.downedVoidEvent), ItemType("VoidEventSummon"), ItemType("ShadeEgg"), new List<int>() { ItemType("CastersCurse"), ItemType("CrimsonShade"), ItemType("LifesLament"), ItemType("CrimsonShade"), ItemType("VoidJelly") },
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

                bossChecklist.Call("AddEvent", radiantRain, new List<int>() {  },
                   this, "Radiant Rain", (Func<bool>)(() => MyWorld.completedRadiantRain), ModContent.ItemType<RadiantRainSummon>(), default, new List<int>() {  },
                   "Has a 25% chance to occur instead of rain or can be summoned using a [i:" + ModContent.ItemType<RadiantRainSummon>() + "]",
                   default, default, default, default);

                bossChecklist.Call("AddMiniBoss", radiantRain, ModContent.NPCType<RadiantMaster>(), this, "Radiant Master", (Func<bool>)(() => MyWorld.downedRadiantMaster), default,
                    default,
                    new List<int>() { },
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
        // shake shake
        public override void ModifyTransformMatrix(ref SpriteViewMatrix Transform)
        {
            if (!Main.gameMenu)
            {
                Player player = Main.LocalPlayer;
                MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
                if (MyWorld.credits)
                {
                    if (!Main.gameMenu)
                    {
                        if (MyWorld.creditsCounter > modPlayer.screenTransDuration / 2)                    // so the screen doesnt go to the top corner before the transition happens
                        {
                            Main.screenPosition = modPlayer.desiredScPos - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2); // t he player gets stuck on blocks so this makes it smooth
                        }
                    }
                }
                if (!ModContent.GetInstance<Config>().screenshakeDisabled)
                {
                    if (modPlayer.screenshakeAmount >= 0)
                    {
                        modPlayer.screenshakeTimer++;
                        if (modPlayer.screenshakeTimer >= 5) modPlayer.screenshakeAmount -= 0.1f;
                    }
                    if (modPlayer.screenshakeAmount < 0)
                    {
                        modPlayer.screenshakeAmount = 0;
                        modPlayer.screenshakeTimer = 0;
                    }
                    Main.screenPosition += new Vector2(modPlayer.screenshakeAmount * Main.rand.NextFloat(), modPlayer.screenshakeAmount * Main.rand.NextFloat()); //NextFloat creates a random value between 0 and 1, multiply screenshake amount for a bit of variety
                }
            }
        }
        public static void ApplyScreenShakeToAll(float amount)
        {
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player player = Main.player[i];
                MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
                if (player.active)
                {
                    modPlayer.screenshakeAmount = amount;
                }
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

    enum ElementsAwokenMessageType : byte
    {
        StarHeartSync,
        AwakenedSync,
        EnergySync,
        Storyteller,
    }
}
