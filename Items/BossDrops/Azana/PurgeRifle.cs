using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace ElementsAwoken.Items.BossDrops.Azana
{
    public class PurgeRifle : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 18;

            item.damage = 550;
            item.knockBack = 4;

            item.useTime = 12;
            item.useAnimation = 14;
            item.useStyle = 5;
            item.crit = 25;

            item.autoReuse = true;
            item.noMelee = true;
            item.ranged = true;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;
            item.value = Item.sellPrice(0, 35, 0, 0);

            item.shoot = 10;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item33;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaotron Accelerator");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("PurgeBeam"), item.damage, 0, player.whoAmI, 0f, 0f);
            return false;
        }
    }
}
