using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Testing
{
    public class WormStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 28;

            item.damage = 52;
            item.knockBack = 3;
            item.mana = 10;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;
            item.UseSound = SoundID.Item44;

            item.summon = true;
            item.noMelee = true;

            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 6;

            item.shoot = mod.ProjectileType("EyeballMinion");
            item.shootSpeed = 7f; 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Worm Staff");
            Tooltip.SetDefault("worm mod");
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
                    if (head == -1 && Main.projectile[i].type == mod.ProjectileType("WormTestHead"))
                    {
                        head = i;
                    }
                    if (tail == -1 && Main.projectile[i].type == mod.ProjectileType("WormTestTail"))
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

                int current = Projectile.NewProjectile(vector2.X, vector2.Y, velocityX, velocityX, mod.ProjectileType("WormTestHead"), damage, knockBack, Main.myPlayer);

                int previous = current;
                current = Projectile.NewProjectile(vector2.X, vector2.Y, velocityX, velocityX, mod.ProjectileType("WormTestBody"), damage, knockBack, Main.myPlayer, (float)previous);

                previous = current;
                current = Projectile.NewProjectile(vector2.X, vector2.Y, velocityX, velocityX, mod.ProjectileType("WormTestTail"), damage, knockBack, Main.myPlayer, (float)previous);
                Main.projectile[previous].localAI[1] = (float)current;
                Main.projectile[previous].netUpdate = true;
            }
            else if (head != -1 && tail != -1)
            {
                int body = Projectile.NewProjectile(vector2.X, vector2.Y, velocityX, velocityY, mod.ProjectileType("WormTestBody"), damage, knockBack, Main.myPlayer, Main.projectile[tail].ai[0]);

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
