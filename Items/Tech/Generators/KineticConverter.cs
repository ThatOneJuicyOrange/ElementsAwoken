using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ElementsAwoken.Items.Tech.Generators
{
    public class KineticConverter : ModItem
    {
        public int fuel = 0;
        public int producePowerCooldown = 0;

        public override bool CloneNewInstances
        {
            get { return true; }
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 44;

            item.rare = 4;

            item.maxStack = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kinetic Energy Converter Generator");
            Tooltip.SetDefault("Projectiles that hit the player are converted into energy\nThe more damage and speed the projectile has, the more energy is transformed");
        }

        public override void UpdateInventory(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            modPlayer.kineticConverter = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofFlight, 8);
            recipe.AddIngredient(ItemID.SoulofLight, 12);
            recipe.AddRecipeGroup("ElementsAwoken:GoldBar", 12);
            recipe.AddIngredient(null, "SiliconBoard", 4);
            recipe.AddIngredient(null, "GoldWire", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
