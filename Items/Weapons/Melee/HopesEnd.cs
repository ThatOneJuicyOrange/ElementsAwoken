using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class HopesEnd : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 270;
            item.melee = true;
            item.width = 70;
            item.height = 70;
            item.useTime = 14;
            item.useTurn = true;
            item.useAnimation = 14;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.buyPrice(1, 20, 0, 0);
            item.rare = 10;
            item.axe = 30;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hope's End");
            Tooltip.SetDefault("Heals life on enemy hit");
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            player.statLife += 4;
            player.HealEffect(4);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(ItemID.LunarHamaxeSolar, 1);
            recipe.AddIngredient(null, "Pyroplasm", 25);    
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(ItemID.LunarHamaxeNebula, 1);
            recipe.AddIngredient(null, "Pyroplasm", 25);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(ItemID.LunarHamaxeStardust, 1);
            recipe.AddIngredient(null, "Pyroplasm", 25);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(ItemID.LunarHamaxeVortex, 1);
            recipe.AddIngredient(null, "Pyroplasm", 25);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
