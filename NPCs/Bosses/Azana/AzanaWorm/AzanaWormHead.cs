using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Azana.AzanaWorm
{
    public class AzanaWormHead : ModNPC
    {
        public int halfLife = 0;
        public int dayTime = 0;
        public override void SetDefaults()
        {
            npc.lifeMax = 150000;
            npc.damage = 300;
            npc.defense = 90;
            npc.knockBackResist = 0f;
            npc.width = 40;
            npc.height = 38;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            Main.npcFrameCount[npc.type] = 1;
            npc.value = Item.buyPrice(0, 5, 0, 0);
            npc.npcSlots = 1f;
            npc.netAlways = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Daybreak] = true;
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;
            npc.buffImmune[mod.BuffType("SoulInferno")] = true;
            npc.buffImmune[mod.BuffType("ExtinctionCurse")] = true;

        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 400;
            npc.lifeMax = 300000;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 450000;
                npc.damage = 500;
                npc.defense = 110;
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Andromeda");
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(), drawColor, npc.rotation, origin, npc.scale, SpriteEffects.None, 0);
            return false;
        }
        public override bool PreAI()
        {
            Player P = Main.player[npc.target];
            bool expertMode = Main.expertMode;
            if (Main.player[npc.target].dead)
            {
                npc.velocity.Y = npc.velocity.Y + 0.11f;
                npc.ai[1]++;
                if (npc.ai[1] >= 300)
                {
                    npc.active = false;
                }
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (npc.ai[0] == 0)
                {
                    // So, here we assing the npc.realLife value.
                    // The npc.realLife value is mainly used to determine which NPC loses life when we hit this NPC.
                    // We don't want every single piece of the worm to have its own HP pool, so this is a neat way to fix that.
                    npc.realLife = npc.whoAmI;
                    // LatestNPC is going to be used later on and I'll explain it there.
                    int latestNPC = npc.whoAmI;
                    int randomWormLength = Main.rand.Next(20, 30);
                    for (int i = 0; i < randomWormLength; ++i)
                    {
                        // We spawn a new NPC, setting latestNPC to the newer NPC, whilst also using that same variable
                        // to set the parent of this new NPC. The parent of the new NPC (may it be a tail or body part)
                        // will determine the movement of this new NPC.
                        // Under there, we also set the realLife value of the new NPC, because of what is explained above.
                        latestNPC = NPC.NewNPC((int)npc.position.X + (npc.width / 2), (int)npc.position.Y + (npc.height / 2), mod.NPCType("AzanaWormBody"), npc.whoAmI, 0, latestNPC);

                        Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                        Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
                    }
                    // When we're out of that loop, we want to 'close' the worm with a tail part!
                    NPC.NewNPC((int)npc.position.X + (npc.width / 2), (int)npc.position.Y + (npc.height / 2), mod.NPCType("AzanaWormTail"), npc.whoAmI, 0, latestNPC);
                    Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                    Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;

                    npc.ai[0] = 1;
                    npc.netUpdate = true;
                }
            }
            // speed determines the max speed at which this NPC can move.
            // Higher value = faster speed.
            float speed = 20f;
            // acceleration is exactly what it sounds like. The speed at which this NPC accelerates.
            float acceleration = 0.1f;

            Vector2 npcCenter = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float targetXPos = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2);
            float targetYPos = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2);

            float targetRoundedPosX = (float)((int)(targetXPos / 16.0) * 16);
            float targetRoundedPosY = (float)((int)(targetYPos / 16.0) * 16);
            npcCenter.X = (float)((int)(npcCenter.X / 16.0) * 16);
            npcCenter.Y = (float)((int)(npcCenter.Y / 16.0) * 16);
            float dirX = targetRoundedPosX - npcCenter.X;
            float dirY = targetRoundedPosY - npcCenter.Y;
            Vector2 vector168 = npc.Center;
            float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
            npc.TargetClosest(true);

            // Else we want to play some audio (soundDelay) and move towards our target.
                float absDirX = Math.Abs(dirX);
                float absDirY = Math.Abs(dirY);
                float newSpeed = speed / length;
                dirX = dirX * newSpeed;
                dirY = dirY * newSpeed;
                if (npc.velocity.X > 0.0 && dirX > 0.0 || npc.velocity.X < 0.0 && dirX < 0.0 || (npc.velocity.Y > 0.0 && dirY > 0.0 || npc.velocity.Y < 0.0 && dirY < 0.0))
                {
                    if (npc.velocity.X < dirX)
                        npc.velocity.X = npc.velocity.X + acceleration;
                    else if (npc.velocity.X > dirX)
                        npc.velocity.X = npc.velocity.X - acceleration;
                    if (npc.velocity.Y < dirY)
                        npc.velocity.Y = npc.velocity.Y + acceleration;
                    else if (npc.velocity.Y > dirY)
                        npc.velocity.Y = npc.velocity.Y - acceleration;
                    if (Math.Abs(dirY) < speed * 0.2 && (npc.velocity.X > 0.0 && dirX < 0.0 || npc.velocity.X < 0.0 && dirX > 0.0))
                    {
                        if (npc.velocity.Y > 0.0)
                            npc.velocity.Y = npc.velocity.Y + acceleration * 2f;
                        else
                            npc.velocity.Y = npc.velocity.Y - acceleration * 2f;
                    }
                    if (Math.Abs(dirX) < speed * 0.2 && (npc.velocity.Y > 0.0 && dirY < 0.0 || npc.velocity.Y < 0.0 && dirY > 0.0))
                    {
                        if (npc.velocity.X > 0.0)
                            npc.velocity.X = npc.velocity.X + acceleration * 2f;
                        else
                            npc.velocity.X = npc.velocity.X - acceleration * 2f;
                    }
                }
                else if (absDirX > absDirY)
                {
                    if (npc.velocity.X < dirX)
                        npc.velocity.X = npc.velocity.X + acceleration * 1.1f;
                    else if (npc.velocity.X > dirX)
                        npc.velocity.X = npc.velocity.X - acceleration * 1.1f;
                    if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.5)
                    {
                        if (npc.velocity.Y > 0.0)
                            npc.velocity.Y = npc.velocity.Y + acceleration;
                        else
                            npc.velocity.Y = npc.velocity.Y - acceleration;
                    }
                }
                else
                {
                    if (npc.velocity.Y < dirY)
                        npc.velocity.Y = npc.velocity.Y + acceleration * 1.1f;
                    else if (npc.velocity.Y > dirY)
                        npc.velocity.Y = npc.velocity.Y - acceleration * 1.1f;
                    if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.5)
                    {
                        if (npc.velocity.X > 0.0)
                            npc.velocity.X = npc.velocity.X + acceleration;
                        else
                            npc.velocity.X = npc.velocity.X - acceleration;
                    }
                }
            // Set the correct rotation for this NPC.
            npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;

            // Some netupdate stuff (multiplayer compatibility).
            if ((npc.velocity.X > 0.0 && npc.oldVelocity.X < 0.0 || npc.velocity.X < 0.0 && npc.oldVelocity.X > 0.0 || (npc.velocity.Y > 0.0 && npc.oldVelocity.Y < 0.0 || npc.velocity.Y < 0.0 && npc.oldVelocity.Y > 0.0)) && !npc.justHit)
                npc.netUpdate = true;

            return false;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.2f;   //this make the NPC Health Bar biger
            return null;
        }
    }
}
