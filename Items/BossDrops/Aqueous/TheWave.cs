using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Aqueous
{
    public class TheWave : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 60;

            item.damage = 70;
            item.knockBack = 18;

            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 8;

            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;

            item.shoot = ProjectileID.Bubble;
            item.shootSpeed = 20f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Overflow");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = Main.rand.Next(2, 4);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(6));
                perturbedSpeed *= Main.rand.NextFloat(0.7f, 1.1f);
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
