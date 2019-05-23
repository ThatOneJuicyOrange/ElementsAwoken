using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossSummons
{
    public class ObsidiousSummon : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;
            item.maxStack = 20;
            item.rare = 5;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;
            item.consumable = true;
            item.shoot = mod.ProjectileType("ObsidiousSpawn");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ulticore");
            Tooltip.SetDefault("The power [c/bc3a31:He] seeks\nUse at night\nSummons Obsidious");
        }


        public override bool CanUseItem(Player player)
        {
            return !Main.dayTime &&
            !NPC.AnyNPCs(mod.NPCType("Obsidious")) &&
            !NPC.AnyNPCs(mod.NPCType("ObsidiousHuman"));
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddIngredient(ItemID.SoulofMight, 3);
            recipe.AddIngredient(ItemID.SoulofNight, 3);
            recipe.AddIngredient(ItemID.SoulofLight, 3);
            recipe.AddIngredient(null, "MysticLeaf", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
