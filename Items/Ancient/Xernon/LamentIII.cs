using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Ancient.Xernon
{
    public class LamentIII : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 96;
            item.mana = 15;
            item.knockBack = 6;
            item.crit = 12;

            item.useStyle = 5;
            item.useTime = 7;
            item.useAnimation = 21;

            Item.staff[item.type] = true;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 10;

            item.UseSound = SoundID.Item20;
            item.shoot = mod.ProjectileType("LamentBallExplosive");
            item.shootSpeed = 13f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lament III");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(7));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            if (Main.rand.Next(5) == 0)
            {
                Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 88);
                int choice = Main.rand.Next(2);
                if (choice == 0)
                {
                    Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("LamentWave"), (int)(item.damage * 1.5f), knockBack, player.whoAmI, 0f, 0f);
                }
                else if (choice == 1)
                {
                    Projectile.NewProjectile(position.X, position.Y, speedX * 1.3f, speedY * 1.3f, mod.ProjectileType("LamentPierce"), (int)(item.damage * 1.2f), knockBack, player.whoAmI, 0f, 0f);
                }
                return false;
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "LamentII", 1);
            recipe.AddIngredient(ItemID.FragmentNebula, 5);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddIngredient(ItemID.FragmentVortex, 5);
            recipe.AddIngredient(ItemID.LunarBar, 18);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
