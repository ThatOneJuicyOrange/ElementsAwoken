using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Obsidious
{
    public class ObsidiousHomingBall : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.alpha = 0;
            projectile.timeLeft = 150;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidious Homing Ball");
        }
        public override void AI()
        {
            projectile.localAI[1] += 1f;

            if (projectile.localAI[1] <= 45f)
            {
                if (projectile.localAI[0] >= 1 && projectile.localAI[0] < 200)
                {
                    double angle = Math.Atan2(Main.player[Main.myPlayer].position.Y - projectile.position.Y, Main.player[Main.myPlayer].position.X - projectile.position.X);
                    projectile.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 7f;
                }
            }
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 4f)
            {
                for (int i = 0; i < 5; i++)
                {
                    int dust1 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 62, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[dust1].noGravity = true;
                    Main.dust[dust1].velocity *= 0f;
                }
                for (int i = 0; i < 2; i++)
                {
                    int dust2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.PinkFlame, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[dust2].noGravity = true;
                    Main.dust[dust2].velocity *= 0f;
                }
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
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
            ProjectileUtils.HostileExplosion(projectile, 62, projectile.damage, soundID: 103);
        }
    }
}