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
    public class InfernaceSummon : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;
            item.maxStack = 20;
            item.rare = 6;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;
            item.consumable = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Core");
            Tooltip.SetDefault("It radiates a burning heat\nSummons Infernace on use");
        }


        public override bool CanUseItem(Player player)
        {
            return 
            player.ZoneUnderworldHeight && 
            !NPC.AnyNPCs(mod.NPCType("Infernace"));
        }
        public override bool UseItem(Player player)
        {
            Main.NewText("You dare challenge me?!", Color.Orange.R, Color.Orange.G, Color.Orange.B);
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Infernace"));
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 10);
            recipe.AddIngredient(ItemID.HellstoneBar, 5);
            recipe.AddIngredient(ItemID.Obsidian, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
