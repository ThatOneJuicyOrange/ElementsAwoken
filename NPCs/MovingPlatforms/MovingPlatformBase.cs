using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.MovingPlatforms
{
    public abstract class MovingPlatformBase : ModNPC
    {
        public override void SetDefaults()
        {
            SetPlatformDefaults();
            npc.aiStyle = -1;

            npc.lifeMax = 1;

            npc.knockBackResist = 0f;

            npc.GetGlobalNPC<NPCs.VolcanicPlateau.PlateauNPCs>().tomeClickable = false;
            npc.lavaImmune = true;
            npc.dontTakeDamage = true;
            npc.immortal = true;
            npc.friendly = true;
            npc.noGravity = true;
        }

        public virtual void SetPlatformDefaults()
        {
        }

        public virtual void PlatformAI(bool hasPlayerOn)
        {
        }

        public override void AI()
        {
            bool playerOn = false;
            Player player = Main.LocalPlayer;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            Rectangle playerRect = new Rectangle((int)player.position.X, (int)player.position.Y + (player.height - 2), player.width, (int)(4 * MathHelper.Clamp(player.velocity.Y / 5,0, 10)));
            Rectangle npcRect = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, 4);

            if (playerRect.Intersects(npcRect) && player.position.Y <= npc.position.Y)
            {
                playerOn = true;
                float value = 1f;
                if (npc.wet || npc.lavaWet) value = 0.5f;
                if (npc.honeyWet) value = 0.25f;
                player.position += npc.velocity * value;
                if (player.controlDown || player.GoingDownWithGrapple) return;
                if (!player.justJumped && player.velocity.Y >= 0)
                {
                    player.gfxOffY = npc.gfxOffY;
                    player.velocity.Y = 0;
                    player.fallStart = (int)(player.position.Y / 16f);
                    player.position.Y = npc.position.Y - player.height + 4;
                }
                if (player.bodyFrame.Y == player.bodyFrame.Height * 5)
                {
                    if (player.velocity.X != 0)
                    {
                        modPlayer.platformWalkCounter += (double)(Math.Abs(player.velocity.X) * 0.5f);
                        while (modPlayer.platformWalkCounter > 8.0)
                        {
                            modPlayer.platformWalkCounter -= 8.0;
                            modPlayer.platformWalkFrame = modPlayer.platformWalkFrame + player.bodyFrame.Height;
                        }
                        if (modPlayer.platformWalkFrame < player.bodyFrame.Height * 7)
                        {
                            modPlayer.platformWalkFrame = player.bodyFrame.Height * 19;
                            return;
                        }
                        if (modPlayer.platformWalkFrame > player.bodyFrame.Height * 19)
                        {
                            modPlayer.platformWalkFrame = player.bodyFrame.Height * 7;
                            return;
                        }
                        player.bodyFrame.Y = modPlayer.platformWalkFrame;
                        player.legFrame.Y = modPlayer.platformWalkFrame;
                    }
                    else
                    {
                        player.bodyFrame.Y = 0;
                        player.legFrame.Y = 0;
                    }
                }
            }
            PlatformAI(playerOn);
        }
    }
}