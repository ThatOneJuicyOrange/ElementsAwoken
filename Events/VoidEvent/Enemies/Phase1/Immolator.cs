using ElementsAwoken.NPCs;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Events.VoidEvent.Enemies.Phase1
{
    public class Immolator : ModNPC
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
        private float shootTimer
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 26;
            npc.height = 20;

            npc.aiStyle = -1;

            npc.damage = 150;
            npc.defense = 35;
            npc.lifeMax = 1000;
            npc.knockBackResist = 0.25f;

            npc.npcSlots = 0.5f;

            npc.value = Item.buyPrice(0, 0, 20, 0);
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;

            npc.noGravity = true;
            npc.noTileCollide = true;

            npc.buffImmune[24] = true;

            banner = npc.type;
            bannerItem = mod.ItemType("ImmolatorBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Immolator");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = NPCsGLOBAL.ReducePierceDamage(damage, projectile);
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 175;
            npc.lifeMax = 1500;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 1500;
                npc.damage = 200;
                npc.defense = 50;
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
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;
            if (npc.frameCounter > 6)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y > frameHeight * 5)  // so it doesnt go over
            {
                npc.frame.Y = 0;
            }
        }

        public override void AI()
        {
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || P.dead || !P.active || Vector2.Distance(P.Center,npc.Center) > 2000) npc.TargetClosest(true);
            changeLocationTimer--;
            if (changeLocationTimer <= 0)
            {
                switch (Main.rand.Next(2))
                {
                    case 0:
                        vectorX = 400f + Main.rand.Next(-100, 100);
                        break;
                    case 1:
                        vectorX = -400f + Main.rand.Next(-100, 100);
                        break;
                    default: break;
                }
                changeLocationTimer = 180;
                npc.netUpdate = true;
            }
            Move(P, 0.05f, new Vector2(P.Center.X + vectorX, P.Center.Y - 200));           

            Vector2 direction = P.Center - npc.Center;
            npc.direction = Math.Sign(direction.X);
            npc.rotation = direction.ToRotation();
            if (direction.X < 0f) npc.rotation += 3.14f;

            shootTimer--;
            if (shootTimer <= 0)
            {
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 12);

                int damage = Main.expertMode ? MyWorld.awakenedMode ? 60 : 40 : 20;
                float Speed = 10f;
                float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                Projectile ball = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("ImmolatorBall"), damage, 0f, 0)];
                ball.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                shootTimer = 160;
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