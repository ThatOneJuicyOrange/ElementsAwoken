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
    public class CrowMount : ModMountData
    {
        public override void SetDefaults()
        {
            mountData.spawnDust = 239;
            mountData.buff = mod.BuffType("CrowBuff");

            mountData.heightBoost = 22;
            mountData.fallDamage = 0f;
            mountData.runSpeed = 7f;
            mountData.dashSpeed = 7f;
            mountData.usesHover = true;
            mountData.flightTimeMax = 900;
            mountData.fatigueMax = 800;
            mountData.acceleration = 0.2f;
            mountData.jumpHeight = 10;
            mountData.jumpSpeed = 4f;

            mountData.blockExtraJumps = false;
            mountData.totalFrames = 14;
            mountData.constantJump = true;
            int[] array = new int[mountData.totalFrames];
            for (int l = 0; l < array.Length; l++)
            {
                array[l] = 30;
            }
            array[2] -= 2;
            array[3] -= 2;
            mountData.playerYOffsets = array;

            mountData.xOffset = 0;
            mountData.bodyFrame = 3;
            mountData.yOffset = 0;
            mountData.playerHeadOffset = 22;

            mountData.standingFrameCount = 1;
            mountData.standingFrameDelay = 12;
            mountData.standingFrameStart = 5;

            mountData.runningFrameCount = 6;
            mountData.runningFrameDelay = 18;
            mountData.runningFrameStart = 0;

            mountData.flyingFrameCount = 8;
            mountData.flyingFrameDelay = 3;
            mountData.flyingFrameStart = 6;

            mountData.inAirFrameCount = 8;
            mountData.inAirFrameDelay = 10;
            mountData.inAirFrameStart = 6;

            mountData.idleFrameCount = 1;
            mountData.idleFrameDelay = 12;
            mountData.idleFrameStart = 5;
            mountData.idleFrameLoop = false;

            mountData.swimFrameCount = 0;
            mountData.swimFrameDelay = 0;
            mountData.swimFrameStart = 0;
            if (Main.netMode != 2)
            {
                mountData.textureWidth = mountData.backTexture.Width;
                mountData.textureHeight = mountData.backTexture.Height;
                mountData.frontTexture = null;
            }
        }
    }
}