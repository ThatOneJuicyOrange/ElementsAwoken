using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Azana
{
    public class InfectionMouth : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 40;

            npc.aiStyle = -1;

            npc.lifeMax = 5000;
            npc.damage =  75 ;
            npc.defense =  50;
            npc.knockBackResist = 0f;

            npc.npcSlots = 0f;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            NPCsGLOBAL.ImmuneAllEABuffs(npc);
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }

            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infection Mouth");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 7500;
            npc.damage = 150;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 10000;
                npc.damage = 300;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            Player P = Main.player[npc.target];

            float amount = MathHelper.Lerp(20, 0, MathHelper.Clamp(Vector2.Distance(npc.Center, P.Center) / 1000, 0, 1));
            npc.velocity.X += hitDirection * Main.rand.NextFloat(amount * 0.5f, amount * 1.1f);
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;

            npc.frameCounter++;
            if (npc.frameCounter > 6)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y >= frameHeight * 4)  // so it doesnt go over
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
            npc.TargetClosest(true);

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
            Move(P, 0.2f, P.Center);
            
            if (!ModContent.GetInstance<Config>().lowDust)
            {
                int dust = Dust.NewDust(npc.position, npc.width, npc.height, 127);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0.1f;
            }
            if (!NPC.AnyNPCs(mod.NPCType("Azana"))) npc.active = false;
        }
        private void Move(Player P, float speed, Vector2 target)
        {
            Vector2 desiredVelocity = target - npc.Center;
            if (Main.expertMode) speed *= 1.1f;
            if (MyWorld.awakenedMode) speed *= 1.1f;

            if (npc.velocity.X < desiredVelocity.X)
            {
                npc.velocity.X = npc.velocity.X + speed;
                if (npc.velocity.X < 0f && desiredVelocity.X > 0f)
                {
                    npc.velocity.X = npc.velocity.X + speed;
                }
            }
            else if (npc.velocity.X > desiredVelocity.X)
            {
                npc.velocity.X = npc.velocity.X - speed;
                if (npc.velocity.X > 0f && desiredVelocity.X < 0f)
                {
                    npc.velocity.X = npc.velocity.X - speed;
                }
            }
            if (npc.velocity.Y < desiredVelocity.Y)
            {
                npc.velocity.Y = npc.velocity.Y + speed;
                if (npc.velocity.Y < 0f && desiredVelocity.Y > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + speed;
                    return;
                }
            }
            else if (npc.velocity.Y > desiredVelocity.Y)
            {
                npc.velocity.Y = npc.velocity.Y - speed;
                if (npc.velocity.Y > 0f && desiredVelocity.Y < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - speed;
                    return;
                }
            }
            float slowSpeed = Main.expertMode ? 0.93f : 0.95f;
            if (MyWorld.awakenedMode) slowSpeed = 0.92f;
            int xSign = Math.Sign(desiredVelocity.X);
            if ((npc.velocity.X < xSign && xSign == 1) || (npc.velocity.X > xSign && xSign == -1)) npc.velocity.X *= slowSpeed;

            int ySign = Math.Sign(desiredVelocity.Y);
            if (MathHelper.Distance(target.Y, npc.Center.Y) > 1000)
            {
                if ((npc.velocity.X < ySign && ySign == 1) || (npc.velocity.X > ySign && ySign == -1)) npc.velocity.Y *= slowSpeed;
            }
        }
    }
}