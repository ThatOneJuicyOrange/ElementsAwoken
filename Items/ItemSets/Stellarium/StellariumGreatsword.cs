using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Stellarium
{
    public class StellariumGreatsword : ModItem
    {
        public override void SetDefaults()
        {          
            item.width = 60;   
            item.height = 60;

            item.damage = 160;
            item.knockBack = 6;

            item.useTime = 32;   
            item.useAnimation = 32;
            item.useStyle = 1;

            item.melee = true;
            item.autoReuse = true;

            item.value = Item.buyPrice(0, 30, 0, 0);
            item.rare = 10;    

            item.UseSound = SoundID.Item1;  
            item.shoot = mod.ProjectileType("StellarPortal");
            item.shootSpeed = 16f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stellarium Greatsword");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y - 500, 0f, 0f, mod.ProjectileType("StellarPortal"), damage, knockBack, Main.myPlayer, 0f, 0f);
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
