using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Sky
{
    public class TimeToken : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 25, 0, 0);
            item.rare = 6;
            item.useAnimation = 20;
            item.useTime = 20;
            item.useStyle = 4;
            item.UseSound = SoundID.Item60;
            item.consumable = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hourglass of Eternity");
            Tooltip.SetDefault("Switches the time between day and night");
        }

        public override bool CanUseItem(Player player)
        {
            if (MyWorld.voidInvasionUp)
            {
                Main.NewText("You cant escape from the void", 182, 15, 15, false);
                return false;
            }
            return true;
        }
        public override bool UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Main.dayTime = !Main.dayTime;
                Main.time = 0; //16220 for midnight 9000 for 10pm
                if (Main.netMode == 2)
                {
                    NetMessage.SendData(MessageID.WorldData);
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SkyEssence", 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
