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

            item.damage = 85;
            item.knockBack = 4;

            item.useTime = 5;
            item.useAnimation = 5;
            item.useStyle = 5;

            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 10;

            item.shoot = mod.ProjectileType("GemLaserHoming");
            item.shootSpeed = 28f;

            item.useAmmo = AmmoID.Bullet;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prism Glow");
            Tooltip.SetDefault("Charges musket balls with mystical light");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int ai = 0;
            if (type == ProjectileID.Bullet)
            {
                type = mod.ProjectileType("GemLaserHoming");
                Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 12);
                ai = Main.rand.Next(7);
            }
            else Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 11);
            int numberProjectiles = 1 + Main.rand.Next(2); 
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(7));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI,ai);
            }
            return false;
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
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
