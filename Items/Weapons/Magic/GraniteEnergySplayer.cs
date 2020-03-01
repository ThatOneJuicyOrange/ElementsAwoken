using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Magic
{
    public class GraniteEnergySplayer : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;

            item.damage = 8;
            item.mana = 4;
            item.knockBack = 5;

            item.useTime = 40;
            item.useAnimation = 40;

            Item.staff[item.type] = true;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = false;

            item.useStyle = 5;

            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 1;

            item.UseSound = SoundID.Item42;
            item.shoot = mod.ProjectileType("GraniteEnergyShot");
            item.shootSpeed = 5f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Granite Energy Splayer");
            Tooltip.SetDefault("Fires a short burst of granite energy");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = Main.rand.Next(3,7);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(35));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GraniteBlock, 20);
            recipe.AddIngredient(null, "Stardust", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
