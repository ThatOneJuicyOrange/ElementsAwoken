using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.zVanilla
{
    class Starthrower : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 48;

            item.damage = 75;
            item.mana = 6;
            item.knockBack = 2.25f;

            item.magic = true;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;

            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 10;

            item.UseSound = SoundID.Item43;
            item.shoot = mod.ProjectileType("Star");
            item.shootSpeed = 20f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starthrower");
            Tooltip.SetDefault("Fires 3 homing stars");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(10);
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .4f;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
