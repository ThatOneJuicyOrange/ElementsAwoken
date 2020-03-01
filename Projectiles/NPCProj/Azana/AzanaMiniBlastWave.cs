using System;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Azana
{
    public class AzanaMiniBlastWave : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/NPCProj/Azana/AzanaMiniBlast"; } }

        float waveAI0 = 0;
        float waveAI1 = 0;
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.penetrate = 1;

            projectile.hostile = true;
            projectile.tileCollide = false;

            projectile.timeLeft = 600;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Blast");
            Main.projFrames[projectile.type] = 4;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
            Texture2D tex = Main.projectileTexture[projectile.type];
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Rectangle rectangle = new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type]);
                sb.Draw(tex, drawPos, rectangle, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void AI()
        {

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            if (Main.rand.NextBool(5))
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 127, 0f, 0f, 100, default(Color), 1f)];
                dust.velocity *= 0.3f;
                dust.fadeIn = 0.9f;
                dust.noGravity = true;
            }
            if (projectile.ai[0] == 1)
            {
                projectile.ai[1]--;
                if (projectile.ai[1] <= 0)
                {
                    Projectile.NewProjectile(projectile.Center, projectile.velocity.RotatedByRandom(MathHelper.ToRadians(10)) * 0.2f, mod.ProjectileType("AzanaCloud"), projectile.damage, projectile.knockBack, Main.myPlayer);
                    projectile.ai[1] = Main.rand.Next(20,40);
                }
            }

            float rotateIntensity = 2f;
            float waveTime = 18f;
            waveAI0++;
            if (waveAI1 == 0) // this part is to fix the offset (it is still slightlyyyy offset)
            {
                if (waveAI0 > waveTime * 0.5f)
                {
                    waveAI0 = 0;
                    waveAI1 = 1;
                }
                else
                {
                    Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(-rotateIntensity));
                    projectile.velocity = perturbedSpeed;
                }
            }
            else
            {
                if (waveAI0 <= waveTime)
                {
                    Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(rotateIntensity));
                    projectile.velocity = perturbedSpeed;
                }
                else
                {
                    Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(-rotateIntensity));
                    projectile.velocity = perturbedSpeed;
                }
                if (waveAI0 >= waveTime * 2)
                {
                    waveAI0 = 0;
                }
            }
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(mod.BuffType("ChaosBurn"), 180);
        }
        public override void Kill(int timeLeft)
        {
            ProjectileGlobal.HostileExplosion(projectile, new int[] { 127 }, projectile.damage);
        }
    }
}