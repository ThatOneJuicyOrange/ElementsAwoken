using System;
using System.Collections.Generic;
using ElementsAwoken.Items.Materials;
using ElementsAwoken.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Donator.Aegida
{
    public class AegidaTempestas : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 55;
            item.knockBack = 3.5f;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useAnimation = 6;
            item.useTime = 6;
            item.useStyle = 5;
            item.UseSound = SoundID.Item41;

            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 11;

            item.shootSpeed = 12f;
            item.shoot = 10;
            item.useAmmo = 97;
            item.GetGlobalItem<EATooltip>().donator = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aegida Tempestas");
            Tooltip.SetDefault("50% chance not to consume ammo\nAegeda's donator item");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 70f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            int numberProjectiles = Main.rand.Next(1, 4);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(3));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7, 0);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<TheEqualizer>(), 1);
            recipe.AddIngredient(ItemType<Pyroplasm>(), 20);
            recipe.AddIngredient(ItemID.FragmentVortex, 12);
            recipe.AddIngredient(ItemID.Sapphire, 4);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.Next(0, 100) <= 50)
                return false;
            return true;
        }
    }
}
