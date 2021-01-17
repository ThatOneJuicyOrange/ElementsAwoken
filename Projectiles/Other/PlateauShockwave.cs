using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Other
{
    public class PlateauShockwave : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }

        private int rippleCount = 3;
        private int rippleSize = 15;
        private int rippleSpeed = 50;

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
            DisplayName.SetDefault("Shockwave");
        }
        public override void AI()
        {
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
                float distortStrength = 500;
                Filters.Scene["Shockwave"].GetShader().UseProgress(progress).UseOpacity(distortStrength * (1 - progress / 3f));
            }
        }
        public override void Kill(int timeLeft)
        {
            Filters.Scene["Shockwave"].Deactivate();
        }
    }
}