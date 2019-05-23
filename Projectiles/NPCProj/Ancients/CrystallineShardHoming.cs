using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Ancients
{
    public class CrystallineShardHoming : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;

            projectile.penetrate = -1;

            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;

            projectile.timeLeft = 120;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Amalgamate");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            Player player = Main.player[Main.myPlayer];
            projectile.localAI[0]++;
            if (projectile.localAI[0] >= 30)
            {
                float toX = player.Center.X - projectile.Center.X;
                float toY = player.Center.Y - projectile.Center.Y;
                float num6 = (float)Math.Sqrt((double)(toX * toX + toY * toY));
                num6 = 9f / num6;
                toX *= num6;
                toY *= num6;

                projectile.velocity = new Vector2((projectile.velocity.X * 20f + toX) / 21f, (projectile.velocity.Y * 20f + toY) / 21f);

                // double angle = Math.Atan2(player.position.Y - projectile.position.Y, player.position.X - projectile.position.X);
                //projectile.velocity += new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 0.2f;

            }

            if (projectile.Hitbox.Intersects(new Rectangle((int)player.Center.X - 4, (int)player.Center.Y - 4, 8, 8)))
            {
                projectile.Kill();
            }

            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, GetDustID());
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;
        }
        private int GetDustID()
        {
            int dustType = mod.DustType("AncientRed");
            switch (Main.rand.Next(4))
            {
                case 0:
                    return mod.DustType("AncientRed");
                case 1:
                    return mod.DustType("AncientGreen");
                case 2:
                    return mod.DustType("AncientBlue");
                case 3:
                    return mod.DustType("AncientPink");
                default:
                    return mod.DustType("AncientRed");
            }
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}