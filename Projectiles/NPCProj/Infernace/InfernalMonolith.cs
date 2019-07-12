using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Infernace
{
    public class InfernalMonolith : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 52;
            projectile.height = 80;

            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.hostile = true;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernal Monolith");
        }
        public override void AI()
        {
            projectile.localAI[0]++;
            if (projectile.localAI[0] < 15)
            {
                projectile.position.Y -= 80 / 15;
            }
            if (projectile.localAI[0] > 120)
            {
                projectile.position.Y += 80 / 30;
            }
            if (projectile.localAI[0] > 150)
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
            if (projectile.localAI[0] < 20)
            {
                target.velocity.Y -= 30;
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