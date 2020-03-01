using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ElementsAwoken.Items.Elements.Sky
{
    public class SkyNeck : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 6;
            item.value = Item.buyPrice(0, 25, 0, 0);
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Badge of the Sky");
            Tooltip.SetDefault("The wind blesses you\nInceased wingtime\nGreatly increased movement speed\nWhen in space:\n5% increased damage\n8 defense");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax += 50;
            player.moveSpeed += 3f;
            if (Main.player[Main.myPlayer].ZoneSkyHeight)
            {
                player.meleeDamage += 0.05f;
                player.thrownDamage += 0.05f;
                player.rangedDamage += 0.05f;
                player.magicDamage += 0.05f;
                player.minionDamage += 0.05f;
                player.meleeSpeed += 0.2f;
                player.thrownVelocity += 0.2f;
                player.statDefense += 8;
                player.moveSpeed += 0.5f;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SkyEssence", 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
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
