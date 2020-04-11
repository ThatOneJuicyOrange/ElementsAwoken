using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Town
{
    [AutoloadHead]
    public class Psychologist : ModNPC
    {
        public override bool Autoload(ref string name)
        {
            return mod.Properties.Autoload;
        }
        public override string Texture
        {
            get
            {
                return "ElementsAwoken/NPCs/Town/Psychologist";
            }
        }
        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 40;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 10;
            npc.defense = 30;
            npc.lifeMax = 250;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 90;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
            animationType = NPCID.Wizard;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Psychologist");
            Main.npcFrameCount[npc.type] = 25;
        }
        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            if (MyWorld.downedToySlime && MyWorld.awakenedMode)
            {
                return true;
            }
            return false;
        }
        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            return true;
        }

        public override string TownNPCName()
        {                                       //NPC names
            switch (WorldGen.genRand.Next(4))
            {
                case 0:
                    return "Drew";
                case 1:
                    return "Sam";
                case 2:
                    return "Alex";
                case 3:
                    return "Dan";
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
            Player player = Main.LocalPlayer;
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>();

            string text = "";
            switch (Main.rand.Next(4))
            {
                case 0: text = "And, how are we feeling today?"; break;
                case 1: text = "I hear there is a 'Book of Shadows' that shows what is affecting your sanity. I'm sure it isnt as effective as my services."; break;
                case 2: text = "I am the best psychologist around...and most likely the only one."; break;
                case 3: text = "Did you see my glasses? I can't seem to find them...oh, there they are. What do you want?"; break;
                default:
                    return "default";
            }
            if (Main.rand.Next(10) == 0 && Main.hardMode)
            {
                text = "To be honest, I'm surprised by the fact that you don't have an trauma - considering you fought an gigantic wall made out of flesh...";
            }
            if (Main.rand.Next(10) == 0 && MyWorld.downedVolcanox && !MyWorld.downedVoidLeviathan)
            {
                text = "The townspeople seem plagued by hallucinations...luckily I am not. Anyways, who's your zombie friend there?";
            }
            if (Main.rand.Next(10) == 0 && modPlayer.sanity < 50)
            {
                text = "Your sanity is pretty low...you should maybe see a psychologist. Oh, wait...";
            }
            if (Main.rand.Next(10) == 0 && !Main.dayTime)
            {
                text = "The fact that we never sleep is extremely strange. Who coded us to be this way?";
            }
            if (Main.rand.Next(10) == 0 && NPC.downedMoonlord)
            {
                text = "You took down a being said to make others grow insane with alone it's presence. Are you even human?";
            }
            if (Main.bloodMoon)
            {
                text = "I'm doing fine. I'm ABSOLUTELY DOING FINE. NOW STOP ASKING!";
            }
            return text;
        }
        public override void SetChatButtons(ref string button, ref string button2)
        {
            int price = (int)(Item.buyPrice(0,0,30,0) * GetPriceScale());

            button = Language.GetTextValue("Counselling" + " (" + GetPriceString(price) + ")");
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 29);
                Player player = Main.LocalPlayer;
                player.AddBuff(mod.BuffType("PsychologistSanity"), 3600 * 3);
                Main.LocalPlayer.BuyItem((int)(Item.buyPrice(0, 0, 30, 0) * GetPriceScale()), -1);
            }
        }  
        private string GetPriceString(int price)
        {
            string text2 = "";
            int platinum = 0;
            int gold = 0;
            int silver = 0;
            int copper = 0;

            if (price >= 1000000)
            {
                platinum = price / 1000000;
                price -= platinum * 1000000;
            }
            if (price >= 10000)
            {
                gold = price / 10000;
                price -= gold * 10000;
            }
            if (price >= 100)
            {
                silver = price / 100;
                price -= silver * 100;
            }
            if (price >= 1)
            {
                copper = price;
            }
            if (platinum > 0)
            {
                text2 = string.Concat(new object[]
                {
                        text2,
                        platinum,
                        " ",
                        Lang.inter[15].Value,
                        " "
                });
            }
            if (gold > 0)
            {
                text2 = string.Concat(new object[]
                {
                        text2,
                        gold,
                        " ",
                        Lang.inter[16].Value,
                        " "
                });
            }
            if (silver > 0)
            {
                text2 = string.Concat(new object[]
                {
                        text2,
                        silver,
                        " ",
                        Lang.inter[17].Value,
                        " "
                });
            }
            if (copper > 0)
            {
                text2 = string.Concat(new object[]
                {
                        text2,
                        copper,
                        " ",
                        Lang.inter[18].Value,
                        " "
                });
            }
            return text2;
        }
        public override void AI()
        {
            if (!MyWorld.awakenedMode) npc.active = false;
        }
        private float GetPriceScale()
        {
            float scale = 0.5f;
            if (NPC.downedSlimeKing) scale += 0.25f;
            if (NPC.downedBoss1) scale += 0.25f;
            if (MyWorld.downedWasteland) scale += 0.25f;
            if (NPC.downedBoss2) scale += 0.25f;
            if (NPC.downedBoss3) scale += 0.5f;
            if (MyWorld.downedInfernace) scale += 0.5f;
            if (Main.hardMode) scale += 4f;
            if (NPC.downedMechBossAny) scale += 2f;
            if (MyWorld.downedScourgeFighter) scale += 1f;
            if (MyWorld.downedRegaroth) scale += 2f;
            if (NPC.downedPlantBoss) scale += 2f;
            if (MyWorld.downedPermafrost) scale += 2f;
            if (MyWorld.downedObsidious) scale += 1f;
            if (NPC.downedFishron) scale += 2f;
            if (MyWorld.downedAqueous) scale += 2f;
            if (NPC.downedMoonlord) scale += 10f;
            if (MyWorld.downedGuardian) scale += 3f;
            if (MyWorld.downedVolcanox) scale += 3f;
            if (MyWorld.downedVoidLeviathan) scale += 3f;
            if (MyWorld.downedAzana || MyWorld.sparedAzana) scale += 3f;
            if (MyWorld.downedAncients) scale += 3f;
            return scale;
        }
    }
}
