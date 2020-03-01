using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ElementsAwoken.Items.Tech.Generators
{
    public class BiofuelBurner : ModItem
    {
        public bool enabled = false;
        public int fuel = 0;
        public int producePowerCooldown = 0;
        public int producePowerCooldownMax = 60;
        public int powerLevel = 0;
        public string fuelItemType = "";
        public override bool CloneNewInstances
        {
            get { return true; }
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 44;

            item.rare = 6;

            item.maxStack = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Biofuel Burner Generator");
            Tooltip.SetDefault("Consumes certain plant materials in the inventory\nDifferent plants have different power outputs\nRight Click to turn on");
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (enabled)
            {
                if (powerLevel != 0)
                {
                    // make it 1 decimal place
                    float powerPerSec = (float)producePowerCooldownMax / 60f;
                    string ppsString = powerPerSec.ToString("n1");
                    TooltipLine fuelQuality = new TooltipLine(mod, "Elements Awoken:Tooltip", "Produces " + powerLevel + " energy every " + ppsString + " seconds");
                    tooltips.Insert(1, fuelQuality);
                }
                TooltipLine fuelType = new TooltipLine(mod, "Elements Awoken:Tooltip", "Fuel Type: " + fuelItemType);
                tooltips.Insert(1, fuelType);

                TooltipLine fuelRemaining = new TooltipLine(mod, "Elements Awoken:Tooltip", "Fuel: " + fuel);
                tooltips.Insert(1, fuelRemaining);
            }
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
                for (int i = 0; i < 50; i++)
                {
                    Item plant = Main.LocalPlayer.inventory[i];
                    if (fuel <= 0)
                    {
                        // flowers
                        if (plant.type == ItemID.Blinkroot)
                        {
                            ConsumePlant(plant, 900, plant.Name, 1, 60);
                        }
                        if (plant.type == ItemID.Daybloom)
                        {
                            ConsumePlant(plant, 600, plant.Name, 1, 60);
                        }
                        if (plant.type == ItemID.Deathweed)
                        {
                            ConsumePlant(plant, 900, plant.Name, 1, 60);
                        }
                        if (plant.type == ItemID.Fireblossom)
                        {
                            ConsumePlant(plant, 600, plant.Name, 2, 60);
                        }
                        if (plant.type == ItemID.Moonglow)
                        {
                            ConsumePlant(plant, 1500, plant.Name, 1, 120);
                        }
                        if (plant.type == ItemID.Shiverthorn)
                        {
                            ConsumePlant(plant, 900, plant.Name, 1, 60);
                        }
                        if (plant.type == ItemID.Waterleaf)
                        {
                            ConsumePlant(plant, 900, plant.Name, 1, 60);
                        }
                        //mushrooms
                        if (plant.type == ItemID.Mushroom)
                        {
                            ConsumePlant(plant, 900, plant.Name, 1, 60);
                        }
                        if (plant.type == ItemID.GlowingMushroom)
                        {
                            ConsumePlant(plant, 200, plant.Name, 1, 90);
                        }
                        if (plant.type == ItemID.VileMushroom)
                        {
                            ConsumePlant(plant, 1200, plant.Name, 1, 60);
                        }
                        if (plant.type == ItemID.ViciousMushroom)
                        {
                            ConsumePlant(plant, 1200, plant.Name, 1, 60);
                        }
                        // other
                        if (plant.type == ItemID.Cactus)
                        {
                            ConsumePlant(plant, 200, plant.Name, 1, 90);
                        }
                    }
                }
            }
            if (fuel <= 0)
            {
                fuelItemType = "Empty";
                powerLevel = 0;
            }
            if (fuel > 0)
            {
                fuel--;
            }
            producePowerCooldown--;
            if (fuel > 0 && producePowerCooldown <= 0)
            {
                producePowerCooldown = producePowerCooldownMax;
                modPlayer.energy += 1;
            }
        }   
        private void ConsumePlant(Item plant, int fuelAmount, string fuelName, int quality, int powerCooldown)
        {
            fuel = fuelAmount;
            fuelItemType = fuelName;
            powerLevel = quality;
            producePowerCooldownMax = powerCooldown;
            plant.stack--;
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
            recipe.AddIngredient(ItemID.ChlorophyteBar, 8);
            recipe.AddIngredient(null, "MysticLeaf", 1);
            recipe.AddIngredient(null, "SiliconBoard", 3);
            recipe.AddIngredient(null, "Transistor", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
