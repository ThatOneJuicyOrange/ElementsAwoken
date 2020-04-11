using ElementsAwoken.Buffs.Debuffs;
using ElementsAwoken.Items.BossDrops.Azana;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Projectiles.Bullets
{
    public class OutbreakDart : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;

            projectile.aiStyle = 1;

            projectile.friendly = true;
            projectile.ranged = true;

            projectile.penetrate = 1;

            projectile.timeLeft = 600;

            projectile.extraUpdates = 1;

            aiType = ProjectileID.Bullet;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Outbreak Dart");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffType<ChaosBurn>(), 300, false);
            Player player = Main.player[projectile.owner];
            Item held = player.HeldItem;
            if (held.type == ItemType<ChaoticGaze>())
            {
                ChaoticGaze gaze = (ChaoticGaze)held.modItem;
                gaze.hitCount++;
                gaze.hitTimer = 30;
                if (gaze.hitCount <= 60) CombatText.NewText(player.getRect(), Color.PaleVioletRed, gaze.hitCount, false, false);
            }
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.3f, 0.9f, 0.6f);
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
