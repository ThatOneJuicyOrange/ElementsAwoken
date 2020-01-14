using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Fire
{
    public class Incinerator : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 58;
            item.height = 22;
            
            item.damage = 25;
            item.knockBack = 2.25f;

            item.useTime = 13;
            item.useAnimation = 13;
            item.useStyle = 5;
            item.UseSound = SoundID.Item5;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;

            item.value = Item.buyPrice(0, 7, 0, 0);
            item.rare = 4;

            item.shoot = 10;
            item.shootSpeed = 12f;
            item.useAmmo = 40;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Incinerator");
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
