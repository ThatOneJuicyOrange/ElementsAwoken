using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Obsidious
{
    public class ObsidiousFireballVoid : ModProjectile
    {
        private float[] moreAi = new float[2];
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;

            projectile.hostile = true;
            projectile.tileCollide = true;

            projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Voidbroken Fireball");
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300, false);
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            projectile.localAI[1]++;
            if (projectile.localAI[1] < 120)
            {
                NPC parent = Main.npc[(int)projectile.ai[0]];
                /*double angle = Math.Atan2(Main.player[parent.target].position.Y - projectile.position.Y, Main.player[parent.target].position.X - projectile.position.X);
                if (Math.Abs(projectile.velocity.X) < 16f) projectile.velocity.X += (float)Math.Cos(angle) * 1f;
                if (Math.Abs(projectile.velocity.Y) < 16f) projectile.velocity.Y += (float)Math.Sin(angle) * 1f;*/

                float speed = MathHelper.Lerp(0, projectile.ai[1], MathHelper.Clamp(projectile.localAI[1] / 60, 0, 1));
                float goToX = Main.player[parent.target].Center.X - projectile.Center.X;
                float goToY = Main.player[parent.target].Center.Y - projectile.Center.Y;
                float dist = (float)Math.Sqrt((double)(goToX * goToX + goToY * goToY));
                dist = speed / dist;
                goToX *= dist;
                goToY *= dist;
                projectile.velocity.X = (projectile.velocity.X * 20f + goToX) / 21f;
                projectile.velocity.Y = (projectile.velocity.Y * 20f + goToY) / 21f;

            }
            Lighting.AddLight(projectile.Center, 0.9f, 0.4f, 0.9f);


            if (Main.rand.NextBool(5))
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 1.5f;
                Main.dust[dust].noGravity = true;
            }
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 sinOff = new Vector2((float)Math.Sin(projectile.localAI[1] / 5 + (k * 0.8f)) * k, 0).RotatedBy(projectile.rotation) ;
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY) + sinOff;
                float alpha = 1 - ((float)k / (float)projectile.oldPos.Length);
                float scale = 1 - ((float)k / (float)projectile.oldPos.Length);
                Color color = Color.Lerp(Color.White, new Color(138, 40, 134), (float)k / (float)projectile.oldPos.Length) * alpha;
                sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale * scale, SpriteEffects.None, 0f);
            }
            sb.End();
            sb.Begin(default, BlendState.Additive);

            Texture2D tex = ModContent.GetTexture("ElementsAwoken/Extra/Bloom");
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 sinOff = new Vector2((float)Math.Sin(projectile.localAI[1] / 5 + (k * 0.8f)) * k, 0).RotatedBy(projectile.rotation);
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY) + sinOff; Color color = new Color(225, 159, 227) * 0.6f * (1 - ((float)k / (float)projectile.oldPos.Length));
                float scale = projectile.scale - ((float)k / (float)projectile.oldPos.Length);
                scale *= 0.6f;
                sb.Draw(tex, drawPos, null, color, 0, tex.Size() / 2, scale, SpriteEffects.None, 0f);
                sb.Draw(tex, drawPos, null, color, 0, tex.Size() / 2, scale * 0.3f, SpriteEffects.None, 0f); // little bit of extra glow to make the projectiles shine more
            }
            sb.End();
            sb.Begin();

            return false;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.DD2_BetsyFireballImpact, projectile.position);
            Main.PlaySound(4, (int)projectile.position.X, (int)projectile.position.Y, 6, pitchOffset: -0.5f);
            ProjectileUtils.HostileExplosion(projectile, new int[] { DustID.PinkFlame }, projectile.damage, soundID: 0);
        }
    }
}