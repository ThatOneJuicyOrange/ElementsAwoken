using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class FistsOfFury : ModItem
    {
        public override void SetDefaults()
        {
            item.height = 32;
            item.width = 32;

            item.damage = 32;
            item.knockBack = 9f;

            item.melee = true;
            item.noMelee = true;
            item.useTurn = true;
            item.noUseGraphic = true;
            item.autoReuse = true;

            item.useAnimation = 9;
            item.useTime = 9;
            item.useStyle = 5;
            item.UseSound = SoundID.Item1;

            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 4;
            item.shoot = mod.ProjectileType("FistsOfFuryP");
            item.shootSpeed = 7f;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fists of Fury");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = Main.rand.Next(1, 2);
            for (int k = 0; k < numberProjectiles; k++)
            {
                position += new Vector2(Main.rand.Next(-15, 15), Main.rand.Next(-15, 15));
                Projectile.NewProjectile(position.X, position.Y, speedX * 0.4f, speedY * 0.4f, mod.ProjectileType("FistsOfFuryFire"), damage, knockBack, player.whoAmI, 0f, 0f);
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Fireblossom, 2);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
