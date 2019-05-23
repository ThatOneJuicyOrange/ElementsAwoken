using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.CosmicObserver
{
    public class CosmicCrusher : ModItem
    {
        public override void SetDefaults()
        {          
            item.width = 60;   
            item.height = 60;

            item.damage = 43;
            item.knockBack = 6;

            item.useTime = 48;   
            item.useAnimation = 48;
            item.useStyle = 1;

            item.melee = true;
            item.autoReuse = true;
            item.useTurn = false;

            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 4;

            item.UseSound = SoundID.Item1;  
            item.shoot = mod.ProjectileType("CosmicCrusherSpawner");
            item.shootSpeed = 16f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Crusher");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, 0f, 0f, type, damage, knockBack, Main.myPlayer, Main.MouseWorld.X, Main.MouseWorld.Y);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CosmicShard", 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
