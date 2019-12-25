using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Storyteller
{
    public class SearingBlaze : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 64;

            item.damage = 45;
            item.knockBack = 5;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useTime = 22;
            item.useAnimation = 22;
            item.UseSound = SoundID.Item5;
            item.useStyle = 5;

            item.value = Item.buyPrice(0, 75, 0, 0);
            item.rare = 10;

            item.shoot = 10;
            item.shootSpeed = 20f;
            item.useAmmo = 40;
        }
        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() > .50f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Searing Blaze");
            Tooltip.SetDefault("Turns normal arrows into searing arrows\nReleases thick clouds of smoke when arrows collide\n50% chance not to consume ammo");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == 1) // The normal arrow
            {
                type = mod.ProjectileType("SearingArrow");
            }
            //tsunami code from 'Player'
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float pi = 0.314159274f;
            int numProjectiles = 3;
            Vector2 vector14 = new Vector2(speedX, speedY);
            vector14.Normalize();
            vector14 *= 40f;
            bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector14, 0, 0);
            for (int num123 = 0; num123 < numProjectiles; num123++)
            {
                float num124 = (float)num123 - ((float)numProjectiles - 1f) / 2f;
                Vector2 vector15 = vector14.RotatedBy((double)(pi * num124), default(Vector2));
                if (!flag11)
                {
                    vector15 -= vector14;
                }
                int num125 = Projectile.NewProjectile(vector2.X + vector15.X, vector2.Y + vector15.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
                Main.projectile[num125].noDropItem = true;
            }
            return false;
        }
    }
}
