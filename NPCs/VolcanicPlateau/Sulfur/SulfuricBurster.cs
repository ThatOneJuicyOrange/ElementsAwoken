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

namespace ElementsAwoken.NPCs.VolcanicPlateau.Sulfur
{
    public class SulfuricBurster : ModNPC
    {
        private bool voidBreak = true;
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
        private float tpLocY
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float visualsAI
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 54;
            npc.height = 90;

            npc.aiStyle = -1;

            npc.defense = 20;
            npc.lifeMax = 700;
            npc.damage = 30;
            npc.knockBackResist = 0f;

            npc.HitSound = SoundID.NPCHit2;
            npc.DeathSound = SoundID.NPCDeath6;

            npc.value = Item.buyPrice(0, 0, 2, 0);
            npc.knockBackResist = 0.2f;

            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.behindTiles = true;

            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffType<Buffs.Debuffs.AcidBurn>()] = true;

            //npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "Acid bursters are powerful creatures formed out of sulphur that chase their foes through the Toxic Dunes. They dig through the sediment and burst out underneath their targets, covering them in acid or throwing balls of sulfur at them instead.";
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid Burster");
        }
        public override void NPCLoot()
        {
             if (Main.rand.NextBool())Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Items.Placeable.Tiles.Plateau.SulfuricSedimentItem>(), Main.rand.Next(1, 3));
        }
        public override void AI()
        {
            if (voidBreak && Main.netMode != NetmodeID.MultiplayerClient)
            {
                PlateauNPCs.TryVoidbreak(npc, NPCType<VoidbrokenBurster>());
                voidBreak = false;
            }

            npc.velocity = Vector2.Zero;
            npc.spriteDirection = npc.direction;
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead) npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            npc.soundDelay--;
            if (npc.soundDelay <= 0)
            {
                float pitch = Main.rand.NextFloat(-0.5f, 0);
                Main.PlaySound(SoundID.Zombie, (int)npc.position.X, (int)npc.position.Y, 93,1, pitch);
                npc.soundDelay = Main.rand.Next(120, 900);
            }
            if (aiState == 0)
            {
                npc.noTileCollide = true;
                npc.noGravity = true;
                npc.position.Y += 2;
                aiTimer++;
                if (aiTimer % 3 == 0)WorldGen.KillTile((int)npc.Bottom.X / 16, (int)npc.Bottom.Y / 16, false, true);
                if (Collision.SolidCollision(npc.position, npc.width, 2))
                {
                    aiState = Main.rand.NextBool() ? 1 : 4;
                    aiTimer = 0;
                }
            }
            else if (aiState == 1)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.velocity = Vector2.Zero;
                    Point playerPoint = player.Center.ToTileCoordinates();
                    int y = FindFirstYTile(playerPoint.X, playerPoint.Y);
                    if (y != -1) npc.Top = new Vector2(player.Center.X, y * 16 + 20);
                    else aiState = 2;
                    npc.netUpdate = true;
                }
                aiState = 2;
            }
            else if (aiState == 2)
            {
                npc.TargetClosest(true);
                aiTimer++;
                if (aiTimer < 120)
                {
                    int width = 10;
                    Dust dust = Main.dust[Dust.NewDust(npc.Top - new Vector2(width / 2, 20), width, 2, 74, 0f, 0f, 150)];
                    dust.velocity.Y = -3 * Main.rand.NextFloat(0.4f, 1f);
                }
                else if (Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    npc.position.Y -= 6;
                }
                else
                {
                    WorldGen.KillTile((int)npc.Bottom.X / 16, (int)npc.Bottom.Y / 16, false, true);
                    Main.PlaySound(SoundID.Zombie, (int)npc.position.X, (int)npc.position.Y, 53,1,-0.5f);
                    aiState = 3;
                    aiTimer = 0;
                }
                if (aiTimer > 300)
                {
                    aiState = 3;
                    aiTimer = 0;
                }
            }
            else if (aiState == 3)
            {
                npc.noTileCollide = false;
                npc.noGravity = false;
                aiTimer++;
                if (aiTimer > 120)
                {
                    aiState = 0;
                    aiTimer = 0;
                }
            }
            else if (aiState == 4)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.velocity = Vector2.Zero;
                    Vector2 desiredPos = player.Center + new Vector2(Main.rand.Next(-300, 300), 0);
                    Point playerPoint = desiredPos.ToTileCoordinates();
                    int y = FindFirstYTile(playerPoint.X, playerPoint.Y);
                    if (y != -1) npc.Top = new Vector2(desiredPos.X, y * 16 + 20);
                    else aiState = 2;
                    npc.netUpdate = true;
                }
                aiState = 5;
            }
            else if (aiState == 5)
            {
                npc.TargetClosest(true);
                npc.velocity = Vector2.Zero;
                aiTimer++;
                if (aiTimer < 60)
                {
                    int width = 10;
                    Dust dust = Main.dust[Dust.NewDust(npc.Top - new Vector2(width / 2, 20), width, 2, 74, 0f, 0f, 150)];
                    dust.velocity.Y = -3 * Main.rand.NextFloat(0.4f, 1f);
                }
                else if (Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    npc.position.Y -= 6;
                }
                else
                {
                    WorldGen.KillTile((int)npc.Bottom.X / 16, (int)npc.Bottom.Y / 16, false, true);
                    Main.PlaySound(SoundID.Zombie, (int)npc.position.X, (int)npc.position.Y, 53, 1, -0.5f);
                    aiState = 6;
                    aiTimer = 0;
                }
            }
            else if (aiState == 6)
            {
                aiTimer++;
                if (aiTimer % 60 == 0)
                {
                    if (Vector2.Distance(player.Center, npc.Center) < 500)
                    {
                        float time = 60;
                        float grav = 0.16f;
                        float xSPD = (player.Center.X - npc.Center.X) / time;
                        float ySPD = (player.Center.Y - npc.Center.Y - 0.5f * grav * time * time) / time;
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, xSPD, ySPD, ProjectileType<SulfurBall>(), npc.damage / 2, 0f, Main.myPlayer, grav, 0f);
                    }
                    else
                    {
                        aiState = 0;
                        aiTimer = 0;
                    }
                }
                if (aiTimer > 600)
                {
                    aiState = 0;
                    aiTimer = 0;
                }
            }
            if (aiState != 3 && aiState != 6)
            {
                if (npc.Hitbox.Contains(Main.MouseWorld.ToPoint())) Main.LocalPlayer.GetModPlayer<MyPlayer>().cantSeeHoverText = true;
            }
        }
        private int FindFirstYTile(int x, int startY)
        {
            for (int j = 0; j < 30; j++)
            {
                Tile t = Framing.GetTileSafely(new Point(x, startY + j));
                if (t.active() && Main.tileSolid[t.type])
                {
                    return startY + j;
                }
            }
            return -1;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            if (aiState != 3 && aiState != 6) return false;
            return base.DrawHealthBar(hbPosition, ref scale, ref position);
        }
    }
}
