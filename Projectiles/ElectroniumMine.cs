using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ElectroniumMine : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;

            projectile.penetrate = 3;

            projectile.friendly = true;
            projectile.ranged = true;
            projectile.tileCollide = false;

            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mine");
        }
        public override void AI()
        {
            if ((projectile.velocity.X > 0.5f || projectile.velocity.X < -0.5f) || (projectile.velocity.Y > 0.5f || projectile.velocity.Y < -0.5f))
            {
                for (int i = 0; i < 2; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 6)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= projectile.velocity / 6f * (float)i;
                    dust.noGravity = true;
                    dust.scale = 1f;
                }
            }
            // tile stick
            try
            {
                int num187 = (int)(projectile.position.X / 16f) - 1;
                int num188 = (int)((projectile.position.X + (float)projectile.width) / 16f) + 2;
                int num189 = (int)(projectile.position.Y / 16f) - 1;
                int num190 = (int)((projectile.position.Y + (float)projectile.height) / 16f) + 2;
                if (num187 < 0)
                {
                    num187 = 0;
                }
                if (num188 > Main.maxTilesX)
                {
                    num188 = Main.maxTilesX;
                }
                if (num189 < 0)
                {
                    num189 = 0;
                }
                if (num190 > Main.maxTilesY)
                {
                    num190 = Main.maxTilesY;
                }
                int num3;
                for (int num191 = num187; num191 < num188; num191 = num3 + 1)
                {
                    for (int num192 = num189; num192 < num190; num192 = num3 + 1)
                    {
                        if (Main.tile[num191, num192] != null && Main.tile[num191, num192].nactive() && !TileID.Sets.Platforms[Main.tile[num191, num192].type] &&  (Main.tileSolid[(int)Main.tile[num191, num192].type] || (Main.tileSolidTop[(int)Main.tile[num191, num192].type] && Main.tile[num191, num192].frameY == 0)))
                        {
                            Vector2 vector18;
                            vector18.X = (float)(num191 * 16);
                            vector18.Y = (float)(num192 * 16);
                            if (projectile.position.X + (float)projectile.width > vector18.X && projectile.position.X < vector18.X + 16f && projectile.position.Y + (float)projectile.height > vector18.Y && projectile.position.Y < vector18.Y + 16f)
                            {
                                projectile.velocity.X = 0f;
                                projectile.velocity.Y = -0.2f;
                            }
                        }
                        num3 = num192;
                    }
                    num3 = num191;
                }
            }
            catch
            {
            }

            if (projectile.ai[1] != 0)
            {
                NPC stick = Main.npc[(int)projectile.ai[0]];
                if (stick.active)
                {
                    projectile.Center = stick.Center - projectile.velocity * 2f;
                    projectile.gfxOffY = stick.gfxOffY;
                }
                else projectile.Kill();
            }
            else
            {
                projectile.velocity.Y += 0.13f;
                projectile.rotation += 0.2f * (projectile.velocity.X * 0.2f);
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.ai[1] == 1) return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[0] = target.whoAmI;
            projectile.ai[1] = 1;
            projectile.velocity =(target.Center - projectile.Center) * 0.75f;
            projectile.netUpdate = true;
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, new int[] { 6 }, projectile.damage, "ranged");
        }
    }
}