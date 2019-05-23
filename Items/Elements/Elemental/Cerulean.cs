using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.DataStructures;

namespace ElementsAwoken.Items.Elements.Elemental
{
    public class Cerulean : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 20, 0, 0);

            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cerulean");
            Tooltip.SetDefault("Increases critical strike chance by 5%\nThe lower your health the less damage you take\n5 defense\nMana increased by 100\nMagic damage increased by 15%\n16% increased movement speed\nImmunity to most debuffs\nWhile under half health:\nReduces mana cost greatly\nReduces damage taken\nDamage increased by 10%");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicCrit += 5;
            player.meleeCrit += 5;
            player.rangedCrit += 5;
            player.thrownCrit += 5;
            if (player.statLife <= (player.statLifeMax2 * 0.5f))
            {
                player.manaCost *= 0.5f;
                player.endurance += 0.2f;
                player.thrownDamage *= 1.1f;
                player.rangedDamage *= 1.1f;
                player.magicDamage *= 1.1f;
                player.minionDamage *= 1.1f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.25f))
            {
                player.endurance += 0.2f;
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
            recipe.AddIngredient(null, "ElementalEssence", 5);
            recipe.AddIngredient(null, "VoidAshes", 8);
            recipe.AddIngredient(null, "Unity", 1);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
