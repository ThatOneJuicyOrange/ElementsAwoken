using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles;
using ElementsAwoken.Projectiles.Arrows;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.BossDrops.RadiantMaster
{
    public class RadiantBow : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 64;

            item.damage = 360;
            item.knockBack = 5;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useTime = 16;
            item.useAnimation = 16;
            item.UseSound = SoundID.Item5;
            item.useStyle = 5;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;

            item.shoot = 10;
            item.shootSpeed = 12f;
            item.useAmmo = AmmoID.Arrow;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rosy Skies");
            Tooltip.SetDefault("Turns normal arrows homing radiant stars");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly) type = ProjectileType<RadiantStarBow>();
            float numberProjectiles = 5;
            float rotation = MathHelper.ToRadians(15);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
