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
    public class AutoMiner : ModItem
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

            item.rare = 3;

            item.maxStack = 1;

            item.pick = 100;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Auto Miner");
            Tooltip.SetDefault("Automatically mines nearby ores\nConsumes energy to mine\nRight Click to turn on");
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
                    for (int k = 0; k < Main.maxItems; k++)
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
                            bool canMineModOre = false;
                            ModTile modTile = TileLoader.GetTile(t.type);
                            if (modTile != null)
                            {
                                if (modTile.GetType().Name.Contains("Ore"))
                                {
                                    canMineModOre = true;
                                }
                            }
                            if ((TileID.Sets.Ore[t.type] == true || canMineModOre) && GlobalTiles.GetTileMinPick(t.type) <= item.pick)
                            {
                                WorldGen.KillTile(tilePos.X, tilePos.Y);
                                NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, (float)tilePos.X, (float)tilePos.Y, 0f, 0, 0, 0);
                                //player.PickTile(i, j, item.pick);
                                justDugOre = new Vector2(i * 16, j * 16);

                                Vector2 difference = player.Center - new Vector2(i * 16, j * 16);
                                for (int k = 0; k < 40; k++)
                                {
                                    Dust dust = Main.dust[Dust.NewDust(new Vector2(i * 16, j * 16) + difference * Main.rand.NextFloat(), 0, 0, 64)];
                                    dust.velocity = Vector2.Zero;
                                    dust.noGravity = true;
                                    dust.scale *= 0.4f;
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
            recipe.AddIngredient(ItemID.MoltenPickaxe, 1);
            recipe.AddIngredient(ItemID.HellstoneBar, 6);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddIngredient(null, "CopperWire", 2);
            recipe.AddIngredient(null, "GoldWire", 6);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
