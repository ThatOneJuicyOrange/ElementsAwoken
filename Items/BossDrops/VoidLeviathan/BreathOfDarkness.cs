using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.VoidLeviathan
{
    public class BreathOfDarkness : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 28;

            item.damage = 120;
            item.knockBack = 3;
            item.mana = 10;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;
            item.UseSound = SoundID.Item44;

            item.summon = true;
            item.noMelee = true;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 25, 0, 0);

            item.shoot = mod.ProjectileType("VleviHead");
            item.shootSpeed = 7f; 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Breath of Darkness");
            Tooltip.SetDefault("Summons a miniature Void Leviathan");
        }
        public override bool CanUseItem(Player player)
        {
            float minions = 0;

            for (int j = 0; j < Main.projectile.Length; j++)
            {
                if (Main.projectile[j].active && Main.projectile[j].owner == player.whoAmI && Main.projectile[j].minion)
                {
                    minions += Main.projectile[j].minionSlots;
                }
            }
            if (minions >= player.maxMinions)
            {
                return false;
            }
            return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float velocityX = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
            float velocityY = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;

            int head = -1;
            int tail = -1;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer)
                {
                    if (head == -1 && Main.projectile[i].type == mod.ProjectileType("VleviHead"))
                    {
                        head = i;
                    }
                    if (tail == -1 && Main.projectile[i].type == mod.ProjectileType("VleviTail"))
                    {
                        tail = i;
                    }
                    if (head != -1 && tail != -1)
                    {
                        break;
                    }
                }
            }
            if (head == -1 && tail == -1)
            {
                velocityX = 0f;
                velocityY = 0f;
                vector2.X = (float)Main.mouseX + Main.screenPosition.X;
                vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;

                int current = Projectile.NewProjectile(vector2.X, vector2.Y, velocityX, velocityX, mod.ProjectileType("VleviHead"), damage, knockBack, Main.myPlayer);

                int previous = current;
                current = Projectile.NewProjectile(vector2.X, vector2.Y, velocityX, velocityX, mod.ProjectileType("VleviBody"), damage, knockBack, Main.myPlayer, (float)previous);

                previous = current;
                current = Projectile.NewProjectile(vector2.X, vector2.Y, velocityX, velocityX, mod.ProjectileType("VleviTail"), damage, knockBack, Main.myPlayer, (float)previous);
                Main.projectile[previous].localAI[1] = (float)current;
                Main.projectile[previous].netUpdate = true;
            }
            else if (head != -1 && tail != -1)
            {
                int body = Projectile.NewProjectile(vector2.X, vector2.Y, velocityX, velocityY, mod.ProjectileType("VleviBody"), damage, knockBack, Main.myPlayer, Main.projectile[tail].ai[0]);

                Main.projectile[body].localAI[1] = (float)tail;
                Main.projectile[body].ai[1] = 1f;
                Main.projectile[body].netUpdate = true;


                Main.projectile[tail].ai[0] = (float)body;
                Main.projectile[tail].netUpdate = true;
                Main.projectile[tail].ai[1] = 1f;
            }
            return false;
        }
    }
}
