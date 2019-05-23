using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Aqueous
{
    public class AquanadoBolt : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.ignoreWater = false;
            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.timeLeft = 250;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquanado");
        }
        public override void AI()
        {
            projectile.velocity.Y += 0.1f;
            for (int num121 = 0; num121 < 2; num121++)
            {
                int num464 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 59, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[num464].noGravity = true;
                Dust dust = Main.dust[num464];
                dust.velocity *= 0.5f;
                dust = Main.dust[num464];
                dust.velocity += projectile.velocity * 0.1f;
            }
            if (projectile.wet)
            {
                projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(4, (int)projectile.Center.X, (int)projectile.Center.Y, 19, 1f, 0f);
            int num326 = 36;
            int num3;
            for (int num327 = 0; num327 < num326; num327 = num3 + 1)
            {
                Vector2 vector16 = Vector2.Normalize(projectile.velocity) * new Vector2((float)projectile.width / 2f, (float)projectile.height) * 0.75f;
                vector16 = vector16.RotatedBy((double)((float)(num327 - (num326 / 2 - 1)) * 6.28318548f / (float)num326), default(Vector2)) + projectile.Center;
                Vector2 vector17 = vector16 - projectile.Center;
                int num328 = Dust.NewDust(vector16 + vector17, 0, 0, 172, vector17.X * 2f, vector17.Y * 2f, 100, default(Color), 1.4f);
                Main.dust[num328].noGravity = true;
                Main.dust[num328].noLight = true;
                Main.dust[num328].velocity = vector17;
                num3 = num327;
            }
            if (projectile.owner == Main.myPlayer)
            {
                if (projectile.ai[1] < 1f)
                {
                    int num329 = Main.expertMode ? 25 : 40;
                    int num330 = Projectile.NewProjectile(projectile.Center.X - (float)(projectile.direction * 30), projectile.Center.Y - 4f, (float)(-(float)projectile.direction) * 0.01f, 0f, mod.ProjectileType("Aquanado"), num329, 4f, projectile.owner, 16f, 15f);
                    Main.projectile[num330].netUpdate = true;
                }
                else
                {
                    int num331 = (int)(projectile.Center.Y / 16f);
                    int num332 = (int)(projectile.Center.X / 16f);
                    int num333 = 100;
                    if (num332 < 10)
                    {
                        num332 = 10;
                    }
                    if (num332 > Main.maxTilesX - 10)
                    {
                        num332 = Main.maxTilesX - 10;
                    }
                    if (num331 < 10)
                    {
                        num331 = 10;
                    }
                    if (num331 > Main.maxTilesY - num333 - 10)
                    {
                        num331 = Main.maxTilesY - num333 - 10;
                    }
                    for (int num334 = num331; num334 < num331 + num333; num334 = num3 + 1)
                    {
                        Tile tile = Main.tile[num332, num334];
                        if (tile.active() && (Main.tileSolid[(int)tile.type] || tile.liquid != 0))
                        {
                            num331 = num334;
                            break;
                        }
                        num3 = num334;
                    }
                    int num335 = Main.expertMode ? 50 : 80;
                    int num336 = Projectile.NewProjectile((float)(num332 * 16 + 8), (float)(num331 * 16 - 24), 0f, 0f, mod.ProjectileType("Aquanado"), num335, 4f, Main.myPlayer, 16f, 24f);
                    Main.projectile[num336].netUpdate = true;
                }
            }
        }
    }
}