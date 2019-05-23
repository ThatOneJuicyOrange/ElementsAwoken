using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class KindleCrusher : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 130;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.width = 36;
            item.height = 28;
            item.useTime = 9;
            item.useAnimation = 9;
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 10;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("KindleCrusherP");
            item.shootSpeed = 18f;

        }
        public override bool CanUseItem(Player player)
        {
            int maxThrown = 3;
            if (player.ownedProjectileCounts[mod.ProjectileType("KindleCrusherP")] >= maxThrown)
            {
                return false;
            }
            else return true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kindle Crusher");
            Tooltip.SetDefault("");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Flairon, 1);
            recipe.AddIngredient(ItemID.FragmentSolar, 18);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

