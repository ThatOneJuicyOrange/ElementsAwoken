using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ElementsAwoken.Items.Tech.Accessories.Tier6
{
    public class GravityCore : ModItem
    {
        public bool enabled = false;
        public int gravityPercent = 100;

        public bool leftArrow = false;
        public int leftArrowCooldown = 0;
        public bool rightArrow = false;
        public int rightArrowCooldown = 0;

        public int energyCD = 0;
        public override bool CloneNewInstances
        {
            get { return true; }
        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 7;    

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gravity Core");
            Tooltip.SetDefault("Allows the player to control the gravity\nUse the arrow keys while hovering over the item to change strength\nPress G on the item to reset gravity to normal\nRight Click to turn on");
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine lifeRegen = new TooltipLine(mod, "Elements Awoken:Tooltip", "Gravity: " + gravityPercent + "%"); 
            tooltips.Insert(1, lifeRegen);

            TooltipLine tip = new TooltipLine(mod, "Elements Awoken:Tooltip", "");
            if (enabled)
            {
                tip = new TooltipLine(mod, "Elements Awoken:Tooltip", "Enabled");
                tip.overrideColor = Color.Green;
            }
            else
            {
                tip = new TooltipLine(mod, "Elements Awoken:Tooltip", "Disabled");
                tip.overrideColor = Color.Red;
            }
            tooltips.Insert(1, tip);
            TooltipLine warning = new TooltipLine(mod, "Elements Awoken:Tooltip", "WARNING: At high gravity there may be undesired effects");
            warning.overrideColor = Color.Red;
            tooltips.Add(warning);
        }
        public override void UpdateInventory(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            if (enabled)
            {
                energyCD--;
                leftArrowCooldown--;
                rightArrowCooldown--;
                if (Main.HoverItem.type == item.type)
                {
                    Keys[] pressedKeys = Main.keyState.GetPressedKeys();

                    for (int j = 0; j < pressedKeys.Length; j++)
                    {
                        Keys key = pressedKeys[j];
                        if (key == Keys.Left)
                        {
                            if (leftArrowCooldown <= 0 && gravityPercent > -100)
                            {
                                gravityPercent--;
                                leftArrowCooldown = 3;
                                Main.PlaySound(SoundID.MenuTick, player.position);
                            }
                        }
                        if (key == Keys.Right)
                        {
                            if (rightArrowCooldown <= 0 && gravityPercent < 300)
                            {
                                gravityPercent++;
                                rightArrowCooldown = 3;
                                Main.PlaySound(SoundID.MenuTick, player.position);
                            }
                        }
                        if (key == Keys.G)
                        {
                            gravityPercent = 100;
                        }
                    }
                }
                if (energyCD <= 0)
                {
                    modPlayer.energy--;
                    energyCD = 60;
                }
                if (modPlayer.energy > 0)
                {
                    player.gravity = 0.4f * (gravityPercent / 100);
                }
            }
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            enabled = !enabled;
            item.stack++;
        }
        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(enabled);
            writer.Write(gravityPercent);
        }

        public override void NetRecieve(BinaryReader reader)
        {
            enabled = reader.ReadBoolean();
            gravityPercent = reader.ReadInt32();
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                { "enabled", enabled},
                { "gravity", gravityPercent}
            };
        }

        public override void Load(TagCompound tag)
        {
            enabled = tag.GetBool("enabled");
            gravityPercent = tag.GetInt("gravity");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofFlight, 18);
            recipe.AddIngredient(null, "GoldWire", 10);
            recipe.AddIngredient(null, "SiliconBoard", 1);
            recipe.AddIngredient(null, "Microcontroller", 2);
            recipe.AddIngredient(null, "Capacitor", 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
