using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.zVanilla
{
    public class Shockstorm : ModItem
    {
        
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 48;

            item.damage = 213;
            item.knockBack = 2.25f;

            item.magic = true;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.mana = 6;

            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = 5;

            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 10;

            item.UseSound = SoundID.Item8;
            item.shoot = mod.ProjectileType("ShockstormPortal");
            item.shootSpeed = 9f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shockstorm");
            Tooltip.SetDefault("Summons a lightning orb which attacks nearby enemies\nOnly two lightning orbs can be active at once");
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("ShockstormPortal")] >= 2)
            {
                return false;
            }
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, type, 0, knockBack, player.whoAmI, 0f, damage);
            return false;
        }
    }
}
