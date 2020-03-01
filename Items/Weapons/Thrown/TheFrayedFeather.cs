using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Thrown
{
    public class TheFrayedFeather : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;

            item.damage = 18;
            item.knockBack = 3f;

            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = false;

            item.useTime = 5;
            item.useAnimation = 15;
            item.reuseDelay = 15;
            item.useStyle = 1;
            item.UseSound = SoundID.Item39;

            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 2;

            item.shoot = mod.ProjectileType("FrayedFeather");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Frayed Feather");
            Tooltip.SetDefault("Throws a burst of three feathers");
        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float rotation = MathHelper.ToRadians(1f);
            float amount = player.direction == -1 ? player.itemAnimation - 15 / 2: -player.itemAnimation + 15 / 2; // change 15 to use animation time
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, amount));
            Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("FrayedFeather"), damage, knockBack, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ThrowingKnife, 150);
            recipe.AddIngredient(ItemID.Feather, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
