using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.ScourgeFighter
{
    public class ScourgeFighterRocketLauncher : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 19;
            item.knockBack = 3.5f;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useAnimation = 22;
            item.useTime = 22;
            item.useStyle = 5;
            item.UseSound = SoundID.Item11;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 6;

            item.shootSpeed = 6f;
            item.shoot = ProjectileID.RocketI;
            item.useAmmo = AmmoID.Rocket;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scourge Rocket Launcher");
            Tooltip.SetDefault("Ripped off the remains of the Scourge Fighter\nTurns normal rockets into scourge rockets");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.RocketI)
            {
                type = mod.ProjectileType("ScourgeRocketFriendly");
            }
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(8));
            Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            return false;
        }
    }
}
