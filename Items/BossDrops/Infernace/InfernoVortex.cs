using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Infernace
{
    public class InfernoVortex : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 32;
            item.magic = true;
            item.width = 50;
            item.height = 50;
            item.useTime = 22;
            item.useAnimation = 22;
            Item.staff[item.type] = true;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 3;
            item.mana = 5;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("SpinningFlame");
            item.shootSpeed = 18f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inferno Vortex");
        }
        public override bool CanUseItem(Player player)
        {
            int max = 11;
            if (player.ownedProjectileCounts[mod.ProjectileType("SpinningFlame")] >= max)
            {
                return false;
            }
            else return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("SpinningFlame"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }
    }
}
