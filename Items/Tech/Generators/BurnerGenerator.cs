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
    public class BurnerGenerator : ModItem
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

            item.rare = 1;

            item.maxStack = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Burner Generator");
            Tooltip.SetDefault("Consumes raw wood in the inventory to produce 1 energy per second\n1 wood burns for 15 seconds\nRight click to turn on");
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
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            if (enabled && modPlayer.energy < modPlayer.maxEnergy)
            {
                for (int i = 0; i < 50; i++)
                {
                    Item wood = Main.LocalPlayer.inventory[i];
                    if (wood.type == ItemID.Wood ||
                        wood.type == ItemID.RichMahogany ||
                        wood.type == ItemID.Ebonwood ||
                        wood.type == ItemID.Shadewood ||
                        wood.type == ItemID.Pearlwood ||
                        wood.type == ItemID.BorealWood ||
                        wood.type == ItemID.PalmWood ||
                        wood.type == ItemID.DynastyWood ||
                        wood.type == ItemID.SpookyWood)
                    {
                        if (fuel <= 0)
                        {
                            fuel = 900;
                            wood.stack--;
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
                    producePowerCooldown = 60;
                    modPlayer.energy += 1;
                }
            }
            //Main.NewText(modPlayer.energy + " " + modPlayer.maxEnergy);
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
            recipe.AddIngredient(ItemID.Wood, 24);
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddRecipeGroup("ElementsAwoken:SilverBar", 3);
            recipe.AddIngredient(null, "CopperWire", 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
