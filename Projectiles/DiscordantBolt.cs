using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class DiscordantBolt : ModProjectile
    {
        int toxinTimer = 10;
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 24;
            projectile.scale = 1.0f;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.alpha = 0;
            projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discordant Bolt");
            Main.projFrames[projectile.type] = 4;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Discord"), 120);
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 4)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > Main.projFrames[projectile.type] - 1)
                    projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 75);
            Main.dust[dust].velocity *= 0.1f;
            Main.dust[dust].scale *= 1.5f;
            Main.dust[dust].noGravity = true;
            dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135);

            toxinTimer--;
            if (toxinTimer <=0)
            {
                int numberProjectiles = 2;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 value15 = new Vector2((float)Main.rand.Next(-12, 12), (float)Main.rand.Next(-12, 12));
                    value15.X *= 0.25f;
                    value15.Y *= 0.25f;
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value15.X, value15.Y, mod.ProjectileType("DiscordantToxin"), projectile.damage, 2f, projectile.owner, 0f, 0f);
                }
                toxinTimer = 10 + Main.rand.Next(0,10);
            }
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, new int[] { 75, 135 }, damageType: "magic");
        }
    }
}