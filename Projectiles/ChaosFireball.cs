using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ChaosFireball : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;

            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.ranged = true;

            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dying Azure");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            bool immune = false;
            if (ElementsAwoken.instakillImmune.Contains(target.type)) immune = true;

            if (target.active && !target.friendly && target.damage > 0 && !target.dontTakeDamage && !target.boss && Main.rand.Next(20) == 0 && target.lifeMax < 30000 && !immune && Main.rand.Next(10) == 0)
            {
                target.StrikeNPCNoInteraction(target.lifeMax, 0f, -target.direction, true);
                ProjectileUtils.Explosion(projectile, new int[] { 219 }, projectile.damage, "melee");
            }
            target.AddBuff(mod.BuffType("ChaosBurn"), 180);
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, new int[] { 127 }, projectile.damage, "melee");
        }
        public override void AI()
        {
            projectile.velocity.X *= 0.99f;
            projectile.velocity.Y *= 0.99f;
            Lighting.AddLight(projectile.Center, 0.9f, 0.1f, 0.2f);

            if (projectile.ai[0]==0)
            {
                projectile.scale = 0.01f;
                projectile.ai[0]++;
            }
            float scaleMax = 0.7f;
            if (projectile.scale < scaleMax)projectile.scale += scaleMax / 20f;
            else projectile.scale = scaleMax;
            projectile.rotation += 0.02f;

            if (!ModContent.GetInstance<Config>().lowDust)
            {
                ProjectileUtils.CreateDustRing(projectile,127, 21, 1);

            }
            ProjectileUtils.Home(projectile, 6f);
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Texture2D shell = ModContent.GetTexture("ElementsAwoken/Projectiles/ChaosFireballShell");
            Vector2 shellOrigin = new Vector2(shell.Width * 0.5f, shell.Height * 0.5f);
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, Main.projectileTexture[projectile.type].Height * 0.5f);
            Vector2 drawPos = projectile.Center  - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
            Color color = projectile.GetAlpha(lightColor);
            sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, 0, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            sb.Draw(shell, drawPos, null, color, projectile.rotation, shellOrigin, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}