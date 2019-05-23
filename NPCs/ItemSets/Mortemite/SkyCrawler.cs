using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.ItemSets.Mortemite
{
    public class SkyCrawler : ModNPC
    {
        public override void SetDefaults()
        {
            npc.npcSlots = 0.5f;
            npc.damage = 200;
            npc.width = 26; //324
            npc.height = 20; //216
            npc.defense = 15;
            npc.lifeMax = 1000;
            npc.knockBackResist = 0.65f;
            npc.value = Item.buyPrice(0, 1, 0, 0);
            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath52;
            banner = npc.type;
            bannerItem = mod.ItemType("SkyCrawlerBanner");
            npc.noGravity = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sky Crawler");
            Main.npcFrameCount[npc.type] = 7;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        { 
            return (spawnInfo.player.ZoneSkyHeight) &&
            !spawnInfo.player.ZoneTowerStardust &&
            !spawnInfo.player.ZoneTowerSolar &&
            !spawnInfo.player.ZoneTowerVortex &&
            !spawnInfo.player.ZoneTowerNebula &&
            !spawnInfo.playerInTown &&
            !Main.snowMoon && !Main.pumpkinMoon && NPC.downedMoonlord ? 0.05f : 0f;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            ++npc.frameCounter;
            if (npc.frameCounter >= 16.0)
                npc.frameCounter = 0.0;
            npc.frame.Y = frameHeight * (int)(npc.frameCounter / 4.0);
        }
        public override void NPCLoot()
        {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MortemiteDust"), Main.rand.Next(1, 2)); //Item spawn
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];

            Lighting.AddLight(npc.Center, 0.3f, 0.3f, 0.3f);
            if (npc.direction == 0)
            {
                npc.TargetClosest(true);
            }
            /*if (!npc.wet)
            {
                npc.rotation += npc.velocity.X * 0.1f;
                if (npc.velocity.Y == 0f)
                {
                    npc.velocity.X = npc.velocity.X * 0.98f;
                    if ((double)npc.velocity.X > -0.01 && (double)npc.velocity.X < 0.01)
                    {
                        npc.velocity.X = 0f;
                    }
                }
                npc.velocity.Y = npc.velocity.Y + 0.2f;
                if (npc.velocity.Y > 10f)
                {
                    npc.velocity.Y = 10f;
                }
                npc.ai[0] = 1f;
                return;
            }*/
            // on collision 
            if (npc.collideX)
            {
                npc.velocity.X = npc.velocity.X * -1f;
                npc.direction *= -1;
            }
            if (npc.collideY)
            {
                if (npc.velocity.Y > 0f)
                {
                    npc.velocity.Y = Math.Abs(npc.velocity.Y) * -1f;
                    npc.directionY = -1;
                    npc.ai[0] = -1f;
                }
                else if (npc.velocity.Y < 0f)
                {
                    npc.velocity.Y = Math.Abs(npc.velocity.Y);
                    npc.directionY = 1;
                    npc.ai[0] = 1f;
                }
            }
            npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f;
            npc.TargetClosest(false);
            if (!Main.player[npc.target].dead)
            {
                npc.velocity *= 0.98f;
                float num263 = 0.2f;
                if (npc.velocity.X > -num263 && npc.velocity.X < num263 && npc.velocity.Y > -num263 && npc.velocity.Y < num263)
                {
                    npc.TargetClosest(true);
                    float speed = 12f;
                    Vector2 vector31 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float targetX = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector31.X;
                    float targetY = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector31.Y;
                    float num267 = (float)Math.Sqrt((double)(targetX * targetX + targetY * targetY));
                    num267 = speed / num267;
                    targetX *= num267;
                    targetY *= num267;
                    npc.velocity.X = targetX;
                    npc.velocity.Y = targetY;
                    return;
                }
            }
            else
            {
                npc.active = false;
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 500;
            npc.lifeMax = 1200;
        }
    }
}