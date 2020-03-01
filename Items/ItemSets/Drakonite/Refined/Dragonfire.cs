using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Drakonite.Refined
{
    public class Dragonfire : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 52;
            item.knockBack = 3.5f;
            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useAnimation = 8;
            item.useTime = 5;
            item.useStyle = 5;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;

            item.UseSound = SoundID.Item11;

            item.shootSpeed = 18f;
            item.shoot = 10;
            item.useAmmo = 97;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragonfire");
            Tooltip.SetDefault("Turns musketballs into dragonfire bullets\n20% chance to not consume ammo");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            if (type == ProjectileID.Bullet) // The normal musket ball
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DragonfireBullet"), (int)(item.damage * 1.1f), knockBack, player.whoAmI, 0f, 0f);
                return false;
            }
            else
            {
                return true;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RefinedDrakonite", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.Next(0, 100) <= 20)
                return false;
            return true;
        }
    }
}
