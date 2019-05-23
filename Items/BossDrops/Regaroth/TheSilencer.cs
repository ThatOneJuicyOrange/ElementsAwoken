using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Regaroth
{
    public class TheSilencer : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 70;
            item.height = 70;

            item.damage = 57;
            item.knockBack = 5;


            item.melee = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.useAnimation = 12;
            item.useTime = 12;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 6;

            item.shoot = mod.ProjectileType("TheSilencerP");
            item.shootSpeed = 18f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Silencer");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("TheSilencerP"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            if (Main.rand.Next(6) == 0)
            {
                float numberProjectiles = 2;
                float rotation = MathHelper.ToRadians(7);
                position += Vector2.Normalize(new Vector2(speedX, speedY)) * 10f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
            }
            return false;
        }
    }
}
