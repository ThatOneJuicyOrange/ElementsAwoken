using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.zVanilla
{
    class TearsOfSorrow : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 44;

            item.knockBack = 2f;
            item.damage = 15;

            item.UseSound = SoundID.Item5;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;

            item.autoReuse = true;
            item.ranged = true;
            item.noMelee = true;

            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 1;

            item.shoot = 10;
            item.shootSpeed = 8f;
            item.useAmmo = 40;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tears of Sorrow");
            Tooltip.SetDefault("Turns normal arrows into tear arrows that slow enemies down greatly");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = mod.ProjectileType("TearArrow");
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
    }
}
