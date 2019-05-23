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
    public class PermafrostSummon : ModItem
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
            item.shoot = mod.ProjectileType("PermafrostSpawn");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Ice Crystal");
            Tooltip.SetDefault("An eternity of time has passed around this crystal\nSummons Permafrost on use");
        }

        public override bool CanUseItem(Player player)
        {
            return 
            player.ZoneSnow && 
            !NPC.AnyNPCs(mod.NPCType("Permafrost"));
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FrostEssence", 10);
            recipe.AddIngredient(ItemID.IceBlock, 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
