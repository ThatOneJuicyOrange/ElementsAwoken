using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ElementsAwoken.Items.Consumable.Potions
{
    public class MysteriousPotion : ModItem
    {
        public int potionNum = 0; 
        public override bool CloneNewInstances
        {
            get { return true; }
        }
        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(potionNum);
        }

        public override void NetRecieve(BinaryReader reader)
        {
            potionNum = reader.ReadInt32();
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                {
                    "potionNum", potionNum
                }
            };
        }

        public override void Load(TagCompound tag)
        {
            potionNum = tag.GetInt("potionNum");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;

            item.useTurn = true;
            item.consumable = true;

            item.useAnimation = 17;
            item.useTime = 17;
            item.UseSound = SoundID.Item3;
            item.useStyle = 2;

            item.maxStack = 1;

            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = 1;
            //item.potion = true;

            item.buffType = BuffID.Regeneration;
            item.buffTime = 0;
            return;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mysterious Potion");
            Tooltip.SetDefault("It has a foul smell about it");
        }
        public override bool OnPickup(Player player)
        {
            if (ModContent.GetInstance<Config>().debugMode) potionNum = Main.rand.Next(10);
            return base.OnPickup(player);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            MyPlayer modPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();

            string name = EAWorldGen.mysteriousPotionColours[potionNum];
            string desc = "It has a foul smell about it";
            if (modPlayer.mysteriousPotionsDrank[potionNum])
            {
                switch (potionNum)
                {
                    case 0:
                        name = "Instant Health";
                        desc = "Heals you for 20% of your total health";
                        break;
                    case 1:
                        name = "Instant Damage";
                        desc = "Damages you for 20% of your total health";
                        break;
                    case 2:
                        name = "Toxic";
                        desc = "Reduces your maximum health by 10%";
                        break;
                    case 3:
                        name = "Healthy";
                        desc = "Increases your maximum health by 10%";
                        break;
                    case 4:
                        name = "Invincibility";
                        desc = "Makes you invincible for 10 seconds";
                        break;
                    case 5:
                        name = "Intensity";
                        desc = "Increases damage by 10%";
                        break;
                    case 6:
                        name = "Bright";
                        desc = "Makes you release blinding light";
                        break;
                    case 7:
                        name = "Poison";
                        desc = "Poisons you";
                        break;
                    case 8:
                        name = "Snail";
                        desc = "Makes you move very slowly";
                        break;
                    case 9:
                        name = "Lightning";
                        desc = "Makes you move at uncontrollable speeds";
                        break;
                }
            }
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name.StartsWith("Tooltip"))
                {
                    line2.text = desc;
                    if (ModContent.GetInstance<Config>().debugMode)
                    {
                        line2.text += "\npotionNum:" + potionNum + "\ncolor:" + EAWorldGen.mysteriousPotionColours[potionNum];
                    }
                }
                if (line2.mod == "Terraria" && line2.Name.Contains("ItemName"))
                {
                    line2.text = name + " Potion";
                }
            }
        }
        public override bool CanUseItem(Player player)
        {
            if (potionNum == 0 && player.FindBuffIndex(BuffID.PotionSickness) != -1)
            {
                return false;
            }
                return base.CanUseItem(player);
        }
        public override bool ConsumeItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (potionNum == 0)
            {
                int amount = (int)(player.statLifeMax2 * 0.2f);
                player.statLife += amount;
                player.HealEffect(amount);
                player.AddBuff(BuffID.PotionSickness, player.pStone ? 2700 : 3600);
            }
            else if (potionNum == 1)
            {
                int amount = (int)(player.statLifeMax2 * 0.2f);
                player.statLife -= amount;
                if (player.statLife < 0) player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " had a bad swig"),1,1);
                CombatText.NewText(player.getRect(), Color.OrangeRed, -amount + " Health");
            }
            else if (potionNum == 2)player.AddBuff(mod.BuffType("RottenHeart"), 3600);
            else if (potionNum == 3) player.AddBuff(mod.BuffType("StrongHeart"), 3600);
            else if (potionNum == 4) player.AddBuff(mod.BuffType("Invincibility"), 600);
            else if (potionNum == 5) player.AddBuff(mod.BuffType("GoldenWeapons"), 1800);
            else if (potionNum == 6) player.AddBuff(mod.BuffType("Glowing"), 1800);
            else if (potionNum == 7) player.AddBuff(BuffID.Poisoned, 3600);
            else if (potionNum == 8) player.AddBuff(mod.BuffType("SuperSlow"), 1200);
            else if (potionNum == 9) player.AddBuff(mod.BuffType("SuperSpeed"), 600);

            modPlayer.mysteriousPotionsDrank[potionNum] = true;
            if (ModContent.GetInstance<Config>().debugMode) item.stack++;
            return base.ConsumeItem(player);
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Color color = Color.Red;
            if (EAWorldGen.mysteriousPotionColours[potionNum] == "Red") color = Color.Red;
            else if (EAWorldGen.mysteriousPotionColours[potionNum] == "Cyan") color = Color.Cyan;
            else if (EAWorldGen.mysteriousPotionColours[potionNum] == "Lime") color = Color.LimeGreen;
            else if (EAWorldGen.mysteriousPotionColours[potionNum] == "Fuschia") color = Color.HotPink;
            else if (EAWorldGen.mysteriousPotionColours[potionNum] == "Pink") color = Color.LightPink;
            else if (EAWorldGen.mysteriousPotionColours[potionNum] == "Brown") color = Color.RosyBrown;
            else if (EAWorldGen.mysteriousPotionColours[potionNum] == "Orange") color = Color.Orange;
            else if (EAWorldGen.mysteriousPotionColours[potionNum] == "Yellow") color = Color.Yellow;
            else if (EAWorldGen.mysteriousPotionColours[potionNum] == "Blue") color = Color.DarkBlue;
            else if (EAWorldGen.mysteriousPotionColours[potionNum] == "Purple") color = Color.Purple;

            Texture2D tex = mod.GetTexture("Items/Consumable/Potions/MysteriousPotionContents");
            spriteBatch.Draw(tex, position, frame, color, 0f, origin, scale, SpriteEffects.None, 0f);
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Color color = Color.Red;
            if (EAWorldGen.mysteriousPotionColours[potionNum] == "Red") color = Color.Red;
            else if (EAWorldGen.mysteriousPotionColours[potionNum] == "Cyan") color = Color.Cyan;
            else if (EAWorldGen.mysteriousPotionColours[potionNum] == "Lime") color = Color.LimeGreen;
            else if (EAWorldGen.mysteriousPotionColours[potionNum] == "Fuschia") color = Color.DeepPink;
            else if (EAWorldGen.mysteriousPotionColours[potionNum] == "Pink") color = Color.LightPink;
            else if (EAWorldGen.mysteriousPotionColours[potionNum] == "Brown") color = Color.RosyBrown;
            else if (EAWorldGen.mysteriousPotionColours[potionNum] == "Orange") color = Color.Orange;
            else if (EAWorldGen.mysteriousPotionColours[potionNum] == "Yellow") color = Color.LightYellow;
            else if (EAWorldGen.mysteriousPotionColours[potionNum] == "Blue") color = Color.DarkBlue;
            else if (EAWorldGen.mysteriousPotionColours[potionNum] == "Purple") color = Color.Purple;
            Texture2D tex = mod.GetTexture("Items/Consumable/Potions/MysteriousPotionContents");
            spriteBatch.Draw(tex, item.position, null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}
