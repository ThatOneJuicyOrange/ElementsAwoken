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
    public class CosmicObserverSummon : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;
            item.maxStack = 20;
            item.rare = 1;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;
            item.consumable = true;
            //item.shoot = mod.ProjectileType("ToySlimeSpawn");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Otherworldly Crystal");
            Tooltip.SetDefault("Increases the spawn chance of the Cosmic Observer for a minute");
        }

        public override bool CanUseItem(Player player)
        {
            return 
            !NPC.AnyNPCs(mod.NPCType("CosmicObserver"))
            && player.ZoneSkyHeight;
        }
        public override bool UseItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.increasedObserverChance = true;
            modPlayer.observerChanceTimer = 3600;
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofFlight, 5);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddRecipeGroup("IronBar", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
