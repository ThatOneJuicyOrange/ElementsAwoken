using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Magic.Tomes
{
    public class FrostMine : ModItem
    {

        public override void SetDefaults()
        {
            item.height = 30;
            item.width = 28;

            item.damage = 58;
            item.knockBack = 3.5f;

            item.noMelee = true;
            item.autoReuse = true;
            item.magic = true;

            item.mana = 5;

            item.useTime = 8;
            item.useAnimation = 20;
            item.useStyle = 5;

            item.value = Item.buyPrice(0, 20, 0, 0);
            item.rare = 6;

            item.UseSound = SoundID.Item20;
            item.shoot = mod.ProjectileType("FrostMine");
            item.shootSpeed = 1f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Mine");
            Tooltip.SetDefault("Exploding frost mines appear around you");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile.NewProjectile(player.Center.X + Main.rand.Next(-360, 360), player.Center.X + Main.rand.Next(-360, 360), 0, 0, mod.ProjectileType("FrostMine"), damage, knockBack, player.whoAmI, 0f, 0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FrostCore, 2);
            recipe.AddIngredient(ItemID.SpellTome, 1);
            recipe.AddIngredient(null, "Stardust", 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
