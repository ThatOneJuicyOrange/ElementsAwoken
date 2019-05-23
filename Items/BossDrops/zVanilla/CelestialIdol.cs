using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.zVanilla
{
    class CelestialIdol : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 46;
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 10;
            item.accessory = true;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Idol");
            Tooltip.SetDefault("10 defense\nAncient stars rain down from the sky on hit\nMagic damage increased by 20%\nInflicts Ancient Decay on hit");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 24));
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.immune)
            {
                if (Main.rand.Next(12) == 0)
                {
                    for (int l = 0; l < 1; l++)
                    {
                        float x = player.position.X + (float)Main.rand.Next(-400, 400);
                        float y = player.position.Y - (float)Main.rand.Next(500, 800);
                        Vector2 vector = new Vector2(x, y);
                        float num15 = player.position.X + (float)(player.width / 2) - vector.X;
                        float num16 = player.position.Y + (float)(player.height / 2) - vector.Y;
                        num15 += (float)Main.rand.Next(-100, 101);
                        int num17 = 22;
                        float num18 = (float)Math.Sqrt((double)(num15 * num15 + num16 * num16));
                        num18 = (float)num17 / num18;
                        num15 *= num18;
                        num16 *= num18;
                        int num19 = Projectile.NewProjectile(x, y, num15, num16, mod.ProjectileType("AncientStar"), 30, 5f, player.whoAmI, 0f, 0f);
                        Main.projectile[num19].ai[1] = player.position.Y;
                    }
                }
            }
            player.magicDamage *= 1.2f;
            player.GetModPlayer<MyPlayer>(mod).ancientDecayWeapon = true;
            player.statDefense += 10;
        }
    }
}
