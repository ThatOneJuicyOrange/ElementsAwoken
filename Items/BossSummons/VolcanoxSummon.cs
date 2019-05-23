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
    public class VolcanoxSummon : ModItem
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
            item.shoot = mod.ProjectileType("VolcanoxSpawn");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Charred Core");
            Tooltip.SetDefault("It's eternal blaze burns your eyes\nSummons Volcanox on use");
        }


        public override bool CanUseItem(Player player)
        {
            return 
            player.ZoneUnderworldHeight && 
            !NPC.AnyNPCs(mod.NPCType("Volcanox"));
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NeutronFragment", 10);
            recipe.AddIngredient(null, "Pyroplasm", 30);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
