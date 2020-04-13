using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.BossDrops.Azana
{
    public class ChaoticImpaler : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 58;
            item.height = 22;

            item.knockBack = 2.25f;
            item.damage = 190;

            item.useTime = 9;
            item.useAnimation = 9;
            item.useStyle = 5;

            item.ranged = true;
            item.autoReuse = true;
            item.noMelee = true;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;
            item.value = Item.sellPrice(0, 35, 0, 0);

            item.UseSound = SoundID.Item5;
            item.shoot = 10;
            item.shootSpeed = 18f;
            item.useAmmo = 40;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaotron Impaler");
            Tooltip.SetDefault("Charges arrows into chaotron lasers\nHas a chance to fire a high damaging chaotron discharge");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 3;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(4));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<ChaosLaser>(), damage, knockBack, player.whoAmI);
            }
            if (Main.rand.Next(5) == 0)
            {
                Main.PlaySound(2, (int)position.X, (int)position.Y, 92, 1, -0.3f);
                int numberProjectiles2 = 2;
                for (int i = 0; i < numberProjectiles2; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(3));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<ChaoticBlast>(), damage * 3, knockBack, player.whoAmI);
                }
            }
            return false;
        }
    }
}
