using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Terraria.DataStructures;

namespace ElementsAwoken.Items.Tech.Generators
{
    public class LifeforceBurner : ModItem
    {
        public bool enabled = false;
        public int lifeRegenAmount = 0;
        public int lifeRegenTimer = 0;

        public bool leftArrow = false;
        public int leftArrowCooldown = 0;
        public bool rightArrow = false;
        public int rightArrowCooldown = 0;

        public override bool CloneNewInstances
        {
            get { return true; }
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 44;

            item.rare = 1;

            item.maxStack = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lifeforce Burner Generator");
            Tooltip.SetDefault("Turns life into energy\nUse the arrow keys while hovering over the item to change amount\nRight Click to turn on");
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {

                TooltipLine lifeRegen = new TooltipLine(mod, "Elements Awoken:Tooltip", "Strength: " + lifeRegenAmount);
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


        }

        public override void UpdateInventory(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            if (enabled && modPlayer.energy < modPlayer.maxEnergy)
            {
                if (lifeRegenAmount > 0)
                {
                    player.lifeRegenCount = 0;

                    lifeRegenTimer += lifeRegenAmount;

                    if (lifeRegenTimer >= 120)
                    {
                        lifeRegenTimer -= 120;
                        modPlayer.energy++;
                        player.statLife -= 2;
                        if (player.statLife <= 0)
                        {
                            player.statLife = 0;
                            player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " boiled away"), 1.0, 0, false);
                        }
                    }
                }
            }
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
                        if (leftArrowCooldown <= 0 && lifeRegenAmount > 0)
                        {
                            lifeRegenAmount--;
                            leftArrowCooldown = 3;
                            Main.PlaySound(SoundID.MenuTick, player.position);
                        }
                    }
                    if (key == Keys.Right)
                    {
                        if (rightArrowCooldown <= 0 && lifeRegenAmount < 50)
                        {
                            lifeRegenAmount++;
                            rightArrowCooldown = 3;
                            Main.PlaySound(SoundID.MenuTick, player.position);
                        }
                    }
                }
            }
            if (!player.active || player.dead)
            {
                lifeRegenAmount = 0; // reset when dead in case player cant turn it off in time
            }
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            if (enabled)
            {
                enabled = false;
            }
            else
            {
                enabled = true;
            }
            item.stack++;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LifeCrystal, 4);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddRecipeGroup("ElementsAwoken:GoldBar", 6);
            recipe.AddIngredient(null, "GoldWire", 6);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
