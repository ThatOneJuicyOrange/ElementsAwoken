using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Void
{
    public class HealingIcon : ModItem
    {
        public float healTimer = 7f;

        public override void SetDefaults()
        {
            item.UseSound = SoundID.Item4;
            item.maxStack = 1;
            item.width = 26;
            item.height = 44;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 11;
            return;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Icon of Healing");
            Tooltip.SetDefault("Constantly heals the player when under 250 life");
        }

        public override void UpdateInventory(Player player)
        {
            if (healTimer > 0f)
            {
                healTimer -= 1f;
            }
            if (healTimer == 0f && player.statLife <= 250)
            {
                player.statLife += 2;
                player.HealEffect(2);
                healTimer = 15f;
            }

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoidEssence", 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ItemID.RegenerationPotion, 1);
            recipe.AddIngredient(ItemID.LesserHealingPotion, 1);
            recipe.AddIngredient(ItemID.HealingPotion, 1);
            recipe.AddIngredient(ItemID.GreaterHealingPotion, 1);
            recipe.AddIngredient(ItemID.SuperHealingPotion, 1);
            recipe.AddIngredient(null, "EpicHealingPotion", 1);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
