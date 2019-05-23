using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles
{
    public class LightningCloud : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 54;
            projectile.height = 28;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 10000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Cloud");
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
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);

            projectile.Center = Main.player[projectile.owner].Center + new Vector2(0, -50);

            if (modPlayer.lightningCloudHidden || !modPlayer.lightningCloud)
            {
                projectile.Kill();
            }
        }
    }
}