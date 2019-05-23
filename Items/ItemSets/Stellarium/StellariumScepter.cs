using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Stellarium
{
    public class StellariumScepter : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 48;

            item.damage = 257;
            item.knockBack = 4f;
            item.mana = 9;

            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = 5;
            Item.staff[item.type] = true;

            item.value = Item.buyPrice(0, 30, 0, 0);
            item.rare = 10;

            item.UseSound = SoundID.Item8;
            item.shoot = mod.ProjectileType("StellariumBolt");
            item.shootSpeed = 16f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stellarium Scepter");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, (int)(damage * 1.5f), knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "StellariumBar", 15);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
