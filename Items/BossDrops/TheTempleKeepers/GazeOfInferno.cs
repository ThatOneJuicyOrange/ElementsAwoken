using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.TheTempleKeepers
{
    public class GazeOfInferno : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 96;
            item.knockBack = 2;
            item.mana = 6;

            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            Item.staff[item.type] = true;

            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 5;
            item.UseSound = SoundID.Item8;

            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 10;

            item.shoot = mod.ProjectileType("InfernoEye");
            item.shootSpeed = 18f;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("InfernoEye")] != 0)
            {
                return false;
            }
            return true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gaze of Inferno");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(player.Center.X, player.Center.Y - 70, 0f, 0f, mod.ProjectileType("InfernoEye"), damage, knockBack, player.whoAmI, Main.MouseWorld.X, Main.MouseWorld.Y);
            return false;
        }
    }
}
 