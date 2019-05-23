using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.TheCelestial
{
    public class AstraMinion : ModNPC
    {
        public float vectorX = 0f;
        public float vectorY = 0f;
        public int changeLocationTimer = 0;
        float speed = 0.1f;
        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 40;

            npc.lifeMax = NPC.downedMoonlord ? 7500 : 4000;
            npc.damage = NPC.downedMoonlord ? 75 : 40;
            npc.defense = NPC.downedMoonlord ? 50 : 30;
            npc.knockBackResist = 0f;

            npc.npcSlots = 0f;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[24] = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astra's Gazer");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = NPC.downedMoonlord ? 12500 : 7000;
            npc.damage = NPC.downedMoonlord ? 125 : 75;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = NPC.downedMoonlord ? 17500 : 10000;
                npc.damage = NPC.downedMoonlord ? 140 : 90;
                npc.defense = NPC.downedMoonlord ? 60 : 35;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            Vector2 direction = P.Center - npc.Center;
            if (direction.X > 0f)
            {
                npc.spriteDirection = 1;
                npc.rotation = direction.ToRotation();
            }
            if (direction.X < 0f)
            {
                npc.spriteDirection = -1;
                npc.rotation = direction.ToRotation() - 3.14f;
            }
            npc.ai[1]++;
            if (npc.ai[1] < 600)
            {
                if (npc.ai[2] == 0)
                {
                    int minDist = 150;
                    vectorX = Main.rand.Next(-500, 500);
                    vectorY = Main.rand.Next(-500, 500);
                    if (vectorX < minDist && vectorX > 0)
                    {
                        vectorX = minDist;
                    }
                    else if (vectorX > -minDist && vectorX < 0)
                    {
                        vectorX = -minDist;
                    }
                    if (vectorY < minDist && vectorY > 0)
                    {
                        vectorY = minDist;
                    }
                    else if (vectorY > -minDist && vectorX < 0)
                    {
                        vectorY = -minDist;
                    }
                    npc.ai[2]++;
                    speed = Main.rand.NextFloat(0.15f, 0.25f);
                }
                // movement
                npc.TargetClosest(true);
                npc.spriteDirection = npc.direction;
                Vector2 vector75 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float targetX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) + vectorX - vector75.X + Main.rand.Next(-25, 25);
                float targetY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) + vectorY - vector75.Y + Main.rand.Next(-25, 25);
                if (npc.velocity.X < targetX)
                {
                    npc.velocity.X = npc.velocity.X + speed * 2;
                }
                else if (npc.velocity.X > targetX)
                {
                    npc.velocity.X = npc.velocity.X - speed * 2;
                }
                if (npc.velocity.Y < targetY)
                {
                    npc.velocity.Y = npc.velocity.Y + speed;
                    if (npc.velocity.Y < 0f && targetY > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + speed;
                        return;
                    }
                }
                else if (npc.velocity.Y > targetY)
                {
                    npc.velocity.Y = npc.velocity.Y - speed;
                    if (npc.velocity.Y > 0f && targetY < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y - speed;
                        return;
                    }
                }
            }
            else
            {
                npc.ai[0]++;

                float speed = 10f;
                float num25 = P.Center.X - npc.Center.X;
                float num26 = P.Center.Y - npc.Center.Y;
                float num27 = (float)Math.Sqrt(num25 * num25 + num26 * num26);
                num27 = speed / num27;
                npc.velocity.X = num25 * num27;
                npc.velocity.Y = num26 * num27;
                if (npc.ai[0] >= 20)
                {
                    npc.ai[1] = 0;
                    npc.ai[2] = 0;
                }
                if (Vector2.Distance(P.Center, npc.Center) < 10)
                {
                    npc.ai[1] = 0;
                    npc.ai[2] = 0;
                }
            }
            if (Main.player[npc.target].dead || !NPC.AnyNPCs(mod.NPCType("TheCelestial")))
            {
                npc.active = false;
            }

            for (int i = 0; i < 2; i++)
            {
                int dustType = 6;
                switch (Main.rand.Next(4))
                {
                    case 0:
                        dustType = 6;
                        break;
                    case 1:
                        dustType = 197;
                        break;
                    case 2:
                        dustType = 229;
                        break;
                    case 3:
                        dustType = 242;
                        break;
                    default: break;
                }
                int dust = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0.1f;
            }
        }      
    }
}