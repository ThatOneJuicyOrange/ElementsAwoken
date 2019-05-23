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
            int random = Main.rand.Next(10);
            if (player.whoAmI == Main.myPlayer)
            {
                if (random == 0)
                {
                    for (int l = 0; l < 200; l++)
                    {
                        NPC nPC = Main.npc[l];
                        if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= maxDistance)
                        {
                            if (shootTimer <= 0)
                            {
                                float num396 = player.position.X;
                                float num397 = player.position.Y;
                                float num398 = 300f;
                                bool flag11 = false;
                                for (int num399 = 0; num399 < 200; num399++)
                                {
                                        float num400 = Main.npc[num399].position.X + (float)(Main.npc[num399].width / 2);
                                        float num401 = Main.npc[num399].position.Y + (float)(Main.npc[num399].height / 2);
                                        float num402 = Math.Abs(player.position.X + (float)(player.width / 2) - num400) + Math.Abs(player.position.Y + (float)(player.height / 2) - num401);
                                        if (num402 < num398 && Collision.CanHit(player.position, player.width, player.height, Main.npc[num399].position, Main.npc[num399].width, Main.npc[num399].height))
                                        {
                                            num398 = num402;
                                            num396 = num400;
                                            num397 = num401;
                                            flag11 = true;
                                        }
                                }
                                if (flag11)
                                {
                                    float num403 = 15f; //modify the speed the projectile are shot.  Lower number = slower projectile.
                                    Vector2 vector29 = new Vector2(player.position.X + (float)player.width * 0.5f, player.position.Y + (float)player.height * 0.5f);
                                    float num404 = num396 - vector29.X;
                                    float num405 = num397 - vector29.Y;
                                    float num406 = (float)Math.Sqrt((double)(num404 * num404 + num405 * num405));
                                    num406 = num403 / num406;
                                    num404 *= num406;
                                    num405 *= num406;

                                    Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 12);
                                    Projectile.NewProjectile(player.Center.X - 4f, player.Center.Y, num404, num405, mod.ProjectileType("CelestialBoltFriendly"), 35, 0f, Main.myPlayer, 0f, 0f);
                                    return;
                                }
                                shootTimer = 20;
                            }
                        }
                    }
                }
            }
        }
    }
}
