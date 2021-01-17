using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.TheKeeper
{
    public class KeeperSpike : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 80;

            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.hostile = true;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernal Spike");
        }
        public override void AI()
        {
            projectile.ai[0]++;
            if (projectile.ai[0] < 0)
            {
                Dust stone = Main.dust[Dust.NewDust(projectile.position, projectile.width, 6, 232)];
                stone.velocity.Y = Main.rand.NextFloat(-4, -2);
                stone.velocity.X *= 0.3f;
            }
            else if (projectile.ai[0] == 0)
            {
                Main.PlaySound(SoundID.Item10, projectile.position);
            }
            else if (projectile.ai[0] < 15)
            {
                projectile.position.Y -= projectile.height / 15;
            }
            else if(projectile.ai[0] > 120)
            {
                projectile.position.Y += projectile.height / 30;
            }
            if(projectile.ai[0] > 150)
            {
                projectile.Kill();
            }

            
        }
        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
        {
            drawCacheProjsBehindNPCsAndTiles.Add(index);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (projectile.ai[0] < 20)
            {
                target.velocity.Y -= 5;
                target.AddBuff(BuffID.OnFire, 300);
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust fire = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f)];
                fire.noGravity = true;
            }
            for (int k = 0; k < 20; k++)
            {
                Dust stone = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 232)];
            }
        }
    }
}