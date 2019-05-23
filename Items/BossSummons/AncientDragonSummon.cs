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
    public class AncientDragonSummon : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;
            item.maxStack = 20;
            item.rare = 2;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;
            item.consumable = true;
            item.shoot = mod.ProjectileType("AncientWyrmSpawn");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Will of Fire");
            Tooltip.SetDefault("Use at night\nSummons the Temple Keepers on use");
        }

        public override bool CanUseItem(Player player)
        {
            return
                !Main.dayTime &&
            !NPC.AnyNPCs(mod.NPCType("TheEye")) &&
            !NPC.AnyNPCs(mod.NPCType("AncientWyrmHead"));
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentNebula, 1);
            recipe.AddIngredient(ItemID.FragmentSolar, 1);
            recipe.AddIngredient(ItemID.FragmentStardust, 1);
            recipe.AddIngredient(ItemID.FragmentVortex, 1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 4);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
