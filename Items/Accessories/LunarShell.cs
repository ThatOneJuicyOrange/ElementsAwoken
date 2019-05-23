using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ElementsAwoken.Items.Accessories
{
    public class LunarShell : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 10;
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Shell");
            Tooltip.SetDefault("The lower your health the less damage you take\n5 defense");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 5;
            if (player.statLife <= (player.statLifeMax2 * 0.9f) && player.statLife >= (player.statLifeMax2 * 0.9f))
            {
                player.endurance += 0.04f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife >= (player.statLifeMax2 * 0.7f))
            {
                player.endurance += 0.08f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.7f) && player.statLife >= (player.statLifeMax2 * 0.6f))
            {
                player.endurance += 0.16f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife >= (player.statLifeMax2 * 0.5f))
            {
                player.endurance += 0.20f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.5f) && player.statLife >= (player.statLifeMax2 * 0.4f))
            {
                player.endurance += 0.24f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife >= (player.statLifeMax2 * 0.3f))
            {
                player.endurance += 0.28f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.3f) && player.statLife >= (player.statLifeMax2 * 0.2f))
            {
                player.endurance += 0.32f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.2f) && player.statLife >= (player.statLifeMax2 * 0.1f))
            {
                player.endurance += 0.36f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.1f))
            {
                player.endurance += 0.40f;
            }

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FrozenTurtleShell, 1);
            recipe.AddIngredient(ItemID.FragmentNebula, 8);
            recipe.AddIngredient(ItemID.FragmentSolar, 8);
            recipe.AddIngredient(ItemID.FragmentStardust, 8);
            recipe.AddIngredient(ItemID.FragmentVortex, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
