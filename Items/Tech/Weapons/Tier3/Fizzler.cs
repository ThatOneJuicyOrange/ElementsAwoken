using ElementsAwoken.Items.Tech.Materials;
using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Tech.Weapons.Tier3
{
    public class Fizzler : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 22;
            item.knockBack = 1f;
            item.GetGlobalItem<ItemEnergy>().energy = 5;

            item.useAnimation = 28;
            item.useTime = 28;
            item.useStyle = 5;
            item.UseSound = SoundID.Item20;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 3;

            item.shootSpeed = 6f;
            item.shoot = ProjectileType<FizzlerP>();
            item.useAmmo = AmmoID.Bullet;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileType<FizzlerP>(), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fizzler");
            Tooltip.SetDefault("Shoots a splitting fireball");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddIngredient(ItemType<GoldWire>(), 6);
            recipe.AddIngredient(ItemType<Capacitor>(), 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
