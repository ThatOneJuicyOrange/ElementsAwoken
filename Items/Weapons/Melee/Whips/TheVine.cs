using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ElementsAwoken.Items.Weapons.Melee.Whips
{
    public class TheVine : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 11;

            item.damage = 20;
            item.knockBack = 4f;

            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 3;

            item.useStyle = 5;
            item.useAnimation = 19;
            item.useTime = 19;
            item.UseSound = SoundID.Item1;

            item.noMelee = true;
            item.noUseGraphic = true;
            item.melee = true;
            item.autoReuse = false;
            item.noMelee = true;

            item.shoot = mod.ProjectileType("TheVineP");
            item.shootSpeed = 15f;

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Vine");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Vine, 3);
            recipe.AddIngredient(ItemID.JungleSpores, 12);
            recipe.AddIngredient(ItemID.Stinger, 6);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float ai3 = (Main.rand.NextFloat() - 0.75f) * 0.7853982f; //0.5
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, ai3);
            return false;
        }
    }
}
