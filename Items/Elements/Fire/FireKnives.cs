using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Fire
{
    public class FireKnives : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.damage = 18;
            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useAnimation = 35;
            item.useStyle = 1;
            item.useTime = 35;
            item.knockBack = 3f;
            item.UseSound = SoundID.Item39;
            item.autoReuse = true;
            item.height = 20;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 7, 0, 0);
            item.rare = 4;
            item.shoot = mod.ProjectileType("FireKnife");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Knives");
            Tooltip.SetDefault("Rapidly throw exploding fire daggers");
        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(5);
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
            recipe.AddIngredient(null, "FireEssence", 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
