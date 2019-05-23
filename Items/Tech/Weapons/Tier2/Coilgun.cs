using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Weapons.Tier2
{
    public class Coilgun : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 65;
            item.knockBack = 3.5f;
            item.GetGlobalItem<ItemEnergy>().energy = 8;

            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 5;
            item.UseSound = SoundID.Item40;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;

            item.value = Item.buyPrice(0, 8, 0, 0);
            item.rare = 3;

            item.shootSpeed = 20f;
            item.shoot = mod.ProjectileType("CoilRound");
            item.useAmmo = 97;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Coilgun");
            Tooltip.SetDefault("Fires a fast high damaging bullet");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("CoilRound"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("EvilBar", 8);
            recipe.AddIngredient(null, "GoldWire", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
