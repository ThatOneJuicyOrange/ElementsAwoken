using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.VoidLeviathan
{
    public class ExtinctionBow : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 64;

            item.knockBack = 5;
            item.damage = 300;

            item.useTime = 14;
            item.useAnimation = 14;
            item.useStyle = 5;
            item.UseSound = SoundID.Item5;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 25, 0, 0);

            item.shoot = 10;
            item.shootSpeed = 30f;
            item.useAmmo = 40;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Extinction Bow");
            Tooltip.SetDefault("Turns normal arrows into extinction arrows\nRight click to create a void portal that follows the cursor\nPortals can only be created every 30 seconds");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.altFunctionUse != 2)
            {
                if (type == ProjectileID.WoodenArrowFriendly)
                {
                    type = mod.ProjectileType("ExtinctionArrow");
                }
                int numProj = 3;
                for (int i = 0; i < numProj; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
            }
            else
            {
                Projectile.NewProjectile(position.X, position.Y - 100, 0f, 0f, mod.ProjectileType("VoidPortal"), damage, knockBack, player.whoAmI);
                modPlayer.voidPortalCooldown = 1800;
            }
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.altFunctionUse == 2)
            {
                if (modPlayer.voidPortalCooldown > 0)
                {
                    return false;
                }
            }
            return true;
        }
        public override void HoldItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.voidPortalCooldown <= 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    int num5 = Dust.NewDust(player.position, player.width, player.height, DustID.PinkFlame, 0f, 0f, 200, default(Color), 0.5f);
                    Main.dust[num5].noGravity = true;
                    Main.dust[num5].velocity *= 0.75f;
                    Main.dust[num5].fadeIn = 1.3f;
                    Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    vector.Normalize();
                    vector *= (float)Main.rand.Next(50, 100) * 0.04f;
                    Main.dust[num5].velocity = vector;
                    vector.Normalize();
                    vector *= 34f;
                    Main.dust[num5].position = player.Center - vector;
                }
            }
        }
    }
}
