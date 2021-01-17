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
    public class WispForm : ModMountData
    {
        public override void SetDefaults()
        {
            //mountData.spawnDust = 239;
           // mountData.buff = mod.BuffType("CrowBuff");

            mountData.heightBoost = -32;
            mountData.fallDamage = 0f;
            mountData.runSpeed = 7f;
            mountData.dashSpeed = 7f;
            mountData.usesHover = true;
            mountData.flightTimeMax = Int32.MaxValue;
            mountData.fatigueMax = Int32.MaxValue;
            mountData.acceleration = 0.2f;
            mountData.jumpHeight = 0;
            mountData.jumpSpeed = 0;

            mountData.blockExtraJumps = false;
            mountData.totalFrames = 1;
            mountData.constantJump = true;
            int[] array = new int[mountData.totalFrames];
            for (int l = 0; l < array.Length; l++)
            {
                array[l] = 30;
            }
            mountData.playerYOffsets = array;

            mountData.xOffset = 0;
            mountData.bodyFrame = 0;
            mountData.yOffset = 0;
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
                mountData.frontTexture = null;
            }
        }
        public override void UpdateEffects(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.wispForm = true;
            player.width = 10;
        }
    }
}