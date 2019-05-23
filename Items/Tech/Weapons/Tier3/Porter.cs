using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Weapons.Tier3
{
    public class Porter : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 18;
            item.knockBack = 1f;
            item.GetGlobalItem<ItemEnergy>().energy = 13;

            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 5;
            item.UseSound = SoundID.Item12;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 3;

            item.shootSpeed = 6f;
            item.shoot = mod.ProjectileType("PorterP");
            item.useAmmo = 97;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("PorterP"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Porter");
            Tooltip.SetDefault("Teleports minor enemies around");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddIngredient(null, "GoldWire", 10);
            recipe.AddIngredient(null, "Capacitor", 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
