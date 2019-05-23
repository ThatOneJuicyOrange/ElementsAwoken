using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;

namespace ElementsAwoken.Items.BossDrops.Permafrost
{
    public class Frigidblaster : ModItem
    {
        public override void SetDefaults()
        {
            item.knockBack = 4;
            item.damage = 48;

            item.width = 38;
            item.height = 18;

            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 5;

            item.ranged = true;
            item.autoReuse = true;
            item.noMelee = true;

            item.value = Item.buyPrice(0, 46, 0, 0);
            item.rare = 7;

            item.shoot = 10;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item36;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frigidblaster");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 2 + Main.rand.Next(2); //This defines how many projectiles to shot. 4 + Main.rand.Next(2)= 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10)); // This defines the projectiles random spread . 30 degree spread.
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("Frigidbullet"), damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
