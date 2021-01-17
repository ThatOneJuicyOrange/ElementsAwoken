using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Obsidious
{
    public class ObsidiousFireball : ModProjectile
    {
        private float[] moreAi = new float[2];
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;

            projectile.hostile = true;
            projectile.tileCollide = false;

            projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fireball");
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300, false);
        }
        public override void AI()
        {
            if (moreAi[1] == 0)
            {
                //moreAi[1] = projectile.ai[1] / (360f / 8f) + 1;
                moreAi[1] = Main.rand.Next(1,100);
            }
            moreAi[1]++;
            moreAi[0] += 3;
            projectile.ai[1] += 3f;

            if (projectile.ai[1] > 600)
            {
                if (projectile.velocity == Vector2.Zero)
                {
                    if (moreAi[1] >= 300)
                    {
                        Main.PlaySound(SoundID.DD2_BetsyFireballShot, projectile.position);
                        NPC parent = Main.npc[(int)projectile.ai[0]];
                        double angle = Math.Atan2(Main.player[parent.target].position.Y - projectile.position.Y, Main.player[parent.target].position.X - projectile.position.X);
                        projectile.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 16f;
                        projectile.ai[0] = 0;
                    }
                }
                else 
                {
                    if (!Collision.SolidCollision(projectile.position, projectile.width, projectile.height)) projectile.tileCollide = true;
                    projectile.ai[0]++;
                }
            }
            if (projectile.velocity == Vector2.Zero)
            {
                NPC parent = Main.npc[(int)projectile.ai[0]];
                int distance = (int)Math.Min(140,moreAi[0]);
                double rad = projectile.ai[1] * (Math.PI / 180);
                projectile.Center = parent.Center - new Vector2((int)(Math.Cos(rad) * distance), (int)(Math.Sin(rad) * distance));

            }

            Lighting.AddLight(projectile.Center, 0.9f, 0.2f, 0.4f);

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            if (Main.rand.NextBool(5))
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire);
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
                    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                float alpha = 1 - ((float)k / (float)projectile.oldPos.Length);
                float scale = 1 - ((float)k / (float)projectile.oldPos.Length);
                Color color = Color.Lerp(Color.White, new Color(252, 32, 3), (float)k / (float)projectile.oldPos.Length) * alpha;
                sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale * scale, SpriteEffects.None, 0f);
            }
            sb.End();
            sb.Begin(default, BlendState.Additive);

            Texture2D tex = ModContent.GetTexture("ElementsAwoken/Extra/Bloom");
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Color color = new Color(230, 202, 161) * 0.6f * (1 - ((float)k / (float)projectile.oldPos.Length));
                float scale = projectile.scale - ((float)k / (float)projectile.oldPos.Length);
                scale *= 0.6f;
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + projectile.Size / 2 + new Vector2(0f, projectile.gfxOffY);
                sb.Draw(tex, drawPos, null, color, 0, tex.Size() / 2, scale, SpriteEffects.None, 0f);
                sb.Draw(tex, drawPos, null, color, 0, tex.Size() / 2, scale * 0.3f, SpriteEffects.None, 0f); // little bit of extra glow to make the projectiles shine more
            }
            int shine = 30;
            if (projectile.ai[1] > 600 && projectile.velocity != Vector2.Zero && projectile.ai[0] < shine)
            {
                Vector2 drawPos = projectile.position - Main.screenPosition + projectile.Size / 2 + new Vector2(0f, projectile.gfxOffY);
                sb.Draw(tex, drawPos, null, new Color(230, 202, 161), 0, tex.Size() / 2, projectile.scale * (float)Math.Sin((projectile.ai[0] / shine) * 3.14f) * 0.8f, SpriteEffects.None, 0f);
            }
            sb.End();
            sb.Begin();

            return false;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.DD2_BetsyFireballImpact, projectile.position);
            ProjectileUtils.HostileExplosion(projectile, new int[] { 6 }, projectile.damage, soundID: 0);
        }
    }
}