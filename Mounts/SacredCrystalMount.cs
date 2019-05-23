using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Mounts
{
    public class SacredCrystalMount : ModMountData
    {
        public float speed = 10f;
        public override void SetDefaults()
        {
            mountData.spawnDust = 239;
            mountData.buff = mod.BuffType("SacredCrystalBuff");

            mountData.heightBoost = 0;
            mountData.fallDamage = 0f;
            mountData.runSpeed = speed;
            mountData.dashSpeed = speed;
            mountData.swimSpeed = speed;
            mountData.usesHover = true;
            mountData.flightTimeMax = 450;
            mountData.fatigueMax = 0;
            mountData.jumpHeight = 5;
            mountData.acceleration = 0.19f;
            mountData.jumpSpeed = 4f;

            mountData.blockExtraJumps = false;
            mountData.totalFrames = 4;
            mountData.constantJump = true;
            int[] array = new int[mountData.totalFrames];
            for (int l = 0; l < array.Length; l++)
            {
                array[l] = 20;
            }
            mountData.playerYOffsets = new int[] { 0 };

            mountData.xOffset = 0;
            mountData.bodyFrame = 3;
            mountData.yOffset = 4;

            mountData.playerHeadOffset = 0;
            mountData.standingFrameCount = 0;
            mountData.standingFrameDelay = 0;
            mountData.standingFrameStart = 0;
            mountData.runningFrameCount = 0;
            mountData.runningFrameDelay = 0;
            mountData.runningFrameStart = 0;
            mountData.flyingFrameCount = 0;
            mountData.flyingFrameDelay = 0;
            mountData.flyingFrameStart = 0;
            mountData.inAirFrameCount = 0;
            mountData.inAirFrameDelay = 0;
            mountData.inAirFrameStart = 0;
            mountData.idleFrameCount = 0;
            mountData.idleFrameDelay = 0;
            mountData.idleFrameStart = 0;
            mountData.idleFrameLoop = false;
            mountData.swimFrameCount = 0;
            mountData.swimFrameDelay = 0;
            mountData.swimFrameStart = 0;
            if (Main.netMode != 2)
            {
                mountData.textureWidth = mountData.backTexture.Width;
                mountData.textureHeight = mountData.backTexture.Height;
            }
        }
        public int shootTimer = 0;
        public int shootCooldown = 0;
        public int type = 0;
        public override void UpdateEffects(Player player)
        {
            shootTimer--;
            shootCooldown--;
            if (shootCooldown <= 0)
            {
                shootCooldown = 80;
                type = Main.rand.Next(4);
            }
            float max = 400f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= max)
                {
                    float Speed = 9f;
                    float rotation = (float)Math.Atan2(player.Center.Y - nPC.Center.Y, player.Center.X - nPC.Center.X);
                    if (shootTimer <= 0 && shootCooldown <= 24)
                    {
                        Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 12);
                        Projectile.NewProjectile(player.Center.X - 4f, player.Center.Y, speed.X, speed.Y, mod.ProjectileType("SacredCrystalLaser"), 30, 5f, player.whoAmI, type);
                        shootTimer = 6;
                    }
                }
            }
        }
    }
}