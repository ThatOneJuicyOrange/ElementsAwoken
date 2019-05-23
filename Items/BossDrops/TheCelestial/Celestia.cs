using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;

namespace ElementsAwoken.Items.BossDrops.TheCelestial
{
    public class Celestia : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 28;

            item.ranged = true;
            item.autoReuse = true;
            item.useTurn = true;

            item.damage = 50;
            item.knockBack = 4;

            item.useTime = 46;
            item.useAnimation = 46;
            item.useStyle = 5;
            item.UseSound = SoundID.Item12;

            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 6;

            item.shoot = mod.ProjectileType("CelestiaPortal");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestia");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = Main.rand.Next(1, 3);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("CelestiaPortal"), damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
