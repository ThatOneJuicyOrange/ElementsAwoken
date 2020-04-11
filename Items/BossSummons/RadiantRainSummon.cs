using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Items.ItemSets.Radia;
using ElementsAwoken.Tiles.Crafting;
using ElementsAwoken.Items.BossDrops.Azana;

namespace ElementsAwoken.Items.BossSummons
{
    public class RadiantRainSummon : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;

            item.useAnimation = 45;
            item.useTime = 45;

            item.useStyle = 4;
            item.UseSound = SoundID.Item44;

            item.consumable = true;
            item.maxStack = 20;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Starholder");
            Tooltip.SetDefault("Enchants the rain with the Radiance");
        }

        public override bool CanUseItem(Player player)
        {
            return !MyWorld.radiantRain;
        }
        public override bool UseItem(Player player)
        {
            if (Main.raining)
            {
                MyWorld.radiantRain = true;
            }
            else if (!Main.raining)
            {
                int num = 86400;
                int num2 = num / 24;
                Main.rainTime = Main.rand.Next(num2 * 8, num);
                if (Main.rand.Next(3) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2);
                }
                if (Main.rand.Next(4) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 2);
                }
                if (Main.rand.Next(5) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 2);
                }
                if (Main.rand.Next(6) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 3);
                }
                if (Main.rand.Next(7) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 4);
                }
                if (Main.rand.Next(8) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 5);
                }
                float num3 = 1f;
                if (Main.rand.Next(2) == 0)
                {
                    num3 += 0.05f;
                }
                if (Main.rand.Next(3) == 0)
                {
                    num3 += 0.1f;
                }
                if (Main.rand.Next(4) == 0)
                {
                    num3 += 0.15f;
                }
                if (Main.rand.Next(5) == 0)
                {
                    num3 += 0.2f;
                }
                Main.rainTime = (int)((float)Main.rainTime * num3);
                Main.raining = true;
                Main.maxRaining = Main.rand.NextFloat(0.2f, 0.8f);
                MyWorld.radiantRain = true;
            }
            Main.NewText("The rain glistens with unknown magic...", Color.HotPink);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<Radia>(), 6);
            recipe.AddIngredient(ItemType<DiscordantBar>(), 3);
            recipe.AddTile(TileType<ChaoticCrucible>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
