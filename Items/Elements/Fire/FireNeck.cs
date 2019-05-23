using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ElementsAwoken.Items.Elements.Fire
{
    public class FireNeck : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 4;
            item.value = Item.buyPrice(0, 7, 0, 0);
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Band of Inferno");
            Tooltip.SetDefault("Unleash the power of flames upon your enemies\nMelee attacks have flames and increased knockback\n10% increased damage");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magmaStone = true;
            player.kbGlove = true;
            player.meleeDamage += 0.1f;
            player.thrownDamage += 0.1f;
            player.rangedDamage += 0.1f;
            player.magicDamage += 0.1f;
            player.minionDamage += 0.1f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
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
