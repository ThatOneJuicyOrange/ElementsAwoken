using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Buffs.Debuffs;

namespace ElementsAwoken.Projectiles.Environmental
{
    public class SulfurCloud : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 34;

            projectile.hostile = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 600;
            projectile.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sulfuric Gas");
            Main.projFrames[projectile.type] = 5;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 4)
                    projectile.frame = 0;
            }
            return true;
        }
        public override bool CanHitPlayer(Player target)
        {
            if (projectile.alpha > 170) return false;
            return base.CanHitPlayer(target);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Debuffs.AcidBurn>(), 300);
        }
        public override void AI()
        {
            projectile.rotation -= 0.08f * projectile.localAI[0];
            projectile.ai[1]++;
            int lowestAlpha = 150;
            int diff = 255 - lowestAlpha;
            if (projectile.ai[1] < 120)
            {
                if (projectile.alpha > lowestAlpha) projectile.alpha -= diff / 20;
            }
            else
            {
                projectile.alpha++;
                if (projectile.alpha >= 255) projectile.Kill();
            }
        }

    }
}