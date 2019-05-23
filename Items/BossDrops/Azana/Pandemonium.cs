using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Azana
{
    public class Pandemonium : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 70;
            item.height = 70;

            item.damage = 340;
            item.knockBack = 5;

            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;

            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;
            item.value = Item.sellPrice(0, 35, 0, 0);

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("PandemoniumBlast");
            item.shootSpeed = 18f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pandemonium");
            Tooltip.SetDefault("");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(3));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
