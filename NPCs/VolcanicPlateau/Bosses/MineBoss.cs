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
using Microsoft.Xna.Framework.Audio;
using ElementsAwoken.Projectiles.NPCProj.MineBoss;

namespace ElementsAwoken.NPCs.VolcanicPlateau.Bosses
{
    public class MineBoss : ModNPC
    {
        private float aiState
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float aiTimer
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float aggro
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float drillRot
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 180;
            npc.height = 68;

            npc.aiStyle = -1;

            npc.defense = 8;
            npc.lifeMax = 3000;
            npc.damage = 30;
            npc.knockBackResist = 0f;

            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;

            npc.value = Item.buyPrice(0, 0, 2, 0);
            npc.knockBackResist = 0f;
            npc.gfxOffY = -4f;

            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.boss = true;
            npc.behindTiles = true;

            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffType<Buffs.Debuffs.AcidBurn>()] = true;

            //npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "The abandoned automatron is an old mining rig designed by a group of scientists to harvest Scarlet Steel, a form of Hellstone compressed by great tectonic movement with superior conductivity and durability. The mining operation was abandoned when chaotron particles were discovered, the last functioning mining rig remaining in its position until it was put to rest.";
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abandoned Automatron");
        }
        public override void NPCLoot()
        {
            if (!MyWorld.downedMineBoss) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Items.Other.BlueprintItem1>());
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Items.ItemSets.ScarletSteel.Scarletite>(), Main.rand.Next(12, 25));
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Dynamite, Main.rand.Next(5, 10));
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Bomb, Main.rand.Next(15, 30));
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Items.BossDrops.MineBoss.MiningCharge>(), Main.rand.Next(60, 80));
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Items.BossDrops.MineBoss.CombatDrill>());
            MyWorld.downedMineBoss = true;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData);
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            int toPlayer = Math.Sign(player.Center.X - npc.Center.X);
            if (toPlayer == npc.direction) damage = 0;
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            int toProj = Math.Sign(projectile.Center.X - npc.Center.X);
            if (toProj == npc.direction) damage = 0;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = mod.GetTexture("NPCs/VolcanicPlateau/Bosses/MineBossDrill");
            Rectangle frame = new Rectangle(0, npc.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[npc.type]);
            Vector2 origin = (npc.spriteDirection == 1 ? new Vector2(14, 32) : new Vector2(88, 32));
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Vector2 shake = Vector2.Zero;
            if (drillRot == -1 || drillRot == 1) shake += Main.rand.NextVector2Square(-3, 3);
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(28 * npc.spriteDirection, 0) + shake, frame, drawColor, drillRot, origin, npc.scale, effects, 0.0f);
            return true;
        }
        public override void AI()
        {
            npc.spriteDirection = npc.direction;
            npc.TargetClosest(false);
            Player player = Main.player[npc.target];
            Point playerTile = player.Center.ToTileCoordinates();
            bool inArena = playerTile.X > EAWorldGen.mineBossArenaLoc.X && playerTile.X < EAWorldGen.mineBossArenaLoc.X + 100 && playerTile.Y > EAWorldGen.mineBossArenaLoc.Y + 4 && playerTile.Y < EAWorldGen.mineBossArenaLoc.Y + 18;
            if (!inArena && aiTimer <= 0)
            {
                npc.directionY = -1;
                npc.dontTakeDamage = true;
                npc.immortal = true;
                npc.GivenName = "???";
                music = -1;
                npc.boss = false;
                npc.direction = 1;
                aggro = 0;
                if (aiTimer == 0)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        for (int j = 0; j < 18; j++)
                        {
                            Tile t = Framing.GetTileSafely(i + EAWorldGen.mineBossArenaLoc.X, j + EAWorldGen.mineBossArenaLoc.Y);
                            t.liquid = 0;
                        }
                    }
                     aiTimer = -120;
                }
            }
            else if (!inArena)
            {
                npc.TargetClosest(true);
                inArena = playerTile.X > EAWorldGen.mineBossArenaLoc.X && playerTile.X < EAWorldGen.mineBossArenaLoc.X + 100 && playerTile.Y > EAWorldGen.mineBossArenaLoc.Y && playerTile.Y < EAWorldGen.mineBossArenaLoc.Y + 18;
                if (!inArena) npc.active = false;
            }
            else
            {
                aggro = 1;
                npc.GivenName = "";
                npc.boss = true;
                music = 0;
                npc.dontTakeDamage = false;
                npc.immortal = false;
                if (aiState == 0)
                {
                    if (drillRot > 0) drillRot -= 0.02f;
                    if (drillRot < 0) drillRot += 0.02f;
                    if (Math.Abs(drillRot) < 0.05) drillRot = 0;
                    npc.noGravity = false;
                    aiTimer++;
                    int num = Main.expertMode ? MyWorld.awakenedMode ? 60 : 120 : 200;
                    if (aiTimer > num / 2)
                    {
                        Dust dust = Main.dust[Dust.NewDust(npc.Center + new Vector2(40 * npc.direction, -10), 20, 20, 169, 0f, 0f, 150)];
                        dust.velocity.X = 4 * npc.direction;
                    }
                    if (aiTimer > num)
                    {
                        aiTimer = 0;
                        aiState = 1;
                    }
                }
                else if (aiState == 1)
                {
                    npc.noGravity = true;
                    aiTimer++;
                    if (Math.Abs(npc.velocity.X) < 0.03f && aiTimer > 5)
                    {
                        aiState = 2;
                        aiTimer = 0;
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 70, 1, -0.3f);
                        Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 14, 1, -0.5f);
                        int tileHeight = npc.height / 16;
                        for (int i = 0; i < tileHeight; i++)
                        {
                            WorldGen.KillTile((int)(npc.Center.X + ((npc.width / 2 + 18) * npc.direction)) / 16, (int)npc.position.Y / 16 + i, false, true);
                        }
                        return;
                    }
                    float lifeSpeedScale = MathHelper.Lerp(2, 1, (float)npc.life / (float)npc.lifeMax);
                    if (Math.Abs(npc.velocity.X) < 10) npc.velocity.X += npc.direction * 0.09f * lifeSpeedScale;
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 55, 1, -0.3f);
                }
                else if (aiState == 2)
                {
                    aiTimer++;
                    if (aiTimer > 180)
                    {
                        npc.direction = -npc.direction;
                        aiState = 3;
                        aiTimer = 0;
                    }
                }
                else if (aiState == 3)
                {
                    if (npc.direction == -1)
                    {
                        if (drillRot < 1) drillRot += 0.02f;
                        else drillRot = 1;
                    }
                    else
                    {

                        if (drillRot > -1) drillRot -= 0.02f;
                        else drillRot = -1;
                    }
                    aiTimer++;
                    if ((drillRot == -1 || drillRot == 1) && aiTimer % 20 == 0) Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 22, 1, -0.5f);
                    int num = Main.expertMode ? MyWorld.awakenedMode ? 6 : 10 : 18;
                    int num2 = Main.expertMode ? MyWorld.awakenedMode ? 3 : 4 : 6;
                    if (Main.rand.NextBool(num) && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 loc = new Vector2((EAWorldGen.mineBossArenaLoc.X + Main.rand.Next(1, 99)) * 16, (EAWorldGen.mineBossArenaLoc.Y + 8) * 16);
                        if (Main.rand.NextBool(num2)) loc = player.Center;
                        Projectile.NewProjectile(loc, new Vector2(0, -6), ProjectileType<IgneousRockSpawner>(), npc.damage / 2, 6, Main.myPlayer);
                    }
                    if (aiTimer > 600)
                    {
                        aiState = 0;
                        aiTimer = 0;
                    }
                }
            }
        }
    }
}
