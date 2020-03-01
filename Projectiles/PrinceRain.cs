using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class PrinceRain : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 18;

            projectile.melee = true;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.light = 1f;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 9;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            projectile.scale *= 0.5f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prince Rain");
            Main.projFrames[projectile.type] = 2;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            // to minimise the loops required and cause lag
            if (projectile.ai[0] == 0)
            {
                float num = 400f;
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, Main.npc[i].Center, 1, 1))
                    {
                        float num1 = Main.npc[i].position.X + (float)(Main.npc[i].width / 2);
                        float num2 = Main.npc[i].position.Y + (float)(Main.npc[i].height / 2);
                        float num3 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num1) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num2);
                        if (num3 < num)
                        {
                            num = num3;
                            projectile.ai[0] = Main.npc[i].whoAmI;
                        }
                    }
                }
            }
            else
            {
                NPC target = Main.npc[(int)projectile.ai[0]];
                if (target.active)
                {
                    float speed = 16f;
                    float num4 = target.Center.X - projectile.Center.X;
                    float num5 = target.Center.Y - projectile.Center.Y;
                    float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
                    num6 = speed / num6;
                    num4 *= num6;
                    num5 *= num6;
                    projectile.velocity.X = (projectile.velocity.X * 20f + num4) / 21f;
                    projectile.velocity.Y = (projectile.velocity.Y * 20f + num5) / 21f;
                }
                else
                {
                    projectile.ai[0] = 0;
                }
            }
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Rectangle rect = new Rectangle(0, 4, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height -4);
                if (k == 0) rect.Y -= 4;
                float scale = 1 - ((float)k / (float)projectile.oldPos.Length);
                sb.Draw(Main.projectileTexture[projectile.type], drawPos, rect, color, projectile.rotation, drawOrigin, scale * projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Ichor, 200);
        }
    }
}