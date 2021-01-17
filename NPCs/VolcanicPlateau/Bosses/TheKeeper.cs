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
using ElementsAwoken.Projectiles.NPCProj.TheKeeper;

namespace ElementsAwoken.NPCs.VolcanicPlateau.Bosses
{
    public class TheKeeper : ModNPC
    {
        private Vector2 start = Vector2.Zero;
        private float activated
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float aiTimer
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float throwTimer
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float aiState
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 52;
            npc.height = 74;

            npc.aiStyle = -1;

            npc.defense = 10;
            npc.lifeMax = 1000;
            npc.damage = 18;
            npc.knockBackResist = 0.4f;

            npc.HitSound = SoundID.NPCHit2;
            npc.DeathSound = SoundID.NPCDeath6;

            npc.value = Item.buyPrice(0, 0, 2, 0);
            npc.knockBackResist = 0.2f;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.gfxOffY = -4f;

            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffType<Buffs.Debuffs.Incineration>()] = true;

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "The Keeper is a well-known and dangerous wisp that summons bones as armour. It, unlike its lesser brethren, has the ability to absorb ambient magic, resulting in its increased size and strength compared to regular flamewisps. Its lair is located in lava, where it nests in the skeletal remains of an ancient flarethorn eel. It can - and frequently does - kill and add the bones of the unwary adventurer to its collection.";
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Extinguished Flamewisp");
            Main.npcFrameCount[npc.type] = 14;
        }
        public override void NPCLoot()
        {
            if (Main.rand.NextBool()) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Obsidian, Main.rand.Next(1, 3));

            MyWorld.downedKeeper = true;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData);
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.frame.Y < npc.localAI[0] * 9 && npc.frame.Y > 0)
            {
                float alpha = MathHelper.Min(aiTimer / 60f, 1f);
                Texture2D skull = GetTexture("ElementsAwoken/NPCs/VolcanicPlateau/Bosses/TheKeeperSkull");
                SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                int y = (int)MathHelper.Lerp(0, -60, MathHelper.Min(aiTimer / 80f, 1f));
                float rot = MathHelper.Lerp(1.57f, 0, MathHelper.Min(aiTimer / 80f, 1f));
                spriteBatch.Draw(skull, npc.Bottom + new Vector2(0, y) - Main.screenPosition + new Vector2(0, npc.gfxOffY), null, drawColor * alpha, rot, skull.Size() / 2, npc.scale, effects, 0.0f);
            }
            if (Main.xMas && npc.frame.Y >= npc.localAI[0] * 9)
            {
                Texture2D texture = GetTexture("ElementsAwoken/NPCs/VolcanicPlateau/Bosses/TheKeeperHat");
                SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                spriteBatch.Draw(texture, npc.Top - new Vector2(-6 * npc.spriteDirection, 0) - Main.screenPosition + new Vector2(0, npc.gfxOffY), null, drawColor, npc.rotation, texture.Size() /2, npc.scale, effects, 0.0f);             
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.localAI[0] = frameHeight;
            if (activated == 1 && npc.frame.Y < frameHeight * 9)
            {
                npc.frameCounter += 1;
                if (npc.frameCounter > 8)
                {
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    npc.frameCounter = 0.0;
                }
            }
            else if (activated == 2 && (throwTimer > -16 || npc.frame.Y != frameHeight * 9))
            {
                npc.frameCounter += 1;
                if (npc.frameCounter > 6)
                {
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    npc.frameCounter = 0.0;
                }
                if (npc.frame.Y > frameHeight * 13) npc.frame.Y = frameHeight * 9;
            }
        }
        public override void AI()
        {
            Lighting.AddLight(npc.Center, 1f, 0.6f, 0.3f);
            EAUtils.PushOtherEntities(npc);
            npc.TargetClosest(true);

            if (start == Vector2.Zero)
            {
                start = npc.Center;
            }
            Player player = Main.player[npc.target];
            if (activated == 0)
            {
                npc.noGravity = false;
                Rectangle arena = new Rectangle((int)start.X - 24 * 16, (int)start.Y - 10 * 16, 49 * 16, 20 * 16);
                if (arena.Contains(player.Center.ToPoint()))
                {
                    activated = 1;
                }
            }
            else if (activated == 1)
            {
                npc.noGravity = true;
                aiTimer++;
                if (aiTimer > 80)
                {
                    activated = 2;
                    aiTimer = 0;
                    throwTimer = -180;
                }
            }
            else
            {
                bool inArena = true;
                Rectangle arena = new Rectangle((int)start.X - 24 * 16, (int)start.Y - 19 * 16, 49 * 16, 30 * 16);
                if (!arena.Contains(player.Center.ToPoint()))
                {
                    inArena = false;
                }
                    npc.GivenName = "The Keeper";
                int tilesAboveBlocks = 99999;

                npc.spriteDirection = Math.Sign(npc.Center.X - player.Center.X);

                Vector2 targetPos = player.Center;
                targetPos.X -= Math.Sign(targetPos.X - npc.Center.X) * 60;

                // floaty
                Point wispTile = npc.Bottom.ToTileCoordinates();
                for (int i = 0; i < 10; i++)
                {
                    Tile t = Framing.GetTileSafely(wispTile.X, wispTile.Y + i);
                    if (t.active() && Main.tileSolid[t.type])
                    {
                        tilesAboveBlocks = i;
                        break;
                    }
                }
                if (tilesAboveBlocks < 8) npc.velocity.Y -= 0.04f;
                else
                {
                    npc.velocity.Y += 0.04f;
                }
                if (Math.Abs(npc.velocity.Y) > 4) npc.velocity.Y *= 0.95f;
                if (aiState == 0)
                {
                    // floaty, but with intention
                    if (throwTimer < 0) throwTimer++;
                    if (throwTimer >= 0)
                    {
                        Main.PlaySound(SoundID.Item19, npc.position);

                        float dist = Vector2.Distance(player.Center, npc.Center);

                        int timeScale = Main.expertMode ? MyWorld.awakenedMode ? 8 : 6 : 4;
                        if (dist > 300) dist = 300; // to stop roof throwing
                        float time = dist / timeScale;
                        float grav = 0.16f;
                        float xSPD = (player.Center.X - npc.Center.X) / time;
                        float ySPD = (player.Center.Y - npc.Center.Y - 0.5f * grav * time * time) / time;
                        Projectile.NewProjectile(npc.Center.X + 28 * npc.spriteDirection, npc.Center.Y + 4, xSPD, ySPD, ProjectileType<KeeperBone>(), npc.damage / 2, 0f, Main.myPlayer, grav, 0f);

                        throwTimer = -90;
                    }
                    int tilesSideBlocks = 30;
                    int dirToBlocks = 0;
                    for (int d = -1; d <= 1; d += 2)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            Tile t = Framing.GetTileSafely(wispTile.X + i * d, wispTile.Y);
                            if (t.active() && Main.tileSolid[t.type])
                            {
                                tilesSideBlocks = i;
                                dirToBlocks = d;
                                d++;
                                break;
                            }
                        }
                    }
                    if (tilesSideBlocks < 8)
                    {
                        npc.velocity.X -= 0.1f * dirToBlocks;

                    }
                    else
                    {
                        int toX = Math.Sign(targetPos.X - npc.Center.X);
                        if (npc.velocity.X < 3 && npc.velocity.X > -3) npc.velocity.X -= toX * 0.05f;
                        if (toX > 0f && npc.velocity.X < 0 || toX < 0f && npc.velocity.X > 0) npc.velocity.X = npc.velocity.X * 0.98f;
                    }
                    if (aiTimer > 300)
                    {
                        aiTimer = 0;
                        aiState = 1;
                    }
                }
                else if (aiState == 1)
                {
                    if (aiTimer <= 30 && tilesAboveBlocks < 15)
                    {
                        npc.velocity.Y -= 0.1f;
                    }
                    else if (aiTimer <= 9999f)
                    {
                        if (Math.Abs(npc.velocity.Y) < 0.3f)
                        {
                            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 69, pitchOffset: -0.5f);
                            aiTimer = 9999f;
                            for (int n = 0; n < 5; n++)
                            {
                                Vector2 monolithPos = start + new Vector2(Main.rand.Next(-27 * 16, 20 * 16), 0); // 28 left    21 right
                                if (n == 4 && inArena) monolithPos = player.Center;
                                Point monolithPoint = monolithPos.ToTileCoordinates();
                                for (int j = monolithPoint.Y; j < Main.maxTilesY; j++)
                                {
                                    Tile newTile = Framing.GetTileSafely(monolithPoint.X, j);
                                    if (newTile.active() && Main.tileSolid[newTile.type])
                                    {
                                        monolithPoint = new Point(monolithPoint.X, j);
                                        monolithPos = new Vector2(monolithPoint.X * 16, monolithPoint.Y * 16);
                                        break;
                                    }
                                }
                                Projectile proj = Main.projectile[Projectile.NewProjectile(monolithPos.X, monolithPos.Y + 40, 0f, 0f, ProjectileType<KeeperSpike>(), npc.damage, 0f, Main.myPlayer, -120)];
                                proj.spriteDirection = Main.rand.NextBool() ? 1 : -1;
                            }
                        }
                        npc.velocity.Y = 10f;
                    }
                    else
                    {
                        if (aiTimer > 10180)
                        {
                            if (Main.rand.NextBool(3)) aiState = 2;
                            else aiState = 0;
                            aiTimer = 0;
                        }
                    }
                }
                else if (aiState == 2)
                {
                    Main.PlaySound(SoundID.DD2_BetsyFireballShot, npc.position);
                    int num = Main.expertMode ? MyWorld.awakenedMode ? 5 : 3 : 2;
                    for (int n = 0; n < num; n++)
                    {
                        NPC wisp = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 12, NPCType<ForgottenBoneWisp>())];
                    }
                    aiState = 0;
                    aiTimer = 0;
                }
                aiTimer++;
            }

        }
    }
}
