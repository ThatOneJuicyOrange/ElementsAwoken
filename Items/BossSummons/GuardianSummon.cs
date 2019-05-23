﻿using System;
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
    public class GuardianSummon : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;
            item.rare = 8;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;
            item.consumable = false;
            item.shoot = mod.ProjectileType("TheGuardianSpawn");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Rune");
            Tooltip.SetDefault("It is covered in dust\nNot consumable\nUse at night\nSummons The Guardian on use");
        }


        public override bool CanUseItem(Player player)
        {
            return
            !Main.dayTime &&
            !NPC.AnyNPCs(mod.NPCType("TheGuardian")) &&
            !NPC.AnyNPCs(mod.NPCType("TheGuardianFly"));
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TempleFragment", 1);
            recipe.AddIngredient(null, "WyrmHeart", 1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
