using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ElementsAwoken.Items.Elements.Water
{
    public class WaterNeck : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.accessory = true;
            item.value = Item.buyPrice(0, 75, 0, 0);
            item.rare = 8;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Emblem of the Ocean");
            Tooltip.SetDefault("Become one with the ocean\nUpon entering water:\n20% increased damage\n20% increased melee speed and throwing velocity\nGrants effects of the Magic Cuffs\nIncreased movement speed\n10 increased defense");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeSpeed += 0.2f;
            player.accMerman = true;
            if (hideVisual)
            {
                player.hideMerman = true;
            }
            bool wet = player.wet;
            if (wet)
            {
                player.meleeDamage += 0.2f;
                player.thrownDamage += 0.2f;
                player.rangedDamage += 0.2f;
                player.magicDamage += 0.2f;
                player.minionDamage += 0.2f;
                player.meleeSpeed += 0.2f;
                player.thrownVelocity += 0.2f;
                player.magicCuffs = true;
                player.statDefense += 10;
                player.moveSpeed += 1f;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterEssence", 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
