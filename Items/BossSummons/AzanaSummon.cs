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
    public class AzanaSummon : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;

            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infected Microchip");
            Tooltip.SetDefault("It gleams with red light\nSummons Azana on use");
        }

        public override bool CanUseItem(Player player)
        {
            return
            !NPC.AnyNPCs(mod.NPCType("AzanaEye")) &&
            !NPC.AnyNPCs(mod.NPCType("Azana"));
        }
        public override bool UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient) NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("AzanaEye"));
            else NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, mod.NPCType("AzanaEye"), 0f, 0f, 0, 0, 0);
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ChaoticFlare", 30);
            recipe.AddIngredient(null, "Pyroplasm", 45);
            recipe.AddIngredient(null, "VoidAshes", 12);
            recipe.AddIngredient(ItemID.LunarBar, 16);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
