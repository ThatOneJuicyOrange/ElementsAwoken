using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using ElementsAwoken.Events.VoidEvent;

namespace ElementsAwoken.Items.BossSummons
{
    public class VoidEventSummon : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;
            item.maxStack = 20;
            item.rare = 10;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;
            item.consumable = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Pearl");
            Tooltip.SetDefault("Summons the Dawn of the Void on use");
        }
        public override bool CanUseItem(Player player)
        {
            if (!Main.dayTime && Main.time > 9000)
            {
                Main.NewText("It is too late to call upon the void...", 182, 15, 15, false);
                return false;
            }
            else if (MyWorld.voidInvasionUp)
            {
                Main.NewText("You cannot intensify the void...", 182, 15, 15, false);
                return false;
            }
            else if (MyWorld.voidInvasionWillStart)
            {
                Main.NewText("Your head is already filled with screams...", 182, 15, 15, false);
                return false;
            }
            return true;
        }
        public override bool UseItem(Player player)
        {
            if (!Main.dayTime && !MyWorld.voidInvasionUp)
            {
                Main.NewText("You feel a wave of cold rush over you...", 182, 15, 15, false);
                VoidEvent.StartInvasion();
                return true;
            }
            else if (!MyWorld.voidInvasionWillStart)
            {
                Main.NewText("Your head throbs", 182, 15, 15, false);
                MyWorld.voidInvasionWillStart = true;
                return true;
            }
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Pyroplasm", 15);
            recipe.AddIngredient(ItemID.LunarBar, 4);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
