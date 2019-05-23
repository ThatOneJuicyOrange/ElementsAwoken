using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Minions
{
    public class Demon : ModProjectile
    {
        public int shootTimer = 30;

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;

            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            projectile.penetrate = -1;
            projectile.minion = true;
            //aiType = 317;
            projectile.aiStyle = 62;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demon");
            Main.projFrames[projectile.type] = 2;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        { 
            projectile.frameCounter++;
            if (projectile.frameCounter >= 12)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 1)
                    projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            projectile.rotation += projectile.velocity.X * 0.04f;
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);

            if (player.dead)
            {
                projectile.active = false;
            }
            if (modPlayer.superbaseballDemon)
            {
                projectile.timeLeft = 2;
            }
            else
            {
                projectile.active = false;
            }

            shootTimer--;
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
                    int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel.X, shootVel.Y, ProjectileID.DemonScythe, 20, projectile.knockBack, Main.myPlayer, 0f, 0f);
                    Main.projectile[proj].timeLeft = 300;
                    Main.projectile[proj].netUpdate = true;
                    projectile.netUpdate = true;
                }
                shootTimer = 40;
            }

            /*float max = 400f;
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && !nPC.dontTakeDamage && Vector2.Distance(projectile.Center, nPC.Center) <= max && Main.npc[i].CanBeChasedBy(projectile, false))
                {
                    Vector2 vector8 = new Vector2(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2));
                    int type = ProjectileID.DemonScythe;
                    float Speed = 12f;
                    int damage = projectile.damage / 3;
                    if (shootTimer <= 0)
                    {
                        for (int l = 0; l < numberProjectiles; l++)
                        {
                            Projectile.NewProjectile(vector8.X, vector8.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 0f, Main.myPlayer, 0f, 0f);
                        }
                        shootTimer = 30;
                    }
                }
            }*/
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