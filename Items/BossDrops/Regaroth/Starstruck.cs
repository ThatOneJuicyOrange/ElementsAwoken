using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Regaroth
{
    public class Starstruck : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 64;

            item.damage = 50;
            item.knockBack = 5;

            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = 5;
            item.UseSound = SoundID.Item5;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 6;

            item.shoot = mod.ProjectileType("StarstruckArrow");
            item.shootSpeed = 20f;
            item.useAmmo = 40;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thundering Recurve");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("StarstruckArrow"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
    }
}
