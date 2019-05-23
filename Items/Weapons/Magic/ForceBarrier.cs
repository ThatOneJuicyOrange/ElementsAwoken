using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Magic
{
    public class ForceBarrier : ModItem
    {

        public override void SetDefaults()
        {
            item.damage = 100;
            item.magic = true;
            item.mana = 18;
            item.width = 54;
            item.height = 52;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.knockBack = 12f;
            item.value = Item.buyPrice(0, 15, 0, 0);
            item.UseSound = SoundID.Item113;
            item.autoReuse = true;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("Barrier");
            item.shootSpeed = 9f;
            item.rare = 5;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force Barrier");
        }
        public override bool CanUseItem(Player player)
        {
            if (player.FindBuffIndex(mod.BuffType("BarrierCooldown")) == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int i = Main.myPlayer;
            float num72 = item.shootSpeed;
            float num74 = knockBack;
            num74 = player.GetWeaponKnockback(item, num74);
            player.itemTime = item.useTime;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            vector2.X = (float)Main.mouseX + Main.screenPosition.X;
            vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;
            int numberProjectiles = 3;
            for (int num131 = 0; num131 < numberProjectiles; num131++)
            {
                Projectile.NewProjectile(vector2.X, vector2.Y, 0, 0, mod.ProjectileType("Barrier"), damage, num74, i, 0f, 0f);
            }
            player.AddBuff(mod.BuffType("BarrierCooldown"), 380);
            return false;
        }
    }
}
