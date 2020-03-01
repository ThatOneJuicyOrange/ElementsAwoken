using ElementsAwoken.NPCs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Events.VoidEvent.Enemies.Phase1
{
    public class VoidFly : ModNPC
    {
        private float mode
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float parentWhoAmI
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float shootTimer
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float flyTimer
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 10;
            npc.height = 12;

            npc.aiStyle = -1;

            npc.damage = 150;
            npc.defense = 35;
            npc.lifeMax = 600;
            npc.knockBackResist = 0.25f;

            npc.npcSlots = 0.5f;

            npc.value = Item.buyPrice(0, 0, 20, 0);
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.noGravity = true;

            npc.buffImmune[24] = true;

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Fly");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = NPCsGLOBAL.ReducePierceDamage(damage, projectile);
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 900;
            npc.defense = 50;
            npc.damage = 200;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 1500;
                npc.defense = 65;
            }
        }
        public override void NPCLoot()
        {
            if (mode == 0)
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
            if (npc.frame.Y > frameHeight * 3)  // so it doesnt go over
            {
                npc.frame.Y = 0;
            }
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            Player P = Main.player[npc.target];

            if (mode == 0)
            {
                if (npc.localAI[0] == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    shootTimer = 90;
                    int numFlies = Main.expertMode ? MyWorld.awakenedMode ? 12 : 8 : 5;
                    for (int l = 0; l < numFlies; l++)
                    {
                        NPC fly = Main.npc[NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-20, 20), (int)npc.Center.Y + Main.rand.Next(-30, 30), mod.NPCType("VoidFly"),npc.whoAmI,1,npc.whoAmI, 240 + (l * 90))]; 
                    }
                    npc.localAI[0]++;
                }
                bool anyFlyChildren = false;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC other = Main.npc[i];
                    if (other.type == npc.type && other.ai[1] == npc.whoAmI && other.active && other.whoAmI != npc.whoAmI)
                    {
                        anyFlyChildren = true;
                    }
                }
                if (!anyFlyChildren)
                {
                    shootTimer--;
                    if (shootTimer <= 0)
                    {
                        Vector2 toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
                        toTarget.Normalize();
                        npc.velocity = toTarget * 10;
                        mode = 2;
                    }
                }
                else
                {
                    Move(P, 0.015f, P.Center - new Vector2(0, 150));
                }
            }
            else if (mode == 1)
            {
                NPC parent = Main.npc[(int)parentWhoAmI];

                if (!parent.active || Collision.CanHit(npc.position, npc.width, npc.height, P.position, P.width, P.height) && Vector2.Distance(npc.Center, P.Center) < 900)
                {
                    shootTimer--;
                    if (shootTimer <= 0)
                    {
                        Vector2 toTarget = new Vector2(P.Center.X - npc.Center.X, P.Center.Y - npc.Center.Y);
                        toTarget.Normalize();
                        npc.velocity = toTarget * 10;
                        mode = 2;
                    }
                }
                Vector2 diff = parent.position - parent.oldPosition;
                npc.position += diff;
                Move(P, 0.01f, parent.Center);
            }
            else
            {
                flyTimer++;
                int boomHitboxSize = 20;
                if (flyTimer > 90 || (Collision.SolidCollision(npc.Center - Vector2.One * (boomHitboxSize / 2), boomHitboxSize, boomHitboxSize) || Vector2.Distance(npc.Center, P.Center) < 20))
                {
                    Explosion(P);
                    npc.active = false;
                }
            }


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
            NPCsGLOBAL.GoThroughPlatforms(npc);
        }
        private void Explosion(Player player)
        {
            Projectile exp = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("ExplosionHostile"), 120, 5f, player.whoAmI, 0f, 0f)];
            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 14);
            int num = ModContent.GetInstance<Config>().lowDust ? 10 : 20;
            for (int i = 0; i < num; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 31, 0f, 0f, 100, default(Color), 1.5f)];
                dust.velocity *= 1.4f;
            }
            int num2 = ModContent.GetInstance<Config>().lowDust ? 5 : 10;
            for (int i = 0; i < num2; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 127, 0f, 0f, 100, default(Color), 2.5f)];
                dust.noGravity = true;
                dust.velocity *= 5f;
                dust = Main.dust[Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 127, 0f, 0f, 100, default(Color), 1.5f)];
                dust.velocity *= 3f;
            }
            int num373 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore85 = Main.gore[num373];
            gore85.velocity.X = gore85.velocity.X + 1f;
            Gore gore86 = Main.gore[num373];
            gore86.velocity.Y = gore86.velocity.Y + 1f;
            num373 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore87 = Main.gore[num373];
            gore87.velocity.X = gore87.velocity.X - 1f;
            Gore gore88 = Main.gore[num373];
            gore88.velocity.Y = gore88.velocity.Y + 1f;
            num373 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore89 = Main.gore[num373];
            gore89.velocity.X = gore89.velocity.X + 1f;
            Gore gore90 = Main.gore[num373];
            gore90.velocity.Y = gore90.velocity.Y - 1f;
            num373 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore91 = Main.gore[num373];
            gore91.velocity.X = gore91.velocity.X - 1f;
            Gore gore92 = Main.gore[num373];
            gore92.velocity.Y = gore92.velocity.Y - 1f;
        }
        private void Move(Player P, float speed, Vector2 target)
        {
            Vector2 desiredVelocity = target - npc.Center;
            if (Main.expertMode) speed *= 1.1f;
            if (MyWorld.awakenedMode) speed *= 1.1f;
            if (Vector2.Distance(target, npc.Center) > 300) speed *= 2f;
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
            float slowSpeed = Main.expertMode ? 0.96f : 0.98f;
            if (MyWorld.awakenedMode) slowSpeed = 0.94f;
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