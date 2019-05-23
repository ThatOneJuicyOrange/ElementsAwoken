using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.ScourgeFighter
{
    public class ScourgeSword : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 32;
            item.knockBack = 2;

            item.autoReuse = true;
            item.useTurn = true;
            item.melee = true;

            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 6;

            item.shoot = mod.ProjectileType("BountyP");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bounty Hunt");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float numberProjectiles = 6;
            float rotation = MathHelper.ToRadians(360);
            float speed = 3f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(4, 4).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * speed;
                int num1 = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, Main.myPlayer, Main.MouseWorld.X, Main.MouseWorld.Y);
            }
            return false;
        }
    }
}
