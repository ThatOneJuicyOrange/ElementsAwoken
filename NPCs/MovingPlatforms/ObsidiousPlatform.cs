using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.NPCProj;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using ElementsAwoken.NPCs.Bosses.Obsidious;
using ElementsAwoken.Projectiles.NPCProj.Obsidious;

namespace ElementsAwoken.NPCs.MovingPlatforms
{
    public class ObsidiousPlatform : ModNPC
    {
        public Vector2 arenaMiddle = Vector2.Zero;
        public override void SetDefaults()
        {
            npc.width = 112;
            npc.height = 22;
            
            npc.aiStyle = -1;

            npc.lifeMax = 1;
            npc.alpha = 255;

            npc.knockBackResist = 0f;

            npc.GetGlobalNPC<VolcanicPlateau.PlateauNPCs>().tomeClickable = false;
            npc.lavaImmune = true;
            npc.dontTakeDamage = true;
            npc.immortal = true;
            npc.friendly = true;
            npc.noGravity = true;
            npc.gfxOffY = -4;

            npc.GivenName = " ";
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidious Platform");
            Main.npcFrameCount[npc.type] = 3;
        }
        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[3] == 1) npc.frame.Y = 0;
            else if (npc.ai[3] == 2) npc.frame.Y = frameHeight;
            else if (npc.ai[3] == 3) npc.frame.Y = frameHeight * 2;
        }
        public override void AI()
        {
            if (npc.alpha == 0) npc.GetGlobalNPC<NPCsGLOBAL>().platformNPC = true;
            npc.ai[0] += 0.33f;
            int distance = 400;
            double rad = npc.ai[0] * (Math.PI / 180);
            double rad2 = (npc.ai[0] + 0.33f) * (Math.PI / 180);
            npc.Center = arenaMiddle - new Vector2((int)(Math.Cos(rad) * distance), (int)(Math.Sin(rad) * distance));
            npc.velocity =  (arenaMiddle - new Vector2((int)(Math.Cos(rad2) * distance), (int)(Math.Sin(rad2) * distance))) - npc.Center; // so the player moves with the 

            if (!NPC.AnyNPCs(NPCType<Obsidious>())) npc.ai[3] = 5;
            if (npc.ai[3] == 0) // spawn
            {
                if (npc.alpha > 0) npc.alpha -= 10;
                else
                {
                    npc.alpha = 0;
                    npc.ai[3] = 1;
                }
            }
            else if (npc.ai[3] == 1) // inactive
            {
                if (npc.alpha < 120) npc.alpha += 5;
                else npc.alpha = 120;
            }
            else if (npc.ai[3] == 2) // fire 
            {
                if (npc.alpha > 0)
                {
                    if (npc.alpha <= 10)
                    {
                        for (int p = 0; p < 20; p++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(npc.Center, 0, 0, 6, 0, 0, 0, default(Color), 1f)];
                            dust.velocity.X = Main.rand.NextBool() ? -4 : 4;
                            dust.velocity.X *= Main.rand.NextFloat(0.3f, 1);
                            dust.velocity.Y *= 0.1f;
                            dust.noGravity = true;
                        }
                    }
                    npc.alpha -= 10;
                }
                else npc.alpha = 0;
            }
            else if (npc.ai[3] == 3) // void
            {
                if (npc.alpha > 0) npc.alpha -= 10;
                else npc.alpha = 0;

                npc.ai[1]++;
                if (npc.ai[1] > 240)
                {
                    bool playerOn = false;
                    for (int p = 0; p < Main.maxPlayers; p++)
                    {
                        Player standPlayer = Main.player[p];
                        if (standPlayer.getRect().Intersects(npc.getRect()))
                        {
                            playerOn = true;
                            standPlayer.velocity.X = Main.rand.NextBool() ? 10 : -10;
                            standPlayer.velocity.Y = -3f;
                            standPlayer.Hurt(PlayerDeathReason.ByCustomReason(standPlayer.name + " got yeeted"), Main.expertMode ? MyWorld.awakenedMode ? 200 : 150 : 50, 0);
                        }
                    }
                    if (playerOn)
                    {
                       
                    }
                    else
                    {
                        float speed = Main.expertMode ? MyWorld.awakenedMode ? 15 : 12 : 9;
                        float rotation = (float)Math.Atan2(npc.Center.Y - Main.LocalPlayer.Center.Y, npc.Center.X - Main.LocalPlayer.Center.X);
                        Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), ProjectileType<ObsidiousPlatformOrb>(), 60, 5, Main.myPlayer)];
                    }
                    npc.ai[3] = 4;
                    npc.ai[1] = 0;
                    Gore.NewGore(npc.Center, new Vector2(-2, -1), mod.GetGoreSlot("Gores/ObsidiousPlatformGore0"), npc.scale);
                    Gore.NewGore(npc.Center, new Vector2(2, -1), mod.GetGoreSlot("Gores/ObsidiousPlatformGore0"), npc.scale);
                }
                Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, DustID.PinkFlame)];
            }
            else if (npc.ai[3] == 4) // gone
            {
                npc.alpha = 255;
                npc.ai[1]++;
                if (npc.ai[1] > 300)
                {
                    npc.ai[3] = 2;
                    npc.ai[1] = 0;
                    npc.netUpdate = true;
                }
            }
            else if (npc.ai[3] == 5) // despawn
            {
                npc.alpha += 5;
                if (npc.alpha >= 255) npc.active = false;
            }
            if (npc.alpha == 0) npc.GetGlobalNPC<NPCsGLOBAL>().platformNPC = true;
            else npc.GetGlobalNPC<NPCsGLOBAL>().platformNPC = false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = mod.GetTexture("NPCs/MovingPlatforms/" + GetType().Name + "_Glow");
            Rectangle frame = new Rectangle(0, npc.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[npc.type]);
            Vector2 origin = new Vector2(texture.Width * 0.5f, (texture.Height / Main.npcFrameCount[npc.type]) * 0.5f);
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, 4), frame, Color.White * (1 - ((float)npc.alpha / 255)), Main.LocalPlayer.fullRotation, origin, npc.scale, effects, 0.0f);
            if (npc.ai[3] == 0) spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, 4), new Rectangle(0,48, texture.Width, 24), Color.White * Math.Min(npc.ai[1] / 180,1), Main.LocalPlayer.fullRotation, origin, npc.scale, effects, 0.0f);

        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Rectangle frame = new Rectangle(0, npc.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[npc.type]);
            Vector2 origin = new Vector2(texture.Width * 0.5f, (texture.Height / Main.npcFrameCount[npc.type]) * 0.5f);
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, 4), frame, drawColor * (1 - ((float)npc.alpha / 255)), Main.LocalPlayer.fullRotation, origin, npc.scale, effects, 0.0f);
            return false;
        }
    }
}
