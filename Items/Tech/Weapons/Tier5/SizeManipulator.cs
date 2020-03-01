using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Weapons.Tier5
{
    public class SizeManipulator : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 10;
            item.knockBack = 1f;
            item.GetGlobalItem<ItemEnergy>().energy = 25;

            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 5;
            item.UseSound = SoundID.Item12;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 6;

            item.shootSpeed = 6f;
            item.shoot = mod.ProjectileType("GrowinatorP");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("ShrinkinatorP"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            }
            else
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("GrowinatorP"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Size Manipulator");
            Tooltip.SetDefault("Left click to grow enemies\nRight Click to shrink enemies\nOnly works on minor enemies");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofSight, 4);
            recipe.AddIngredient(ItemID.HallowedBar, 2);
            recipe.AddIngredient(null, "Transistor", 5);
            recipe.AddIngredient(null, "Shrinkinator", 1);
            recipe.AddIngredient(null, "Growinator", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
