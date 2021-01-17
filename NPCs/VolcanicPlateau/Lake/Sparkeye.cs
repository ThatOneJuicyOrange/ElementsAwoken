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

namespace ElementsAwoken.NPCs.VolcanicPlateau.Lake
{
    public class Sparkeye : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 18;

            npc.aiStyle = -1;

            npc.defense = 5;
            npc.lifeMax = 75;
            npc.damage = 18;
            npc.knockBackResist = 0.4f;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(0, 0, 2, 0);
            npc.knockBackResist = 0.2f;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.dontTakeDamageFromHostiles = true;

            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffType<Buffs.Debuffs.Incineration>()] = true;

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "Sparkeyes are an offshoot of charred cod that evolved their large eye to see through the thick lavas of the volcanic lake. They are solitary beings and you may catch one jumping out of the lava for fun, or to attack anything that moves above the surface...";
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sparkeye");
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = mod.GetTexture("NPCs/VolcanicPlateau/Lake/" + GetType().Name + "_Glow");
            Rectangle frame = new Rectangle(0, npc.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[npc.type]);
            Vector2 origin = new Vector2(texture.Width * 0.5f, (texture.Height / Main.npcFrameCount[npc.type]) * 0.5f);
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, 4), frame, Color.White, npc.rotation, origin, npc.scale, effects, 0.0f);
        }
        public override void NPCLoot()
        {
            // Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Drakonite"), Main.rand.Next(1, 3));
        }

        
        public override void AI()
        {
            npc.spriteDirection = npc.direction;
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            bool aggressive = false;
            for (int i = 0; i < 6; i++)
            {
                Point tilePos = player.Bottom.ToTileCoordinates();
                if (Framing.GetTileSafely((int)tilePos.X, (int)tilePos.Y + i).lava())
                {
                    aggressive = true;
                    break;
                }            
            }
            if (!aggressive)
            {
                npc.ai[0]++;
                if (npc.ai[0] < 600)
                {
                    npc.rotation = 0;
                    Vector2 target = new Vector2(npc.ai[2], npc.ai[3]);
                    if (GetInstance<Config>().debugMode)
                    {
                        Dust dust = Main.dust[Dust.NewDust(target, 2, 2, DustID.PinkFlame)];
                        dust.noGravity = true;
                    }
                    npc.ai[1]++;
                    if (npc.ai[1] > 600 || npc.ai[2] == 0 || Math.Abs(npc.Center.X - npc.ai[2]) < 20 || Math.Abs(npc.Center.Y - npc.ai[3]) < 20)
                    {
                        npc.ai[1] = 0;
                        FindLocation();
                    }

                    if (npc.lavaWet)
                    {
                        npc.direction = Math.Sign(target.X - npc.Center.X);
                        npc.directionY = Math.Sign(target.Y - npc.Center.Y);

                        if (npc.velocity.X > 0f && npc.direction < 0) npc.velocity.X = npc.velocity.X * 0.95f;
                        if (npc.velocity.X < 0f && npc.direction > 0) npc.velocity.X = npc.velocity.X * 0.95f;
                        if (npc.velocity.Y > 0f && npc.directionY < 0) npc.velocity.Y = npc.velocity.Y * 0.95f;
                        if (npc.velocity.Y < 0f && npc.directionY > 0) npc.velocity.Y = npc.velocity.Y * 0.95f;
                        float spdX = 0.03f;
                        float spdY = 0.01f;
                        npc.velocity.X = npc.velocity.X + (float)npc.direction * spdX;
                        npc.velocity.Y = npc.velocity.Y + (float)npc.directionY * spdY;

                        float maxVelX = 12;
                        float maxVelY = 8;
                        if (npc.velocity.X > maxVelX) npc.velocity.X = maxVelX;
                        if (npc.velocity.X < -maxVelX) npc.velocity.X = -maxVelX;
                        if (npc.velocity.Y > maxVelY) npc.velocity.Y = maxVelY;
                        if (npc.velocity.Y < -maxVelY) npc.velocity.Y = -maxVelY;
                    }
                }
                else
                {
                    if (npc.lavaWet)
                    {
                        if (npc.velocity.Y > -6) npc.velocity.Y -= 0.16f;
                        if ((npc.velocity.X > -6 && npc.direction == -1) || (npc.velocity.X < 6 && npc.direction == 1)) npc.velocity.X += npc.direction * 0.1f;
                        else npc.velocity.X *= 0.95f;
                        if (npc.ai[0] > 2000) npc.ai[0] = 0;
                        npc.rotation = npc.velocity.Y * 0.1f * npc.direction;
                    }
                    else
                    {
                        npc.ai[0] = 3000;
                    }
                }
            }
            else
            {
                if (npc.ai[0] > 10) npc.ai[0] = 0;
                npc.direction = Math.Sign(npc.velocity.X);
                if (npc.ai[0] < 2)
                {
                    npc.rotation = npc.velocity.Y * 0.1f * npc.direction;
                    if (npc.lavaWet)
                    {
                        if (npc.ai[0] == 1) npc.ai[0] = 2;
                        if (npc.ai[0] != 2)
                        {
                            Vector2 toTarget = new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y);
                            toTarget.Normalize();
                            if (npc.velocity.X > 0f && toTarget.X < 0) npc.velocity.X = npc.velocity.X * 0.95f;
                            if (npc.velocity.X < 0f && toTarget.X > 0) npc.velocity.X = npc.velocity.X * 0.95f;
                            float spdX = 0.2f;
                            float spdY = 0.1f;
                            float fullSpd = 5;
                            if (Math.Abs(npc.Center.X - player.Center.X) > 500)
                            {
                                npc.velocity.X += toTarget.X * spdX;
                                if (Math.Abs(npc.Center.Y - player.Center.Y) < 600)
                                {
                                    if (npc.velocity.Y < 6) npc.velocity.Y += 0.16f;
                                }
                                else npc.velocity.Y *= 0.95f;
                            }
                            else
                            {
                                npc.velocity.X = toTarget.X * fullSpd;
                                npc.velocity.Y += toTarget.Y * spdY;
                            }

                            float maxVelX = 8;
                            float maxVelY = 8;
                            if (npc.velocity.X > maxVelX) npc.velocity.X = maxVelX;
                            if (npc.velocity.X < -maxVelX) npc.velocity.X = -maxVelX;
                            if (npc.velocity.Y > maxVelY) npc.velocity.Y = maxVelY;
                            if (npc.velocity.Y < -maxVelY) npc.velocity.Y = -maxVelY;
                        }
                    }
                    else
                    {
                        npc.ai[0] = 1;
                       // npc.velocity.Y += 0.16f;
                    }
                }
                else
                {
                    npc.ai[0]++;
                    npc.velocity.Y += 0.16f;
                    npc.velocity.X *= 0.95f;
                }
            }
                
            if (!npc.lavaWet)
            {
                npc.velocity.Y += 0.16f;
            }
            NPCsGLOBAL.GoThroughPlatforms(npc);
        }
        private void FindLocation(int attempts = 0)
        {
            npc.ai[2] = npc.Center.X + Main.rand.Next(-200, 201);
            npc.ai[3] = npc.Center.Y + Main.rand.Next(-200, 201);
            Tile tile = Framing.GetTileSafely((int)npc.ai[2]/16, (int)npc.ai[3] / 16);
            if ((!tile.lava() || !Collision.CanHit(npc.Center,2,2,new Vector2(npc.ai[2],npc.ai[3]),2,2)) && attempts < 20) FindLocation(attempts + 1);
        }
    }
}
