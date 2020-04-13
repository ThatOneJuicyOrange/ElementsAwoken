using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.TheGuardian
{
    public class GuardianFireball : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.penetrate = 1;

            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;

            projectile.alpha = 0;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Guardian");
            Main.projFrames[projectile.type] = 4;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.5f, 0.0f, 0.2f);
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;
            }
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1f);
            Main.dust[dust].velocity *= 0.3f;
            Main.dust[dust].fadeIn = 0.9f;
            Main.dust[dust].noGravity = true;

            if (Vector2.Distance(Main.player[Main.myPlayer].Center, projectile.Center) <= 30)
            {
                projectile.Kill();
            }
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.OnFire, 200);
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.HostileExplosion(projectile, 6);
        }
    }
}