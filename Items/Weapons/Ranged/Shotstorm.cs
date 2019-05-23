using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Ranged
{
    public class Shotstorm : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 70;
            item.height = 26;

            item.damage = 60;
            item.knockBack = 13f;

            item.noMelee = true;
            item.autoReuse = true;
            item.ranged = true;

            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 5;

            item.value = Item.buyPrice(0, 20, 0, 0);
            item.rare = 2;

            item.UseSound = SoundID.Item99;
            item.shootSpeed = 24f;
            item.shoot = mod.ProjectileType("ShotstormP");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shotstorm");
            Tooltip.SetDefault("Fires 3 supersonic bolts");
        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(3);
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 10f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X *2, perturbedSpeed.Y*2, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteShotbow, 1);
            recipe.AddIngredient(ItemID.FragmentVortex, 6);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
