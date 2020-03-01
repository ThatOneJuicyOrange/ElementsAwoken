using ElementsAwoken.Items.Tech.Materials;
using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Tech.Weapons.Tier5
{
    public class NapalmCannon : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 32;
            item.knockBack = 1f;
            item.GetGlobalItem<ItemEnergy>().energy = 15;

            item.useAnimation = 28;
            item.useTime = 28;
            item.useStyle = 5;
            item.UseSound = SoundID.Item20;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 2, 50, 0);
            item.rare = 6;

            item.shootSpeed = 12f;
            item.shoot = ProjectileType<NapalmCannonP>();
            item.useAmmo = AmmoID.Bullet;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileType<NapalmCannonP>(), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Napalm Cannon");
            Tooltip.SetDefault("Shoots a glob of sticky napalm");
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7, 0);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("ElementsAwoken:AdamantiteBar", 12);
            recipe.AddIngredient(ItemType<Capacitor>(), 1);
            recipe.AddIngredient(ItemType<GoldWire>(), 10);
            recipe.AddIngredient(ItemType<SiliconBoard>(), 1);
            recipe.AddIngredient(ItemType<Transistor>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
        }
    }
}
