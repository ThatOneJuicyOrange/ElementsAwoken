using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;

namespace ElementsAwoken.Items.GemLasers.Tier4
{
    public class PrismGlow : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 28;

            item.ranged = true;
            item.autoReuse = true;

            item.UseSound = SoundID.Item12;
            item.damage = 85;
            item.knockBack = 4;

            item.useTime = 5;
            item.useAnimation = 5;

            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 10;

            item.shoot = mod.ProjectileType("AmberRay");
            item.shootSpeed = 24f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 1 + Main.rand.Next(2); 
            for (int i = 0; i < numberProjectiles; i++)
            {
                switch (Main.rand.Next(7))
                {
                    case 0: type = mod.ProjectileType("AmberLaserHoming"); break;
                    case 1: type = mod.ProjectileType("AmethystLaserHoming"); break;
                    case 2: type = mod.ProjectileType("DiamondLaserHoming"); break;
                    case 3: type = mod.ProjectileType("EmeraldLaserHoming"); break;
                    case 4: type = mod.ProjectileType("RubyLaserHoming"); break;
                    case 5: type = mod.ProjectileType("SapphireLaserHoming"); break;
                    case 6: type = mod.ProjectileType("TopazLaserHoming"); break;
                }
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(7)) * 1.2f;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prism Glow");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(null, "AmberBlaster", 1);
            recipe.AddIngredient(null, "AmethystBlaster", 1);
            recipe.AddIngredient(null, "DiamondBlaster", 1);
            recipe.AddIngredient(null, "EmeraldBlaster", 1);
            recipe.AddIngredient(null, "RubyBlaster", 1);
            recipe.AddIngredient(null, "SapphireBlaster", 1);
            recipe.AddIngredient(null, "TopazBlaster", 1);
            recipe.AddIngredient(null, "Pyroplasm", 50);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
