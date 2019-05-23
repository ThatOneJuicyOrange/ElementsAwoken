using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.TheTempleKeepers
{
    public class WyrmClaw : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;

            item.damage = 87;
            item.knockBack = 7f;

            item.autoReuse = true;
            item.useTurn = true;

            item.useStyle = 1;
            item.useAnimation = 22;
            item.useTime = 22;

            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 10;

            item.UseSound = SoundID.Item71;
            item.melee = true;

            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("WyrmClawSlash");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wyrm Claw");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            //tsunami code from 'Player'
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float pi = 0.314159274f;
            int numProjectiles = 3;
            Vector2 vector14 = new Vector2(speedX, speedY);
            vector14.Normalize();
            vector14 *= 40f;
            bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector14, 0, 0);
            for (int num123 = 0; num123 < numProjectiles; num123++)
            {
                float num124 = (float)num123 - ((float)numProjectiles - 1f) / 2f;
                Vector2 vector15 = vector14.RotatedBy((double)(pi * num124), default(Vector2));
                if (!flag11)
                {
                    vector15 -= vector14;
                }
                Projectile.NewProjectile(vector2.X + vector15.X, vector2.Y + vector15.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Fire);
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
    }
}
