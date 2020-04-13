using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Wasteland
{
    public class Pincer : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 44; 
            
            item.damage = 14;
            item.knockBack = 4f;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = 5;
            item.UseSound = SoundID.Item5;

            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 2;

            item.shoot = 10;
            item.shootSpeed = 10f;
            item.useAmmo = 40;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Defender's Recurve");
            Tooltip.SetDefault("Turns normal arrows into venom imbued arrows");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly) type = mod.ProjectileType("PincerArrow");
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
    }
}
