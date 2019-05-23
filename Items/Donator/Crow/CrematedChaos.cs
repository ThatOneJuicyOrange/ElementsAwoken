using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Crow
{
    public class CrematedChaos : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 16;

            item.damage = 34;
            item.mana = 5;
            item.knockBack = 3.25f;

            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useTime = 8;
            item.useAnimation = 16;
            item.useStyle = 5;
            item.UseSound = SoundID.Item34;

            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 5;

            item.shoot = mod.ProjectileType("PoisonFire");
            item.shootSpeed = 5f;

            item.GetGlobalItem<EATooltip>().donator = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cremated Chaos");
            Tooltip.SetDefault("Crow's donator item");
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, -4);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numProj = 2;
            for (int i = 0; i < numProj; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(3));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
