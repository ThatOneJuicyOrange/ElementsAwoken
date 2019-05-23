using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;

namespace ElementsAwoken.Items.BossDrops.Infernace
{
    public class FireBlaster : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 18;

            item.damage = 18;
            item.knockBack = 4;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 5;
            item.UseSound = SoundID.Item11;

            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 3;

            item.ranged = true;
            item.autoReuse = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.shoot = mod.ProjectileType("FireBlasterBolt");
            item.useAmmo = 97;
            item.shootSpeed = 8f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Blaster");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
            Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("FireBlasterBolt"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
    }
}
