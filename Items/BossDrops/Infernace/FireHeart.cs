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

namespace ElementsAwoken.Items.BossDrops.Infernace
{
    public class FireHeart : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 26;
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.expert = true;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Furious Soul");
            Tooltip.SetDefault("Ignites nearby enemies\nBlows up nearby lesser enemies that are on low health\nLights up the area");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0.7f, 0.2f, 0.2f);

            if (Main.rand.NextBool()) Dust.NewDust(player.position, player.width, player.height, 6, 0, 0, 0, default(Color));

            float range = 500f;
            for (int l = 0; l < 200; l++)
            {
                NPC nPC = Main.npc[l];
                if (!nPC.CanBeChasedBy(this) || Vector2.Distance(player.Center, nPC.Center) > range) continue;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (Main.rand.Next(120) == 0)
                    {
                        if (nPC.life <= nPC.lifeMax * 0.3f && nPC.life <= 30)
                        {
                            Explosion(player, nPC.Center);
                        }
                    }
                    if (Main.rand.Next(90) == 0)
                    {
                        if (!nPC.wet)
                        {
                            nPC.AddBuff(BuffID.OnFire, 180, false);
                        }
                    }
                }
            }
        }
        private void Explosion(Player player, Vector2 target)
        {
            Projectile exp = Main.projectile[Projectile.NewProjectile(target.X, target.Y, 0f, 0f, mod.ProjectileType("Explosion"), 50, 5f, player.whoAmI, 0f, 0f)];
            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 14);
            int num = ModContent.GetInstance<Config>().lowDust ? 10 : 20;
            for (int i = 0; i < num; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 31, 0f, 0f, 100, default(Color), 1.5f)];
                dust.velocity *= 1.4f;
            }
            int num2 = ModContent.GetInstance<Config>().lowDust ? 5 : 10;
            for (int i = 0; i < num2; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 6, 0f, 0f, 100, default(Color), 2.5f)];
                dust.noGravity = true;
                dust.velocity *= 5f;
                dust = Main.dust[Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 6, 0f, 0f, 100, default(Color), 1.5f)];
                dust.velocity *= 3f;
            }
            int num373 = Gore.NewGore(new Vector2(player.position.X, player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore85 = Main.gore[num373];
            gore85.velocity.X = gore85.velocity.X + 1f;
            Gore gore86 = Main.gore[num373];
            gore86.velocity.Y = gore86.velocity.Y + 1f;
            num373 = Gore.NewGore(new Vector2(player.position.X, player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore87 = Main.gore[num373];
            gore87.velocity.X = gore87.velocity.X - 1f;
            Gore gore88 = Main.gore[num373];
            gore88.velocity.Y = gore88.velocity.Y + 1f;
            num373 = Gore.NewGore(new Vector2(player.position.X, player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore89 = Main.gore[num373];
            gore89.velocity.X = gore89.velocity.X + 1f;
            Gore gore90 = Main.gore[num373];
            gore90.velocity.Y = gore90.velocity.Y - 1f;
            num373 = Gore.NewGore(new Vector2(player.position.X, player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore91 = Main.gore[num373];
            gore91.velocity.X = gore91.velocity.X - 1f;
            Gore gore92 = Main.gore[num373];
            gore92.velocity.Y = gore92.velocity.Y - 1f;
        }
    }
}
