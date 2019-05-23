using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Storyteller
{
    public class ForeverSword : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 48;
            item.melee = true;
            item.width = 50;
            item.height = 50;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 1;
            item.knockBack = 3;
            item.value = Item.buyPrice(0, 15, 0, 0);
            item.rare = 5;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("ForeverBolt");
            item.shootSpeed = 16f;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forever");
            Tooltip.SetDefault("Shoots life stealing bolts");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 1 + Main.rand.Next(4);
            for (int i = 0; i < numberProjectiles; i++)
            {
                float rand = Main.rand.Next(8, 12);
                rand = rand / 10;
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X * rand, perturbedSpeed.Y * rand, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
