using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Weapons.Tier1
{
    public class Electrozzitron : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 19;
            item.knockBack = 1f;
            item.GetGlobalItem<ItemEnergy>().energy = 3;

            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 5;
            item.UseSound = SoundID.Item61;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 1;

            item.shootSpeed = 6f;
            item.shoot = mod.ProjectileType("ElectroniumMine");
            item.useAmmo = 97;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("ElectroniumMine"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Electrozzitron");
            Tooltip.SetDefault("Looks like it will electrocute you any moment...\nShoots exploding mines");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("CopperBar", 3);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddIngredient(null, "CopperWire", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
