using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.Azana
{
    public class AzanaSlice : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.penetrate = 1;

            projectile.tileCollide = true;
            projectile.hostile = true;

            projectile.timeLeft = 220;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Slice");
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            if (projectile.localAI[0] > 30)
            {
                Texture2D tex = Main.projectileTexture[projectile.type];
                Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, projectile.height * 0.5f);
                for (int k = 0; k < projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                    Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                    Rectangle rectangle = new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type]);
                    sb.Draw(tex, drawPos, rectangle, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] < 30f)
            {
                NPC parent = Main.npc[(int)projectile.ai[1]];

                    int distance = (int)MathHelper.Clamp(projectile.localAI[0] * 30 ,0, 600);
                    double rad = projectile.ai[0] * (Math.PI / 180);
                    projectile.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - projectile.width / 2;
                    projectile.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - projectile.height / 2;
                
                if (!parent.active)
                {
                    projectile.Kill();
                }
            }
            else if (projectile.localAI[0] == 30f)
            {
               /* float speed = 15f;
                double angle = Math.Atan2(Main.player[Main.myPlayer].position.Y - projectile.position.Y, Main.player[Main.myPlayer].position.X - projectile.position.X);
                projectile.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * speed;*/
            }
            else if (projectile.localAI[0] > 30f)
            {
                if (Main.rand.NextBool(3))
                {
                    int dust1 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 127, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[dust1].noGravity = true;
                    Main.dust[dust1].velocity *= 0f;
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 27);
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 135, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}