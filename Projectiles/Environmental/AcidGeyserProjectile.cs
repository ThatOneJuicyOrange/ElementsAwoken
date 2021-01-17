using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Environmental
{
    public class AcidGeyserProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.hostile = true;

            projectile.timeLeft = 300;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 30;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;

            projectile.extraUpdates = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eruption");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (Main.rand.NextBool(8))
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 74, -projectile.velocity.X, -projectile.velocity.Y)];
                dust.noGravity = true;
                dust.velocity *= 0.5f;
            }
                if (projectile.velocity.Y < 6) projectile.velocity.Y += 0.06f;
            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.75f * 0.5f) / 255f, ((255 - projectile.alpha) * 0.95f * 0.5f) / 255f, ((255 - projectile.alpha) * 0.5f * 0.5f) / 255f);
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                float scale = 1 - ((float)k / (float)projectile.oldPos.Length);
                sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCHit18, (int)projectile.position.X, (int)projectile.position.Y);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/AcidHiss"));
            target.AddBuff(ModContent.BuffType<Buffs.Debuffs.AcidBurn>(), Main.hardMode ? 300 : 60);
        }
    }
}