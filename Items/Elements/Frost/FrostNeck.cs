using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ElementsAwoken.Items.Elements.Frost
{
    [AutoloadEquip(EquipType.Neck)]
    public class FrostNeck : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 7;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pendant of Frost");
            Tooltip.SetDefault("Harness the power of frost\nImmunity to chilled and frozen\nUpon entering snow:\n5% increased damage\n5% ncreased melee speed and throwing velocity");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[47] = true;
            player.buffImmune[46] = true;
            if (Main.player[Main.myPlayer].ZoneSnow)
            {
                player.meleeDamage += 0.05f;
                player.thrownDamage += 0.05f;
                player.rangedDamage += 0.05f;
                player.magicDamage += 0.05f;
                player.minionDamage += 0.05f;
                player.meleeSpeed += 0.05f;
                player.thrownVelocity += 0.1f;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FrostEssence", 7);
            recipe.AddRecipeGroup("ElementsAwoken:IceGroup", 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool DrawBody()
        {
            return false;
        }
    }
}
