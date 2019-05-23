using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Magic
{
    public class Nightfall : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 54;
            item.height = 52;

            item.damage = 40;
            item.mana = 18;
            item.knockBack = 5;

            item.useTime = 25;
            item.useAnimation = 25;
            item.UseSound = SoundID.Item20;
            item.useStyle = 5;

            Item.staff[item.type] = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.noMelee = true;
            item.magic = true;

            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 10;

            item.shoot = mod.ProjectileType("Nightball");
            item.shootSpeed = 9f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nightfall");
            Tooltip.SetDefault("Time to go to sleep...");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 3;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile.NewProjectile(Main.MouseWorld.X + Main.rand.Next(-60, 60), Main.MouseWorld.Y + Main.rand.Next(-60, 60), 0, 0, mod.ProjectileType("Nightball"), damage, knockBack, Main.myPlayer, 0f, 0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 16);
            recipe.AddIngredient(ItemID.SoulofNight, 12);
            recipe.AddIngredient(null, "CInfinityCrys", 1);
            recipe.AddIngredient(null, "Pyroplasm", 50);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
