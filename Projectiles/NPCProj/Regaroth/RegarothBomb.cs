using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Regaroth
{
    public class RegarothBomb : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;

            projectile.hostile = true;
            projectile.tileCollide = false;

            projectile.timeLeft = 600;
            projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Regaroth Bomb");
            Main.projFrames[projectile.type] = 4;
        }
        public override void AI()
        {
            Main.projFrames[projectile.type] = 4;

            projectile.velocity.X *= 0.99f;
            projectile.velocity.Y *= 0.99f;
            // create dusts in a circle shape
            if (Main.rand.Next(6) == 0 && !ModContent.GetInstance<Config>().lowDust)
            {
                int dustType = Main.rand.Next(2) == 0 ? 135 : 164;
                Vector2 position = projectile.Center + Main.rand.NextVector2Circular(projectile.width * 0.5f, projectile.height * 0.5f);
                Dust dust = Dust.NewDustPerfect(position, dustType, Vector2.Zero);
                dust.noGravity = true;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            projectile.Kill();
            ProjectileUtils.HostileExplosion(projectile, new int[] { 135, 164}, projectile.damage);
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 4)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
            return true;
        }
    }
}