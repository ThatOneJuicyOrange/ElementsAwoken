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
            float rotation = MathHelper.TwoPi;
            float numProj = 6;
            float speed = 12f;
            for (int i = 0; i < numProj; i++)
            {
                Vector2 perturbedSpeed = (rotation / numProj * i).ToRotationVector2() * speed;
                Projectile.NewProjectile(position,perturbedSpeed, type, damage, knockBack, player.whoAmI, Main.MouseWorld.X, Main.MouseWorld.Y);
            }
            return false;
        }
    }
}
