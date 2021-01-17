using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Mortemite
{
    public class Downfall : ModItem
    {

        public override void SetDefaults()
        {
            item.damage = 110;
            item.ranged = true;
            item.width = 16;
            item.height = 14;
            item.useTime = 5;
            item.useAnimation = 5;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 10;
            item.UseSound = SoundID.Item36;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Downfall");
            Tooltip.SetDefault("Occasionally fires a mortemite blade that does double damage\n50% chance to not consume ammo");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            //innacurate fire
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(7));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            if (Main.rand.Next(5) == 0)
            {
                Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 72);
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("MortemiteBlade"), item.damage * 2, 0, player.whoAmI, 0f, 0f);
                return false;
            }
            return true;
        }
        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() > .50f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MortemiteDust", 50);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
