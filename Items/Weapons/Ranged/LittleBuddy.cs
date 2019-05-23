using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Ranged
{
    public class LittleBuddy : ModItem
    {

        public override void SetDefaults()
        {
            item.damage = 18;
            item.ranged = true;
            item.width = 70;
            item.height = 26;
            item.useTime = 36;
            item.useAnimation = 36;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 3.5f;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 2;
            item.UseSound = SoundID.Item92;
            item.autoReuse = true;
            item.shootSpeed = 12f;
            item.shoot = mod.ProjectileType("ElectricBolt");
            item.useAmmo = 97;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Little Buddy");
            Tooltip.SetDefault("Fires a bouncing blue bolt");
        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("ElectricBolt"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FlintlockPistol, 1);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
