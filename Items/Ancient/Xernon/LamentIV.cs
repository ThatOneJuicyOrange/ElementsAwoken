using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Ancient.Xernon
{
    public class LamentIV : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 240;
            item.mana = 12;
            item.knockBack = 9;
            item.crit = 20;

            item.useStyle = 5;
            item.useTime = 3;
            item.useAnimation = 15;

            Item.staff[item.type] = true;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 50, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 14;

            item.UseSound = SoundID.Item20;
            item.shoot = mod.ProjectileType("LamentBallExplosive");
            item.shootSpeed = 15f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lament IV");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(7));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            if (Main.rand.Next(3) == 0)type = mod.ProjectileType("LamentSuperPierce");
            if (Main.rand.Next(5) == 0)
            {
                Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 88);
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("LamentWave"), (int)(item.damage * 2f), knockBack, player.whoAmI, 0f, 0f);

                return false;
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "LamentIII", 1);
            recipe.AddIngredient(null, "AncientShard", 5);
            recipe.AddIngredient(null, "VoiditeBar", 4);
            recipe.AddIngredient(null, "DiscordantBar", 20);
            recipe.AddTile(null, "ChaoticCrucible");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
