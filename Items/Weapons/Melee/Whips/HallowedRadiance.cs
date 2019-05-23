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
    public class HallowedRadiance : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 11;

            item.damage = 49;
            item.knockBack = 5f;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.useStyle = 5;
            item.useAnimation = 12;
            item.useTime = 12;
            item.UseSound = SoundID.Item1;

            item.noMelee = true;
            item.noUseGraphic = true;
            item.melee = true;
            item.autoReuse = false;
            item.noMelee = true;

            item.shoot = mod.ProjectileType("HallowedRadianceP");
            item.shootSpeed = 15f;

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallowed Radiance");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float ai3 = (Main.rand.NextFloat() - 0.75f) * 0.7853982f; //0.5
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, ai3);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
