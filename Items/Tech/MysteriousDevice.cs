using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using ElementsAwoken.Structures;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ElementsAwoken.Items.Tech
{
    public class MysteriousDevice : ModItem
    {
        public static int sizeMult = (int)(Math.Floor(Main.maxTilesX / 4200f)); //Small = 2; Medium = ~3; Large = 4;

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.useStyle = 4;
            item.UseSound = SoundID.Item60;
            item.consumable = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mysterious Device");
            Tooltip.SetDefault("Filled with all sorts of different data\nGenerates the Labs in the world\nWont work if they have already generated\nWARNING: MAY CRASH THE GAME");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("ElementsAwoken:EvilBar", 8);
            recipe.AddIngredient(null, "Capacitor", 1);
            recipe.AddIngredient(null, "GoldWire", 10);
            recipe.AddIngredient(null, "SiliconBoard", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool CanUseItem(Player player)
        {
            if (MyWorld.generatedLabs)
            {
                Main.NewText("Labs have already been generated", Color.White.R, Color.White.G, Color.White.B);
                return false;
            }
            if (ModContent.GetInstance<Config>().labsDisabled)
            {
                Main.NewText("Labs arent enabled in the EA Config!", Color.White.R, Color.White.G, Color.White.B);
                return false;
            }
            return true;
        }
        public override bool UseItem(Player player)
        {
            int xO = Main.maxTilesX / 2;
            int yO = (int)(Main.maxTilesY * .7f);
            if (!ModContent.GetInstance<Config>().labsDisabled && !MyWorld.generatedLabs)
            {
                LabStructures(xO, yO);
                Main.NewText("Generating...", Color.White.R, Color.White.G, Color.White.B);
            }
            return true;
        }
        private void LabStructures(int xO, int yO)
        {
            int s = 0;
            int structX = xO - 225 * sizeMult + Main.rand.Next(225 * sizeMult * 2);
            int structY = yO - 275 * sizeMult + Main.rand.Next(275 * sizeMult / 2);
            MyWorld.generatedLabs = true;
            Main.NewText("Generated!", Color.White.R, Color.White.G, Color.White.B);
            for (int q = 0; q < 12; q++)
            {
                int border = 300 * sizeMult;
                int xMin = border;
                int xMax = Main.maxTilesX - border;
                int labPosX = WorldGen.genRand.Next(xMin, xMax);

                int yMin = (int)(WorldGen.worldSurfaceHigh + 200.0);
                int yMax = Main.maxTilesY - 230;
                int labPosY = WorldGen.genRand.Next(yMin, yMax);

                s = GenerateLab(s, labPosX, labPosY);
            }
        }

        private int GenerateLab(int s, int structX, int structY)
        {
            if (MyWorld.TileCheckSafe(structX, structY))
            {
                if (!Chest.NearOtherChests(structX, structY))
                {
                    if (structY < Main.maxTilesY - 220)
                    {
                        bool mirrored = false;
                        if (Main.rand.Next(2) == 0)
                            mirrored = true;

                        PickLab(s, structX, structY, mirrored);
                        s++;
                        if (s > 11)
                        {
                            s = 0; // this should never happen but just to be safe
                        }
                    }
                }
            }
            return s;
        }
        private void PickLab(int s, int structX, int structY, bool mirrored)
        {
            if (s == 0)
            {
                WastelandLab.StructureGen(structX, structY, mirrored); // create the structure first
                WastelandLabPlatforms.StructureGen(structX, structY, mirrored); // then create the platforms (on top of the walls)
                WastelandLabFurniture.StructureGen(structX, structY, mirrored); // then the furniture
            }
            else if (s >= 1 && s < 11)
            {
                Lab.StructureGen(structX, structY, mirrored); // create the structure first
                LabPlatforms.StructureGen(structX, structY, mirrored); // create the structure first
                LabFurniture.StructureGen(structX, structY, mirrored, s - 1); // then the furniture. s - 1 so the drives start at 0 again
            }

        }
    }
}
