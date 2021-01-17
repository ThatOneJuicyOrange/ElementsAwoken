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

namespace ElementsAwoken.NPCs.TheOrder
{
    public class ResistanceLeader : ModNPC
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
        private float damageTaken
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
            npc.width = 20;
            npc.height = 40;

            npc.aiStyle = -1;

            npc.defense = 20;
            npc.lifeMax = 5000;
            npc.damage = 30;
            npc.knockBackResist = 0f;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(0, 15, 0, 0);
            npc.knockBackResist = 0f;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.boss = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffType<Buffs.Debuffs.AcidBurn>()] = true;

            //npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Resistance Commander");
        }
        public override void NPCLoot()
        {
            //if (Main.rand.NextBool())Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Items.Placeable.Tiles.Plateau.SulfuricSedimentItem>(), Main.rand.Next(1, 3));
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void FindFrame(int frameHeight)
        {

            /*npc.frameCounter += (int)Math.Abs(npc.velocity.X);
            if (Math.Abs(npc.velocity.Y) < 0.02f)
            {
                if (npc.frameCounter > 15)
                {
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    npc.frameCounter = 0.0;
                }
                if (npc.frame.Y > frameHeight * 3)
                {
                    npc.frame.Y = 0;
                }
            }
            else
            {
                npc.frame.Y = frameHeight * 4;
            }*/
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            npc.spriteDirection = npc.direction;
            Player player = Main.player[npc.target];
            Point playerTile = player.Center.ToTileCoordinates();
            if (aiState == 0)
            {
                if (Vector2.Distance(player.Center, npc.Center) < 1000)
                {
                    music = MusicID.Boss1;
                    aiState = 1;
                }
            }
            else if (aiState == 1)
            {
                aiTimer++;
                if (aiTimer % 60 == 0)
                {
                    TrackingShot(player,12);
                }
                if (aiTimer >= 300)
                {
                    aiState = 2;
                    aiTimer = 0;
                }
                Fly(0.02f, new Vector2(player.Center.X - 300 * npc.direction, player.Center.Y));
            }
            else if (aiState == 2)
            {
                npc.velocity *= 0.95f;
                aiTimer++;
                if (aiTimer == 1)
                {
                    Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/MinigunCharge"), 0.4f);
                }
                if (aiTimer >= 72)
                {
                    if (npc.soundDelay <= 0)
                    {
                        npc.soundDelay = 68;
                        //Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/MinigunSpinLoop"), 0.4f);
                    }
                    if (aiTimer % 3 == 0)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 40);
                        float speed = 12;
                        float rotation = (float)Math.Atan2(npc.Center.Y - player.Center.Y, npc.Center.X - player.Center.X);
                        Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1)).RotatedByRandom(MathHelper.ToRadians(30));
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.BulletDeadeye, npc.damage / 2, 0f, Main.myPlayer);
                    }
                }
                if (aiTimer >= 252)
                {
                    aiState = 3;
                    aiTimer = 0;
                }
            }
            else if (aiState == 3)
            {
                aiTimer++;
                if (aiTimer >= 180)
                {
                    aiState = 4;
                    aiTimer = 0;
                }
                if (aiTimer % 60 == 0)
                {
                    TrackingShot(player,12);
                }
                Fly(0.02f, new Vector2(player.Center.X - 300 * npc.direction, player.Center.Y));
            }
            else if (aiState == 4)
            {
                aiTimer++;
                Fly(0.2f, new Vector2(player.Center.X, player.Center.Y - 800));
                if (npc.Center.Y < player.Center.Y - 800)
                {
                    npc.velocity.Y = 0;
                    aiTimer = 260;
                }
                if (aiTimer >= 260)
                {
                    aiState = 5;
                    aiTimer = 0;
                }
            }
            else if (aiState == 5)
            {
                npc.velocity *= 0.95f;
                aiTimer++;
                if (aiTimer % 20 == 0)
                {
                    float spread = 10;
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 61);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(-spread, spread), 0, ProjectileType<ResistanceRocket>(), npc.damage, 0f, Main.myPlayer);
                }
                if (aiTimer >= 260)
                {
                    aiState = 1;
                    aiTimer = 0;
                }
            }
            if (Vector2.Distance(player.Center, npc.Center) > 3000)
            {
                npc.active = false;
            }
            if (TileID.Sets.Platforms[Framing.GetTileSafely((int)npc.Bottom.X / 16, (int)npc.Bottom.Y / 16).type]) npc.position.Y += 0.3f;
            // dusts
            Vector2 dustPos = new Vector2(-22f, 11);
            if (npc.direction == -1)
            {
                dustPos.X = 22f;
            }
            dustPos = dustPos.RotatedBy((double)npc.rotation, default(Vector2));
            for (int i = 0; i < 2; i++)
            {
                Dust fire = Main.dust[Dust.NewDust(npc.Center + dustPos - Vector2.One * 5f, 2, 2, 6, 0f, 0f, 0, default(Color), 1f)];
                fire.scale *= 0.5f + (float)Main.rand.Next(10) * 0.1f;
                fire.noGravity = true;
                fire.velocity.X *= 0.2f;
                fire.velocity.Y = Main.rand.NextFloat(3, 6);
            }
                Dust smoke = Main.dust[Dust.NewDust(npc.Center + dustPos - Vector2.One * 5f, 2, 2, 31, 0f, 0f, 0, default(Color), 1f)];
                smoke.scale *= 0.3f + (float)Main.rand.Next(10) * 0.1f;
                smoke.noGravity = true;
                smoke.fadeIn = 1f + Main.rand.NextFloat(0, 0.5f);
                smoke.velocity.X *= 0.2f;
                smoke.velocity.Y = Main.rand.NextFloat(4, 7);
            
        }
        private void TrackingShot(Player player, float speed)
        {
            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 40);
            float framesToTravel = Vector2.Distance(player.Center, npc.Center) / speed - 1;
            float rotation = (float)Math.Atan2(npc.Center.Y - (player.Center.Y + player.velocity.Y * framesToTravel), npc.Center.X - (player.Center.X + player.velocity.X * framesToTravel));
            Vector2 vel = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1));
            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vel.X, vel.Y, ProjectileID.BulletDeadeye, npc.damage / 2, 0f, Main.myPlayer);
        }
        private void Fly(float speed, Vector2 position)
        {
            float toY = position.Y - npc.Center.Y;
            float toX = position.X - npc.Center.X;
            if (npc.velocity.X < toX)
            {
                npc.velocity.X = npc.velocity.X + speed;
                if (npc.velocity.X < 0f && toX > 0f)
                {
                    npc.velocity.X = npc.velocity.X + speed;
                }
            }
            else if (npc.velocity.X > toX)
            {
                npc.velocity.X = npc.velocity.X - speed;
                if (npc.velocity.X > 0f && toX < 0f)
                {
                    npc.velocity.X = npc.velocity.X - speed;
                }
            }
            if (npc.velocity.Y < toY)
            {
                npc.velocity.Y = npc.velocity.Y + speed;
                if (npc.velocity.Y < 0f && toY > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + speed;
                }
            }
            else if (npc.velocity.Y > toY)
            {
                npc.velocity.Y = npc.velocity.Y - speed;
                if (npc.velocity.Y > 0f && toY < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - speed;
                }
            }
        }
    }
}
