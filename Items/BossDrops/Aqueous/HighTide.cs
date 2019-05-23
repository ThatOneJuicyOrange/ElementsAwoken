using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Aqueous
{
    public class HighTide : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 38;  
            item.height = 38;
            item.damage = 70;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useAnimation = 7;
            item.useStyle = 1;
            item.useTime = 7;
            item.knockBack = 7.5f;
            item.UseSound = SoundID.Item1;
            item.thrown = true;
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 8;
            item.shoot = mod.ProjectileType("HighTideP");
            item.shootSpeed = 16f;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("High Tide");
      Tooltip.SetDefault("Throws 2 chakrams that explode into bubbles on enemy hit");
    }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float numberProjectiles = 2;
            float rotation = MathHelper.ToRadians(3);
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 2f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("HighTideP"), damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
