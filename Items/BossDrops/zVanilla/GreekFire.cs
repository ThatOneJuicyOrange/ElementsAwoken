using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.zVanilla
{
    public class GreekFire : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;

            item.damage = 45;
            item.knockBack = 2;
            item.mana = 5;

            item.magic = true;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.autoReuse = true;


            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.UseSound = SoundID.Item13;

            item.value = Item.buyPrice(0, 75, 0, 0);
            item.rare = 8;

            item.shoot = mod.ProjectileType("GreekFire");
            item.shootSpeed = 14f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Greek Fire");
            Tooltip.SetDefault("Fires bouncing greek flames");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpookyWood, 30);
            recipe.AddIngredient(ItemID.SpellTome);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = Main.rand.Next(2, 3);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
                perturbedSpeed *= Main.rand.NextFloat(0.8f, 1.1f);
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
