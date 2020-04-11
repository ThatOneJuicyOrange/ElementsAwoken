using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Weapons.Ranged
{
    public class Shotstorm : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 70;
            item.height = 26;

            item.damage = 190;
            item.knockBack = 6f;

            item.noMelee = true;
            item.autoReuse = true;
            item.ranged = true;

            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = 5;

            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 10;

            item.UseSound = SoundID.Item17;
            item.shootSpeed = 24f;
            item.shoot = ProjectileType<ShotstormDart>();
            item.useAmmo = AmmoID.Dart;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shotstorm");
            Tooltip.SetDefault("Fires three supersonic darts that inflict a rapid acting poison\nUses darts as ammo");
        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(1.5f);
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 60f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<ShotstormDart>(), damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DartPistol, 1);
            recipe.AddIngredient(ItemID.FragmentVortex, 6);
            recipe.AddIngredient(ItemID.Ruby, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DartRifle, 1);
            recipe.AddIngredient(ItemID.FragmentVortex, 6);
            recipe.AddIngredient(ItemID.Ruby, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
