using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.TheGuardian
{
    public class TemplesWrath : ModItem
    {

        public override void SetDefaults()
        {
            item.damage = 50;
            item.ranged = true;
            item.width = 48;
            item.height = 64;
            item.useTime = 5;
            item.useAnimation = 5;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 10;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("TemplesWrathArrow");
            item.shootSpeed = 26f;
            item.useAmmo = 40;
        }
        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() > .60f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Temple's Wrath");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int addPosition = Main.rand.Next(-30, 8);
            Projectile.NewProjectile(position.X + addPosition, position.Y + addPosition, speedX, speedY, mod.ProjectileType("TemplesWrathArrow"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            if (Main.rand.Next(5) == 0)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("TemplesWrathSword"), 70, knockBack, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }
    }
}
