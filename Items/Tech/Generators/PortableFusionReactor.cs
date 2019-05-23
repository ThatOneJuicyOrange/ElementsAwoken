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
    public class PortableFusionReactor : ModItem
    {
        public bool enabled = false;
        public int fuel = 0;
        public int producePowerCooldown = 0;

        public override bool CloneNewInstances
        {
            get { return true; }
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 44;

            item.rare = 10;

            item.maxStack = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Portable Fusion Reactor");
            Tooltip.SetDefault("Consumes 2 luminite ore to generate power\nProduces 15 seconds of power\nCreates lunar waste which harms the player if it builds up too much\nRight click to turn on");
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (enabled)
            {
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
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>(mod);
            if (enabled && modPlayer.energy < modPlayer.maxEnergy)
            {
                for (int i = 0; i < 50; i++)
                {
                    Item other = Main.LocalPlayer.inventory[i];
                    if (other.type == ItemID.LunarOre)
                    {
                        if (fuel <= 0 && other.stack >= 2)
                        {
                            fuel = 900;
                            other.stack -= 2;
                            player.QuickSpawnItem(mod.ItemType("LunarWaste"));
                        }
                    }
                }

                if (fuel > 0)
                {
                    fuel--;
                }
                producePowerCooldown--;
                if (fuel > 0 && producePowerCooldown <= 0)
                {
                    producePowerCooldown = 20;
                    modPlayer.energy += 3;
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
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(null, "CopperWire", 8);
            recipe.AddIngredient(null, "GoldWire", 5);
            recipe.AddIngredient(null, "SiliconBoard", 2);
            recipe.AddIngredient(null, "Microcontroller", 1);
            recipe.AddIngredient(null, "PortableFissionReactor", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
