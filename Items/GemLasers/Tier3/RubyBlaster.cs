using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;

namespace ElementsAwoken.Items.GemLasers.Tier3
{
    public class RubyBlaster : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 54;
            item.height = 28;

            item.ranged = true;
            item.autoReuse = true;

            item.damage = 60;
            item.knockBack = 4;

            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 5;

            item.value = Item.buyPrice(0, 30, 0, 0);
            item.rare = 7;

            item.shoot = mod.ProjectileType("GemRay");
            item.shootSpeed = 24f;

            item.useAmmo = AmmoID.Bullet;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ruby Blaster");
            Tooltip.SetDefault("Charges musket balls with crimson light");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int ai = 0;
            if (type == ProjectileID.Bullet)
            {
                type = mod.ProjectileType("GemRay");
                Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 12);
                ai = 5;
            }
            else Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 11);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, ai);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BeetleHusk, 8);
            recipe.AddIngredient(null, "RubyRifle", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
