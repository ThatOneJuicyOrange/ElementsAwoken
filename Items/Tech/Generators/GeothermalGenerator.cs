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
    public class GeothermalGenerator : ModItem
    {
        public int producePowerCooldown = 0;
        public int producePowerCooldownMax = 60;
        public int productionAmount = 1;

        public Tile closest = null;
        public Vector2 closestPos = new Vector2();
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
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.LocalPlayer;
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();

            float powerPerSec = (float)producePowerCooldownMax / 60f;
            string ppsString = powerPerSec.ToString("n1");
            TooltipLine powerOutput = new TooltipLine(mod, "Elements Awoken:Tooltip", "Power Output: " + productionAmount + " energy every " + ppsString + " seconds");
            if (Vector2.Distance(closestPos, player.Center) < 160 && closest.lava())
            {
                powerOutput = new TooltipLine(mod, "Elements Awoken:Tooltip", "Power Output: " + productionAmount + " energy every " + ppsString + " seconds");

            }
            else
            {
                powerOutput = new TooltipLine(mod, "Elements Awoken:Tooltip", "Inactive");
            }
            tooltips.Insert(1, powerOutput);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Geothermal Generator");
            Tooltip.SetDefault("Turns heat into energy\nThe closer the player is to a lava source the more energy is generated");
        }

        public override void UpdateInventory(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();

            producePowerCooldown--;

            int distance = 10 * 16;
            Point topLeft = ((player.position - new Vector2(distance, distance)) / 16).ToPoint();
            Point bottomRight = ((player.BottomRight + new Vector2(distance, distance)) / 16).ToPoint();

            // draws dust where the hitbox is 
            /*for (int i = 0; i < 20; i++)
            {
                int dust = Dust.NewDust(new Vector2(topLeft.X * 16, topLeft.Y * 16), (bottomRight.X - topLeft.X) * 16, (bottomRight.Y - topLeft.Y) * 16, 57, 0f, 0f, 100);
                Main.dust[dust].velocity *= 0.01f;
            }*/
            // needs to check all of the tiles 

            for (int i = topLeft.X; i <= bottomRight.X; i++)
            {
                for (int j = topLeft.Y; j <= bottomRight.Y; j++)
                {
                    Tile t = Framing.GetTileSafely(i, j);
                    if (t.lava())
                    {
                        Vector2 tileCenter = new Vector2(i * 16, j * 16);
                        if (closest != null)
                        {
                            if (Vector2.Distance(tileCenter, player.Center) < Vector2.Distance(closestPos, player.Center))
                            {
                                closest = t;
                                closestPos = new Vector2(i * 16, j * 16);
                            }
                        }
                        else
                        {
                            closest = t;
                            closestPos = new Vector2(i * 16, j * 16);
                        }

                    }
                }
            }
            if (Vector2.Distance(closestPos, player.Center) < 160 && closest.lava())
            {
                producePowerCooldownMax = (int)MathHelper.Lerp(15, 120, Vector2.Distance(closestPos, player.Center) / 160);
                productionAmount = (int)Math.Round(MathHelper.Lerp(2, 1, Vector2.Distance(closestPos, player.Center) / 160));

                if (producePowerCooldown <= 0)
                {
                    modPlayer.energy += productionAmount;
                    producePowerCooldown = producePowerCooldownMax;
                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddIngredient(null, "MagmaCrystal", 4);
            recipe.AddIngredient(null, "SiliconBoard", 4);
            recipe.AddIngredient(null, "GoldWire", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
