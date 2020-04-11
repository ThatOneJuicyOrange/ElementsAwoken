using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.Permafrost
{
    public class IceMagic : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 42;
            projectile.height = 44;

            projectile.hostile = true;
            projectile.tileCollide = false;

            projectile.penetrate = 1;
            projectile.timeLeft = 600;
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Magic");
            Main.projFrames[projectile.type] = 5;
        }
        public override void AI()
        {
            projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(1f));
            projectile.ai[0]++;
            if (projectile.ai[0] >= 180)
            {
                projectile.velocity *= 0.96f;
            }
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Frostburn, 120, false);
        }
        public override void Kill(int timeLeft)
        {
            if (!ModContent.GetInstance<Config>().lowDust)
            {
                // make dust in an expanding circle
                int numDusts = 30;
                for (int i = 0; i < numDusts; i++)
                {
                    Vector2 position = (Vector2.One * new Vector2((float)projectile.width / 2f, (float)projectile.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + projectile.Center;
                    Vector2 velocity = position - projectile.Center;
                    int dust = Dust.NewDust(position + velocity, 0, 0, 135, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity = Vector2.Normalize(velocity) * 5f;
                }
            }
        }
    }
}