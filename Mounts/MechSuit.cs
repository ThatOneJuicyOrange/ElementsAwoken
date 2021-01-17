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
    public class MechSuit : ModMountData
    {
        public int hover = 120;
        public bool stateHover = false;
        public override void SetDefaults()
        {
            mountData.spawnDust = 239;
           // mountData.buff = mod.BuffType("CrowBuff");

            mountData.heightBoost = 70;
            mountData.fallDamage = 0f;
            mountData.runSpeed = 7f;
            mountData.dashSpeed = 7f;
            mountData.fatigueMax = 800;
            mountData.acceleration = 0.2f;

            mountData.jumpHeight = 10;
            mountData.jumpSpeed = 12f;

            mountData.blockExtraJumps = false;
            mountData.totalFrames = 1;
            mountData.constantJump = false;
            int[] array = new int[mountData.totalFrames];
            for (int l = 0; l < array.Length; l++)
            {
                array[l] = 50;
            }
            mountData.playerYOffsets = array;

            mountData.xOffset = 0;
            mountData.bodyFrame = 0;
            mountData.yOffset = 0;
            mountData.playerHeadOffset = 22;
            mountData.playerYOffsets = array;

            mountData.standingFrameCount = 1;
            mountData.standingFrameDelay = 12;
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
                mountData.backTexture = mod.GetTexture("Mounts/MechSuit");
                mountData.textureWidth = mountData.backTexture.Width;
                mountData.textureHeight = mountData.backTexture.Height;
                mountData.frontTexture = mod.GetTexture("Mounts/MechSuit");
            }
        }
        public override void UpdateEffects(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.inMech = true;
            player.lavaImmune = true;
            if (player.jump == 10 && !player.wet && !player.lavaWet && !player.honeyWet)
            {
                for (int k = 0; k < 15; k++)
                {
                    Dust dust = Main.dust[Dust.NewDust(player.Bottom + new Vector2(-20, 0), 8, 8, 6)];
                    dust.velocity.Y = Main.rand.NextFloat(1, 3);
                    dust = Main.dust[Dust.NewDust(player.Bottom + new Vector2(20, 0), 8, 8, 6)];
                    dust.velocity.Y = Main.rand.NextFloat(1, 3);
                }
            }
            if (player.velocity.Y != 0 && !player.controlJump) stateHover = true;
            if (player.controlJump && stateHover && hover >= 0)
            {
                hover--;
                player.velocity.Y -= 0.43f;
                if (player.velocity.Y > 0) player.velocity.Y *= 0.9f;
                Dust dust = Main.dust[Dust.NewDust(player.Bottom + new Vector2(-20, 0), 8, 8, 6)];
                dust.velocity.Y = Main.rand.NextFloat(1, 3);
                dust = Main.dust[Dust.NewDust(player.Bottom + new Vector2(20, 0), 8, 8, 6)];
                dust.velocity.Y = Main.rand.NextFloat(1, 3);
            }
            if (player.velocity.Y == 0)
            {
                stateHover = false;
                   hover = 120;
            }
        }
    }
}