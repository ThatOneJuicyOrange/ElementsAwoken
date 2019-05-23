using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.TheTempleKeepers
{
    public class TheAllSeer : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 70;
            item.height = 70;

            item.damage = 80;
            item.knockBack = 5;

            item.melee = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 10;

            item.shoot = mod.ProjectileType("FireRunes");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The All Seer");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 4;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(1f, 1f).RotatedByRandom(MathHelper.ToRadians(360));
                Projectile.NewProjectile(player.Center.X + Main.rand.Next(-300, 300), player.Center.Y + Main.rand.Next(-300, 300), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("FireRunes"), damage, knockBack, player.whoAmI, 0f, 0f);
            }
            return false;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 200);
        }
    }
}
