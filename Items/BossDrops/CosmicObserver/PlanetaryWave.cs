using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.CosmicObserver
{
    public class PlanetaryWave : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 35;
            item.knockBack = 2;
            item.mana = 6;

            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            Item.staff[item.type] = true;

            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.UseSound = SoundID.Item8;

            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 4;

            item.shoot = mod.ProjectileType("PlanetaryWavePortal");
            item.shootSpeed = 18f;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("PlanetaryWavePortal")] != 0)
            {
                return false;
            }
            return true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Planetary Wave");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(player.Center.X, player.Center.Y - 70, 0f, 0f, mod.ProjectileType("PlanetaryWavePortal"), damage, knockBack, player.whoAmI, speedX, speedY);
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
 