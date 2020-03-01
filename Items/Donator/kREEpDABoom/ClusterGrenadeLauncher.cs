using System;
using System.Collections.Generic;
using ElementsAwoken.Items.Materials;
using ElementsAwoken.Items.Weapons.Ranged;
using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Donator.kREEpDABoom
{
    public class ClusterGrenadeLauncher : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 140;
            item.knockBack = 3.5f;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useAnimation = 34;
            item.useTime = 34;
            item.useStyle = 5;
            item.UseSound = SoundID.Item61;

            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 11;

            item.shootSpeed = 12f;
            item.shoot = 10;
            item.useAmmo = AmmoID.Rocket;
            item.GetGlobalItem<EATooltip>().donator = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Recursive Cluster Launcher");
            Tooltip.SetDefault("Fires a cluster grenade\n'For all of your exploding needs'\nkREEpDABoom's donator item");
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type = ProjectileType<ClusterGrenade>(), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GrenadeLauncher, 1);
            recipe.AddIngredient(ItemType<Pyroplasm>(), 20);
            recipe.AddIngredient(ItemID.FragmentVortex, 4);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
