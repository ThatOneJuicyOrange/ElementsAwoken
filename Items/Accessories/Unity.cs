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
    public class Unity : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 11;
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unity");
            Tooltip.SetDefault("The lower your health the less damage you take\n5 defense\nMana increased by 100\nMagic damage increased by 15%\n16% increased movement speed\nImmunity to most debuffs");
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

            player.statManaMax2 += 100;
            player.magicDamage *= 1.15f;
            player.moveSpeed *= 1.16f;
            player.noKnockback = true;
            player.fireWalk = true;
            player.buffImmune[46] = true;
            player.buffImmune[44] = true;
            player.buffImmune[33] = true;
            player.buffImmune[36] = true;
            player.buffImmune[30] = true;
            player.buffImmune[20] = true;
            player.buffImmune[32] = true;
            player.buffImmune[31] = true;
            player.buffImmune[35] = true;
            player.buffImmune[23] = true;
            player.buffImmune[22] = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Pyroplasm", 50);
            recipe.AddIngredient(null, "LunarShell", 1);
            recipe.AddIngredient(null, "EnergyGeode", 1);
            recipe.AddIngredient(null, "VoiditeBar", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
