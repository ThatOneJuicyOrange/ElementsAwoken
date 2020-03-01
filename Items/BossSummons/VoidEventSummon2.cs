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
    public class VoidEventSummon2 : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;

            item.maxStack = 20;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;

            item.useAnimation = 45;
            item.useTime = 45;

            item.useStyle = 4;
            item.UseSound = SoundID.Item44;
            item.consumable = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Insignia of Emptyness");
            Tooltip.SetDefault("Summons the Dawn of the Void II on use\nUse after midnight");
        }
        public override bool CanUseItem(Player player)
        {
            if (MyWorld.voidInvasionUp)
            {
                Main.NewText("You cannot intensify the void...", 182, 15, 15, false);
                return false;
            }
            else if (Main.time < 16220 || Main.dayTime)
            {
                Main.NewText("The void retreats from the light", 182, 15, 15, false);
                return false;
            }
            return true;
        }
        public override bool UseItem(Player player)
        {
            if (!Main.dayTime && !MyWorld.voidInvasionUp && Main.time > 16220)
            {
                Main.NewText("The shadows grow darker...", 182, 15, 15, false);
                VoidEvent.StartInvasion();
                return true;
            }
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoidAshes", 2);
            recipe.AddIngredient(null, "VoidEssence", 10);
            recipe.AddIngredient(null, "Darkstone", 16);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
