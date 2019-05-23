using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;

namespace ElementsAwoken.Items.Tech.Generators
{
    public class WindGenerator : ModItem
    {
        public bool enabled = true;
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

            item.rare = 1;

            item.maxStack = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wind Generator");
            Tooltip.SetDefault("Produces energy based on the wind speed\nRight click to turn on");
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (enabled)
            {
                int windMPH = (int)(Main.windSpeed * 100f);
                string windspeed = "";
                if (windMPH < 0)
                {
                    windspeed += Language.GetTextValue("GameUI.WestWind", Math.Abs(windMPH));
                }
                else if (windMPH > 0)
                {
                    windspeed += Language.GetTextValue("GameUI.EastWind", windMPH);
                }

                TooltipLine windSpd = new TooltipLine(mod, "Elements Awoken:Tooltip", "Wind speed: " + windspeed);
                tooltips.Insert(1, windSpd);
                float powerPerSec = (float)producePowerCooldownMax / 60f;
                string ppsString = powerPerSec.ToString("n1");
                TooltipLine powerCooldown = new TooltipLine(mod, "Elements Awoken:Tooltip", "Production Rate: 1 energy every " + ppsString + " seconds");
                tooltips.Insert(1, powerCooldown);
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
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>(mod);
            if (enabled)
            {
                // maximum wind is: 50mph?
                producePowerCooldownMax = (int)MathHelper.Lerp(300, 20, (Main.windSpeed * 100) / 50); 
                producePowerCooldown--;
                if (producePowerCooldown <= 0)
                {
                    producePowerCooldown = producePowerCooldownMax;
                    modPlayer.energy += 1;
                }
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
    }
}
