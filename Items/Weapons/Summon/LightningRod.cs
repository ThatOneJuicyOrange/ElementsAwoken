using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Summon
{
    public class LightningRod : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 48;

            item.damage = 30;
            item.mana = 15;
            item.knockBack = 5f;

            item.summon = true;
            item.sentry = true;
            Item.staff[item.type] = true;
            item.autoReuse = true;
            item.noMelee = true;

            item.useTime = 38;
            item.useAnimation = 38;
            item.useStyle = 5;
            item.UseSound = SoundID.Item20;

            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 3;

            item.shoot = mod.ProjectileType("EnergyStorm");
            item.shootSpeed = 9f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Rod");
            Tooltip.SetDefault("Summons an energy storm that fires lightning at enemies");
        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, mod.ProjectileType("EnergyStorm"), damage, knockBack, Main.myPlayer, 0f, 0f);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar, 12);
            recipe.AddIngredient(ItemID.Bone, 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
