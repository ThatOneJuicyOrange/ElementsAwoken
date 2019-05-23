using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Ranged
{
    public class AstralBarrage : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 65;
            item.knockBack = 3.5f;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useAnimation = 18;
            item.useTime = 18;
            item.useStyle = 5;

            item.value = Item.sellPrice(0, 25, 0, 0);
            item.rare = 10;

            item.UseSound = SoundID.Item12;

            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("AstralRound");
            item.useAmmo = ItemID.FallenStar;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Barrage");
            Tooltip.SetDefault("Fires a volley of stars\n75% chance to not consume ammo");
        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = Main.rand.Next(3, 6);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X * 0.8f, perturbedSpeed.Y * 0.8f, mod.ProjectileType("AstralStar"), damage, knockBack, player.whoAmI);
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.FallingStar, damage, knockBack, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NeutronFragment", 8);
            recipe.AddIngredient(null, "CInfinityCrys", 1);
            recipe.AddIngredient(null, "Pyroplasm", 25);
            recipe.AddIngredient(ItemID.StarCannon, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.Next(0, 100) <= 75)
                return false;
            return true;
        }
    }
}
