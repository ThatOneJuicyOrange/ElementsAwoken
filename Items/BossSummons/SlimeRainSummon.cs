using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ElementsAwoken.Items.BossSummons
{
    public class SlimeRainSummon : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.maxStack = 20;

            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;

            item.rare = 1;

            item.UseSound = SoundID.Item44;
            item.consumable = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energized Gel");
            Tooltip.SetDefault("Looks strange, I would suggest to not drink it.\nSummons the Slime Rain on use");
        }

        public override bool CanUseItem(Player player)
        {
            return
            !Main.slimeRain;
        }
        public override bool UseItem(Player player)
        {
            Main.NewText("A sticky drop appears on your shoulder.", 175, 75, 255, false);
            Main.slimeRainTime = (double)Main.rand.Next(32400, 54000);
            Main.slimeRain = true;
            Main.slimeRainKillCount = 0;

            Main.StartSlimeRain(true); // doesnt want to work by itsself
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Gel, 30);
            recipe.AddIngredient(ItemID.PinkGel, 5);
            recipe.AddIngredient(ItemID.Bottle, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
