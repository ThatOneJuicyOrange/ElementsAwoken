using System;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.VoidLeviathan
{
    public class VoidRunes : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 20;
            projectile.penetrate = -1;

            projectile.hostile = true;
            projectile.tileCollide = false;

            projectile.alpha = 255;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rune of Void");
            Main.projFrames[projectile.type] = 20;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frame = (int)projectile.ai[0];
            return true;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 1f, 0.2f, 0.55f);

            if (projectile.localAI[0] == 0)
            {
                projectile.ai[0] = Main.rand.Next(20);
                projectile.localAI[0]++;
            }

            projectile.ai[1]++;
            int timeAlive = Main.expertMode ? 160 : 200;
            if (MyWorld.awakenedMode) timeAlive = 120;
            if (projectile.ai[1] < 60)
            {
                projectile.alpha -= 255 / 60;
            }
            if (projectile.ai[1] > timeAlive)
            {
                projectile.Kill();
            }

            if (!ModContent.GetInstance<Config>().lowDust)
            {
                int maxDist = (int)(projectile.width * 1.2f);
                int numDust = (int)MathHelper.Lerp(15, 0, (float)projectile.alpha / 255f);
                for (int i = 0; i < numDust; i++)
                {
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                    Dust dust = Main.dust[Dust.NewDust(projectile.Center + offset - Vector2.One * 4, 0, 0, DustID.PinkFlame, 0, 0, 100)];
                    dust.noGravity = true;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            if (!ModContent.GetInstance<Config>().lowDust) ProjectileUtils.HostileExplosion(projectile, new int[] { DustID.PinkFlame }, projectile.damage);
            else
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("VoidRuneExplosion"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (ModContent.GetInstance<Config>().lowDust)
            {
                Texture2D auraTex = mod.GetTexture("Projectiles/NPCProj/VoidLeviathan/VoidRunesCircle");
                Vector2 drawOrigin = new Vector2(auraTex.Width * 0.5f, auraTex.Height * 0.5f);
                Vector2 drawPos = projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
                spriteBatch.Draw(auraTex, drawPos, null, Color.White * (1-((float)projectile.alpha / 255f)), 0f, drawOrigin, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}