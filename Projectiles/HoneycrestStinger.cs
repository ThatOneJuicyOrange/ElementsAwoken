using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class HoneycrestStinger : ModProjectile
    {       
        public override string Texture { get { return "ElementsAwoken/Projectiles/StingerP"; } }
    public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;

            projectile.friendly = true;
            projectile.melee = true;

            projectile.timeLeft = 600;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stinger");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 80, false);
            target.AddBuff(BuffID.Venom, 80, false);
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            projectile.ai[0]++;
            if (projectile.ai[0] >= 60) projectile.velocity.Y += 0.08f;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                float alpha = (1 - projectile.alpha / 255) - ((float)k / (float)projectile.oldPos.Length);
                float scale = 1 - ((float)k / (float)projectile.oldPos.Length);
                Color color = Color.Lerp(Color.White, new Color(245, 224, 66), (float)k / (float)projectile.oldPos.Length) * alpha;
                sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale * scale, SpriteEffects.None, 0f);
            }
            return false;
        }
    }
}