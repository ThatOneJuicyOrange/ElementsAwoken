using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Aqueous
{
    class BrinyBuster : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 10;

            item.damage = 60;
            item.knockBack = 7.5f;

            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 8;

            item.melee = true;
            item.autoReuse = true;
            item.noMelee = true;
            item.noUseGraphic = true;

            item.useStyle = 5;
            item.useAnimation = 40; 
            item.useTime = 40;
            item.UseSound = SoundID.Item1;

            item.shoot = mod.ProjectileType("BrinyBusterP");
            item.shootSpeed = 15.1f;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Briny Buster");
            Tooltip.SetDefault("Strike your enemies with the force of the ocean");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
