using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Regaroth
{
    public class EyeOfRegaroth : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;

            item.damage = 42;
            item.mana = 5;

            item.useTime = 12;
            item.useAnimation = 16;
            item.useStyle = 5;
            item.UseSound = SoundID.Item8;

            item.magic = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            Item.staff[item.type] = true;

            item.knockBack = 2;

            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 6;

            item.shoot = mod.ProjectileType("TheSilencerP");
            item.shootSpeed = 18f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eye of Regaroth");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 2 + Main.rand.Next(2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        // making dust on hand
        public override void HoldItem(Player player)
        {
            Vector2 vector32 = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
            if (player.direction != 1)
            {
                vector32.X = (float)player.bodyFrame.Width - vector32.X;
            }
            if (player.gravDir != 1f)
            {
                vector32.Y = (float)player.bodyFrame.Height - vector32.Y;
            }
            vector32 -= new Vector2((float)(player.bodyFrame.Width - player.width), (float)(player.bodyFrame.Height - 42)) / 2f;
            Vector2 position = player.RotatedRelativePoint(player.position + vector32, true) - player.velocity;
            for (int num277 = 0; num277 < 4; num277++)
            {
                int dustType = 135;
                switch (Main.rand.Next(2))
                {
                    case 0:
                        dustType = 135;
                        break;
                    case 1:
                        dustType = 164;
                        break;
                    default: break;
                }
                Dust dust = Main.dust[Dust.NewDust(player.Center, 0, 0, dustType, (float)(player.direction * 2), 0f, 150, default(Color), 1.3f)];
                dust.position = position;
                dust.velocity *= 0f;
                dust.noGravity = true;
                dust.fadeIn = 1f;
                dust.velocity += player.velocity;
                if (Main.rand.Next(2) == 0)
                {
                    dust.position += Utils.RandomVector2(Main.rand, -4f, 4f);
                    dust.scale += Main.rand.NextFloat();
                    if (Main.rand.Next(2) == 0)
                    {
                        dust.customData = player;
                    }
                }
            }
        }
    }
}
