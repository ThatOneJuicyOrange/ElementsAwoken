using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Azana
{
    public class ChaoticGaze : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 14;

            item.damage = 280;
            item.knockBack = 4;
            item.crit = 10;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useTime = 3;
            item.useAnimation = 3;
            item.useStyle = 5;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;
            item.value = Item.sellPrice(0, 35, 0, 0);

            item.UseSound = SoundID.Item36;
            item.shoot = 10;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Outbreak");
            Tooltip.SetDefault("Occasionally fires a chaos eye\n50% chance to not consume ammo");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            //innacurate fire
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(6));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            if (Main.rand.Next(6) == 0)
            {
                Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 72);
                int choice = Main.rand.Next(2);
                if (choice == 0)
                {
                    Projectile.NewProjectile(position.X, position.Y, speedX * 0.5f, speedY * 0.5f, mod.ProjectileType("ChaosGazer"), item.damage * 3, 0, player.whoAmI, 0f, 0f);
                }
                return false;
            }
            return true;
        }
        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() > .50f;
        }
    }
}
