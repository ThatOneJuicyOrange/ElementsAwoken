using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles;
using ElementsAwoken.Projectiles.Bullets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.BossDrops.Azana
{
    public class ChaoticGaze : ModItem
    {
        public int hitCount = 0;
        public int hitTimer = 0;
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

            item.useTime = 4;
            item.useAnimation = 16;
            item.reuseDelay = 18;
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
            Tooltip.SetDefault("Turns regular bullets into outbreak darts that increase the damage with each hit to a maximum of 2x\nHas a chance to fire a chaos eater dealing 3x damage\n50% chance to not consume ammo");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.Bullet) type = ProjectileType<OutbreakDart>();
            damage = (int)(damage * (1 + MathHelper.Clamp(hitCount / 60, 0, 1)));
            //innacurate fire
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(4));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            if (Main.rand.Next(6) == 0)
            {
                Main.PlaySound(SoundID.Item95, position);
                Projectile.NewProjectile(position.X, position.Y, speedX * 0.75f, speedY * 0.75f, ProjectileType<ChaosGazer>(), damage * 3, 0, player.whoAmI, 0f, 0f);
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void UpdateInventory(Player player)
        {
            hitTimer--;
            if (hitTimer <= 0)
            {
                hitCount = 0;
            }
        }
        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() > .50f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7, 0);
        }
    }
}
