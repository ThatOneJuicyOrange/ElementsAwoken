using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ElementsAwoken.Projectiles.GlobalProjectiles;

namespace ElementsAwoken.Projectiles.NPCProj.VoidLeviathan
{
    public class VoidBolt : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.penetrate = -1;
            projectile.scale *= 1.4f;

            projectile.hostile = true;

            projectile.timeLeft = 300;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Leviathan Bolt");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 1f, 0.2f, 0.55f);

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (!ModContent.GetInstance<Config>().lowDust && Main.rand.Next(2) == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 6f;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(mod.BuffType("ExtinctionCurse"), 80, true);
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