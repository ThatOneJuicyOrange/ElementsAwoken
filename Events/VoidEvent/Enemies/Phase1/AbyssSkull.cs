using ElementsAwoken.NPCs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Events.VoidEvent.Enemies.Phase1
{
    public class AbyssSkull : ModNPC
    {
        private float changeLocationTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float vectorX
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float vectorY
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
            npc.width = 28;
            npc.height = 26;

            npc.aiStyle = -1;

            npc.damage = 150;
            npc.defense = 35;
            npc.lifeMax = 1000;
            npc.knockBackResist = 0.25f;

            npc.value = Item.buyPrice(0, 0, 20, 0);
            npc.HitSound = SoundID.NPCHit2;
            npc.DeathSound = SoundID.NPCDeath2;

            npc.noGravity = true;

            npc.buffImmune[24] = true;

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyss Skull");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 2000;
            npc.defense = 50;
            npc.damage = 200;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 3000;
                npc.defense = 65;
                npc.damage = 300;
            }
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEssence"), 1);
            }
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidStone"), Main.rand.Next(3, 5));
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = NPCsGLOBAL.ReducePierceDamage(damage, projectile);
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            Player P = Main.player[npc.target];
            if (Vector2.Distance(P.Center, npc.Center) < 400) aiState = 1;
            else aiState = 0;

            npc.spriteDirection = Math.Sign(npc.velocity.X);
   
            if (aiState == 0)
            {
                changeLocationTimer--;
                if ((vectorX == 0 || vectorY == 0) || changeLocationTimer <= 0 || MathHelper.Distance(vectorX,P.Center.X) > 2000)
                {
                    float midX = (P.Center.X + npc.Center.X) / 2;
                    vectorX = midX + Main.rand.Next(-200,200);
                    if (Main.rand.Next(3) == 0) vectorX = npc.Center.X + Main.rand.Next(-200, 200);
                    vectorY = P.Center.Y + Main.rand.Next(-100,100);
                    changeLocationTimer = 190;
                    npc.netUpdate = true;
                }
                Vector2 targetLoc = new Vector2(vectorX, vectorY);
                Move(P, 0.015f, targetLoc);
                if (ModContent.GetInstance<Config>().debugMode)
                {
                    Dust dust = Main.dust[Dust.NewDust(targetLoc, 2, 2, 6)];
                    dust.noGravity = true;
                }
            }
            else
            {
                Dust dust = Main.dust[Dust.NewDust(npc.Center - new Vector2(0, -2) - Vector2.One * 4, 2, 2, 127)];
                dust.velocity = Vector2.Zero;
                dust.noGravity = true;
                dust.scale = 1f;

                Move(P, 0.05f, P.Center);
            }

            if (npc.localAI[0] == 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int numSkullettes = Main.expertMode ? MyWorld.awakenedMode ? 4 : 3 : 2;
                for (int l = 0; l < numSkullettes; l++)
                {
                    int distance = 360 / numSkullettes;
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AbyssSkullette"), npc.whoAmI, l * distance, npc.whoAmI + 1); //add one so that if no parent then we can check for ai[1] == 0 without risk of the parent being 0
                }
                npc.localAI[0]++;
            }
            NPCsGLOBAL.GoThroughPlatforms(npc);
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
                npc.velocity.Y = npc.velocity.Y + speed * 0.5f;
                if (npc.velocity.Y < 0f && desiredVelocity.Y > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + speed * 0.5f;
                    return;
                }
            }
            else if (npc.velocity.Y > desiredVelocity.Y)
            {
                npc.velocity.Y = npc.velocity.Y - speed * 0.5f;
                if (npc.velocity.Y > 0f && desiredVelocity.Y < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - speed * 0.5f;
                    return;
                }
            }
            float slowSpeed = Main.expertMode ? 0.97f : 0.99f;
            if (MyWorld.awakenedMode) slowSpeed = 0.96f;
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