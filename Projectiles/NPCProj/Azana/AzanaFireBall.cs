using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Azana
{
    public class AzanaFireBall : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.alpha = 0;
            projectile.timeLeft = 400;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Azana");
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(mod.BuffType("ChaosBurn"), 180);
        }
        public override void AI()
        {
            projectile.localAI[1] += 1f;

            if (projectile.localAI[1] <= 30f)
            {
                projectile.velocity.Y -= 0.15f;
                projectile.velocity.X *= 0.99f;
            }
            if (projectile.localAI[1] >= 30f && projectile.localAI[1] < 150f)
            {
                projectile.velocity.Y *= 0.99f;
                projectile.velocity.X *= 0.99f;
            }
            if (projectile.localAI[1] == 150f)
            {
                if (Main.rand.Next(2) == 0)
                {
                    Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 8);
                }
                double angle = Math.Atan2(Main.player[Main.myPlayer].position.Y - projectile.position.Y, Main.player[Main.myPlayer].position.X - projectile.position.X);
                projectile.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 10;
            }
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 4f)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (Main.rand.Next(4) == 0)
                    {
                        int dust1 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 127, 0f, 0f, 100, default(Color), 1.5f);
                        Main.dust[dust1].noGravity = true;
                        Main.dust[dust1].velocity *= 0f;
                        int dust2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 127, 0f, 0f, 100, default(Color), 1.5f);
                        Main.dust[dust2].noGravity = true;
                        Main.dust[dust2].velocity *= 0f;
                    }
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.HostileExplosion(projectile, 127);
        }
    }
}