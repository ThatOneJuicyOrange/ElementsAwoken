using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Weapons.Tier4
{
    public class Shrinkinator : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 10;
            item.knockBack = 1f;
            item.GetGlobalItem<ItemEnergy>().energy = 20;

            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 5;
            item.UseSound = SoundID.Item12;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 5;

            item.shootSpeed = 6f;
            item.shoot = mod.ProjectileType("ShrinkinatorP");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("ShrinkinatorP"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shrinkinator");
            Tooltip.SetDefault("Shrinks minor enemies");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddIngredient(ItemID.SoulofLight, 4);
            recipe.AddIngredient(null, "GoldWire", 5);
            recipe.AddIngredient(null, "Capacitor", 1);
            recipe.AddIngredient(null, "SiliconBoard", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
