using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class LifesAura : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 170;
            projectile.height = 170;

            projectile.alpha = 150;

            projectile.penetrate = -1;

            projectile.tileCollide = false;

            projectile.timeLeft = 3600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Life's Aura");
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            int maxDist = 85;
            for (int i = 0; i < 30; i++)
            {
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                Dust dust = Main.dust[Dust.NewDust(projectile.Center + offset - Vector2.One * 4, 0, 0, 75, 0, 0, 100)];
                dust.noGravity = true;
            }
            projectile.ai[0]++;
            if (ProjectileUtils.CountProjectiles(projectile.type, projectile.owner) > 1)
            {
                if (ProjectileUtils.HasLeastTimeleft(projectile.whoAmI))
                {
                    projectile.alpha += 5;
                    if (projectile.alpha >= 255)
                    {
                        projectile.Kill();
                    }
                }
            }
            if (projectile.timeLeft < 60)
            {
                projectile.alpha += 5;
                if (projectile.alpha >= 255)
                {
                    projectile.Kill();
                }
            }
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player loopP = Main.player[i];
                if (loopP.active && !loopP.dead)
                {
                    float dist = Vector2.Distance(projectile.Center, loopP.Center);
                    if (dist < maxDist)
                    {
                        if (projectile.ai[0] % 10 == 0 && loopP.statLife < loopP.statLifeMax2 && loopP.team == player.team)
                        {
                            int amount = Main.rand.Next(3, 9);
                            loopP.statLife += amount;
                            loopP.HealEffect(amount, true);
                        }
                    }
                }
            }
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && nPC.townNPC)
                {
                    float dist = Vector2.Distance(projectile.Center, nPC.Center);
                    if (dist < maxDist)
                    {
                        if (projectile.ai[0] % 10 == 0 && nPC.life < nPC.life)
                        {
                            int amount = Main.rand.Next(3, 9);
                            nPC.life += amount;
                            nPC.HealEffect(amount, true);
                        }
                    }
                }
            }
        }
    }
}