using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;
using System;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles;
using ElementsAwoken.Tiles.Crafting;

namespace ElementsAwoken.Items.ItemSets.Radia
{
    public class GlobuleCannon : ModItem
    {
        public override string Texture { get { return "ElementsAwoken/Items/TODO"; } }
        private int shotCount = 0;
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 18;

            item.damage = 360;
            item.knockBack = 15;

            item.useTime = 12;
            item.useAnimation = 36;
            item.reuseDelay = 24;
            item.useStyle = 5;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;

            item.shoot = ProjectileType<RadiantGlobule>();
            item.shootSpeed = 24f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Globule Cannon");
            Tooltip.SetDefault("Shoots a cluster of radiant goop");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 40f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0)) position += muzzleOffset;

            shotCount++;
            Main.PlaySound(2, (int)position.X, (int)position.Y, 95, 1, -0.35f);
            float angle = 4 * shotCount;
            int numberProjectiles = 3 * shotCount;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(angle));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            if (shotCount >= 3) shotCount = 0;
            return false;
        }

    }
}
