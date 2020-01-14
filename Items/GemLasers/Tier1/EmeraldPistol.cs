using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;

namespace ElementsAwoken.Items.GemLasers.Tier1
{
    public class EmeraldPistol : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 24;

            item.ranged = true;
            item.autoReuse = true;

            item.damage = 18;
            item.knockBack = 4;

            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 5;

            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 1;

            item.shoot = mod.ProjectileType("GemLaser");
            item.shootSpeed = 24f;

            item.useAmmo = AmmoID.Bullet;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Emerald Pistol");
            Tooltip.SetDefault("Charges musket balls with emerald light");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int ai = 0;
            if (type == ProjectileID.Bullet)
            {
                type = mod.ProjectileType("GemLaser");
                Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 12);
                ai = 3;
            }
            else Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 11);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, ai);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MeteoriteBar, 8);
            recipe.AddIngredient(ItemID.Emerald, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
