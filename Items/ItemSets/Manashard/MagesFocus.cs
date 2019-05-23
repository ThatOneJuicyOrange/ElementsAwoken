using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ElementsAwoken.Items.ItemSets.Manashard
{
    public class MagesFocus : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 5;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mages Focus");
            Tooltip.SetDefault("10% increased magic damage, critical strike chance\n+50 max mana\n15% reduced mana usage");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicDamage *= 1.1f;
            player.magicCrit += 10;
            player.statManaMax2 += 50;
            player.manaCost -= 0.15f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Manashard", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
