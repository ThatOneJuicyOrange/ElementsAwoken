using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Ancients.Gores
{
    public class DeathShockwave : ModProjectile
    {
        public bool[] hasPushed = new bool[255];

        private int rippleCount = 3;
        private int rippleSize = 15;
        private int rippleSpeed = 15;
        private float distortStrength = 300f;


        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.tileCollide = false;

            projectile.timeLeft = 400;
            projectile.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void AI()
        {
            projectile.ai[0] += 8;
            /*for (int i = 0; i < 120; i++)
            {
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                Vector2 offset = new Vector2((float)Math.Sin(angle) * distance, (float)Math.Cos(angle) * distance);
                Dust dust = Main.dust[Dust.NewDust(projectile.Center + offset, 0, 0, 63, 0, 0, 100)];
                dust.noGravity = true;
            }*/
            Player player = Main.player[Main.myPlayer];
            if (!hasPushed[player.whoAmI] && Vector2.Distance(player.Center, projectile.Center) < projectile.ai[0])
            {
                Vector2 toTarget = new Vector2(projectile.Center.X - player.Center.X, projectile.Center.Y - player.Center.Y);
                toTarget.Normalize();
                player.velocity -= toTarget * 15f;
                hasPushed[player.whoAmI] = true;
            }
            if (projectile.ai[1] == 0)
            {
                projectile.ai[1] = 1;
                if (!Filters.Scene["Shockwave"].IsActive())
                {
                    Filters.Scene.Activate("Shockwave", projectile.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(projectile.Center);
                }
            }
            else
            {
                projectile.ai[1]++;
                float progress = projectile.ai[1] / 60f;
                float distortStrength = 200;
                Filters.Scene["Shockwave"].GetShader().UseProgress(progress).UseOpacity(distortStrength * (1 - progress / 3f));
            }
        }
        public override void Kill(int timeLeft)
        {
            Filters.Scene["Shockwave"].Deactivate();
        }
    }
}