using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.TheGuardian
{
    public class GuardianProbe : ModNPC
    {
        public float vectorX = 0f;
        public float vectorY = 0f;
        public int changeLocationTimer = 0;
        float speed = 0.1f;
        public override void SetDefaults()
        {
            npc.width = 46;
            npc.height = 52;

            npc.damage = 60;
            npc.defense = 20;
            npc.lifeMax = 1500;
            npc.knockBackResist = 0f;

            npc.npcSlots = 0f;

            npc.HitSound = SoundID.NPCHit2;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.buffImmune[24] = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Temple Guard");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 3000;
            npc.damage = 75;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 4500;
                npc.damage = 85;
                npc.defense = 30;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            if (npc.frameCounter > 5)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y > frameHeight * 3)
            {
                npc.frame.Y = 0;
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
                npc.spriteDirection = -1;
                npc.rotation = direction.ToRotation();
            }
            if (direction.X < 0f)
            {
                npc.spriteDirection = 1;
                npc.rotation = direction.ToRotation() - 3.14f;
            }
            npc.ai[1]--;
            if (npc.ai[1] == 0)
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
                npc.ai[1] = 200;
                speed = Main.rand.NextFloat(0.05f, 0.1f);
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
            if (Main.rand.Next(200) == 0)
            {
                float Speed = 12f;
                int damage = 60;
                float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("GuardianShot"), damage, 0f, 0, npc.ai[2]);
            }
            if (Main.player[npc.target].dead || !NPC.AnyNPCs(mod.NPCType("TheGuardianFly")))
            {
                npc.active = false;
            }

            for (int i = 0; i < 5; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(npc.Center, 4, 4, 6)];
                dust.velocity = Vector2.Zero;
                dust.position -= npc.velocity / 6f * (float)i;
                dust.noGravity = true;
            }
        }      
    }
}