using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Volcanox
{
    public class VolcanicDemon : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.scale = 1.0f;
            projectile.width = 40;
            projectile.height = 40;
            projectile.penetrate = 1;

            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;

            projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Volcanic Demon");
            Main.projFrames[projectile.type] = 4;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 3.14f;
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;
            }
            Vector2 vector = projectile.velocity.SafeNormalize(Vector2.UnitY);
            float num2 = projectile.ai[0] / 60f;
            float num3 = 2f;
            int num4 = 0;
            while ((float)num4 < num3)
            {
                Dust expr_20B = Dust.NewDustDirect(projectile.Center, 14, 14, 6, 0f, 0f, 110, default(Color), 1f);
                expr_20B.velocity = vector * 2f;
                expr_20B.position = projectile.Center + vector.RotatedBy((double)(num2 * 6.28318548f * 2f + (float)num4 / num3 * 6.28318548f), default(Vector2)) * 7f;
                expr_20B.scale = 1f + 0.6f * Main.rand.NextFloat();
                expr_20B.velocity += vector * 3f;
                expr_20B.noGravity = true;
                num4++;
            }

            if (projectile.timeLeft <= 50)
            {
                if (Main.rand.Next(12) == 0)
                {
                    projectile.Kill();
                }
            }
            if (Vector2.Distance(Main.player[Main.myPlayer].Center, projectile.Center) <= 50)
            {
                if (Main.rand.Next(6) == 0)
                {
                    projectile.Kill();
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("VolcanicBoom"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.OnFire, 200);
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 2)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > Main.projFrames[projectile.type] - 1)
                    projectile.frame = 0;
            }
            return true;
        }
    }
}