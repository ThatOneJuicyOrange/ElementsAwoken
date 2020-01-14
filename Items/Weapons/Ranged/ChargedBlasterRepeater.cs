using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Ranged
{
    public class ChargedBlasterRepeater : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 44;

            item.damage = 28;
            item.knockBack = 2f;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.UseSound = SoundID.Item61;

            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 3;

            item.shoot = 10;
            item.shootSpeed = 14f;
            item.useAmmo = 40;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Charged Blaster Repeater");
            Tooltip.SetDefault("Turns wooden arrows into an explosive bolt");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly) type = mod.ProjectileType("ExplosiveBolt");
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(3));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);

            /*float numberProjectiles = 2;
            float rotation = MathHelper.ToRadians(2);
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 15f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("ExplosiveBolt"), damage, knockBack, player.whoAmI);
            }*/
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 14);
            recipe.AddIngredient(ItemID.Obsidian, 4);
            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
