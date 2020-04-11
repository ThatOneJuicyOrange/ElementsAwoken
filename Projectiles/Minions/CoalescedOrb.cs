using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Minions
{
    public class CoalescedOrb : ModProjectile
    {
        public bool blinking = false;
        public int shootTimer = 30;
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.minionSlots = 3f;
            projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            //aiType = 388;
            projectile.aiStyle = 62;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Coalesced Orb");
            Main.projFrames[projectile.type] = 7;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            if (blinking)
            {
                projectile.frameCounter++;
                if (projectile.frameCounter >= 3)
                {
                    projectile.frame++;
                    projectile.frameCounter = 0;
                    if (projectile.frame > 6)
                    {
                        projectile.frame = 0;
                        blinking = false;
                    }
                }
            }
            else
            {
                projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            projectile.rotation += projectile.velocity.X * 0.04f;
            bool flag64 = projectile.type == mod.ProjectileType("CoalescedOrb");
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(mod.BuffType("CoalescedOrbBuff"), 3600);
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.coalescedOrb = false;
                }
                if (modPlayer.coalescedOrb)
                {
                    projectile.timeLeft = 2;
                }
            }
            if (Main.rand.Next(200) == 0)
            {
                blinking = true;
            }
            ProjectileUtils.PushOtherEntities(projectile);

            shootTimer--;
            if (shootTimer <= 15 && shootTimer >= 0)
            {
                int maxdusts = 20;
                for (int i = 0; i < maxdusts; i++)
                {
                    float dustDistance = 50;
                    float dustSpeed = 6;
                    Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                    Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                    Dust vortex = Dust.NewDustPerfect(projectile.Center + offset, 64, velocity, 0, default(Color), 1.5f);
                    vortex.noGravity = true;
                }
            }
            if (shootTimer <= 0)
            {
                if (Main.rand.Next(2) == 0)
                {
                    int num1 = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 64, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
                    Main.dust[num1].noGravity = true;
                    Main.dust[num1].velocity *= 0.9f;
                }
            }
            float maxDist = 400f;
            float shootSpeed = 15f;
            Vector2 targetPos = projectile.position;
            float targetDist = maxDist;
            bool target = false;
            projectile.tileCollide = true;
            for (int k = 0; k < 200; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.CanBeChasedBy(this, false))
                {
                    float distance = Vector2.Distance(npc.Center, projectile.Center);
                    if ((distance < targetDist || !target) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height))
                    {
                        targetDist = distance;
                        targetPos = npc.Center;
                        target = true;
                    }
                }
            }
            if (target && shootTimer <= 0)
            {
                if ((targetPos - projectile.Center).X > 0f)
                {
                    projectile.spriteDirection = (projectile.direction = -1);
                }
                else if ((targetPos - projectile.Center).X < 0f)
                {
                    projectile.spriteDirection = (projectile.direction = 1);
                }
                if (Main.myPlayer == projectile.owner)
                {
                    Vector2 shootVel = targetPos - projectile.Center;
                    if (shootVel == Vector2.Zero)
                    {
                        shootVel = new Vector2(0f, 1f);
                    }
                    shootVel.Normalize();
                    shootVel *= shootSpeed;
                    Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 8);
                    int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel.X, shootVel.Y, mod.ProjectileType("CoalescedBolt"), 230, projectile.knockBack, Main.myPlayer, 0f, 0f);
                    Main.projectile[proj].timeLeft = 300;
                    Main.projectile[proj].netUpdate = true;
                    projectile.netUpdate = true;
                }
                shootTimer = 30 + Main.rand.Next(0, 30);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.penetrate == 0)
            {
                projectile.Kill();
            }
            return false;
        }
    }
}