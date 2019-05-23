using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.Obsidious.Human
{
    public class ObsidiousFireCrystalStationary : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
            projectile.timeLeft = 220;
            drawOffsetX = -10;
            drawOriginOffsetY = -10;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidious Fire Crystal");
            Main.projFrames[projectile.type] = 2;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            if (projectile.localAI[0] <= 30)
            {
                projectile.frame = 0;
            }
            else
            {
                projectile.frame = 1;
            }
            return true;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] == 30f)
            {
                float speed = 15f;
                double angle = Math.Atan2(Main.player[Main.myPlayer].position.Y - projectile.position.Y, Main.player[Main.myPlayer].position.X - projectile.position.X);
                projectile.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * speed;
                Main.PlaySound(SoundID.DD2_SonicBoomBladeSlash, projectile.position);
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 6, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}