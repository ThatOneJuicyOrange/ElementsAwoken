using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Fire
{
    public class BlazeFury : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 44; 
            
            item.damage = 42;
            item.knockBack = 2f;

            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 5;
            item.UseSound = SoundID.Item5;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = false;

            item.value = Item.buyPrice(0, 7, 0, 0);
            item.rare = 4;

            item.shoot = 10;
            item.shootSpeed = 8f;
            item.useAmmo = 40;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blaze Fury");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly) type = ProjectileID.FireArrow;
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f); 
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
