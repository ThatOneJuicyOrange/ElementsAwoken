using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Aqueous
{
    public class WaterKnifeHoming : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.alpha = 0;
            projectile.timeLeft = 150;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aqueous");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            projectile.localAI[0]++;

            if (Vector2.Distance(Main.player[Main.myPlayer].Center, projectile.Center) <= 300)
            {
                if (projectile.localAI[0] >= 30f && projectile.localAI[0] <= 90f)
                {
                    int speed = 6;
                    double angle = Math.Atan2(Main.player[Main.myPlayer].position.Y - projectile.position.Y, Main.player[Main.myPlayer].position.X - projectile.position.X);
                    projectile.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * speed;
                }
            }
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 111);
            Main.dust[dust].velocity *= 0.1f;
            Main.dust[dust].scale *= 1.5f;
            Main.dust[dust].noGravity = true;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            projectile.Kill();
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
            return false;
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.HostileExplosion(projectile, 15);
        }
    }
}