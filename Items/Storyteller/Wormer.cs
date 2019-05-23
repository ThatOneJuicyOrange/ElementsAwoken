using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Storyteller
{
    public class Wormer : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 29;
            item.knockBack = 1f;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useAnimation = 40;
            item.useTime = 40;
            item.useStyle = 5;

            item.value = Item.buyPrice(0, 8, 0, 0);
            item.rare = 3;

            item.UseSound = SoundID.Item11;
            item.shootSpeed = 9f;
            item.shoot = mod.ProjectileType("Worm");
            item.useAmmo = 97;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wormer");
            Tooltip.SetDefault("Fires a parasitic worm\n50% chance to not consume ammo");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Worm"), damage, knockBack, player.whoAmI);
            return false;
        }
        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.Next(0, 100) <= 50)
                return false;
            return true;
        }
    }
}
