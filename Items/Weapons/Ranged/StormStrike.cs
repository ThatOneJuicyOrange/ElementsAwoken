using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Ranged
{
    public class StormStrike : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 250;
            item.knockBack = 3.5f;

            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 5;
            item.UseSound = SoundID.Item61;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;

            item.rare = 10;
            item.value = Item.buyPrice(1, 0, 0, 0);

            item.shootSpeed = 10f;
            item.shoot = 10;
            item.useAmmo = AmmoID.Rocket;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storm Strike");
            Tooltip.SetDefault("Fires a storm of homing rockets\nHas a chance to shoot a Mega Rocket\n70% chance to not consume ammo");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 5;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(8)); 
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("HomingRocket"), damage, knockBack, player.whoAmI);
            }
            if (Main.rand.Next(2) == 0)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(0));
                int num1 = damage * 2;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("MegaRocket"), num1, knockBack, player.whoAmI);
            }
            return false;
        }

        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.Next(0, 100) <= 70)
                return false;
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MegaRocket", 1);
            recipe.AddIngredient(null, "CInfinityCrys", 1);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(null, "Pyroplasm", 50);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
