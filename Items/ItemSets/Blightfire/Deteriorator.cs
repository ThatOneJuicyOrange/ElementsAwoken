using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Blightfire
{
    public class Deteriorator : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 20;

            item.damage = 60;
            item.knockBack = 3f;

            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;

            item.useTime = 4;
            item.useAnimation = 12;
            item.reuseDelay = 16;
            item.useStyle = 1;

            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 11;

            item.shoot = mod.ProjectileType("DeterioratorKnife");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deteriorator");
            Tooltip.SetDefault("Rapidly throw corrosive daggers");
        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(39, (int)player.position.X, (int)player.position.Y, 0);

            float numberProjectiles = 6;
            float rotation = MathHelper.ToRadians(7);
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 5f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Blightfire", 10);
            recipe.AddIngredient(ItemID.LunarBar, 2);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
