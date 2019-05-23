using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Summon
{
    public class SupremeAwakener : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 28;

            item.damage = 215;
            item.mana = 20;
            item.knockBack = 3;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;

            item.summon = true;
            item.noMelee = true;

            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 10;

            item.UseSound = SoundID.Item44;

            item.shoot = mod.ProjectileType("WokeMinion");
            item.shootSpeed = 7f; 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Supreme Awakener");
            Tooltip.SetDefault("Summons the awakened to serve you");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Awakener", 1);
            recipe.AddIngredient(ItemID.LunarBar, 15);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
