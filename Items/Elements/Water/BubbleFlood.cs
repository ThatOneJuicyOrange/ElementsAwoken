using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Water
{
    public class BubbleFlood : ModItem
    {

        public override void SetDefaults()
        {
            item.damage = 49;
            item.ranged = true;
            item.width = 92;
            item.height = 28;
            item.useTime = 9;
            item.useAnimation = 9;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1.75f;
            item.value = Item.buyPrice(0, 75, 0, 0);
            item.rare = 8;
            item.UseSound = SoundID.Item85;
            item.autoReuse = true;
            item.shoot = ProjectileID.Bubble;
            item.shootSpeed = 24f;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Typhos");
    }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, -10);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 2 + Main.rand.Next(3);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterEssence", 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddIngredient(ItemID.BubbleGun, 1);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
