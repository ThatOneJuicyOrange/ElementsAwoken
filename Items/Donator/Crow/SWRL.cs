using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Crow
{
    public class SWRL : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 16;

            item.damage = 140;
            item.knockBack = 3.25f;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useTime = 4;
            item.useAnimation = 12;
            item.useStyle = 5;
            item.UseSound = SoundID.Item61;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 25, 0, 0);

            item.shootSpeed = 19f;
            item.shoot = ProjectileID.RocketI;
            item.useAmmo = AmmoID.Rocket;

            item.GetGlobalItem<EATooltip>().donator = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("S.W.R.L");
            Tooltip.SetDefault("Turns normal projectiles into S.W.Rs\n50% chance not to consume ammo\nCrow's donator item");
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, -4);
        }
        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() > .5f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.RocketI)
            {
                type = mod.ProjectileType("SWRLRocket");
            }
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(8));
            Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ChaoticFlare", 10);
            recipe.AddIngredient(null, "VoiditeBar", 4);
            recipe.AddIngredient(ItemID.SDMG, 1);
            recipe.AddIngredient(ItemID.RocketLauncher, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
