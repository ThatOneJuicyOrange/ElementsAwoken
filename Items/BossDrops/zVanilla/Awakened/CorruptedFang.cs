using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Projectiles;

namespace ElementsAwoken.Items.BossDrops.zVanilla.Awakened
{
    public class CorruptedFang : ModItem
    {
        private int timer = 0;
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;

            item.rare = 1;
            item.value = Item.sellPrice(0, 5, 0, 0);

            item.accessory = true;
            item.GetGlobalItem<EARarity>().awakened = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corrupted Fang");
            Tooltip.SetDefault("The player spawns corrupted fangs that shoot at nearby enemies\nWhen the fangs hit, the player gets 5% increased damage and attacks inflict posion and venom");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            timer--;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            float maxDistance = 500f; 
            if (timer <= 0)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    for (int l = 0; l < Main.maxNPCs; l++)
                    {
                        NPC nPC = Main.npc[l];
                        if (nPC.CanBeChasedBy(player) && Vector2.Distance(player.Center, nPC.Center) <= maxDistance && Collision.CanHit(player.Center, 2, 2, nPC.Center, 2, 2))
                        {
                            Main.PlaySound(SoundID.Item20, (int)player.position.X, (int)player.position.Y);
                            int numberProjectiles = 3;
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                int dist = 30;
                                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                                Vector2 spawnPos = player.Center + new Vector2((float)Math.Sin(angle) * dist, (float)Math.Cos(angle) * dist);
                                float Speed = 12f;
                                float rotation = (float)Math.Atan2(spawnPos.Y - nPC.Center.Y, spawnPos.X - nPC.Center.X);

                                Projectile.NewProjectile(spawnPos.X, spawnPos.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ModContent.ProjectileType<CorruptedFangP>(), 30, 0f, Main.myPlayer);

                                int numDusts = 16;
                                for (int d = 0; d < numDusts; d++)
                                {
                                    Vector2 position = Vector2.One.RotatedBy((double)((float)(d - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + spawnPos;
                                    Vector2 velocity = position - spawnPos;
                                    int dust = Dust.NewDust(position + velocity, 0, 0, 46, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                                    Main.dust[dust].noGravity = true;
                                    Main.dust[dust].noLight = true;
                                    Main.dust[dust].velocity = Vector2.Normalize(velocity) * 1.5f;
                                }
                            }

                            timer = Main.rand.Next(60,120);
                            return;
                        }
                    }
                }
            }
        }
    }
}
