using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Minions
{
    public class EnergyStorm : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 54;
            projectile.height = 28;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.sentry = true;

            projectile.timeLeft = Projectile.SentryLifeTime + 300; // 7200, 300 so it doesnt vanish and can be faded out
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            projectile.penetrate = -1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energy Storm");
            Main.projFrames[projectile.type] = 6;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6) 
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 5)
                    projectile.frame = 0;
            }
            return true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void AI()
        {
            int num1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 15, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            Main.dust[num1].noGravity = true;
            Main.dust[num1].velocity *= 0.9f;
        
            if (projectile.timeLeft <= 300f || (ProjectileUtils.CountProjectiles(projectile.type,projectile.owner) > 1 && ProjectileUtils.HasLeastTimeleft(projectile.whoAmI)))
            {
                projectile.alpha += 5;
                if (projectile.alpha > 255)
                {
                    projectile.alpha = 255;
                    projectile.Kill();
                }
            }

            projectile.localAI[1]--;
            if (projectile.owner == Main.myPlayer)
            {
                float max = 400f;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(projectile.Center, nPC.Center) <= max)
                    {
                        float Speed = 9f;
                        float rotation = (float)Math.Atan2(projectile.Center.Y - nPC.Center.Y, projectile.Center.X - nPC.Center.X);
                        if (projectile.localAI[1] <= 0)
                        {
                            Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                            //Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 12);
                            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, speed.X, speed.Y, mod.ProjectileType("LightningBlast"), projectile.damage, projectile.knockBack, projectile.owner);
                            projectile.localAI[1] = Main.rand.Next(16,60);
                        }
                    }
                }
            }
        }

    }
}