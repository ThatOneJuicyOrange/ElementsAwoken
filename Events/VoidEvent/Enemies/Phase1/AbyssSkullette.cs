using ElementsAwoken.NPCs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Events.VoidEvent.Enemies.Phase1
{
    public class AbyssSkullette : ModNPC
    {
        private float rotation
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float parentWhoAmI
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
            npc.width = 14;
            npc.height = 14;

            npc.damage = 50;
            npc.defense = 10;
            npc.lifeMax = 200;
            npc.knockBackResist = 0.25f;

            npc.HitSound = SoundID.NPCHit2;

            npc.noGravity = true;
            npc.noTileCollide = true;

            npc.buffImmune[24] = true;

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyss Skullette");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 300;
            npc.defense = 14;
            npc.damage = 100;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 700;
                npc.defense = 40;
                npc.damage = 200;
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = NPCsGLOBAL.ReducePierceDamage(damage, projectile);
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int l = 0; l < 6; l++)
                {
                    Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 54)];
                    dust.velocity.X = hitDirection * 2;
                    dust.color = new Color(120, 120, 255);
                }
            }
        }
        public override void AI()
        {
            npc.TargetClosest(true);

            Player P = Main.player[npc.target];

            if (npc.ai[1] != 0)
            {
                NPC parent = Main.npc[(int)npc.ai[1] - 1];

                if (parent.active)
                {
                    npc.ai[0] += 1.5f; // speed
                    float distance = parent.width * 1.1f;
                    double rad = npc.ai[0] * (Math.PI / 180); // angle to radians
                    npc.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - npc.width / 2;
                    npc.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - npc.height / 2;

                    npc.spriteDirection = parent.spriteDirection;
                    if (MyWorld.awakenedMode)
                    {
                        npc.immortal = true;
                        npc.dontTakeDamage = true;
                    }
                }
                else
                {
                    npc.ai[1] = 0;
                }
            }
            else
            {
                npc.immortal = false;
                npc.dontTakeDamage = false;
                npc.noTileCollide = false;

                npc.spriteDirection = Math.Sign(npc.velocity.X);
                npc.rotation = npc.velocity.X * 0.2f;
                Move(P, 0.15f, P.Center);

                //STOP CLUMPING FOOLS
                for (int k = 0; k < Main.npc.Length; k++)
                {
                    NPC other = Main.npc[k];
                    if (k != npc.whoAmI && other.type == npc.type && other.active && Math.Abs(npc.position.X - other.position.X) + Math.Abs(npc.position.Y - other.position.Y) < npc.width)
                    {
                        const float pushAway = 0.05f;
                        if (npc.position.X < other.position.X)
                        {
                            npc.velocity.X -= pushAway;
                        }
                        else
                        {
                            npc.velocity.X += pushAway;
                        }
                        if (npc.position.Y < other.position.Y)
                        {
                            npc.velocity.Y -= pushAway;
                        }
                        else
                        {
                            npc.velocity.Y += pushAway;
                        }
                    }
                }
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
                npc.velocity.Y = npc.velocity.Y + speed * 0.3f;
                if (npc.velocity.Y < 0f && desiredVelocity.Y > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + speed * 0.3f;
                    return;
                }
            }
            else if (npc.velocity.Y > desiredVelocity.Y)
            {
                npc.velocity.Y = npc.velocity.Y - speed * 0.3f;
                if (npc.velocity.Y > 0f && desiredVelocity.Y < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - speed * 0.3f;
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