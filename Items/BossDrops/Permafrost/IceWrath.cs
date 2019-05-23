using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Permafrost
{
    public class IceWrath : ModItem
    {

        public override void SetDefaults()
        {
            item.damage = 50;
            item.ranged = true;
            item.width = 48;
            item.height = 64;
            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 46, 0, 0);
            item.rare = 7;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("IceWrathArrow");
            item.shootSpeed = 20f;
            item.useAmmo = 40;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Wrath");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("IceWrathArrow"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
    }
}
