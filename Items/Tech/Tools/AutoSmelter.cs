using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace ElementsAwoken.Items.Tech.Tools
{
    public class AutoSmelter : ModItem
    {
        public bool enabled = false;
        public int smeltCooldown = 0;

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
            DisplayName.SetDefault("Auto Smelter");
            Tooltip.SetDefault("Automatically turns ores into bars\nConsumes energy to smelt bars\nRight Click to turn on");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 4));
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
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
            int energyConsumed = 2;
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            smeltCooldown--;
            if (enabled)
            {
                if (smeltCooldown <= 0)
                {
                    for (int i = 0; i < 50; i++)
                    {
                        Item other = Main.LocalPlayer.inventory[i];

                        if (other.type == ItemID.CopperOre && other.stack >= 3)
                        {
                            smeltCooldown = 90;
                            modPlayer.energy -= energyConsumed;

                            other.stack -= 3;
                            player.QuickSpawnItem(ItemID.CopperBar);
                            break;
                        }
                        if (other.type == ItemID.TinOre && other.stack >= 3)
                        {
                            smeltCooldown = 90;
                            modPlayer.energy -= energyConsumed;

                            other.stack -= 3;
                            player.QuickSpawnItem(ItemID.TinBar);
                            break;
                        }
                        if (other.type == ItemID.IronOre && other.stack >= 3)
                        {
                            smeltCooldown = 90;
                            modPlayer.energy -= energyConsumed;

                            other.stack -= 3;
                            player.QuickSpawnItem(ItemID.IronBar);
                            break;
                        }
                        if (other.type == ItemID.LeadOre && other.stack >= 3)
                        {
                            smeltCooldown = 90;
                            modPlayer.energy -= energyConsumed;

                            other.stack -= 3;
                            player.QuickSpawnItem(ItemID.LeadBar);
                            break;
                        }
                        if (other.type == ItemID.SilverOre && other.stack >= 4)
                        {
                            smeltCooldown = 90;
                            modPlayer.energy -= energyConsumed;

                            other.stack -= 4;
                            player.QuickSpawnItem(ItemID.SilverBar);
                            break;
                        }
                        if (other.type == ItemID.TungstenOre && other.stack >= 4)
                        {
                            smeltCooldown = 90;
                            modPlayer.energy -= energyConsumed;

                            other.stack -= 4;
                            player.QuickSpawnItem(ItemID.TungstenBar);
                            break;
                        }
                        if (other.type == ItemID.GoldOre && other.stack >= 4)
                        {
                            smeltCooldown = 90;
                            modPlayer.energy -= energyConsumed;

                            other.stack -= 4;
                            player.QuickSpawnItem(ItemID.GoldBar);
                            break;
                        }
                        if (other.type == ItemID.PlatinumOre && other.stack >= 4)
                        {
                            smeltCooldown = 90;
                            modPlayer.energy -= energyConsumed;

                            other.stack -= 4;
                            player.QuickSpawnItem(ItemID.PlatinumBar);
                            break;
                        }
                        if (other.type == ItemID.Meteorite && other.stack >= 3)
                        {
                            smeltCooldown = 90;
                            modPlayer.energy -= energyConsumed;

                            other.stack -= 3;
                            player.QuickSpawnItem(ItemID.MeteoriteBar);
                            break;
                        }
                        if (other.type == ItemID.DemoniteOre && other.stack >= 3)
                        {
                            smeltCooldown = 90;
                            modPlayer.energy -= energyConsumed;

                            other.stack -= 3;
                            player.QuickSpawnItem(ItemID.DemoniteBar);
                            break;
                        }
                        if (other.type == ItemID.CrimtaneOre && other.stack >= 3)
                        {
                            smeltCooldown = 90;
                            modPlayer.energy -= energyConsumed;

                            other.stack -= 3;
                            player.QuickSpawnItem(ItemID.CrimtaneBar);
                            break;
                        }
                        if (other.type == ItemID.CobaltOre && other.stack >= 3)
                        {
                            smeltCooldown = 90;
                            modPlayer.energy -= energyConsumed;

                            other.stack -= 3;
                            player.QuickSpawnItem(ItemID.CobaltBar);
                            break;
                        }
                        if (other.type == ItemID.PalladiumOre && other.stack >= 3)
                        {
                            smeltCooldown = 90;
                            modPlayer.energy -= energyConsumed;

                            other.stack -= 3;
                            player.QuickSpawnItem(ItemID.PalladiumBar);
                            break;
                        }
                        if (other.type == ItemID.MythrilOre && other.stack >= 4)
                        {
                            smeltCooldown = 90;
                            modPlayer.energy -= energyConsumed;

                            other.stack -= 4;
                            player.QuickSpawnItem(ItemID.MythrilBar);
                            break;
                        }
                        if (other.type == ItemID.OrichalcumOre && other.stack >= 4)
                        {
                            smeltCooldown = 90;
                            modPlayer.energy -= energyConsumed;

                            other.stack -= 4;
                            player.QuickSpawnItem(ItemID.OrichalcumBar);
                            break;
                        }
                    }
                }
                // do hellstone last, must be in a seperate if so it is run afterwards
                if (smeltCooldown <= 0)
                {                    
                    // hellstone
                    Item hellOre = null;
                    Item obsidian = null;
                    for (int i = 0; i < 50; i++)
                    {
                        Item other = Main.LocalPlayer.inventory[i];
                        if (other.type == ItemID.Hellstone)
                        {
                            hellOre = other;
                        }
                        if (other.type == ItemID.Obsidian)
                        {
                            obsidian = other;
                        }
                    }
                    if ((hellOre != null && hellOre.stack >= 3) && (obsidian != null && obsidian.stack >= 1))
                    {
                        smeltCooldown = 90;
                        modPlayer.energy -= energyConsumed;

                        hellOre.stack -= 3;
                        obsidian.stack--;
                        player.QuickSpawnItem(ItemID.HellstoneBar);
                    }
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
            recipe.AddIngredient(ItemID.Hellforge, 1);
            recipe.AddIngredient(ItemID.HellstoneBar, 3);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddIngredient(null, "CopperWire", 2);
            recipe.AddIngredient(null, "GoldWire", 6);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
