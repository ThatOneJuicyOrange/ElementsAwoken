﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.TheGuardian
{
    public class GuardianStrike : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;

            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;

            projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Guardian");
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
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;
            if (projectile.ai[1] == 0)
            {
                projectile.tileCollide = false;
            }
            else
            {
                
            }
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.OnFire, 200);
        }
        public override void Kill(int timeLeft)
        {
            if (projectile.ai[1] == 1)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("ExplosionHostile"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
                for (int num369 = 0; num369 < 20; num369++)
                {
                    int num370 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num370].velocity *= 1.4f;
                }
                for (int num371 = 0; num371 < 10; num371++)
                {
                    int num372 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2.5f);
                    Main.dust[num372].noGravity = true;
                    Main.dust[num372].velocity *= 5f;
                    num372 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num372].velocity *= 3f;
                }
                int num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num373].velocity *= 0.4f;
                Gore gore85 = Main.gore[num373];
                gore85.velocity.X = gore85.velocity.X + 1f;
                Gore gore86 = Main.gore[num373];
                gore86.velocity.Y = gore86.velocity.Y + 1f;
                num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num373].velocity *= 0.4f;
                Gore gore87 = Main.gore[num373];
                gore87.velocity.X = gore87.velocity.X - 1f;
                Gore gore88 = Main.gore[num373];
                gore88.velocity.Y = gore88.velocity.Y + 1f;
                num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num373].velocity *= 0.4f;
                Gore gore89 = Main.gore[num373];
                gore89.velocity.X = gore89.velocity.X + 1f;
                Gore gore90 = Main.gore[num373];
                gore90.velocity.Y = gore90.velocity.Y - 1f;
                num373 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num373].velocity *= 0.4f;
                Gore gore91 = Main.gore[num373];
                gore91.velocity.X = gore91.velocity.X - 1f;
                Gore gore92 = Main.gore[num373];
                gore92.velocity.Y = gore92.velocity.Y - 1f;
                for (int i = 0; i < 6; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(2f, 2f).RotatedByRandom(MathHelper.ToRadians(360));
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("GuardianFire"), (int)(projectile.damage * 0.75f), 0f, 0);
                }
            }
        }
    }
}