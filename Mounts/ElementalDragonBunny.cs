using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Mounts
{
    public class ElementalDragonBunny : ModMountData
    {
        public float speed = 10f;
        public override void SetDefaults()
        {
            mountData.spawnDust = 15;
            mountData.buff = mod.BuffType("ElementalDragonBunnyBuff");

            mountData.heightBoost = 20;
            mountData.flightTimeMax = 0;
            mountData.fallDamage = 0.8f;
            mountData.runSpeed = 11f;
            mountData.dashSpeed = 7.5f;
            mountData.acceleration = 0.07f;
            mountData.jumpHeight = 30;
            mountData.jumpSpeed = 9f;
            mountData.totalFrames = 7;

            mountData.blockExtraJumps = false;
            mountData.totalFrames = 7;
            mountData.constantJump = true;

            int[] array = new int[mountData.totalFrames];
            for (int k = 0; k < array.Length; k++)
            {
                array[k] = 14;
            }
            array[2] += 2;
            array[3] += 4;
            array[4] += 8;
            array[5] += 8;
            mountData.playerYOffsets = array;
            mountData.xOffset = -7;
            mountData.bodyFrame = 3;
            mountData.yOffset = 4;
            mountData.playerHeadOffset = 22;

            mountData.standingFrameCount = 1;
            mountData.standingFrameDelay = 12;
            mountData.standingFrameStart = 0;

            mountData.runningFrameCount = 7;
            mountData.runningFrameDelay = 12;
            mountData.runningFrameStart = 0;

            mountData.flyingFrameCount = 6;
            mountData.flyingFrameDelay = 6;
            mountData.flyingFrameStart = 1;

            mountData.inAirFrameCount = 1;
            mountData.inAirFrameDelay = 12;
            mountData.inAirFrameStart = 5;

            mountData.idleFrameCount = 0;
            mountData.idleFrameDelay = 0;
            mountData.idleFrameStart = 0;
            if (Main.netMode != 2)
            {
                mountData.textureWidth = mountData.backTexture.Width;
                mountData.textureHeight = mountData.backTexture.Height;
                mountData.frontTexture = null;
            }
        }

        public int shootTimer = 0;
        public int type = 0;
        public override void UpdateEffects(Player player)
        {
            shootTimer--;
            float max = 400f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= max)
                {
                    float Speed = 9f;
                    float rotation = (float)Math.Atan2(player.Center.Y - nPC.Center.Y, player.Center.X - nPC.Center.X);
                    if (shootTimer <= 0)
                    {
                        Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 20);
                        Projectile.NewProjectile(player.Center.X - 4f, player.Center.Y, speed.X, speed.Y - 3.5f, mod.ProjectileType("BunnyBreath"), 25, 5f, player.whoAmI, type);
                        shootTimer = 90;
                    }
                }
            }
            if (player.controlJump && player.velocity.Y == 0f)
            {
                for (int l = 0; l < 12; l++)
                {
                    Vector2 vector3 = Vector2.UnitX * (float)(-(float)player.width) / 2f;
                    vector3 += -Vector2.UnitY.RotatedBy((double)((float)l * 3.14159274f / 6f), default(Vector2)) * new Vector2(8f, 16f);
                    vector3 = vector3.RotatedBy(1.57079637f, default(Vector2));
                    int num9 = Dust.NewDust(player.Center, 0, 0, 181, 0f, 0f, 160, default(Color), 1f);
                    Main.dust[num9].scale = 1.1f;
                    Main.dust[num9].noGravity = true;
                    Main.dust[num9].fadeIn = 1f;
                    Main.dust[num9].position = player.Center + vector3;
                    Main.dust[num9].velocity = player.velocity * 0.1f;
                    Main.dust[num9].velocity = Vector2.Normalize(player.Bottom - new Vector2(0f, 24f) - player.velocity * 3f - Main.dust[num9].position) * 1.25f;
                }
            }
        }
    }
}