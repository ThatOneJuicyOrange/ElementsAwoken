using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Permafrost
{
    public class IceReaver : ModItem
    {
        int uses = 0;
        public override void SetDefaults()
        {
            item.width = 70;
            item.height = 70;

            item.knockBack = 5;
            item.damage = 57;

            item.autoReuse = true;
            item.melee = true;
            item.useTurn = true;

            item.useTime = 4;
            item.useAnimation = 12;
            item.useStyle = 1;

            item.value = Item.buyPrice(0, 46, 0, 0);
            item.rare = 7;

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("IceReaverP");
            item.shootSpeed = 18f;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 200);
        }
        public override bool CanUseItem(Player player)
        {
            uses++;
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (uses % 2 == 0)
            {
                float rotation = MathHelper.ToRadians(8);
                if (player.direction == -1) rotation = -rotation;
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(rotation, -rotation, (float)player.itemAnimation / (float)item.useAnimation));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Reaver");
        }
    }
}
