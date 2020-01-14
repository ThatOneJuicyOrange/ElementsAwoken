using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Utilities;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.TheCelestial
{
    public class CelestialFlame : ModItem
    {
        public int shootTimer = 20;
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 26;
            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 6;
            item.expert = true;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astra's Flare");
            Tooltip.SetDefault("10% increased critical strike chance\nShoot lasers at nearby enemies");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeCrit += 10;
            player.rangedCrit += 10;
            player.thrownCrit += 10;
            player.magicCrit += 10;

            shootTimer--;
            float maxDistance = 500f;
            if (player.whoAmI == Main.myPlayer)
            {
                if (shootTimer <= 0)
                {
                    for (int l = 0; l < 200; l++)
                    {
                        NPC nPC = Main.npc[l];
                        if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Collision.CanHit(player.position, player.width, player.height, nPC.position, nPC.width, nPC.height) &&Vector2.Distance(player.Center, nPC.Center) <= maxDistance)
                        {
                            float projSpeed = 15f; //modify the speed the projectile are shot.  Lower number = slower projectile.
                            float speedX = nPC.Center.X - player.Center.X;
                            float speedY = nPC.Center.Y - player.Center.Y;
                            float num406 = (float)Math.Sqrt((double)(speedX * speedX + speedY * speedY));
                            num406 = projSpeed / num406;
                            speedX *= num406;
                            speedY *= num406;

                            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 12);
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, mod.ProjectileType("CelestialBoltFriendly"), 35, 0f, Main.myPlayer, 0f, Main.rand.Next(4));
                            shootTimer = Main.rand.Next(20,40);
                            return;
                        }
                    }
                }
            }
        }
    }
}
