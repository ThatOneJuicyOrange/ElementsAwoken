using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.TheCelestial
{
    public class CelestialInferno : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;

            item.damage = 68;
            item.knockBack = 2;
            item.mana = 5;

            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.UseSound = SoundID.Item8;

            Item.staff[item.type] = true;
            item.magic = true;
            item.autoReuse = true;
            item.noMelee = true;

            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 6;

            item.shoot = mod.ProjectileType("CelestialInfernoSpin");
            item.shootSpeed = 18f;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("CelestialInfernoSpin")] > 12)
            {
                return false;
            }
            return true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skylight Swirl");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, Main.rand.Next(4));
            return false;
        }
    }
}
