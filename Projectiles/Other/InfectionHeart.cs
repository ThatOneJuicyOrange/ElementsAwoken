using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Other
{
    public class InfectionHeart : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 48;

            projectile.friendly = true;
            projectile.tileCollide = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 1200;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infection Heart");
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            projectile.velocity *= 0.5f;
            return false;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 1f, 0.3f, 0.4f);

            projectile.velocity *= 0.95f;

            int maxConvert = Main.expertMode ? MyWorld.awakenedMode ? 75 : 65 : 60;
            int maxDist = 300;
            for (int k = 0; k < Main.maxItems; k++)
            {
                Item other = Main.item[k];
                if (other.Name.Contains("Ore") && other.type != mod.ItemType("DiscordantOre") && other.maxStack > 1 && other.active && Vector2.Distance(other.Center,projectile.Center) < maxDist)
                {
                    if (other.stack + projectile.ai[0] >= maxConvert)
                    {
                        int diff = other.stack - (int)projectile.ai[0];
                        if (diff > maxConvert) diff = maxConvert;
                        other.stack -= diff;
                        int item = Item.NewItem(other.Center, mod.ItemType("DiscordantOre"), diff);
                        if (Main.netMode == 1 && item >= 0)
                        {
                            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
                        }
                        projectile.ai[0] = maxConvert;
                    }
                    else
                    {
                        int stack = other.stack;
                        other.SetDefaults(mod.ItemType("DiscordantOre"));
                        other.stack = stack;
                        projectile.ai[0] += other.stack;
                    }
                    for (int p = 0; p < 10; p++)
                    {
                        Dust d = Main.dust[Dust.NewDust(projectile.Center + (other.Center - projectile.Center) * Main.rand.NextFloat() - new Vector2(4, 4), 0, 0, 127)];
                        d.noGravity = true;
                        d.velocity *= 0.04f;
                        d.scale *= 0.8f;
                    }
                }
            }
            if (projectile.ai[0] >= maxConvert)
            {
                projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item95, projectile.Center);
            {
                int numDusts = 50;
                for (int i = 0; i < numDusts; i++)
                {
                    Vector2 position = (Vector2.One * new Vector2((float)projectile.width / 2f, (float)projectile.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + projectile.Center;
                    Vector2 velocity = position - projectile.Center;
                    int dust = Dust.NewDust(position + velocity, 0, 0, 127, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.8f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                    Main.dust[dust].velocity = Vector2.Normalize(velocity) * 6f;
                }
            }
            {
                int numDusts = 20;
                for (int i = 0; i < numDusts; i++)
                {
                    Vector2 position = (Vector2.One * new Vector2((float)projectile.width / 2f, (float)projectile.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + projectile.Center;
                    Vector2 velocity = position - projectile.Center;
                    int dust = Dust.NewDust(position + velocity, 0, 0, 127, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                    Main.dust[dust].velocity = Vector2.Normalize(velocity) * 3f;
                }
            }
            if (projectile.ai[0] == 0 && projectile.owner == Main.myPlayer)
            {
                int item = Item.NewItem(projectile.Center, mod.ItemType("InfectionHeart"));
                if (Main.netMode == 1 && item >= 0)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
                }
            }
        }
    }
}