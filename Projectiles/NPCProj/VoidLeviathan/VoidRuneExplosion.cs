using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Explosions
{
    public class VoidRuneExplosion : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Explosions/LightsAfflictionExplosion"; } }

        public override void SetDefaults()
        {
            projectile.width = 98;
            projectile.height = 98;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 40;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Rune");
            Main.projFrames[projectile.type] = 7;

        }
        public override bool CanHitPlayer(Player target)
        {
            if (projectile.ai[0] > 5) return false;
            return base.CanHitPlayer(target);
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(mod.BuffType("ExtinctionCurse"), 80, true);
        }
        public override void AI()
        {
            projectile.ai[0]++;

            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1.5f;
            Main.dust[dust].velocity *= 1f;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 3)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 6)
                    projectile.Kill();
            }
            return true;
        }
    }
}