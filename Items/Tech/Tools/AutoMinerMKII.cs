using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.DataStructures;
using ElementsAwoken.Tiles;

namespace ElementsAwoken.Items.Tech.Tools
{
    public class AutoMinerMKII : ModItem
    {
        public bool enabled = false;
        public int digCooldown = 0;

        public Vector2 justDugOre = new Vector2();

        public override bool CloneNewInstances
        {
            get { return true; }
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 44;

            item.rare = 4;

            item.maxStack = 1;

            item.pick = 165;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Auto Miner MK II");
            Tooltip.SetDefault("Automatically mines nearby ores\nConsumes energy to mine\nRight click to turn on");
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
            int energyConsumed = 4;
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            if (enabled && modPlayer.energy > energyConsumed)
            {
                digCooldown--;
                if (digCooldown >= 55)
                {
                    for (int k = 0; k < Main.item.Length; k++)
                    {
                        Item other = Main.item[k];
                        if (other.getRect().Intersects(new Rectangle((int)justDugOre.X, (int)justDugOre.Y, 16, 16)))
                        {
                            other.Center = player.Center;
                        }

                    }
                }
                if (digCooldown <= 0)
                {
                    int distance = 10 * 16;
                    Point topLeft = ((player.position - new Vector2(distance, distance)) / 16).ToPoint();
                    Point bottomRight = ((player.BottomRight + new Vector2(distance, distance)) / 16).ToPoint();
                    
                    for (int i = topLeft.X; i <= bottomRight.X; i++)
                    {
                        for (int j = topLeft.Y; j <= bottomRight.Y; j++)
                        {
                            Point tilePos = new Point(i, j);
                            Tile t = Framing.GetTileSafely(i, j);
                            if (TileID.Sets.Ore[t.type] == true && GlobalTiles.GetTileMinPick(t.type) <= item.pick)
                            {
                                WorldGen.KillTile(tilePos.X, tilePos.Y);
                                justDugOre = new Vector2(i * 16, j * 16);

                                Vector2 difference = player.Center - new Vector2(i * 16, j * 16);
                                for (int k = 0; k < 40; k++)
                                {
                                    Dust dust = Main.dust[Dust.NewDust(new Vector2(i * 16, j * 16) + difference * Main.rand.NextFloat(), 0, 0, 61)];
                                    dust.velocity = Vector2.Zero;
                                    dust.noGravity = true;
                                    dust.color = Color.Turquoise;
                                }

                                digCooldown = 60;
                                modPlayer.energy -= energyConsumed;
                                return;
                            }
                        }
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
            recipe.AddIngredient(null, "AutoMiner", 1);
            recipe.AddIngredient(ItemID.MythrilPickaxe, 1);
            recipe.AddRecipeGroup("AdamantiteBar", 8);
            recipe.AddIngredient(null, "GoldWire", 6);
            recipe.AddIngredient(null, "SiliconBoard", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AutoMiner", 1);
            recipe.AddIngredient(ItemID.OrichalcumPickaxe, 1);
            recipe.AddRecipeGroup("AdamantiteBar", 8);
            recipe.AddIngredient(null, "GoldWire", 6);
            recipe.AddIngredient(null, "SiliconBoard", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
