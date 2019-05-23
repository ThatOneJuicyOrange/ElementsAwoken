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
    public class SolarGeneratorII : ModItem
    {
        public int productionAmount = 1;
        public int producePowerCooldown = 0;
        public int producePowerCooldownMax = 60;

        public override bool CloneNewInstances
        {
            get { return true; }
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 44;

            item.rare = 5;

            item.maxStack = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solar Generator MKII");
            Tooltip.SetDefault("Generates power during the day\nGenerates more power the higher the sun is in the sky");
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            float powerPerSec = (float)producePowerCooldownMax / 60f;
            string ppsString = powerPerSec.ToString("n1");
            TooltipLine powerOutput = new TooltipLine(mod, "Elements Awoken:Tooltip", "Power Output: " + productionAmount + " energy every " + ppsString + " seconds");
            if (Main.dayTime)
            {
                powerOutput = new TooltipLine(mod, "Elements Awoken:Tooltip", "Power Output: " + productionAmount + " energy every " + ppsString + " seconds");
            }
            else
            {
                powerOutput = new TooltipLine(mod, "Elements Awoken:Tooltip", "Inactive");
            }
            tooltips.Insert(1, powerOutput);
        }

        public override void UpdateInventory(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>(mod);
            if (modPlayer.energy < modPlayer.maxEnergy && Main.dayTime)
            {
                producePowerCooldownMax = (int)MathHelper.Lerp(60, 300, MathHelper.Distance((float)Main.time, 27000) / 27000);
                productionAmount = (int)Math.Round(MathHelper.Lerp(3, 1, MathHelper.Distance((float)Main.time, 27000) / 27000));

                producePowerCooldown--;
                if (producePowerCooldown <= 0)
                {
                    producePowerCooldown = producePowerCooldownMax;
                    modPlayer.energy += productionAmount;
                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SolarGeneratorI", 1);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddIngredient(ItemID.SoulofLight, 6);
            recipe.AddIngredient(null, "GoldWire", 4);
            recipe.AddIngredient(null, "Capacitor", 2);
            recipe.AddIngredient(null, "SiliconBoard", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
