using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class VoxusP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;

            projectile.friendly = true;
            projectile.ranged = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;

            projectile.penetrate = 4;

            projectile.timeLeft = 200;

            projectile.light = 2f;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Voxus");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            int numberProjectiles = 1;
            for (int num131 = 0; num131 < numberProjectiles; num131++)
            {
                int num1 = Main.rand.Next(-30, 30);
                int num2 = Main.rand.Next(300, 500);
                int type = mod.ProjectileType("Voxus1");
                switch (Main.rand.Next(4))
                {
                    case 0: type = mod.ProjectileType("Voxus1"); break;
                    case 1: type = mod.ProjectileType("Voxus2"); break;
                    case 2: type = mod.ProjectileType("Voxus3"); break;
                    case 3: type = mod.ProjectileType("Voxus4"); break;
                    default: break;
                }
                Projectile.NewProjectile(projectile.Center.X + num1, projectile.Center.Y - num2, 0, 20, type, projectile.damage, 0, projectile.owner);
                int num3 = Main.rand.Next(-500, -300);
                Projectile.NewProjectile(projectile.Center.X + num1, projectile.Center.Y - num3, 0, -20, type, projectile.damage, 0, projectile.owner);
            }
        }
    }
}