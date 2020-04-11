using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles.Arrows;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.Putrid
{
    public class RottedBow : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 64;

            item.damage = 76;
            item.knockBack = 5;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useTime = 22;
            item.useAnimation = 22;
            item.UseSound = SoundID.Item5;
            item.useStyle = 5;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;

            item.shoot = 10;
            item.shootSpeed = 8f;
            item.useAmmo = 40;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rotted Bow");
            Tooltip.SetDefault("Turns normal arrows into putrid arrows\nPutrid arrows have a chance to splatter into poisonous goop");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly) type = ProjectileType<PutridArrow>();
            float numberProjectiles = 5;
            float rotation = MathHelper.ToRadians(15);
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
            recipe.AddIngredient(ItemType<PutridBar>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
